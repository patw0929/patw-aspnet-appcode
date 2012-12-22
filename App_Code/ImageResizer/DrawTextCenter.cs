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
    /// 繪製文字在圖片上
    /// </summary>
    public class DrawTextCenter : IDecorator
    {
        Image image;
        private string _text;
        private int _mask_pos_x = 0;            // 水印橫向座標
        private int _Xoffset = 0;               // 水平偏移量
        private int _mask_pos_y = 0;            // 水印縱向座標
        public Color _fontcolor = Color.Black;  // 文字顏色
        private int _FontMaxSize;               // 字體大小上限
        private int _FontMinSize;               // 字體大小下限
        private int _Width;                     // 文字寬度
        private string _FontFamily = "Tahoma";  // 字型名稱
        

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="image">圖片</param>
        /// <param name="text">文字</param>
        /// <param name="FontMaxSize">字體大小上限</param>
        /// <param name="Width">文字寬度</param>
        /// <param name="y">垂直高度</param>
        public DrawTextCenter(Image image, string text, int FontMaxSize, int Width, int y)
        {
            this._text = text;
            this._FontMaxSize = FontMaxSize;
            this._Width = Width;
            this._mask_pos_y = y;
            this.image = image;
        }

        

        /// <summary>
        /// 主要作業方法
        /// </summary>
        public Image Operation()
        {
            Graphics g = Graphics.FromImage(image);
            _mask_pos_x = (image.Width / 2) + _Xoffset;
            SizeF steSize = new SizeF();
            for (int i = _FontMaxSize; i >= _FontMinSize; i--)
            {
                using (Font testFont = new Font(_FontFamily, i, FontStyle.Bold))
                {
                    steSize = g.MeasureString(_text, testFont);


                    if ((ushort)steSize.Width < (ushort)_Width || i == _FontMinSize)
                    {
                        
                        using (SolidBrush semiTransBrush = new SolidBrush(_fontcolor))
                        {
                            StringFormat StrFormat = new StringFormat();
                            StrFormat.Alignment = StringAlignment.Center;
                            g.DrawString(_text, testFont, semiTransBrush, new PointF(_mask_pos_x, _mask_pos_y), StrFormat);
                            semiTransBrush.Dispose();
                        }
                        testFont.Dispose();
                        break;
                    }
                }
            }
            
                
            
            return image;
        }

        /// <summary>
        /// 設定水平位置偏移
        /// </summary>
        /// <param name="val">水平偏移位置</param>
        public void setXoffset(int val)
        {
            this._Xoffset = val;
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
        /// 設定字體大小下限
        /// </summary>
        /// <param name="val">下限</param>
        public void setFontMinSize(int val)
        {
            if (val >= _FontMaxSize)
            {
                this._FontMinSize = _FontMaxSize;
            }
            else
            {
                this._FontMinSize = val;
            }
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
