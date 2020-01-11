using Bohi.ERP.DAL;
using Bohi.ERP.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bohi.ERP.BLL
{
    public class PoundTotalManager
    {
        /// <summary>
        /// 根据车号获取车辆皮重
        /// </summary>
        /// <param name="autoCode">车号</param>
        /// <returns>皮重泛型</returns>
        public List<TareQtyMD> GetAllTareQty(string autoCode)
        {
            PoundTotalService pts = new PoundTotalService();
            return pts.GetAllTareQty(autoCode);
        }

            /// <summary>
            /// 获取某段时间车辆过磅信息
            /// </summary>        
            /// <returns>车辆过磅信息泛型</returns>
            public List<PoundTotalMD> GetPoundListByDateTime(DateTime StartTime, DateTime EndTime)
        {
            PoundTotalService pts = new PoundTotalService();
            return pts.GetPoundListByDateTime(StartTime, EndTime);
        }
        /// <summary>
        ///根据车号获取车辆过磅信息
        /// </summary>        
        /// <returns>车辆过磅信息泛型</returns>
        public List<PoundTotalMD> GetPoundListByAutoCode(string autoCode)
        {
            PoundTotalService pts = new PoundTotalService();
            return pts.GetPoundListByAutoCode(autoCode);
        }
        /// <summary>
        /// 根据时间获取车辆过磅信息
        /// </summary>        
        /// <returns>车辆过磅信息泛型</returns>
        public List<PoundTotalMD> GetPoundListByDateTime(DateTime TareTime)
        {
            PoundTotalService pts = new PoundTotalService();
            return pts.GetPoundListByDateTime(TareTime);
        }
        /// <summary>
        /// 获取最近500次车辆过磅信息
        /// </summary>        
        /// <returns>车辆过磅信息泛型</returns>
        public List<PoundTotalMD> GetPoundListByTop500()
        {
            PoundTotalService pts = new PoundTotalService();
            return pts.GetPoundListByTop500();
        }
        /// <summary>
        /// 获取最近500次车辆过磅信息
        /// </summary>        
        /// <returns>车辆过磅信息泛型</returns>
        public List<PoundTotalMD> GetSybeanPoundListByTop500()
        {
            PoundTotalService pts = new PoundTotalService();
            return pts.GetSybeanPoundListByTop500();
        }
        /// <summary>
        /// 获取最近200次车辆过磅信息
        /// </summary>        
        /// <returns>车辆过磅信息泛型</returns>
        public List<PoundTotalMD> GetPoundListByTop200()
        {
            PoundTotalService pts = new PoundTotalService();
            return pts.GetPoundListByTop200();
        }
        /// <summary>
        /// 根据车号获取第一次皮重泛型
        /// </summary>        
        /// <returns>皮重泛型</returns>
        public List<PoundTotalMD> GetPoundListFirst()
        {
            PoundTotalService pts = new PoundTotalService();
            return pts.GetPoundListFirst();
        }
        /// <summary>
        /// 根据车号获取第一次皮重泛型(大豆)
        /// </summary>        
        /// <returns>皮重泛型</returns>
        public List<PoundTotalMD> GetPoundListFirstSoybean()
        {
            PoundTotalService pts = new PoundTotalService();
            return pts.GetPoundListFirstSoyben();
        }
        /// <summary>
        /// 根据车号获取第一次皮重泛型(非大豆)
        /// </summary>        
        /// <returns>皮重泛型</returns>
        public List<PoundTotalMD> GetPoundListFirstNoSoybean()
        {
            PoundTotalService pts = new PoundTotalService();
            return pts.GetPoundListFirstNoSoyben();
        }
        /// <summary>
        /// 根据车号获取所有磅重泛型
        /// </summary>
        /// <param name="autoCode">车号</param>
        /// <returns>重量泛型</returns>
        public List<PoundTotalMD> GetAllPoundList(string autoCode)
        {
            PoundTotalService pts = new PoundTotalService();
            return pts.GetAllPoundList(autoCode);
        }
        /// <summary>
        /// 获取最后一个No
        /// </summary>
        /// <returns>No</returns>
        public long GetLastNo()
        {
            long newNo;
            PoundTotalService ps = new PoundTotalService();
            string Date = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2,'0') + DateTime.Now.Day.ToString().PadLeft(2,'0');
            string No = ps.GetLastNo().ToString();
            if (Date==No.Substring(0,8))
            {
                newNo = ps.GetLastNo() + 1;
            }
            else
            {
                newNo = long.Parse(Date + "0001");
            }
            return newNo;
        }
        /// <summary>
        /// 删除一行地磅数据
        /// </summary>
        /// <param name="pt"></param>
        /// <returns>影响行</returns>
        public bool DeletePound(PoundTotalMD pt)
        {
            PoundTotalService pts = new PoundTotalService();
            return pts.DeletePound(pt)>0;
        }
        /// <summary>
        /// 修改一行地磅数据
        /// </summary>
        /// <param name="pt"></param>
        /// <returns>影响行</returns>
        public bool UpdataPound(PoundTotalMD pt)
        {
            PoundTotalService pts = new PoundTotalService();
            return pts.UpdataPound(pt) > 0;
        }
        /// <summary>
        /// 插入一行大豆数据
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public bool InsertSobean(PoundTotalMD pt)
        {
            pt.No = GetLastNo();
            PoundTotalService pts = new PoundTotalService();
            return pts.InsertQtyTare(pt)>0;
        }
        /// <summary>
        /// 更新大豆出厂地磅数据，更新毛重
        /// </summary>
        /// <param name="pt"></param>
        /// <returns>影响行</returns>
        public bool UpdataSobearnGross(PoundTotalMD pt)
        {            
            PoundTotalService pts = new PoundTotalService();
            return pts.UpdataSobearnGross(pt);
        }
        /// <summary>
        /// 插入或更新一行磅重信息
        /// </summary>
        /// <param name="AutoCode">车号</param>
        /// <param name="PoundTare">皮重</param>
        /// <param name="poundName">磅名称</param>
        /// <param name="MatID">物料ID</param>
        /// <param name="CustID">客户ID</param>
        /// <returns></returns>
        public int Judge(string AutoCode, decimal PoundTare,String poundName,string matName,string customer, string ShipName,string ShipNo,int TareStfID,
            string TareStfName,string Remark,string TransportCo,string DeliveryCo,object tag,bool IsSoybean)
        {
            int de = 0;
            PoundTotalService pts = new PoundTotalService();
            bool Judge= pts.JudgeTareTrueOrFalse(AutoCode);
            if (Judge==false)//第一次插入数据
            {
               
                PoundTotalMD pt = new PoundTotalMD();                
                pt.QTYTare = PoundTare;
                pt.AutoCode = AutoCode;
                pt.MatName = matName;
                pt.TareTime = DateTime.Now;
                pt.PoundTareName = poundName;
                pt.No = GetLastNo();
                pt.Customer = customer;
                pt.TareStfID = TareStfID;
                pt.Remark = Remark;
                pt.IsSoybean = IsSoybean;
                pt.DeliveryCo = DeliveryCo;
                pt.TransportCo = TransportCo;
                pt.TareStfName = TareStfName;
                pt.ShipName = ShipName;
               
                if (tag==null)
                {
                    pt.IsManual = false;
                }
                else
                {
                    pt.IsManual = true;
                }
                if (ShipNo!=string.Empty)
                {
                    pt.ShipNo = int.Parse(ShipNo);
                }
                else
                {
                    pt.ShipNo = null;
                }                
                de= pts.InsertQtyTare(pt)>0?1:0;                  
            }
            else
            {
                List<PoundTotalMD> lp= pts.GetPoundList(AutoCode);
                List<PoundTotalMD> nlp = new List<PoundTotalMD>();
                if (PoundTare>=lp[0].QTYTare)//第二次磅重比第一次大，即为毛重
                {
                    lp[0].QTYGross = PoundTare;
                    lp[0].GrossTime = DateTime.Now;
                    lp[0].GrossStfID = TareStfID;
                    lp[0].MatName = matName;
                    lp[0].TransportCo = TransportCo;
                    lp[0].DeliveryCo = DeliveryCo;
                    lp[0].Customer = customer;
                    lp[0].Remark = Remark;
                    lp[0].QTYNet =PoundTare - lp[0].QTYTare;
                    lp[0].PoundGrossName = poundName;
                    lp[0].GrossStfName = TareStfName;
                    lp[0].IsFinished = true;
                    lp[0].PrintStfName = TareStfName;
                    lp[0].PrintTime = DateTime.Now;
                    if (tag != null)
                    {
                        lp[0].IsManual = true;
                    } 
                    de = pts.UpdataTareAndGross(lp[0])>0?2:0;
                }
                else//第二次磅重比第一次小，即为皮重
                {
                    lp[0].QTYGross = lp[0].QTYTare;
                    lp[0].GrossTime = lp[0].TareTime;
                    lp[0].GrossStfName = lp[0].TareStfName;
                    lp[0].GrossStfID = lp[0].TareStfID;
                    lp[0].TareStfID = TareStfID;
                    lp[0].MatName = matName;
                    lp[0].TransportCo = TransportCo;
                    lp[0].DeliveryCo = DeliveryCo;
                    lp[0].Customer = customer;
                    lp[0].Remark = Remark;
                    lp[0].PoundGrossName = lp[0].PoundTareName;
                    lp[0].QTYTare = PoundTare;
                    lp[0].TareStfName = TareStfName;
                    lp[0].TareTime = DateTime.Now;
                    lp[0].PoundTareName = poundName;                    
                    lp[0].QTYNet = lp[0].QTYGross - PoundTare;
                    lp[0].IsFinished = true;
                    lp[0].PrintStfName = TareStfName;
                    lp[0].PrintTime = DateTime.Now;
                    if (tag != null)
                    {
                        lp[0].IsManual = true;
                    }                    
                    de =pts.UpdataTareAndGross(lp[0])>0?3:0;
                }
            }
            return de;
        }
        /// <summary>
        /// 插入一行地磅数据
        /// </summary>
        /// <param name="pt"></param>
        /// <returns>影响行</returns>
        public bool InsertQtyTareSoybean(PoundTotalMD pt)
        {    
            PoundTotalService pts = new PoundTotalService();
            return pts.InsertQtyTare(pt)>0;
        }
        /// <summary>
        /// 根据车号获取第一次皮重泛型
        /// </summary>
        /// <param name="autoCode">车号</param>
        /// <returns>皮重泛型</returns>
        public List<PoundTotalMD> GetPoundList(string autoCode)
        {
            PoundTotalService pts = new PoundTotalService();
            return pts.GetPoundList(autoCode);
        }
    }
}
