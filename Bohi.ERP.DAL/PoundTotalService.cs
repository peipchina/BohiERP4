using Bohi.ERP.MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;

namespace Bohi.ERP.DAL
{
    public class PoundTotalService
    {
        /// <summary>
        /// 获取某段时间车辆过磅信息
        /// </summary>        
        /// <returns>车辆过磅信息泛型</returns>
        public List<PoundTotalMD> GetPoundListByDateTime(DateTime StartTime,DateTime EndTime)
        {
            try
            {
                string connection = PublicClass.getConnecion180();
                using (IDbConnection sc = new SqlConnection(connection))
                {
                    string sql = @"select * from PoundTotal
                        WHERE  IsDelete=0  and (IsFinished = 1) AND (GrossTime BETWEEN @StartTime AND @EndTime)
                        ORDER BY No DESC ";
                    return (List<PoundTotalMD>)sc.Query<PoundTotalMD>(sql, new { StartTime = StartTime, EndTime = EndTime });
                }
            }
            catch (Exception)
            {
                return null;
                //throw;
            }
        }
        /// <summary>
        ///根据车号获取车辆过磅信息
        /// </summary>        
        /// <returns>车辆过磅信息泛型</returns>
        public List<PoundTotalMD> GetPoundListByAutoCode(string autoCode)
        {
            try
            {
                string connection = PublicClass.getConnecion180();
                using (IDbConnection sc = new SqlConnection(connection))
                {
                    string sql = @"select * from PoundTotal
                        WHERE  IsDelete=0  and (IsFinished = 1) AND  Autocode like @AutoCode 
                        ORDER BY No DESC ";
                    return (List<PoundTotalMD>)sc.Query<PoundTotalMD>(sql, new { AutoCode= autoCode });
                }
            }
            catch (Exception)
            {
                return null;
                //throw;
            }
        }
        /// <summary>
        /// 根据车号获取车辆皮重
        /// </summary>
        /// <param name="autoCode">车号</param>
        /// <returns>皮重泛型</returns>
        public List<TareQtyMD> GetAllTareQty(string autoCode)
        {
            string connection = PublicClass.getConnecion180();
            using (IDbConnection sc = new SqlConnection(connection))
            {
                string sql = @"select top 10 QTYTare from PoundTotal 
                                where IsDelete=0 and  AutoCode=@AutoCode and QTYTare is not null order by TareTime desc";
                return (List<TareQtyMD>)sc.Query<TareQtyMD>(sql, new { AutoCode = autoCode });
            }
        }
        /// <summary>
        /// 根据时间获取车辆过磅信息
        /// </summary>        
        /// <returns>车辆过磅信息泛型</returns>
        public List<PoundTotalMD> GetPoundListByDateTime(DateTime TareTime)
        {
            string connection = PublicClass.getConnecion180();
            using (IDbConnection sc = new SqlConnection(connection))
            {
                string sql = @"select * from PoundTotal
                    where IsDelete=0 and IsFinished=1 AND DateDiff(dd,GrossTime,@TareTime)=0 order by No DESC ";
                return (List<PoundTotalMD>)sc.Query<PoundTotalMD>(sql, new { TareTime= TareTime });
            }
        }
        /// <summary>
        /// 获取最近500次车辆过磅信息
        /// </summary>        
        /// <returns>车辆过磅信息泛型</returns>
        public List<PoundTotalMD> GetPoundListByTop500()
        {
            string connection = PublicClass.getConnecion180();
            using (IDbConnection sc = new SqlConnection(connection))
            {
                string sql = @"select top 500 * from PoundTotal
                    where IsDelete=0 and IsFinished=1 order by PrintTime DESC ";
                return (List<PoundTotalMD>)sc.Query<PoundTotalMD>(sql, null);
            }
        }
        /// <summary>
        /// 获取大豆最近500次车辆过磅信息
        /// </summary>        
        /// <returns>车辆过磅信息泛型</returns>
        public List<PoundTotalMD> GetSybeanPoundListByTop500()
        {
            string connection = PublicClass.getConnecion180();
            using (IDbConnection sc = new SqlConnection(connection))
            {
                string sql = @"select top 500 * from PoundTotal
                    where IsDelete=0 and IsFinished=1 and IsSoybean=1 order by PrintTime DESC ";
                return (List<PoundTotalMD>)sc.Query<PoundTotalMD>(sql, null);
            }
        }
        /// <summary>
        /// 获取最近500次车辆过磅信息
        /// </summary>        
        /// <returns>车辆过磅信息泛型</returns>
        public List<PoundTotalMD> GetPoundListByTop200()
        {
            string connection = PublicClass.getConnecion180();
            using (IDbConnection sc = new SqlConnection(connection))
            {
                string sql = @"select top 200 * from PoundTotal
                    where IsDelete=0 and IsFinished=1 order by No DESC ";
                return (List<PoundTotalMD>)sc.Query<PoundTotalMD>(sql, null);
            }
        }
        /// <summary>
        /// 根据车号获取第一次皮重泛型(全部)
        /// </summary>        
        /// <returns>皮重泛型</returns>
        public List<PoundTotalMD> GetPoundListFirst()
        {
            string connection = PublicClass.getConnecion180();
            using (IDbConnection sc = new SqlConnection(connection))
            {
                string sql = @"select * from PoundTotal 
                                where IsDelete=0 and IsFinished=0 ";
                return (List<PoundTotalMD>)sc.Query<PoundTotalMD>(sql, null);
            }
        }
        /// <summary>
        /// 根据车号获取第一次皮重泛型（大豆）
        /// </summary>        
        /// <returns>皮重泛型</returns>
        public List<PoundTotalMD> GetPoundListFirstSoyben()
        {
            string connection = PublicClass.getConnecion180();
            using (IDbConnection sc = new SqlConnection(connection))
            {
                string sql = @"select * from PoundTotal 
                                where IsDelete=0 and IsFinished=0 and IsSoybean=1 order by No DESC ";
                return (List<PoundTotalMD>)sc.Query<PoundTotalMD>(sql, null);
            }
        }
        /// <summary>
        /// 根据车号获取第一次皮重泛型（大豆）
        /// </summary>        
        /// <returns>皮重泛型</returns>
        public List<PoundTotalMD> GetPoundListFirstNoSoyben()
        {
            string connection = PublicClass.getConnecion180();
            using (IDbConnection sc = new SqlConnection(connection))
            {
                string sql = @"select * from PoundTotal 
                                where IsDelete=0 and IsFinished=0 and IsSoybean=0 order by No DESC ";
                return (List<PoundTotalMD>)sc.Query<PoundTotalMD>(sql, null);
            }
        }
        /// <summary>
        /// 获取新的No号
        /// </summary>
        /// <returns>No号</returns>
        public long GetLastNo()
        {
            string connection = PublicClass.getConnecion180();
            using (IDbConnection sc = new SqlConnection(connection))
            {
                string sql = @"select top 1 No from PoundTotal 
                                order by No desc";
                List<PoundTotalMD> ld = (List<PoundTotalMD>)sc.Query<PoundTotalMD>(sql,null);
                long No = ld[0].No;
                return No;
            }
        }
        /// <summary>
        /// 根据车号所有磅重泛型
        /// </summary>
        /// <param name="autoCode">车号</param>
        /// <returns>皮重泛型</returns>
        public List<PoundTotalMD> GetAllPoundList(string autoCode)
        {
            string connection = PublicClass.getConnecion180();
            using (IDbConnection sc = new SqlConnection(connection))
            {
                string sql = @"select * from PoundTotal 
                                where IsDelete=0 and  AutoCode=@AutoCode order by No desc";
                return (List<PoundTotalMD>)sc.Query<PoundTotalMD>(sql, new { AutoCode = autoCode });
            }
        }
        /// <summary>
        /// 根据车号获取第一次皮重泛型(全部)
        /// </summary>
        /// <param name="autoCode">车号</param>
        /// <returns>皮重泛型</returns>
        public List<PoundTotalMD> GetPoundList(string autoCode)
        {
            string connection = PublicClass.getConnecion180();
            using (IDbConnection sc=new SqlConnection(connection))
            {
                string sql = @"select * from PoundTotal 
                                where IsDelete=0 and IsFinished=0 and  AutoCode=@AutoCode";
                return (List<PoundTotalMD>)sc.Query<PoundTotalMD>(sql,new { AutoCode= autoCode });
            }
        }
       
