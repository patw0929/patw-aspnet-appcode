using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace tw.patw.ImageResizer
{
    class Functions
    {
        /// <summary>
        /// 將檔案轉換為 Byte
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static byte[] ReadFile(string path)
        {

            byte[] data = null;

            FileInfo fInfo = new FileInfo(path);

            long length = fInfo.Length;

            FileStream fStream = new FileStream(path, FileMode.Open, FileAccess.Read);

            using (BinaryReader br = new BinaryReader(fStream))
            {
                data = br.ReadBytes((int)length);
                br.Close();
                br.Dispose();
            }
            return data;


        }

    }
}