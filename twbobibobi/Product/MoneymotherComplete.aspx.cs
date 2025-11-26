using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using twbobibobi.Data;
using Temple.data;
using System.Globalization;
using System.Web.Services;
using Read.data;
using Org.BouncyCastle.Asn1.Ocsp;

namespace twbobibobi.Product
{
    public partial class MoneymotherComplete : AjaxBasePage
    {
        public string OrderItemTag, OrderNumString, OrderName, OrderTel, OrderEmail, OrderAdd, OrderZipCode, OrderinvoiceType, OrderinvoiceNum, OrderCarrierCode, OrderinvCode, OrderinvName;

        public int OrderItemAcount = 0, 
            OrderItemBcount = 0, 
            OrderItemCcount = 0, 
            OrderItemDcount = 0, 
            OrderItemEcount = 0, 
            OrderItemFcount = 0,
            OrderItemGcount = 0,
            OrderItemHcount = 0,
            OrderItemIcount = 0,
            OrderItemJcount = 0,
            OrderTotal = 0;

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
                    string Year = "2026";

                    int.TryParse(Request["aid"], out ApplicantID);
                    int.TryParse(Request["a"], out AdminID);

                    DataTable dtApplicantInfo = objLightDAC.Getapplicantinfo_Moneymother(ApplicantID, AdminID, Year);
                    if (dtApplicantInfo.Rows.Count > 0)
                    {
                        int.TryParse(dtApplicantInfo.Rows[0]["AppCost"].ToString(), out OrderTotal);
                        OrderNumString = dtApplicantInfo.Rows[0]["Num2String"].ToString();
                        OrderName = dtApplicantInfo.Rows[0]["AppName"].ToString();
                        OrderTel = dtApplicantInfo.Rows[0]["AppMobile"].ToString();
                        OrderEmail = dtApplicantInfo.Rows[0]["AppEmail"].ToString();
                        OrderAdd = dtApplicantInfo.Rows[0]["Address"].ToString();
                        OrderZipCode = dtApplicantInfo.Rows[0]["ZipCode"].ToString();
                        string invType = dtApplicantInfo.Rows[0]["InvoiceType"].ToString();

                        if (!string.IsNullOrEmpty(invType))
                        {
                            switch (invType)
                            {
                                case "1": OrderinvoiceType = "一般電子發票"; break;
                                case "2": OrderinvoiceType = "手機載具發票"; break;
                                //case "3": OrderinvoiceType = "一般電子發票"; break;
                                case "4": OrderinvoiceType = "公司發票"; break;
                            }
                        }

                        if (!string.IsNullOrEmpty(dtApplicantInfo.Rows[0]["InvoiceNumber"].ToString()))
                        {
                            OrderinvoiceNum = dtApplicantInfo.Rows[0]["InvoiceNumber"].ToString().Insert(2, "-");
                        }

                        this.orderCCode.Visible = false;
                        this.orderCompany.Visible = false;
                        if (invType == "2")
                        {
                            this.orderCCode.Visible = true;

                            OrderCarrierCode = dtApplicantInfo.Rows[0]["CarrierCode"].ToString();
                        }
                        else if (invType == "4")
                        {
                            this.orderCompany.Visible = true;

                            OrderinvCode = dtApplicantInfo.Rows[0]["BuyerIdentifier"].ToString();
                            OrderinvName = dtApplicantInfo.Rows[0]["BuyerName"].ToString();
                        }

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
                        this.OrderItemH.Visible = false;
                        this.OrderItemI.Visible = false;
                        this.OrderItemJ.Visible = false;

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

                        DataTable dtProductInfo_H = objLightDAC.GetProduct_Moneymother(ApplicantID, 9, Year);
                        if (dtProductInfo_H.Rows.Count > 0)
                        {
                            if (int.TryParse(dtProductInfo_H.Rows[0]["Count"].ToString(), out OrderItemHcount))
                            {
                                this.OrderItemH.Visible = true;
                            }
                            else
                            {
                                this.OrderItemH.Visible = false;
                            }
                        }

                        DataTable dtProductInfo_I = objLightDAC.GetProduct_Moneymother(ApplicantID, 10, Year);
                        if (dtProductInfo_I.Rows.Count > 0)
                        {
                            if (int.TryParse(dtProductInfo_I.Rows[0]["Count"].ToString(), out OrderItemIcount))
                            {
                                this.OrderItemI.Visible = true;
                            }
                            else
                            {
                                this.OrderItemI.Visible = false;
                            }
                        }

                        DataTable dtProductInfo_J = objLightDAC.GetProduct_Moneymother(ApplicantID, 11, Year);
                        if (dtProductInfo_J.Rows.Count > 0)
                        {
                            if (int.TryParse(dtProductInfo_J.Rows[0]["Count"].ToString(), out OrderItemJcount))
                            {
                                this.OrderItemJ.Visible = true;
                            }
                            else
                            {
                                this.OrderItemJ.Visible = false;
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