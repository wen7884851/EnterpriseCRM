using System;
using System.Text;
using System.Security.Cryptography;

namespace Framework.Common.SecurityHelper
{
    /// <summary>
    /// MD5Provider 的摘要说明
    /// </summary>
    public class MD5Provider
    {
        private MD5Provider()
        {
        }
        /// <summary>
        /// 计算指定字符串的MD5哈希值
        /// </summary>
        /// <param name="message">要进行哈希计算的字符串</param>
        /// <returns></returns>
        public static string Hash(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return string.Empty;
            }
            else
            {
                MD5 md5 = MD5.Create();
                byte[] source = Encoding.UTF8.GetBytes(message);
                byte[] result = md5.ComputeHash(source);
                StringBuilder buffer = new StringBuilder(result.Length);

                for (int i = 0; i < result.Length; i++)
                {
                    buffer.Append(result[i].ToString("x"));//将byte值转换成十六进制字符串
                }
                return buffer.ToString() ;
            }

        }


        /// <summary>
        /// 返回源字符串的16或32位MD5加密结果
        /// </summary>
        public static string GetMD5String(string Source, int Digit)
        {
            if (string.IsNullOrEmpty(Source))
                Source = string.Empty;
            if (Digit == 16) //16位MD5加密（取32位加密的9~25字符） 
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(Source, "MD5").Substring(8, 16);
            }

            if (Digit == 32) //32位加密 
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(Source, "MD5");
            }
            return "00000000000000000000000000000000";
        }

        public static string GetMD5String(string Source)
        {
            return GetMD5String(Source, 32);
        }
    }
}