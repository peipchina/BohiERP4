using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bohi.ERP.DAL;
using Bohi.ERP.MODEL;

namespace Bohi.ERP.BLL
{
    public class PubMaterialManager
    {
        /// <summary>
        /// 获取物料名称泛型
        /// </summary>
        /// <returns>物料名称泛型</returns>
        public List<PubMaterialMD> GetPubMaterial()
        {
            PubMaterialService pms = new PubMaterialService();
            return pms.GetPubMaterial();
        }
        /// <summary>
        /// 获取物料名称泛型
        /// </summary>
        /// <returns>物料名称泛型</returns>
        public List<PubMaterialMD> GetPubSoybeanMaterial()
        {
            PubMaterialService pms = new PubMaterialService();
            return pms.GetPubSoybeanMaterial();
        }
    }
}
