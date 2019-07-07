using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZXService.DataContracts.ZX_DesigerReComd;

namespace ZXService.DataAccess.ZX_AD
{
    public class DataAdFactoty : IDomainObjectFactory<ZX_ADEntity>
    {

        public ZX_ADEntity Construct(System.Data.IDataReader reader)
        {

            var item = new ZX_ADEntity();

            int ID = reader.GetOrdinal("ID");
            if (!reader.IsDBNull(ID))
            {
                item.ID = reader.GetValue(ID).ToString();
            }

            int Position = reader.GetOrdinal("Position");
            if (!reader.IsDBNull(Position))
            {
                item.Position = reader.GetInt32(Position);
            }

            int ImageFile = reader.GetOrdinal("ImageFile");
            if (!reader.IsDBNull(ImageFile))
            {
                item.ImageFile = reader.GetValue(ImageFile).ToString();
            }

            int Http = reader.GetOrdinal("Http");
            if (!reader.IsDBNull(Http))
            {
                item.Http = reader.GetValue(Http).ToString();
            }

            int AdName = reader.GetOrdinal("AdName");
            if (!reader.IsDBNull(AdName))
            {
                item.AdName = reader.GetValue(AdName).ToString();
            }

            return item;
        }
    }
}
