using StartProject.Enums;
using StartProject.Models.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartProject.Services.IServices
{
    public interface ITokenService
    {
        public TokenModel TokenDecrypt(string token);
        public ResponseCodeEnum TokenCheck();
    }
}
