using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bohi.ERP.MODEL
{
    public class SoybeanAutoCodeMD
    {
        public long ID { get; set; }
        public string BilID { get; set; }
        public int Item { get; set; }
        public string  CarNo { get; set; }
        public Nullable<long> CorpID { get; set; }
        public string BilNo { get; set; }
        public Nullable<long> MatID { get; set; }
        public string MatName { get; set; }
        public Nullable<decimal> Qty { get; set; }
        public Nullable<decimal> QtyTo { get; set; }
        public Nullable<decimal> QtyUnRece { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<long> IDInStc { get; set; }
        public Nullable<long> PIPID { get; set; }
        public Nullable<long> AutoID { get; set; }
        public string AutoCode { get; set; }
        public string Driver { get; set; }
        public Nullable<decimal> QtyTare { get; set; }
        public Nullable<decimal> QtyGross { get; set; }
        public Nullable<System.DateTime> TareTime { get; set; }
        public Nullable<System.DateTime> GrossTime { get; set; }
        public Nullable<System.DateTime> SealsTime { get; set; }
        public Nullable<System.DateTime> UnLoadTime { get; set; }
        public string ShipName { get; set; }
        public Nullable<long> TareStfID { get; set; }
        public string TareName { get; set; }
        public string TarePoundName { get; set; }
        public string TareStfName { get; set; }

    }
}
