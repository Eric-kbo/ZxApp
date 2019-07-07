using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZXService.DataAccess.ZX_Designer;
using ZXService.DataContracts;
using ZXService.DataContracts.ZX_DeCase;

namespace ZXService.DataAccess.ZX_CaseDa
{
    public class CaseRepository : Repository<ZX_DeCaseInfoEntity>
    {
        public List<ZX_DeCaseInfoEntity> GetDeCaseList(PageDataEntity<ZX_DeCaseInfoEntity, ZX_DeCaseCondsEntity> pageData)
        {
            var sql = new SelectCaseFac();
            return FindWithOutpuTotalCount(sql, new DataCaseFactory(), new DataPagedataCaseFactory(), pageData);
        }
    }
}