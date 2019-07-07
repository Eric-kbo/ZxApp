using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZXService.DataContracts.ZX_UserEntity
{
    [DataContract]
    public class ZX_UserEntityConds
    {
        [DataMember]
        public string ID { get; set; }
    }
}
