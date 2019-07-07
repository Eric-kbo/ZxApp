using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZXService.DataContracts.ZX_DesigerListEntity;

namespace ZXService.DataAccess.ZX_DesignerListDa
{
    public class DataDesFactory : IDomainObjectFactory<ZX_DesignerListEntity>
    {

        public ZX_DesignerListEntity Construct(IDataReader reader)
        {
            var item = new ZX_DesignerListEntity();

            int Rank = reader.GetOrdinal("Rank");
            if (!reader.IsDBNull(Rank))
            {
                item.Rank = reader.GetInt64(Rank);
            }

            int ID = reader.GetOrdinal("ID");
            if (!reader.IsDBNull(ID))
            {
                item.ID = reader.GetValue(ID).ToString();
            }

            int AreaID = reader.GetOrdinal("AreaID");
            if (!reader.IsDBNull(AreaID))
            {
                item.AreaID = reader.GetValue(AreaID).ToString();
            }

            int DeName = reader.GetOrdinal("DeName");
            if (!reader.IsDBNull(DeName))
            {
                item.DeName = reader.GetString(DeName);
            }

            int Sex = reader.GetOrdinal("Sex");
            if (!reader.IsDBNull(Sex))
            {
                item.Sex = reader.GetBoolean(Sex);
            }

            int WorkYear = reader.GetOrdinal("WorkYear");
            if (!reader.IsDBNull(WorkYear))
            {
                item.WorkYear = reader.GetInt32(WorkYear);
            }

            int School = reader.GetOrdinal("School");
            if (!reader.IsDBNull(School))
            {
                item.School = reader.GetString(School);
            }

            int Experience = reader.GetOrdinal("Experience");
            if (!reader.IsDBNull(Experience))
            {
                item.Experience = reader.GetString(Experience);
            }

            int Idea = reader.GetOrdinal("Idea");
            if (!reader.IsDBNull(Idea))
            {
                item.Idea = reader.GetString(Idea);
            }

            int Price = reader.GetOrdinal("Price");
            if (!reader.IsDBNull(Price))
            {
                item.Price = reader.GetInt32(Price);
            }

            int Mobile = reader.GetOrdinal("Mobile");
            if (!reader.IsDBNull(Mobile))
            {
                item.Mobile = reader.GetString(Mobile);
            }

            int WxCode = reader.GetOrdinal("WxCode");
            if (!reader.IsDBNull(WxCode))
            {
                item.WxCode = reader.GetString(WxCode);
            }

            int Email = reader.GetOrdinal("Email");
            if (!reader.IsDBNull(Email))
            {
                item.Email = reader.GetString(Email);
            }

            int ClickCount = reader.GetOrdinal("ClickCount");
            if (!reader.IsDBNull(ClickCount))
            {
                item.ClickCount = reader.GetInt32(ClickCount);
            }

            int ClinchCount = reader.GetOrdinal("ClinchCount");
            if (!reader.IsDBNull(ClinchCount))
            {
                item.ClinchCount = reader.GetInt32(ClinchCount);
            }

            int CheckDesc = reader.GetOrdinal("CheckDesc");
            if (!reader.IsDBNull(CheckDesc))
            {
                item.CheckDesc = reader.GetString(CheckDesc);
            }

            return item;
        }
    }
}
