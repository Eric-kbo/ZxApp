using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZXService.DataContracts.ZX_DesignEntity;

namespace ZXService.DataAccess.ZX_Designer
{
   public class DataDesDetailFactory : IDomainObjectFactory<ZX_DesignersEntity>
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

            int Price = reader.GetOrdinal("Price");
            if (!reader.IsDBNull(Price))
            {
                item.Price = reader.GetInt32(Price);
            }

            int Idea = reader.GetOrdinal("Idea");
            if (!reader.IsDBNull(Idea))
            {
                item.Idea = reader.GetString(Idea);
            }

            int Photo = reader.GetOrdinal("Photo");
            if (!reader.IsDBNull(Photo))
            {
                item.Photo = reader.GetString(Photo);
            } 

            return item;
        }
    }
}
