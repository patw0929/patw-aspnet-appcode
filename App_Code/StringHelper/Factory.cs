using System;
using System.Collections;
using System.Collections.Generic;

namespace tw.patw.StringHelper
{

    public class Factory
    {

        public static object GetHelper(string helperName)
        {

            switch (helperName.ToString().ToLower())
            {

                case "big5":
                    return new Big5();

                case "utf8":
                    return new UTF8();

                case "normal":
                    return new Normal();

                default:
                    throw new Exception("無法辨識 helperName");
            }


        }

    }

}
