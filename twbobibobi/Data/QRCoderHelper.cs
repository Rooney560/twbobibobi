using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.QrCode.Internal;

namespace WorkTime.data
{
    public class QRCodeHelper
    {
        #region 一維碼 二維碼讀取
        /// <summary>
        /// 一維碼 二維碼讀取
        /// </summary>
        /// <param name="filename">文件路径</param>
        /// <returns></returns>
        static string Read(string filename)
        {
            BarcodeReader reader = new BarcodeReader();
            reader.Options.CharacterSet = "UTF-8";
            Bitmap map = new Bitmap(filename);
            Result result = reader.Decode(map);
            return result == null ? "" : result.Text;
        }
        #endregion

        #region ZXing生成二維碼

        /// <summary>
        /// 生成二維碼
        /// </summary>
        /// <param name="text">內容</param>
        /// <param name="width">寬度</param>
        /// <param name="height">高度</param>
        /// <returns>圖片</returns>
        public static Bitmap GenerateQRcode(string text, int width, int height)
        {
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            QrCodeEncodingOptions options = new QrCodeEncodingOptions()
            {
                DisableECI = true,//設置內容編碼
                CharacterSet = "UTF-8",  //設置二維碼的寬度和高度
                Width = width,
                Height = height,
                Margin = 0,//設置二維碼的邊距,單位不是固定像素
                ErrorCorrection = ErrorCorrectionLevel.H
            };

            writer.Options = options;
            Bitmap map = writer.Write(text);
            return map;
        }

        /// <summary>
        /// 生成二維碼
        /// </summary>
        /// <param name="text">內容</param>
        /// <param name="width">寬度</param>
        /// <param name="height">高度</param>
        /// <returns>圖片</returns>
        public static Bitmap GenerateQRcode2(string text, int width, int height)
        {
            // 1. 產生最小化、無 margin 的 PixelData（只包含 QR 矩陣本體）
            var pixelWriter = new BarcodeWriterPixelData
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    DisableECI = true,
                    CharacterSet = "UTF-8",
                    ErrorCorrection = ErrorCorrectionLevel.H,
                    Margin = 0   // 不留規範中的 Quiet Zone
                }
            };
            var pixelData = pixelWriter.Write(text);

