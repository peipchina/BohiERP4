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
    public class SoybeanAutoCodeService
    {
        /// <summary>
        /// 获取皮重车辆
        /// </summary>
        /// <returns></returns>
        public List<SoybeanAutoCodeMD> GetTareAutoCode()
        {
            //string connection = @"Password=strive@4012;Persist Security Info=True;User ID=sa;Initial Catalog=SSS_TEST;Data Source=172.168.1.39";
            string connection = PublicClass.getConnecion();
            using (SqlConnection sc = new SqlConnection(connection))
            {
                string sql = @"select * from v_bohi_soybeanAutocode
                                where taretime is null and grosstime is null order by SealsTime ";
                return (List<SoybeanAutoCodeMD>)sc.Query<SoybeanAutoCodeMD>(sql,null);
            }
        }
        /// <summary>
        /// 获取毛重车辆
        /// </summary>
        /// <returns></returns>
        public List<SoybeanAutoCodeMD> GetGrossAutoCode()
        {
            //string connection = @"Password=strive@4012;Persist Security Info=True;User ID=sa;Initial Catalog=SSS_TEST;Data Source=172.168.1.39";
            string connection = PublicClass.getConnecion();
            using (SqlConnection sc = new SqlConnection(connection))
            {
                string sql = @"SELECT VBS.*,TP.TarePoundName,tp.TareStfName FROM V_BOHI_SoybeanAutoCode AS VBS 
                                LEFT JOIN TarePoundName TP ON TP.IDFrom=VBS.PIPID
                                where taretime is not null and grosstime is null order by VBS.TareTime";
                return (List<SoybeanAutoCodeMD>)sc.Query<SoybeanAutoCodeMD>(sql, null);
            }
        }
        /// <summary>
        /// 插入一行毛重
        /// </summary>
        /// <param name="sac"></param>
        /// <returns></returns>
        public bool UpdataQTYGross(decimal QtyGross,long GrossStfID, DateTime GrossTime,long ID)
        {
            try
            {
                //string connection = @"Password=strive@4012;Persist Security Info=True;User ID=sa;Initial Catalog=SSS_TEST;Data Source=172.168.1.39";
                string connection = PublicClass.getConnecion();
                using (SqlConnection sc = new SqlConnection(connection))
                {
                    string sql = @"UPDATE [PurInPrison]
                               SET [QtyGross] = @QtyGross
                                  ,[GrossStfID] = @GrossStfID
                                  ,[GrossTime] = @GrossTime
                             WHERE [ID] = @ID";
                    int da = sc.Execute(sql, new { QtyGross= QtyGross, GrossStfID= GrossStfID, GrossTime= GrossTime, ID=ID });
                    return da > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 插入一行皮重
        /// </summary>
        /// <param name="sac"></param>
        /// <returns></returns>
        public bool UpdataQTYTare(decimal QtyTare, DateTime TareTime, long ID,long TareStfID)
        {
            try
            {
                //string connection = @"Password=strive@4012;Persist Security Info=True;User ID=sa;Initial Catalog=SSS_TEST;Data Source=172.168.1.39";
                string connection = PublicClass.getConnecion();
                using (SqlConnection sc = new SqlConnection(connection))
                {
                    string sql = @"UPDATE [PurInPrison]
                               SET [QtyTare] = @QtyTare
                                  ,[TareStfID] = @TareStfID
                                  ,[TareTime] = @TareTime
                             WHERE [ID] = @ID";
                    int da = sc.Execute(sql, new { QtyTare = QtyTare, TareTime = TareTime, TareStfID= TareStfID, ID = ID });
                    return da > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 事务，更新3S表皮重，插入一行对应的皮重地磅名
        /// </summary>
        /// <param name="QtyTare"></param>
        /// <param name="TareTime"></param>
        /// <param name="ID"></param>
        /// <param name="tpn"></param>
        /// <returns></returns>
        public bool TranTareAndTarnPound(decimal QtyTare,long TareStfID, DateTime TareTime, long ID, TarePoundNameMD tpn)
        {
            string sql1 = @"UPDATE [PurInPrison]
                               SET [QtyTare] = @QtyTare
                                  ,[TareStfID] = @TareStfID
                                  ,[TareTime] = @TareTime
                             WHERE [ID] = @ID";
            string sql2 = @"INSERT INTO [TarePoundName]
                                   ([TarePoundName]
                                   ,[IDFrom],[TareStfName])
                             VALUES
                                   (@TarePoundName
                                   ,@IDFrom,@TareStfName)";
            bool a;
            //string connection = @"Password=strive@4012;Persist Security Info=True;User ID=sa;Initial Catalog=SSS_TEST;Data Source=172.168.1.39";
            string connection = PublicClass.getConnecion();
            using (SqlConnection sc = new SqlConnection(connection))
            {
                if (sc.State== ConnectionState.Closed)
                {
                    sc.Open();
                }                
                IDbTransaction transaction = sc.BeginTransaction();
                try
                {
                    if( sc.Execute(sql1, new { QtyTare = QtyTare, TareStfID= TareStfID, TareTime = TareTime, ID = ID },transaction) <1)
                    {
                        transaction.Rollback();
                        a=false;
                    }
                    if( sc.Execute(sql2, tpn,transaction)<1)
                    {
                        transaction.Rollback();
                        a = false;
                    }
                    transaction.Commit();
                    a = true;
                }
                catch (Exception )
                {
                    a= false;
                   // transaction.Rollback();
                   // throw ex;
                }
            }
           // a = true;
            return a;
        }
        /// <summary>
        /// 插入一行地磅名（皮重）
        /// </summary>
        /// <param name="sac"></param>
        /// <returns></returns>
        public bool InsertTarePoundName(TarePoundNameMD tpn)
        {
            try
            {
                //string connection = @"Password=strive@4012;Persist Security Info=True;User ID=sa;Initial Catalog=SSS_TEST;Data Source=172.168.1.39";
                string connection = PublicClass.getConnecion();
                using (SqlConnection sc = new SqlConnection(connection))
                {
                    string sql = @"INSERT INTO [TarePoundName]
                                   ([TarePoundName]
                                   ,[IDFrom])
                             VALUES
                                   (@TarePoundName
                                   ,@IDFrom)";
                    int da = sc.Execute(sql, tpn);
                    return da > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 上一步（皮重）
        /// </summary>
        /// <param name="sac"></param>
        /// <returns></returns>
        public bool BackNext(long ID)
        {
            string sql = @"DELETE FROM [TarePoundName]
                                WHERE IDFrom=@ID";
            string sql1 = @"UPDATE [PurInPrison]
                               SET [QtyTare] = 0
                                  ,[TareStfID] = null
                                  ,[TareTime] = null
                             WHERE [ID] = @ID";
            bool a;
            //string connection = @"Password=strive@4012;Persist Security Info=True;User ID=sa;Initial Catalog=SSS_TEST;Data Source=172.168.1.39";
            string connection = PublicClass.getConnecion();
            using (SqlConnection sc = new SqlConnection(connection))
            {
                if (sc.State == ConnectionState.Closed)
                {
                    sc.Open();
                }
                IDbTransaction transaction = sc.BeginTransaction();
                try
                {
                    if (sc.Execute(sql1, new { ID = ID },transaction) < 1)
                    {
                        transaction.Rollback();
                        a = false;
                    }
                    if (sc.Execute(sql, new { ID = ID }, transaction) < 1)
                    {
                        transaction.Rollback();
                        a = false;
                    }
                    transaction.Commit();
                    a = true;
                }
                catch (Exception)
                {
                    a = false;
                    //transaction.Rollback();
                    // throw ex;
                }
            }
            // a = true;
            return a;
        }
    }
}
