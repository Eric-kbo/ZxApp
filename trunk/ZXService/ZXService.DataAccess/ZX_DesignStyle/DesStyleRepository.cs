using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZXService.DataContracts.ZX_DesigerReComd;

namespace ZXService.DataAccess.ZX_DesignStyle
{
    public class DesStyleRepository : Repository<ZX_DesignStyleEntity>
    {
        public List<ZX_DesignStyleEntity> SelDesStyleInfo()
        {
            var selectfac = new SelectDesStyleFac();
            return base.Find<ZX_DesignStyleEntity>(selectfac, new DataDesStyleFactoty(), null);
        }
    }
}
