﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace StartProject.Enums
{
    public enum ResponseCodeEnum
    {
        [Description("缺少參數")]
        param_lack = -1,

        [Description("參數錯誤")]
        param_error = -2,

        [Description("Information does not contain token")]
        not_contain_token = -4,

        [Description("TokenNotExist")]
        token_not_exist = -5,

        [Description("TokenExpired")]
        token_expired = -10,

        [Description("RefreshToken does not exist")]
        refreah_token_not_exist = -12,

        [Description("SystemError")]
        system_error = -999,

        [Description("SystemContentError")]
        system_content_error = -998,

        [Description("Unknown error")]
        unknown_error = -10001,

        [Description("SystemMaintain")]
        system_maintain = 98,

        [Description("NoDataResponse")]
        no_data_error = 999,


        [Description("Success")]
        success = 1,
    }
}
