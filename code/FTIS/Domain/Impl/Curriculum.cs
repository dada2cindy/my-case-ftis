using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FTIS.Domain.Impl
{
    /// <summary>
    /// 課程講義
    /// </summary>
    [Serializable]
    [DataContract]
    public class Curriculum : Entity
    {
        #region Constructor

        #endregion

        #region Property

        /// <summary>
        /// PK
        /// </summary>
        [DataMember]
        public virtual int CurriculumId { get; set; }

        /// <summary>
        /// 刊登日期
        /// </summary>
        [DataMember]
        public virtual DateTime? PostDate { get; set; }

        /// <summary>
        /// 內容
        /// </summary>
        [DataMember]
        public virtual string Content { get; set; }

        /// <summary>
        /// 圖片1
        /// </summary>
        [DataMember]
        public virtual string Pic1 { get; set; }

        /// <summary>
        /// 圖片2
        /// </summary>
        [DataMember]
        public virtual string Pic2 { get; set; }

        /// <summary>
        /// 圖片3
        /// </summary>
        [DataMember]
        public virtual string Pic3 { get; set; }

        /// <summary>
        /// 圖片1名稱
        /// </summary>
        [DataMember]
        public virtual string Pic1Name { get; set; }

        /// <summary>
        /// 圖片2名稱
        /// </summary>
        [DataMember]
        public virtual string Pic2Name { get; set; }

        /// <summary>
        /// 圖片3名稱
        /// </summary>
        [DataMember]
        public virtual string Pic3Name { get; set; }

        /// <summary>
        /// 附件檔1
        /// </summary>
        [DataMember]
        public virtual string AFile1 { get; set; }

        /// <summary>
        /// 附件檔2
        /// </summary>
        [DataMember]
        public virtual string AFile2 { get; set; }

        /// <summary>
        /// 附件檔3
        /// </summary>
        [DataMember]
        public virtual string AFile3 { get; set; }

        /// <summary>
        /// 附件檔1名稱
        /// </summary>
        [DataMember]
        public virtual string AFile1Name { get; set; }

        /// <summary>
        /// 附件檔2名稱
        /// </summary>
        [DataMember]
        public virtual string AFile2Name { get; set; }

        /// <summary>
        /// 附件檔3名稱
        /// </summary>
        [DataMember]
        public virtual string AFile3Name { get; set; }

        /// <summary>
        /// 資料來源1
        /// </summary>
        [DataMember]
        public virtual string AUrl1 { get; set; }

        /// <summary>
        /// 資料來源2
        /// </summary>
        [DataMember]
        public virtual string AUrl2 { get; set; }

        /// <summary>
        /// 資料來源3
        /// </summary>
        [DataMember]
        public virtual string AUrl3 { get; set; }

        /// <summary>
        /// 顯示New. 0.關閉 1.開啟
        /// </summary>
        [DataMember]
        public virtual string IsNew { get; set; }

        /// <summary>
        /// 瀏覽人數
        /// </summary>
        [DataMember]
        public virtual int Vister { get; set; }

        /// <summary>
        /// 主題分類編號
        /// </summary>
        [DataMember]
        public virtual string MainCode { get; set; }

        /// <summary>
        /// 主題分類名稱
        /// </summary>
        [DataMember]
        public virtual string MainName { get; set; }

        /// <summary>
        /// 施政分類編號
        /// </summary>
        [DataMember]
        public virtual string AdminCode { get; set; }

        /// <summary>
        /// 施政分類名稱
        /// </summary>
        [DataMember]
        public virtual string AdminName { get; set; }

        /// <summary>
        /// 服務分類編號
        /// </summary>
        [DataMember]
        public virtual string ServiceCode { get; set; }

        /// <summary>
        /// 服務分類名稱
        /// </summary>
        [DataMember]
        public virtual string ServiceName { get; set; }

        #endregion
    }
}
