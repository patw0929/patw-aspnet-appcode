using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Microsoft.VisualBasic.CompilerServices;
using System.Text.RegularExpressions;


public class ValidatorFuncs
{

    /// <summary>
    /// 檢查輸入的字串是否不為空值
    /// </summary>
    /// <param name="Value">輸入字串</param>
    /// <returns>True / False</returns>
    /// <remarks></remarks>
    public static bool IsNotEmpty(string Value)
    {
        return (!(Operators.CompareString(("" + Value).Trim(), string.Empty, false) == 0));
    }

    /// <summary>
    /// 檢查輸入的字串是否為合法的 E-mail
    /// </summary>
    /// <param name="Value">輸入字串</param>
    /// <returns>True / False</returns>
    /// <remarks></remarks>
    public static bool IsEMail(string Value)
    {
        bool flag = false;
        Value = Value.Trim();
        if ((Operators.CompareString(Value, string.Empty, false) == 0))
        {
            return flag;
        }
        return Regex.Match(Value, "^\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$").Success;
    }

    /// <summary>
    /// 檢查輸入的字串是否為合法的網址
    /// </summary>
    /// <param name="Value"></param>
    /// <returns></returns>
    /// <remarks></remarks>
    public static bool IsURL(string Value)
    {
        bool flag = false;
        Value = Value.Trim();
        if ((Operators.CompareString(Value, string.Empty, false) == 0))
        {
            return flag;
        }
        return Regex.Match(Value, "^(https?|ftp):\\/\\/(((([a-zA-Z]|\\d|-|\\.|_|~|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|(%[\\da-f]{2})|[!\\$&'\\(\\)\\*\\+,;=]|:)*@)?(((\\d|[1-9]\\d|1\\d\\d|2[0-4]\\d|25[0-5])\\.(\\d|[1-9]\\d|1\\d\\d|2[0-4]\\d|25[0-5])\\.(\\d|[1-9]\\d|1\\d\\d|2[0-4]\\d|25[0-5])\\.(\\d|[1-9]\\d|1\\d\\d|2[0-4]\\d|25[0-5]))|((([a-zA-Z]|\\d|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|(([a-zA-Z]|\\d|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])([a-zA-Z]|\\d|-|\\.|_|~|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])*([a-zA-Z]|\\d|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])))\\.)+(([a-zA-Z]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|(([a-zA-Z]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])([a-zA-Z]|\\d|-|\\.|_|~|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])*([a-zA-Z]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])))\\.?)(:\\d*)?)(\\/((([a-zA-Z]|\\d|-|\\.|_|~|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|(%[\\da-f]{2})|[!\\$&'\\(\\)\\*\\+,;=]|:|@)+(\\/(([a-zA-Z]|\\d|-|\\.|_|~|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|(%[\\da-f]{2})|[!\\$&'\\(\\)\\*\\+,;=]|:|@)*)*)?)?(\\?((([a-zA-Z]|\\d|-|\\.|_|~|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|(%[\\da-f]{2})|[!\\$&'\\(\\)\\*\\+,;=]|:|@)|[\\uE000-\\uF8FF]|\\/|\\?)*)?(\\#((([a-zA-Z]|\\d|-|\\.|_|~|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|(%[\\da-f]{2})|[!\\$&'\\(\\)\\*\\+,;=]|:|@)|\\/|\\?)*)?$").Success;
    }

    /// <summary>
    /// 檢查輸入的字串是否包含英數之外的字元
    /// </summary>
    /// <param name="Value">輸入字串</param>
    /// <returns>True / False</returns>
    /// <remarks></remarks>
    public static bool IsEngAndNum(string Value)
    {
        bool flag = false;
        Value = Value.Trim();
        if ((Operators.CompareString(Value, string.Empty, false) == 0))
        {
            return flag;
        }
        return Regex.Match(Value, "^[a-zA-Z0-9]*$").Success;
    }

    /// <summary>
    /// 檢查輸入的兩個字串是否相等
    /// </summary>
    /// <param name="Value">字串1</param>
    /// <param name="TargetValue">字串2</param>
    /// <returns>True / False</returns>
    /// <remarks></remarks>
    public static bool IsEqualTo(string Value, string TargetValue)
    {
        return (Operators.CompareString(Value, TargetValue, false) == 0);
    }

    /// <summary>
    /// 檢查輸入的字串是否為合法的日期
    /// </summary>
    /// <param name="Value">輸入字串</param>
    /// <returns>True / False</returns>
    /// <remarks></remarks>
    public static bool IsValidDate(string Value)
    {

        try
        {
            System.DateTime.Parse(Value);
        }
        catch (Exception ex)
        {
            return false;
        }

        return true;

    }

    /// <summary>
    /// 檢查輸入的字串是否為合法的數字
    /// </summary>
    /// <param name="Value">輸入字串</param>
    /// <returns>True / False</returns>
    /// <remarks></remarks>
    public static bool IsDigits(string Value)
    {
        bool flag = false;
        Value = Value.Trim();
        if ((Operators.CompareString(Value, string.Empty, false) == 0))
        {
            return flag;
        }
        return Regex.Match(Value, "^\\d+$").Success;
    }

