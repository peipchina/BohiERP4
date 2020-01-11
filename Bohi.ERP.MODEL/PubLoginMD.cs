using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bohi.ERP.MODEL
{
    public class PubLoginMD
    {
        public string LoginName { get; set; }
        public string Name { get; set; }
        public string LastLoginIPAdress { get; set; }
        public int ID { get; set; }
        public bool AddUser { get; set; }        
        public string PassWord { get; set; }
        public Nullable<long> StfNameID { get; set; }
    }
}
