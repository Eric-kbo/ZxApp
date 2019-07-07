using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZXService.DataContracts.ZX_WebPageEntity
{
    [DataContract]
    public class ZX_WebPageInfoEntity
    {
        /// <summary>
        /// 文章ID
        /// </summary>
        [DataMember]
        public string ID { get; set; }

        /// <summary>
        /// 文章类型
        /// </summary>
        [DataMember]
        public int ContentType { get; set; }

        /// <summary>
        /// 文章标题
        /// </summary>
        [DataMember]
        public string Title { get; set; }

        /// <summary>
        /// 文章简介
        /// </summary>
        [DataMember]
        public string ContentDesc { get; set; }

        /// <summary>
        /// 图片文件
        /// </summary>
        [DataMember]
        public string ImageFile { get; set; }

        /// <summary>
        /// 文章地址
        /// </summary>
        [DataMember]
        public string Http { get; set; }


        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        public string CreateTime { get; set; }

        /// <summary>
        /// 点击次数
        /// </summary>
        [DataMember]
        public string VisitCount { get; set; }

        /// <summary>
        /// 生效
        /// </summary>
        [DataMember]
        public string Enable { get; set; }
        
    }
}
