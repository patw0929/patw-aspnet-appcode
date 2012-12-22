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
    public class DrawTextAutoBreakline : IDecorator
    {
        Image image;
        private string _text;
        private int _fontsize;
        public Color _fontcolor = Color.Black; // 文字顏色
        private int _x = 0;
        private int _y = 0;
        private int _width = 0;
        private int _height = 0;

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="image">圖片</param>
        /// <param name="text">文字</param>
        /// <param name="fontsize">字體大小</param>
        public DrawTextAutoBreakline(Image image, string text, int fontsize, Color fontcolor)
        {
            this._text = text;
            this._fontsize = fontsize;
            this._fontcolor = fontcolor;
            this.image = image;
        }

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="image">圖片</param>
        /// <param name="text">文字</param>
        /// <param name="x">X軸座標</param>
        /// <param name="y">Y軸座標</param>
        public DrawTextAutoBreakline(Image image, string text, int fontsize, Color fontcolor, int x, int y, int width, int height)
        {
            this._text = text;
            this._fontsize = fontsize;
            this._fontcolor = fontcolor;
            this.image = image;
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
            //StringFormat strFormat = new StringFormat();
            //strFormat.Alignment = StringAlignment.Center;
            //strFormat.LineAlignment = StringAlignment.Center;
            //g.DrawString(_text, new Font("微軟正黑體", _fontsize), new SolidBrush(_fontcolor), _mask_pos_x, _mask_pos_y);

            Font font = new Font("微軟正黑體", _fontsize);
            List<string> textRows = GetStringRows(g, font, _text, _width);
            int rowHeight = (int)(Math.Ceiling(g.MeasureString("測試", font).Height));

            int maxRowCount = _height / rowHeight;
            int drawRowCount = (maxRowCount < textRows.Count) ? maxRowCount : textRows.Count;
            int top = (_height - rowHeight * drawRowCount) / 2;
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Center;
            for (int i = 0; i < drawRowCount; i++)
            {
                Rectangle fontRectanle = new Rectangle(_x, top + rowHeight * i, _width, rowHeight);
                g.DrawString(textRows[i], font, new SolidBrush(_fontcolor), fontRectanle, sf);
            }


            return image;
        }

        /// <summary>
        /// 存檔方法
        /// </summary>
        public void Save(string file, string path)
        {
            string fOutput = System.IO.Path.Combine(path, file);
            image.Save(fOutput);
        }

        /// <summary>
        /// 將文本分行
        /// </summary>
        /// <param name="graphic">繪圖圖面</param>
        /// <param name="font">字體</param>
        /// <param name="text">文本</param>
        /// <param name="width">行寬</param>
        /// <returns></returns>
        private List<string> GetStringRows(Graphics graphic, Font font, string text, int width)
        {
            int RowBeginIndex = 0;
            int rowEndIndex = 0;
            int textLength = text.Length;
            List<string> textRows = new List<string>();
            for (int index = 0; index < textLength; index++)
            {
                rowEndIndex = index;
                if (index == textLength - 1)
                {
                    textRows.Add(text.Substring(RowBeginIndex));
                }
                else if (rowEndIndex + 1 < text.Length && text.Substring(rowEndIndex, 2) == "\r\n")
                {
                    textRows.Add(text.Substring(RowBeginIndex, rowEndIndex - RowBeginIndex));
                    rowEndIndex = index += 2;
                    RowBeginIndex = rowEndIndex;
                }
                else if (graphic.MeasureString(text.Substring(RowBeginIndex, rowEndIndex - RowBeginIndex + 1), font).Width > width)
                {
                    textRows.Add(text.Substring(RowBeginIndex, rowEndIndex - RowBeginIndex));
                    RowBeginIndex = rowEndIndex;
                }
            }
            return textRows;
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
