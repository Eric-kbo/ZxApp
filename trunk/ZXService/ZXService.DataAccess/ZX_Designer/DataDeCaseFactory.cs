using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZXService.DataContracts.ZX_DeCase;

namespace ZXService.DataAccess.ZX_Designer
{
    public class DataDeCaseFactory : IDomainObjectFactory<ZX_DecaseEntity>
    {
        public ZX_DecaseEntity Construct(IDataReader reader)
      {
          var item = new ZX_DecaseEntity();
          int ID = reader.GetOrdinal("ID");
          if (!reader.IsDBNull(ID))
          {
              item.ID = reader.GetGuid(ID);
          }

          int DesignerID = reader.GetOrdinal("DesignerID");
          if (!reader.IsDBNull(DesignerID))
          {
              item.DesignerID = reader.GetGuid(DesignerID);
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
          return item;
      }
    }
}
