using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZXService.DataContracts.ZX_DesigerReComd;

namespace ZXService.DataAccess.ZX_DesignStyle
{
    public class DataDesStyleFactoty : IDomainObjectFactory<ZX_DesignStyleEntity>
    {

        public ZX_DesignStyleEntity Construct(System.Data.IDataReader reader)
        {
            var item = new ZX_DesignStyleEntity();
            int ID = reader.GetOrdinal("ID");
            if (!reader.IsDBNull(ID))
            {
                item.ID = reader.GetValue(ID).ToString();
            }

            int StyleType = reader.GetOrdinal("StyleType");
            if (!reader.IsDBNull(StyleType))
            {
                item.StyleType = reader.GetInt32(StyleType);
            }

            int ImageFile = reader.GetOrdinal("ImageFile");
            if (!reader.IsDBNull(ImageFile))
            {
                item.ImageFile = reader.GetValue(ImageFile).ToString();
            }


            int Remark = reader.GetOrdinal("Remark");
            if (!reader.IsDBNull(Remark))
            {
                item.Remark = reader.GetValue(Remark).ToString();
            }

            int ImageDesc = reader.GetOrdinal("ImageDesc");
            if (!reader.IsDBNull(ImageDesc))
            {
                item.ImageDesc = reader.GetValue(ImageDesc).ToString();
            }

            return item;
        }
    }
}
