using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bohi.ERP.MODEL;
using Bohi.ERP.DAL;

namespace Bohi.ERP.BLL
{
    public class PubLoginManager
    {

        #region 检查用户名是否存在
        /// <summary>
        /// 检查用户名是否存在
        /// </summary>
        /// <param name="lonname"></param>
        /// <returns>是否存在</returns>
        public bool CheckLoginName(string lonname)
        {
            PubLoginService plm = new PubLoginService();
            return plm.CheckLoginName(lonname);
        }
        #endregion

        #region 根据登录名获取所有信息
        /// <summary>
        /// 根据登录名获取所有信息
        /// </summary>
        /// <param name="lonname"></param>
        /// <returns>登录信息</returns>
        public List<PubLoginMD> getPassWordByLoginName(string lonname)
        {
            PubLoginService pls = new PubLoginService();
            return pls.getPassWordByLoginName(lonname);
        }
        #endregion

        #region 添加用户
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="lonname"></param>
        /// <returns>是否成功</returns>
        public bool AddNewUser(PubLoginMD pl)
        {            
            PubLoginService pls = new PubLoginService();
            return pls.AddNewUser(pl);
        }
        #endregion

        #region 修改密码
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="lonname"></param>
        /// <returns>影响行</returns>
        public bool UpPassWord(PubLoginMD pdi)
        {
            PubLoginService pls = new PubLoginService();
            return pls.UpPassWord(pdi)>0;
        }
        #endregion
    }
}
