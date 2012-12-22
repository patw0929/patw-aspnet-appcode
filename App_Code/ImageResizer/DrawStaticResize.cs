using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.IO;
using System.Web;

namespace tw.patw.ImageResizer
{
    /// <summary>
    /// 轉換為指定尺寸
    /// </summary>
    public class DrawStaticResize : IDecorator
    {
        Image image;
        Bitmap bmPhoto;

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="image">圖片</param>
        /// <param name="width">寬度</param>
        /// <param name="height">高度</param>
        public DrawStaticResize(Image image, int width, int height)
        {
            this.image = image;
            this._width = width;
            this._height = height;
        }

        int _width;
        int _height;

        /// <summary>
        /// 主要作業方法
        /// </summary>
        public Image Operation()
        {
            bmPhoto = new Bitmap(_width, _height, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(72, 72);
            Graphics g = Graphics.FromImage(bmPhoto);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.DrawImage(image,
                        new Rectangle(0, 0, _width, _height),
                        0, 0, image.Width, image.Height,
                        GraphicsUnit.Pixel);
            image = (Image)bmPhoto;
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
