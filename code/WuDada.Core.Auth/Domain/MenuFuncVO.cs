using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WuDada.Core.Auth.Domain
{
    /// <summary>
    /// 後台功能
    /// </summary>
    [Serializable]
    [DataContract]
    public class MenuFuncVO
    {
        #region Constructor

        public MenuFuncVO(string menuFuncName, MenuFuncVO parentMenu) 
        {
            this.MenuFuncName = menuFuncName;
            this.ParentMenu = parentMenu;
        }

        public MenuFuncVO(string menuFuncName, MenuFuncVO parentMenu,string note)
        {
            this.MenuFuncName = menuFuncName;
            this.ParentMenu = parentMenu;
            this.Note = note;
        }

        public MenuFuncVO()
        {
           
        }

        #endregion

        #region Property

        [DataMember]
        public virtual int MenuFuncId { get; set; }

        /// <summary>
        /// 父層功能
        /// </summary>
        [DataMember]
        public virtual MenuFuncVO ParentMenu { get; set; }

        /// <summary>
        /// 功能名稱
        /// </summary>
        [DataMember]
        public virtual string MenuFuncName { get; set; }

        /// <summary>
        /// 子功能
        /// </summary>
        [DataMember]
        public virtual IList<MenuFuncVO> SubFuncs { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [DataMember]
        public virtual int ListOrder { get; set; }

        [DataMember]
        public virtual String MainPath { get; set; }

        [DataMember]
        public virtual IList<FunctionPathVO> FuncionPaths { get; set; }

        [DataMember]
        public virtual String Note { get; set; }

        #endregion
    }
}
