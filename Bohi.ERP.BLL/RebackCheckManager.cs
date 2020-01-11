using Bohi.ERP.DAL;
using Bohi.ERP.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bohi.ERP.BLL
{
    public class RebackCheckManager
    {
        /// <summary>
        /// 添加一行车辆重量不足，回装数据
        /// </summary>        
        /// <returns>真假</returns>
        public bool AddReback(RebackCheckMD rebackCheckMD)
        {
            RebackCheckService rebackCheckService = new RebackCheckService();
            return rebackCheckService.AddReback(rebackCheckMD);
        }
    }
}
