using Bohi.ERP.DAL;
using Bohi.ERP.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bohi.ERP.BLL
{
    public class PubBlackAutoCodeManager
    {
        #region 获取黑名单列表
        /// <summary>
        /// 获取黑名单列表
        /// </summary>
        /// <returns>司机黑名单</returns>
        public List<PubBlackAutoCodeMD> getPubBlackCode()
        {
            PubBlackAutoCodeService pubBlackAutoCodeService = new PubBlackAutoCodeService();
            return pubBlackAutoCodeService.getPubBlackCode();

        }
        #endregion
        #region 根据车号获取黑名单列表
        /// <summary>
        /// 根据车号获取黑名单列表
        /// </summary>
        /// <returns>司机黑名单</returns>
        public List<PubBlackAutoCodeMD> getPubBlackCodeByAutocode(string AutoCode)
        {
            PubBlackAutoCodeService pubBlackAutoCodeService = new PubBlackAutoCodeService();
            return pubBlackAutoCodeService.getPubBlackCodeByAutocode(AutoCode);

        }
        #endregion
    }
}
