﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ZXService.DataContracts.ZX_DeCase;

namespace ZXService.DataContracts.ZX_DesignEntity
{
    [DataContract]
    public class ZX_DesignersEntity
    {
        /// <summary>
        /// 用户id
        /// </summary>
        [DataMember]
        public string ID { get; set; }

        /// <summary>
        /// 区域ID
        /// </summary>
        [DataMember]
        public string AreaID { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [DataMember]
        public string DeName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [DataMember]
        public bool Sex { get; set; }

        /// <summary>
        /// 工作年数
        /// </summary>
        [DataMember]
        public int WorkYear { get; set; }

        /// <summary>
        /// 毕业学校
        /// </summary>
        [DataMember]
        public string School { get; set; }

        /// <summary>
        /// 工作经历
        /// </summary>
        [DataMember]
        public string Experience { get; set; }

        /// <summary>
        /// 设计理念
        /// </summary>
        [DataMember]
        public string Idea { get; set; }

        /// <summary>
        /// 设计定价
        /// </summary>
        [DataMember]
        public int Price { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [DataMember]
        public string Mobile { get; set; }

        /// <summary>
        /// 微信号
        /// </summary>
        [DataMember]
        public string WxCode { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [DataMember]
        public string Email { get; set; }

        /// <summary>
        /// 关注度
        /// </summary>
        [DataMember]
        public int ClickCount { get; set; }

        /// <summary>
        /// 成交数
        /// </summary>
        [DataMember]
        public int ClinchCount { get; set; }

        [DataMember]
        public string CheckDesc { get; set; }

        [DataMember]
        public string AreaName { get; set; }

        //[DataMember]
        //public string CaseTitle { get; set; }

        //[DataMember]
        //public string CaseDesc { get; set; }

        [DataMember]
        public string ImageFile { get; set; }

        /// <summary>
        /// 头像照片
        /// </summary>
        [DataMember]
        public string Photo { get; set; }

        [DataMember]
        public List<ImageFileList> ImageFileList { get; set; }

        [DataMember]
        public string Img { get; set; }

        [DataMember]
        public string ImgTitle { get; set; }

        [DataMember]
        public string ExistId { get; set; }

        [DataMember]
        public string CaseDesc { get; set; }

        [DataMember]
        public string Eexpert { get; set; }

        [DataMember]
        public string DesignerTitle { get; set; }

        [DataMember]
        public int DeLevel { get; set; }

         [DataMember]
        public int PreCount { get; set; }

         [DataMember]
         public int WorkCount { get; set; }

        [DataMember]
        public List<ZX_DeCaseInfoEntity> CaseList { get; set; }
    }

    [DataContract]
    public class ImageFileList
    {
        [DataMember]
        public string ImageTitle { get; set; }

        [DataMember]
        public string ImageFile { get; set; }

    }
}