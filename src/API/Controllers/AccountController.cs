using BLL.Models;
using BLL.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Utils;

namespace API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AccountController : ControllerBase
	{
		private readonly IUnitOfWork _uof;

		public AccountController(IUnitOfWork uof)
		{
			_uof = uof;
		}

		[Route("login")]
		[HttpPost]
		public async Task<ActionResult> Signin([FromBody] UserDto userDto)
		{
			var userSignin = await _uof.UserRepository.SignInAsync(userDto);

			if (userSignin.IsSuccess)
			{
				var mac = MACAdress.GetUserMAC();

				var device = await _uof.DevicesRepository.GetDeviceByMAC(mac);

				if (device.DeviceId > 0)
				{
					var user = await _uof.UserRepository.GetUserByMailAsync(userDto.Email);					

					var token = await GenerateToken(user);

					user.LastLogin = DateTime.Now;

					await _uof.CommitAsync();

					return Ok(token);
				}

				return Ok(new UserToken() { Message = "Dispositivo não autorizado" });
			}

			if (!userSignin.IsSuccess)
			{
				var _user = await _uof.UserRepository.GetUserByMailAsync(userDto.Email);

				if (_user.UserId > 0)
				{
					_user.FailedCount = _user.FailedCount++;

					await _uof.CommitAsync();
				}
			}

			return Ok(new UserToken() { Message = userSignin.Message });
		}


		[Route("register")]
		[HttpPost]
		public async Task<ActionResult> Register([FromBody] User user)
		{
			var userExists = await _uof.UserRepository.UserExistingAsync(user);

			if (!userExists)
			{
				await _uof.UserRepository.CreateAsync(user);

				await _uof.CommitAsync();

				var mac = MACAdress.GetUserMAC();

				List<Devices> devices = new();

				foreach (var macAdress in mac)
				{
					devices.Add(new Devices()
					{
						Mac = macAdress,
						UserId = user.UserId,
					});
				}

				await _uof.DevicesRepository.CreateAsync(devices);

				await _uof.CommitAsync();

				return Ok("Usuário cadastrado com sucesso");
			}

			return Ok("Este usuário já está cadastrado");
		}

		private async Task<UserToken> GenerateToken(User userInfo)
		{
			try
			{
				var roles = await _uof.UserRoleRepository.GetRolesByUserId(userInfo.UserId);

				var authClaims = new List<Claim>();

				authClaims.Add(new Claim(ClaimTypes.Actor, $"{userInfo.FirstName} {userInfo.LastName}"));

				authClaims.Add(new Claim(ClaimTypes.Email, userInfo.Email));

				foreach (var item in roles)
				{
					authClaims.Add(new Claim(ClaimTypes.Role, item.Name));
				}

				var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtKey.Key));

				var creds =
				   new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

				DateTime expiration = DateTime.UtcNow.AddHours(6);

				string message = "success";

				JwtSecurityToken token = new JwtSecurityToken(
				issuer: null,
				audience: null,
				claims: authClaims,
				expires: expiration,
				signingCredentials: creds);

				return new UserToken()
				{
					Token = new JwtSecurityTokenHandler().WriteToken(token),
					Expiration = expiration,
					Message = message
				};
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			return new UserToken();
		}
	}
}
