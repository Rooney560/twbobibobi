using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

namespace Temple.data
{
    public enum MaskType
    {
        Name,
        Creditcard,
        Addr,
        Id,
        Tel
    }

    class UtilDataMask
    {
        /// 

        /// 資料遮罩
        /// 

        ///需遮罩之字串
        ///選擇需遮罩之型態:手機,地址....
        /// 加密過後的字串
        public static string MaskValue(string val, MaskType Type)
        {

            switch (Type.ToString())
            {
                case "Name":
                    val = MaskName(val);
                    break;
                case "Creditcard":
                    val = MaskCreditcard(val);
                    break;
                case "Addr":
                    val = MaskAddr(val);
                    break;
                case "Id":
                    val = MaskId(val);
                    break;
                case "Tel":
                    val = MaskTel(val);
                    break;
                default:
                    break;
            }

            return val;
        }
        /// 

        /// 姓名遮罩包含英文姓名
        /// 

        ///
        /// 姓名
        /// 
        /// 王O名
        public static string MaskName(string val)
        {
            if (!string.IsNullOrEmpty(val))
            {
                string maskstr, maskchar;
                maskchar = null;
                if (Regex.IsMatch(val, "[A-Za-z]"))
                {
                    if (val.IndexOf("-") > 1)
                    {
                        maskstr = val.Split('-')[1];
                        val = val.Replace(maskstr, "*");
                    }
                    if (val.IndexOf(" ") > 1)
                    {
                        maskstr = val.Split(' ')[1];
                        val = val.Replace(maskstr, "*");
                    }
                }
                else
                {

                    int End = (int)(val.Length / 2);
                    if (End > 0)
                    {
                        maskstr = val.Substring(1, End);
                        for (int i = 0; i < maskstr.Length; i++)
                        {
                            maskchar = maskchar + "Ｏ";
                        }
                        val = val.Replace(maskstr, maskchar);
                    }
                }
            }
            else
            {

                val = "";
            }

            return val;
        }
        /// 

        /// 信用卡遮罩--信用卡號=>前6後4不mask
        /// 

        ///5555-2525-1266-2213
        /// 
        /// 5555-25**-****-2213
        /// 
        private static string MaskCreditcard(string val)
        {
            if (!string.IsNullOrEmpty(val))
            {

                val = (Regex.IsMatch(val.Replace("-", ""), RegularExp.creditcard) ? val.Substring(0, 7) + "**-****" + val.Substring(14, 5) : "");

            }
            else
            {

                val = "";
            }
            return val;

        }
        /// 

        /// 地址=>留前6個字(Unicode)
        /// 

        ///台南市中正路321巷7弄17號2F
        /// 台南市中正路***********
        private static string MaskAddr(string val)
        {
            string maskstr, maskchar;
            maskchar = null;
            if (!string.IsNullOrEmpty(val))
            {
                int End = (int)(val.Length - 6);
                maskstr = val.Substring(6, End);
                for (int i = 0; i < maskstr.Length; i++)
                {
                    maskchar = maskchar + "*";
                }
                val = val.Replace(maskstr, maskchar);

            }
            else
            {

                val = "";
            }
            return val;


        }

        private static void SplitStr(string val, char[] splitChar, out string[] result)
        {
            result = null;
            foreach (char splitstr in splitChar)
            {
                SplitStr(val, splitstr.ToString(), out result);
                if (result != null)
                {
                    break;
                }
            }
        }

        private static void SplitStr(string val, string splitStr, out string[] result)
        {
            if (val.IndexOf(splitStr, StringComparison.OrdinalIgnoreCase) > 0)
            {
                result = val.Split(splitStr.ToCharArray());
                result[0] += splitStr;
            }
            else
            {
                result = null;
            }
        }

        private static char[] addrchar = { '弄', '巷', '路', '街', '段', '區', '市', '縣' };


        private static string MaskStr(string val, int startLen, int num, string MaskChar)
        {
            string val_start = string.Empty;
            string val_end = string.Empty;
            string maskchar = string.Empty;

            for (int i = 0; i < num; i++)
            {
                maskchar += MaskChar;
            }

            if (startLen > 0)
            {
                val_start = val.Substring(0, startLen - 1);
            }

            if (val.Length - startLen - num > 0)
            {
                int startindex = startLen + num - 1;
                int length = val.Length - startindex;
                val_end = val.Substring(startindex, length);
            }

            return val_start + maskchar + val_end;
        }

        /// 
        /// 地址(Unicode)=>保留縣、市及鄉、鎮、區、路、段
        /// 
        /// 台南市中正路321巷7弄17號2F
        /// 台南市中正路***********
        public static string MaskTWAddr(string val)
        {
            string maskchar;
            string[] maskRoadnum, maskFloornum;
            maskchar = null;
            int len = val.Length;
            if (!string.IsNullOrEmpty(val))
            {
                SplitStr(val, addrchar, out maskRoadnum);
                if (maskRoadnum != null)
                {
                    if (!string.IsNullOrEmpty((maskRoadnum[1])))
                    {
                        int roadsNum = maskRoadnum[1].IndexOf("號");
                        if (roadsNum > 0)
                        {
                            maskchar = Maskchar("*", roadsNum);

                            val = maskRoadnum[0] + maskchar + "號";

                            if (maskRoadnum[1].IndexOf("樓") > 0)
                            {
                                //台南市中正路321巷
                                //17號2樓
                                SplitStr(maskRoadnum[1], "樓", out maskFloornum);
                                if (maskFloornum != null)
                                {
                                    int floorNum = maskFloornum[0].IndexOf("樓");
                                    if (floorNum > 0)
                                    {
                                        maskchar = Maskchar("*", floorNum - roadsNum);

                                        val += maskchar + "樓";

                                        //樓後面有字元
                                        if (maskFloornum[1] != "")
                                        {
                                            maskchar = Maskchar("*", maskFloornum[1].Length);

                                            val += maskchar;
                                        }
                                    }

                                }
                                else
                                {
                                    maskchar = Maskchar("*", maskRoadnum[1].Length);

                                    val += maskchar;
                                }
                            }
                            else
                            {
                                maskchar = Maskchar("*", maskRoadnum[1].Length);

                                val += maskchar;
                            }
                        }
                        else
                        {
                            maskchar = Maskchar("*", maskRoadnum[1].Length);

                            val = val.Replace(maskRoadnum[1], maskchar);
                        }
                    }
                }
                else
                {
                    maskchar = Maskchar("*", val.Length);

                    val = val.Replace(val, maskchar);
                }
            }
            else
            {
                val = "";
            }
            return val;


        }
        /// 

