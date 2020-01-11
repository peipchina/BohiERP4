using Bohi.ERP.MODEL;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Bohi.ERP.DAL
{
    public class RebackCheckService
    {
        /// <summary>
        /// 添加一行车辆重量不足，回装数据
        /// </summary>        
        /// <returns>真假</returns>
        public bool AddReback(RebackCheckMD rebackCheckMD)
        {
            try
            {
                string connection = PublicClass.getConnecion180();
                using (IDbConnection sc = new SqlConnection(connection))
                {
                    string sql = @"INSERT INTO [dbo].[RebackCheck]
                           ([AutoCode]
                           ,[TareTime]
                           ,[QtyTare],[LestQty]
                           ,[RebackTime]
                           ,[MatName]
                           ,[RebackStf]
                           )
                     VALUES
                           (@AutoCode
                           ,@TareTime
                           ,@QtyTare,@LestQty
                           ,@RebackTime
                           ,@MatName
                           ,@RebackStf
                           )";
                    return sc.Execute(sql,rebackCheckMD)>0;
                }
            }
            catch (Exception)
            {
                return false;
                //throw;
            }
        }
    }
}
