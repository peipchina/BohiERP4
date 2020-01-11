using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using UpModel;
using System.Net;

namespace UpConfig
{
    public class UpDateConfig
    {
        public Updater Updater { get; set; }
        public List<UpModel.File> files { get; set; }

        #region XML文件对象化
        /// <summary>
        /// XML文件对象化
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public UpDateConfig getUpConfit(string fileName)
        {
            XmlSerializer xs = new XmlSerializer(typeof(UpDateConfig));
            StreamReader sr = new StreamReader(fileName);
            UpDateConfig config = (UpDateConfig)xs.Deserialize(sr);
            sr.Close();
            return config;
        }
        #endregion

        private UpDateConfig getUpconfig(string xmlFile)
        {
            XmlSerializer xs = new XmlSerializer(typeof(UpDateConfig));
            StringReader sr = new StringReader(xmlFile);
            UpDateConfig up = xs.Deserialize(sr) as UpDateConfig;
            sr.Dispose();
            return up;
        }

        #region
        public bool judgeUpdate()
        {

            bool a;
            string path = Environment.CurrentDirectory + "\\UpDateConfig.config";
            UpDateConfig ud = new UpDateConfig();
            string oldVerson = ud.getUpConfit(path).Updater.Verson;
            string url = ud.getUpConfit(path).Updater.Url + "UpDateConfig.config";
            WebClient wc = new WebClient();
            //if (Directory.Exists(Environment.CurrentDirectory + "\\tempconfig") != true)
            //{
            //    Directory.CreateDirectory(Environment.CurrentDirectory + "\\tempconfig");
            //}
            //else
            //{
            //    Directory.Delete(Environment.CurrentDirectory + "\\tempconfig", true);
            //    Directory.CreateDirectory(Environment.CurrentDirectory + "\\tempconfig");
            //}
            //wc.DownloadFile(url, Environment.CurrentDirectory + "\\tempconfig" + "\\UpDateConfig.config");
            string xml = wc.DownloadString(url);
            wc.Dispose();
            string newVerson = ud.getUpconfig(xml).Updater.Verson;
            Version ov = new Version(oldVerson);
            Version nv = new Version(newVerson);
            if (nv > ov)
            {
                a = true;
            }
            else
            {
                a = false;
            }
            return a;


        }
        #endregion
    }
}
