using System;
using System.Security.Cryptography;
using System.Globalization;
using System.Text;
using System.IO;
using System.Drawing;

namespace tw.patw
{
    public partial class PatwCommon
    {
        public class Convert
        {
            /// <summary>
            /// 將 Base64 轉換為 Image
            /// </summary>
            /// <param name="base64String"></param>
            /// <returns></returns>
            public static Image Base64ToImage(string base64String)
            {
                byte[] imageBytes = System.Convert.FromBase64String(base64String);
                MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                // Convert byte[] to Image
                ms.Write(imageBytes, 0, imageBytes.Length);
                Image image = Image.FromStream(ms, true);
                return image;
            }

            /// <summary>
            /// 將字串轉換為日期
            /// </summary>
            /// <param name="input"></param>
            /// <returns></returns>
            public static DateTime ToDateTime(string input)
            {
                DateTime dt;
                DateTime.TryParse(input, new System.Globalization.CultureInfo("zh-TW"), System.Globalization.DateTimeStyles.None, out dt);
                return dt;
            }
        }
		
	}
}
