using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using Bohi.ERP.MODEL;

namespace Bohi.ERP.DAL
{
    public class PubMaterialService
    {
        /// <summary>
        /// 获取物料名称泛型
        /// </summary>
        /// <returns>物料名称泛型</returns>
        public List<PubMaterialMD> GetPubMaterial()
        {
            try
            {
                string connection = PublicClass.getConnecion();
                using (IDbConnection sc = new SqlConnection(connection))
                {
                    string sql = @"select ID,Code,Name,Mnemonic from PubMaterial";
                    return (List<PubMaterialMD>)sc.Query<PubMaterialMD>(sql, null);
                }
            }
            catch (Exception)
            {
                return null;
               // throw;
            }
        }
        /// <summary>
        /// 获取物料名称泛型
        /// </summary>
        /// <returns>物料名称泛型</returns>
        public List<PubMaterialMD> GetPubSoybeanMaterial()
        {
            try
            {
                string connection = PublicClass.getConnecion();
                using (IDbConnection sc = new SqlConnection(connection))
                {
                    string sql = @"select * from pubmaterial where  mattypeid='1013778'";
                    return (List<PubMaterialMD>)sc.Query<PubMaterialMD>(sql, null);
                }
            }
            catch (Exception)
            {
                return null;
              //  throw;
            }
        }
    }
}
