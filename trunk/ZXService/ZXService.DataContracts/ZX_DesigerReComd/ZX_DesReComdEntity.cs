using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ZXService.DataContracts.ZX_DesignEntity;

namespace ZXService.DataContracts.ZX_DesigerReComd
{
    [DataContract]
   public class ZX_DesReComdEntity
    {
        [DataMember]
        public int Count{ get; set; }

        [DataMember]
        public string ImageDesc { get; set; }

       [DataMember]
       public List<ZX_DesignStyleEntity> DesStyleList { get; set; }

    }
}
