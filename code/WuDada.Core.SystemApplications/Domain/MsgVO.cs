using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WuDada.Core.SystemApplications.Domain
{
    /// <summary>
    /// 訊息
    /// </summary>
    public class MsgVO
    {
        public static readonly string ALERT_PLEASE_SELECT = "請選擇";
        public static readonly string ALERT_PLEASE_INPUT = "請輸入";
        public static readonly string INSERT_OK = "新增成功";
        public static readonly string INSERT_CLASSIFY_OK = "新增分類成功";
        public static readonly string INSERT_PROGRAM_OK = "新增課程成功";
        public static readonly string DELETE_OK = "刪除成功";
        public static readonly string DELETE_ERROR = "刪除失敗";
        public static readonly string UPDATE_OK = "修改成功";
        public static readonly string DATA_NOT_FOUND = "無此資料";
        public static readonly string STALE_EXCEPTION_MSG = "資料更被更新過 請重新載入再次執行!";
        public static readonly string USER_ALREADY_EXIST = " 該會員已存在資料庫!";
        public static readonly string USER_EXIST_ROLE = " 組群中有帳號存在";
        public static readonly string MENU_EXIST_ROLE = " 組群中有功能存在";
        public static readonly string SUBCLASS_EXISTS = " 該分類下層還有子分類存在，需先刪除後，才可以刪除此分類";
        public static readonly string MINUTES = "分鐘";
        public static readonly string ALREADY_COMBINE_PRODUCT_ADD_ERROR = "已為組合品，則不能再新增。";
        public static readonly string PLEASE_USE_RESTRICTION = "請使用查詢條件";
        public static readonly string EMAIL_EXIST = "Email已經存在";
        public static readonly string PASSWORD_WRONG = "密碼不正確";
        public static readonly string LOGIN_ERROR = "帳號或密碼錯誤";
        public static readonly string SCRIPT_SELECT_ALL = "javascript:this.value='';";
        public static readonly string MESSAGE_SEND_SUCCESS = "您的留言已送出。";
        public static readonly string MESSAGE_EDM_SUCCESS = "謝謝您已訂閱電子報成功。";
        public static readonly string MESSAGE_EDM_FAIL = "您已經訂閱了喔。";
        public static readonly string SECURCODE_EMPTY = "請輸入驗證碼。";
        public static readonly string SECURCODE_ERROR = "您輸入的驗證碼錯誤。";

        public enum Action
        {
            新增,
            刪除,
            修改,
            查詢,
            執行,
            新增或修改
        }

        public enum LogTitleName
        {
            登入記錄
        }

    }
}
