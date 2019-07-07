using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZXService.DataContracts.ZX_DeCase
{
    [DataContract]
    public class ZX_DeCaseCondsEntity : PageDataConds
    {
        /// <summary>
        /// 风格
        /// </summary>
        [DataMember]
        public string CaseTitle { get; set; }

        [DataMember]
        public string CreateTime { get; set; }

        [DataMember]
        public string UpdateTime { get; set; }
    }
}
