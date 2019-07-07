using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZXService.DataContracts
{
    public enum Order
    {
        [EnumMember]
        关注度 = 0,
        [EnumMember]
        成交数 = 1
    }
}
