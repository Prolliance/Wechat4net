using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Wechat4net
{
    /// <summary>
    /// 微信应用设置
    /// </summary>
    public class WechatConfig
    {
        public static readonly string Token;

        public static readonly string CorpID;

        public static readonly string EncodingAESKey;

        public static readonly string Secret;

        public static readonly int AppID;

        /// <summary>
        /// 语言(MP需要，QY不需要)
        /// </summary>
        public static Utils.Enums.MP.Language Language;

        public static string Value(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return "";
            }
            return config.AppSettings.Settings[key].Value;
        }

        private static Configuration config = null;
        static WechatConfig()
        {
            /*
            Assembly assembly = Assembly.GetCallingAssembly();
            Uri uri = new Uri(Path.GetDirectoryName(assembly.CodeBase));
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
            fileMap.ExeConfigFilename = Path.Combine(uri.LocalPath, "Wechat4net.dll.config");// assembly.GetName().Name + ".dll.config");
            */
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
            fileMap.ExeConfigFilename = new Uri((Assembly.GetExecutingAssembly()).CodeBase).LocalPath + ".config";
            config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);

            //TokenIssuer = config.AppSettings.Settings["TokenIssuer"].Value; //What should be a good value here? web api url?
            //TokenAudience = config.AppSettings.Settings["TokenAudience"].Value;  //What should be a good value here?
            //TokenLifetimeInMinutes = Convert.ToDouble(config.AppSettings.Settings["TokenLifetimeInMinutes"].Value);
            ////RNGCryptoServiceProvider cryptoProvider = new RNGCryptoServiceProvider();
            ////cryptoProvider.GetNonZeroBytes(SymmetricKey);   //Secure enough? Will change on every call. Has to be made a constant.
            //SymmetricKey = Encoding.UTF8.GetBytes(config.AppSettings.Settings["Key"].Value);

            Token = config.AppSettings.Settings["Token"].Value;
            CorpID = config.AppSettings.Settings["CorpID"].Value;
            EncodingAESKey = config.AppSettings.Settings["EncodingAESKey"].Value;
            Secret = config.AppSettings.Settings["Secret"].Value;
            AppID = Convert.ToInt32(config.AppSettings.Settings["AppID"].Value);
            Language = (Utils.Enums.MP.Language)Convert.ToInt32(config.AppSettings.Settings["Language"].Value);
        }
    }
}