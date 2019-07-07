using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZXService.DataContracts.Paras
{
   public class ResultImg
    {
            /// <summary> 
            /// 文件内容（） 
            /// </summary> 
            [DataMember]
            public bool result { get; set; }
            /// <summary>
            /// 文件扩展名称
            /// </summary>
            [DataMember]
            public string Message { get; set; }

            [DataMember]
            public string Path { get; set; }

      
    }
}
