using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Web;

namespace tw.patw.ImageResizer
{
    public class DrawFBPic : IDecorator
    {
        Image image;
        private string _FBUID;
        private int _size;

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="FBUID">FBUID</param>
        /// <param name="size">圖片大小</param>
        public DrawFBPic(string FBUID, int size)
        {
            this._FBUID = FBUID;
            this._size = size;
        }



        /// <summary>
        /// 主要作業方法
        /// </summary>
        public Image Operation()
        {
            System.Drawing.Image img = GenData(string.Format("https://graph.facebook.com/{0}/picture?type=large", this._FBUID));

            if (img.Width > img.Height)
            {
                int width = (int)(img.Width * this._size / img.Height);
                DrawStaticResize DS = new DrawStaticResize(img, width, this._size);
                img = DS.Operation();

                DrawCrop DC = new DrawCrop(img, (int)((width - this._size) / 2), 0, _size, _size);
                img = DC.Operation();
            }
            else
            {
                int height = (int)(img.Height * this._size / img.Width);
                DrawStaticResize DS = new DrawStaticResize(img, this._size, (int)(img.Height * this._size / img.Width));
                img = DS.Operation();

                DrawCrop DC = new DrawCrop(img, 0, (int)((height - this._size) / 2), this._size, this._size);
                img = DC.Operation();
            }
            image = img;
            return image;
        }


        private System.Drawing.Image GenData(string sImgUrl)
        {
            //建立瀏覽物件
            System.Net.WebClient webClient = new System.Net.WebClient();
            //取得目標網址圖片的Byte陣列 
            byte[] bImage = webClient.DownloadData(sImgUrl);
            //轉換為Strema 
            System.IO.MemoryStream oMemoryStream = new System.IO.MemoryStream(bImage);
            //設定資料流位置 
            oMemoryStream.Position = 0;
            //將Stream轉為Image物件 
            System.Drawing.Image oImage = System.Drawing.Image.FromStream(oMemoryStream);
            return oImage;
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
