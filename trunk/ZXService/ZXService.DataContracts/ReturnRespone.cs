using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZXService.DataContracts
{
    [DataContract]
    public class CallResult
    {
        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public bool IsSuccess { get; set; }

        [DataMember]
        public string RequestInTime { get; set; }

        [DataMember]
        public string RequestOutTime { get; set; }

        [DataMember]
        public string Code { get; set; }
    }

    [DataContract]
    public class ReturnRespone<T> : CallResult
    {
        [DataMember]
        public T ResultInfo { get; set; }
    }
}
