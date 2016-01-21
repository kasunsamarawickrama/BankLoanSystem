using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Text;

namespace BankLoanSystem.Code
{
    public class PasswordEncryption
    {
        public const int SALT_BYTE_SIZE = 24;

        public static Random random = new Random((int)DateTime.Now.Ticks);
        public static string RandomString()
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < 8; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

        public static string encryptPassword(string password , string saltFirst)
        {

            byte[] salt = Encoding.ASCII.GetBytes(saltFirst);
            var passwordEncryp = System.Text.Encoding.UTF8.GetBytes(password);

            var hmacMD5 = new HMACMD5(salt);
            var saltedHash = hmacMD5.ComputeHash(passwordEncryp);

            var saitPlusHash = Convert.ToBase64String(saltedHash) + ":" + saltFirst; 
            return saitPlusHash;
        }
    }
}