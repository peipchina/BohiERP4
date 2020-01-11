using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bohi.ERP.MODEL
{
    public class SacOutSuperviseMD
    {
        public Nullable<long> MatID { get; set; }
        public string matName { get; set; }
        public long ID { get; set; }
        public string AutoCode { get; set; }
        public Nullable<decimal> QtyTare { get; set; }
        public Nullable<System.DateTime> TareTime { get; set; }
        public Nullable<long> TareStfID { get; set; }
        public Nullable<long> Expr1 { get; set; }
        public Nullable<long> CustID { get; set; }
        public Nullable<decimal> QtyGross { get; set; }
        public Nullable<System.DateTime> GrossTime { get; set; }
        public Nullable<long> GrossStfID { get; set; }
        public Nullable<System.DateTime> PassTime { get; set; }
        public Nullable<long> PassStfID { get; set; }
        public Nullable<int> SuperviseIndex { get; set; }
        public Nullable<long> MultKeyID { get; set; }
    }
}
