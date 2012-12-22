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
    /// 加浮水印方法
    /// </summary>
    public class DrawWatermark : IDecorator
    {
        Image image;
        private Image _watermark;
        public enum positionType {LeftTop, LeftBottom, CenterMiddle, RightTop, RightBottom};
        private positionType _position = positionType.RightBottom;
        private int _mask_pos_x = 0; // 水印橫向座標
        private int _mask_pos_y = 0; // 水印縱向座標
        private int _mask_offset_x = 5; // 水印橫向偏移
        private int _mask_offset_y = 5; // 水印縱向偏移
        private Single _Alpha = 0.25F; // 透明度
        private float _Angle = 0;
        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="image">圖片</param>
        /// <param name="watermark">浮水印圖片</param>
        public DrawWatermark(Image image, Image watermark)
        {
            this.image = image;
            this._watermark = watermark;

            _countMaskPos(_position);
        }

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="image">圖片</param>
        /// <param name="watermark">浮水印圖片</param>
        /// <param name="position">位置</param>
        public DrawWatermark(Image image, Image watermark, positionType position)
        {
            this.image = image;
            this._watermark = watermark;
            this._position = position;

            _countMaskPos(_position);
        }

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="image">圖片</param>
        /// <param name="watermark">浮水印圖片</param>
        /// <param name="x">X軸座標</param>
        /// <param name="y">Y軸座標</param>
        public DrawWatermark(Image image, Image watermark, int x, int y)
        {
            this.image = image;
            this._watermark = watermark;
            this._mask_pos_x = x;
            this._mask_pos_y = y;
        }

        /// <summary>
        /// 主要作業方法
        /// </summary>
        /// <returns></returns>
        public Image Operation()
        {
            Graphics g = System.Drawing.Graphics.FromImage(image);

            using (Bitmap TransparentLogo = new Bitmap(_watermark.Width, _watermark.Height))
            {
                Graphics TGraphics = Graphics.FromImage(TransparentLogo);
                ColorMatrix ColorMatrix = new ColorMatrix();
                ColorMatrix.Matrix33 = _Alpha;
                ImageAttributes ImgAttributes = new ImageAttributes();

                ImgAttributes.SetColorMatrix(ColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                TGraphics.DrawImage(_watermark,
                                    new Rectangle(0, 0, TransparentLogo.Width, TransparentLogo.Height),
                                    0, 0, TransparentLogo.Width, TransparentLogo.Height,
                                    GraphicsUnit.Pixel, ImgAttributes);

                TGraphics.Dispose();

                Bitmap RotateLogo = KiRotate(TransparentLogo, _Angle);

                //g.RotateTransform(_Angle);

                g.DrawImage(RotateLogo,
                            _mask_pos_x, _mask_pos_y,
                            _watermark.Width, _watermark.Height
                            );

                g.ResetTransform();
            }
            return image;
        }

        // 旋轉方法
        private Bitmap KiRotate(Bitmap bmp, float angle)
        {
            int w = bmp.Width + 2;
            int h = bmp.Height + 2;

            PixelFormat pf;

            Color bkColor = Color.Transparent;
            pf = PixelFormat.Format32bppArgb;

            Bitmap tmp = new Bitmap(w, h, pf);
            Graphics g = Graphics.FromImage(tmp);
            g.Clear(bkColor);
            g.DrawImageUnscaled(bmp, 1, 1);
            g.Dispose();

            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(new RectangleF(0f, 0f, w, h));
            Matrix mtrx = new Matrix();
            mtrx.Rotate(angle);
            RectangleF rct = path.GetBounds(mtrx);

            Bitmap dst = new Bitmap((int)rct.Width, (int)rct.Height, pf);
            g = Graphics.FromImage(dst);
            g.Clear(bkColor);
            g.TranslateTransform(-rct.X, -rct.Y);
            g.RotateTransform(angle);
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;
            g.DrawImageUnscaled(tmp, 0, 0);
            g.Dispose();

            tmp.Dispose();

            return dst;

        }

        /// <summary>
        /// 計算出座標位置
        /// </summary>
        private void _countMaskPos(positionType position)
        {

                switch(position)
                {
                    case positionType.LeftTop:
                        // 左上
                        _mask_pos_x = _mask_offset_x;
                        _mask_pos_y = _mask_offset_y;
                        break;

                    case positionType.LeftBottom:
                        // 左下
                        _mask_pos_x = _mask_offset_x;
                        _mask_pos_y = image.Height - _watermark.Height - _mask_offset_y;
                        break;

                    case positionType.RightTop:
                        // 右上
                        _mask_pos_x = image.Width - _watermark.Width - _mask_offset_x;
                        _mask_pos_y = _mask_offset_y;
                        break;

                    case positionType.RightBottom:
                        // 右下
                        _mask_pos_x = image.Width - _watermark.Width - _mask_offset_x;
                        _mask_pos_y = image.Height - _watermark.Height - _mask_offset_y;
                        break;

                    case positionType.CenterMiddle:
                        // 置中
                        _mask_pos_x = image.Width / 2 - _watermark.Width / 2;
                        _mask_pos_y = image.Height / 2 - _watermark.Height / 2;
                        break;

                    default:
                        // 預設將水印放到右下,偏移指定像素
                        _mask_pos_x = image.Width - _watermark.Width - _mask_offset_x;
                        _mask_pos_y = image.Height - _watermark.Height - _mask_offset_y;
                        break;
                }
        }
        /// <summary>
        /// 設定透明度
        /// </summary>
        /// <param name="val">設定值(1:不透明)</param>
        public void setAlpha(Single val)
        {
            this._Alpha = val;
        }

        /// <summary>
        /// 設定旋轉角度
        /// </summary>
        /// <param name="val"></param>
        public void setAngle(float val)
        {
            this._Angle = val;
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
