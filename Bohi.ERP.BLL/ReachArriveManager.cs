using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bohi.ERP.DAL;
using Bohi.ERP.MODEL;

namespace Bohi.ERP.BLL
{
    public class ReachArriveManager
    {
        public List<ReachArriveMD> GetReachArrive(string AutoCode)
        {
            ReachArriveService ras = new ReachArriveService();
            return ras.getAllReachArrive(AutoCode);
        }

    }
}
