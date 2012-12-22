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
        /// 取得 GET 值
        /// </summary>
        /// <param name="vRequestName"></param>
        /// <returns></returns>
        public static string GetGET(string vRequestName)
        {
            // GET REQUEST QUERY STRING 
            return (string.Empty + System.Web.HttpContext.Current.Request[vRequestName]).Trim();
        }

        /// <summary>
        /// 取得 POST 值
        /// </summary>
        /// <param name="strFieldName"></param>
        /// <returns></returns>
        public static string GetPOST(string strFieldName)
        {
            return ("" + System.Web.HttpContext.Current.Request.Form[strFieldName]).Trim();
        }
    }
}
