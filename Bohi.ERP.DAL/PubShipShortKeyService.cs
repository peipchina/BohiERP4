using Bohi.ERP.MODEL;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Bohi.ERP.DAL
{
    public class PubShipShortKeyService
    {
        /// <summary>
        /// 获取船名快捷泛型
        /// </summary>
        /// <returns>获取船名快捷泛型</returns>
        public List<ShortKeyNameMD> GetAllShipShortKey()
        {
            string connection = PublicClass.getConnecion180();
            using (SqlConnection sc = new SqlConnection(connection))
            {
                string sql = @"select * from PubShipShortKey";
                return (List<ShortKeyNameMD>)sc.Query<ShortKeyNameMD>(sql, null);
            }
        }
        /// <summary>
        /// 插入船名对应快捷方式
        /// </summary>
        /// <param name="pa"></param>
        /// <returns>是否成功</returns>
        public bool InsertShipName(ShortKeyNameMD pa)
        {
            string connection = PublicClass.getConnecion180();
            using (SqlConnection sc = new SqlConnection(connection))
            {
                string sql = @"INSERT INTO [BohiErp].[dbo].[PubShipShortKey]
                               ([ShortKey]
                               ,[Name])
                         VALUES
                               (@ShortKey
                               ,@Name)";
                return sc.Execute(sql, pa) > 0;
            }
        }
        /// <summary>
        /// 修改快捷键对应船名
        /// </summary>
        /// <param name="pa"></param>
        /// <returns>是否成功</returns>
        public bool UpdataShipName(ShortKeyNameMD pa)
        {
            string connection = PublicClass.getConnecion180();
            using (SqlConnection sc = new SqlConnection(connection))
            {
                string sql = @"UPDATE [BohiErp].[dbo].[PubShipShortKey]
                               SET [ShortKey] = @ShortKey
                                  ,[Name] = @Name
                             WHERE ID=@ID";
                return sc.Execute(sql, pa) > 0;
            }
        }
        /// <summary>
        /// 删除快捷键对应船名
        /// </summary>
        /// <param name="pa"></param>
        /// <returns>是否成功</returns>
        public bool DeleteShipName(ShortKeyNameMD pa)
        {
            string connection = PublicClass.getConnecion180();
            using (SqlConnection sc = new SqlConnection(connection))
            {
                string sql = @"DELETE FROM [BohiErp].[dbo].[PubShipShortKey]
                                WHERE id=@id";
                return sc.Execute(sql, pa) > 0;
            }
        }
        /// <summary>
        /// 根据快捷键检查船名是否存在
        /// </summary>
        /// <param name="number"></param>
        /// <returns>CountMD</returns>
        public List<CountMD> ChekShortKey(string ShortKey)
        {
            string connection = PublicClass.getConnecion180();
            using (SqlConnection sc = new SqlConnection(connection))
            {
                string sql = @"select count(*) counts from PubShipShortKey
                                where ShortKey=@shortKey";
                return (List<CountMD>)sc.Query<CountMD>(sql, new { ShortKey = ShortKey });
            }
        }
        /// <summary>
        /// 根据船名检查是否存在
        /// </summary>
        /// <param name="number"></param>
        /// <returns>CountMD</returns>
        public List<CountMD> ChekShipName(string Name)
        {
            string connection = PublicClass.getConnecion180();
            using (SqlConnection sc = new SqlConnection(connection))
            {
                string sql = @"select count(*) counts from PubShipShortKey
                                where Name=@Name";
                return (List<CountMD>)sc.Query<CountMD>(sql, new { Name = Name });
            }
        }
    }
}
