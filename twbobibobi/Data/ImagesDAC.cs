using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Configuration;
using System.Drawing;

namespace twbobibobi.Data
{
    public class ImagesDAC : SqlClientBase
    {
        public string ImageWebHome = WebConfigurationManager.AppSettings["ImageWebHome"];


        public ImagesDAC(BasePage basePage)
            : base(basePage)
        {

        }

        public int AddImage(int meetingID, int addOneID, int buyOneID, int queueID, string ImageType,
           string OriginalImageAddress, string DisplayImageAddress, string ThumbnailImageAddress, int Num, int Status)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            //int imagsType = ImagesTypeToInt(ImageType);

            if (OriginalImageAddress != "")
                OriginalImageAddress = ImageWebHome + OriginalImageAddress;
            if (DisplayImageAddress != "")
                DisplayImageAddress = ImageWebHome + DisplayImageAddress;
            if (ThumbnailImageAddress != "")
                ThumbnailImageAddress = ImageWebHome + ThumbnailImageAddress;

            DataTable dtResource = new DataTable();
            string sql = "Select Top 0 * From Images";
            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.Fill(dtResource);

            DataRow drNewResource = dtResource.NewRow();
            drNewResource["OriginalImageAddress"] = OriginalImageAddress;
            drNewResource["ThumbnailImageAddress"] = ThumbnailImageAddress;
            drNewResource["DisplayImageAddress"] = DisplayImageAddress;
            drNewResource["MeetingID"] = meetingID;
            drNewResource["AddOneID"] = addOneID;
            drNewResource["BuyOneID"] = buyOneID;
            drNewResource["QueueID"] = queueID;
            drNewResource["ImageType"] = ImageType;
            drNewResource["Num"] = Num;
            drNewResource["Status"] = Status;
            drNewResource["CreateDate"] = dtNow;

            dtResource.Rows.Add(drNewResource);
            AdapterObj.Update(dtResource);

            return this.GetIdentity();
        }

        public int UpdateMeetingIDToImages(int meetingID, string ImageType,
           string OriginalImageAddress, string DisplayImageAddress, string ThumbnailImageAddress, int Status)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            int imagsType = ImagesTypeToInt(ImageType);

            if (OriginalImageAddress != "")
                OriginalImageAddress = ImageWebHome + OriginalImageAddress;
            if (DisplayImageAddress != "")
                DisplayImageAddress = ImageWebHome + DisplayImageAddress;
            if (ThumbnailImageAddress != "")
                ThumbnailImageAddress = ImageWebHome + ThumbnailImageAddress;

            DataTable dtData = new DataTable();
            string sql = "Select * From Images Where MeetingID=@MeetingID and ImageType = @ImageType";
            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.AddParameterToSelectCommand("@MeetingID", meetingID);
            AdapterObj.AddParameterToSelectCommand("@ImageType", imagsType);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.Fill(dtData);

