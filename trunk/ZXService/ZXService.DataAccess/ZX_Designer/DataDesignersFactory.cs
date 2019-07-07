using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZXService.DataContracts.ZX_DesigerReComd;
using ZXService.DataContracts.ZX_DesignEntity;

namespace ZXService.DataAccess.ZX_Designer
{
    public class DataDesignersFactory : IDomainObjectFactory<ZX_DesignersEntity>
    {
        public ZX_DesignersEntity Construct(System.Data.IDataReader reader)
        {
            var item = new ZX_DesignersEntity();

            int DesignerTitle = reader.GetOrdinal("DesignerTitle");
            if (!reader.IsDBNull(DesignerTitle))
            {
                item.DesignerTitle = reader.GetValue(DesignerTitle).ToString();
            }

            int ID = reader.GetOrdinal("ID");
            if (!reader.IsDBNull(ID))
            {
                item.ID = reader.GetValue(ID).ToString();
            }

            int Eexpert = reader.GetOrdinal("Eexpert");
            if (!reader.IsDBNull(Eexpert))
            {
                item.Eexpert = reader.GetValue(Eexpert).ToString();
            }

            int DeName = reader.GetOrdinal("DeName");
            if (!reader.IsDBNull(DeName))
            {
                item.DeName = reader.GetString(DeName);
            }

            //int Sex = reader.GetOrdinal("Sex");
            //if (!reader.IsDBNull(Sex))
            //{
            //    item.Sex = reader.GetBoolean(Sex);
            //}

            //int WorkYear = reader.GetOrdinal("WorkYear");
            //if (!reader.IsDBNull(WorkYear))
            //{
            //    item.WorkYear = reader.GetInt32(WorkYear);
            //}

            //int School = reader.GetOrdinal("School");
            //if (!reader.IsDBNull(School))
            //{
            //    item.School = reader.GetString(School);
            //}

            //int Experience = reader.GetOrdinal("Experience");
            //if (!reader.IsDBNull(Experience))
            //{
            //    item.Experience = reader.GetString(Experience);
            //}

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

            int ImageFile = reader.GetOrdinal("ImageFile");
            if (!reader.IsDBNull(ImageFile))
            {
                item.ImageFile = reader.GetString(ImageFile);
            }

            int DeLevel = reader.GetOrdinal("DeLevel");
            if (!reader.IsDBNull(DeLevel))
            {
                item.DeLevel = reader.GetInt32(DeLevel);
            }

            int PreCount = reader.GetOrdinal("PreCount");
            if (!reader.IsDBNull(PreCount))
            {
                item.PreCount = reader.GetInt32(PreCount);
            }


            int WorkCount = reader.GetOrdinal("WorkCount");
            if (!reader.IsDBNull(WorkCount))
            {
                item.WorkCount = reader.GetInt32(WorkCount);
            }


            int AreaName = reader.GetOrdinal("AreaName");
            if (!reader.IsDBNull(AreaName))
            {
                item.AreaName = reader.GetValue(AreaName).ToString();
            }

            int Photo = reader.GetOrdinal("Photo");
            if (!reader.IsDBNull(Photo))
            {
                item.Photo = reader.GetString(Photo);
            }



            //int WxCode = reader.GetOrdinal("WxCode");
            //if (!reader.IsDBNull(WxCode))
            //{
            //    item.WxCode = reader.GetString(WxCode);
            //}

            //int Email = reader.GetOrdinal("Email");
            //if (!reader.IsDBNull(Email))
            //{
            //    item.Email = reader.GetString(Email);
            //}

            //int ClickCount = reader.GetOrdinal("ClickCount");
            //if (!reader.IsDBNull(ClickCount))
            //{
            //    item.ClickCount = reader.GetInt32(ClickCount);
            //}

            //int ClinchCount = reader.GetOrdinal("ClinchCount");
            //if (!reader.IsDBNull(ClinchCount))
            //{
            //    item.ClinchCount = reader.GetInt32(ClinchCount);
            //}

            //int CheckDesc = reader.GetOrdinal("CheckDesc");
            //if (!reader.IsDBNull(CheckDesc))
            //{
            //    item.CheckDesc = reader.GetString(CheckDesc);
            //}


            return item;
        }
        #region
        //int ID = reader.GetOrdinal("ID");
        //if (!reader.IsDBNull(ID))
        //{
        //    item.ID = reader.GetValue(ID).ToString();
        //}

