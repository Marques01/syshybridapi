using System.Net.NetworkInformation;

namespace Utils
{
	public class MACAdress
	{
		public static IEnumerable<string> GetUserMAC()
		{	
			NetworkInterface[] networkInterface = NetworkInterface.GetAllNetworkInterfaces();
			
			List<string> macAdressList = new();

			for (int i = 0; i < networkInterface.Length; i++)
			{
				string macAdress = networkInterface[i].GetPhysicalAddress().ToString();

				if (!string.IsNullOrEmpty(macAdress))
					macAdressList.Add(macAdress);
			}

			return macAdressList;
		}
	}
}
