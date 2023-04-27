using BLL.Models;
using BLL.Repository.Interfaces;
using DAL.Context;
using Microsoft.EntityFrameworkCore;
using Utils;
using Utils.Logger;

namespace DAL.Repository
{
	public class UserRepository : IUserRepository
	{
		private readonly ApplicationDbContext _context;

		public UserRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task CreateAsync(User user)
		{
			try
			{
				user.Password = Encryption.GenerateHash(user.Password.Trim());

				user.Login = user.Login.Trim().ToLower();

				await _context.Users.AddAsync(user);
			}
			catch (Exception ex)
			{
				string errorMessage = $"Não foi possível registrar o usuário\t";

				await RegisterLogs.CreateAsync(errorMessage += ex.Message, this.GetType().ToString());

				throw new Exception(errorMessage);
			}
		}

		public async Task<User> GetUserByIdAsync(int id)
		{
			try
			{
				var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId.Equals(id));

				if (user is not null)
					return user;

				return new User();
			}
			catch (Exception ex)
			{
				string errorMessage = $"Não foi possível buscar o usuário pelo id\t";

				await RegisterLogs.CreateAsync(errorMessage += ex.Message, this.GetType().ToString());

				throw new Exception(errorMessage);
			}
		}

		public async Task<User> GetUserByLoginAsync(string mail)
		{
			try
			{
				var user = await _context.Users.FirstOrDefaultAsync(x => x.Login.Equals(mail.Trim().ToLower()));

				if (user is not null)
					return user;

				return new User();
			}
			catch (Exception ex)
			{
				string errorMessage = $"Não foi possível buscar o usuário pelo email\t";

				await RegisterLogs.CreateAsync(errorMessage += ex.Message, this.GetType().ToString());

				throw new Exception(errorMessage);
			}
		}

		public async Task<BaseModel> SignInAsync(UserDto user)
		{
			try
			{
				user.Password = Encryption.GenerateHash(user.Password);

				var userValid = await _context.Users
					.FirstOrDefaultAsync(x => x.Login.Equals(user.Email.Trim().ToLower()) && x.Password.Equals(user.Password.Trim()));

				if (userValid is not null && !userValid.Enabled)
					return new BaseModel() { IsSuccess = false, Message = "Login desabilitado." };

				if (userValid is not null && userValid.FailedCount <= 3)
					return new BaseModel() { IsSuccess = true, Message = "Login realizado com sucesso" };

				if (userValid is not null && userValid.FailedCount >= 3)
					return new BaseModel() { IsSuccess = false, Message = "Limite de tentativas excedida. Entre em contato com o administrador do sistema." };

				return new BaseModel() { IsSuccess = false, Message = "Login inválido." };
			}
			catch (Exception ex)
			{
				string errorMessage = $"Não foi possível verificar a existência do usuário\t";

				await RegisterLogs.CreateAsync(errorMessage += ex.Message, this.GetType().ToString());

				throw new Exception(errorMessage);
			}
		}

		public async Task UpdateAsync(User user)
		{
			try
			{
				_context.Users.Update(user);
			}
			catch (Exception ex)
			{
				string errorMessage = $"Não foi possível atualizar as informações do usuário\t";

				await RegisterLogs.CreateAsync(errorMessage += ex.Message, this.GetType().ToString());

				throw new Exception(errorMessage);
			}
		}

		public async Task<bool> UserExistingAsync(User user)
		{
			try
			{
				var userExists = await _context.Users
					.AsNoTracking()
					.FirstOrDefaultAsync(x => x.Login.Equals(user.Login.Trim().ToLower()) && x.Password.Equals(user.Password.Trim()));

				if (userExists is not null)
					return true;

				return false;
			}
			catch (Exception ex)
			{
				string errorMessage = $"Não foi possível verificar a existência do usuário\t";

				await RegisterLogs.CreateAsync(errorMessage += ex.Message, this.GetType().ToString());

				throw new Exception(errorMessage);
			}
		}
	}
}
