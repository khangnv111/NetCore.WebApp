using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Lib.Security
{
    public class Encrypt
    {
        public static string Md5(string s)
        {
            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
            var md5 = new MD5CryptoServiceProvider();
            var originalBytes = Encoding.Default.GetBytes(s);
            var encodedBytes = md5.ComputeHash(originalBytes);

            //Convert encoded bytes back to a 'readable' string
            return BitConverter.ToString(encodedBytes).ToLower().Replace("-", "");
        }

    }
}
