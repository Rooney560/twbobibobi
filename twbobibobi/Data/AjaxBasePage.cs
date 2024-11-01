using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Web;
using Temple.data;
using AL.Common;
using System.IO;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace MotoSystem.Data
{
    public class AjaxBasePage : ResourceUploadPage
    {
        protected System.Collections.Hashtable AjaxHandlers = new Hashtable();

        protected override void OnInit(EventArgs e)
        {
            InitAjaxHandler();

            if (Request["RequestPageType"] != null && Request["RequestPageType"].ToUpper() == "JSON")
            {
                
                this.mJSonHelper.AddContent("StatusCode", 0);
                try
                {
                    if (OnValidate(this))
                    {
                        InvokeClassMethod();
                    }
                    else
                    {
                        mJSonHelper.AddContent("StatusCode", "-2");
                    }
                }
                catch (Exception error)
                {
                    SaveErrorLog(error);
                    this.mJSonHelper.AddContent("StatusCode", -1);
                }
                ResponseJSonString();

                Response.End();
            }

            base.OnInit(e);

           
        }

        protected virtual bool OnValidate(BasePage basePage)
        {
            return true;
        }

        protected virtual void InitAjaxHandler()
        {
        }

        protected void AddAjaxHandler(System.Web.UI.WebControls.Button button, Type HandlerType, string AjaxHandler)
        {
            button.Attributes["RequestMethod"] = AjaxHandler;

            button.Attributes["ServerID"] = button.ID;
            button.CssClass = ajaxButtonCss;

            AddAjaxHandler(HandlerType, AjaxHandler);
        }
        protected void AddAjaxHandler(Type HandlerType, string AjaxHandler)
        {
            if (!AjaxHandlers.ContainsKey(AjaxHandler))
            {
                AjaxHandlers.Add(AjaxHandler, HandlerType);
            }
        }

        public virtual void InvokeClassMethod()
        {
            Type HandlerType = null;
            string AjaxHandler = string.Empty;

            if (AjaxHandlers.ContainsKey(Request["RequestMethod"]))
            {
                HandlerType = (Type)AjaxHandlers[Request["RequestMethod"]];
                AjaxHandler = Request["RequestMethod"];
            }
            else
            {
                this.mJSonHelper.AddContent("StatusCode", -2);
            }

            if (HandlerType != null && AjaxHandler != string.Empty)
            {
                Activator.CreateInstance(HandlerType);
                var handler = Activator.CreateInstance(HandlerType);


                HandlerType.InvokeMember(AjaxHandler,
                                  BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Instance, null, handler,
                                  new object[] { this });
            }

        


        }

        protected string ajaxButtonCss = "ajaxButton";
        protected string ajaxSelectCss = "ajaxSelect";

        public void SetTipID(System.Web.UI.WebControls.Button button, string TipsID)
        {
            button.Attributes.Add("TipsID", TipsID);
        }

        public void SetSelectRelation(System.Web.UI.HtmlControls.HtmlSelect parentList, System.Web.UI.HtmlControls.HtmlSelect childList)
        {
            parentList.Attributes.Add("class", ajaxSelectCss);
            parentList.Attributes.Add("ChildList", childList.ClientID);
        }

        public void AddDropChecker(System.Web.UI.WebControls.Button button, string requestControlID)
        {
            button.CssClass = ajaxButtonCss;

            if (button.Attributes["DropChecker"] != null)
            {
                button.Attributes["DropChecker"] = button.Attributes["DropChecker"] + "|" + requestControlID;
            }
            else
            {
                button.Attributes["DropChecker"] = requestControlID;
            }
        }

        public void AddRequestChecker(System.Web.UI.WebControls.Button button, string requestControlID)
        {
            button.CssClass = ajaxButtonCss;

            if (button.Attributes["RequestChecker"] != null)
            {
                button.Attributes["RequestChecker"] = button.Attributes["RequestChecker"] + "|" + requestControlID;
            }
            else
            {
                button.Attributes["RequestChecker"] = requestControlID;
            }
        }

        public void AddLengthChecker(System.Web.UI.WebControls.Button button, string requestControlID, int minLength, int maxLength)
        {
            button.CssClass = ajaxButtonCss;

            if (button.Attributes["LengthChecker"] != null)
            {
                button.Attributes["LengthChecker"] = button.Attributes["LengthChecker"] + "|" + requestControlID + "," + minLength + "," + maxLength;
            }
            else
            {
                button.Attributes["LengthChecker"] = requestControlID + "," + minLength + "," + maxLength;
            }
        }

        public void AddEqualChecker(System.Web.UI.WebControls.Button button, string requestControlID, string requestControlID2)
        {
            button.CssClass = ajaxButtonCss;

            if (button.Attributes["EqualChecker"] != null)
            {
                button.Attributes["EqualChecker"] = button.Attributes["EqualChecker"] + "|" + requestControlID + "," + requestControlID2;
            }
            else
            {
                button.Attributes["EqualChecker"] = requestControlID + "," + requestControlID2;
            }
        }

        public void AddRegexChecker(System.Web.UI.WebControls.Button button, string requestControlID, string regex)
        {
            button.CssClass = ajaxButtonCss;

            if (button.Attributes["RegexChecker"] != null)
            {
                button.Attributes["RegexChecker"] = button.Attributes["RegexChecker"] + "|" + requestControlID + "," + regex;
            }
            else
            {
                button.Attributes["RegexChecker"] = requestControlID + "," + regex;
            }
        }

        public void AddCustomChecker(System.Web.UI.WebControls.Button button, string requestControlID, string handlerName)
        {
            button.CssClass = ajaxButtonCss;

            if (button.Attributes["CustomChecker"] != null)
            {
                button.Attributes["CustomChecker"] = button.Attributes["CustomChecker"] + "|" + requestControlID + "," + handlerName;
            }
            else
            {
                button.Attributes["CustomChecker"] = requestControlID + "," + handlerName;
            }
        }

        /// <summary>
        /// 由兩個日期計算出年齡(歲、月、天)
        /// </summary>
        public static string GetAge(DateTime beginDateTime, DateTime endDateTime)
        {
            string result = string.Empty;

            if (beginDateTime <= endDateTime)
            {
                /*計算出生日期到當前日期總月數*/
                int Months = endDateTime.Month - beginDateTime.Month + 12 * (endDateTime.Year - beginDateTime.Year);
                /*出生日期加總月數後，如果大於當前日期則減一個月*/
                int totalMonth = (beginDateTime.AddMonths(Months) > endDateTime) ? Months - 1 : Months;
                /*計算整年*/
                int fullYear = totalMonth / 12;
                /*計算整月*/
                int fullMonth = totalMonth % 12;
                /*計算天數*/
                DateTime changeDate = beginDateTime.AddMonths(totalMonth);
                double days = (endDateTime - changeDate).TotalDays;

                result = fullYear.ToString();
            }


            return result;
        }

        public static int GetAge(int Year, int Month, int Day)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            int age = dtNow.Year - Year;
            if (dtNow.Month < Month || (dtNow.Month == Month && dtNow.Day < Day))
            {
                age--;
            }
            return age < 0 ? 0 : age;
        }

        public static int GetAge(int Year, int Month, int Day, int lYear, int lMonth, int lDay)
        {
            int age = lYear - Year;
            if (lMonth < Month || (lMonth == Month && lDay < Day))
            {
                age--;
            }
            return age < 0 ? 0 : age;
        }

        /// <summary>
        /// 取得上個月第一天
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public DateTime FirstDayOfPreviousMonth(DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day).AddMonths(-1);
        }

        /// <summary>
        /// 取得上個月最後一天
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public DateTime LastDayOfPrdviousMonth(DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day).AddDays(-1);
        }

        public static string TextToHtml(string text)
        {
            text = HttpUtility.HtmlEncode(text);
            text = text.Replace("\r\n", "\r");
            text = text.Replace("\n", "\r");
            text = text.Replace("\r", "<br>\r\n");
            text = text.Replace("  ", " &nbsp;");
            return text;
        }

        public static string CheckedDateZero(string d, int t)
        {
            if (t == 1)
            {
                //雙位數 前面+0
                if (d.Length < 2)
                {
                    return "0" + d;
                }
                else
                {
                    return d;
                }
            }
            else if (t == 2)
            {
                //三位數
                switch (d.Length)
                {
                    case 2:
                        return "0" + d;
                    case 1:
                        return "00" + d;
                    default:
                        return d;
                }
            }
            else
            {
                return d;
            }
        }

        /// <summary>
        /// 取得生日的月份、年齡及生肖
        /// </summary>
        /// <param name="b"></param>
        /// <param name="BirthMonth"></param>
        /// <param name="Age"></param>
        /// <param name="Zodiac"></param>
        public static void GetBirthDetail(string b, ref string BirthMonth, ref string Age, ref string Zodiac)
        {
            string Birth = string.Empty;
            string year = string.Empty;
            string month = string.Empty;
            string day = string.Empty;

            string birth = b;
            int s1 = birth.IndexOf("民國");
            int s2 = birth.IndexOf("年");
            int s3 = birth.IndexOf("月");
            int s4 = birth.IndexOf("日");
            if (birth.IndexOf("民國") >= 0 && birth.IndexOf("年") > 0 && birth.IndexOf("月") > 0 && birth.IndexOf("日") > 0)
            {
                int year_index = birth.IndexOf("年");
                int month_index = birth.IndexOf("月");
                year = (int.Parse(birth.Substring(2, year_index - 2)) + 1911).ToString();
                month = BirthMonth = birth.Substring(year_index + 1, month_index - year_index - 1);
                day = birth.Substring(month_index + 1, birth.Length - month_index - 2);

                Birth = year + "-" + month + "-" + day;
                LunarSolarConverter.shuxiang(int.Parse(year), ref Zodiac);
                Age = GetAge(int.Parse(year), int.Parse(month), int.Parse(day)).ToString();
            }

            BirthMonth = BirthMonth.Length < 2 ? "0" + BirthMonth : BirthMonth;
        }

        /// <summary>
        /// 取得亡者生日的月份、年齡及生肖
        /// </summary>
        /// <param name="b"></param>
        /// <param name="BirthMonth"></param>
        /// <param name="Age"></param>
        /// <param name="Zodiac"></param>
        public static void GetDeathBirthDetail(string b, string d, ref string BirthMonth, ref string Age, ref string Zodiac)
        {
            string Birth = string.Empty;
            string year = string.Empty;
            string month = string.Empty;
            string day = string.Empty;

            string Deathday = string.Empty;
            string dyear = string.Empty;
            string dmonth = string.Empty;
            string dday = string.Empty;

            string birth = b;
            int s1 = birth.IndexOf("民國");
            int s2 = birth.IndexOf("年");
            int s3 = birth.IndexOf("月");
            int s4 = birth.IndexOf("日");

            string deathd = d;
            int ds1 = deathd.IndexOf("民國");
            int ds2 = deathd.IndexOf("年");
            int ds3 = deathd.IndexOf("月");
            int ds4 = deathd.IndexOf("日");

            if ((birth.IndexOf("民國") >= 0 && birth.IndexOf("年") > 0 && birth.IndexOf("月") > 0 && birth.IndexOf("日") > 0) &&
                (deathd.IndexOf("民國") >= 0 && deathd.IndexOf("年") > 0 && deathd.IndexOf("月") > 0 && deathd.IndexOf("日") > 0))
            {
                int year_index = birth.IndexOf("年");
                int month_index = birth.IndexOf("月");
                year = (int.Parse(birth.Substring(2, year_index - 2)) + 1911).ToString();
                month = BirthMonth = birth.Substring(year_index + 1, month_index - year_index - 1);
                day = birth.Substring(month_index + 1, birth.Length - month_index - 2);

                int dyear_index = deathd.IndexOf("年");
                int dmonth_index = deathd.IndexOf("月");
                dyear = (int.Parse(deathd.Substring(2, dyear_index - 2)) + 1911).ToString();
                dmonth = BirthMonth = deathd.Substring(dyear_index + 1, dmonth_index - dyear_index - 1);
                dday = deathd.Substring(dmonth_index + 1, deathd.Length - dmonth_index - 2);

                Deathday = dyear + "-" + dmonth + "-" + dday;
                LunarSolarConverter.shuxiang(int.Parse(year), ref Zodiac);
                Age = GetAge(int.Parse(year), int.Parse(month), int.Parse(day), int.Parse(dyear), int.Parse(dmonth), int.Parse(dday)).ToString();
            }

            BirthMonth = BirthMonth.Length < 2 ? "0" + BirthMonth : BirthMonth;
        }


        //燈種 3-光明燈 4-安太歲 5-文昌燈 6-財神燈 7-姻緣燈 8-藥師燈 9-財利燈 10-貴人燈 11-福祿(壽)燈 12-寵物平安燈 13-龍王燈 14-虎爺燈 15-轉運納福燈 16-光明燈上層 17-偏財旺旺燈 18-廣進安財庫 19-財庫燈 20-月老姻緣燈 21-孝親祈福燈
        public static string GetLightsType(string LightsString, string adminID)
        {
            string result = "-1";
            switch (LightsString)
            {
                case "光明燈":
                    result = "3";
                    break;
                case "元神光明燈":
                    result = "3";
                    break;
                case "安太歲":
                    result = "4";
                    break;
                case "太歲燈":
                    result = "4";
                    break;
                case "太歲平安符":
                    result = "4";
                    break;
                case "文昌燈":
                    result = "5";
                    break;
                case "五文昌燈":
                    result = "5";
                    break;
                case "文魁智慧燈":
                    result = "5";
                    break;
                case "財神燈":
                    result = "6";
                    break;
                case "發財燈":
                    result = "6";
                    break;
                case "福財燈":
                    result = "6";
                    break;
                case "正財福報燈":
                    result = "6";
                    break;
                case "姻緣燈":
                    result = "7";
                    break;
                case "桃花燈":
                    result = "7";
                    break;
                case "月老桃花燈":
                    result = "7";
                    break;
                case "藥師燈":
                    result = "8";
                    break;
                case "藥師佛燈":
                    result = "8";
                    break;
                case "財利燈":
                    result = "9";
                    break;
                case "貴人燈":
                    result = "10";
                    if (adminID == "15")
                    {
                        result = "3";
                    }
                    break;
                case "福壽燈":
                    result = "11";
                    break;
                case "福祿燈":
                    result = "11";
                    break;
                case "寵物平安燈":
                    result = "12";
                    break;
                case "龍王燈":
                    result = "13";
                    break;
                case "虎爺燈":
                    result = "14";
                    break;
                case "轉運納福燈":
                    result = "15";
                    break;
                case "光明燈上層":
                    result = "16";
                    break;
                case "偏財旺旺燈":
                    result = "17";
                    break;
                case "廣進安財庫":
                    result = "18";
                    break;
                case "財庫燈":
                    result = "19";
                    break;
                case "月老姻緣燈":
                    result = "20";
                    break;
                case "孝親祈福燈":
                    result = "21";
                    break;
                case "事業燈":
                    result = "22";
                    break;
                case "全家光明燈":
                    result = "23";
                    break;
                case "觀音佛祖燈":
                    result = "24";
                    break;
                case "財神斗":
                    result = "25";
                    break;
                case "事業斗":
                    result = "26";
                    break;
                case "平安斗":
                    result = "27";
                    break;
                case "文昌斗":
                    result = "28";
                    break;
                case "藥師斗":
                    result = "29";
                    break;
                case "元神斗":
                    result = "30";
                    break;
                case "福祿壽斗":
                    result = "31";
                    break;
                case "觀音斗":
                    result = "32";
                    break;
            }

            return result;
        }

        //關聖帝君聖誕項目 1-忠義狀功德主 2-富貴狀功德主 3-招財補運 4-招財補運紀念版
        public static string GetEmperorGuanshengType(string EmperorGuanshengString, string adminID)
        {
            string result = "-1";
            switch (EmperorGuanshengString)
            {
                case "忠義狀功德主":
                    result = "1";
                    break;
                case "富貴狀功德主":
                    result = "2";
                    break;
                case "招財補運":
                    result = "3";
                    break;
                case "招財補運紀念版":
                    result = "4";
                    break;
            }

            return result;
        }

        //靈寶禮斗項目 1-靈寶禮斗-功德主 2-靈寶禮斗-隨喜功德主 3-靈寶禮斗-消災解厄科儀 
        public static string GetLingbaolidouType(string LingbaolidouString, string adminID)
        {
            string result = "-1";
            switch (LingbaolidouString)
            {
                case "靈寶禮斗-功德主":
                    result = "1";
                    break;
                case "靈寶禮斗-隨喜功德主":
                    result = "2";
                    break;
                case "靈寶禮斗-消災解厄科儀":
                    result = "3";
                    break;
            }

            return result;
        }

        //七朝清醮項目 1-祈安七朝清醮-普渡施食 2-祈安七朝清醮-王船添載(天錢天庫) 3-祈安七朝清醮-王船添載(添載物資) 4-祈安七朝清醮-公斗 5-祈安七朝清醮-燃放水燈(大) 6-祈安七朝清醮-燃放水燈(中) 7-祈安七朝清醮-燃放水燈(小)
        public static string GetTaoistJiaoCeremonyType(string TaoistJiaoCeremonyString, string adminID)
        {
            string result = "-1";
            switch (TaoistJiaoCeremonyString)
            {
                case "祈安七朝清醮-普渡施食":
                    result = "1";
                    break;
                case "祈安七朝清醮-王船添載(天錢天庫)":
                    result = "2";
                    break;
                case "祈安七朝清醮-王船添載(添載物資)":
                    result = "3";
                    break;
                case "祈安七朝清醮-公斗":
                    result = "4";
                    break;
                case "祈安七朝清醮-燃放水燈(大)":
                    result = "5";
                    break;
                case "祈安七朝清醮-燃放水燈(中)":
                    result = "6";
                    break;
                case "祈安七朝清醮-燃放水燈(小)":
                    result = "7";
                    break;
            }

            return result;
        }

        /// <summary>
        /// 轉換點燈項目
        /// </summary>
        /// <param name="lightstype">lightstype=燈種 3-光明燈 4-安太歲 5-文昌燈 6-財神燈 7-姻緣燈 8-藥師燈 9-財利燈 10-貴人燈 11-福祿(壽)燈 12-寵物平安燈 13-龍王燈 14-虎爺燈 15-轉運納福燈 16-光明燈上層 17-偏財旺旺燈 18-廣進安財庫 19-財庫燈 20-月老姻緣燈 21-孝親祈福燈</param>
        /// <returns></returns>
        public static string LightsType2String(string lightstype, string adminID)
        {
            string result = string.Empty;
            switch (lightstype)
            {
                case "3":
                    switch (adminID)
                    {
                        case "15":
                            result = "貴人燈";
                            break;
                        case "21":
                            result = "元神光明燈";
                            break;
                        default:
                            result = "光明燈";
                            break;
                    }

                    break;
                case "4":
                    switch (adminID)
                    {
                        case "14":
                            result = "太歲燈";
                            break;
                        case "21":
                            result = "太歲平安符";
                            break;
                        case "23":
                            result = "太歲燈";
                            break;
                        default:
                            result = "安太歲";
                            break;
                    }
                    break;
                case "5":
                    switch (adminID)
                    {
                        case "21":
                            result = "文魁智慧燈";
                            break;
                        case "23":
                            result = "五文昌燈";
                            break;
                        default:
                            result = "文昌燈";
                            break;
                    }
                    break;
                case "6":
                    switch (adminID)
                    {
                        case "15":
                            result = "發財燈";
                            break;
                        case "21":
                            result = "正財福報燈";
                            break;
                        default:
                            result = "財神燈";
                            break;
                    }
                    break;
                case "7":
                    switch (adminID)
                    {
                        case "15":
                            result = "月老桃花燈";
                            break;
                        default:
                            result = "姻緣燈";
                            break;
                    }
                    break;
                case "8":
                    switch (adminID)
                    {
                        case "14":
                            result = "藥師燈";
                            break;
                        case "31":
                            result = "藥師燈";
                            break;
                        default:
                            result = "藥師佛燈";
                            break;
                    }
                    break;
                case "9":
                    switch (adminID)
                    {
                        default:
                            result = "財利燈";
                            break;
                    }
                    break;
                case "10":
                    switch (adminID)
                    {
                        default:
                            result = "貴人燈";
                            break;
                    }
                    break;
                case "11":
                    switch (adminID)
                    {
                        case "14":
                            result = "福祿燈";
                            break;
                        default:
                            result = "福壽燈";
                            break;
                    }
                    break;
                case "12":
                    switch (adminID)
                    {
                        default:
                            result = "寵物平安燈";
                            break;
                    }
                    break;
                case "13":
                    switch (adminID)
                    {
                        default:
                            result = "龍王燈";
                            break;
                    }
                    break;
                case "14":
                    switch (adminID)
                    {
                        default:
                            result = "虎爺燈";
                            break;
                    }
                    break;
                case "15":
                    switch (adminID)
                    {
                        default:
                            result = "轉運納福燈";
                            break;
                    }
                    break;
                case "16":
                    switch (adminID)
                    {
                        default:
                            result = "光明燈上層";
                            break;
                    }
                    break;
                case "17":
                    switch (adminID)
                    {
                        default:
                            result = "偏財旺旺燈";
                            break;
                    }
                    break;
                case "18":
                    switch (adminID)
                    {
                        default:
                            result = "廣進安財庫";
                            break;
                    }
                    break;
                case "19":
                    switch (adminID)
                    {
                        default:
                            result = "財庫燈";
                            break;
                    }
                    break;
                case "20":
                    switch (adminID)
                    {
                        default:
                            result = "月老姻緣燈";
                            break;
                    }
                    break;
                case "21":
                    switch (adminID)
                    {
                        default:
                            result = "孝親祈福燈";
                            break;
                    }
                    break;
                case "22":
                    switch (adminID)
                    {
                        default:
                            result = "事業燈";
                            break;
                    }
                    break;
                case "23":
                    switch (adminID)
                    {
                        default:
                            result = "全家光明燈";
                            break;
                    }
                    break;
                case "24":
                    switch (adminID)
                    {
                        default:
                            result = "觀音佛祖燈";
                            break;
                    }
                    break;
                case "25":
                    switch (adminID)
                    {
                        default:
                            result = "財神斗";
                            break;
                    }
                    break;
                case "26":
                    switch (adminID)
                    {
                        default:
                            result = "事業斗";
                            break;
                    }
                    break;
                case "27":
                    switch (adminID)
                    {
                        default:
                            result = "平安斗";
                            break;
                    }
                    break;
                case "28":
                    switch (adminID)
                    {
                        default:
                            result = "文昌斗";
                            break;
                    }
                    break;
                case "29":
                    switch (adminID)
                    {
                        default:
                            result = "藥師斗";
                            break;
                    }
                    break;
                case "30":
                    switch (adminID)
                    {
                        default:
                            result = "元神斗";
                            break;
                    }
                    break;
                case "31":
                    switch (adminID)
                    {
                        default:
                            result = "福祿壽斗";
                            break;
                    }
                    break;
                case "32":
                    switch (adminID)
                    {
                        default:
                            result = "觀音斗";
                            break;
                    }
                    break;
            }
            return result;
        }

        /// <summary>
        /// 轉換普度項目
        /// </summary>
        /// <param name="purdueType">purdueType=普度項目 1-贊普 2-九玄七祖 3-指名亡者 4-本境地基主 5-冤親債主 6-嬰靈 7-為國捐軀三軍將士英靈 8-鐵公路車傷死亡眾魂 9-本境水難傷亡諸魂 10-本境男女無嗣孤魂等眾 11-六畜往生 12-法船 13-壽生錢 14-孝道功德主 15-光明功德主 16-發心功德主 17-誦經迴向 18-白米50台斤 19-白米3台斤</param>
        /// <returns></returns>
        public static string PurdueType2String(string purdueType, string adminID)
        {
            string result = string.Empty;

            switch (purdueType)
            {
                case "1":
                    //贊普
                    result = "贊普";
                    break;
                case "2":
                    //九玄七祖
                    switch (adminID)
                    {
                        case "3":
                            result = "O家堂上歷代九玄七祖";
                            break;
                        case "8":
                            result = "超度九玄七祖";
                            break;
                        case "16":
                            result = "歷代祖先";
                            break;
                        default:
                            result = "九玄七祖";
                            break;
                    }
                    break;
                case "3":
                    //指名亡者
                    switch (adminID)
                    {
                        case "8":
                            result = "超度指名亡者";
                            break;
                        case "16":
                            result = "往生親友";
                            break;
                        default:
                            result = "指名亡者";
                            break;
                    }
                    break;
                case "4":
                    //本境地基主
                    switch (adminID)
                    {
                        case "3":
                            result = "本境地基主";
                            break;
                        case "8":
                            result = "超度地基主";
                            break;
                        default:
                            result = "地基主";
                            break;
                    }
                    break;
                case "5":
                    //冤親債主
                    switch (adminID)
                    {
                        case "8":
                            result = "解冤親債主";
                            break;
                        case "23":
                            result = "累世冤親債主";
                            break;
                        default:
                            result = "冤親債主";
                            break;
                    }
                    break;
                case "6":
                    //嬰靈
                    switch (adminID)
                    {
                        case "3":
                            result = "O家嬰靈";
                            break;
                        case "8":
                            result = "超度嬰靈";
                            break;
                        case "16":
                            result = "嬰靈(無緣子女)";
                            break;
                        default:
                            result = "嬰靈";
                            break;
                    }
                    break;
                case "7":
                    //為國捐軀三軍將士英靈
                    switch (adminID)
                    {
                        case "23":
                            result = "三軍將士";
                            break;
                        default:
                            result = "為國捐軀三軍將士英靈";
                            break;
                    }
                    break;
                case "8":
                    //鐵公路車傷死亡眾魂
                    result = "鐵公路車傷死亡眾魂";
                    break;
                case "9":
                    //本境水難傷亡諸魂
                    result = "本境水難傷亡諸魂";
                    break;
                case "10":
                    //本境男女無嗣孤魂等眾
                    result = "本境男女無嗣孤魂等眾";
                    break;
                case "11":
                    //六畜往生
                    switch (adminID)
                    {
                        case "8":
                            result = "超度動物靈";
                            break;
                        case "23":
                            result = "動物靈";
                            break;
                        default:
                            result = "六畜往生";
                            break;
                    }
                    break;
                case "12":
                    //法船
                    result = "法船";
                    break;
                case "13":
                    //壽生錢
                    result = "壽生錢";
                    break;
                case "14":
                    //孝道功德主
                    result = "孝道功德主";
                    break;
                case "15":
                    //光明功德主
                    result = "光明功德主";
                    break;
                case "16":
                    //發心功德主
                    result = "發心功德主";
                    break;
                case "17":
                    //誦經迴向
                    result = "誦經迴向";
                    break;
                case "18":
                    //白米50台斤
                    result = "白米50台斤";
                    break;
                case "19":
                    //白米3台斤
                    result = "白米3台斤";
                    break;
                case "20":
                    //寵物普度-毛小孩
                    result = "寵物普度-毛小孩";
                    break;
                case "21":
                    //寵物普度-喵星人
                    result = "寵物普度-喵星人";
                    break;
            }

            return result;
        }

        //七朝清醮項目 1-祈安七朝清醮-普渡施食 2-祈安七朝清醮-王船添載(天錢天庫) 3-祈安七朝清醮-王船添載(添載物資) 4-祈安七朝清醮-公斗 5-祈安七朝清醮-燃放水燈(大) 6-祈安七朝清醮-燃放水燈(中) 7-祈安七朝清醮-燃放水燈(小)
        public static string GetTaoistJiaoCeremonyType2String(string TaoistJiaoCeremonyType, string adminID)
        {
            string result = "";
            switch (TaoistJiaoCeremonyType)
            {
                case "1":
                    result = "祈安七朝清醮-普渡施食";
                    break;
                case "2":
                    result = "祈安七朝清醮-王船添載(天錢天庫)";
                    break;
                case "3":
                    result = "祈安七朝清醮-王船添載(添載物資)";
                    break;
                case "4":
                    result = "祈安七朝清醮-公斗";
                    break;
                case "5":
                    result = "祈安七朝清醮-燃放水燈(大)";
                    break;
                case "6":
                    result = "祈安七朝清醮-燃放水燈(中)";
                    break;
                case "7":
                    result = "祈安七朝清醮-燃放水燈(小)";
                    break;
            }

            return result;
        }


        ///燈種 3-光明燈 4-安太歲 5-文昌燈 6-財神燈 7-姻緣燈 8-藥師燈 9-財利燈 10-貴人燈 11-福祿(壽)燈 12-寵物平安燈 13-龍王燈 14-虎爺燈 15-轉運納福燈 16-光明燈上層 17-偏財旺旺燈
        ///18-廣進安財庫 19-財庫燈 20-月老姻緣燈 21-孝親祈福燈 22-事業登 23-全家光明燈 24-觀音佛祖燈 25-財神斗 26-事業斗 27-平安斗 28-文昌斗 29-藥師斗 30-元神斗 31-福祿壽斗 
        ///32-觀音斗
        public static int GetLightsCost(int AdminID, string LightsType)
        {
            int result = 0;

            switch (AdminID)
            {
                case 3:
                    //大甲鎮瀾宮
                    switch (LightsType)
                    {
                        case "3":
                            //光明燈
                            result = 620;
                            break;
                        case "4":
                            //安太歲
                            result = 520;
                            break;
                        case "5":
                            //文昌燈
                            result = 820;
                            break;
                    }
                    break;
                case 4:
                    //新港奉天宮
                    switch (LightsType)
                    {
                        case "3":
                            //光明燈
                            result = 620;
                            break;
                        case "4":
                            //安太歲
                            result = 620;
                            break;
                    }
                    break;
                case 6:
                    //北港武德宮
                    switch (LightsType)
                    {
                        case "3":
                            //光明燈
                            result = 600;
                            break;
                        case "4":
                            //安太歲
                            result = 600;
                            break;
                        case "6":
                            //財神燈
                            result = 600;
                            break;
                    }
                    break;
                case 8:
                    //西螺福興宮
                    switch (LightsType)
                    {
                        case "3":
                            //光明燈
                            result = 600;
                            break;
                        case "4":
                            //安太歲
                            result = 300;
                            break;
                        case "5":
                            //文昌燈
                            result = 600;
                            break;
                        case "6":
                            //財神燈
                            result = 600;
                            break;
                        case "8":
                            //藥師燈
                            result = 600;
                            break;
                        case "24":
                            //觀音佛祖燈
                            result = 600;
                            break;
                    }
                    break;
                case 10:
                    //台南正統鹿耳門聖母廟
                    switch (LightsType)
                    {
                        case "3":
                            //光明燈
                            result = 500;
                            break;
                        case "4":
                            //安太歲
                            result = 500;
                            break;
                        case "5":
                            //文昌燈
                            result = 500;
                            break;
                        case "7":
                            //姻緣燈
                            result = 999;
                            break;
                        case "9":
                            //財利燈
                            result = 600;
                            break;
                        case "11":
                            //福壽燈
                            result = 999;
                            break;
                        case "12":
                            //寵物平安燈
                            result = 500;
                            break;
                        case "20":
                            //月老姻緣燈
                            result = 999;
                            break;
                    }
                    break;
                case 14:
                    //桃園威天宮
                    switch (LightsType)
                    {
                        case "3":
                            //光明燈
                            result = 300;
                            break;
                        case "4":
                            //太歲燈
                            result = 300;
                            break;
                        case "6":
                            //財神燈
                            result = 600;
                            break;
                        case "8":
                            //藥師燈
                            result = 600;
                            break;
                        case "10":
                            //貴人燈
                            result = 600;
                            break;
                        case "11":
                            //福祿燈
                            result = 600;
                            break;
                        case "21":
                            //孝親祈福燈
                            result = 880;
                            break;
                    }
                    break;
                case 15:
                    //斗六五路財神宮
                    switch (LightsType)
                    {
                        case "3":
                            //貴人燈(光明燈)
                            result = 500;
                            break;
                        case "4":
                            //安太歲
                            result = 500;
                            break;
                        case "6":
                            //發財燈
                            result = 500;
                            break;
                        case "7":
                            //月老桃花燈
                            result = 500;
                            break;
                        case "12":
                            //寵物平安燈
                            result = 500;
                            break;
                        case "19":
                            //財庫燈
                            result = 500;
                            break;
                    }
                    break;
                case 16:
                    //台東東海龍門天聖宮
                    switch (LightsType)
                    {
                        case "3":
                            //光明燈
                            result = 500;
                            break;
                        case "4":
                            //安太歲
                            result = 500;
                            break;
                        case "5":
                            //文昌燈
                            result = 500;
                            break;
                        case "9":
                            //財利燈
                            result = 500;
                            break;
                        case "13":
                            //龍王燈
                            result = 800;
                            break;
                        case "14":
                            //虎爺燈
                            result = 500;
                            break;
                    }
                    break;
                case 21:
                    //鹿港城隍廟
                    switch (LightsType)
                    {
                        case "3":
                            //元神光明燈
                            result = 500;
                            break;
                        case "4":
                            //太歲平安符
                            result = 500;
                            break;
                        case "5":
                            //文魁智慧燈
                            result = 500;
                            break;
                        case "6":
                            //正財福報燈
                            result = 500;
                            break;
                        case "15":
                            //轉運納福燈
                            result = 1000;
                            break;
                        case "16":
                            //光明燈上層
                            result = 1000;
                            break;
                        case "17":
                            //偏財旺旺燈
                            result = 500;
                            break;
                        case "18":
                            //廣進安財庫
                            result = 300;
                            break;
                    }
                    break;
                case 23:
                    //玉敕大樹朝天宮
                    switch (LightsType)
                    {
                        case "3":
                            //光明燈
                            result = 500;
                            break;
                        case "4":
                            //太歲燈
                            result = 500;
                            break;
                        case "5":
                            //五文昌燈
                            result = 500;
                            break;
                        case "6":
                            //福財燈
                            result = 500;
                            break;
                    }
                    break;
                case 29:
                    //進寶財神廟
                    switch (LightsType)
                    {
                        case "3":
                            //光明燈
                            result = 600;
                            break;
                        case "4":
                            //太歲燈
                            result = 600;
                            break;
                        case "9":
                            //財利燈
                            result = 600;
                            break;
                        case "10":
                            //貴人燈
                            result = 600;
                            break;
                    }
                    break;
                case 31:
                    //台灣道教總廟無極三清總道院
                    switch (LightsType)
                    {
                        case "3":
                            //光明燈
                            result = 500;
                            break;
                        case "4":
                            //太歲燈
                            result = 500;
                            break;
                        case "5":
                            //文昌燈
                            result = 500;
                            break;
                        case "6":
                            //財神燈
                            result = 500;
                            break;
                        case "8":
                            //藥師燈
                            result = 500;
                            break;
                        case "22":
                            //事業燈
                            result = 500;
                            break;
                        case "23":
                            //全家光明燈
                            result = 1000;
                            break;
                        case "25":
                            //財神斗
                            result = 3000;
                            break;
                        case "26":
                            //事業斗
                            result = 3000;
                            break;
                        case "27":
                            //平安斗
                            result = 3000;
                            break;
                        case "28":
                            //文昌斗
                            result = 3000;
                            break;
                        case "29":
                            //藥師斗
                            result = 3000;
                            break;
                        case "30":
                            //元神斗
                            result = 3000;
                            break;
                        case "31":
                            //福祿壽斗
                            result = 3000;
                            break;
                        case "32":
                            //觀音斗
                            result = 3000;
                            break;
                    }
                    break;
            }

            return result;
        }

        //普度項目 1-贊普 2-九玄七祖 3-指名亡者 4-本境地基主 5-冤親債主 6-嬰靈 7-為國捐軀三軍將士英靈 8-鐵公路車傷死亡眾魂 9-本境水難傷亡諸魂 10-本境男女無嗣孤魂等眾 11-六畜往生 12-法船 13-壽生錢 14-孝道功德主 15-光明功德主 16-發心功德主 17-誦經迴向 18-白米50台斤 19-白米3台斤
        public static int GetPurdueCost(int AdminID, string PurdueType)
        {
            int result = 0;

            switch (AdminID)
            {
                case 3:
                    //大甲鎮瀾宮
                    switch (PurdueType)
                    {
                        case "1":
                            //贊普
                            result = 1000;
                            break;
                        default:
                            //超拔
                            result = 620;
                            break;
                    }
                    break;
                case 4:
                    //新港奉天宮
                    break;
                case 6:
                    //北港武德宮
                    switch (PurdueType)
                    {
                        case "1":
                            //贊普
                            result = 1500;
                            break;
                    }
                    break;
                case 8:
                    //西螺福興宮
                    switch (PurdueType)
                    {
                        case "1":
                            //贊普
                            result = 1500;
                            break;
                        default:
                            //超拔
                            result = 600;
                            break;
                    }
                    break;
                case 9:
                    //桃園大廟景福宮
                    switch (PurdueType)
                    {
                        case "1":
                            //贊普
                            result = 1000;
                            break;
                    }
                    break;
                case 10:
                    //台南正統鹿耳門聖母廟
                    switch (PurdueType)
                    {
                        case "1":
                            //贊普
                            result = 1000;
                            break;
                    }
                    break;
                case 14:
                    //桃園威天宮
                    switch (PurdueType)
                    {
                        case "1":
                            //贊普
                            result = 600;
                            break;
                        case "14":
                            //孝道功德主 兩項普渡項目$3000/2
                            result = 3000;
                            break;
                        case "15":
                            //光明功德主
                            result = 1000;
                            break;
                        case "16":
                            //發心功德主
                            result = 600;
                            break;
                        case "18":
                            //白米50台斤
                            result = 1600;
                            break;
                        case "19":
                            //白米3台斤
                            result = 400;
                            break;
                    }
                    break;
                case 15:
                    //斗六五路財神宮
                    switch (PurdueType)
                    {
                        case "1":
                            //贊普
                            result = 1200;
                            break;
                        default:
                            //誦經迴向
                            result = 1200;
                            break;
                    }
                    break;
                case 16:
                    //台東東海龍門天聖宮
                    switch (PurdueType)
                    {
                        case "1":
                            //贊普
                            result = 1500;
                            break;
                        default:
                            //超拔
                            result = 500;
                            break;
                    }
                    break;
                case 21:
                    //鹿港城隍廟
                    switch (PurdueType)
                    {
                        case "1":
                            //贊普
                            result = 2000;
                            break;
                    }
                    break;
                case 23:
                    //玉敕大樹朝天宮
                    switch (PurdueType)
                    {
                        case "1":
                            //贊普
                            result = 1200;
                            break;
                        case "12":
                            //法船
                            result = 580;
                            break;
                        case "13":
                            //壽生錢
                            result = 1500;
                            break;
                        default:
                            //法會其他項目
                            result = 300;
                            break;
                    }
                    break;
                case 30:
                    //鎮瀾買足
                    switch (PurdueType)
                    {
                        default:
                            //寵物普渡
                            result = 1100;
                            break;
                    }
                    break;
            }

            return result;
        }

        //補庫項目 1-下元補庫 2-呈疏補庫 3-企業補財庫 4-天赦日補運 5-天赦日祭改 6-天貺納福添運法會 7-招財補運 8-招財補運九九重陽升級版
        public static int GetSuppliesCost(int AdminID, string SuppliesType)
        {
            int result = 0;

            switch (AdminID)
            {
                case 6:
                    //北港武德宮
                    switch (SuppliesType)
                    {
                        case "1":
                            //下元補庫
                            result = 600;
                            break;
                        case "2":
                            //呈疏補庫
                            result = 600;
                            break;
                        case "3":
                            //企業補財庫
                            result = 1300;
                            break;
                    }
                    break;
                case 14:
                    //桃園威天宮
                    switch (SuppliesType)
                    {
                        case "4":
                            //天赦日補運
                            result = 1280;
                            break;
                        case "7":
                            //招財補運
                            result = 1280;
                            break;
                        case "8":
                            //招財補運九九重陽升級版
                            result = 5880;
                            break;
                    }
                    break;
            }

            return result;
        }

        //關聖帝君聖誕項目 1-忠義狀功德主 2-富貴狀功德主 3-招財補運 4-招財補運紀念版
        public static int GetEmperorGuanshengCost(int AdminID, string EmperorGuanshengType)
        {
            int result = 0;

            switch (AdminID)
            {
                case 14:
                    //桃園威天宮
                    switch (EmperorGuanshengType)
                    {
                        case "1":
                            //忠義狀功德主
                            result = 800;
                            break;
                        case "2":
                            //富貴狀功德主
                            result = 3000;
                            break;
                        case "3":
                            //招財補運
                            result = 1280;
                            break;
                        case "4":
                            //招財補運紀念版
                            result = 5880;
                            break;
                    }
                    break;
            }

            return result;
        }

        //靈寶禮斗項目 1-靈寶禮斗-功德主 2-靈寶禮斗-隨喜功德主 3-靈寶禮斗-消災解厄科儀 
        public static int GetLingbaolidouCost(int AdminID, string LingbaolidouType)
        {
            int result = 0;

            switch (AdminID)
            {
                case 23:
                    //玉敕大樹朝天宮
                    switch (LingbaolidouType)
                    {
                        case "1":
                            //靈寶禮斗-功德主
                            result = 6800;
                            break;
                        case "2":
                            //靈寶禮斗-隨喜功德主
                            result = 1000;
                            break;
                        case "3":
                            //靈寶禮斗-消災解厄科儀 
                            result = 550;
                            break;
                    }
                    break;
            }

            return result;
        }

        //七朝清醮項目 1-祈安七朝清醮-普渡施食 2-祈安七朝清醮-王船添載(天錢天庫) 3-祈安七朝清醮-王船添載(添載物資) 4-祈安七朝清醮-公斗 5-祈安七朝清醮-燃放水燈(大) 6-祈安七朝清醮-燃放水燈(中) 7-祈安七朝清醮-燃放水燈(小)
        public static int GetTaoistJiaoCeremonyCost(int AdminID, string TaoistJiaoCeremonyType)
        {
            int result = 0;

            switch (AdminID)
            {
                case 3:
                    //大甲鎮瀾宮
                    switch (TaoistJiaoCeremonyType)
                    {
                        case "1":
                            //祈安七朝清醮-普渡施食
                            result = 1000;
                            break;
                        case "2":
                            //祈安七朝清醮-王船添載(天錢天庫)
                            result = 600;
                            break;
                        case "3":
                            //祈安七朝清醮-王船添載(添載物資)
                            result = 600;
                            break;
                        case "4":
                            //祈安七朝清醮-公斗
                            result = 1000;
                            break;
                        case "5":
                            //祈安七朝清醮-燃放水燈(大)
                            result = 2200;
                            break;
                        case "6":
                            //祈安七朝清醮-燃放水燈(中)
                            result = 1000;
                            break;
                        case "7":
                            //祈安七朝清醮-燃放水燈(小)
                            result = 600;
                            break;
                    }
                    break;
            }

            return result;
        }

        public bool HttpPost(string url, string strPostData, ref string resp, string contentType = "application/x-www-form-urlencoded", Dictionary<string, string> headers = null)
        {
            bool result = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.DefaultConnectionLimit = 1000;
            try
            {
                Encoding encoding = ((!Tools.containUnicodeChars(strPostData)) ? Encoding.ASCII : Encoding.UTF8);
                byte[] bytes = encoding.GetBytes(strPostData);
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertificate;
                }

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentType = contentType;
                httpWebRequest.ContentLength = bytes.Length;
                if (headers != null)
                {
                    foreach (KeyValuePair<string, string> header in headers)
                    {
                        httpWebRequest.Headers.Add(header.Key, header.Value);
                    }
                }

                string log = "Header: " + httpWebRequest.Headers.ToString() + ", JsonData: " + strPostData;

                using (Stream stream = httpWebRequest.GetRequestStream())
                {
                    stream.Write(bytes, 0, bytes.Length);
                }

                using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    Stream stream2 = httpWebResponse.GetResponseStream();
                    StreamReader streamReader = new StreamReader(stream2);
                    resp = streamReader.ReadToEnd();
                }

                httpWebRequest.Abort();
                return result;
            }
            catch (Exception ex)
            {
                result = false;
                resp = ex.Message;
                return result;
            }
        }

        private static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

    }
}