    /// <summary>
    /// 檢查輸入的字串長度是否在輸入最大長度內。
    /// </summary>
    /// <param name="Value">輸入字串</param>
    /// <param name="Param">最大長度</param>
    /// <returns>True / False</returns>
    /// <remarks></remarks>
    public static bool IsMaxLength(string Value, int Param)
    {
        bool flag = false;
        Value = Value.Trim();
        if ((Operators.CompareString(Value, string.Empty, false) == 0))
        {
            return flag;
        }
        return (Value.Length <= Param);
    }

    /// <summary>
    /// 檢查輸入的字串長度是否介於輸入範圍間。
    /// </summary>
    /// <param name="Value">輸入字串</param>
    /// <param name="Min">最小長度(含)</param>
    /// <param name="Max">最大長度(含)</param>
    /// <returns>True / False</returns>
    /// <remarks></remarks>
    public static bool IsBetweenLength(string Value, int Min, int Max)
    {
        bool flag = false;
        Value = Value.Trim();
        if ((Operators.CompareString(Value, string.Empty, false) == 0))
        {
            return flag;
        }
        return (Value.Length >= Min && Value.Length <= Max);
    }

    /// <summary>
    /// 檢查輸入的字串是否為合法的台灣手機
    /// </summary>
    /// <param name="Value"></param>
    /// <returns></returns>
    /// <remarks></remarks>
    public static bool IsMobile(string Value)
    {
        bool flag = false;
        Value = Value.Trim();
        if ((Operators.CompareString(Value, string.Empty, false) == 0))
        {
            return flag;
        }
        return Regex.Match(Value, "(^09[0-9]{8}$)").Success;
    }

    /// <summary>
    /// 檢查輸入的字串是否為合法的台灣身分證字號
    /// </summary>
    /// <param name="IDCard">輸入字串</param>
    /// <returns></returns>
    /// <remarks></remarks>
    public static bool IsValidTWID(string IDCard)
    {
        string strFirstCode = "";
        if (IDCard.Length != 10)
        {
            //長度為10
            return false;
        }
        if (IDCard.Substring(1, 1) != "1" && IDCard.Substring(1, 1) != "2")
        {
            //第二位必須為1或2
            return false;
        }
        switch (IDCard.Substring(0, 1))
        {
            case "A":
                strFirstCode = "10";
                break;

            case "B":
                strFirstCode = "11";
                break;

            case "C":
                strFirstCode = "12";
                break;

            case "D":
                strFirstCode = "13";
                break;

            case "E":
                strFirstCode = "14";
                break;

            case "F":
                strFirstCode = "15";
                break;

            case "G":
                strFirstCode = "16";
                break;

            case "H":
                strFirstCode = "17";
                break;

            case "I":
                strFirstCode = "34";
                break;

            case "J":
                strFirstCode = "18";
                break;

            case "K":
                strFirstCode = "19";
                break;

            case "L":
                strFirstCode = "20";
                break;

            case "M":
                strFirstCode = "21";
                break;

            case "N":
                strFirstCode = "22";
                break;

            case "O":
                strFirstCode = "35";
                break;

            case "P":
                strFirstCode = "23";
                break;

            case "Q":
                strFirstCode = "24";
                break;

            case "R":
                strFirstCode = "25";
                break;

            case "S":
                strFirstCode = "26";
                break;

            case "T":
                strFirstCode = "27";
                break;

            case "U":
                strFirstCode = "28";
                break;

            case "V":
                strFirstCode = "29";
                break;

            case "W":
                strFirstCode = "32";
                break;

            case "X":
                strFirstCode = "30";
                break;

            case "Y":
                strFirstCode = "31";
                break;

            case "Z":
                strFirstCode = "33";
                break;

            default:
                return false;
        }
        int iAllNum = Convert.ToInt32(strFirstCode.Substring(0, 1)) + Convert.ToInt32(strFirstCode.Substring(1, 1)) * 9 + Convert.ToInt32(IDCard.Substring(1, 1)) * 8 + Convert.ToInt32(IDCard.Substring(2, 1)) * 7 + Convert.ToInt32(IDCard.Substring(3, 1)) * 6 + Convert.ToInt32(IDCard.Substring(4, 1)) * 5 + Convert.ToInt32(IDCard.Substring(5, 1)) * 4 + Convert.ToInt32(IDCard.Substring(6, 1)) * 3 + Convert.ToInt32(IDCard.Substring(7, 1)) * 2 + Convert.ToInt32(IDCard.Substring(8, 1)) * 1 + Convert.ToInt32(IDCard.Substring(9, 1)) * 1;
        if (iAllNum % 10 != 0)
        {
            return false;
        }
        return true;
    }
	/// <summary>
    /// 檢查輸入的字串是否為合法的發票號碼
    /// </summary>
    /// <param name="Value"></param>
    /// <returns></returns>
    /// <remarks></remarks>
	public static bool IsInvoiceNo(string Value)
    {
        bool flag = false;
        Value = Value.Trim();
        if ((Operators.CompareString(Value, string.Empty, false) == 0))
        {
            return flag;
        }
        return Regex.Match(Value, @"^[a-zA-Z]{2}\d{8}$").Success;
    }
    /// <summary>
    /// 檢查輸入的字串是否為中文字
    /// </summary>
    /// <param name="Value"></param>
    /// <returns></returns>
    /// <remarks></remarks>
    public static bool IsChinese(string Value)
    {
        bool flag = false;
        Value = Value.Trim();
        if ((Operators.CompareString(Value, string.Empty, false) == 0))
        {
            return flag;
        }
        return Regex.Match(Value, "(^[\u4E00-\u9fa5]+$)").Success;
    }

}