using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.Caching;
using System.Web;

namespace tw.patw
{

    public class ValidateCode
    {

        //驗證碼
        private string _checkCode;
        //驗證碼產生位數

        private int _num = 5;

        public ValidateCode()
        {
        }

        public int num
        {
            get
            {
                return _num;
            }

            set
            {
                if (ValidatorFuncs.IsBetweenLength(_num.ToString(), 1, 8))
                {
                    _num = value;
                }
                else
                {
                    throw new Exception("驗證碼產生位數必須為 1 ~ 8 之間的正整數。");
                }
            }
        }

        /// <summary>
        /// 繪製驗證圖
        /// </summary>
        /// <returns>傳回驗證碼，請放置在 Session 中。</returns>
        /// <remarks></remarks>
        public string DrawImage()
        {

            this._checkCode = this.GetRandomNumberString(_num);
            this.CreateImages(this._checkCode);
            return this._checkCode;

        }

        /// <summary>
        /// 產生驗證圖片
        /// </summary>
        /// <param name="checkCode">加密字元</param>
        /// <remarks></remarks>

        private void CreateImages(string checkCode)
        {
            if (checkCode == null || checkCode.Trim() == String.Empty)
            {
                return;
            }

            System.Drawing.Bitmap image = new System.Drawing.Bitmap(Convert.ToInt32(Math.Ceiling((checkCode.Length * 13.5))), 25);
            System.Drawing.Graphics g = Graphics.FromImage(image);

            try
            {
                //生成隨機生成器
                Random random = new Random();

                //清空圖片背景色
                g.Clear(Color.White);

                for (int i = 0; i <= 24; i++)
                {
                    //畫圖片的背景噪音線
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);

                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }

                Font font = new System.Drawing.Font("Arial", 14, (System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic));

                System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2f, true);
                g.DrawString(checkCode, font, brush, 2, 2);

                for (int i = 0; i <= 99; i++)
                {
                    //畫圖片的前景噪音點
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);

                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }

                //畫圖片的邊框線
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ContentType = "image/Gif";
                HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }

        /// <summary>
        /// 隨機產生字元
        /// </summary>
        /// <param name="int_NumberLength">長度</param>
        /// <returns>數字</returns>
        /// <remarks></remarks>
        public string GetRandomNumberString(int int_NumberLength)
        {
            string str_Number = string.Empty;
            Random theRandomNumber = new Random();
            for (int int_index = 0; int_index <= int_NumberLength - 1; int_index++)
            {
                str_Number += theRandomNumber.Next(10).ToString();
            }
            return str_Number;
        }

    }

}
