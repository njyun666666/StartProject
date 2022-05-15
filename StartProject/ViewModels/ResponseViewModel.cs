using StartProject.Common;
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
		public long TimeStamp { get; set; } = CommonTools.GetTimeStamp();

		[JsonPropertyName("data")]
		public object Data { get; set; }
		public ResponseViewModel()
		{
			this.Code = (int)ResponseCodeEnum.unknown_error;
			//this.Message = "Unknown error";
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
		public DisableResponse() : base(ResponseCodeEnum.account_disable)
		{
		}
	}

	public class MissParamResponse : ResponseViewModel
	{
		public MissParamResponse() : base(ResponseCodeEnum.param_lack)
		{
		}
		public MissParamResponse(string paramName) : base(ResponseCodeEnum.param_error)
		{
			this.Message = "缺少參數:" + paramName;
		}
	}

	public class NoTokenResponse : ResponseViewModel
	{
		public NoTokenResponse() : base(ResponseCodeEnum.not_contain_token)
		{
		}
	}

	public class TokenNotExistResponse : ResponseViewModel
	{
		public TokenNotExistResponse() : base(ResponseCodeEnum.token_not_exist)
		{
		}
	}

	public class TokenExpiredResponse : ResponseViewModel
	{
		public TokenExpiredResponse() : base(ResponseCodeEnum.token_expired)
		{
		}
	}

	public class DecryptErrorResponse : ResponseViewModel
	{
		public DecryptErrorResponse() : base(ResponseCodeEnum.token_decrypt_error)
		{
		}
	}

	public class NoRefreshTokenResponse : ResponseViewModel
	{
		public NoRefreshTokenResponse() : base(ResponseCodeEnum.refreah_token_not_exist)
		{
		}
	}

	public class SystemErrorResponse : ResponseViewModel
	{
		public SystemErrorResponse() : base(ResponseCodeEnum.system_error)
		{
		}
	}

	public class SystemContentErrorResponse : ResponseViewModel
	{
		public SystemContentErrorResponse(string msg) : base(ResponseCodeEnum.system_content_error)
		{
			this.Message = msg;
		}
	}

	public class NoDataResponse : ResponseViewModel
	{
		public NoDataResponse() : base(ResponseCodeEnum.no_data_error)
		{
			this.Message = "查無資料";
		}
	}
	public class ExceptionResponse : ResponseViewModel
	{
		public ExceptionResponse() : base(ResponseCodeEnum.exception)
		{

		}
	}
}
