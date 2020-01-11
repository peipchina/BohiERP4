using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bohi.ERP.MODEL;
using Bohi.ERP.DAL;

namespace Bohi.ERP.BLL
{
    public class PubAutoCodeManager
    {
        /// <summary>
        /// 获取大都车泛型
        /// </summary>
        /// <returns>所有大豆车泛型</returns>
        public List<PubAutoCodeMD> GetAllAutoCode()
        {
            PubAutoCodeService pas = new PubAutoCodeService();
            return pas.GetAllAutoCode();
        }
        /// <summary>
        /// 获取大都车泛型
        /// </summary>
        /// <returns>所有大豆车泛型</returns>
        public List<ShortKeyNameMD> GetAllAutoShortKey()
        {
            PubAutoCodeService pas = new PubAutoCodeService();
            return pas.GetAllAutoShortKey();
        }
        /// <summary>
        /// 插入车辆对应快捷方式
        /// </summary>
        /// <param name="pa"></param>
        /// <returns>是否成功</returns>
        public bool InsertSoybean(PubAutoCodeMD pa)
        {
            PubAutoCodeService pas = new PubAutoCodeService();
            return pas.InsertSoybean(pa);
        }
        /// <summary>
        /// 修改快捷键对应车号
        /// </summary>
        /// <param name="pa"></param>
        /// <returns>是否成功</returns>
        public bool UpdataSoybean(PubAutoCodeMD pa)
        {
            PubAutoCodeService pas = new PubAutoCodeService();
            return pas.UpdataSoybean(pa);
        }
        /// <summary>
        /// 删除快捷键对应车号
        /// </summary>
        /// <param name="pa"></param>
        /// <returns>是否成功</returns>
        public bool DeleteSoybean(PubAutoCodeMD pa)
        {
            PubAutoCodeService pas = new PubAutoCodeService();
            return pas.DeleteSoybean(pa);
        }
        /// <summary>
        /// 根据快捷键检查车号是否存在
        /// </summary>
        /// <param name="number"></param>
        /// <returns>CountMD</returns>
        public bool ChekShortKey(string  number)
        {
            PubAutoCodeService pas = new PubAutoCodeService();
            List<CountMD> lc= pas.ChekShortKey(number);
            if(lc.Count>0)
            {
                return lc[0].Counts > 0;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 根据车号检查数据库
        /// </summary>
        /// <param name="number"></param>
        /// <returns>CountMD</returns>
        public bool ChekAutoCode(string autocode)
        {
            PubAutoCodeService pas = new PubAutoCodeService();
            List<CountMD> lc = pas.ChekAutoCode(autocode);
            if (lc.Count > 0)
            {
                return lc[0].Counts > 0;
            }
            else
            {
                return false;
            }
        }
    }
}
