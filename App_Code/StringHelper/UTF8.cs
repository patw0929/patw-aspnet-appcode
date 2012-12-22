using System;
using System.Collections.Generic;

using System.Text;

namespace tw.patw.StringHelper
{

    public class UTF8 : tw.patw.StringHelper.IStringHelper
    {

        public int getLength(char value)
        {

            if (string.IsNullOrEmpty(Convert.ToString(value)))
                return 0;

            return System.Text.Encoding.GetEncoding("UTF-8").GetBytes(value.ToString()).Length;

        }

        public int getLength(string value)
        {

            if (string.IsNullOrEmpty(value))
                return 0;

            byte[] strbytes = null;
            int tmpcnt = 0;

            for (int i = 0; i <= value.Length - 1; i++)
            {
                strbytes = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(value.Substring(i, 1));
                if (strbytes.Length == 3)
                {
                    tmpcnt += 2;
                }
                else
                {
                    tmpcnt += 1;
                }
            }
            return tmpcnt;

        }

        /// <summary>
        /// 連特殊字的中文字也視為佔用二個字的長度
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string Left(string value, int length)
        {

            byte[] strbytes = null;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            int nLength = 0;

            for (int i = 0; i <= value.Length - 1; i++)
            {
                strbytes = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(value.Substring(i, 1));
                if (strbytes.Length == 3)
                {
                    sb.Append(value.Substring(i, 1));
                    nLength += 2;
                }
                else
                {
                    sb.Append(value.Substring(i, 1));
                    nLength += 1;
                }

                if (nLength >= length)
                {
                    break;
                }
            }

            return sb.ToString();

        }

    }

}
