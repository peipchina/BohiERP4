using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bohi.ERP.MODEL
{
    public class RebackCheckMD
    {
        public long ID { get; set; }
        public string AutoCode { get; set; }
        public Nullable<System.DateTime> TareTime { get; set; }
        public Nullable<decimal> QtyTare { get; set; }
        public Nullable<decimal> LestQty { get; set; }
        public Nullable<System.DateTime> RebackTime { get; set; }
        public string MatName { get; set; }
        public string RebackStf { get; set; }
        public Nullable<bool> IsFinished { get; set; }
    }
}
