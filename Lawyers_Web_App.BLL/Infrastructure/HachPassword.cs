using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Lawyers_Web_App.BLL.Infrastructure
{
    public class HachPassword
    {
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.Default.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
