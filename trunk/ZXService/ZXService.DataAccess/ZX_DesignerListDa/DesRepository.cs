using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZXService.DataContracts;
using ZXService.DataContracts.ZX_DesigerListEntity;

namespace ZXService.DataAccess.ZX_DesignerListDa
{
    public class DesRepository : Repository<ZX_DesignerListEntity>
    {
        public List<ZX_DesignerListEntity> GetDesignerList(PageDataEntity<ZX_DesignerListEntity,ZX_DesignerListCondsEntity> pageData)
        {
            var sql = new SelectDesFac();
            return FindWithOutpuTotalCount(sql, new DataDesFactory(), new DataPagedataDesFactory(), pageData);
        }
    }
}
