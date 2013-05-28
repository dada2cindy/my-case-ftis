using System;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;

namespace FTIS.Domain.Dto
{
    public class AjaxResult
    {
        #region Constructor
        public AjaxResult()
        {          
        }

        public AjaxResult(AjaxResultStatus code, string message)
        {
            this.ErrorCode = code;
            this.Message = message;
        }

        public AjaxResult(AjaxResultStatus code, string message, object obj)
            : this(code, message)
        {
            this.Data = new JavaScriptSerializer().Serialize(obj);
        }

        public AjaxResult(AjaxResultStatus code, string message, string data)
            : this(code, message)
        {
            this.Data = data;
        }
        #endregion

        #region Property

        [DataMember]
        public AjaxResultStatus ErrorCode { get; set; }

        [DataMember]
        public string Message { get; set; }

        /// <summary>
        /// 純文字，不然就是把Object轉成Json string
        /// </summary>
        [DataMember]
        public string Data { get; set; }

        #endregion
    }
}