        /// 身分證字號=>遮前五個字元
        /// 

        ///A123456789
        /// A1234*****
        private static string MaskId(string val)
        {
            string maskstr, maskchar;
            maskchar = null;
            if (!string.IsNullOrEmpty(val))
            {
                if (Regex.IsMatch(val, RegularExp.Id))
                {
                    int End = (int)(val.Length - 5);
                    maskstr = val.Substring(5, End);
                    for (int i = 0; i < maskstr.Length; i++)
                    {
                        maskchar = maskchar + "*";
                    }
                    val = val.Substring(0, 5) + maskchar;
                }
                else
                {

                    val = "";

                }
            }
            else
            {

                val = "";
            }
            return val;


        }
        /// 

        /// 聯絡方式(電話) =>固定遮後四碼
        /// 

        ///02-12345678
        /// 02-1234****
        private static string MaskTel(string val)
        {
            string maskstr, maskchar;
            maskchar = "****";
            if (!string.IsNullOrEmpty(val))
            {
                int tal = Regex.Replace(val, RegularExp.allsc, "").Length;
                int Strl = tal - 4;
                int minus = val.IndexOf("-") > 1 ? Strl + 1 : Strl;
                maskstr = val.Substring(minus, 4); //;val.Substring(Strl, 4);
                val = val.Substring(0, minus) + maskchar;

            }
            else
            {

                val = "";
            }
            return val;


        }
        /// 

        /// 手機號碼 => 10位數從哪位數開始 遮幾碼
        /// 

        /// 0912345678
        /// 0912***678 第五位 遮三碼
        public static string MaskMobile(string val, int startLen, int num)
        {
            string maskchar = string.Empty;

            maskchar = Maskchar("*", num);


            if (!string.IsNullOrEmpty(val))
            {
                if (val.Length > startLen + num)
                {
                    if (startLen != 0)
                    {
                        string startstr = val.Substring(0, startLen - 1);

                        int endindex = val.Length - (startLen + num - 1);

                        string laststr = val.Substring(startLen + num - 1, endindex);

                        val = startstr + maskchar + laststr;
                    }
                    else
                    {
                        val = val.Replace(val, maskchar);
                    }
                }
                else
                {
                    val = val.Replace(val, maskchar);
                }
            }
            else
            {

                val = "";
            }
            return val;


        }

        protected static string Maskchar(string mark, int num)
        {
            string result = string.Empty;

            for (int i = 0; i < num; i++)
            {
                result += mark;
            }

            return result;
        }
    }
}
public struct RegularExp
{
    //所有特殊字元
    public const string allsc = @"[\W_]+";
    public const string Chinese = @"^[\u4E00-\u9FA5\uF900-\uFA2D]+$";
    public const string Color = "^#[a-fA-F0-9]{6}";
    public const string Date = @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$";
    public const string DateTime = @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-)) (20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d$";
    public const string Email = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
    public const string Float = @"^(-?\d+)(\.\d+)?$";
    public const string ImageFormat = @"\.(?i:jpg|bmp|gif|ico|pcx|jpeg|tif|png|raw|tga)$";
    public const string Integer = @"^-?\d+$";
    public const string IP = @"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$";
    public const string Letter = "^[A-Za-z]+$";
    public const string LowerLetter = "^[a-z]+$";
    public const string MinusFloat = @"^(-(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*)))$";
    public const string MinusInteger = "^-[0-9]*[1-9][0-9]*$";
    public const string Mobile = @"[0-9]{4}\-[0-9]{3}\-[0-9]{3}";
    public const string NumbericOrLetterOrChinese = @"^[A-Za-z0-9\u4E00-\u9FA5\uF900-\uFA2D]+$";
    public const string Numeric = "^[0-9]+$";
    public const string NumericOrLetter = "^[A-Za-z0-9]+$";
    public const string NumericOrLetterOrUnderline = @"^\w+$";
    public const string PlusFloat = @"^(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$";
    public const string PlusInteger = "^[0-9]*[1-9][0-9]*$";
    public const string Telephone = @"[0-9]{2}\-[0-9]{7}";
    public const string UnMinusFloat = @"^\d+(\.\d+)?$";
    public const string UnMinusInteger = @"\d+$";
    public const string UnPlusFloat = @"^((-\d+(\.\d+)?)|(0+(\.0+)?))$";
    public const string UnPlusInteger = @"^((-\d+)|(0+))$";
    public const string UpperLetter = "^[A-Z]+$";
    public const string Url = @"^http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$";
    public const string creditcard = @"^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$";
    //([A-Z]|[a-z])\d{9}
    public const string Id = @"([A-Z]|[a-z])\d{9}";
}