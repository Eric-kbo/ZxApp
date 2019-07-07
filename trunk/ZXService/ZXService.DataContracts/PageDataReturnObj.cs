using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZXService.DataContracts
{
    [DataContract]
    public class PageDataReturnObj<T>
    {
        /// <summary>
        /// 总条数
        /// </summary>
        [DataMember]
        public int TotalCount { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        [DataMember]
        public int PageCount { get; set; }
        /// <summary>
        /// 当前页数
        /// </summary>
        [DataMember]
        public int PageIndex { get; set; }
        /// <summary>
        /// 返回对象
        /// </summary> 
        [DataMember]
        public List<T> DateList { get; set; }
    }
}
