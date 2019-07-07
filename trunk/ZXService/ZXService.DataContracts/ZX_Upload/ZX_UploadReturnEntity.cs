using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZXService.DataContracts.ZX_Upload
{
    [DataContract]
    public class ZX_UploadReturnEntity
    {
        [DataMember]
        public string servicePath { get; set; }
    }
}
