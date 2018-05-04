using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Google.Web.Extensions
{
    /// <summary>
    /// 结果Json
    /// </summary>
    public class CustomJsonResult
    {
        public bool Result { get; set; }

        public string ErrorCode { get; set; }

        public string Msg { get; set; }

        public CustomJsonResult() { }

        public CustomJsonResult(bool result, string msg)
        {
            Result = result;
            Msg = msg;
        }
        public CustomJsonResult(bool result, string errorCode, string msg)
        {
            Result = result;
            ErrorCode = errorCode;
            Msg = msg;
        }
    }
}