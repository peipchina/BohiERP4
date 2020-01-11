using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bohi.ERP.MODEL
{
    public class PubBlackAutoCodeMD
    {
        public int ID { get; set; }
        public string AutoCode { get; set; }
        public string Driver { get; set; }
        public System.DateTime BlackTime { get; set; }
        public string reason { get; set; }
        public string CheckName { get; set; }
        public string Results { get; set; }
        public string CreatName { get; set; }
    }
}
