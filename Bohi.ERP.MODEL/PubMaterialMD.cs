using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bohi.ERP.MODEL
{
    public class PubMaterialMD
    {
        public long ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string BilID { get; set; }
        public string Mnemonic { get; set; }
        public long MatTypeID { get; set; }
        public Nullable<int> MatPropID { get; set; }
        public Nullable<long> UnitGrpID { get; set; }
        public Nullable<long> UnitID { get; set; }
        public Nullable<int> GetWayID { get; set; }
        public string Rem { get; set; }
        public Nullable<decimal> RateTax { get; set; }
        public Nullable<long> CreateStfID { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<System.DateTime> ModifyTime { get; set; }
        public Nullable<long> CheckStfID { get; set; }
        public Nullable<System.DateTime> CheckTime { get; set; }
        public Nullable<System.DateTime> EffectTime { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<bool> IsLogMat { get; set; }
        public Nullable<long> ModifyStfID { get; set; }

    }
}
