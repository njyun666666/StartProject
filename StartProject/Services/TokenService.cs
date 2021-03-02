using StartProject.Common;
using StartProject.Enums;
using StartProject.Helper;
using StartProject.Models.Token;
using StartProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace StartProject.Services
{
    public class TokenService : ITokenService
    {
        private IMyService _myService;
        public TokenModel tokenModel;

        public TokenService(IMyService myService)
        {
            _myService = myService;
        }

        public string CreateToken(string uid, int expireddate)
        {
            TokenModel tokenModel = new TokenModel() { UID = uid, ExpiresDate = expireddate };
            string iv = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 16);
            string jsonstr = JsonSerializer.Serialize(tokenModel);
            string encodestr = EncryptHelper.UrlEncode(EncryptHelper.AES_encrypt(jsonstr, _myService.StartProjectKey(), iv));
            return iv + "." + encodestr;
        }

        /// <summary>
        /// Token 解密
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public TokenModel TokenDecrypt(string token)
        {
            try
            {
                var split = token.Split('.');
                var iv = split[0];
                var encrypt = split[1];
                encrypt = EncryptHelper.UrlDecode(encrypt);

                string tokenDecrypt = EncryptHelper.AES_decrypt(encrypt, _myService.StartProjectKey(), iv);

                if (!string.IsNullOrWhiteSpace(tokenDecrypt))
                {
                    tokenModel = JsonSerializer.Deserialize<TokenModel>(tokenDecrypt);
                    return tokenModel;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return null;
        }

        public ResponseCodeEnum TokenCheck()
        {
            if (tokenModel == null)
            {
                return ResponseCodeEnum.token_not_exist;
            }

            if (tokenModel.ExpiresDate < CommonTools.GetTimeStamp(DateTime.Now))
            {
                return ResponseCodeEnum.token_expired;
            }


            return ResponseCodeEnum.success;
        }

    }
}
