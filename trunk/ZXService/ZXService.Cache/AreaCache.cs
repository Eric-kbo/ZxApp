using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ZXService.DataContracts.ZX_AreaEntity;

namespace ZXService.Cache
{
    [DataContract]
    public class AreaCache
    {
        [DataMember]
        public List<ZX_AreaInfoEntity> AllAreaCache
        {
            get;
            set;
        }

        /// <summary>
        /// 过期时间
        /// </summary>
        [DataMember]
        public DateTime Expiration { get; set; }
    }
}
