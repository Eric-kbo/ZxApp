using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZXService.DataContracts.ZX_DeImageEntity
{
    [DataContract]
    public class ImageDesigner
    {
        /// <summary>
        /// 设计师id
        /// </summary>
        [DataMember]
        public string DeID { get; set; }

        [DataMember]
        public string DeName { get; set; }

        /// <summary>
        /// 成交数
        /// </summary>
        [DataMember]
        public int ClinchCount { get; set; }

        /// <summary>
        /// 关注度
        /// </summary>
        [DataMember]
        public int ClickCount { get; set; }

        /// <summary>
        /// 设计师认证级别
        /// </summary>
        [DataMember]
        public int DeLevel { get; set; }

        /// <summary>
        /// 设计理念
        /// </summary>
        [DataMember]
        public string Idea { get; set; }

        [DataMember]
        public List<ZX_ImageList> DeCaseList { get; set; }
    }

    [DataContract]
    public class ZX_ImageList
    {
        /// <summary>
        /// 作品id
        /// </summary>
        [DataMember]
        public string DecaseID { get; set; }

        [DataMember]
        public string ImageFile { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [DataMember]
        public string CaseTitle { get; set; }

        /// <summary>
        /// 介绍
        /// </summary>
        [DataMember]
        public string CaseDesc { get; set; }
    }

    
}
