using System;
using System.Configuration;

namespace tw.patw
{
    public sealed class ConfigHelper
    {
        /// <summary>
        /// 取得 AppSettings 中設定的字串
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConfigString(string key)
        {
            string result = string.Empty;
            try
            {
                result = ConfigurationManager.AppSettings[key];
            }
            catch
            { }
            return result.ToString();
        }

        /// <summary>
        /// 取得 AppSettings 中設定的布林值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool GetConfigBool(string key)
        {
            bool result = false;
            string cfgVal = GetConfigString(key);
            if (null != cfgVal && string.Empty != cfgVal)
            {
                try
                {
                    result = bool.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    // 忽略格式例外
                }
            }
            return result;
        }
        /// <summary>
        /// 取得 AppSettings 中設定的浮點數值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static decimal GetConfigDecimal(string key)
        {
            decimal result = 0;
            string cfgVal = GetConfigString(key);
            if (null != cfgVal && string.Empty != cfgVal)
            {
                try
                {
                    result = decimal.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    // 忽略格式例外
                }
            }

            return result;
        }
        /// <summary>
        /// 取得 AppSettings 中設定的整數值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetConfigInt(string key)
        {
            int result = 0;
            string cfgVal = GetConfigString(key);
            if (null != cfgVal && string.Empty != cfgVal)
            {
                try
                {
                    result = int.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    // 忽略格式例外
                }
            }

            return result;
        }
    }
}