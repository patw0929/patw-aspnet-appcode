namespace tw.patw.FileUploader
{
    using Microsoft.VisualBasic;
    using System;
    using System.IO;
    using System.Web;
    using System.Web.UI.WebControls;

    public class FileUploader
    {
        public static bool DelFile(string strFile)
        {
            if (File.Exists(strFile))
            {
                File.Delete(strFile);
                return true;
            }
            return false;
        }

        public static string GetExt(FileUpload FileUpload)
        {
            if (!((FileUpload != null) && FileUpload.HasFile))
            {
                throw new Exception("未選擇上傳檔案");
            }
            return Path.GetExtension(FileUpload.FileName).ToLower();
        }

        public static SaveFileResult SaveFile(string strAcct, FileUpload FileUpload, string SaveFilePath, int MaxKBSize, params string[] Extensions)
        {
            SaveFileResult result = null;
            result = new SaveFileResult();
            if ((FileUpload != null) && FileUpload.HasFile)
            {
                if (string.IsNullOrEmpty((SaveFilePath ?? "").Trim()))
                {
                    result.Msg = "未設定儲存路徑";
                    return result;
                }
                if ((MaxKBSize > 0) && (FileUpload.PostedFile.ContentLength > (MaxKBSize * 0x400)))
                {
                    result.Msg = "超出大小限制";
                    return result;
                }
                if (Extensions.Length > 0)
                {
                    bool flag = false;
                    foreach (string str in Extensions)
                    {
                        if (Path.GetExtension(FileUpload.FileName).ToLower() == ("." + str.ToLower()))
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        result.Msg = "不是允許的副檔名";
                        return result;
                    }
                }
                if (Strings.Right(SaveFilePath, 1) != "/")
                {
                    SaveFilePath = SaveFilePath + "/";
                }
                SetFolder(SaveFilePath);
                string str2 = strAcct + DateTime.Now.ToString("yyMMddHHmmssfff") + Path.GetExtension(FileUpload.FileName);
                string filename = HttpContext.Current.Server.MapPath(SaveFilePath + str2);
                FileUpload.SaveAs(filename);
                FileUpload.Dispose();
                result.Result = true;
                result.Msg = SaveFilePath + str2;
            }
            return result;
        }

        public static SaveFileResult SaveFile(FileUpload FileUpload, string filename, string SaveFilePath, int MaxKBSize, params string[] Extensions)
        {
            SaveFileResult result = null;
            result = new SaveFileResult();
            if ((FileUpload != null) && FileUpload.HasFile)
            {
                if (string.IsNullOrEmpty((SaveFilePath ?? "").Trim()))
                {
                    result.Msg = "未設定儲存路徑";
                    return result;
                }
                if ((MaxKBSize > 0) && (FileUpload.PostedFile.ContentLength > (MaxKBSize * 0x400)))
                {
                    result.Msg = "超出大小限制";
                    return result;
                }
                if (Extensions.Length > 0)
                {
                    bool flag = false;
                    foreach (string str in Extensions)
                    {
                        if (Path.GetExtension(FileUpload.FileName).ToLower() == ("." + str.ToLower()))
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        result.Msg = "不是允許的副檔名";
                        return result;
                    }
                }
                if (Strings.Right(SaveFilePath, 1) != "/")
                {
                    SaveFilePath = SaveFilePath + "/";
                }
                SetFolder(SaveFilePath);
                string str2 = filename + Path.GetExtension(FileUpload.FileName);
                string str3 = HttpContext.Current.Server.MapPath(SaveFilePath + str2);
                FileUpload.SaveAs(str3);
                FileUpload.Dispose();
                result.Result = true;
                result.Msg = SaveFilePath + str2;
            }
            return result;
        }

        public static void SetFolder(string RelativePath)
        {
            string path = HttpContext.Current.Server.MapPath(RelativePath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static MemoryStream UploadFile(FileUpload FileUpload, int MaxKBSize, params string[] Extensions)
        {
            if (!((FileUpload != null) && FileUpload.HasFile))
            {
                throw new Exception("未選擇上傳檔案");
            }
            if ((MaxKBSize > 0) && (FileUpload.PostedFile.ContentLength > (MaxKBSize * 0x400)))
            {
                throw new Exception("超出大小限制");
            }
            if (Extensions.Length > 0)
            {
                bool flag = false;
                foreach (string str in Extensions)
                {
                    if (Path.GetExtension(FileUpload.FileName).ToLower() == ("." + str.ToLower()))
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    throw new Exception("不是允許的副檔名");
                }
            }
            return new MemoryStream(FileUpload.FileBytes);
        }

        public class SaveFileResult
        {
            public string Msg = "";
            public bool Result = false;
        }
    }
}

