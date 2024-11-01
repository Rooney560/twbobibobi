using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BCFBaseLibrary.Media;

namespace MotoSystem.Data
{
    public class ResourceUploadPage : BasePage
    {
        protected void DeleteImageFile(String imageFileName)
        {
            string filePath = ImageHome + imageFileName;
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

        }

        protected bool AddImages(string meetingID, string addOneID, string buyOneID, string queueID, string fileName , string ImageType, string imageFileName, string imageFilePath, int Num, out string displayImageName)
        {
            int imagesID = 0;
            return AddImages(meetingID, addOneID, buyOneID, queueID, fileName, ImageType, imageFileName, imageFilePath, Num, out  displayImageName, out imagesID);
        }

        protected bool AddImages(string meetingID, string addOneID, string buyOneID, string queueID, string fileName, string ImageType, string imageFileName, string imageFilePath, int Num, out string displayImageName, out int imagesID)
        {
            int status = 0;
            return AddImages(meetingID, addOneID, buyOneID, queueID, fileName, ImageType, imageFileName, imageFilePath, Num, status, out displayImageName, out imagesID);
        }

        protected bool AddImages(string meetingID, string addOneID, string buyOneID, string queueID, string fileName, string ImageType, string imageFileName, string imageFilePath, int Num , int status, out string displayImageName, out int imagesID)
        {
            imagesID = 0;
            bool bResult = false;
            string newFileName = MakeLocalFileName(fileName, imageFileName);
            string filePath = ImageHome + newFileName;


            string thumbnailImagePath = filePath.Substring(0, filePath.Length - 4) + "_thumb.jpg";
            string displayImagePath = filePath.Substring(0, filePath.Length - 4) + "_disp.jpg";
            displayImageName = newFileName.Substring(0, newFileName.Length - 4) + "_disp.jpg";

            bool supportedZoomout = false;
            JPEGAPI.CompressJpeg(imageFilePath, filePath, this.OriginalImageWidth, 80L, supportedZoomout);

            JPEGAPI.CompressJpeg(imageFilePath, displayImagePath, this.DisplayImageWidth, 70L, supportedZoomout);

            supportedZoomout = true;
            JPEGAPI.CompressJpeg(filePath, thumbnailImagePath, this.ThumbnailImageWidth, ThumbnailImageHeight, 60L, supportedZoomout);


            if (System.IO.File.Exists(thumbnailImagePath))
            {
                ImagesDAC objImagesDAC = new ImagesDAC(this);
                imagesID = objImagesDAC.AddImage(int.Parse(meetingID), int.Parse(addOneID), int.Parse(buyOneID), int.Parse(queueID), ImageType,
                    newFileName, newFileName.Substring(0, newFileName.Length - 4) + "_disp.jpg",
                    newFileName.Substring(0, newFileName.Length - 4) + "_thumb.jpg", Num, status);

                bResult = true;

                displayImageName = ImageWebHome + displayImageName;
            }

            return bResult;
        }

        protected bool UpdateImage(string meetingID, string ImageType, string imageFileName, string imageFilePath, int status, out string displayImageName, out int imageID)
        {
            imageID = 0;
            bool bResult = false;
            string newFileName = MakeLocalFileName(ImageType, imageFileName);
            string filePath = ImageHome + newFileName;


            string thumbnailImagePath = filePath.Substring(0, filePath.Length - 4) + "_thumb.jpg";
            string displayImagePath = filePath.Substring(0, filePath.Length - 4) + "_disp.jpg";
            displayImageName = newFileName.Substring(0, newFileName.Length - 4) + "_disp.jpg";

            bool supportedZoomout = false;
            JPEGAPI.CompressJpeg(imageFilePath, filePath, this.OriginalImageWidth, 80L, supportedZoomout);

            JPEGAPI.CompressJpeg(imageFilePath, displayImagePath, this.DisplayImageWidth, 70L, supportedZoomout);

            supportedZoomout = true;
            JPEGAPI.CompressJpeg(filePath, thumbnailImagePath, this.ThumbnailImageWidth, ThumbnailImageHeight, 60L, supportedZoomout);


            if (System.IO.File.Exists(thumbnailImagePath))
            {
                ImagesDAC objImagesDAC = new ImagesDAC(this);
                imageID = objImagesDAC.UpdateMeetingIDToImages(int.Parse(meetingID), ImageType,
                    newFileName, newFileName.Substring(0, newFileName.Length - 4) + "_disp.jpg",
                    newFileName.Substring(0, newFileName.Length - 4) + "_thumb.jpg", status);

                bResult = true;
            }

            return bResult;
        }

        private string MakeLocalFileName(string photoType, string fileName)
        {
            Guid tempId = Guid.NewGuid();
            string localFileName = string.Empty;
            string locatDirectary = photoType + @"\";
            if (!System.IO.Directory.Exists(ImageHome + locatDirectary))
            {
                System.IO.Directory.CreateDirectory(ImageHome + locatDirectary);
            }
            locatDirectary += DateTime.Now.ToString("yyyyMMdd") + @"\";
            if (!System.IO.Directory.Exists(ImageHome + locatDirectary))
            {
                System.IO.Directory.CreateDirectory(ImageHome + locatDirectary);
            }

            if (fileName.LastIndexOf(".") > 0)
            {

                localFileName = string.Format(@"{2}{0}.{1}", tempId.ToString(), fileName.Substring(fileName.LastIndexOf(".") + 1),
                    locatDirectary);
            }
            else
            {
                localFileName = string.Format(@"{2}{0}.{1}", tempId.ToString(), fileName.Substring(fileName.Length - 3),
                    locatDirectary);
            }
            return localFileName;
        }

        // ShowType 0-出車頁面 1-紀錄頁面
        protected static string RentTimeToString(string RentTime, int ShowType)
        {
            string result = string.Empty;
            if(ShowType == 1)
            {
                switch (RentTime)
                {
                    case "0":
                        result = "24H";
                        break;
                    case "1":
                        result = "夜跑";
                        break;
                    case "2":
                        result = "假日夜跑";
                        break;
                    case "3":
                        result = "平日六小";
                        break;
                    case "4":
                        result = "平日十小";
                        break;
                    case "5":
                        result = "24H + 24H";
                        break;
                    case "6":
                        result = "12H";
                        break;
                    case "7":
                        result = "6H";
                        break;
                }
            }
            else
            {
                switch (RentTime)
                {
                    case "0":
                        result = "24H";
                        break;
                    case "1":
                        result = "夜跑";
                        break;
                    case "4":
                        result = "平日十小";
                        break;
                    case "5":
                        result = "24H + 24H";
                        break;
                    case "6":
                        result = "12H";
                        break;
                    case "7":
                        result = "6H";
                        break;
                }
            }
            return result;
        }


        public static Dictionary<string, string> RentTime = new Dictionary<string, string>()
        {
            {"0", "24H"}, 
            {"1", "夜跑"},
            {"4", "平日十小"},
            {"5", "24H + 24H"},
            {"6", "12H"},
            {"7", "6H"}
        };
    }
}