using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Bohi.ERP.MODEL;
using Dapper;

namespace Bohi.ERP.DAL
{
    public class ReachArriveService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ReachArriveMD> getAllReachArrive(string AutoCode)
        {
            try
            {
                string connection = PublicClass.getConnecion();
                using (IDbConnection sc = new SqlConnection(connection))
                {
                    //string sql = @"select pm.Name MatName,drt.MatID,drt.QtyTare,drt.QtyGross,drt.StockID,drt.AutoCode,drt.ToCust,ps.IsNotLocal,drt.ContID,drt.ContBilID,sc.CustID from DelReachDtl drt
                    //        left join DelReach dr on dr.ID=drt.ID
                    //        left join PubStock ps on ps.ID=drt.StockID
                    //        left join PubMaterial pm on pm.ID=drt.MatID
                    //        left join SacContract sc on sc.ID=drt.ContID
                    //        where dr.Status in(3,4) and (ps.IsNotLocal=0 or ps.IsNotLocal is null) and 
                    //        (drt.QtyTare is null or drt.QtyGross is null) and dr.CorpID=1190056  and drt.AutoCode=@AutoCode ";
                    string sql = @"select *, ID=row_Number() over(order by BilNo) from View_BohiInfoByAutoCode Where AutoCode=@AutoCode";//view_bohiInfoByAutoCode为视图
                    return (List<ReachArriveMD>)sc.Query<ReachArriveMD>(sql, new { AutoCode = AutoCode });
                }
            }
            catch (Exception)
            {
                return null;
               // throw;
            }
        }
    }
}
