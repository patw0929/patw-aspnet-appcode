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
    /// 裁切方法
    /// </summary>
    public class DrawCrop : IDecorator
    {
        Image image;
        Bitmap bmPhoto;
        private int _x;
        private int _y;
        private int _width;
        private int _height;

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="image">圖片</param>
        /// <param name="x">X軸</param>
        /// <param name="y">Y軸</param>
        /// <param name="width">裁切寬度</param>
        /// <param name="height">裁切高度</param>
        public DrawCrop(Image image, int x, int y, int width, int height)
        {
            this._x = x;
            this._y = y;
            this._width = width;
            this._height = height;
            this.image = image;
        }

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
                              _x, _y, _width, _height,
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
