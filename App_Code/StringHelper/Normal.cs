using System;
using System.Collections.Generic;

using System.Text;

namespace tw.patw.StringHelper
{

    public class Normal : tw.patw.StringHelper.IStringHelper
    {

        public int getLength(char value)
        {

            if (string.IsNullOrEmpty(Convert.ToString(value)))
                return 0;

            return value.ToString().Length;

        }

        public int getLength(string value)
        {

            if (string.IsNullOrEmpty(value))
                return 0;

            return value.ToString().Length;

        }

        /// <summary>
        /// 中英文均視為一個字
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string Left(string value, int length)
        {

            if (value.Length <= length)
                return value;
            return value.Substring(0, length);

        }

    }

}
