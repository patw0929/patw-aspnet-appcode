using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Web;

namespace tw.patw.ImageResizer
{
    /// <summary>
    /// 繪製一個方框在圖片上
    /// </summary>
    public class DrawRectangle : IDecorator
    {
        Image image;
        private Color _color;       // 顏色
        private int _size;          // 框線大小
        private int _x;             // 座標(x)
        private int _y;             // 座標(y)
        private int _width;         // 寬度
        private int _height;        // 高度
        

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="image">圖片</param>
        public DrawRectangle(Image image, Color color, int size, int x, int y, int width, int height)
        {
            this.image = image;
            this._color = color;
            this._size = size;
            this._x = x;
            this._y = y;
            this._width = width;
            this._height = height;
        }

        /// <summary>
        /// 主要作業方法
        /// </summary>
        public Image Operation()
        {
            Graphics g = Graphics.FromImage(image);
            using (Pen blackPen = new Pen(_color, _size))
            {
                Rectangle rect = new Rectangle(_x, _y, _width, _height);
                g.DrawRectangle(blackPen, rect);
                blackPen.Dispose();
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
