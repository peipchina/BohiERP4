using Bohi.ERP.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bohi.ERP.DAL;

namespace Bohi.ERP.BLL
{
    public class PubAutoCodeListManager
    {
        /// <summary>
        /// 所有进厂车辆信息
        /// </summary>        
        /// <returns>所有进厂车辆信息</returns>
        public List<PubAutoCodeListMD> GetAllAutoCode()
        {
            PubAutoCodeListServiec pacls = new PubAutoCodeListServiec();
            return pacls.GetAllAutoCode();
        }
    }
}
