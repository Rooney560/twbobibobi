using System;
using System.IO;
using System.Net;
using System.Net.Mime;
using System.Net.Mail;
using System.Drawing.Imaging;
using System.Drawing;
using WorkTime.data;

namespace twbobibobi.Helpers
{
    /// <summary>
    /// 處理電子郵件中影像相關的功能，例如：產生條碼、QR code，或將圖檔轉成 LinkedResource
    /// </summary>
    public class EmailImageHelper
    {
        /// <summary>
        /// 產生 Code39 條碼的 PNG bytes
        /// </summary>
        /// <param name="content">條碼內容</param>
        /// <param name="width">圖寬度</param>
        /// <param name="height">圖高度</param>
        public static byte[] GetBarcode(string content, int width, int height)
        {
            // 寫出 Bitmap
            Bitmap bmp = QRCodeHelper.GenerateCode39(content, width, height);

            // 存成 MemoryStream
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Png);

            // 釋放資源
            bmp.Dispose();

            byte[] bytes = ms.ToArray();
            ms.Dispose();
            return bytes;
        }

        /// <summary>
        /// 產生 QR Code 的 PNG bytes
        /// </summary>
        public static byte[] GetQRcode(string content, int width, int height)
        {
            // 寫出 Bitmap
            Bitmap bmp = QRCodeHelper.GenerateQRcode2(content, width, height);

            // 存成 MemoryStream
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Png);

            // 釋放資源
            bmp.Dispose();

            byte[] bytes = ms.ToArray();
            ms.Dispose();
            return bytes;
        }

        /// <summary>
        /// 將二進位圖檔轉為 Base64 Data URI 格式，方便嵌入 HTML
        /// </summary>
        public static string ToBase64DataUri(byte[] bytes, string mediaType)
        {
            var base64 = Convert.ToBase64String(bytes);
            return $"data:{mediaType};base64,{base64}";
        }

        /// <summary>
        /// 下載指定 URL 的圖片，並封裝成 Embedded LinkedResource (CID)。
        /// </summary>
        public static LinkedResource DownloadImageAsLinkedResource(string imageUrl, string contentId, string mediaType)
        {
            // 1. 用 WebClient 下載圖片 bytes
            byte[] imageBytes;
            using (var wc = new WebClient())
            {
                imageBytes = wc.DownloadData(imageUrl);
            }

            // 2. 把 bytes 放到 MemoryStream
            var ms = new MemoryStream(imageBytes);

            // 3. 建 LinkedResource
            var res = new LinkedResource(ms, mediaType);
            res.ContentId = contentId;
            res.TransferEncoding = TransferEncoding.Base64;
            // 設定檔名副檔名(非必要，但有助於 MailClient 處理)
            res.ContentType.Name = contentId + GetExtension(mediaType);

            return res;
        }

        /// <summary>
        /// 將Base64的圖片，並封裝成 Embedded LinkedResource (CID)。
        /// </summary>
        public static LinkedResource Base64ImageAsLinkedResource(string Base64str, string contentId, string mediaType = "image/png")
        {
            // 產生 LinkedResource// 假设 barDataUri = "data:image/png;base64,iVBORw0KGgoAAAANSUhE…"
            // 先把前缀移除
            //var prefix = "data:image/png;base64,";
            //var base64 = Base64str.StartsWith(prefix)
            //    ? Base64str.Substring(prefix.Length)
            //    : Base64str;
            var parts = Base64str.Split(new[] { "," }, 2, StringSplitOptions.None);
            var meta = parts[0];
            var base64 = parts[1];
            var base64mediaType = meta.Substring(5, meta.IndexOf(';') - 5);

            // 再把可能的换行或空白都去掉
            base64 = base64.Trim()
                           .Replace("\r", "")
                           .Replace("\n", "")
                           .Replace(" ", "");

            // 现在就可以安全解码了
            var imageBytes = Convert.FromBase64String(base64);

            // 把 bytes 放到 MemoryStream
            var ms = new MemoryStream(imageBytes);

            // 建 LinkedResource
            var res = new LinkedResource(ms, mediaType);
            res.ContentId = contentId;
            res.TransferEncoding = TransferEncoding.Base64;
            // 設定檔名副檔名(非必要，但有助於 MailClient 處理)
            res.ContentType.Name = contentId + GetExtension(mediaType);

            return res;
        }


        /// <summary>
        /// 將Base64的圖片，並封裝成 Embedded LinkedResource (CID)。
        /// </summary>
        public static LinkedResource QRCodeImageAsLinkedResource(string QRCodestr, string contentId, string mediaType)
        {
            var qrBase64 = QRCodestr.Substring(QRCodestr.IndexOf(",") + 1)
                         .Trim()
                         .Replace("\r", "").Replace("\n", "").Replace(" ", "");
            var qrBytes = Convert.FromBase64String(qrBase64);

            // 把 bytes 放到 MemoryStream
            var ms = new MemoryStream(qrBytes);

            // 建 LinkedResource
            var res = new LinkedResource(ms, mediaType);
            res.ContentId = contentId;
            res.TransferEncoding = TransferEncoding.Base64;
            // 設定檔名副檔名(非必要，但有助於 MailClient 處理)
            res.ContentType.Name = contentId + GetExtension(mediaType);

            return res;
        }

        /// <summary>
        /// 回傳對應 mediaType 的副檔名
        /// </summary>
        private static string GetExtension(string mediaType)
        {
            switch (mediaType)
            {
                case "image/jpeg":
                    return ".jpg";
                case "image/png":
                    return ".png";
                case "image/gif":
                    return ".gif";
                default:
                    return "";
            }
        }

    }
}