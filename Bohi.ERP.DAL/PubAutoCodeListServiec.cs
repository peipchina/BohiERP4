using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bohi.ERP.MODEL;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace Bohi.ERP.DAL
{
    public class PubAutoCodeListServiec
    {
        /// <summary>
        /// 所有进厂车辆信息
        /// </summary>        
        /// <returns>所有进厂车辆信息</returns>
        public List<PubAutoCodeListMD> GetAllAutoCode()
        {
            try
            {
                string connection = PublicClass.getConnecion180();
                using (IDbConnection sc = new SqlConnection(connection))
                {
                    string sql = @"select  distinct autocode from poundtotal
                                where IsDelete=0";
                    return (List<PubAutoCodeListMD>)sc.Query<PubAutoCodeListMD>(sql,null);
                }
            }
            catch (Exception)
            {
                return null;                
            }
        }
    }
}
