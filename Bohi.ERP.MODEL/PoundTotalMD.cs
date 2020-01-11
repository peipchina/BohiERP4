using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bohi.ERP.MODEL
{
    public class PoundTotalMD
    {
        public string CarNo { get; set; }
        public long ID { get; set; }
        public long No { get; set; }
        public Nullable<decimal> QTYTare { get; set; }
        public Nullable<System.DateTime> TareTime { get; set; }
        public Nullable<decimal> QTYGross { get; set; }
        public Nullable<System.DateTime> GrossTime { get; set; }
        public Nullable<decimal> QTYNet { get; set; }
        public string MatName { get; set; }
        public string TransportCo { get; set; }
        public string DeliveryCo { get; set; }
        public string Customer { get; set; }
        public string ShipName { get; set; }
        public Nullable<int> ShipNo { get; set; }
        public Nullable<int> TareStfID { get; set; }
        public Nullable<int> GrossStfID { get; set; }
        public string AutoCode { get; set; }
        public bool IsFinished { get; set; }
        public string PoundTareName { get; set; }
        public string PoundGrossName { get; set; }
        public string TareStfName { get; set; }
        public string Remark { get; set; }
        public Nullable<bool> IsPrint { get; set; }
        public bool IsManual { get; set; }
        public bool IsSoybean { get; set; }
        public bool IsDelete { get; set; }
        public string GrossStfName { get; set; }
        public string ChangeStfName { get; set; }
        public string DeletStfName { get; set; }
        public Nullable<System.DateTime> ChangeTime { get; set; }
        public Nullable<System.DateTime> DeletTime { get; set; }
        public Nullable<System.DateTime> PrintTime { get; set; }
        public string PrintStfName { get; set; }
    }
}
