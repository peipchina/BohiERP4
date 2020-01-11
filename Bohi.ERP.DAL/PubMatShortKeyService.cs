using Bohi.ERP.MODEL;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Bohi.ERP.DAL
{
    public class PubMatShortKeyService
    {

        /// <summary>
        /// 获取物料快捷泛型
        /// </summary>
        /// <returns>获取物料快捷泛型</returns>
        public List<MatShortKeyMD> GetAllMatShortKey()
        {
            string connection = PublicClass.getConnecion180();
            using (SqlConnection sc = new SqlConnection(connection))
            {
                string sql = @"select * from PubMatShortKey";
                return (List<MatShortKeyMD>)sc.Query<MatShortKeyMD>(sql, null);
            }
        }
        /// <summary>
        /// 获取大豆快捷泛型
        /// </summary>
        /// <returns>获取大豆快捷泛型</returns>
        public List<MatShortKeyMD> GetSoybeanMatShortKey()
        {
            string connection = PublicClass.getConnecion180();
            using (SqlConnection sc = new SqlConnection(connection))
            {
                string sql = @"select * from PubMatShortKey where Name='大豆'";
                return (List<MatShortKeyMD>)sc.Query<MatShortKeyMD>(sql, null);
            }
        }
        /// <summary>
        /// 插入物料对应快捷方式
        /// </summary>
        /// <param name="pa"></param>
        /// <returns>是否成功</returns>
        public bool InsertMatName(MatShortKeyMD pa)
        {
            string connection = PublicClass.getConnecion180();
            using (SqlConnection sc = new SqlConnection(connection))
            {
                string sql = @"INSERT INTO [BohiErp].[dbo].[PubMatShortKey]
                               ([ShortKey]
                               ,[Name]
                                ,[Remarks]
                                ,[TransportCo]
                                ,[DeliveryCo],[ShipAndVoyage]
                                ,[Customer])
                         VALUES
                               (@ShortKey
                               ,@Name,@Remarks,@TransportCo,@DeliveryCo,@ShipAndVoyage,@Customer)";
                return sc.Execute(sql, pa) > 0;
            }
        }
        /// <summary>
        /// 修改快捷键对应物料
        /// </summary>
        /// <param name="pa"></param>
        /// <returns>是否成功</returns>
        public bool UpdataMatName(MatShortKeyMD pa)
        {
            string connection = PublicClass.getConnecion180();
            using (SqlConnection sc = new SqlConnection(connection))
            {
                string sql = @"UPDATE [BohiErp].[dbo].[PubMatShortKey]
                               SET [ShortKey] = @ShortKey
                                  ,[Name] = @Name,[Remarks] = @Remarks
                                  ,[TransportCo] = @TransportCo
                                  ,[DeliveryCo] = @DeliveryCo,[ShipAndVoyage]=@ShipAndVoyage
                                  ,[Customer] = @Customer
                             WHERE ID=@ID";
                return sc.Execute(sql, pa) > 0;
            }
        }
        /// <summary>
        /// 删除快捷键对应物料
        /// </summary>
        /// <param name="pa"></param>
        /// <returns>是否成功</returns>
        public bool DeleteMatName(MatShortKeyMD pa)
        {
            string connection = PublicClass.getConnecion180();
            using (SqlConnection sc = new SqlConnection(connection))
            {
                string sql = @"DELETE FROM [BohiErp].[dbo].[PubMatShortKey]
                                WHERE id=@id";
                return sc.Execute(sql, pa) > 0;
            }
        }
        /// <summary>
        /// 根据快捷键检查物料是否存在
        /// </summary>
        /// <param name="number"></param>
        /// <returns>CountMD</returns>
        public List<CountMD> ChekShortKey(string ShortKey)
        {
            string connection = PublicClass.getConnecion180();
            using (SqlConnection sc = new SqlConnection(connection))
            {
                string sql = @"select count(*) counts from pubmatshortkey
                                where ShortKey=@shortKey";
                return (List<CountMD>)sc.Query<CountMD>(sql, new { ShortKey = ShortKey });
            }
        }
        /// <summary>
        /// 根据物料名检查快捷键是否存在
        /// </summary>
        /// <param name="number"></param>
        /// <returns>CountMD</returns>
        public List<CountMD> ChekMatName(string Name)
        {
            string connection = PublicClass.getConnecion180();
            using (SqlConnection sc = new SqlConnection(connection))
            {
                string sql = @"select count(*) counts from pubmatshortkey
                                where Name=@Name";
                return (List<CountMD>)sc.Query<CountMD>(sql, new { Name = Name });
            }
        }
    }
}
