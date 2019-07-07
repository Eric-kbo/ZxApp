using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZXService.DataContracts.ZX_AreaEntity
{
    [DataContract]
    public class ZX_AreaInfoEntity
    {
        [DataMember]
        public string ID { get; set; }

        [DataMember]
        public string FID { get; set; }

        [DataMember]
        public string AreaName { get; set; }

        [DataMember]
        public int SortNo { get; set; }
    }

    [DataContract]
    public class ZX_AreaEntity
    {
         
        /// <summary>
        /// 所有地址
        /// </summary>
        [DataMember]
        public string formatted_address { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        [DataMember]
        public string country { get; set; }

        /// <summary>
        /// 国家编号
        /// </summary>
        [DataMember]
        public int country_code { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        [DataMember]
        public string province { get; set; }

        [DataMember]
        public string provinceID { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        [DataMember]
        public string city { get; set; }

        [DataMember]
        public string cityID { get; set; }

        /// <summary>
        /// 城市级别
        /// </summary>
        [DataMember]
        public string city_level { get; set; }

        /// <summary>
        /// 区县
        /// </summary>
        [DataMember]
        public string district { get; set; }

        [DataMember]
        public string districtID { get; set; }
    }

    [DataContract]
    public class ZX_AreaCond
    {
        [DataMember]
        public string location { get; set; }
    }
}