            // 2. 將 PixelData 轉成「原始大小」Bitmap
            using (var rawBmp = new Bitmap(pixelData.Width, pixelData.Height, PixelFormat.Format32bppRgb))
            {
                var bmpData = rawBmp.LockBits(
                    new Rectangle(0, 0, rawBmp.Width, rawBmp.Height),
                    ImageLockMode.WriteOnly,
                    rawBmp.PixelFormat);

                // PixelData.Pixels 是一維 byte[]：BGRA 排序
                System.Runtime.InteropServices.Marshal.Copy(
                    pixelData.Pixels, 0, bmpData.Scan0, pixelData.Pixels.Length);
                rawBmp.UnlockBits(bmpData);

                // 3. 用 Nearest-Neighbor 插值，將它精確拉到 width×height
                var finalBmp = new Bitmap(width, height);
                using (var g = Graphics.FromImage(finalBmp))
                {
                    g.InterpolationMode = InterpolationMode.NearestNeighbor;
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.SmoothingMode = SmoothingMode.None;

                    // 原圖 (rawBmp) → 目標 (finalBmp)
                    g.DrawImage(
                        rawBmp,
                        new Rectangle(0, 0, width, height),
                        new Rectangle(0, 0, rawBmp.Width, rawBmp.Height),
                        GraphicsUnit.Pixel);
                }

                return finalBmp;
            }
        }

        /// <summary>
        /// 生成二維碼
        /// </summary>
        /// <param name="text">內容</param>
        /// <param name="width">寬度</param>
        /// <param name="height">高度</param>
        /// <param name="ServerPth">儲存路徑 @"H:\桌面\截圖\"</param>
        /// <returns>圖片檔名</returns>
        public string GenerateQRcode(string text, int width, int height, string ServerPth, ImageFormat imagesFrt)
        {
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            QrCodeEncodingOptions options = new QrCodeEncodingOptions()
            {
                DisableECI = true,//設置內容編碼
                CharacterSet = "UTF-8",  //設置二維碼的寬度和高度
                Width = width,
                Height = height,
                Margin = 0,//設置二維碼的邊距,單位不是固定像素
                ErrorCorrection = ErrorCorrectionLevel.H
            };

            writer.Options = options;
            Bitmap map = writer.Write(text);
            string imagesname = text + "." + imagesFrt;
            string filename = ServerPth + imagesname.ToLower();
            //請注意，保存格式和文件檔名要一致
            map.Save(filename, imagesFrt);
            return imagesname.ToLower();
        }
        #endregion

        #region ZXing生成一維條形碼
        /// <summary>
        /// 生成一維條形碼
        /// </summary>
        /// <param name="text">只支援數字 只支援偶數個 最大長度80</param>
        /// <param name="width">寬度</param>
        /// <param name="height">高度</param>
        /// <returns></returns>
        public static Bitmap Generate1DBarcode(string text, int width, int height)
        {
            BarcodeWriter writer = new BarcodeWriter();
            //使用ITF 格式，不能被現在常用的支付寶、微信掃出來
            //如果想生成可識別的可以使用 CODE_128 格式
            //writer.Format = BarcodeFormat.ITF;
            writer.Format = BarcodeFormat.CODE_128;
            EncodingOptions options = new EncodingOptions()
            {
                Width = width,
                Height = height,
                Margin = 2
            };
            writer.Options = options;
            Bitmap map = writer.Write(text);
            return map;
        }
        /// <summary>
        /// 生成一維條形碼
        /// </summary>
        /// <param name="text">只支援數字 只支援偶數個 最大長度80</param>
        /// <param name="width">寬度</param>
        /// <param name="height">高度</param>
        /// <param name="ServerPth">儲存路徑 @"H:\桌面\截圖\"</param>
        /// <returns>圖片檔名</returns>
        public string Generate1DBarcode(string text, int width, int height, string ServerPth, ImageFormat imagesFrt)
        {
            BarcodeWriter writer = new BarcodeWriter();
            //使用ITF 格式，不能被現在常用的支付寶、微信掃出來
            //如果想生成可識別的可以使用 CODE_128 格式
            //writer.Format = BarcodeFormat.ITF;
            writer.Format = BarcodeFormat.CODE_128;
            EncodingOptions options = new EncodingOptions()
            {
                Width = width,
                Height = height,
                Margin = 2
            };
            writer.Options = options;
            Bitmap map = writer.Write(text);
            string imagesname = text + "." + imagesFrt;
            string filename = ServerPth + imagesname.ToLower();
            //請注意，保存格式和文件檔名要一致
            map.Save(filename, imagesFrt);
            return imagesname.ToLower();
        }
        /// <summary>
        /// 生成一維條形碼
        /// </summary>
        /// <param name="text">只支援數字 只支援偶數個 最大長度80</param>
        /// <param name="width">寬度</param>
        /// <param name="height">高度</param>
        /// <returns></returns>
        public static Bitmap GenerateCode39(string text, int width, int height)
        {
            BarcodeWriter writer = new BarcodeWriter();
            //使用ITF 格式，不能被現在常用的支付寶、微信掃出來
            //如果想生成可識別的可以使用 CODE_128 格式
            //writer.Format = BarcodeFormat.ITF;
            writer.Format = BarcodeFormat.CODE_39;
            EncodingOptions options = new EncodingOptions()
            {
                Width = width,
                Height = height,
                Margin = 0,
                PureBarcode = true    // 不產生下方 human‐readable 文字
            };
            writer.Options = options;
            Bitmap map = writer.Write(text);
            return map;
        }

        #endregion

        #region ZXing生成Logo二維碼
        /// <summary>
        /// 生成帶Logo的二維碼
        /// </summary>
        /// <param name="text">內容</param>
        /// <param name="width">寬度</param>
        /// <param name="height">高度</param>
        /// <param name="logoname">logo名稱</param>
        /// <returns>圖片</returns>
        public Bitmap GenerateLogoQRcode(string text, int width, int height, string logoname)
        {
            //Logo 圖片
            string logoPath = System.AppDomain.CurrentDomain.BaseDirectory + @"\images\" + logoname + ".png";
            Bitmap logo = new Bitmap(logoPath);
            //構造二維碼寫碼器
            MultiFormatWriter writer = new MultiFormatWriter();
            Dictionary<EncodeHintType, object> hint = new Dictionary<EncodeHintType, object>();
            hint.Add(EncodeHintType.CHARACTER_SET, "UTF-8");
            hint.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);
            hint.Add(EncodeHintType.MARGIN, 0);//舊版本不起作用，需要手動去除白邊

            //生成二維碼 
            BitMatrix bm = writer.encode(text, BarcodeFormat.QR_CODE, width, height, hint);
            //bm = deleteWhite(bm);
            BarcodeWriter barcodeWriter = new BarcodeWriter();
            Bitmap map = barcodeWriter.Write(bm);

            //獲取二維碼實際尺寸（去掉二維碼兩邊空白後的實際尺寸）
            int[] rectangle = bm.getEnclosingRectangle();

            //計算插入圖片的大小和位置
            int middleW = Math.Min((int)(rectangle[2] / 3), logo.Width);
            int middleH = Math.Min((int)(rectangle[3] / 3), logo.Height);
            int middleL = (map.Width - middleW) / 2;
            int middleT = (map.Height - middleH) / 2;

            Bitmap bmpimages = new Bitmap(map.Width, map.Height, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(bmpimages))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.DrawImage(map, 0, 0, width, height);
                //白底將二維碼插入圖片
                g.FillRectangle(Brushes.White, middleL, middleT, middleW, middleH);
                g.DrawImage(logo, middleL, middleT, middleW, middleH);
            }
            return bmpimages;
        }
        /// <summary>
        /// 生成帶Logo的二維碼
        /// </summary>
        /// <param name="text">內容</param>
        /// <param name="width">寬度</param>
        /// <param name="height">高度</param>
        /// <param name="logoname">logo名稱</param>
        /// <param name="ServerPth">儲存路徑 @"H:\桌面\截圖\"</param>
        /// <returns>圖片檔名</returns>
        public string GenerateLogoQRcode(string text, int width, int height, string logoname, string ServerPth, ImageFormat imagesFrt)
        {
            //Logo 圖片
            string logoPath = System.AppDomain.CurrentDomain.BaseDirectory + @"\images\" + logoname + ".png";
            Bitmap logo = new Bitmap(logoPath);
            //構造二維碼寫碼器
            MultiFormatWriter writer = new MultiFormatWriter();
            Dictionary<EncodeHintType, object> hint = new Dictionary<EncodeHintType, object>();
            hint.Add(EncodeHintType.CHARACTER_SET, "UTF-8");
            hint.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);
            hint.Add(EncodeHintType.MARGIN, 0);//舊版本不起作用，需要手動去除白邊

            //生成二維碼 
            BitMatrix bm = writer.encode(text, BarcodeFormat.QR_CODE, width, height, hint);
            //bm = deleteWhite(bm);
            BarcodeWriter barcodeWriter = new BarcodeWriter();
            Bitmap map = barcodeWriter.Write(bm);

            //獲取二維碼實際尺寸（去掉二維碼兩邊空白後的實際尺寸）
            int[] rectangle = bm.getEnclosingRectangle();

            //計算插入圖片的大小和位置
            int middleW = Math.Min((int)(rectangle[2] / 3), logo.Width);
            int middleH = Math.Min((int)(rectangle[3] / 3), logo.Height);
            int middleL = (map.Width - middleW) / 2;
            int middleT = (map.Height - middleH) / 2;

            Bitmap bmpimages = new Bitmap(map.Width, map.Height, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(bmpimages))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.DrawImage(map, 0, 0, width, height);
                //白底將二維碼插入圖片
                Brush b = new SolidBrush(Color.FromArgb(0, Color.Green));
                g.FillRectangle(b, middleL, middleT, middleW, middleH);
                g.DrawImage(logo, middleL, middleT, middleW, middleH);
            }
            string imagesname = text + "." + imagesFrt;
            string filename = ServerPth + imagesname.ToLower();
            //請注意，保存格式和文件檔名要一致
            bmpimages.Save(filename, imagesFrt);
            return imagesname.ToLower();
        }
        #endregion

        #region ZXing生成特定顏色二維碼
        /// <summary>
        /// 生成特定顏色二維碼
        /// </summary>
        /// <param name="msg">內容</param>
        /// <param name="codeSizeInPixels">正方形 邊長</param>
        /// <param name="backgroundcolor">背景顏色</param>
        /// <param name="foregroundcolor">點點顏色</param>
        /// <returns>圖片</returns>
        public Bitmap GenerateColorQRcode(string msg, int codeSizeInPixels, Color backgroundcolor, Color foregroundcolor)
        {
            BarcodeWriter writer = new BarcodeWriter();
            writer.Renderer = new ZXing.Rendering.BitmapRenderer { Background = backgroundcolor, Foreground = foregroundcolor };
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options.Hints.Add(EncodeHintType.CHARACTER_SET, "UTF-8");//編碼問題
            writer.Options.Hints.Add(EncodeHintType.ERROR_CORRECTION, ZXing.QrCode.Internal.ErrorCorrectionLevel.H);

            writer.Options.Height = writer.Options.Width = codeSizeInPixels;
            writer.Options.Margin = 0;//設置邊框1            ZXing.Common.BitMatrix bm = writer.Encode(msg);
            Bitmap images = writer.Write(msg);
            return images;
        }
        /// <summary>
        /// 生成特定顏色二維碼
        /// </summary>
        /// <param name="msg">內容</param>
        /// <param name="codeSizeInPixels">正方形 邊長</param>
        /// <param name="backgroundcolor">背景顏色</param>
        /// <param name="foregroundcolor">點點顏色</param>
        /// <returns>圖片</returns>
        public string GenerateColorQRcode(string msg, int codeSizeInPixels, Color backgroundcolor, Color foregroundcolor, string ServerPth, ImageFormat imagesFrt)
        {
            BarcodeWriter writer = new BarcodeWriter();
            writer.Renderer = new ZXing.Rendering.BitmapRenderer { Background = backgroundcolor, Foreground = foregroundcolor };
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options.Hints.Add(EncodeHintType.CHARACTER_SET, "UTF-8");//編碼問題
            writer.Options.Hints.Add(EncodeHintType.ERROR_CORRECTION, ZXing.QrCode.Internal.ErrorCorrectionLevel.H);

            writer.Options.Height = writer.Options.Width = codeSizeInPixels;
            writer.Options.Margin = 0;//設置邊框1            ZXing.Common.BitMatrix bm = writer.Encode(msg);
            Bitmap images = writer.Write(msg);
            string imagesname = msg + "." + imagesFrt;
            string filename = ServerPth + imagesname.ToLower();
            //請注意，保存格式和文件檔名要一致
            images.Save(filename, imagesFrt);
            return imagesname.ToLower();
        }
        #endregion

        #region ZXing生成特定顏色Logo二維碼
        /// <summary>
        /// 生成特定顏色二維碼
        /// </summary>
        /// <param name="text">text=內容</param>
        /// <param name="filename">filename=檔案名稱</param>
        /// <param name="codeSizeInPixels">codeSizeInPixels=正方形 邊長</param>
        /// <param name="logoname">logoname=logo名稱</param>
        /// <param name="backgroundcolor">backgroundcolor=背景顏色</param>
        /// <param name="foregroundcolor">foregroundcolor=點點顏色</param>
        public Bitmap GenerateColorLogoQRcode(string text, string filename, int codeSizeInPixels, string logoname, Color backgroundcolor, Color foregroundcolor)
        {
            //Logo 圖片
            string logoPath = System.AppDomain.CurrentDomain.BaseDirectory + @"\images\" + logoname + ".png";
            Bitmap logo = new Bitmap(logoPath);

            //構造二維碼寫碼器
            MultiFormatWriter writer = new MultiFormatWriter();
            Dictionary<EncodeHintType, object> hint = new Dictionary<EncodeHintType, object>();
            hint.Add(EncodeHintType.CHARACTER_SET, "UTF-8");
            hint.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);
            hint.Add(EncodeHintType.MARGIN, 0);

            //生成二維碼 
            BitMatrix bm = writer.encode(text, BarcodeFormat.QR_CODE, codeSizeInPixels, codeSizeInPixels, hint);
            //bm = deleteWhite(bm);
            BarcodeWriter barcodeWriter = new BarcodeWriter();
            barcodeWriter.Renderer = new ZXing.Rendering.BitmapRenderer { Background = backgroundcolor, Foreground = foregroundcolor };
            Bitmap map = barcodeWriter.Write(bm);

            //獲取二維碼實際尺寸（去掉二維碼兩邊空白後的實際尺寸）
            int[] rectangle = bm.getEnclosingRectangle();

            //計算插入圖片的大小和位置
            int middleW = Math.Min((int)(rectangle[2] / 2), logo.Width);
            int middleH = Math.Min((int)(rectangle[3] / 2), logo.Height);
            int middleL = (map.Width - middleW) / 2;
            int middleT = (map.Height - middleH) / 2;

            Bitmap bmpimages = new Bitmap(map.Width, map.Height, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(bmpimages))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.DrawImage(map, 0, 0, codeSizeInPixels, codeSizeInPixels);
                //白底將二維碼插入圖片
                Brush b = new SolidBrush(Color.FromArgb(0, Color.Green));
                g.FillRectangle(b, middleL, middleT, middleW, middleH);
                g.DrawImage(logo, middleL, middleT, middleW, middleH);
            }
            return bmpimages;
        }
        /// <summary>
        /// 生成特定顏色二維碼
        /// </summary>
        /// <param name="text">text=內容</param>
        /// <param name="filename">filename=檔名</param>
        /// <param name="codeSizeInPixels">codeSizeInPixels=正方形 邊長</param>
        /// <param name="backgroundcolor">backgroundcolor=背景顏色</param>
        /// <param name="foregroundcolor">foregroundcolor=點點顏色</param>
        /// <param name="ServerPth">ServerPth=儲存位置</param>
        /// <param name="imagesFrt">imagesFrt=副檔名/格式</param>
        /// <returns>圖片</returns>
        public string GenerateColorLogoQRcode(string text,string filename, int codeSizeInPixels, string logoname, Color backgroundcolor, Color foregroundcolor, string ServerPth, ImageFormat imagesFrt)
        {
            //Logo 圖片
            string logoPath = System.AppDomain.CurrentDomain.BaseDirectory + @"\images\" + logoname + ".png";
            Bitmap logo = new Bitmap(logoPath);

            //構造二維碼寫碼器
            MultiFormatWriter writer = new MultiFormatWriter();
            Dictionary<EncodeHintType, object> hint = new Dictionary<EncodeHintType, object>();
            hint.Add(EncodeHintType.CHARACTER_SET, "UTF-8");
            hint.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);
            hint.Add(EncodeHintType.MARGIN, 0);

            //生成二維碼 
            BitMatrix bm = writer.encode(text, BarcodeFormat.QR_CODE, codeSizeInPixels, codeSizeInPixels, hint);
            //bm = deleteWhite(bm);
            BarcodeWriter barcodeWriter = new BarcodeWriter();
            barcodeWriter.Renderer = new ZXing.Rendering.BitmapRenderer { Background = backgroundcolor, Foreground = foregroundcolor };
            Bitmap map = barcodeWriter.Write(bm);

            //獲取二維碼實際尺寸（去掉二維碼兩邊空白後的實際尺寸）
            int[] rectangle = bm.getEnclosingRectangle();

            //計算插入圖片的大小和位置
            int middleW = Math.Min((int)(rectangle[2] / 3), logo.Width);
            int middleH = Math.Min((int)(rectangle[3] / 3), logo.Height);
            int middleL = (map.Width - middleW) / 2;
            int middleT = (map.Height - middleH) / 2;

            Bitmap bmpimages = new Bitmap(map.Width, map.Height, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(bmpimages))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.DrawImage(map, 0, 0, codeSizeInPixels, codeSizeInPixels);
                //白底將二維碼插入圖片
                Brush b = new SolidBrush(Color.FromArgb(10, Color.White));
                g.FillRectangle(b, middleL, middleT, middleW, middleH);
                g.DrawImage(logo, middleL, middleT, middleW, middleH);
            }
            string imagesname = filename + "." + imagesFrt;
            string path = ServerPth + imagesname.ToLower();
            //請注意，保存格式和文件檔名要一致
            bmpimages.Save(path, imagesFrt);

            bmpimages.Dispose();
            logo.Dispose();
            map.Dispose();
            return imagesname.ToLower();
        }
        #endregion
        
        public static Bitmap GenerateLogo(string text, Color backgroundcolor, Color foregroundcolor)
        {
            //Logo 图片
            string logoPath = System.AppDomain.CurrentDomain.BaseDirectory + @"\images\logo.png";
            Bitmap logo = new Bitmap(logoPath);
            //构造二维码写码器
            MultiFormatWriter writer = new MultiFormatWriter();
            Dictionary<EncodeHintType, object> hint = new Dictionary<EncodeHintType, object>();
            hint.Add(EncodeHintType.CHARACTER_SET, "UTF-8");
            hint.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);

            //生成二维码 
            BitMatrix bm = writer.encode(text, BarcodeFormat.QR_CODE, 300, 300, hint);
            BarcodeWriter barcodeWriter = new BarcodeWriter();
            barcodeWriter.Renderer = new ZXing.Rendering.BitmapRenderer { Background = backgroundcolor, Foreground = foregroundcolor };
            Bitmap map = barcodeWriter.Write(bm);


            //获取二维码实际尺寸（去掉二维码两边空白后的实际尺寸）
            int[] rectangle = bm.getEnclosingRectangle();

            //计算插入图片的大小和位置
            int middleW = Math.Min((int)(rectangle[2] / 3), logo.Width);
            int middleH = Math.Min((int)(rectangle[3] / 3), logo.Height);
            int middleL = (map.Width - middleW) / 2;
            int middleT = (map.Height - middleH) / 2;

            //将img转换成bmp格式，否则后面无法创建Graphics对象
            Bitmap bmpimg = new Bitmap(map.Width, map.Height, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(bmpimg))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.DrawImage(map, 0, 0);
            }
            //将二维码插入图片
            Graphics myGraphic = Graphics.FromImage(bmpimg);
            //白底
            myGraphic.FillRectangle(Brushes.White, middleL, middleT, middleW, middleH);
            myGraphic.DrawImage(logo, middleL, middleT, middleW, middleH);

            //保存成图片
            return bmpimg;
        }
    }
}