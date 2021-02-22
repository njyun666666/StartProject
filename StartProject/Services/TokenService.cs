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
            return "";
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
                string tokenDecrypt = EncryptHelper.AES_decrypt(token, _myService.StartProjectKey());

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

            if (tokenModel.ExpiresDate < DateTime.Now)
            {
                return ResponseCodeEnum.token_expired;
            }


            return ResponseCodeEnum.success;
        }

    }
}
