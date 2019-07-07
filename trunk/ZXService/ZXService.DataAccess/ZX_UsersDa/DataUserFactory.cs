using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZXService.DataContracts.ZX_UserEntity;

namespace ZXService.DataAccess.ZX_UsersDa
{
    public class DataUserFactory : IDomainObjectFactory<ZX_UserInfoEntity>
    {
        public ZX_UserInfoEntity Construct(IDataReader reader)
        {
            var item = new ZX_UserInfoEntity();
            int ID = reader.GetOrdinal("ID");
            if (!reader.IsDBNull(ID))
            {
                item.ID = reader.GetValue(ID).ToString();
            }

            int UserType = reader.GetOrdinal("UserType");
            if (!reader.IsDBNull(UserType))
            {
                item.UserType = reader.GetString(UserType);
            }

            int LoginName = reader.GetOrdinal("LoginName");
            if (!reader.IsDBNull(LoginName))
            {
                item.LoginName = reader.GetString(LoginName);
            }

            int FName = reader.GetOrdinal("FName");
            if (!reader.IsDBNull(FName))
            {
                item.FName = reader.GetString(FName);
            }

            int PW = reader.GetOrdinal("PW");
            if (!reader.IsDBNull(PW))
            {
                item.PW = reader.GetString(PW);
            }

            int Email = reader.GetOrdinal("Email");
            if (!reader.IsDBNull(Email))
            {
                item.Email = reader.GetString(Email);
            }

            int Mobile = reader.GetOrdinal("Mobile");
            if (!reader.IsDBNull(Mobile))
            {
                item.Mobile = reader.GetString(Mobile);
            }

            int Remark = reader.GetOrdinal("Remark");
            if (!reader.IsDBNull(Remark))
            {
                item.Remark = reader.GetString(Remark);
            }

            int CreateTime = reader.GetOrdinal("CreateTime");
            if (!reader.IsDBNull(CreateTime))
            {
                item.CreateTime = reader.GetDateTime(CreateTime).ToString();
            }

            int UpdateTime = reader.GetOrdinal("UpdateTime");
            if (!reader.IsDBNull(UpdateTime))
            {
                item.UpdateTime = reader.GetDateTime(UpdateTime).ToString();
            }

            int AreaID = reader.GetOrdinal("AreaID");
            if (!reader.IsDBNull(AreaID))
            {
                item.AreaID = reader.GetValue(AreaID).ToString();
            }

            int WxID = reader.GetOrdinal("WxID");
            if (!reader.IsDBNull(WxID))
            {
                item.WxID = reader.GetString(WxID);
            }
            return item;
        }
    }
}
