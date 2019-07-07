using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZXService.DataContracts.ZX_DesigerReComd;

namespace ZXService.DataAccess.ZX_AD
{
    public class ADExRepository : Repository<ZX_ADEntity>
    {
        public List<ZX_ADEntity> SelAdInfo(ZX_ADEntity model)
        {
            var selectfac = new SelectADFac();
            return base.Find<ZX_ADEntity>(selectfac, new DataAdFactoty(), model);
        }
    }
}
