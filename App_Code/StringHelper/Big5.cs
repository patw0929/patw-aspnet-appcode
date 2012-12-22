using System;
using System.Collections.Generic;

using System.Text;

namespace tw.patw.StringHelper
{

    public class Big5 : tw.patw.StringHelper.IStringHelper
    {

        public int getLength(char value)
        {

            if (string.IsNullOrEmpty(Convert.ToString(value)))
                return 0;

            return System.Text.Encoding.GetEncoding("Big5").GetBytes(value.ToString()).Length;

        }

        public int getLength(string value)
        {

            if (string.IsNullOrEmpty(value))
                return 0;

            return System.Text.Encoding.GetEncoding("Big5").GetBytes(value.ToString()).Length;

        }

        /// <summary>
        /// 中文視為佔用二個字的長度，特殊字元佔一個字
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string Left(string value, int length)
        {

            string result = "";
            int counter = 0;


            foreach (char chr in value)
            {
                counter = counter + System.Text.Encoding.GetEncoding("Big5").GetBytes(chr.ToString()).Length;
                if (counter <= length)
                {
                    result = result + chr;
                }
                else
                {
                    break;
                }

            }

            return result;

        }

    }

}
