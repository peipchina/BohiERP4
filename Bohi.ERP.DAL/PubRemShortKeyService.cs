using Bohi.ERP.MODEL;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Bohi.ERP.DAL
{
    public class PubRemShortKeyService
    {
        /// <summary>
        /// 获取备注快捷泛型
        /// </summary>
        /// <returns>获取备注快捷泛型</returns>
        public List<ShortKeyNameMD> GetAllRemShortKey()
        {
            string connection = PublicClass.getConnecion180();
            using (SqlConnection sc = new SqlConnection(connection))
            {
                string sql = @"select * from PubRemShortKey";
                return (List<ShortKeyNameMD>)sc.Query<ShortKeyNameMD>(sql, null);
            }
        }
        /// <summary>
        /// 插入备注对应快捷方式
        /// </summary>
        /// <param name="pa"></param>
        /// <returns>是否成功</returns>
        public bool InsertRemName(ShortKeyNameMD pa)
        {
            string connection = PublicClass.getConnecion180();
            using (SqlConnection sc = new SqlConnection(connection))
            {
                string sql = @"INSERT INTO [BohiErp].[dbo].[PubRemShortKey]
                               ([ShortKey]
                               ,[Name])
                         VALUES
                               (@ShortKey
                               ,@Name)";
                return sc.Execute(sql, pa) > 0;
            }
        }
        /// <summary>
        /// 修改快捷键对应备注
        /// </summary>
        /// <param name="pa"></param>
        /// <returns>是否成功</returns>
        public bool UpdataRemName(ShortKeyNameMD pa)
        {
            string connection = PublicClass.getConnecion180();
            using (SqlConnection sc = new SqlConnection(connection))
            {
                string sql = @"UPDATE [BohiErp].[dbo].[PubRemShortKey]
                               SET [ShortKey] = @ShortKey
                                  ,[Name] = @Name
                             WHERE ID=@ID";
                return sc.Execute(sql, pa) > 0;
            }
        }
        /// <summary>
        /// 删除快捷键对应备注
        /// </summary>
        /// <param name="pa"></param>
        /// <returns>是否成功</returns>
        public bool DeleteRemName(ShortKeyNameMD pa)
        {
            string connection = PublicClass.getConnecion180();
            using (SqlConnection sc = new SqlConnection(connection))
            {
                string sql = @"DELETE FROM [BohiErp].[dbo].[PubRemShortKey]
                                WHERE id=@id";
                return sc.Execute(sql, pa) > 0;
            }
        }
        /// <summary>
        /// 根据快捷键检查备注是否存在
        /// </summary>
        /// <param name="number"></param>
        /// <returns>CountMD</returns>
        public List<CountMD> ChekShortKey(string ShortKey)
        {
            string connection = PublicClass.getConnecion180();
            using (SqlConnection sc = new SqlConnection(connection))
            {
                string sql = @"select count(*) counts from PubRemShortKey
                                where ShortKey=@shortKey";
                return (List<CountMD>)sc.Query<CountMD>(sql, new { ShortKey = ShortKey });
            }
        }
        /// <summary>
        /// 根据备注检查是否存在
        /// </summary>
        /// <param name="number"></param>
        /// <returns>CountMD</returns>
        public List<CountMD> ChekRemName(string Name)
        {
            string connection = PublicClass.getConnecion180();
            using (SqlConnection sc = new SqlConnection(connection))
            {
                string sql = @"select count(*) counts from PubRemShortKey
                                where Name=@Name";
                return (List<CountMD>)sc.Query<CountMD>(sql, new { Name = Name });
            }
        }
    }
}
