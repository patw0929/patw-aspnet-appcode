using System;
using System.Web.Security;

namespace tw.patw
{

    public partial class PatwCommon
    {
        /// <summary>
        /// MD5 的摘要描述
        /// </summary>
        public class MD5
        {
            // Methods
            public static string Encrypt(string stringToEncrypt)
            {
                try
                {
                    return FormsAuthentication.HashPasswordForStoringInConfigFile(stringToEncrypt, "MD5");
                }
                catch (Exception exception)
                {
                    return exception.Message;
                }
            }
        }
    }

}