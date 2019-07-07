using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using ZXService.DataContracts;
using ZXService.DataContracts.ZX_DeCase;

namespace ZXService.DataAccess.ZX_CaseDa
{
    public class DataCaseFactory : IDomainObjectFactory<ZX_DeCaseInfoEntity>
    {
        public ZX_DeCaseInfoEntity Construct(IDataReader reader)
        {
            var item = new ZX_DeCaseInfoEntity();
            int ID = reader.GetOrdinal("ID");
            if (!reader.IsDBNull(ID))
            {
                item.ID = reader.GetValue(ID).ToString();
            }

            int DesignerID = reader.GetOrdinal("DesignerID");
            if (!reader.IsDBNull(DesignerID))
            {
                item.DesignerID = reader.GetString(DesignerID);
            }

            int CaseTitle = reader.GetOrdinal("CaseTitle");
            if (!reader.IsDBNull(CaseTitle))
            {
                item.CaseTitle = reader.GetString(CaseTitle);
            }

            int CaseDesc = reader.GetOrdinal("CaseDesc");
            if (!reader.IsDBNull(CaseDesc))
            {
                item.CaseDesc = reader.GetString(CaseDesc);
            }

            int ImageFile = reader.GetOrdinal("ImageFile");
            if (!reader.IsDBNull(ImageFile))
            {
                item.ImageFile = reader.GetString(ImageFile);
            }

            //int CreateTime = reader.GetOrdinal("CreateTime");
            //if (!reader.IsDBNull(CreateTime))
            //{
            //    item.CreateTime = reader.GetString(CreateTime);
            //}

            int UpdateTime = reader.GetOrdinal("UpdateTime");
            if (!reader.IsDBNull(UpdateTime))
            {
                item.UpdateTime = reader.GetString(UpdateTime);
            } 
            return item;
        }
    }
}
