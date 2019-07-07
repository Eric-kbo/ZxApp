using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZXService.DataContracts;
using ZXService.DataContracts.ZX_DeCase;

namespace ZXService.DataAccess.ZX_CaseDa
{
    public class DataPagedataCaseFactory : IDomainObjectPageFactory<PageDataEntity<ZX_DeCaseInfoEntity, ZX_DeCaseCondsEntity>>
    {
        public void Construct(IDataReader reader, PageDataEntity<ZX_DeCaseInfoEntity, ZX_DeCaseCondsEntity> idObject)
        {
            int rowCountIndex = reader.GetOrdinal("rowCount");
            if (!reader.IsDBNull(rowCountIndex))
            {
                idObject.TotalCount = reader.GetInt32(rowCountIndex);
            }
        }
    }
}
