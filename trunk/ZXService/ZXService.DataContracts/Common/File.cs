using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace ZXService.DataContracts.Common
{
   public class File
    {
        [MessageContract]
        public class DownFile
        {
            [MessageHeader]
            public string FileName { get; set; }
        }

        [MessageContract]
        public class UpFileResult
        {
            [MessageHeader]
            public bool IsSuccess { get; set; }
            [MessageHeader]
            public string Message { get; set; }
        }

        [MessageContract]
        public class UpFile
        {
            [MessageHeader]
            public long FileSize { get; set; }
            [MessageHeader]
            public string FileName { get; set; }
            [MessageBodyMember]
            public Stream FileStream { get; set; }

            public string DeId { get; set; }
        }

        [MessageContract]
        public class DownFileResult
        {
            [MessageHeader]
            public long FileSize { get; set; }
            [MessageHeader]
            public bool IsSuccess { get; set; }
            [MessageHeader]
            public string Message { get; set; }
            [MessageBodyMember]
            public Stream FileStream { get; set; }
        }
    }

   
}
