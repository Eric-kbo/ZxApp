using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZXService.DataContracts.ZX_UserEntity
{
    [DataContract]
    public class ZX_UserInfoEntity
    {
        /// <summary>
        /// 用户id
        /// </summary>
        [DataMember]
        public string ID { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        [DataMember]
        public string UserType { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        [DataMember]
        public string LoginName { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [DataMember]
        public string FName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [DataMember]
        public string PW { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [DataMember]
        public string Email { get; set; }

        /// <summary>
        /// Mobile
        /// </summary>
        [DataMember]
        public string Mobile { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        public string Remark { get; set; }
        
        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        public string CreateTime { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [DataMember]
        public string UpdateTime { get; set; }

        /// <summary>
        /// 所属地区ID
        /// </summary>
        [DataMember]
        public string AreaID { get; set; }
        
        /// <summary>
        /// 微信唯一识别码
        /// </summary>
        [DataMember]
        public string WxID { get; set; }
    }
}
