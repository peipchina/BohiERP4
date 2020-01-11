using Bohi.ERP.DAL;
using Bohi.ERP.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bohi.ERP.BLL
{
    public class SoybeanAutoCodeManager
    {
        /// <summary>
        /// 获取皮重车辆
        /// </summary>
        /// <returns></returns>
        public List<SoybeanAutoCodeMD> GetTareAutoCode()
        {
            SoybeanAutoCodeService sac = new SoybeanAutoCodeService();
            return sac.GetTareAutoCode();
        }
        /// <summary>
        /// 获取毛重车辆
        /// </summary>
        /// <returns></returns>
        public List<SoybeanAutoCodeMD> GetGrossAutoCode()
        {
            SoybeanAutoCodeService sac = new SoybeanAutoCodeService();
            return sac.GetGrossAutoCode();
        }
        /// <summary>
        /// 插入一行毛重
        /// </summary>
        /// <param name="sac"></param>
        /// <returns></returns>
        public bool UpdataQTYGross(decimal QtyGross,long GrossStfID, DateTime GrossTime, long ID)
        {
            SoybeanAutoCodeService sac = new SoybeanAutoCodeService();
            return sac.UpdataQTYGross(QtyGross, GrossStfID, GrossTime,ID);
        }
        /// <summary>
        /// 插入一行皮重
        /// </summary>
        /// <param name="sac"></param>
        /// <returns></returns>
        public bool UpdataQTYTare(decimal QtyTare, DateTime QtyTareTime, long ID,long TareStfID)
        {
            SoybeanAutoCodeService sac = new SoybeanAutoCodeService();
            return sac.UpdataQTYTare(QtyTare, QtyTareTime, ID,TareStfID);
        }
        /// <summary>
        /// 插入一行托利多数据
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public bool InsertQTYTareTLD(PoundTotalMD pt)
        {
            PoundTotalService ptm = new PoundTotalService();
            return ptm.InsertQtyTare(pt)>0;
        }
        /// <summary>
        /// 插入一行车辆皮重及毛重
        /// </summary>
        /// <param name="pt"></param>
        /// <returns>影响行</returns>
        public bool InsertTareAndGross(PoundTotalMD pt)
        {
            PoundTotalService ptm = new PoundTotalService();
            return ptm.InsertTareAndGross(pt);

        }
        /// <summary>
        /// 插入一行地磅名（皮重）
        /// </summary>
        /// <param name="sac"></param>
        /// <returns></returns>
        public bool InsertTarePoundName(TarePoundNameMD tpn)
        {
            SoybeanAutoCodeService sac= new SoybeanAutoCodeService();
            return sac.InsertTarePoundName(tpn);
        }
        /// <summary>
        /// 事务，更新3S表皮重，插入一行对应的皮重地磅名
        /// </summary>
        /// <param name="QtyTare"></param>
        /// <param name="TareTime"></param>
        /// <param name="ID"></param>
        /// <param name="tpn"></param>
        /// <returns></returns>
        public bool TranTareAndTarnPound(decimal QtyTare,long TareStfID, DateTime TareTime, long ID, TarePoundNameMD tpn)
        {
            SoybeanAutoCodeService sac = new SoybeanAutoCodeService();
            return sac.TranTareAndTarnPound(QtyTare, TareStfID, TareTime,ID,tpn);
        }
        /// <summary>
        /// 上一步（皮重）
        /// </summary>
        /// <param name="sac"></param>
        /// <returns></returns>
        public bool BackNext(long ID)
        {
            SoybeanAutoCodeService sac = new SoybeanAutoCodeService();
            return sac.BackNext(ID);
        }
    }
}
