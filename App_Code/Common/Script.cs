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
        /// 註冊 Script 區塊
        /// </summary>
        /// <param name="vPage"></param>
        /// <param name="vScript"></param>
        public static void RegisterClientScriptBlock(System.Web.UI.Page vPage, string vScript)
        {
            System.Threading.Thread.Sleep(10);
            ScriptManager.RegisterClientScriptBlock(vPage, typeof(System.String), "JSPBlock" + DateAndTime.Now.ToString("yyyyMMddHHmmssffff"), "<script>" + vScript + "</script>", false);
        }

        /// <summary>
        /// 註冊 Alert 提示框
        /// </summary>
        /// <param name="vPage"></param>
        /// <param name="vMessage"></param>
        public static void RegisterClientScriptAlert(System.Web.UI.Page vPage, string vMessage)
        {
            PatwCommon.RegisterClientScriptBlock(vPage, "alert('" + vMessage.Replace("'", "\\'") + "') ; ");
        }

        /// <summary>
        /// 註冊 Dialog 提示框
        /// </summary>
        /// <param name="vPage"></param>
        /// <param name="vMessage"></param>
        public static void RegisterClientScriptDialog(System.Web.UI.Page vPage, string vMessage)
        {
            string act="";
            act += "$(document).ready(function () {";
            act += "$(\"#msg\").html('" + vMessage.Replace("'", "\\'") + "');";
            act += "$(\"#msg\").dialog({resizable: false,draggable: false,height: 200,modal: true,buttons: {\"OK\": function() {$( this ).dialog( \"close\" );}}});";
            act += "});";
            PatwCommon.RegisterClientScriptBlock(vPage, act);
        }

        /// <summary>
        /// 註冊控制項焦點
        /// </summary>
        /// <param name="vForm"></param>
        /// <param name="vObject"></param>
        /// <param name="vMessage"></param>
        public static void RegisterClientScriptObjectFocus(System.Web.UI.Page vForm, System.Web.UI.WebControls.WebControl vObject, string vMessage)
        {
            // Set javascript alert message & Set obj focus
            if (vMessage != string.Empty)
                PatwCommon.RegisterClientScriptAlert(vForm, vMessage);
            PatwCommon.RegisterClientScriptBlock(vForm, "document.getElementById('" + vObject.ClientID + "').focus() ;");
        }
    }
}

        