        /// <summary>
        /// 获取是否进厂过磅
        /// </summary>
        /// <param name="AutoCode">车号</param>
        /// <returns>假为未过磅，真为已过皮重</returns>
        public bool JudgeTareTrueOrFalse(string AutoCode)
        {
            string connection = PublicClass.getConnecion180();
            bool Judge;
            using (IDbConnection sc=new SqlConnection(connection))
            {
                sc.Open();
                string sql = @"select COUNT(*) as coun from poundtotal where IsDelete=0 and IsFinished=0 and  AutoCode=@AutoCode";

                SqlCommand sqlCommand = new SqlCommand(sql, (SqlConnection)sc);
                sqlCommand.Parameters.Add(new SqlParameter("@AutoCode", AutoCode));
                var reader= sqlCommand.ExecuteReader();
                int a=0;
                while(reader.Read())
                {
                    a = reader.GetInt32(0);
                }
                
                //var a =int.Parse( sc.Query(sql,new {AutoCode= AutoCode }).ToString());
                if (a==0)
                {
                    Judge = false;
                }
                else
                {
                    Judge = true;
                }
                return Judge;
            }
        }
        /// <summary>
        /// 插入一行地磅数据
        /// </summary>
        /// <param name="pt"></param>
        /// <returns>影响行</returns>
        public int InsertQtyTare(PoundTotalMD pt)
        {
            string connection = PublicClass.getConnecion180();
            using (IDbConnection sc = new SqlConnection(connection))
            {
                string sql = @"INSERT INTO [BohiErp].[dbo].[PoundTotal]
                               ([No]
                               ,[QTYTare]
                               ,[TareTime]
                               ,[QTYNet]
                               ,[MatName]
                               ,[TransportCo]
                               ,[DeliveryCo]
                               ,[Customer]
                               ,[ShipName]
                               ,[ShipNo]
                               ,[TareStfID]
                               ,[AutoCode]
                               ,[IsFinished]
                               ,[PoundTareName]
                               ,[TareStfName],[IsManual],[IsSoybean]
                               ,[Remark])
                         VALUES
                               (@No,@QTYTare
                               ,@TareTime
                               ,@QTYNet
                               ,@MatName
                               ,@TransportCo
                               ,@DeliveryCo
                               ,@Customer
                               ,@ShipName
                               ,@ShipNo
                               ,@TareStfID
                               ,@AutoCode
                               ,@IsFinished
                               ,@PoundTareName
                               ,@TareStfName,@IsManual,@IsSoybean
                               ,@Remark)";
                 int insert= sc.Execute(sql,pt);
                return insert;
            }
        }
        /// <summary>
        /// 更新大豆出厂地磅数据，更新毛重
        /// </summary>
        /// <param name="pt"></param>
        /// <returns>影响行</returns>
        public bool UpdataSobearnGross(PoundTotalMD pt)
        {
            string connection = PublicClass.getConnecion180();
            using (IDbConnection sc = new SqlConnection(connection))
            {
                string sql = @"UPDATE [BohiErp].[dbo].[PoundTotal]
                               SET                                   
                                  [QTYGross] = @QTYGross
                                  ,[GrossTime] = @GrossTime
                                  ,[QTYNet] = @QTYNet 
                                  ,[GrossStfID] = @GrossStfID
                                  ,[GrossStfName] = @GrossStfName
                                  ,[IsFinished] = 1
                                  ,[PoundGrossName] = @PoundGrossName
                                 ,PrintTime=@PrintTime,PrintStfName=@PrintStfName
                             WHERE ID=@ID";
                return  sc.Execute(sql, pt)>0;
               
            }
        }
        /// <summary>
        /// 删除一行地磅数据
        /// </summary>
        /// <param name="pt"></param>
        /// <returns>影响行</returns>
        public int DeletePound(PoundTotalMD pt)
        {
            string connection = PublicClass.getConnecion180();
            using (IDbConnection sc = new SqlConnection(connection))
            {
                string sql = @"UPDATE [BohiErp].[dbo].[PoundTotal]
                               SET [IsDelete] = 1  
                                  ,[DeletStfName] = @DeletStfName      
                                  ,[DeletTime] = (select GETDATE())
                             WHERE 
                             NO=@NO";
                int insert = sc.Execute(sql, pt);
                return insert;
            }
        }
        /// <summary>
        /// 修改一行地磅数据
        /// </summary>
        /// <param name="pt"></param>
        /// <returns>影响行</returns>
        public int UpdataPound(PoundTotalMD pt)
        {
            string connection = PublicClass.getConnecion180();
            using (IDbConnection sc = new SqlConnection(connection))
            {
                string sql = @"UPDATE [BohiErp].[dbo].[PoundTotal]
                               SET 
                                 -- [QTYTare] = @QTYTare
                                 -- ,[QTYGross] = @QTYGross
                                 -- ,[QTYNet] = @QTYNet，
                                   [MatName] = @MatName
                                  ,[TransportCo] = @TransportCo
                                  ,[DeliveryCo] = @DeliveryCo
                                  ,[Customer] = @Customer
                                  ,[ShipName] = @ShipName
                                  ,[ShipNo] = @ShipNo
                                  ,[Remark] = @Remark
                                  ,PrintTime=@PrintTime,PrintStfName=@PrintStfName
                                  ,[ChangeStfName] = @ChangeStfName
                                  ,[ChangeTime] = @ChangeTime      
                             WHERE No=@No";
                int insert = sc.Execute(sql, pt);
                return insert;
            }
        }
        /// <summary>
        /// 更新出厂地磅数据，补齐皮重，毛重
        /// </summary>
        /// <param name="pt"></param>
        /// <returns>影响行</returns>
        public int UpdataTareAndGross(PoundTotalMD pt)
        {
            string connection = PublicClass.getConnecion180();
            using (IDbConnection sc = new SqlConnection(connection))
            {
                string sql = @"UPDATE [BohiErp].[dbo].[PoundTotal]
                               SET 
                                  [QTYTare] = @QTYTare
                                  ,[TareTime] = @TareTime
                                  ,[QTYGross] = @QTYGross
                                  ,[GrossTime] = @GrossTime
                                  ,[QTYNet] = @QTYNet
                                  ,[MatName] = @MatName
                                  ,[TransportCo] = @TransportCo
                                  ,[DeliveryCo] = @DeliveryCo
                                  ,[Customer] = @Customer                                 
                                  ,[Remark] = @Remark
                                 -- ,[MatName] = @MatName
                                 -- ,[ToCutID] = @ToCutID
                                  ,[ShipName] = @ShipName
                                  ,[ShipNo] = @ShipNo
                                  ,[TareStfID] = @TareStfID
                                  ,[GrossStfID] = @GrossStfID
                                  ,[GrossStfName] = @GrossStfName
                                  ,[AutoCode] = @AutoCode
                                  ,[IsFinished] = @IsFinished
                                  ,[PoundTareName] = @PoundTareName
                                  ,[PoundGrossName] = @PoundGrossName
                                    ,[PrintTime] = @PrintTime
                                  ,[PrintStfName] = @PrintStfName
                             WHERE ID=@ID";
                int Updata = sc.Execute(sql, pt);
                return Updata;
            }
        }
        /// <summary>
        /// 更新出厂地磅数据，更新毛重
        /// </summary>
        /// <param name="pt"></param>
        /// <returns>影响行</returns>
        public int UpdataGross(PoundTotalMD pt)
        {
            string connection = PublicClass.getConnecion180();
            using (IDbConnection sc = new SqlConnection(connection))
            {
                string sql = @"UPDATE [BohiErp].[dbo].[PoundTotal]
                               SET                                   
                                  [QTYGross] = @QTYGross
                                  ,[GrossTime] = @GrossTime
                                  ,[QTYNet] = @QTYNet 
                                  ,[GrossStfID] = @GrossStfID
                                  ,[GrossStfName] = @GrossStfName
                                  ,[IsFinished] = 1
                                  ,[PoundGrossName] = @PoundGrossName
                             WHERE ID=@ID";
                int Updata = sc.Execute(sql, pt);
                return Updata;
            }
        }
        /// <summary>
        /// 插入一行车辆皮重及毛重
        /// </summary>
        /// <param name="pt"></param>
        /// <returns>影响行</returns>
        public bool InsertTareAndGross(PoundTotalMD pt)
        {
            string connection = PublicClass.getConnecion180();
            using (IDbConnection sc = new SqlConnection(connection))
            {
              string   sql = @"INSERT INTO [BohiErp].[dbo].[PoundTotal]
                       ([No]        ,[QTYTare]       ,[TareTime]       ,[QTYGross]      ,[GrossTime]
                       ,[QTYNet]    ,[MatName]     ,[TransportCo]          ,[Customer]
                       ,[ShipName]       ,[TareStfID]    ,[GrossStfID]     ,[AutoCode]
                       ,[IsFinished]     ,[PoundTareName]     ,[PoundGrossName]      ,[TareStfName]
                       ,[Remark]     ,[IsSoybean]    ,[GrossStfName] )
                 VALUES
                       (@No      , @QTYTare      , @TareTime     , @QTYGross       , @GrossTime     , @QTYNet
                       , @MatName    , @TransportCo         , @Customer     , @ShipName
                            , @TareStfID     , @GrossStfID      , @AutoCode
                       ,1        , @PoundTareName , @PoundGrossName  , @TareStfName          , @Remark        ,1
                       , @GrossStfName )";
                return sc.Execute(sql,pt)>0;
            }
                
        }
    }
}
