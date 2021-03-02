using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace StartProject.Helper
{
    public class EncryptHelper
    {
        public static string UrlEncode(string value)
        {
            var tmp = value;
            return HttpUtility.UrlEncode(tmp, System.Text.Encoding.GetEncoding("utf-8"));
            //return WebUtility.UrlEncode(tmp);
        }

        public static string UrlDecode(string value)
        {
            var tmp = value;
            return HttpUtility.UrlDecode(tmp);
            //return WebUtility.UrlDecode(tmp);
        }

        public static string AES_encrypt(string ciphertext, string tokenKey, string tokenIV = null)
        {
            string encrypt = "";
            try
            {
                AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();
                byte[] key = sha256.ComputeHash(Encoding.UTF8.GetBytes(tokenKey));
                byte[] iv = md5.ComputeHash(Encoding.UTF8.GetBytes(tokenIV == null ? tokenKey : tokenIV));
                aes.Key = key;
                aes.IV = iv;

                byte[] dataByteArray = Encoding.UTF8.GetBytes(ciphertext);

                var transform = aes.CreateEncryptor();
                encrypt = Convert.ToBase64String(transform.TransformFinalBlock(dataByteArray, 0, dataByteArray.Length));
            }
            catch (Exception e)
            {

            }
            return encrypt;

        }

        public static string AES_decrypt(string ciphertext, string tokenKey, string tokenIV = null)
        {
            string decrypt = "";
            try
            {
                AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();
                byte[] key = sha256.ComputeHash(Encoding.UTF8.GetBytes(tokenKey));
                byte[] iv = md5.ComputeHash(Encoding.UTF8.GetBytes(tokenIV == null ? tokenKey : tokenIV));
                aes.Key = key;
                aes.IV = iv;

                byte[] dataByteArray = Convert.FromBase64String(ciphertext);

                var transform = aes.CreateDecryptor();
                decrypt = Encoding.UTF8.GetString(transform.TransformFinalBlock(dataByteArray, 0, dataByteArray.Length));
            }
            catch (Exception e)
            {

            }
            return decrypt;

        }


        public static string SHA256(string data)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            byte[] hash = SHA256Managed.Create().ComputeHash(bytes);

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                builder.Append(hash[i].ToString("X2"));
            }

            return builder.ToString();
        }


        //產生 HMACSHA256 雜湊
        public static string ComputeHMACSHA256(string data, string key)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key);
            using (var hmacSHA = new HMACSHA256(keyBytes))
            {
                var dataBytes = Encoding.UTF8.GetBytes(data);
                var hash = hmacSHA.ComputeHash(dataBytes, 0, dataBytes.Length);
                return BitConverter.ToString(hash).Replace("-", "").ToUpper();
            }
        }


        public static string MD5_String(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public static string Base64Decode(string txt)
        {
            byte[] decodedBytes = Convert.FromBase64String(Convert.ToBase64String(System.Text.Encoding.Unicode.GetBytes(txt)));
            return System.Text.Encoding.UTF8.GetString(decodedBytes);
        }

        public static string Base64Encode(string txt)
        {
            byte[] encodedBytes = System.Text.Encoding.Unicode.GetBytes(txt);
            return Convert.ToBase64String(encodedBytes);
        }


    }
}
