using System.Security.Cryptography;
using System.Text;

namespace Utils
{
    public class Encryption
    {
        public static string GenerateHash(string plainText)
        {
            using (SHA384 SHA384Hash = SHA384.Create())
            {
                // Computing Hash - returns here byte array
                byte[] bytes = SHA384Hash.ComputeHash(Encoding.UTF8.GetBytes(plainText));

                // now convert byte array to a string   
                StringBuilder stringbuilder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    stringbuilder.Append(bytes[i].ToString("x2"));
                }

                return stringbuilder.ToString();
            }
        }
    }
}