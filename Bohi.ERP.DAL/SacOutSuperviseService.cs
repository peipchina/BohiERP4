using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Bohi.ERP.MODEL;

namespace Bohi.ERP.DAL
{
    public class SacOutSuperviseService
    {
        #region ---------------根据车号获取车辆在厂信息---------------
        /// <summary>
        /// 根据车号获取车辆在厂信息
        /// </summary>
        /// <param name="AutoCode"></param>
        /// <returns>未完成监装车辆</returns>
        public List<SacOutSuperviseMD> GetSacList(string AutoCode)
        {
            try
            {
                string connection = PublicClass.getConnecion();
                using (IDbConnection sc = new SqlConnection(connection))
                {
                    string sql = @"select dr.matid,pm.Name matName,sc.ID,SC.AutoCode,SC.QtyTare,SC.TareTime,SC.TareStfID,SC.TareStfID,sct.CustID
                            ,SC.QtyGross,SC.GrossTime,SC.GrossStfID,SC.PassTime,SC.PassStfID,SC.SuperviseIndex,SC.MultKeyID from SacOutSupervise sc
                            left join delreachdtl dr on dr.id=sc.idfrom
                            left join PubMaterial pm on pm.ID=dr.MatID
                            left join SacContract sct on sct.ID=dr.ContID
                            WHERE SC.AutoCode=@AutoCode and sc.CorpID=1190056 and sc.StockID is not null and ( TareTime is null or GrossTime is null) and  sc.BilIDFrom='DERC'";
                    return (List<SacOutSuperviseMD>)sc.Query<SacOutSuperviseMD>(sql, new { AutoCode = AutoCode });
                }
            }
            catch (Exception)
            {
                return null;
                //throw;
            }
        } 
        #endregion
    }
}
