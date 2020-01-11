using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Bohi.ERP.MODEL;
using Dapper;

namespace Bohi.ERP.DAL
{
    public class PubcustomerService
    {
        /// <summary>
        /// 获取所有客户资料
        /// </summary>
        /// <returns>客户资料泛型</returns>
        public List<PubcustomerMD> GetPubCustomer()
        {
            string connection = PublicClass.getConnecion();
            using (IDbConnection sc=new SqlConnection(connection))
            {
                string sql = @"select ID,Name from pubcustomer";
                return (List<PubcustomerMD>)sc.Query<PubcustomerMD>(sql,null);
            }
        }
    }
}
