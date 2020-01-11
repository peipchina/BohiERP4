using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bohi.ERP.MODEL;
using Bohi.ERP.DAL;

namespace Bohi.ERP.BLL
{
    public class SacOutSuperviseManager
    {
        #region ---------------根据车号获取车辆在厂信息---------------
        /// <summary>
        /// 根据车号获取车辆在厂信息
        /// </summary>
        /// <param name="AutoCode"></param>
        /// <returns>未完成监装车辆</returns>
        public List<SacOutSuperviseMD> GetSacList(string AutoCode)
        {
            SacOutSuperviseService sos = new SacOutSuperviseService();
            return sos.GetSacList(AutoCode);
        }
        #endregion
    }
}
