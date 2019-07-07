using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZXService.DataContracts.ZX_DeCaseImage;

namespace ZXService.DataAccess.ZX_DeCaseImage
{
    public class DataDeCaseImgFactory : IDomainObjectFactory<ZX_DeCaseImageEntity>
    {
        public ZX_DeCaseImageEntity Construct(IDataReader reader)
        {
            var item = new ZX_DeCaseImageEntity();
            int ID = reader.GetOrdinal("ID");
            if (!reader.IsDBNull(ID))
            {
                item.ID = reader.GetValue(ID).ToString();
            }

            int CaseID = reader.GetOrdinal("CaseID");
            if (!reader.IsDBNull(CaseID))
            {
                item.CaseID = reader.GetValue(CaseID).ToString();
            }

            int ImageTitle = reader.GetOrdinal("ImageTitle");
            if (!reader.IsDBNull(ImageTitle))
            {
                item.ImageTitle = reader.GetString(ImageTitle);
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
