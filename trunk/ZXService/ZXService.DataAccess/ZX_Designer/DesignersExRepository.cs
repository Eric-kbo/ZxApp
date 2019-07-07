using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZXService.DataContracts.ZX_DesigerReComd;
using ZXService.DataContracts.ZX_DesignEntity;

namespace ZXService.DataAccess.ZX_Designer
{
    public class DesignersExRepository : Repository<ZX_DesignersEntity>
    {
        public int AddDesignerInfo(ZX_DesignersEntity model)
        {
            var insertfac =new InsertDesignerFac();
            return base.Add(insertfac,model);
        }

        public List<ZX_DesignersEntity> SelDesignerInfo(ZX_DesignersEntity model)
        {
            var selectfac = new SelectDesignerFac();
            return base.Find<ZX_DesignersEntity>(selectfac, new DataDesignersFactory(), model); 
        }

        public List<ZX_DesignersEntity> SelDesignerDetail(ZX_DesignersEntity model)
        {
            var selectfac = new SelectDesignerDetaillFac();
            return base.Find<ZX_DesignersEntity>(selectfac, new DataDesDetailFactory(), model);
        }
    }
}
