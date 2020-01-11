using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bohi.ERP.DAL;
using Bohi.ERP.MODEL;

namespace Bohi.ERP.BLL
{
    public class PubcustomerManager
    {
        /// <summary>
        /// 获取所有客户资料
        /// </summary>
        /// <returns>客户资料泛型</returns>
        public List<PubcustomerMD> GetPubCustomer()
        {
            PubcustomerService ps = new PubcustomerService();
            return ps.GetPubCustomer();
        }
    }
}
