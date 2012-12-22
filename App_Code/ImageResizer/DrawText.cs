using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Web;
using System.Drawing.Drawing2D;

namespace tw.patw.ImageResizer
{
    /// <summary>
    /// 繪製文字在圖片上
    /// </summary>
    public class DrawText : IDecorator
    {
        Image image;
        private string _text;
        private int _fontsize;
        private int _mask_pos_x = 0; // 水印橫向座標
        private int _mask_pos_y = 0; // 水印縱向座標
        private Color _fontcolor = Color.Black; // 文字顏色
        private string _FontFamily = "微軟正黑體"; // 字形
        private int _degree = 0; // 旋轉角度

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="image">圖片</param>
        /// <param name="text">文字</param>
        /// <param name="fontsize">字體大小</param>
        /// <param name="x">X軸座標</param>
        /// <param name="y">Y軸座標</param>
        public DrawText(Image image, string text, int fontsize, int x, int y)
        {
            this.image = image;
            this._text = text;
            this._fontsize = fontsize;
            this._mask_pos_x = x;
            this._mask_pos_y = y;
        }

        /// <summary>
        /// 主要作業方法
        /// </summary>
        public Image Operation()
        {
            Graphics g = Graphics.FromImage(image);
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            StringFormat strFormat = new StringFormat();
            strFormat.Alignment = StringAlignment.Center;
            strFormat.LineAlignment = StringAlignment.Center;
            using (SolidBrush SB = new SolidBrush(_fontcolor))
            {
                using (Font str_Font = new Font(_FontFamily, _fontsize, FontStyle.Bold))
                {
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    SizeF labelSize = g.MeasureString(_text, new Font(_FontFamily, _fontsize, FontStyle.Bold));
                    //g.TranslateTransform(0, labelSize.Width);
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    g.RotateTransform(_degree);
                    g.DrawString(_text, str_Font, new SolidBrush(_fontcolor), _mask_pos_x, _mask_pos_y);
                    str_Font.Dispose();
                }
                SB.Dispose();
            }
            return image;
        }

        /// <summary>
        /// 設定旋轉角度
        /// </summary>
        /// <param name="val">旋轉角度</param>
        public void setDegree(int val)
        {
            this._degree = val;
        }

        /// <summary>
        /// 設定文字顏色
        /// </summary>
        /// <param name="val">顏色</param>
        public void setFontcolor(Color val)
        {
            this._fontcolor = val;
        }

        /// <summary>
        /// 設定字體
        /// </summary>
        /// <param name="val">字體名稱</param>
        public void setFontFamily(string val)
        {
            this._FontFamily = val;
        }

        /// <summary>
        /// 存檔方法
        /// </summary>
        public void Save(string file, string path)
        {
            string fOutput = System.IO.Path.Combine(path, file);
            image.Save(fOutput, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        /// <summary>
        /// 輸出方法
        /// </summary>
        public void Output()
        {
            MemoryStream ms_r = new MemoryStream();
            image.Save(ms_r, System.Drawing.Imaging.ImageFormat.Jpeg);
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ContentType = "image/Jpeg";
            HttpContext.Current.Response.BinaryWrite(ms_r.ToArray());
        }
    }
}