            if (dtData.Rows.Count > 0)
            {
                dtData.Rows[0]["OriginalImageAddress"] = OriginalImageAddress;
                dtData.Rows[0]["ThumbnailImageAddress"] = ThumbnailImageAddress;
                dtData.Rows[0]["DisplayImageAddress"] = DisplayImageAddress;
                dtData.Rows[0]["MeetingID"] = meetingID;
                dtData.Rows[0]["ImageType"] = imagsType;
                dtData.Rows[0]["Status"] = Status;
                dtData.Rows[0]["CreateDate"] = dtNow;
                AdapterObj.Update(dtData);
            }
            return int.Parse(dtData.Rows[0]["ImagesID"].ToString());
        }

        private int ImagesTypeToInt(string ImageType)
        {
            int imagetype = 0;
            switch (ImageType)
            {
                case "left":
                    //imagetype = 0;
                    break;
                case "Presentation":
                    //imagetype = 2;
                    break;
                case "Advertising":
                    //imagetype = 3;
                    break;
                case "content":
                    //imagetype = 4;
                    break;
            }
            return imagetype;
        }

        /// <summary>
        /// 缩小图片
        /// </summary>
        /// <param name="strOldPic">源图文件名(包括路径)</param>
        /// <param name="strNewPic">缩小后保存为文件名(包括路径)</param>
        /// <param name="intWidth">缩小至宽度</param>
        /// <param name="intHeight">缩小至高度</param>
        public System.Drawing.Bitmap SmallPic(Bitmap strOldPic, int intWidth, int intHeight)
        {
            Bitmap objNewPic;
            try
            {
                objNewPic = new Bitmap(strOldPic, intWidth, intHeight);
                return objNewPic;
            }
            catch (Exception exp) { throw exp; }
        }

        /// <summary>
        /// 按比例缩小图片，自动计算高度
        /// </summary>
        /// <param name="strOldPic">源图文件名(包括路径)</param>
        /// <param name="strNewPic">缩小后保存为文件名(包括路径)</param>
        /// <param name="intWidth">缩小至宽度</param>
        public Bitmap SmallPic(Bitmap strOldPic, int intWidth)
        {

            Bitmap objNewPic;
            try
            {
                int intHeight = (int)(((intWidth * 1.0000) / strOldPic.Width) * strOldPic.Height);
                objNewPic = new Bitmap(strOldPic, intWidth, intHeight);
                return objNewPic;
            }
            catch (Exception exp) { throw exp; }
        }


        /// <summary>
        /// 按比例缩小图片，自动计算宽度
        /// </summary>
        /// <param name="strOldPic">源图文件名(包括路径)</param>
        /// <param name="strNewPic">缩小后保存为文件名(包括路径)</param>
        /// <param name="intHeight">缩小至高度</param>
        public Bitmap SmallPicH(Bitmap strOldPic, int intHeight)
        {
            Bitmap objNewPic;
            try
            {
                int intWidth = (int)(((intHeight * 1.0000) / strOldPic.Height) * strOldPic.Width);
                objNewPic = new Bitmap(strOldPic, intWidth, intHeight);
                return objNewPic;
            }
            catch (Exception exp) { throw exp; }
        }

        public Bitmap CutImage(Bitmap sourceBitmap, Rectangle rc)
        {
            if (rc.Bottom < 0)
                return null;
            Bitmap TempsourceBitmap = new Bitmap(rc.Right - rc.Left, rc.Bottom - rc.Top);
            Graphics gr = Graphics.FromImage(TempsourceBitmap);
            gr.DrawImage(sourceBitmap, 0, 0, new RectangleF(rc.Left, rc.Top, rc.Right - rc.Left, rc.Bottom - rc.Top), GraphicsUnit.Pixel);
            gr.Dispose();
            return TempsourceBitmap;
        }
        public Bitmap JoinImage(Bitmap sourceBitmap, Bitmap joinBitmap, Rectangle rc)
        {
            Bitmap TempsourceBitmap = sourceBitmap;
            Graphics gr = Graphics.FromImage(TempsourceBitmap);
            gr.DrawImage(joinBitmap, 0, 0, new RectangleF(rc.Left, rc.Top, rc.Right - rc.Left, rc.Bottom - rc.Top), GraphicsUnit.Pixel);
            gr.Dispose();
            return TempsourceBitmap;
        }
        /// <summary>
        /// 铺满
        /// </summary>
        /// <param name="sourceBitmap"></param>
        /// <param name="joinBitmap"></param>
        /// <param name="rc"></param>
        /// <returns></returns>
        public Bitmap JoinMImage(Bitmap sourceBitmap, Bitmap joinBitmap, Rectangle rc)
        {
            joinBitmap = SmallPic(joinBitmap, sourceBitmap.Width, sourceBitmap.Height);
            Bitmap TempsourceBitmap = sourceBitmap;
            Graphics gr = Graphics.FromImage(TempsourceBitmap);
            gr.DrawImage(joinBitmap, 0, 0, new RectangleF(rc.Left, rc.Top, rc.Right - rc.Left, rc.Bottom - rc.Top), GraphicsUnit.Pixel);
            gr.Dispose();
            return TempsourceBitmap;
        }

        public Bitmap JoinMImage(Bitmap sourceBitmap, Bitmap joinBitmap, Rectangle rc, int x, int y)
        {
            Bitmap TempsourceBitmap = sourceBitmap;
            Graphics gr = Graphics.FromImage(TempsourceBitmap);
            gr.DrawImage(joinBitmap, x, y, new RectangleF(rc.Left, rc.Top, rc.Right - rc.Left, rc.Bottom - rc.Top), GraphicsUnit.Pixel);
            gr.Dispose();
            return TempsourceBitmap;
        }
        public Bitmap JoinTxtImage(Bitmap sourceBitmap, string txt, Color color, int x, int y)
        {
            Bitmap TempsourceBitmap = sourceBitmap;
            Graphics gr = Graphics.FromImage(TempsourceBitmap);
            if (sourceBitmap.Width > 200)
                gr.DrawString(txt, new Font("微软雅黑", 13, FontStyle.Bold), new SolidBrush(color), new PointF(x, y));
            else
                gr.DrawString(txt, new Font("微软雅黑", 12, FontStyle.Bold), new SolidBrush(color), new PointF(x, y));
            gr.Dispose();
            return TempsourceBitmap;
        }
    }
}