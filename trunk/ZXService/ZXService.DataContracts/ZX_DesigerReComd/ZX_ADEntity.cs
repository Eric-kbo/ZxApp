using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZXService.DataContracts.ZX_DesigerReComd
{
   [DataContract]
   public class ZX_ADEntity
    {

       [DataMember]
       public string ID { get; set; }

       [DataMember]
       public int Position { get; set; }

       [DataMember]
       public string ImageFile { get; set; }

       [DataMember]
       public string Http { get; set; }

       [DataMember]
       public string AdName { get; set; }
    }
}
