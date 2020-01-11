using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Bohi.ERP.MODEL;
using Dapper;

namespace Bohi.ERP.DAL
{
    public class PubAutoCodeService
    {
        /// <summary>
        /// 获取大都车泛型
        /// </summary>
        /// <returns>所有大豆车泛型</returns>
        public  List<PubAutoCodeMD> GetAllAutoCode()
        {
            string connection = PublicClass.getConnecion180();
            using (SqlConnection sc=new SqlConnection(connection))
            {
                string sql = @"select * from PubAutoCode";
                return (List<PubAutoCodeMD>)sc.Query<PubAutoCodeMD>(sql,null);
            }
        }
        /// <summary>
        /// 获取大都车泛型
        /// </summary>
        /// <returns>所有大豆车泛型</returns>
        public List<ShortKeyNameMD> GetAllAutoShortKey()
        {
            string connection = PublicClass.getConnecion180();
            using (SqlConnection sc = new SqlConnection(connection))
            {
                string sql = @"select ID,autocode as Name,Number as ShortKey from PubAutoCode";
                return (List<ShortKeyNameMD>)sc.Query<ShortKeyNameMD>(sql, null);
            }
        }
        /// <summary>
        /// 插入车辆对应快捷方式
        /// </summary>
        /// <param name="pa"></param>
        /// <returns>是否成功</returns>
        public bool InsertSoybean(PubAutoCodeMD pa)
        {
            string connection = PublicClass.getConnecion180();
            using (SqlConnection sc = new SqlConnection(connection))
            {
                string sql = @"INSERT INTO [BohiErp].[dbo].[PubAutoCode]
                               ([autocode]
                               ,[Number])
                         VALUES
                               (@autocode
                               ,@Number)";
                return sc.Execute(sql, pa)>0;
            }
        }
        /// <summary>
        /// 修改快捷键对应车号
        /// </summary>
        /// <param name="pa"></param>
        /// <returns>是否成功</returns>
        public bool UpdataSoybean(PubAutoCodeMD pa)
        {
            string connection = PublicClass.getConnecion180();
            using (SqlConnection sc = new SqlConnection(connection))
            {
                string sql = @"UPDATE [BohiErp].[dbo].[PubAutoCode]
                               SET [autocode] = @autocode     
                             WHERE id=@id";
                return sc.Execute(sql, pa) > 0;
            }
        }
        /// <summary>
        /// 删除快捷键对应车号
        /// </summary>
        /// <param name="pa"></param>
        /// <returns>是否成功</returns>
        public bool DeleteSoybean(PubAutoCodeMD pa)
        {
            string connection = PublicClass.getConnecion180();
            using (SqlConnection sc = new SqlConnection(connection))
            {
                string sql = @"DELETE FROM [BohiErp].[dbo].[PubAutoCode]
                                WHERE id=@id";
                return sc.Execute(sql, pa) > 0;
            }
        }
        /// <summary>
        /// 根据快捷键检查车号个数
        /// </summary>
        /// <param name="number"></param>
        /// <returns>CountMD</returns>
        public List<CountMD> ChekShortKey(string  number)
        {
            string connection = PublicClass.getConnecion180();
            using (SqlConnection sc = new SqlConnection(connection))
            {
                string sql = @"select COUNT(*) counts from pubautocode where Number=@Number";
                return (List<CountMD>)sc.Query<CountMD>(sql, new { Number= number });
            }
        }
        /// <summary>
        /// 根据车号检查数据库
        /// </summary>
        /// <param name="number"></param>
        /// <returns>CountMD</returns>
        public List<CountMD> ChekAutoCode(string autocode)
        {
            string connection = PublicClass.getConnecion180();
            using (SqlConnection sc = new SqlConnection(connection))
            {
                string sql = @"select COUNT(*) counts from pubautocode where  autocode=@autocode";
                return (List<CountMD>)sc.Query<CountMD>(sql, new { autocode = autocode });
            }
        }
    }
}
