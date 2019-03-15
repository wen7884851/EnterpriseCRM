using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace Framework.Common
{
    /// <summary>
    /// XML序列化工具
    /// </summary>
    public static class XmlHelper
    {
        /// <summary>
        ///  获取xml中的单个节点值
        /// </summary>
        /// <param name="doc">xml字符串</param>
        /// <param name="nodeName">节点名称</param>
        /// <returns></returns>
        public static string GetNodeValue(string strXml, string nodeName)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(strXml);
            return GetNodeValue(doc, nodeName, "");
        }

        /// <summary>
        ///  获取xml中的单个节点值
        /// </summary>
        /// <param name="doc">xml字符串</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="nodeName">默认值</param>
        /// <returns></returns>
        public static string GetNodeValue(string strXml, string nodeName, string Default)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(strXml);
            return GetNodeValue(doc, nodeName, Default);
        }

        /// <summary>
        ///  获取xml中的单个节点值
        /// </summary>
        /// <param name="doc">xml文本对象</param>
        /// <param name="nodeName">节点名称</param>
        /// <returns></returns>
        public static string GetNodeValue(XmlDocument doc, string nodeName)
        {
            return GetNodeValue(doc, nodeName, "");
        }

        /// <summary>
        ///  获取xml中的单个节点中的属性值 
        /// </summary>
        /// <param name="doc">xml文本对象</param>
        /// <param name="nodeName">节点名称</param>
        /// <returns></returns>
        public static string GetNodeAttribute(XmlDocument doc, string nodeName, string AttributeName)
        {
            return GetNodeAttributeValue(doc, nodeName, AttributeName);
        }

        /// <summary>
        ///  获取xml中的单个节点中的属性值 
        /// </summary>
        /// <param name="doc">xml文本对象</param>
        /// <param name="nodeName">节点名称</param>
        /// <returns></returns>
        public static string GetNodeAttribute(string xml, string nodeName, string AttributeName)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            return GetNodeAttributeValue(doc, nodeName, AttributeName);
        }


        /// <summary>
        /// 获取xml中的单个节点中的属性值并可返回默认值
        /// </summary>
        /// <param name="doc">xml文本对象</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="Default">默认值</param>
        /// <returns></returns>
        public static string GetNodeAttributeValue(XmlDocument doc, string nodeName, string AttributeName)
        {
            XmlElement xe = doc.DocumentElement;
            XmlNode xn = xe.SelectSingleNode("//" + nodeName);
            if (xn != null)
            {
                if (xn.Attributes[AttributeName] == null || string.IsNullOrEmpty(xn.Attributes[AttributeName].InnerText))
                    return string.Empty;
                else
                    return xn.Attributes[AttributeName].InnerText.Trim();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取单个节点植
        /// </summary>
        /// <param name="node">XmlNode节点对象</param>
        /// <param name="itemName">节点名称</param>
        /// <returns></returns>
        public static string GetNodeText(XmlNode node, string itemName)
        {
            string strvalue = string.Empty;
            if (node.SelectSingleNode(itemName) != null)
            {
                strvalue = node.SelectSingleNode(itemName).InnerText;
            }
            return strvalue;
        }

        /// <summary>
        /// 获取单个节点植 没解析到节点会返回默认值
        /// </summary>
        /// <param name="node">XmlNode节点对象</param>
        /// <param name="itemName">节点名称</param>
        /// <param name="defaultvalue">默认值</param>
        /// <returns></returns>
        public static string GetNodeText(XmlNode node, string itemName, string defaultvalue)
        {
            if (node.SelectSingleNode(itemName) != null)
            {
                defaultvalue = node.SelectSingleNode(itemName).InnerText;
            }
            return defaultvalue;
        }


        /// <summary>
        /// 获取xml中的单个节点值并可返回默认值
        /// </summary>
        /// <param name="doc">xml文本对象</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="Default">默认值</param>
        /// <returns></returns>
        public static string GetNodeValue(XmlDocument doc, string nodeName, string Default)
        {
            XmlElement xe = doc.DocumentElement;
            XmlNode xn = xe.SelectSingleNode("//" + nodeName);
            if (xn != null)
            {
                if (xn.InnerText.Trim() == "")
                    return Default;
                else
                    return xn.InnerText.Trim();
            }
            else
            {
                return Default;
            }
        }


        /// <summary>
        /// 获取xml中的单个节点
        /// </summary>
        /// <param name="doc">xml文本对象</param>
        /// <param name="nodeName">节点名称</param>
        /// <returns></returns>
        public static XmlNode GetXmlNode(XmlDocument doc, string nodeName)
        {
            XmlElement xe = doc.DocumentElement;
            XmlNode xn = xe.SelectSingleNode("//" + nodeName);
            if (xn != null)
            {
                return xn;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 根据节点名称和节点值获取xml中的节点
        /// </summary>
        /// <param name="doc">xml文本对象</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="nodeValue">节点值</param>
        /// <returns></returns>
        public static XmlNode GetXmlNode(XmlDocument doc, string nodeName, string nodeValue)
        {
            XmlNode node = null;
            XmlElement xe = doc.DocumentElement;
            XmlNodeList list = xe.SelectNodes("//" + nodeName);
            if (list != null && list.Count > 0)
            {
                foreach (XmlNode nodeItem in list)
                {
                    if (nodeItem.InnerText.Trim() == nodeValue)
                    {
                        node = nodeItem;
                    }
                }
            }
            return node;
        }

        /// <summary>
        /// 根据节点元素获取节点中的子节点
        /// </summary>
        /// <param name="doc">xml文本对象</param>
        /// <param name="node">节点名称</param>
        /// <param name="nodeItemName">节点中的子节点名称</param>
        /// <returns></returns>
        public static XmlNode GetXmlNodeItem(XmlDocument doc, XmlNode node, string nodeItemName)
        {
            XmlNode nodeItem = null;
            XmlElement xe = doc.DocumentElement;
            XmlNodeList list = xe.ChildNodes;
            if (list != null && list.Count > 0)
            {
                foreach (XmlNode xnode in list)
                {
                    if (xnode == node)
                    {
                        XmlNode xn = xnode.SelectSingleNode(nodeItemName);
                        if (xn != null)
                        {
                            nodeItem = xn;
                        }
                    }
                }
            }
            return nodeItem;
        }


        /// <summary>
        /// 获取xml中某个节点的子集合节点
        /// </summary>
        /// <param name="doc">xml文本对象</param>
        /// <param name="node">集合节点</param>
        /// <param name="nodeName">子集合节点名称</param>
        /// <returns></returns>
        public static XmlNodeList GetNodeList(XmlDocument doc, string nodeName, string nodeItemName, string serviceKey)
        {
            XmlNodeList nodelist = null;
            XmlElement xe = doc.DocumentElement;
            XmlNodeList list = xe.SelectNodes("//" + nodeName);
            if (list != null && list.Count > 0)
            {
                foreach (XmlNode node in list)
                {
                    if (node.SelectSingleNode("Key").InnerText.Trim() == serviceKey)
                    {
                        nodelist = node.SelectNodes(nodeItemName);
                        if (nodelist != null && nodelist.Count > 0)
                        {
                            return nodelist;
                        }
                        else
                        {
                            return null;
                        }

                    }
                }
            }

            return nodelist;
        }

        /// <summary>
        /// 获取xml中某个节点的子集合节点
        /// </summary>
        /// <param name="doc">xml文本对象</param>
        /// <param name="node">集合节点</param>
        /// <param name="nodeName">子集合节点名称</param>
        /// <returns></returns>
        public static XmlNodeList GetNodeList(string xml, string nodeName, string nodeItemName, string serviceKey)
        {
            XmlNodeList nodelist = null;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlElement xe = doc.DocumentElement;
            XmlNodeList list = xe.SelectNodes("//" + nodeName);
            if (list != null && list.Count > 0)
            {
                foreach (XmlNode node in list)
                {
                    if (node.SelectSingleNode("Key").InnerText.Trim() == serviceKey)
                    {
                        nodelist = node.SelectNodes(nodeItemName);
                        if (nodelist != null && nodelist.Count > 0)
                        {
                            return nodelist;
                        }
                        else
                        {
                            return null;
                        }

                    }
                }
            }

            return nodelist;
        }

        /// <summary>
        /// 获取xml中的集合节点
        /// </summary>
        /// <param name="doc">xml文本对象</param>
        /// <param name="nodeName">集合节点名称</param>
        /// <param name="Default">默认值</param>
        /// <returns></returns>
        public static XmlNodeList GetNodeList(XmlDocument doc, string nodeName)
        {
            XmlElement xe = doc.DocumentElement;
            XmlNodeList list = xe.SelectNodes("//" + nodeName);
            if (list != null && list.Count > 0)
            {
                return list;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取xml中的集合节点
        /// </summary>
        /// <param name="doc">xml文本对象</param>
        /// <param name="nodeName">集合节点名称</param>
        /// <param name="Default">默认值</param>
        /// <returns></returns>
        public static XmlNodeList GetNodeList(string strXml, string nodeName)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(strXml);
            XmlElement xe = doc.DocumentElement;
            XmlNodeList list = xe.SelectNodes("//" + nodeName);
            if (list != null && list.Count > 0)
            {
                return list;
            }
            else
            {
                return null;
            }
        }



        /// <summary>
        /// 读取测试程序中的配置文件
        /// </summary>
        /// <param name="name">配置文件名称</param>
        /// <returns>string</returns>
        public static string ReadTestXML(string name)
        {
            string configPath = AppDomain.CurrentDomain.BaseDirectory + "Config\\" + name + ".xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(configPath);
            configPath = "<Response>" + doc.SelectSingleNode("Response").InnerXml + "</Response>";
            return configPath;
        }




    }
}
