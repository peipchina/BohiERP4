using Bohi.ERP.DAL;
using Bohi.ERP.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bohi.ERP.BLL
{
    public class PubRemShortKeyManager
    {
        /// <summary>
        /// 获取备注快捷泛型
        /// </summary>
        /// <returns>获取备注快捷泛型</returns>
        public List<ShortKeyNameMD> GetAllRemShortKey()
        {
            PubRemShortKeyService psks = new PubRemShortKeyService();
            return psks.GetAllRemShortKey();
        }
        /// <summary>
        /// 插入备注对应快捷方式
        /// </summary>
        /// <param name="pa"></param>
        /// <returns>是否成功</returns>
        public bool InsertRemName(ShortKeyNameMD pa)
        {
            PubRemShortKeyService psks = new PubRemShortKeyService();
            return psks.InsertRemName(pa);
        }
        /// <summary>
        /// 修改快捷键对应备注
        /// </summary>
        /// <param name="pa"></param>
        /// <returns>是否成功</returns>
        public bool UpdataRemName(ShortKeyNameMD pa)
        {
            PubRemShortKeyService psks = new PubRemShortKeyService();
            return psks.UpdataRemName(pa);
        }
        /// <summary>
        /// 删除快捷键对应备注
        /// </summary>
        /// <param name="pa"></param>
        /// <returns>是否成功</returns>
        public bool DeleteRemName(ShortKeyNameMD pa)
        {
            PubRemShortKeyService psks = new PubRemShortKeyService();
            return psks.DeleteRemName(pa);
        }
        /// <summary>
        /// 根据快捷键检查备注是否存在
        /// </summary>
        /// <param name="number"></param>
        /// <returns>CountMD</returns>
        public bool ChekRemName(string Name)
        {
            PubRemShortKeyService psks = new PubRemShortKeyService();
            List<CountMD> lc = psks.ChekRemName(Name);
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
        /// 根据备注检查快捷键是否存在
        /// </summary>
        /// <param name="number"></param>
        /// <returns>CountMD</returns>
        public bool ChekShortKey(string ShortKey)
        {
            PubRemShortKeyService psks = new PubRemShortKeyService();
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
