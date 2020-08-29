using StartProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StartProject.Services
{
    public class AuthService 
    {
        private static readonly string TokenKey = "+%WqvSs?8!48xt#n4=$e8?rG!u$A3GMM";

        /// <summary>
        /// token encode
        /// </summary>
        /// <param name="string_secretContent"></param>
        /// <returns></returns>
        public static string TokenKeyAES_encrypt(string string_secretContent)
        {
            //密碼轉譯一定都是用byte[] 所以把string都換成byte[]
            byte[] byte_secretContent = Encoding.UTF8.GetBytes(string_secretContent);
            byte[] byte_pwd = Encoding.UTF8.GetBytes(TokenKey);
            //加解密函數的key通常都會有固定的長度 而使用者輸入的key長度不定 因此用hash過後的值當做key
            MD5CryptoServiceProvider provider_MD5 = new MD5CryptoServiceProvider();
            byte[] byte_pwdMD5 = provider_MD5.ComputeHash(byte_pwd);
            //產生加密實體 如果要用其他不同的加解密演算法就改這裡(ex:3DES)
            RijndaelManaged provider_AES = new RijndaelManaged();
            ICryptoTransform encrypt_AES = provider_AES.CreateEncryptor(byte_pwdMD5, byte_pwdMD5);
            //output就是加密過後的結果
            byte[] output = encrypt_AES.TransformFinalBlock(byte_secretContent, 0, byte_secretContent.Length);
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < output.Length; i++)
            {
                sBuilder.Append(output[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        /// <summary>
        /// token decode
        /// </summary>
        /// <param name="ciphertext"></param>
        /// <returns></returns>
        public static string TokenKeyAES_decrypt(string ciphertext)
        {
            try
            {
                byte[] byte_ciphertext = new byte[ciphertext.Length / 2];
                for (int x = 0; x < ciphertext.Length / 2; x++)
                {
                    int i = (Convert.ToInt32(ciphertext.Substring(x * 2, 2), 16));
                    byte_ciphertext[x] = (byte)i;
                }
                //密碼轉譯一定都是用byte[] 所以把string都換成byte[]
                byte[] byte_pwd = Encoding.UTF8.GetBytes(TokenKey);
                //加解密函數的key通常都會有固定的長度 而使用者輸入的key長度不定 因此用hash過後的值當做key
                MD5CryptoServiceProvider provider_MD5 = new MD5CryptoServiceProvider();
                byte[] byte_pwdMD5 = provider_MD5.ComputeHash(byte_pwd);
                //產生解密實體
                RijndaelManaged provider_AES = new RijndaelManaged();
                ICryptoTransform decrypt_AES = provider_AES.CreateDecryptor(byte_pwdMD5, byte_pwdMD5);
                //string_secretContent就是解密後的明文
                byte[] byte_secretContent = decrypt_AES.TransformFinalBlock(byte_ciphertext, 0, byte_ciphertext.Length);
                string string_secretContent = Encoding.UTF8.GetString(byte_secretContent);
                return string_secretContent;
            }
            catch (Exception)
            {
                //service_log log = new service_log();
                //log.LogMessage("AESdecrypt error" + ex.ToString());
            }
            return "";
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


    }
}
