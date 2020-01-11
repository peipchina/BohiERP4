using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UpModel
{
    public class File
    {
        [XmlAttribute("Ver")]
        public string Ver { get; set; }
        [XmlAttribute("Name")]
        public string Name { get; set; }
    }
}
