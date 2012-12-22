using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace tw.patw
{
    public partial class PatwCommon
    {
        /// <summary>
        /// 取得客戶端 IP
        /// </summary>
        /// <returns></returns>
        public static string GetClientIP()
        {
            string strIPAddr = "";
            if (string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]) ||
                Strings.InStr(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"], "unknown", CompareMethod.Text) > 0)
            {
                strIPAddr = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            else if (Strings.InStr(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"], ",", CompareMethod.Text) > 0)
            {
                strIPAddr = Strings.Mid(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"], 1,
                            Strings.InStr(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"], ",", CompareMethod.Text) - 1);
            }
            else if (Strings.InStr(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"], ";",
                     CompareMethod.Text) > 0)
            {
                strIPAddr = Strings.Mid(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"], 1,
                            Strings.InStr(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"], ";", CompareMethod.Text) - 1);
            }
            else
            {
                strIPAddr = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }

            return Strings.Trim(Strings.Mid(strIPAddr, 1, 30));
        }
    }
}

        




