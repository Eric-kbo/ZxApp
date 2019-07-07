using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZXService.DataContracts;
using ZXService.DataContracts.ZX_DesigerListEntity;

namespace ZXService.DataAccess.ZX_DesignerListDa
{
    class DataPagedataDesFactory : IDomainObjectPageFactory<PageDataEntity<ZX_DesignerListEntity, ZX_DesignerListCondsEntity>>
    {  
        public void Construct(IDataReader reader, PageDataEntity<ZX_DesignerListEntity, ZX_DesignerListCondsEntity> idObject)
        {
            int rowCountIndex = reader.GetOrdinal("RN");
            if (!reader.IsDBNull(rowCountIndex))
            {
                idObject.TotalCount = reader.GetInt32(rowCountIndex);
            }
        }
    }
}
