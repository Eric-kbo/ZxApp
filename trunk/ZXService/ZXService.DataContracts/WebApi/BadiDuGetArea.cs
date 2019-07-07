using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZXService.DataContracts.WebApi
{
    [DataContract]
    public class BadiDuGetArea
    {
        /// <summary>
        /// 状态码
        /// </summary>
        [DataMember]
        public int status { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        [DataMember]
        public result result { get; set; }


    }

    [DataContract]
    public class result
    {
        [DataMember]
        public Location location { get; set; }

        /// <summary>
        /// 所有地址
        /// </summary>
        [DataMember]
        public string formatted_address { get; set; }

        [DataMember]
        public string business { get; set; }

        [DataMember]
        public addressComponent addressComponent { get; set; }

        [DataMember]
        public string sematic_description { get; set; }

        [DataMember]
        public int cityCode { get; set; }

    }

    /// <summary>
    /// 坐标
    /// </summary>
    [DataContract]
    public class Location
    {
        [DataMember]
        public string lng { get; set; }
        [DataMember]
        public string lat { get; set; }
    }

    [DataContract]
    public class addressComponent
    {
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
        /// 
        /// </summary>
        [DataMember]
        public string country_code_iso { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string country_code_iso2 { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        [DataMember]
        public string province { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        [DataMember]
        public string city { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string town { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string adcode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string street { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string street_number { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string direction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string distance { get; set; }
    }

}
