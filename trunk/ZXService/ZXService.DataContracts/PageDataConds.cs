using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZXService.DataContracts
{
    [DataContract]
    public class PageDataConds
    {

        /// <summary>
        /// 每页多少条
        /// </summary>
        [DataMember]
        public int PageSize { get; set; }

        /// <summary>
        /// 第几页
        /// </summary>
        [DataMember]
        public int PageIndex { get; set; }
    }
}
