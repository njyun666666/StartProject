using StartProject.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StartProject.ViewModels
{
    public class ResponseViewModel
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
        
        [JsonPropertyName("timestamp")]
        public long TimeStamp { get; set; } //= CommonTools.GetTimeStamp();

        [JsonPropertyName("data")]
        public object Data { get; set; }
        public ResponseViewModel()
        {
            this.Code = -10001;
            this.Message = "Unknown error";
        }
        public ResponseViewModel(int code, string msg = null)
        {
            this.Code = code;
            this.Message = msg;
        }
        public ResponseViewModel(ResponseCodeEnum code, string message = null)
        {
            this.Code = (int)code;
            this.Message = message;
            if (message == null)
                this.Message = EnumExtenstions.GetEnumDescription(code);
        }
    }
    public class OKResponse : ResponseViewModel
    {
        public OKResponse() : base(ResponseCodeEnum.success)
        {
        }
    }
    public class DisableResponse : ResponseViewModel
    {
        public DisableResponse() : base(2)
        {
            this.Message = "Account Disable";
        }
    }

    public class MissParamResponse : ResponseViewModel
    {
        public MissParamResponse() : base(-1)
        {
        }
        public MissParamResponse(string paramName) : base(-1)
        {
            this.Message = "缺少參數:" + paramName;
        }
    }

    public class NoTokenResponse : ResponseViewModel
    {
        public NoTokenResponse() : base(-4)
        {
            this.Message = "Information does not contain token";
        }
    }

    public class TokenNotExistResponse : ResponseViewModel
    {
        public TokenNotExistResponse() : base(-5)
        {
            this.Message = "TokenNotExist";
        }
    }

    public class TokenExpiredResponse : ResponseViewModel
    {
        public TokenExpiredResponse() : base(-10)
        {
            this.Message = "TokenExpired";
        }
    }

    public class DecryptErrorResponse : ResponseViewModel
    {
        public DecryptErrorResponse() : base(-11)
        {
            this.Message = "TokenDecryptError";
        }
    }

    public class NoRefreshTokenResponse : ResponseViewModel
    {
        public NoRefreshTokenResponse() : base(-12)
        {
            this.Message = "RefreshToken does not exist";
        }
    }

    public class SystemErrorResponse : ResponseViewModel
    {
        public SystemErrorResponse() : base(-999)
        {
            this.Message = "SystemError";
        }
    }

    public class SystemContentErrorResponse : ResponseViewModel
    {
        public SystemContentErrorResponse(string msg) : base(-998)
        {
            this.Message = msg;
        }
    }

    public class NoDataResponse : ResponseViewModel
    {
        public NoDataResponse() : base(999)
        {
            this.Message = "查無資料";
        }
    }
}
