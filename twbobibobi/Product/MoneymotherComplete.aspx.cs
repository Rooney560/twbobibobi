using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MotoSystem.Data;
using Temple.data;
using System.Globalization;
using System.Web.Services;
using Read.data;
using Org.BouncyCastle.Asn1.Ocsp;

namespace twbobibobi.Product
{
    public partial class MoneymotherComplete : AjaxBasePage
    {
        public string OrderItemTag, OrderNumString, OrderName, OrderTel, OrderAdd, OrderZipCode;

        public int OrderItemAcount = 0, OrderItemBcount = 0, OrderItemCcount = 0, OrderItemDcount = 0, OrderItemEcount = 0, OrderItemFcount = 0, OrderItemGcount = 0, OrderTotal = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request["aid"] != null && Request["a"] != null)
                {
                    TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                    DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                    LightDAC objLightDAC = new LightDAC(this);

                    int ApplicantID = 0;
                    int AdminID = 0;
                    string Year = "2025";

                    int.TryParse(Request["aid"], out ApplicantID);
                    int.TryParse(Request["a"], out AdminID);

                    DataTable dtApplicantInfo = objLightDAC.Getapplicantinfo_Moneymother(ApplicantID, AdminID, Year);
                    if (dtApplicantInfo.Rows.Count > 0)
                    {
                        int.TryParse(dtApplicantInfo.Rows[0]["AppCost"].ToString(), out OrderTotal);
                        OrderNumString = dtApplicantInfo.Rows[0]["Num2String"].ToString();
                        OrderName = dtApplicantInfo.Rows[0]["AppName"].ToString();
                        OrderTel = dtApplicantInfo.Rows[0]["Mobile"].ToString();
                        OrderAdd = dtApplicantInfo.Rows[0]["Address"].ToString();
                        OrderZipCode = dtApplicantInfo.Rows[0]["ZipCode"].ToString();

                        if (dtApplicantInfo.Rows[0]["DiscountType"] != DBNull.Value)
                        {
                            string type = dtApplicantInfo.Rows[0]["DiscountType"].ToString();
                            int cost = int.Parse(dtApplicantInfo.Rows[0]["DisCost"].ToString());
                            if (type == "0")
                            {
                                //有金額折扣
                                //OrderTotal = OrderTotal - cost;
                            }
                            else
                            {
                                //Free
                                OrderTotal = 0;

                            }
                        }

                        this.OrderItemA.Visible = false;
                        this.OrderItemD.Visible = false;
                        this.OrderItemE.Visible = false;
                        this.OrderItemF.Visible = false;
                        this.OrderItemG.Visible = false;

                        DataTable dtProductInfo_A = objLightDAC.GetProduct_Moneymother(ApplicantID, 1, Year);
                        if (dtProductInfo_A.Rows.Count > 0)
                        {
                            if (int.TryParse(dtProductInfo_A.Rows[0]["Count"].ToString(), out OrderItemAcount))
                            {
                                this.OrderItemA.Visible = true;
                            }
                            else
                            {
                                this.OrderItemA.Visible = false;
                            }
                        }

                        //DataTable dtProductInfo_B = objLightDAC.GetProduct_Moneymother(ApplicantID, 3, Year);
                        //if (dtProductInfo_B.Rows.Count > 0)
                        //{
                        //    OrderItemBcount = dtProductInfo_B.Rows.Count;
                        //}

                        //DataTable dtProductInfo_C = objLightDAC.GetProduct_Moneymother(ApplicantID, 4, Year);
                        //if (dtProductInfo_C.Rows.Count > 0)
                        //{
                        //    OrderItemCcount = dtProductInfo_C.Rows.Count;
                        //}

                        DataTable dtProductInfo_D = objLightDAC.GetProduct_Moneymother(ApplicantID, 5, Year);
                        if (dtProductInfo_D.Rows.Count > 0)
                        {
                            if (int.TryParse(dtProductInfo_D.Rows[0]["Count"].ToString(), out OrderItemDcount))
                            {
                                this.OrderItemD.Visible = true;
                            }
                            else
                            {
                                this.OrderItemD.Visible = false;
                            }
                        }

                        DataTable dtProductInfo_E = objLightDAC.GetProduct_Moneymother(ApplicantID, 6, Year);
                        if (dtProductInfo_E.Rows.Count > 0)
                        {
                            if (int.TryParse(dtProductInfo_E.Rows[0]["Count"].ToString(), out OrderItemEcount))
                            {
                                this.OrderItemE.Visible = true;
                            }
                            else
                            {
                                this.OrderItemE.Visible = false;
                            }
                        }

                        DataTable dtProductInfo_F = objLightDAC.GetProduct_Moneymother(ApplicantID, 7, Year);
                        if (dtProductInfo_F.Rows.Count > 0)
                        {
                            if (int.TryParse(dtProductInfo_F.Rows[0]["Count"].ToString(), out OrderItemFcount))
                            {
                                this.OrderItemF.Visible = true;
                            }
                            else
                            {
                                this.OrderItemF.Visible = false;
                            }
                        }

                        DataTable dtProductInfo_G = objLightDAC.GetProduct_Moneymother(ApplicantID, 8, Year);
                        if (dtProductInfo_G.Rows.Count > 0)
                        {
                            if (int.TryParse(dtProductInfo_G.Rows[0]["Count"].ToString(), out OrderItemGcount))
                            {
                                this.OrderItemG.Visible = true;
                            }
                            else
                            {
                                this.OrderItemG.Visible = false;
                            }
                        }
                    }
                }
                else
                {
                    Response.Write("<script>alert('訪問網址錯誤。');location='https://bobibobi.tw/Product/MoneymotherIndex.aspx'</script>");
                }
            }
        }
    }
}