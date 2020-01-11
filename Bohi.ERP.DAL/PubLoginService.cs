using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bohi.ERP.MODEL;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace Bohi.ERP.DAL
{
    public class PubLoginService
    {
        #region 根据登录名获取所有信息
        /// <summary>
        /// 根据登录名获取所有信息
        /// </summary>
        /// <param name="lonname"></param>
        /// <returns>登录信息</returns>
        public List<PubLoginMD> getPassWordByLoginName(string lonname)
        {
            string connection = PublicClass.getConnecion180();
            using (SqlConnection sc = new SqlConnection(connection))
            {
                string sql = @"SELECT    ID, LoginName, LastLoginIPAdress,
                      PassWord,Name,StfNameID
                    FROM         pubDelIn 
                    Where LoginName=@LoginName";
                return (List<PubLoginMD>)sc.Query<PubLoginMD>(sql, new { LoginName = lonname });
            }
        }
        #endregion

        #region 检查用户名是否存在
        /// <summary>
        /// 检查用户名是否存在
        /// </summary>
        /// <param name="lonname"></param>
        /// <returns>是否存在</returns>
        public bool CheckLoginName(string lonname)
        {
            string connection = PublicClass.getConnecion180();
            using (SqlConnection sc = new SqlConnection(connection))
            {
                string sql = @"select COUNT(*) from pubDelIn where LoginName=@LoginName";
                SqlCommand sqlCommand = sc.CreateCommand();
                sqlCommand.CommandText = sql;
                SqlParameter sp = new SqlParameter("@LoginName", lonname);                
                sqlCommand.Parameters.Add(sp);
                if (sc.State!=ConnectionState.Open)
                {
                    sc.Open();
                }
                SqlDataReader reader = sqlCommand.ExecuteReader();
                reader.Read();
                return reader.GetInt32(0) > 0;                
            }
        }
        #endregion

        #region 添加用户
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="lonname"></param>
        /// <returns>是否成功</returns>
        public bool  AddNewUser(PubLoginMD pl)
        {
            string connection = PublicClass.getConnecion180();
            using (SqlConnection sc = new SqlConnection(connection))
            {
                string sql = @"INSERT INTO [BohiErp].[dbo].[pubDelIn]
                                   ([LoginName]
                                   ,[PassWord]           
                                   ,[Name])
                             VALUES
                                   (@LoginName
                                   ,@PassWord           
                                   ,@Name)";
                return  sc.Execute(sql, pl)>0;
            }
        }
        #endregion

        #region 修改密码
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="lonname"></param>
        /// <returns>影响行</returns>
        public int UpPassWord(PubLoginMD pdi)
        {
            string connection = PublicClass.getConnecion180();
            using (SqlConnection sc = new SqlConnection(connection))
            {
                string sql = @" update pubDelIn set PassWord=@PassWord where LoginName=@LoginName";
                int a = sc.Execute(sql, pdi);
                return a;
            }
        }
        #endregion
    }
}
