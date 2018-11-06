using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Core.Common.SecurityHelper
{
    /// <summary>
    /// DES算法加密解密
    /// </summary>
    public class DESProvider
    {
        private DESProvider()
        {
        }
        //默认的初始化密钥
        private static string key = "netskycn";

        /// <summary>
        /// 对称加密解密的密钥
        /// </summary>
        public static string Key
        {
            get
            {
                return key;
            }
            set
            {
                key = value;
            }
        }
        #region 加密
        /// <summary>
        /// 采用DES算法对字符串加密
        /// </summary>
        /// <param name="encryptString">要加密的字符串</param>
        /// <param name="key">加密的密钥</param>
        /// <returns></returns>
        public static string EncryptString(string encryptString, string key)
        {
            //加密加密字符串是否为空
            if (string.IsNullOrEmpty(encryptString))
            {
                throw new ArgumentNullException("encryptString", "不能为空");
            }
            //加查密钥是否为空
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key", "不能为空");
            }
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(encryptString);
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="encryptString">要加密的字符串</param>
        /// <returns></returns>
        public static string EncryptString(string encryptString)
        {
            return EncryptString(encryptString, key);
        }
        /// <summary>
        /// 采用DES算法对字节数组加密
        /// </summary>
        /// <param name="sourceBytes">要加密的字节数组</param>
        /// <param name="keyBytes">算法的密钥，长度为8的倍数，最大长度64</param>
        /// <param name="keyIV">算法的初始化向量，长度为8的倍数，最大长度64</param>
        /// <returns></returns>
        public static byte[] EncryptBytes(byte[] sourceBytes, byte[] keyBytes, byte[] keyIV)
        {
            if (sourceBytes == null || keyBytes == null || keyIV == null)
            {
                throw new ArgumentNullException("sourceBytes和keyBytes", "不能为空。");
            }
            else
            {
                //检查密钥数组长度是否是8的倍数并且长度是否小于64
                keyBytes = CheckByteArrayLength(keyBytes);
                //检查初始化向量数组长度是否是8的倍数并且长度是否小于64
                keyIV = CheckByteArrayLength(keyIV);
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                //实例化内存流MemoryStream
                MemoryStream mStream = new MemoryStream();
                //实例化CryptoStream
                CryptoStream cStream = new CryptoStream(mStream, provider.CreateEncryptor(keyBytes, keyIV), CryptoStreamMode.Write);
                cStream.Write(sourceBytes, 0, sourceBytes.Length);
                cStream.FlushFinalBlock();
                //将内存流转换成字节数组
                byte[] buffer = mStream.ToArray();
                mStream.Close();//关闭流
                cStream.Close();//关闭流
                return buffer;
            }
        }
        #endregion
        #region 解密
        public static string DecryptString(string decryptString, string key)
        {
            if (string.IsNullOrEmpty(decryptString))
            {
                return "";
            }
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key", "不能为空");
            }
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            int len;
            len = decryptString.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(decryptString.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="decryptString">要解密的字符串</param>
        /// <returns></returns>
        public static string DecryptString(string decryptString)
        {
            return DecryptString(decryptString, key);
        }

        /// <summary>
        /// 采用DES算法对字节数组解密
        /// </summary>
        /// <param name="sourceBytes">要加密的字节数组</param>
        /// <param name="keyBytes">算法的密钥，长度为8的倍数，最大长度64</param>
        /// <param name="keyIV">算法的初始化向量，长度为8的倍数，最大长度64</param>
        /// <returns></returns>
        public static byte[] DecryptBytes(byte[] sourceBytes, byte[] keyBytes, byte[] keyIV)
        {
            if (sourceBytes == null || keyBytes == null || keyIV == null)
            {
                throw new ArgumentNullException("soureBytes和keyBytes及keyIV", "不能为空。");
            }
            else
            {
                //检查密钥数组长度是否是8的倍数并且长度是否小于64
                keyBytes = CheckByteArrayLength(keyBytes);
                //检查初始化向量数组长度是否是8的倍数并且长度是否小于64
                keyIV = CheckByteArrayLength(keyIV);
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, provider.CreateDecryptor(keyBytes, keyIV), CryptoStreamMode.Write);
                cStream.Write(sourceBytes, 0, sourceBytes.Length);
                cStream.FlushFinalBlock();
                //将内存流转换成字节数组
                byte[] buffer = mStream.ToArray();
                mStream.Close();//关闭流
                cStream.Close();//关闭流
                return buffer;
            }
        }
        #endregion
        /// <summary>
        /// 检查密钥或初始化向量的长度，如果不是8的倍数或长度大于64则截取前8个元素
        /// </summary>
        /// <param name="byteArray">要检查的数组</param>
        /// <returns></returns>
        private static byte[] CheckByteArrayLength(byte[] byteArray)
        {
            byte[] resultBytes = new byte[8];
            //如果数组长度小于8
            if (byteArray.Length < 8)
            {
                return Encoding.UTF8.GetBytes("12345678");
            }
            //如果数组长度不是8的倍数
            else if (byteArray.Length % 8 != 0 || byteArray.Length > 64)
            {
                Array.Copy(byteArray, 0, resultBytes, 0, 8);
                return resultBytes;
            }
            else
            {
                return byteArray;
            }
        }
    }
}