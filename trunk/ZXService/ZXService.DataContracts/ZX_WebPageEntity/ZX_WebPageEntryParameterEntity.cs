using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZXService.DataContracts.ZX_WebPageEntity
{
    [DataContract]
    public class ZX_WebPageEntryParameterEntity
    {
        [DataMember]
        public int ContentType { get; set; }
        [DataMember]
        public int PageIndex { get; set; }
        [DataMember]
        public int PageSize { get; set; }
    }
}
