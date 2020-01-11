using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bohi.ERP.MODEL
{
    public class ReachArriveMD
    {
        public int ID { get; set; }
        public string MatName { get; set; }
        public Nullable<long> MatID { get; set; }
        public Nullable<long> CustID { get; set; }
        public Nullable<decimal> QtyTare { get; set; }
        public Nullable<decimal> QtyGross { get; set; }
        public Nullable<long> StockID { get; set; }
        public string AutoCode { get; set; }
        public string ToCust { get; set; }
        public string SuppName { get; set; }
        public string BilNo { get; set; }
        public Nullable<bool> IsNotLocal { get; set; }        
    }
}