        //int AreaID = reader.GetOrdinal("AreaID");
        //if (!reader.IsDBNull(AreaID))
        //{
        //    item.AreaID = reader.GetValue(AreaID).ToString();
        //}

        //int DeName = reader.GetOrdinal("DeName");
        //if (!reader.IsDBNull(DeName))
        //{
        //    item.DeName = reader.GetValue(DeName).ToString();
        //}

        //int Sex = reader.GetOrdinal("Sex");
        //if (!reader.IsDBNull(Sex))
        //{
        //    item.Sex = reader.GetBoolean(Sex);
        //}

        //int WorkYear = reader.GetOrdinal("WorkYear");
        //if (!reader.IsDBNull(WorkYear))
        //{
        //    item.WorkYear = reader.GetInt32(WorkYear);
        //}

        //int School = reader.GetOrdinal("School");
        //if (!reader.IsDBNull(School))
        //{
        //    item.School = reader.GetValue(School).ToString();
        //}

        ////int Experience = reader.GetOrdinal("Experience");
        ////if (!reader.IsDBNull(Experience))
        ////{
        ////    item.Experience = reader.GetValue(Experience).ToString();
        ////}

        //int Idea = reader.GetOrdinal("Idea");
        //if (!reader.IsDBNull(Idea))
        //{
        //    item.Idea = reader.GetValue(Idea).ToString();
        //}

        //int Price = reader.GetOrdinal("Price");
        //if (!reader.IsDBNull(Price))
        //{
        //    item.Price = reader.GetInt32(Price);
        //}

        //int Mobile = reader.GetOrdinal("Mobile");
        //if (!reader.IsDBNull(Mobile))
        //{
        //    item.Mobile = reader.GetValue(Mobile).ToString();
        //}

        ////int WxCode = reader.GetOrdinal("WxCode");
        ////if (!reader.IsDBNull(WxCode))
        ////{
        ////    item.WxCode = reader.GetValue(WxCode).ToString();
        ////}

        ////int Email = reader.GetOrdinal("Email");
        ////if (!reader.IsDBNull(Email))
        ////{
        ////    item.Email = reader.GetValue(Email).ToString();
        ////}

        ////int ClickCount = reader.GetOrdinal("ClickCount");
        ////if (!reader.IsDBNull(ClickCount))
        ////{
        ////    item.ClickCount = reader.GetInt32(ClickCount);
        ////}

        ////int ClinchCount = reader.GetOrdinal("ClinchCount");
        ////if (!reader.IsDBNull(ClinchCount))
        ////{
        ////    item.ClinchCount = reader.GetInt32(ClinchCount);
        ////}

        //int AreaName = reader.GetOrdinal("AreaName");
        //if (!reader.IsDBNull(AreaName))
        //{
        //    item.AreaName = reader.GetValue(AreaName).ToString();
        //}

        ////int CaseTitle = reader.GetOrdinal("CaseTitle");
        ////if (!reader.IsDBNull(CaseTitle))
        ////{
        ////    item.CaseTitle = reader.GetString(CaseTitle);
        ////}

        ////int CaseDesc = reader.GetOrdinal("CaseDesc");
        ////if (!reader.IsDBNull(CaseDesc))
        ////{
        ////    item.CaseDesc = reader.GetString(CaseDesc);
        ////}

        ////int ImageFile = reader.GetOrdinal("ImageFile");
        ////if (!reader.IsDBNull(ImageFile))
        ////{
        ////    item.ImageFile = reader.GetString(ImageFile);
        ////}

        #endregion
    }
}
