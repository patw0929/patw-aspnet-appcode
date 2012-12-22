using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Web;

namespace tw.patw.ImageResizer
{
    public class DrawBorder : IDecorator
    {
        Image image;
        private int _borderSize;
        private Color _borderColor;

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="image">圖片</param>
        /// <param name="borderSize">邊框寬度</param>
        /// <param name="borderColor">邊框顏色</param>
        public DrawBorder(Image image, int borderSize, System.Drawing.Color borderColor)
        {
            this._borderColor = borderColor;
            this._borderSize = borderSize;
            this.image = image;
        }

        /// <summary>
        /// 主要作業方法
        /// </summary>
        public Image Operation()
        {
            Color bordcolor = _borderColor;
            Graphics g = Graphics.FromImage(image);

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            using (Pen Pen_border = new Pen(this._borderColor, this._borderSize))
            {
                g.DrawRectangle(Pen_border, new Rectangle(0, 0, image.Width, image.Height));
                Pen_border.Dispose();
            }
            return image;
        }

        /// <summary>
        /// 存檔方法
        /// </summary>
        public void Save(string file, string path)
        {
            string fOutput = System.IO.Path.Combine(path, file);
            image.Save(fOutput, System.Drawing.Imaging.ImageFormat.Jpeg);
            image.Dispose();
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
            image.Dispose();
        }

    }
}
