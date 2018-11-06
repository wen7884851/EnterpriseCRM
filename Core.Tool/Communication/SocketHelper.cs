using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace Core.Tool
{
    /// <summary>
    /// socket通讯类
    /// </summary>
    public class SocketHelper
    {
        /// <summary>
        /// Socket调用
        /// </summary>
        /// <param name="requestStr">发送字符串</param>
        /// <param name="PrefixLen">表示数据包长度的字节数</param>
        /// <param name="socketIP">IP地址</param>
        /// <param name="socketPort">端口</param>
        /// <param name="encode">编码规则</param>
        /// <param name="timeOut">超时时间</param>
        /// <param name="bufSize">接收数据长度</param>
        /// <param name="onlySend">是否等待接收数据</param>
        /// <returns></returns>
        public static String Call(String requestStr, int PrefixLen, string socketIP, int socketPort,
            string encode, int timeOut, int bufSize, bool onlySend = false)
        {
            string response = String.Empty;
            Socket socket = null;
            try
            {
                byte[] sendBuffer = Encode(requestStr, PrefixLen, encode) as byte[];
                socket = GetConnect(socketIP, socketPort, timeOut);

                if (socket != null)
                {
                    socket.Send(sendBuffer);
                    if (!onlySend)
                        response = Decode(socket, PrefixLen, encode, bufSize) as string;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (socket != null)
                {
                    Close(socket);
                }
            }
            return response;
        }

        /// <summary>
        /// 发送数据并接收返回数据包
        /// </summary>
        /// <param name="sendBytes">发送数据</param>
        /// <param name="socketIP">服务器IP</param>
        /// <param name="socketPort">服务器端口</param>
        /// <param name="timeOut">超时时间</param>
        /// <param name="encode">数据长度编码规则</param>
        /// <param name="RevPrefixLen">表示获取数据包长度的字节数,当为0时按实际数据长度接收</param>
        /// <param name="onlySend">是否不接收数据 默认为false(接收)</param>
        /// <returns></returns>
        public static byte[] ReceiveByte(byte[] sendBuffer, string socketIP, int socketPort, int timeOut, string encode,
            int RevPrefixLen, bool onlySend = false)
        {
            Socket socket = null;
            byte[] RevBuffer = null;
            try
            {
                socket = GetConnect(socketIP, socketPort, timeOut);

                if (socket != null)
                {
                    try
                    {
                        socket.Send(sendBuffer);
                    }
                    catch (Exception e)
                    {
                        throw new Exception("发送数据异常!(" + e.Message + ")");
                    }
                    try
                    {
                        if (!onlySend)//接收数据
                        {
                            if (RevPrefixLen == 0)//按传入的接收数据长度接收
                            {
                                List<byte> recvBufferList = new List<byte>();//接收的数据集合
                                byte[] RecvBytes = new byte[1024 * 8];//定义接收缓冲区大小为8KB
                                int iBytes = 1;
                                while (iBytes > 0)
                                {
                                    iBytes = socket.Receive(RecvBytes, RecvBytes.Length, 0);
                                    recvBufferList.AddRange(RecvBytes.ToArray());
                                }
                                RevBuffer = new byte[recvBufferList.Count];
                                RevBuffer = recvBufferList.ToArray();
                            }
                            else
                            {
                                byte[] lenBytes = new byte[RevPrefixLen];//存放接收数据长度的字节数组
                                socket.Receive(lenBytes, lenBytes.Length, 0);//接收数据包长度数据
                                int dataLen = Int32.Parse(Encoding.GetEncoding(encode).GetString(lenBytes));
                                RevBuffer = new byte[dataLen];
                                socket.Receive(RevBuffer, RevBuffer.Length, 0);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception("接收数据异常!(" + e.Message + ")");
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (socket != null)
                {
                    Close(socket);
                }
            }
            return RevBuffer;
        }

        private static object Encode(Object input, int prefixLen, string encode)
        {
            String inputString = string.Empty;
            if (input != null)
                inputString = input.ToString();
            string dataLenStr = string.Empty.PadLeft(prefixLen, '0');
            byte[] msg = Encoding.GetEncoding(encode).GetBytes(dataLenStr + inputString);
            if (prefixLen > 0)
            {
                byte[] len = Encoding.GetEncoding(encode).GetBytes((msg.Length - prefixLen).ToString().PadLeft(prefixLen, '0'));
                for (int i = 0; i < len.Length; i++)
                {
                    msg[i] = len[i];
                }
            }
            return msg;
        }
        private static object Decode(object getObject, int prefixLen, string encode, int bufSize)
        {
            Socket socket = getObject as Socket;
            byte[] len = new byte[prefixLen];
            byte[] bytes = new byte[bufSize];
            string reStr = string.Empty;
            if (prefixLen == 0)
            {
                List<byte> data = new List<byte>();
                byte[] buffer = new byte[bufSize];
                int length = 0;
                while ((length = socket.Receive(buffer)) > 0)
                {
                    for (int j = 0; j < length; j++)
                    {
                        data.Add(buffer[j]);
                    }
                    if (length < buffer.Length)
                    {
                        break;
                    }
                    buffer = new byte[bufSize];
                }
                if (data.Count > 0)
                {
                    reStr = Encoding.GetEncoding(encode).GetString(data.ToArray(), 0, data.Count);
                    //LogUtil.Write(reStr);
                }
            }
            else
            {
                int reLen = socket.Receive(len, prefixLen, 0);
                if (reLen == prefixLen)
                {
                    string str1 = Encoding.GetEncoding(encode).GetString(len, 0, prefixLen);
                    int dataLen = Int32.Parse(str1);
                    //30246
                    byte[] temp = new byte[dataLen];
                    int getSize = 0;
                    while (true)
                    {
                        bytes = new byte[bufSize];

                        int getLen = socket.Receive(bytes, bytes.Length, 0);
                        if (getSize + getLen <= dataLen)
                        {
                            System.Array.Copy(bytes, 0, temp, getSize, getLen);
                            getSize += getLen;
                        }
                        else
                        {
                            System.Array.Copy(bytes, 0, temp, getSize, dataLen - getSize);
                            getSize += dataLen - getSize;
                        }
                        //30249
                        if (getSize >= dataLen)
                            break;
                    }
                    reStr = Encoding.GetEncoding(encode).GetString(temp, 0, getSize);
                }
            }
            return reStr;
        }

        #region Socket操作
        private static Socket GetConnect(string serIp, int serPort, int secondTimeout)
        {

            Socket socket = null;
            IPAddress ipAddress = IPAddress.Parse(serIp);
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, serPort);
            // Create a TCP/IP  socket.
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            if (secondTimeout > 0)
                socket.SendTimeout = secondTimeout * 1000;
            if (secondTimeout > 0)
                socket.ReceiveTimeout = secondTimeout * 1000;
            socket.Connect(remoteEP);

            return socket;
        }
        private static void Close(Socket socket)
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
        #endregion

    }
}
