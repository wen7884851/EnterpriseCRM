//**********************************
//*ClassName:
//*Version:
//*Date:
//*Author:
//*Effect:
//**********************************

using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace Core.Common.ToolsHelper
{
    public class RSACommunicateServer
    {
        RSACryptoServiceProvider _selfRsa;//= new RSACryptoServiceProvider(1024 * 2);
        RSACryptoServiceProvider _friendRsa;
        public RSACommunicateServer()
        {
            CspParameters RSAParams = new CspParameters();
            RSAParams.Flags = CspProviderFlags.NoFlags;
            //RSACryptoServiceProvider sp = new RSACryptoServiceProvider(1024, RSAParams);
            _selfRsa = new RSACryptoServiceProvider(1024 * 2, RSAParams);// new RSACryptoServiceProvider(1024 * 2);
        }
        /// <summary>
        /// 创建密钥对
        /// </summary>
        public void CreateKeyPair()
        {
            CspParameters RSAParams = new CspParameters();
            RSAParams.Flags = CspProviderFlags.NoFlags;
            _selfRsa = new RSACryptoServiceProvider(1024 * 2, RSAParams);// new RSACryptoServiceProvider(1024 * 2);
        }
        /// <summary>
        /// 获得公钥
        /// </summary>
        /// <returns></returns>
        public string PublicKeyXML
        {
            get
            {
                return _selfRsa.ToXmlString(false);
            }
        }
        /// <summary>
        /// 获得或设置公钥和私钥
        /// </summary>
        /// <returns></returns>
        public string KeyPairXML
        {
            get
            {
                return _selfRsa.ToXmlString(true);
            }
            set
            {
                _selfRsa.FromXmlString(value);
            }
        }
        /// <summary>
        /// 设置通讯对方的公钥
        /// </summary>
        /// <param name="xmlPubKey"></param>
        public void CreateFriendRsa(string xmlPubKey)
        {
            CspParameters RSAParams = new CspParameters();
            RSAParams.Flags = CspProviderFlags.NoFlags;
            _friendRsa = new RSACryptoServiceProvider(1024 * 2, RSAParams);
            _friendRsa.FromXmlString(xmlPubKey);
        }
        /// <summary>
        /// 生成数据摘要
        /// </summary>
        /// <param name="arr">用自己私钥要生成摘要的数据</param>
        /// <returns>摘要信息</returns>
        public byte[] SignData(byte[] arr)
        {
            return _selfRsa.SignData(arr, "MD5");
        }

        /// <summary>
        /// 用对方公钥验证摘要是否正确
        /// </summary>
        /// <param name="data"></param>
        /// <param name="brief"></param>
        /// <returns></returns>
        public bool VerifyData(byte[] data, byte[] brief)
        {
            if (_friendRsa == null)
                throw new Exception("没有初始化通讯对方的公钥");
            return _friendRsa.VerifyData(data, "MD5", brief);
        }
        /// <summary>
        /// 使用对方的公钥进行加密
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public byte[] EncryptData(byte[] data)
        {
            if (_friendRsa == null)
                throw new Exception("没有初始化通讯对方的公钥");

            return _friendRsa.Encrypt(data, false);
        }

        public string EncryptDataToASCStr(byte[] data)
        {
            if (_friendRsa == null)
                throw new Exception("没有初始化通讯对方的公钥");
            byte[] arr = _friendRsa.Encrypt(data, false);
            return Convert.ToBase64String(arr);
        }
        /// <summary>
        /// 使用自己的私钥进行解密
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public byte[] DecryptData(byte[] data)
        {
            return _selfRsa.Decrypt(data, false);
        }

        public string DecryptDataToASCStr(byte[] data)
        {
            byte[] arr = _selfRsa.Decrypt(data, false);
            return Convert.ToBase64String(arr);
        }
    }
}
