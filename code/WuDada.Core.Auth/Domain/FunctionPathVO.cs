using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WuDada.Core.Auth.Domain
{
    [Serializable]
    [DataContract]
    public class FunctionPathVO
    {
        #region Property

        [DataMember]
        public virtual int FunctionPathId { get; set; }

        [DataMember]
        public virtual MenuFuncVO MenuFunc { get; set; }

        [DataMember]
        public virtual string Path { get; set; }

        #endregion
    }
}
