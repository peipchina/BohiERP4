using Bohi.ERP.DAL;
using Bohi.ERP.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bohi.ERP.BLL
{
    public class TradeManager
    {
        /// <summary>
        /// 机修楼托利多数据
        /// </summary>
        /// <returns></returns>
        public List<TradeMD> GetTrade_M()
        {
            TradeServiece ts = new TradeServiece();
            return ts.GetTrade_M();
        }
        /// <summary>
        /// 办公楼多利多数据
        /// </summary>
        /// <returns></returns>
        public List<TradeMD> GetTrade_O()
        {
            TradeServiece ts = new TradeServiece();
            return ts.GetTrade_O();
        }
        /// <summary>
        /// 按时间获取办公楼多利多数据
        /// </summary>
        /// <returns></returns>
        public List<TradeMD> GetTrade_OByDate(DateTime startDate, DateTime endDate)
        {
            TradeServiece ts = new TradeServiece();
            return ts.GetTrade_OByDate(startDate,endDate);
        }
        /// <summary>
        /// 按时间获取机修楼多利多数据
        /// </summary>
        /// <returns></returns>
        public List<TradeMD> GetTrade_mByDate(DateTime startDate, DateTime endDate)
        {
            TradeServiece ts = new TradeServiece();
            return ts.GetTrade_mByDate(startDate, endDate);
        }
    }
}
