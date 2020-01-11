using Bohi.ERP.DAL;
using Bohi.ERP.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bohi.ERP.BLL
{
    public class PubShipShortKeyManager
    {
        /// <summary>
        /// 获取物料快捷泛型
        /// </summary>
        /// <returns>获取物料快捷泛型</returns>
        public List<ShortKeyNameMD> GetAllShipShortKey()
        {
            PubShipShortKeyService psks = new PubShipShortKeyService();
            return psks.GetAllShipShortKey();
        }
        /// <summary>
        /// 插入物料对应快捷方式
        /// </summary>
        /// <param name="pa"></param>
        /// <returns>是否成功</returns>
        public bool InsertShipName(ShortKeyNameMD pa)
        {
            PubShipShortKeyService psks = new PubShipShortKeyService();
            return psks.InsertShipName(pa);
        }
        /// <summary>
        /// 修改快捷键对应物料
        /// </summary>
        /// <param name="pa"></param>
        /// <returns>是否成功</returns>
        public bool UpdataShipName(ShortKeyNameMD pa)
        {
            PubShipShortKeyService psks = new PubShipShortKeyService();
            return psks.UpdataShipName(pa);
        }
        /// <summary>
        /// 删除快捷键对应物料
        /// </summary>
        /// <param name="pa"></param>
        /// <returns>是否成功</returns>
        public bool DeleteShipName(ShortKeyNameMD pa)
        {
            PubShipShortKeyService psks = new PubShipShortKeyService();
            return psks.DeleteShipName(pa);
        }
        /// <summary>
        /// 根据快捷键检查物料是否存在
        /// </summary>
        /// <param name="number"></param>
        /// <returns>CountMD</returns>
        public bool ChekShipName(string Name)
        {
            PubShipShortKeyService psks = new PubShipShortKeyService();
            List<CountMD> lc = psks.ChekShipName(Name);
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
            PubShipShortKeyService psks = new PubShipShortKeyService();
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
