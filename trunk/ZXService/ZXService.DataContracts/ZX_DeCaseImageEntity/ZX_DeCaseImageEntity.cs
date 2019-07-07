using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZXService.DataContracts.ZX_DeCaseImage
{
    [DataContract]
   public class ZX_DeCaseImageEntity
    {
        /// <summary>
        /// 用户id
        /// </summary>
        [DataMember]
        public string ID { get; set; }

        /// <summary>
        /// 设计师作品id
        /// </summary>
        [DataMember]
        public string CaseID { get; set; }

        /// <summary>
        /// 图片标题
        /// </summary>
        [DataMember]
        public string ImageTitle { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        [DataMember]
        public string ImageFile { get; set; }
    }
}
