using Bohi.ERP.MODEL;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Bohi.ERP.DAL
{
    public class PubBlackAutoCodeService
    {
        #region 获取黑名单列表
        /// <summary>
        /// 获取黑名单列表
        /// </summary>
        /// <returns>司机黑名单</returns>
        public List<PubBlackAutoCodeMD> getPubBlackCode()
        {
            string connection = PublicClass.getConnecion180();
            using (SqlConnection sc = new SqlConnection(connection))
            {
                string sql = @"SELECT  [ID]
                          ,[AutoCode]
                          ,[Driver]
                          ,[BlackTime]
                          ,[reason]
                          ,[CheckName]
                          ,[Results]
                          ,[CreatName]
                      FROM [BohiErp].[dbo].[PubBlackAutoCode] where isdelete=0";
                return (List<PubBlackAutoCodeMD>)sc.Query<PubBlackAutoCodeMD>(sql, null);
            }

        }
        #endregion
        #region 根据车号获取黑名单列表
        /// <summary>
        /// 根据车号获取黑名单列表
        /// </summary>
        /// <returns>司机黑名单</returns>
        public List<PubBlackAutoCodeMD> getPubBlackCodeByAutocode(string AutoCode)
        {
            string connection = PublicClass.getConnecion180();
            using (SqlConnection sc = new SqlConnection(connection))
            {
                string sql = @"SELECT  [ID]
                          ,[AutoCode]
                          ,[Driver]
                          ,[BlackTime]
                          ,[reason]
                          ,[CheckName]
                          ,[Results]
                          ,[CreatName]
                      FROM [BohiErp].[dbo].[PubBlackAutoCode] where AutoCode=@AutoCode and isdelete=0";
                return (List<PubBlackAutoCodeMD>)sc.Query<PubBlackAutoCodeMD>(sql, new { AutoCode= AutoCode });
            }

        }
        #endregion
    }
}
