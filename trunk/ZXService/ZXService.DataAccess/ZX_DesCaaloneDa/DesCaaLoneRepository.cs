using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZXService.DataContracts.ZX_DeCase;

namespace ZXService.DataAccess.ZX_DesCaaloneDa
{
    public class DesCaaLoneRepository : Repository<ZX_DeCaseInfoEntity>
    {
        /// <summary>
        /// 获取设计师作品
        /// </summary>
        /// <param name="arg">T实体对象</param>
        /// <returns>返回获取到的用户信息数组</returns>
        public List<ZX_DeCaseInfoEntity> GetDeCaseEntityList(string ID="")
        {
            ZX_DeCaseInfoEntity model = new ZX_DeCaseInfoEntity() {DesignerID=ID };
            //定义数据库查询字符串
            var selectfac = new SelectDesCaaloneFac();
                 
            //父类继承的查找方法   参数 分别为查询字符串，查询出来的数据的实体对象，和查询条件参数
            return Find(selectfac, new DataDesCaaloneFactory(), model);
        }
    }
}
