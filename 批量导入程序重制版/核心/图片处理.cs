using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Spire.Pdf;
using Spire.Pdf.Graphics;

namespace 批量导入程序重制版
{
    /// <summary>
    /// 图片处理后台：对应旧程序的图片下载与处理逻辑。
    /// 支持 FTP / HTTP(S) / BASE64 下载，以及 PDF 转图片。
    /// </summary>
    public static class 图片处理
    {
        /// <summary>
        /// 下载 FTP 文件（支持匿名与账号密码）
        /// </summary>
        public static void 下载FTP文件(string ftpUrl, string localPath, string user, string password)
        {
            var request = (FtpWebRequest)WebRequest.Create(ftpUrl);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.UseBinary = true;
            if (!string.IsNullOrEmpty(user))
                request.Credentials = new NetworkCredential(user, password ?? string.Empty);

            using (var response = (FtpWebResponse)request.GetResponse())
            using (var stream = response.GetResponseStream())
            using (var file = new FileStream(localPath, FileMode.Create, FileAccess.Write))
            {
                stream.CopyTo(file);
            }
        }

        /// <summary>
        /// 下载 HTTP(S) 文件
        /// </summary>
        public static void 下载HTTP文件(string url, string localPath)
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(url, localPath);
            }
        }

        /// <summary>
        /// 异步下载 HTTP(S) 文件
        /// </summary>
        public static async Task 下载HTTP文件异步(string url, string localPath)
        {
            using (var client = new WebClient())
            {
                await client.DownloadFileTaskAsync(url, localPath);
            }
        }

        /// <summary>
        /// 保存 BASE64 图片
        /// </summary>
        public static void 保存BASE64图片(string base64, string localPath)
        {
            if (string.IsNullOrWhiteSpace(base64))
                throw new ArgumentException("BASE64 内容为空。");

            var data = base64.Contains(",") ? base64.Substring(base64.IndexOf(',') + 1) : base64;
            var bytes = Convert.FromBase64String(data);
            var dir = Path.GetDirectoryName(localPath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            File.WriteAllBytes(localPath, bytes);
        }

        /// <summary>
        /// PDF 转图片（逐页输出，清晰度为缩放后的宽高）
        /// </summary>
        public static void PDF转图片(string pdfPath, string imageDir, string imageName, string format, int 清晰度)
        {
            if (!File.Exists(pdfPath))
                throw new FileNotFoundException("PDF 文件不存在。", pdfPath);
            if (!Directory.Exists(imageDir))
                Directory.CreateDirectory(imageDir);

            var ext = "jpg";
            if (!string.IsNullOrEmpty(format))
            {
                var f = format.ToLowerInvariant().TrimStart('.');
                if (f == "png" || f == "bmp" || f == "jpg" || f == "jpeg")
                    ext = f == "jpeg" ? "jpg" : f;
            }

            using (var doc = new PdfDocument())
            {
                doc.LoadFromFile(pdfPath);
                for (int i = 0; i < doc.Pages.Count; i++)
                {
                    using (var image = doc.SaveAsImage(i, PdfImageType.Bitmap, 清晰度, 清晰度))
                    {
                        var path = Path.Combine(imageDir, imageName + "_" + (i + 1) + "." + ext);
                        image.Save(path);
                    }
                }
            }
        }

        /// <summary>
        /// 压缩并旋转图片（对应旧程序 fun处理已下载图像文件 的压缩/旋转部分）
        /// </summary>
        public static void 压缩旋转图片(string sourcePath, string targetPath, int 压缩程度, float 旋转角度)
        {
            using (var source = Image.FromFile(sourcePath))
            {
                var image = source;
                if (旋转角度 != 0)
                {
                    image = new Bitmap(source.Width, source.Height);
                    using (var g = Graphics.FromImage(image))
                    {
                        g.TranslateTransform(source.Width / 2f, source.Height / 2f);
                        g.RotateTransform(旋转角度);
                        g.TranslateTransform(-source.Width / 2f, -source.Height / 2f);
                        g.DrawImage(source, 0, 0);
                    }
                }

                var dir = Path.GetDirectoryName(targetPath);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                image.Save(targetPath);
            }
        }
    }
}
