using Bohi.ERP.DAL;
using Bohi.ERP.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bohi.ERP.BLL
{
    public class PubMatShortKeyManager
    {

        /// <summary>
        /// 获取物料快捷泛型
        /// </summary>
        /// <returns>获取物料快捷泛型</returns>
        public List<MatShortKeyMD> GetAllMatShortKey()
        {
            PubMatShortKeyService psks = new PubMatShortKeyService();
            return psks.GetAllMatShortKey();
        }
        /// <summary>
        /// 获取大豆快捷泛型
        /// </summary>
        /// <returns>获取大豆快捷泛型</returns>
        public List<MatShortKeyMD> GetSoybeanMatShortKey()
        {
            PubMatShortKeyService psks = new PubMatShortKeyService();
            return psks.GetSoybeanMatShortKey();
        }
        /// <summary>
        /// 插入物料对应快捷方式
        /// </summary>
        /// <param name="pa"></param>
        /// <returns>是否成功</returns>
        public bool InsertMatName(MatShortKeyMD pa)
        {
            PubMatShortKeyService psks = new PubMatShortKeyService();
            return psks.InsertMatName(pa);
        }
        /// <summary>
        /// 修改快捷键对应物料
        /// </summary>
        /// <param name="pa"></param>
        /// <returns>是否成功</returns>
        public bool UpdataMatName(MatShortKeyMD pa)
        {
            PubMatShortKeyService psks = new PubMatShortKeyService();
            return psks.UpdataMatName(pa);
        }
        /// <summary>
        /// 删除快捷键对应物料
        /// </summary>
        /// <param name="pa"></param>
        /// <returns>是否成功</returns>
        public bool DeleteMatName(MatShortKeyMD pa)
        {
            PubMatShortKeyService psks = new PubMatShortKeyService();
            return psks.DeleteMatName(pa);
        }
        /// <summary>
        /// 根据快捷键检查物料是否存在
        /// </summary>
        /// <param name="number"></param>
        /// <returns>CountMD</returns>
        public bool ChekMatName(string Name)
        {
            PubMatShortKeyService psks = new PubMatShortKeyService();
            List<CountMD> lc= psks.ChekMatName(Name);
            if (lc.Count > 0)
            {
                return lc[0].Counts > 0;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 根据物料名检查快捷键是否存在
        /// </summary>
        /// <param name="number"></param>
        /// <returns>CountMD</returns>
        public bool ChekShortKey(string ShortKey)
        {
            PubMatShortKeyService psks = new PubMatShortKeyService();
            List<CountMD> lc = psks.ChekShortKey(ShortKey);
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
