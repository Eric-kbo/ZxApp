using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZXService.DataContracts.ZX_DesigerReComd
{
   [DataContract]
   public class ZX_DesignStyleEntity
    {
       [DataMember]
       public string ID { get; set; }

       [DataMember]
       public int StyleType { get; set; }

       [DataMember]
       public string ImageFile { get; set; }

       [DataMember]
       public string ImageDesc { get; set; }

       [DataMember]
       public string Remark { get; set; }
    }
}
