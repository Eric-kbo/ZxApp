using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZXService.DataContracts.ZX_DeCase
{
    [DataContract]
   public class ZX_DecaseEntity
    {
        [DataMember]
        public Guid ID { get; set; }

        [DataMember]
        public Guid DesignerID { get; set; }

        [DataMember]
        public string CaseTitle { get; set; }

        [DataMember]
        public string CaseDesc { get; set; }

        [DataMember]
        public string ImageFile { get; set; }

    }
}
