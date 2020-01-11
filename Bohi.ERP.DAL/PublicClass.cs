using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Configuration;

namespace Bohi.ERP.DAL
{
    public class PublicClass
    {
        #region 获取链接字符串
        /// <summary>
        /// 获取链接字符串
        /// </summary>
        /// <returns>数据库链接字符串</returns>
        public static string getConnecion()
        {
            string connection = ConfigurationManager.ConnectionStrings["connection"].ToString();
            return AESDecypt(connection);
        }
        #endregion

        #region 获取链接字符串
        /// <summary>
        /// 获取链接字符串
        /// </summary>
        /// <returns>数据库链接字符串</returns>
        public static string getConnecion180()
        {
            string connection = ConfigurationManager.ConnectionStrings["connection180"].ToString();
            return AESDecypt(connection);
        }
        #endregion


        private static readonly string _AESKey = "[bh/*Yiikyj..R;]";

        #region AEC解密
        /// <summary>
        /// AEC解密
        /// </summary>
        /// <param name="enc"></param>
        /// <param name="aesKey"></param>
        /// <returns></returns>
        public static string AESDecypt(string enc, string aesKey = null)
        {
            if (string.IsNullOrEmpty(aesKey)) aesKey = _AESKey;
            byte[] keyArray = Encoding.UTF8.GetBytes(aesKey);
            byte[] toEncryptArray = Convert.FromBase64String(enc);

            RijndaelManaged rm = new RijndaelManaged();
            rm.Key = keyArray;
            rm.Mode = CipherMode.ECB;
            rm.Padding = PaddingMode.PKCS7;

            ICryptoTransform ct = rm.CreateDecryptor();
            byte[] resultArray = ct.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Encoding.UTF8.GetString(resultArray);

        }
        #endregion

        #region MD5加密
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="inCode"></param>
        /// <returns></returns>
        public string MD5Dec(string inCode)
        {
            MD5 mdd = System.Security.Cryptography.MD5.Create();
            byte[] md5Bytes = mdd.ComputeHash(Encoding.Default.GetBytes(inCode + "acb@"));
            string outCode = BitConverter.ToString(md5Bytes).Replace("-", "");
            return outCode;
        }
        #endregion
    }
}
