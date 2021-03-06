using System;
using System.Collections.Specialized;
using System.Configuration;
namespace Adpost.Common.Function
{
    /// <summary>
    /// Web.config 操作类
    ///  Copyright (C) 2006-2008 ChenZhiRen(Adpost) All Right Reserved.
    /// 定义为不可继承性
    /// typeof(System.Configuration.NameValueFileSectionHandler).Assembly.FullName.ToString()
    /// </summary>
    public sealed class ConfigModel
    {
        #region 通用获取指定节点值
        /// <summary>
        /// 取得配置文件中的字符串KEY
        /// </summary>
        /// <param name="SectionName">节点名称</param>
        /// <param name="key">KEY名</param>
        /// <returns>返回KEY值</returns>
        public static string GetConfigString(string SectionName, string key)
        {
            string returnVal = "";
            if (SectionName != "")
            {
                try
                {
                    var cfgName = (NameValueCollection)ConfigurationSettings.GetConfig(SectionName);
                    //NameValueCollection cfgName = (NameValueCollection)ConfigurationManager.GetSection(SectionName);
                    if (cfgName[key] != null)
                    {
                        returnVal = cfgName[key];
                    }
                    cfgName = null;
                }catch
                {}
            }
            return returnVal;
        }
        /// <summary>
        /// 得到配置文件中的配置decimal信息
        /// </summary>
        /// <param name="SectionName">节点名称</param>
        /// <param name="key">KEY名称</param>
        /// <returns>返回浮点数</returns>
        public static decimal GetConfigDecimal(string SectionName, string key)
        {
            decimal result = 0;
            string cfgVal = GetConfigString(SectionName, key);
            if (null != cfgVal && string.Empty != cfgVal)
            {
                //result = Convert.ToDecimal(cfgVal);
                try
                {
                result = decimal.Parse(cfgVal);
                }
                catch(FormatException)
                {
                  //格式化错误
                }
            }
            return result;
        }
        /// <summary>
        /// 得到配置文件中的配置int信息
        /// </summary>
        /// <param name="SectionName">节点名称</param>
        /// <param name="key">KEY名</param>
        /// <returns>返回整数</returns>
        public static int GetConfigInt(string SectionName, string key)
        {
            int result = 0;
            string cfgVal = GetConfigString(SectionName, key);
            if (null != cfgVal && string.Empty != cfgVal)
            {
                //result = Convert.ToInt32(cfgVal);
                try
                {
                    result = Int32.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    //格式化错误.
                }
            }
            return result;
        }
        /// <summary>
        /// 得到配置文件中的配置int信息
        /// </summary>
        /// <param name="SectionName">节点名称</param>
        /// <param name="key">KEY名</param>
        /// <returns>返回布尔值</returns>
        public static bool GetConfigBoolean(string SectionName, string key)
        {
            bool result = false;
            string cfgVal = GetConfigString(SectionName, key);
            if (null != cfgVal && string.Empty != cfgVal)
            {
                //result = Convert.ToInt32(cfgVal);
                try
                {
                    result = bool.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    //格式化错误.
                }
            }
            return result;
        }
        #endregion        
        #region 获取AppSetting节点值
        /// <summary>
        /// 取得默认节点的配置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetAppSettingString(string key)
        {
            string returnVal = "";
            NameValueCollection appSettings = ConfigurationSettings.AppSettings;
            //NameValueCollection appSettings = ConfigurationManager.AppSettings;
            if (appSettings[key] != null)
            {
                returnVal = appSettings[key];
            }
            appSettings = null;
            return returnVal;
        }
        /// <summary>
        /// 得到配置文件中的默认节点配置int信息
        /// </summary>
        /// <param name="key">KEY名</param>
        /// <returns>返回整数</returns>
        public static int GetAppSettingInt(string key)
        {
            int result = 0;
            string cfgVal = GetAppSettingString(key);
            if (null != cfgVal && string.Empty != cfgVal)
            {
                //result = Convert.ToInt32(cfgVal);
                try
                {
                    result = Int32.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    //格式化错误.
                }
            }
            return result;
        }
        /// <summary>
        /// 取得配置文件中 默认节点的 浮点数型
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static decimal GetAppSettingDecimal(string key)
        {
            decimal result = 0;
            string cfgVal = GetAppSettingString(key);
            if (null != cfgVal && string.Empty != cfgVal)
            {
                //result = Convert.ToDecimal(cfgVal);
                try
                {
                    result = decimal.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    //格式化错误
                }
            }
            return result;
        }
        /// <summary>
        /// 得到配置文件中的配置int信息
        /// </summary>
        /// <param name="key">KEY名</param>
        /// <returns>返回布尔值</returns>
        public static bool GetAppSettingBoolean(string key)
        {
            bool result = false;
            string cfgVal = GetAppSettingString(key);
            if (null != cfgVal && string.Empty != cfgVal)
            {
                //result = Convert.ToInt32(cfgVal);
                try
                {
                    result = bool.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    //格式化错误.
                }
            }
            return result;
        }
        #endregion
        #region 获取 ConnectionStrings节点值
        /// <summary>
        /// 取得ConnectionStrings节点的配置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConnectionString(string key)
        {
            string returnVal = "";
            //ConnectionStringSettingsCollection connectionStrings = ConfigurationManager.ConnectionStrings;
            //if (null != connectionStrings[key])
            //{
            //    returnVal = connectionStrings[key].ConnectionString;
            //}
            //connectionStrings = null;
            return returnVal;
        }
        #endregion
        #region 节点管理
        /// <summary>
        /// 写入,更新配置文件节点
        /// </summary>
        /// <param name="SectionName">节点名称</param>
        /// <parma name="key">键名</param>
        /// <parma name="value">键值</param>
        public static void SetConfigKeyValue(string SectionName, string key, string keyvalue)
        {
            //导入配置文件
            System.Xml.XmlDocument doc = loadConfigDocument();
            //重新取得 节点名
            System.Xml.XmlNode node = doc.SelectSingleNode("//" + SectionName);
            if (node == null)
                throw new InvalidOperationException(SectionName + " section not found in config file.");

            try
            {
                // 用 'add'元件 格式化是否包含键名 
                // select the 'add' element that contains the key
                System.Xml.XmlElement elem = (System.Xml.XmlElement)node.SelectSingleNode(string.Format("//add[@key='{0}']", key));

                if (elem != null)
                {
                    //修改或添加键值
                    elem.SetAttribute("value", keyvalue);
                }
                else
                {
                    //如果没有发现键名则进行添加设置键名和键值
                    elem = doc.CreateElement("add");
                    elem.SetAttribute("key", key);
                    elem.SetAttribute("value", keyvalue);
                    node.AppendChild(elem);
                }
                doc.Save(getConfigFilePath());
            }
            catch
            {
                throw;
            }

        }
        /// <summary>
        /// 写入,更新配置文件默认节点
        /// </summary>
        /// <parma name="key">键名</param>
        /// <parma name="value">键值</param>
        public static void SetConfigKeyValue(string key, string keyvalue)
        {
            //导入配置文件
            string SectionName = "appSettings";
            System.Xml.XmlDocument doc = loadConfigDocument();
            //重新取得 节点名
            System.Xml.XmlNode node = doc.SelectSingleNode("//" + SectionName);
            if (node == null)
                throw new InvalidOperationException(SectionName + " section not found in config file.");

            try
            {
                // 用 'add'元件 格式化是否包含键名 
                // select the 'add' element that contains the key
                System.Xml.XmlElement elem = (System.Xml.XmlElement)node.SelectSingleNode(string.Format("//add[@key='{0}']", key));

                if (elem != null)
                {
                    //修改或添加键值
                    elem.SetAttribute("value", keyvalue);
                }
                else
                {
                    //如果没有发现键名则进行添加设置键名和键值
                    elem = doc.CreateElement("add");
                    elem.SetAttribute("key", key);
                    elem.SetAttribute("value", keyvalue);
                    node.AppendChild(elem);
                }
                doc.Save(getConfigFilePath());
            }
            catch
            {
                throw;
            }

        }
        /// <summary>
        /// 读入配置文件
        /// </summary>
        private static System.Xml.XmlDocument loadConfigDocument()
        {
            System.Xml.XmlDocument doc = null;
            try
            {
                doc = new System.Xml.XmlDocument();
                doc.Load(getConfigFilePath());
                return doc;
            }
            catch (System.IO.FileNotFoundException e)
            {
                throw new Exception("No configuration file found.", e);
            }
        }
        /// <summary>
        /// 取得置文件路径和名称
        /// </summary>
        private static string getConfigFilePath()
        {
            return AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            //return Assembly.GetExecutingAssembly().Location + ".config";
        }
        #endregion
    }
}
