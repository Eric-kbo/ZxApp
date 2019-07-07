using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZXService.DataContracts.ZX_DesigerListEntity
{
    [DataContract]
    public class ZX_DesignerListCondsEntity : PageDataConds
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

        /// <summary>
        /// 关注度排序
        /// </summary>
        [DataMember]
        public string SortBy { get; set; }

        /// <summary>
        /// 成交率排序
        /// </summary>
        [DataMember]
        public string SortOrder { get; set; }
    }
}
