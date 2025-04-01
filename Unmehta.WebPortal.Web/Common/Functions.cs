using System;
using BO;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Common;
using static Unmehta.WebPortal.Model.Common.EnumClass;
using System.Net;
using System.Text;
using System.Security.Cryptography;
using Unmehta.WebPortal.Repository.Interface.Payment;
using Unmehta.WebPortal.Repository.Repository.Payment;
using System.Reflection;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Net.Mail;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using Newtonsoft.Json;
using Unmehta.WebPortal.Model.Common;
using System.ServiceModel.Channels;
using RestSharp;
using BAL;

namespace Unmehta.WebPortal.Web.Common
{

    public class AgeModel
    {
        public int Years { get; set; }
        public int Months { get; set; }
        public int Days { get; set; }
    }
    public static class DateTimeExtensions
    {
        public static AgeModel ToAgeString(this DateTime dob, DateTime today)
        {
            if (today == null)
            {
                today = DateTime.Today;
            }

            int months = today.Month - dob.Month;
            int years = today.Year - dob.Year;

            if (today.Day < dob.Day)
            {
                months--;
            }

            if (months < 0)
            {
                years--;
                months += 12;
            }

            int days = (today - dob.AddMonths((years * 12) + months)).Days;

            AgeModel ageModel = new AgeModel();
            ageModel.Years=years;
            ageModel.Months=months;
            ageModel.Days=days;

            return ageModel;
        }
    }

    public class MainLine
    {
        public string type { get; set; }
        public string name { get; set; }
        public bool showInLegend { get; set; }
        public string xValueFormatString { get; set; }
        public List<SubDetails> dataPoints { get; set; }
        public List<SubDetailsLine> dataSubDetailsLine { get; set; }
    }

    public class SubDetails
    {
        public long y { get; set; }
        public DateTime x { get; set; }
    }

    public class SubDetailsLine
    {
        public long y { get; set; }
        public string x { get; set; }
    }


    public static class BilldeskPayment
    {
        private static string strSqlConnectionString = ConfigurationManager.ConnectionStrings["UNMehtaConnectionString"].ToString();

        private static string GetRandomNumberStringForPayment()
        {
            Random generator = new Random();
            string r = generator.Next(0, 999999999).ToString("D9");
            return r;
        }

        private static string GetHMACSHA256(string text, string key)
        {
            UTF8Encoding encoder = new UTF8Encoding();

            byte[] hashValue;
            byte[] keybyt = encoder.GetBytes(key);
            byte[] message = encoder.GetBytes(text);

            HMACSHA256 hashString = new HMACSHA256(keybyt);
            string hex = "";

            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;
        }

        public static string PaymentRedirectToBillDesk(string ReturnURL, string feeAmount, string TransactionFor, out long id, out string strError, string ProjectName = "UNMehta", string ProjectModule = "Direct Test", string PaymentReason = " Test Page")
        {
            #region Declarations
            string Transacionid = "NA";
            string TSPLTxnCode = string.Empty;
            string TSPLtxtITC = string.Empty;
            string TransactionCode = "";
            using (IPaymentRepository objPay = new PaymentRepository(Functions.strSqlConnectionString))
            {
                loop: TransactionCode = GetRandomNumberStringForPayment();
                if (objPay.ValidatePaymentTransactionCode(TransactionCode, out strError))
                {
                    goto loop;
                }
            }
            #endregion

            #region BillDesk Data Declaration
            string MerchantID = string.Empty;
            string UniTranNo = string.Empty;
            string NA1 = string.Empty;
            string txn_amount = string.Empty;
            string NA2 = string.Empty;
            string NA3 = string.Empty;
            string NA4 = string.Empty;
            string CurrencyType = string.Empty;
            string NA5 = string.Empty;
            string TypeField1 = string.Empty;
            string SecurityID = string.Empty;
            string NA6 = string.Empty;
            string NA7 = string.Empty;
            string TypeField2 = string.Empty;
            string additional_info1 = string.Empty;
            string additional_info2 = string.Empty;
            string additional_info3 = "NA";
            string additional_info4 = string.Empty;
            string additional_info5 = string.Empty;
            string additional_info6 = string.Empty;
            string additional_info7 = string.Empty;
            string ChecksumKey = string.Empty;
            string PaymentURL = string.Empty;
            #endregion

            #region Get Payment Details
            try
            {
                if (ConfigDetailsValue.BillDeskPaymentMode == "0")
                {

                    MerchantID = ConfigDetailsValue.BillDeskTestMerchantId;

                    SecurityID = ConfigDetailsValue.BillDeskTestSecurityCode;

                    ChecksumKey = ConfigDetailsValue.BillDeskTestChecksumKey;

                    PaymentURL = ConfigDetailsValue.BillDeskTestURL;
                }
                else
                {

                    MerchantID = ConfigDetailsValue.BillDeskMerchantId;

                    SecurityID = ConfigDetailsValue.BillDeskSecurityCode;

                    ChecksumKey = ConfigDetailsValue.BillDeskChecksumKey;

                    PaymentURL = ConfigDetailsValue.BillDeskURL;
                }
            }
            catch (Exception ex)
            {
                strError = "Payment Cred is Not Found";
                id = 0;
                return "";
            }
            #endregion

            #region Payment Log for Different Transaction Id

            #endregion

            #region Validate Replace
            if (string.IsNullOrWhiteSpace(ProjectName))
            {
                ProjectName = "NA";
            }
            if (string.IsNullOrWhiteSpace(ProjectModule))
            {
                ProjectModule = "NA";
            }
            if (string.IsNullOrWhiteSpace(PaymentReason))
            {
                PaymentReason = "NA";
            }
            #endregion

            #region Set Bill Desk Param Data
            UniTranNo = TransactionCode;
            txn_amount = feeAmount;
            CurrencyType = "INR";
            additional_info1 = ProjectName; // Project Name
            additional_info2 = ProjectModule; // Project Code
            additional_info3 = TransactionFor; // Transaction for??
            additional_info4 = PaymentReason; // Payment Reason
            additional_info5 = feeAmount; // Amount Passed
            additional_info6 = Transacionid; // Record Id
            additional_info7 = "NA";

            //ReturnURL = ConfigurationManager.AppSettings["PaymentPath"] + "PaymentResponse.aspx";
            #endregion

            #region Generate Bill Desk Check Sum

            StringBuilder billRequest = new StringBuilder();
            billRequest.Append(MerchantID).Append("|");
            billRequest.Append(UniTranNo).Append("|");
            billRequest.Append("NA").Append("|");
            billRequest.Append(txn_amount).Append("|");
            billRequest.Append("NA").Append("|");
            billRequest.Append("NA").Append("|");
            billRequest.Append("NA").Append("|");
            billRequest.Append(CurrencyType).Append("|");
            billRequest.Append("DIRECT").Append("|");
            billRequest.Append("R").Append("|");
            billRequest.Append(SecurityID).Append("|");
            billRequest.Append("NA").Append("|");
            billRequest.Append("NA").Append("|");
            billRequest.Append("F").Append("|");
            billRequest.Append(additional_info1).Append("|");
            billRequest.Append(additional_info2).Append("|");
            billRequest.Append("NA").Append("|");
            billRequest.Append(additional_info4).Append("|");
            billRequest.Append(additional_info5).Append("|");
            billRequest.Append(additional_info6).Append("|");
            billRequest.Append(additional_info7).Append("|");
            billRequest.Append(ReturnURL);

            string data = billRequest.ToString();

            String hash = String.Empty;
            hash = GetHMACSHA256(data, ChecksumKey);
            hash = hash.ToUpper();

            string msg = data + "|" + hash;

            #endregion

            #region Post to BillDesk Payment Gateway

            PaymentURL = PaymentURL + msg;

            #endregion

            #region Save Transaction For Record
            using (IPaymentRepository objPay = new PaymentRepository(strSqlConnectionString))
            {
                objPay.InsertOrUpdatePaymentRequest(PaymentURL, TransactionCode, MerchantID, UniTranNo, NA1, feeAmount, NA2, NA3, NA4, CurrencyType, NA5, TypeField1, SecurityID, NA6, NA7, TypeField2, additional_info1,
                    additional_info2, additional_info3, additional_info4, additional_info5, additional_info6, additional_info7, ReturnURL, ChecksumKey, hash, msg, out id, out strError);
            }
            #endregion

            StringBuilder strForm = new StringBuilder();
            strForm.Append("<form name='s1_2' id='s1_2' action='" + PaymentURL + "' method='post'> ");
            strForm.Append("<script type='text/javascript' language='javascript' >document.getElementById('s1_2').submit();");
            strForm.Append("</script>");
            strForm.Append("<script language='javascript' >");
            strForm.Append("</script>");
            strForm.Append("</form> ");

            return strForm.ToString();
        }

        public static bool PaymentResponseFromBillDesk(string[] BDResponse, out string returnUrl, out string ResponseMsg, out string txnStatus)
        {
            #region Declaration for Response processing
            string ErrorMessage = string.Empty;
            ResponseMsg = "";
            #endregion

            #region BillDesk Response Data
            string MerchantID = BDResponse[0].Replace('+', ' ');
            string UniTranNo = BDResponse[1].Replace('+', ' ');
            string TxnReferenceNo = BDResponse[2].Replace('+', ' ');
            string BankReferenceNo = BDResponse[3].Replace('+', ' ');
            string txn_amount = BDResponse[4].Replace('+', ' ');
            string BankID = BDResponse[5].Replace('+', ' ');
            string BankMerchantID = BDResponse[6].Replace('+', ' ');
            string TxnType = BDResponse[7].Replace('+', ' ');
            string CurrencyType = BDResponse[8].Replace('+', ' ');
            string ItemCode = BDResponse[9].Replace('+', ' ');
            string SecurityType = BDResponse[10].Replace('+', ' ');
            string SecurityID = BDResponse[11].Replace('+', ' ');
            string SecurityPasswod = BDResponse[12].Replace('+', ' ');
            string TxnDate = BDResponse[13].Replace('+', ' ');
            string AuthStatus = BDResponse[14].Replace('+', ' ');
            string SettlementType = BDResponse[15].Replace('+', ' ');
            string additional_info1 = BDResponse[16].Replace('+', ' ');
            string additional_info2 = BDResponse[17].Replace('+', ' ');
            string additional_info3 = BDResponse[18].Replace('+', ' ');
            string additional_info4 = BDResponse[19].Replace('+', ' ');
            string additional_info5 = BDResponse[20].Replace('+', ' ');
            string additional_info6 = BDResponse[21].Replace('+', ' ');
            string additional_info7 = BDResponse[22].Replace('+', ' ');
            string ErrorStatus = BDResponse[23].Replace('+', ' ');
            string errorDescription = BDResponse[24].Replace('+', ' ');
            String Checksum = BDResponse[25].Replace('+', ' ');
            #endregion

            #region Generate Bill Desk Check Sum
            StringBuilder billRequest = new StringBuilder();
            billRequest.Append(MerchantID).Append("|");
            billRequest.Append(UniTranNo).Append("|");
            billRequest.Append(TxnReferenceNo).Append("|");
            billRequest.Append(BankReferenceNo).Append("|");
            billRequest.Append(txn_amount).Append("|");
            billRequest.Append(BankID).Append("|");
            billRequest.Append(BankMerchantID).Append("|");
            billRequest.Append(TxnType).Append("|");
            billRequest.Append(CurrencyType).Append("|");
            billRequest.Append(ItemCode).Append("|");
            billRequest.Append(SecurityType).Append("|");
            billRequest.Append(SecurityID).Append("|");
            billRequest.Append(SecurityPasswod).Append("|");
            billRequest.Append(TxnDate).Append("|");
            billRequest.Append(AuthStatus).Append("|");
            billRequest.Append(SettlementType).Append("|");
            billRequest.Append(additional_info1).Append("|");
            billRequest.Append(additional_info2).Append("|");
            billRequest.Append(additional_info3).Append("|");
            billRequest.Append(additional_info4).Append("|");
            billRequest.Append(additional_info5).Append("|");
            billRequest.Append(additional_info6).Append("|");
            billRequest.Append(additional_info7).Append("|");
            billRequest.Append(ErrorStatus).Append("|");
            billRequest.Append(errorDescription);

            string data = billRequest.ToString();

            //string ChecksumKey = "RLutg0ougPs3";
            returnUrl = additional_info2;
            string ChecksumKey = "";
            #region Get Payment Details
            try
            {
                if (ConfigDetailsValue.BillDeskPaymentMode == "0")
                {
                    ChecksumKey = ConfigDetailsValue.BillDeskTestChecksumKey;
                }
                else
                {
                    ChecksumKey = ConfigDetailsValue.BillDeskChecksumKey;
                }
            }
            catch (Exception ex)
            {
                txnStatus = "Payment Faild";
                ResponseMsg = "Something went wrong. Please try again.";
                return false;
            }
            #endregion


            String hash = String.Empty;
            hash = GetHMACSHA256(data, ChecksumKey);
            hash = hash.ToUpper();

            #endregion

            #region Payment Transaction Update

            string txnMessage = string.Empty;
            string txnMode = string.Empty;

            if (hash == Checksum)
            {
                using (IPaymentRepository objPay = new PaymentRepository(strSqlConnectionString))
                {
                    objPay.UpdatePaymentTransactionRequest(UniTranNo, true, out txnStatus);
                }

                #region Get Transaction Details
                if (AuthStatus == "0300")
                {
                    txnMessage = "Successful Transaction";
                    txnStatus = "Success";
                }
                else if (AuthStatus == "0399")
                {
                    txnMessage = "Cancel Transaction";
                    txnStatus = "Invalid Authentication at Bank";
                }
                else if (AuthStatus == "NA")
                {
                    txnMessage = "Cancel Transaction";
                    txnStatus = "Invalid Input in the Request Message";
                }
                else if (AuthStatus == "0002")
                {
                    txnMessage = "Cancel Transaction";
                    txnStatus = "BillDesk is waiting for Response from Bank";
                }
                else if (AuthStatus == "0001")
                {
                    txnMessage = "Cancel Transaction";
                    txnStatus = "Error at BillDesk";
                }
                else
                {
                    txnMessage = "Something went wrong. Try Again!.";
                    txnStatus = "Payment Faild";
                }
                #endregion

                #region Transaction Type
                if (TxnType == "01")
                    txnMode = "Netbanking";
                else if (TxnType == "02")
                    txnMode = "Credit Card";
                else if (TxnType == "03")
                    txnMode = "Debit Card";
                else if (TxnType == "04")
                    txnMode = "Cash Card";
                else if (TxnType == "05")
                    txnMode = "Mobile Wallet";
                else if (TxnType == "06")
                    txnMode = "IMPS";
                else if (TxnType == "07")
                    txnMode = "Reward Points";
                else if (TxnType == "08")
                    txnMode = "Rupay";
                #endregion

                #region Assign Values to objEntity
                string TxRefNo = TxnReferenceNo;
                string PgTxnNo = BankReferenceNo;
                decimal TxnAmount = Convert.ToDecimal(txn_amount);
                string TxStatus = txnStatus;
                string TxMssg = txnMessage;
                string TransactionType = "Online";
                string PaymentFor = TxnType;
                #endregion

                using (IPaymentRepository objPay = new PaymentRepository(strSqlConnectionString))
                {
                    objPay.InsertOrUpdatePaymentResponse("", MerchantID, UniTranNo, TxRefNo, BankReferenceNo, txn_amount, BankID, BankMerchantID, TxnType, CurrencyType, ItemCode, SecurityType, SecurityID, SecurityPasswod, TxnDate,
                        AuthStatus, SettlementType, additional_info1, additional_info2, additional_info3, additional_info4, additional_info5, additional_info6, additional_info7, ErrorStatus, errorDescription, hash, true, TxRefNo, PgTxnNo, txn_amount,
                        TxStatus, TxMssg, PaymentFor, out ResponseMsg);
                }

                if (txnStatus == "Success")
                {
                    ResponseMsg = "Payment success.";
                    return true;
                    //Redirect to acknowledgement page
                    //Response.Redirect("~/Payments/Acknowledgement.aspx", false);
                }
                else
                {
                    ResponseMsg = "Please try again.";
                    return true;
                    //Redirect to acknowledgement page
                    //Response.Redirect("~/Payments/PaymentSelection.aspx", false);
                }
            }
            else
            {


                txnMessage = "Something went wrong. Try Again!.";
                txnStatus = "Payment Faild";
                ResponseMsg = "Something went wrong. Please try again.";

                using (IPaymentRepository objPay = new PaymentRepository(strSqlConnectionString))
                {
                    objPay.InsertOrUpdatePaymentResponse("", MerchantID, UniTranNo, TxnReferenceNo, BankReferenceNo, txn_amount, BankID, BankMerchantID, TxnType, CurrencyType, ItemCode, SecurityType, SecurityID, SecurityPasswod, TxnDate,
                        AuthStatus, SettlementType, additional_info1, additional_info2, additional_info3, additional_info4, additional_info5, additional_info6, additional_info7, ErrorStatus, errorDescription, hash, false, TxnReferenceNo, BankReferenceNo, txn_amount,
                        txnStatus, txnMessage, TxnType, out ResponseMsg);
                }
                return false;
            }

            #endregion
        }


    }


    public static class Functions
    {
        #region Static Member Value 

        public static string strSqlConnectionString = ConfigurationManager.ConnectionStrings["UNMehtaConnectionString"].ToString();


        public static int LanguageId
        {
            get
            {
                object value = HttpContext.Current.Session["LanguageId"];
                return value == null ? 1 : (int)value;
            }
            set
            {
                HttpContext.Current.Session["LanguageId"] = value;
            }
        }

        #endregion

        #region Encode Decode Methods
        public static string Decrypt(string strEncText, bool isHtmlEncode = false)
        {
            string strDecrypt = EncryptDecrypt.DecryptUsingCBC(strEncText);
            //return isHtmlEncode ? HttpUtility.UrlDecode(strDecrypt) : strDecrypt;
            return strDecrypt;
        }
        public static string ToHexString(string str)
        {
            var sb = new StringBuilder();

            var bytes = Encoding.Unicode.GetBytes(str);
            foreach (var t in bytes)
            {
                sb.Append(t.ToString("X2"));
            }

            return sb.ToString(); // returns: "48656C6C6F20776F726C64" for "Hello world"
        }

        public static string ToHexString(byte[] bytes)
        {
            var sb = new StringBuilder();
            
            foreach (var t in bytes)
            {
                sb.Append(t.ToString("X2"));
            }

            return sb.ToString(); // returns: "48656C6C6F20776F726C64" for "Hello world"
        }

        public static string FromHexString(string hexString)
        {
            var bytes = new byte[hexString.Length / 2];
            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }

            return Encoding.Unicode.GetString(bytes); // returns: "Hello world" for "48656C6C6F20776F726C64"
        }

        public static string Encrypt(string strDecText, bool isHtmlEncode = false)
        {
            string strEncrypt = EncryptDecrypt.EncryptUsingCBC(strDecText);
            //return isHtmlEncode ? HttpUtility.UrlEncode(strEncrypt) : strEncrypt;
            return strEncrypt;
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        #endregion

        #region Static Function

        #region Graph Details

        public static string GenerateScriptGraph(long ChartId, string controller, out string htmlString, bool isPopup = false)
        {
            StringBuilder strChartScript = new StringBuilder();
            htmlString = "";

            using (IStatisticsChartRepository objPatientsEducationBrochureRepository = new StatisticsChartRepository(Functions.strSqlConnectionString))
            {
                GetAllStatisticsChartMasterResult chartMainDetails = objPatientsEducationBrochureRepository.GetStatisticsChartById(ChartId);
                if (chartMainDetails != null)
                {
                    List<GetAllStatisticsChartMasterColumnListByChartIdResult> chartColumnDetails = objPatientsEducationBrochureRepository.GetAllStatisticsChartColumnListByChartId(ChartId);

                    List<GetAllStatisticsChartMasterDetailsByChartIdResult> dataRelated = objPatientsEducationBrochureRepository.GetAllStatisticsChartDetailsByChartId(ChartId).ToList();


                    switch (chartMainDetails.ChartType)
                    {
                        case "ColumnChart":
                            {
                                strChartScript.Append(GetBarChartDetails(controller, chartMainDetails, chartColumnDetails, dataRelated, isPopup));
                                break;
                            }
                        case "PIEChart":
                            {
                                strChartScript.Append(GetPieChartDetails(controller, chartMainDetails, chartColumnDetails, dataRelated, isPopup));
                                break;
                            }
                        case "LineChart":
                            {
                                strChartScript.Append(GetLineChartDetails(controller, chartMainDetails, chartColumnDetails, dataRelated, isPopup));
                                break;
                            }
                        case "ColumnChartswithMultipleAxes":
                            {
                                strChartScript.Append(GetColumnChartswithMultipleAxesDetails(controller, chartMainDetails, chartColumnDetails, dataRelated, isPopup));
                                break;
                            }
                        default:
                            throw new Exception("Unexpected Case");
                    }

                    htmlString = ConvertDataTableToHTML(ChartId);

                }
            }
            return strChartScript.ToString();
        }


        private static string GetColumnChartswithMultipleAxesDetails(string controller, GetAllStatisticsChartMasterResult chartMainDetails, List<GetAllStatisticsChartMasterColumnListByChartIdResult> chartColumnDetails, List<GetAllStatisticsChartMasterDetailsByChartIdResult> dataRelated, bool isPopup = false)
        {
            StringBuilder strChartScript = new StringBuilder();
            strChartScript.Append("");

            if (!isPopup)
            {
                strChartScript.Append("window.onload = function () {                            ");
            }
            strChartScript.Append("var chart = new CanvasJS.Chart(\"" + controller + "\", { ");
            strChartScript.Append("width:908,animationEnabled: true,                                 ");
            strChartScript.Append("theme: \"light2\",                                      ");
            strChartScript.Append("title: {");
            strChartScript.Append("text: \"" + chartMainDetails.ChartName + "\"");
            strChartScript.Append("},");


            if (!string.IsNullOrWhiteSpace(chartMainDetails.XValueName))
            {
                strChartScript.Append("axisX:{");
                strChartScript.Append("title: \"" + chartMainDetails.XValueName + "\",");
                strChartScript.Append("valueFormatString: \"" + chartMainDetails.XValueFormate + "\",");
                strChartScript.Append("crosshair: {");
                strChartScript.Append("enabled: true,");
                strChartScript.Append("snapToDataPoint: true");
                strChartScript.Append("}");
                strChartScript.Append("},");
            }

            if (!string.IsNullOrWhiteSpace(chartMainDetails.YValueName))
            {
                strChartScript.Append("	axisY : {");
                strChartScript.Append("title: \"" + chartMainDetails.YValueName + "\",");
                strChartScript.Append("includeZero: true,");
                strChartScript.Append("crosshair: {");
                strChartScript.Append("	enabled: true");
                strChartScript.Append("}");
                strChartScript.Append("},");

            }

            strChartScript.Append("toolTip: {");
            strChartScript.Append("shared: true");
            strChartScript.Append("},");
            strChartScript.Append("legend: {");
            strChartScript.Append("cursor: \"pointer\",");
            strChartScript.Append("verticalAlign: \"bottom\",");
            strChartScript.Append("horizontalAlign: \"center\",");
            strChartScript.Append("dockInsidePlotArea: false,");
            strChartScript.Append("itemclick: toogleDataSeries");
            strChartScript.Append("},");

            List<MainLine> lstMainLine = new List<MainLine>();



            foreach (var yAxis in chartColumnDetails.Where(x => x.TypeColumn == "Y"))
            {
                MainLine objData = new MainLine();

                objData.type = "column";
                objData.name = yAxis.ColName;
                objData.showInLegend = true;
                objData.xValueFormatString = chartMainDetails.XValueFormate.ToUpper();
                objData.dataSubDetailsLine = new List<SubDetailsLine>();

                foreach (var row in dataRelated.Select(x => x.SequanceNo).Distinct().ToList())
                {

                    var xColDetails = chartColumnDetails.Where(x => x.TypeColumn == "X").FirstOrDefault();

                    var sequanceData = dataRelated.Where(x => x.SequanceNo == row && x.ColumnId == yAxis.Id).OrderBy(x => x.TypeColumn).FirstOrDefault();
                    var sequancexData = dataRelated.Where(x => x.SequanceNo == row && x.ColumnId == xColDetails.Id).OrderBy(x => x.TypeColumn).FirstOrDefault();

                    SubDetailsLine objSubData = new SubDetailsLine();

                    objSubData.y = Convert.ToInt32(sequanceData.ColumnValue);

                    //IFormatProvider culture = new CultureInfo("en-US", true);
                    //DateTime dateVal = DateTime.ParseExact(sequancexData.ColumnValue, chartMainDetails.XValueFormate.ToLower(), culture);
                    IFormatProvider culture = new CultureInfo("en-US", true);
                    DateTime dateVal = DateTime.ParseExact(sequancexData.ColumnValue, chartMainDetails.XValueFormate.ToLower(), culture);
                    objSubData.x = dateVal.ToString(chartMainDetails.XValueFormate.ToLower());

                    objData.dataSubDetailsLine.Add(objSubData);

                }

                lstMainLine.Add(objData);

            }
            //JsonSerializerSettings config = new JsonSerializerSettings { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore };

            //var strDataa = JsonConvert.SerializeObject(lstMainLine, Formatting.Indented, config);
            StringBuilder strDataa = new StringBuilder();
            //var strDataa1 = JsonConvert.SerializeObject(lstMainLine,Formatting.None);
            int mainIndex = 0;
            strDataa.Append("[");
            foreach (var objMainLine in lstMainLine)
            {

                if (mainIndex != 0)
                {
                    strDataa.Append(",");
                }
                strDataa.Append("{");
                strDataa.Append("type:\"" + objMainLine.type + "\",");
                strDataa.Append("name:\"" + objMainLine.name + "\",");
                strDataa.Append("legendText:\"" + objMainLine.name + "\",");
                strDataa.Append("showInLegend:\"" + objMainLine.showInLegend + "\",");

                strDataa.Append("dataPoints:[");

                int subIndex = 0;
                foreach (var objsubLine in objMainLine.dataSubDetailsLine)
                {

                    if (subIndex != 0)
                    {
                        strDataa.Append(",");
                    }
                    strDataa.Append("{");
                    strDataa.Append("y:" + objsubLine.y + ",");
                    strDataa.Append("label:'" + objsubLine.x + "'");
                    strDataa.Append("}");

                    subIndex++;

                }
                strDataa.Append("]");
                strDataa.Append("}");

                mainIndex++;
            }
            strDataa.Append("]");

            strChartScript.Append("data:" + strDataa.ToString() + "");
            strChartScript.Append("});");
            strChartScript.Append("chart.render();");

            if (!isPopup)
            {
                strChartScript.Append("}");
            }


            string str = strChartScript.ToString();


            return str;
        }

        private static string GetLineChartDetails(string controller, GetAllStatisticsChartMasterResult chartMainDetails, List<GetAllStatisticsChartMasterColumnListByChartIdResult> chartColumnDetails, List<GetAllStatisticsChartMasterDetailsByChartIdResult> dataRelated, bool isPopup = false)
        {
            StringBuilder strChartScript = new StringBuilder();
            strChartScript.Append("");


            if (!isPopup)
            {
                strChartScript.Append("window.onload = function () {                            ");
            }
            strChartScript.Append("var chart = new CanvasJS.Chart(\"" + controller + "\", {     ");
            strChartScript.Append(" width:908,	animationEnabled: true,                                 ");
            strChartScript.Append("	theme: \"light2\",                                      ");
            strChartScript.Append("	title: {                                                ");
            strChartScript.Append("		text: \"" + chartMainDetails.ChartName + "\"");
            strChartScript.Append("	},                                                      ");
            strChartScript.Append("	axisX: {                                                ");
            strChartScript.Append("	},                                                      ");

            if (!string.IsNullOrWhiteSpace(chartMainDetails.XValueName))
            {
                strChartScript.Append("	axisX : {");
                strChartScript.Append("		intervalType: '" + (chartMainDetails.XValueFormate == "YYYY" ? "year" : "day") + "',interval: 1, title: \"" + chartMainDetails.XValueName + "\",                                  ");
                strChartScript.Append("	    valueFormatString: \"" + chartMainDetails.XValueFormate + "\",                   ");
                strChartScript.Append("		crosshair: {                                                                     ");
                strChartScript.Append("			enabled: true,                                                   ");
                strChartScript.Append("			snapToDataPoint: true                                                        ");
                strChartScript.Append("		}                                                                                ");
                strChartScript.Append("	},                                                                                   ");
            }

            if (!string.IsNullOrWhiteSpace(chartMainDetails.YValueName))
            {
                strChartScript.Append("	axisY : {");
                strChartScript.Append("		title: \"" + chartMainDetails.YValueName + "\",                 ");
                strChartScript.Append("		includeZero: true,                                  ");
                strChartScript.Append("		crosshair: {                                        ");
                strChartScript.Append("			enabled: true                                   ");
                strChartScript.Append("		}                                                   ");
                strChartScript.Append("	},                                                   ");

            }

            strChartScript.Append("	toolTip: {                                              ");
            strChartScript.Append("		shared: true                                        ");
            strChartScript.Append("	},                                                      ");
            strChartScript.Append("	legend: {                                               ");
            strChartScript.Append("		cursor: \"pointer\",                                ");
            strChartScript.Append("		verticalAlign: \"bottom\",                          ");
            strChartScript.Append("		horizontalAlign: \"center\",                          ");
            strChartScript.Append("		dockInsidePlotArea: false,                           ");
            strChartScript.Append("		itemclick: toogleDataSeries                         ");
            strChartScript.Append("	},                                                      ");

            List<MainLine> lstMainLine = new List<MainLine>();



            foreach (var yAxis in chartColumnDetails.Where(x => x.TypeColumn == "Y"))
            {
                MainLine objData = new MainLine();

                objData.type = "line";
                objData.name = yAxis.ColName;
                objData.showInLegend = true;
                objData.xValueFormatString = chartMainDetails.XValueFormate;
                objData.dataPoints = new List<SubDetails>();

                foreach (var row in dataRelated.Select(x => x.SequanceNo).Distinct().ToList())
                {

                    var xColDetails = chartColumnDetails.Where(x => x.TypeColumn == "X").FirstOrDefault();

                    var sequanceData = dataRelated.Where(x => x.SequanceNo == row && x.ColumnId == yAxis.Id).OrderBy(x => x.TypeColumn).FirstOrDefault();
                    var sequancexData = dataRelated.Where(x => x.SequanceNo == row && x.ColumnId == xColDetails.Id).OrderBy(x => x.TypeColumn).FirstOrDefault();

                    SubDetails objSubData = new SubDetails();

                    objSubData.y = Convert.ToInt32(sequanceData.ColumnValue);

                    IFormatProvider culture = new CultureInfo("en-US", true);
                    DateTime dateVal = DateTime.ParseExact(sequancexData.ColumnValue, chartMainDetails.XValueFormate.Replace("Y", "y"), culture);
                    //string dateVal = "";
                    //dateVal = Convert.ToDateTime(sequancexData.ColumnValue).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                    objSubData.x = dateVal;

                    objData.dataPoints.Add(objSubData);

                }

                lstMainLine.Add(objData);

            }
            //JsonSerializerSettings config = new JsonSerializerSettings { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore };

            //var strDataa = JsonConvert.SerializeObject(lstMainLine, Formatting.Indented, config);
            StringBuilder strDataa = new StringBuilder();
            //var strDataa1 = JsonConvert.SerializeObject(lstMainLine,Formatting.None);
            int mainIndex = 0;
            strDataa.Append("[");
            foreach (var objMainLine in lstMainLine)
            {

                if (mainIndex != 0)
                {
                    strDataa.Append("  ,");
                }
                strDataa.Append("  {");
                strDataa.Append("    type: \"" + objMainLine.type + "\",");
                strDataa.Append("    name:  \"" + objMainLine.name + "\",");
                strDataa.Append("    showInLegend:  \"" + objMainLine.showInLegend + "\",");
                if (mainIndex == 0)
                {
                    strDataa.Append("    xValueFormatString: \"" + objMainLine.xValueFormatString + "\",");
                }
                strDataa.Append("    dataPoints: [");

                int subIndex = 0;
                foreach (var objsubLine in objMainLine.dataPoints)
                {

                    if (subIndex != 0)
                    {
                        strDataa.Append("      ,");
                    }
                    strDataa.Append("      {");
                    strDataa.Append("        y: " + objsubLine.y + ",");
                    strDataa.Append("        x: new Date(" + objsubLine.x.Year + ", " + objsubLine.x.Month + ", " + objsubLine.x.Day + ")");
                    strDataa.Append("      }");

                    subIndex++;

                }
                strDataa.Append("    ]");
                strDataa.Append("      }");

                mainIndex++;
            }
            strDataa.Append("    ]");

            strChartScript.Append("	data: " + strDataa.ToString() + "                                   ");
            strChartScript.Append("});                                                      ");
            strChartScript.Append("chart.render();                                          ");


            if (!isPopup)
            {
                strChartScript.Append("}");
            }
            string str = strChartScript.ToString();


            return str;
        }

        private static string GetPieChartDetails(string controller, GetAllStatisticsChartMasterResult chartMainDetails, List<GetAllStatisticsChartMasterColumnListByChartIdResult> chartColumnDetails, List<GetAllStatisticsChartMasterDetailsByChartIdResult> dataRelated, bool isPopup = false)
        {

            StringBuilder strChartScript = new StringBuilder();
            strChartScript.Append("");

            if (!isPopup)
            {
                strChartScript.Append("window.onload = function () {");
            }
            strChartScript.Append("");
            strChartScript.Append("var chart = new CanvasJS.Chart(\"" + controller + "\", {");
            strChartScript.Append("	width:908,exportEnabled: true,");
            strChartScript.Append("	animationEnabled: true,");
            strChartScript.Append("	title:{");
            strChartScript.Append("		text: \"" + chartMainDetails.ChartName + "\"");
            strChartScript.Append("	},");
            strChartScript.Append("	legend:{");
            strChartScript.Append("		cursor: \"pointer\",");
            strChartScript.Append("		itemclick: explodePie");
            strChartScript.Append("	},");
            strChartScript.Append("	data: [{");
            strChartScript.Append("		type: \"pie\",");
            strChartScript.Append("		showInLegend: true,");
            strChartScript.Append("		toolTipContent: \"{name}: <strong>{y}</strong>\",");
            strChartScript.Append("		indexLabel: \"{name} - {y}\",");
            strChartScript.Append("		dataPoints: [");

            foreach (var row in dataRelated.Select(x => x.SequanceNo).Distinct().ToList())

            {
                var sequanceData = dataRelated.Where(x => x.SequanceNo == row).OrderBy(x => x.TypeColumn).ToList();

                var xRowData = sequanceData.FirstOrDefault(x => x.TypeColumn == "X");
                var yRowData = sequanceData.Where(x => x.TypeColumn == "Y").ToList();

                if (yRowData.Count() == 1)
                {
                    strChartScript.Append("			{ y: " + yRowData.FirstOrDefault().ColumnValue + ", name: \"" + xRowData.ColumnValue + "\" },                            ");
                }
            }

            strChartScript.Append("		]");
            strChartScript.Append("	}]");
            strChartScript.Append("});");
            strChartScript.Append("chart.render();");

            if (!isPopup)
            {
                strChartScript.Append("}");
            }
            strChartScript.Append("");


            string str = strChartScript.ToString();


            return str;
        }

        private static string GetBarChartDetails(string controller, GetAllStatisticsChartMasterResult chartMainDetails, List<GetAllStatisticsChartMasterColumnListByChartIdResult> chartColumnDetails, List<GetAllStatisticsChartMasterDetailsByChartIdResult> dataRelated, bool isPopup = false)
        {



            StringBuilder strChartScript = new StringBuilder();
            strChartScript.Append("");


            if (!isPopup)
            {
                strChartScript.Append("window.onload = function () {                                            ");
            }
            strChartScript.Append("                                                                         ");
            strChartScript.Append("var chart = new CanvasJS.Chart(\"" + controller + "\", {                     ");
            strChartScript.Append("width:908,	animationEnabled: true,                                                 ");
            strChartScript.Append("	theme: \"light2\",       ");
            strChartScript.Append("	title:{                                                                 ");
            strChartScript.Append("		text: \"" + chartMainDetails.ChartName + "\"                                          ");
            strChartScript.Append("	},                                                                      ");

            if (!string.IsNullOrWhiteSpace(chartMainDetails.XValueName))
            {
                strChartScript.Append("	axisX : {");
                strChartScript.Append("		title: \"" + chartMainDetails.XValueName + "\",                                  ");
                strChartScript.Append("	    valueFormatString: \"" + chartMainDetails.XValueFormate + "\",                   ");
                strChartScript.Append("		crosshair: {                                                                     ");
                strChartScript.Append("			enabled: true,                                                               ");
                strChartScript.Append("			snapToDataPoint: true                                                        ");
                strChartScript.Append("		}                                                                                ");
                strChartScript.Append("	},                                                                                   ");
            }

            if (!string.IsNullOrWhiteSpace(chartMainDetails.YValueName))
            {
                strChartScript.Append("	axisY : {");
                strChartScript.Append("		title: \"" + chartMainDetails.YValueName + "\",                 ");
                strChartScript.Append("		includeZero: true,                                  ");
                strChartScript.Append("		crosshair: {                                        ");
                strChartScript.Append("			enabled: true                                   ");
                strChartScript.Append("		}                                                   ");
                strChartScript.Append("	},                                                   ");

            }


            var xData = chartColumnDetails.Where(x => x.TypeColumn == "X").FirstOrDefault();

            strChartScript.Append("	data: [{                                                                ");
            strChartScript.Append("		type: \"column\",                                                   ");
            strChartScript.Append("		showInLegend: true,                                                 ");
            strChartScript.Append("		legendMarkerColor: \"grey\",                                        ");
            strChartScript.Append("		legendText: \"" + xData.ColName + "\",                        ");
            strChartScript.Append("		dataPoints: [                                                       ");
            foreach (var row in dataRelated.Select(x => x.SequanceNo).Distinct().ToList())
            {
                var sequanceData = dataRelated.Where(x => x.SequanceNo == row).OrderBy(x => x.TypeColumn).ToList();

                var xRowData = sequanceData.FirstOrDefault(x => x.TypeColumn == "X");
                var yRowData = sequanceData.Where(x => x.TypeColumn == "Y").ToList();

                if (yRowData.Count() == 1)
                {
                    strChartScript.Append("			{ y: " + yRowData.FirstOrDefault().ColumnValue + ", label: \"" + xRowData.ColumnValue + "\" },                            ");
                }
            }
            strChartScript.Append("		]                                                                   ");
            strChartScript.Append("	}]                                                                      ");
            strChartScript.Append("});                                                                      ");
            strChartScript.Append("chart.render();                                                          ");

            if (!isPopup)
            {
                strChartScript.Append("}");
            }
            string str = strChartScript.ToString();


            return str;


        }


        public static string ConvertDataTableToHTML(long ChartId)
        {
            DataTable dtStatisticsChartDetailsModelList = new System.Data.DataTable();

            using (IStatisticsChartRepository objIStatisticsChartRepository = new StatisticsChartRepository(Functions.strSqlConnectionString))
            {
                GetAllStatisticsChartMasterResult chartMainDetails = objIStatisticsChartRepository.GetStatisticsChartById(ChartId);

                if (chartMainDetails.ChartName == "Facilities at UNMICRC")
                {

                }

                var dataStatisticsChartColumnListModelList = objIStatisticsChartRepository.GetAllStatisticsChartColumnListByChartId(ChartId).ToList();


                var dataStatisticsChartDataModelList = objIStatisticsChartRepository.GetAllStatisticsChartDetailsByChartId(ChartId).OrderBy(x => x.ColumnId).ToList();



                //DataColumn column = new DataColumn();
                //{
                //    column.Caption = "Sr No";
                //    column.ColumnName = "Sr No";
                //    column.DataType = typeof(String);
                //    dtStatisticsChartDetailsModelList.Columns.Add(column);
                //}

                //DataColumn columnAlias = new DataColumn();
                //{
                //    columnAlias.Caption = "Alias Name";
                //    columnAlias.ColumnName = "Alias Name";
                //    columnAlias.DataType = typeof(String);
                //    dtStatisticsChartDetailsModelList.Columns.Add(columnAlias);
                //}

                foreach (var row in dataStatisticsChartColumnListModelList.Where(x => x.TypeColumn == "X"))
                {
                    DataColumn column1 = new DataColumn();
                    {
                        column1.Caption = row.Id.ToString();
                        column1.ColumnName = row.ColName;
                        column1.DataType = typeof(String);
                        dtStatisticsChartDetailsModelList.Columns.Add(column1);
                    }
                }


                foreach (var row in dataStatisticsChartColumnListModelList.Where(x => x.TypeColumn != "X").OrderBy(x => x.SequanceNo).ToList())
                {
                    DataColumn column1 = new DataColumn();
                    {
                        column1.Caption = row.Id.ToString();
                        column1.ColumnName = row.ColName;
                        column1.DataType = typeof(String);
                        dtStatisticsChartDetailsModelList.Columns.Add(column1);
                    }
                }

                if (dataStatisticsChartDataModelList.Count() > 0)
                {
                    int index = 1;
                    foreach (var row in dataStatisticsChartDataModelList.Select(x => x.SequanceNo).Distinct().ToList())
                    {

                        var sequanceData = dataStatisticsChartDataModelList.Where(x => x.SequanceNo == row).OrderBy(x => x.ColumnId).ToList();

                        DataRow dr = dtStatisticsChartDetailsModelList.NewRow();

                        //dr[0] = index;
                        dr[0] = sequanceData.FirstOrDefault(x => x.TypeColumn == "X").AliasName;

                        int columnIndex = 1;

                        foreach (var subRow in sequanceData.Where(x => x.TypeColumn != "X").OrderBy(x => x.SequanceNo).ToList())
                        {
                            dr[columnIndex] = subRow.ColumnValue;
                            columnIndex++;
                        }

                        dtStatisticsChartDetailsModelList.Rows.Add(dr);
                        index++;
                    }
                }
            }

            //var dt = new System.Data.DataTable();

            //#region Create Table
            //if (chartColumnDetails.Count() > 0)
            //{
            //    //DataColumn column = new DataColumn();
            //    //{
            //    //    column.Caption = "Sr No";
            //    //    column.ColumnName = "Sr No";
            //    //    column.DataType = typeof(long);
            //    //    dt.Columns.Add(column);
            //    //}

            //    foreach (var row in chartColumnDetails.Where(x => x.TypeColumn == "X"))
            //    {
            //        DataColumn column1 = new DataColumn();
            //        {
            //            column1.Caption = row.Id.ToString();
            //            column1.ColumnName = row.ColName;
            //            column1.DataType = typeof(String);
            //            dt.Columns.Add(column1);
            //        }
            //    }

            //    foreach (var row in chartColumnDetails.Where(x => x.TypeColumn != "X"))
            //    {
            //        DataColumn column1 = new DataColumn();
            //        {
            //            column1.Caption = row.Id.ToString();
            //            column1.ColumnName = row.ColName;
            //            column1.DataType = typeof(String);
            //            dt.Columns.Add(column1);
            //        }
            //    }
            //}
            //#endregion

            //#region Bind Table
            //if (dataRelated.Count() > 0)
            //{

            //    foreach (var row in dataRelated.Select(x => x.SequanceNo).Distinct().ToList())
            //    {

            //        var sequanceData = dataRelated.Where(x => x.SequanceNo == row).OrderBy(x => x.TypeColumn).ToList();

            //        DataRow dr = dt.NewRow();

            //        dr[0] = sequanceData.FirstOrDefault(x => x.TypeColumn == "X").AliasName;

            //        int columnIndex = 1;

            //        foreach (var subRow in sequanceData.Where(x => x.TypeColumn != "X"))
            //        {
            //            dr[columnIndex] = subRow.ColumnValue;
            //            columnIndex++;
            //        }

            //        dt.Rows.Add(dr);

            //    }
            //}
            //#endregion

            #region Table String HTML
            string html = "<div class=\"fixTableHead\"><table class=\"table table-hover table-center mb-0 maintable text-center\">";
            //add header row
            html += "<thead><tr>";
            for (int i = 0; i < dtStatisticsChartDetailsModelList.Columns.Count; i++)
            {
                html += "<th>" + dtStatisticsChartDetailsModelList.Columns[i].ColumnName + "</th>";
            }
            html += "</tr></thead><tbody>";
            //add rows
            for (int i = 0; i < dtStatisticsChartDetailsModelList.Rows.Count; i++)
            {
                html += "<tr>";
                for (int j = 0; j < dtStatisticsChartDetailsModelList.Columns.Count; j++)
                {
                    html += "<td>" + dtStatisticsChartDetailsModelList.Rows[i][j].ToString() + "</td>";
                }
                html += "</tr>";
            }
            html += "</tbody></table></div>";
            #endregion

            return html;
        }

        //public static string ConvertDataTableToHTML(List<GetAllStatisticsChartMasterColumnListByChartIdResult> chartColumnDetails, List<GetAllStatisticsChartMasterDetailsByChartIdResult> dataRelated)
        //{
        //    var dt = new System.Data.DataTable();

        //    #region Create Table
        //    if (chartColumnDetails.Count() > 0)
        //    {
        //        //DataColumn column = new DataColumn();
        //        //{
        //        //    column.Caption = "Sr No";
        //        //    column.ColumnName = "Sr No";
        //        //    column.DataType = typeof(long);
        //        //    dt.Columns.Add(column);
        //        //}

        //        foreach (var row in chartColumnDetails.Where(x => x.TypeColumn == "X").OrderBy(x=> x.SequanceNo).ToList())
        //        {
        //            DataColumn column1 = new DataColumn();
        //            {
        //                column1.Caption = row.Id.ToString();
        //                column1.ColumnName = row.ColName;
        //                column1.DataType = typeof(String);
        //                dt.Columns.Add(column1);
        //            }
        //        }

        //        foreach (var row in chartColumnDetails.Where(x => x.TypeColumn != "X").OrderBy(x => x.SequanceNo).ToList())
        //        {
        //            DataColumn column1 = new DataColumn();
        //            {
        //                column1.Caption = row.Id.ToString();
        //                column1.ColumnName = row.ColName;
        //                column1.DataType = typeof(String);
        //                dt.Columns.Add(column1);
        //            }
        //        }
        //    }
        //    #endregion

        //    #region Bind Table
        //    if (dataRelated.Count() > 0)
        //    {

        //        foreach (var row in dataRelated.Select(x => x.SequanceNo).Distinct().ToList())
        //        {

        //            var sequanceData = dataRelated.Where(x => x.SequanceNo == row).OrderBy(x => x.TypeColumn).ToList();

        //            DataRow dr = dt.NewRow();

        //            dr[0] = sequanceData.FirstOrDefault(x => x.TypeColumn == "X").AliasName;

        //            int columnIndex = 1;

        //            foreach (var subRow in sequanceData.Where(x => x.TypeColumn != "X"))
        //            {
        //                dr[columnIndex] = subRow.ColumnValue;
        //                columnIndex++;
        //            }

        //            dt.Rows.Add(dr);

        //        }
        //    }
        //    #endregion

        //    #region Table String HTML
        //    string html = "<table class=\"table table-hover table-center mb-0 maintable text-center\">";
        //    //add header row
        //    html += "<thead><tr>";
        //    for (int i = 0; i < dt.Columns.Count; i++)
        //    {
        //        html += "<th>" + dt.Columns[i].ColumnName + "</th>";
        //    }
        //    html += "</tr></thead><tbody>";
        //    //add rows
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        html += "<tr>";
        //        for (int j = 0; j < dt.Columns.Count; j++)
        //        {
        //            html += "<td>" + dt.Rows[i][j].ToString() + "</td>";
        //        }
        //        html += "</tr>";
        //    }
        //    html += "</tbody></table>";
        //    #endregion

        //    return html;
        //}

        #endregion

        public static string GetRandomNumberStringForPayment()
        {
            Random generator = new Random();
            string r = generator.Next(0, 999999999).ToString("D9");
            return r;
        }

        public static string GetReturnPayment(string returnUrl)
        {
            string strReturnURL = "";
            switch (returnUrl)
            {
                case "Admission":
                    strReturnURL = "~/Admin/Admission/PaymentStatus.aspx";
                    break;
                case "Contribution":
                    strReturnURL = "~/PaymentStatusPage.aspx";
                    break;
                case "Test":
                    strReturnURL = "~/Admission/CoursePaymentStatusPage.aspx";
                    break;
            }
            return strReturnURL;
        }

        public static string GetRandomNumberString()
        {
            Random generator = new Random();
            string r = generator.Next(0, 1000000).ToString("D6");
            return r;
        }

        public static string GenerateOTP()
        {
            Random generator = new Random();
            string r = generator.Next(100000, 999999).ToString("D6");
            return r;
        }


        public static void GetDatatableWithWhereCondition(DataTable dtMain, string strCondition, out DataTable dtClone)
        {
            dtClone = new DataTable();
            foreach (DataColumn col in dtMain.Columns)
            {
                dtClone.Columns.Add(col.ColumnName, col.DataType);
            }

            DataRow[] dr = dtMain.Select(strCondition);

            foreach (DataRow row in dr)
            {
                dtClone.Rows.Add(row.ItemArray);
            }

        }

        public static bool GetDateFromString(string strDate, out DateTime? dt, out string strError)
        {
            try
            {
                strError = "";
                dt = DateTime.ParseExact(strDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                return false;
            }
            catch (Exception ex)
            {
                strError = "Please Enter Proper Date formate ex dd/MM/yyyy";
                dt = null;
                return true;
            }
        }
        public static bool GetDateTimeFromString(string strDate, out DateTime? dt, out string strError)
        {
            try
            {
                strError = "";
                dt = DateTime.ParseExact(strDate, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                return false;
            }
            catch (Exception ex)
            {
                strError = "Please Enter Proper Date formate ex dd/MM/yyyy HH:mm";
                dt = null;
                return true;
            }
        }

        public static List<T> ToListof<T>(this DataTable dt)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
            var columnNames = dt.Columns.Cast<DataColumn>()
                .Select(c => c.ColumnName)
                .ToList();
            var objectProperties = typeof(T).GetProperties(flags);
            var targetList = dt.AsEnumerable().Select(dataRow =>
            {
                var instanceOfT = Activator.CreateInstance<T>();

                foreach (var properties in objectProperties.Where(properties => columnNames.Contains(properties.Name) && dataRow[properties.Name] != DBNull.Value))
                {
                    properties.SetValue(instanceOfT, dataRow[properties.Name], null);
                }
                return instanceOfT;
            }).ToList();

            return targetList;
        }

        public static DataTable GetFromDataTableToDataView(DataView dv)
        {
            return dv.Table.Copy();
        }

        public static bool PageRightsCheck()
        {
            using (IMenuMasterRepository objMenuMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
            {
                string[] rowURLArray = HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.Split('/');

                string rowUrl = rowURLArray[rowURLArray.Count() - 1];

                var data = objMenuMasterRepository.GetAllMenuListByRole(SessionWrapper.UserDetails.RoleId);

                if (data != null)
                {
                    var subModel = data.Where(x => x.MenuUrl.ToLower().Contains(rowUrl.ToLower())).FirstOrDefault();
                    if (subModel != null)
                    {
                        SessionWrapper.UserPageDetails = subModel;
                        return true;
                    }
                    else
                    {
                        SessionWrapper.UserPageDetails = new UserRightsBO { CanAdd = false, CanDelete = false, CanUpdate = false, CanView = false };
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }



        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, typeof(string));
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public static bool ValidateEmailId(string emailId)
        {
            return Regex.IsMatch(emailId, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }

        public static bool SendEmail(string strToEMail, string strSubject, string strBody, out string strError, bool ishtml = false, List<Attachment> lstAttachment = null)
        {
            strError = "";
            try
            {
                string smtpserver = "";
                string smtpPassword = "";
                string fromemail = "";
                string smtpaccount = "";
                string smtpPort = "";
                string smtpIsSecure = "";

                if (ConfigDetailsValue.SMTPIsTest != "1")
                {
                    smtpserver = ConfigDetailsValue.SMTPServer;
                    smtpPassword = ConfigDetailsValue.SMTPPassword;
                    fromemail = ConfigDetailsValue.SMTPFromEmail;
                    smtpaccount = ConfigDetailsValue.SMTPAccount;
                    smtpPort = ConfigDetailsValue.SMTPPort;
                    smtpIsSecure = ConfigDetailsValue.SMTPIsSecure;
                }
                else
                {

                    smtpserver = ConfigDetailsValue.TestSMTPServer;
                    smtpPassword = ConfigDetailsValue.TestSMTPPassword;
                    fromemail = ConfigDetailsValue.TestSMTPFromEmail;
                    smtpaccount = ConfigDetailsValue.TestSMTPAccount;
                    smtpPort = ConfigDetailsValue.TestSMTPPort;
                    smtpIsSecure = ConfigDetailsValue.TestSMTPIsSecure;
                }


                MailMessage msg = new MailMessage();
                //SmtpClient client;

                //if (!string.IsNullOrEmpty(ConfigDetailsValue.SMTPPort))
                //{
                //    client = new SmtpClient(smtpserver, Convert.ToInt32(ConfigDetailsValue.SMTPPort));

                //}
                //else
                //{
                //    client = new SmtpClient(smtpserver);

                //}
                SmtpClient client = new SmtpClient();
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Host = smtpserver;
                if (!string.IsNullOrWhiteSpace(smtpPort))
                {
                    client.Port = Convert.ToInt32(smtpPort);
                }
                // setup Smtp authentication
                System.Net.NetworkCredential credentials =
                    new System.Net.NetworkCredential(smtpaccount, smtpPassword);
                client.UseDefaultCredentials = false;
                client.Credentials = credentials;
                msg.To.Add(strToEMail);
                //msg.CC.Add("hardik.mistry@kcspl.co.in");      
                // msg.CC.Add("parul@kcspl.co.in");
                msg.IsBodyHtml = ishtml;
                if (lstAttachment != null)
                {
                    foreach (var row in lstAttachment)
                    {
                        msg.Attachments.Add(row);
                    }
                }
                msg.Subject = strSubject;
                msg.Body = strBody;

                msg.From = new System.Net.Mail.MailAddress(fromemail);
                if (smtpIsSecure != "0")
                {

                    client.EnableSsl = smtpIsSecure == "1" ? true : false;
                }
                else
                {
                    client.EnableSsl = false;
                }
                //client.UseDefaultCredentials = false;
                //client.Credentials = new NetworkCredential(smtpaccount, smtpPassword);
                client.Send(msg);
                return true;
            }
            catch (Exception ex)
            {
                strError = "Message=>" + ex.Message + " Inner Exception=>" + ex.InnerException.Message;
                return false;
            }
        }

        public static bool ImageResize(Stream inp_Stream, string strPath, string fileName, int width, int height)
        {
            using (var image = System.Drawing.Image.FromStream(inp_Stream))
            {
                Bitmap myImg = new Bitmap(width, height);
                Graphics myImgGraph = Graphics.FromImage(myImg);
                myImgGraph.CompositingQuality = CompositingQuality.HighQuality;
                myImgGraph.SmoothingMode = SmoothingMode.HighQuality;
                myImgGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                var imgRectangle = new Rectangle(0, 0, width, height);
                myImgGraph.DrawImage(image, imgRectangle);

                // Save the file   
                var path = Path.Combine(strPath + "/Resize/", fileName);
                myImg.Save(path, image.RawFormat);

                return true;
            }
        }

        public static void MessagePopup(Page p, string errorMessage, PopupMessageType msgType)
        {
            ScriptManager.RegisterStartupScript(p, p.GetType(), "Alert", "<script>TosterMessage('" + errorMessage + "','" + msgType + "')</script>", false);
        }

        public static void MessageFrontPopup(Page p, string errorMessage, PopupMessageType msgType)
        {
            ScriptManager.RegisterStartupScript(p, p.GetType(), "Alert", "<script>TosterMessage('" + errorMessage + "','" + msgType + "')</script>", false);
        }

        

        public static List<string> CreateTimes(DateTime start, DateTime end, int intervalMin)
        {
            TimeSpan duration = DateTime.Parse(end.ToString("HH:mm tt")).Subtract(DateTime.Parse(start.ToString("HH:mm tt")));
            TimeSpan work = end.TimeOfDay;
            List<string> lstSlots = new List<string>();
            DateTime currentStart = start;
            while (currentStart < end)
            {
                lstSlots.Add(String.Format("{0}", currentStart.ToString("hh:mm tt")));
                //Console.WriteLine(String.Format("{0} - {1}", currentStart, currentStart.Add(work)));
                currentStart = currentStart.AddMinutes(intervalMin);
            }
            return lstSlots;
        }

        public static void ClosePreLoader(Page p)
        {
            ScriptManager.RegisterStartupScript(p, p.GetType(), "Alert", "<script>ClosePreloder();</script>", false);
        }

        public static void PopulateDropDownList(DropDownList ddl, DataTable dtdata, string dataTextField, string dataValueField, bool AddDefault)
        {
            try
            {
                ddl.DataSource = dtdata;
                ddl.DataTextField = dataTextField;
                ddl.DataValueField = dataValueField;
                ddl.DataBind();

                if (AddDefault)
                    ddl.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Please Select --", "0"));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static string GetIPAddress
        {

            get
            {
                try
                {
                    string ipAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (ipAddress == null || ipAddress == "")
                    {
                        ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    }
                    return ipAddress;
                }
                catch (Exception)
                {
                    string ip = "";
                    var props = System.ServiceModel.OperationContext.Current.IncomingMessageProperties;
                    var endpointProperty = props[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                    if (endpointProperty != null)
                    {
                        ip = endpointProperty.Address;
                    }
                    return ip;
                }
            }
        }

        public static void ClearControlValues(Control container)
        {
            try
            {
                foreach (Control ctrl in container.Controls)
                {
                    if (ctrl.GetType() == typeof(TextBox))
                        ((TextBox)ctrl).Text = "";
                    if (ctrl.GetType() == typeof(DropDownList))
                        ((DropDownList)ctrl).SelectedIndex = -1;
                    if (ctrl.GetType() == typeof(CheckBox))
                        ((CheckBox)ctrl).Checked = false;
                    if (ctrl.GetType() == typeof(System.Web.UI.WebControls.Image))
                        ((System.Web.UI.WebControls.Image)ctrl).ImageUrl = string.Empty;
                    if (ctrl.GetType() == typeof(GridView))
                        ((GridView)ctrl).DataSource = null;
                    if (ctrl.Controls.Count > 0)
                        ClearControlValues(ctrl);
                    if (ctrl is ListControl && ctrl.GetType().Name != "DropDownList")
                    {
                        ListControl listControl = ctrl as ListControl;
                        foreach (ListItem listItem in listControl.Items)
                            listItem.Selected = false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal static string DecryptText(string v)
        {
            throw new NotImplementedException();
        }

        internal static string CreateQuickLink(string mainpage, string pagename)
        {
            StringBuilder strLink = new StringBuilder();
            Control obj = new Control();
            strLink.Append("");
            switch (mainpage)
            {
                case "HiddenPage":
                    {
                        using (IMenuMasterRepository objBlogCategoryMasterRepository = new MenuMasterRepository(Functions.strSqlConnectionString))
                        {
                            var sideMenu = objBlogCategoryMasterRepository.GetAllMenuList(Functions.LanguageId).Where(x => x.col_menu_type == '4').ToList();
                            if(sideMenu.Count()>0)
                            {
                                foreach(var row in sideMenu)
                                {
                                    string strName = Regex.Replace(row.col_menu_name, "<.*?>", String.Empty);

                                    if (row.MaskingURL == "StentPrice")
                                    {

                                        using (IStentPriceTypeRepository objBlMasterRepository = new StentPriceTypeRepository(Functions.strSqlConnectionString))
                                        {
                                            var dataMain = objBlMasterRepository.GetAllStentPriceTypeMasterByLanguageId(1).FirstOrDefault();
                                            if (dataMain != null)
                                            {
                                                if (dataMain.IsVisableInQuickLink.HasValue)
                                                {
                                                    if (dataMain.IsVisableInQuickLink.Value)
                                                    {
                                                        strLink.Append("<li><a href='" + obj.ResolveUrl("~/" + row.MaskingURL) + "' class='" + (pagename == row.MaskingURL ? "active" : "") + "'>" + strName + "</a></li>");

                                                    }
                                                    else
                                                    {
                                                        continue;
                                                    }
                                                }
                                                else
                                                {
                                                    continue;
                                                }
                                            }
                                            else
                                            {
                                                continue;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        strLink.Append("<li><a href='" + obj.ResolveUrl("~/" + row.MaskingURL) + "' class='" + (pagename == row.MaskingURL ? "active" : "") + "'>" + strName + "</a></li>");
                                    }
                                }
                            }
                        }
                        //strLink.Append("<li><a href='" + obj.ResolveUrl("~/Packages") + "' class='" + (pagename == "Packages" ? "active" : "") + "'>Packages</a></li>");
                        //strLink.Append("<li><a href='" + obj.ResolveUrl("~/Career") + "' class='" + (pagename == "Career" ? "active" : "") + "'>Career</a></li>");
                        //strLink.Append("<li><a href='" + obj.ResolveUrl("~/Tenders") + "' class='" + (pagename == "Tenders" ? "active" : "") + "'>Tenders</a></li>");
                        //strLink.Append("<li><a href='" + obj.ResolveUrl("~/Statistics") + "' class='" + (pagename == "Statistics" ? "active" : "") + "'>Statistics</a></li>");
                        //strLink.Append("<li><a href='" + obj.ResolveUrl("~/StentPrice") + "' class='" + (pagename == "StentPrice" ? "active" : "") + "'>Stent Price</a></li>");
                        //strLink.Append("<li><a href='" + obj.ResolveUrl("~/VisitorsDetails") + "' class='" + (pagename == "Visitor" ? "active" : "") + "'>Visitor</a></li>");
                        //strLink.Append("<li><a href='" + obj.ResolveUrl("~/e-Citizen") + "' class='" + (pagename == "E-Citizen" ? "active" : "") + "'>E-Citizen</a></li>");

                        break;
                    }
                case "Cares":
                    {
                        strLink.Append("<li><a href='" + obj.ResolveUrl("~/Contribution") + "' class='" + (pagename == "Contribution" ? "active" : "") + "'>Donation</a></li>");
                        strLink.Append("<li><a href='" + obj.ResolveUrl("~/HealthTips") + "' class='" + (pagename == "HealthTips" ? "active" : "") + "'>Health Tips</a></li>");
                        strLink.Append("<li><a href='" + obj.ResolveUrl("~/NursingCare") + "' class='" + (pagename == "NursingCare" ? "active" : "") + "'>Nursing Care</a></li>");
                        strLink.Append("<li><a href='" + obj.ResolveUrl("~/PatientCareDetails") + "' class='" + (pagename == "PatientCareDetail" ? "active" : "") + "'>Patient Care</a></li>");
                        break;
                    }
                case "Home":
                    {
                        strLink.Append("<li><a href='" + obj.ResolveUrl("~/AdmissionDetails") + "' class='" + (pagename == "AdmissionDetails" ? "active" : "") + "'>Admission</a></li>");
                        strLink.Append("<li><a href='" + obj.ResolveUrl("~/OPDetails") + "' class='" + (pagename == "OPDetails" ? "active" : "") + "'>OPD Timing</a></li>");
                        strLink.Append("<li><a href='" + obj.ResolveUrl("~/InfoMSRClause") + "' class='" + (pagename == "InfoMSRClause" ? "active" : "") + "'>Information Under MSR Clause B.1.11 Committees</a></li>");
                        strLink.Append("<li><a href='" + obj.ResolveUrl("~/GovernmentApproval") + "' class='" + (pagename == "GovernmentApproval" ? "active" : "") + "'>Government Approvals</a></li>");
                        break;
                    }
                case "Career":
                    {
                        using (IStarOfRepository objBlogCategoryMasterRepository = new StarOfRepository(Functions.strSqlConnectionString))
                        {

                            using (IUnmicrCareerRepository objUnmicrCareerRepository = new UnmicrCareerRepository(Functions.strSqlConnectionString))
                            {
                                var data = objUnmicrCareerRepository.GetUnmicrCareerMasterByLanguageId(Functions.LanguageId);
                                if (data != null)
                                {
                                    strLink.Append("<li><a href='" + obj.ResolveUrl("~/WhyJoinUInmicrc") + "' class='" + (pagename == "WhyJoinUInmicrc" ? "active" : "") + "'>" + data.UnmicrcWhyJoinTitle + "</a></li>");
                                    strLink.Append("<li><a href='" + obj.ResolveUrl("~/GrowthatUnmicrc") + "' class='" + (pagename == "GrowthatUnmicrc" ? "active" : "") + "'>" + data.UnmicrcGroveThatTitle + "</a></li>");
                                    strLink.Append("<li><a href='" + obj.ResolveUrl("~/EmployeCareUnmicrc") + "' class='" + (pagename == "EmployeCareUnmicrc" ? "active" : "") + "'>" + data.UnmicrcEmployeeCareTitle + "</a></li>");
                                }
                                else
                                {
                                    strLink.Append("<li><a href='" + obj.ResolveUrl("~/WhyJoinUInmicrc") + "' class='" + (pagename == "WhyJoinUInmicrc" ? "active" : "") + "'>Why Join UNMICRC</a></li>");
                                    strLink.Append("<li><a href='" + obj.ResolveUrl("~/GrowthatUnmicrc") + "' class='" + (pagename == "GrowthatUnmicrc" ? "active" : "") + "'>Growth at UNMICRC</a></li>");
                                    strLink.Append("<li><a href='" + obj.ResolveUrl("~/EmployeCareUnmicrc") + "' class='" + (pagename == "EmployeCareUnmicrc" ? "active" : "") + "'>Employee care at UNMICRC</a></li>");

                                }
                                var dataMain = objBlogCategoryMasterRepository.GetAllStarOfDetails(LanguageId).FirstOrDefault();
                                if(dataMain!=null)
                                {
                                    strLink.Append("<li><a href='" + obj.ResolveUrl("~/StarOfHospital") + "' class='" + (pagename == "StarOfHospital" ? "active" : "") + "'>" + dataMain.StarPageTitle + "</a></li>");

                                }
                                else
                                {
                                    strLink.Append("<li><a href='" + obj.ResolveUrl("~/StarOfHospital") + "' class='" + (pagename == "StarOfHospital" ? "active" : "") + "'>Star Of Hospital</a></li>");

                                }
                            }
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            return strLink.ToString();
        }

        #endregion

        #region Type
        public enum MessageType { Success, Error, Info, Warning };

        #endregion


        public static List<string> FetchImgsFromSource(string htmlSource)
        {
            List<string> listOfImgdata = new List<string>();
            string regexImgSrc = @"<img[^>]*?src\s*=\s*[""']?([^'"" >]+?)[ '""][^>]*?>";
            MatchCollection matchesImgSrc = Regex.Matches(htmlSource, regexImgSrc, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            foreach (Match m in matchesImgSrc)
            {
                string href = m.Groups[1].Value;
                listOfImgdata.Add(href);
            }
            return listOfImgdata;
        }

        public static string ResolveUrl(string originalUrl)
        {
            if (originalUrl == null)
                return null;

            // *** Absolute path - just return
            if (originalUrl.IndexOf("://") != -1)
                return originalUrl;

            // *** Fix up image path for ~ root app dir directory
            if (originalUrl.StartsWith("~"))
            {
                string newUrl = "";
                if (HttpContext.Current != null)
                    newUrl = HttpContext.Current.Request.ApplicationPath +
                          originalUrl.Substring(1).Replace("//", "/");
                else
                    // *** Not context: assume current directory is the base directory
                    throw new ArgumentException("Invalid URL: Relative URL not allowed.");

                // *** Just to be sure fix up any double slashes
                return newUrl.Replace("//", "/");
            }

            return originalUrl;
        }

        public static string CustomHTMLDecode(string htmlSource, Page p)
        {
            string strHeader = HttpUtility.HtmlDecode(htmlSource);
            string strUPdatedHeader = HttpUtility.HtmlDecode(htmlSource);

            StringBuilder b = new StringBuilder(strUPdatedHeader);


            var Urls = FetchImgsFromSource(strHeader);

            foreach (var row in Urls)
            {
                string strData = p.Request.Url.Authority.ToString();
                string strSubData = p.Request.ApplicationPath.ToString().Replace("/", "");

                string strURLss = (string.IsNullOrWhiteSpace(strSubData) ? "" : strSubData + "/") + (row);

                if (strURLss.StartsWith("~/", StringComparison.Ordinal))
                {
                    strURLss = strURLss.Replace("~/", "");
                }
                if (strURLss.StartsWith("../", StringComparison.Ordinal))
                {
                    strURLss = strURLss.Replace("../", "");
                }
                if (strURLss.StartsWith("/", StringComparison.Ordinal))
                {
                    strURLss = strURLss.Remove(0, 1);
                }
                if (strURLss.StartsWith("/", StringComparison.Ordinal))
                {
                    strURLss = strURLss.Remove(0, 1);
                }
                strURLss = ResolveUrl("~/" + strURLss.Replace("/", "//")).ToString();


                //ErrorLogger.ERROR("Page Resolve => "+strURLss, row, p);

                if (!string.IsNullOrWhiteSpace(strSubData))
                {
                    //ErrorLogger.ERROR("Page Resolve Sub Replace=> " + strURLss, row, p);

                    strURLss = strURLss.Replace(p.Request.ApplicationPath + p.Request.ApplicationPath, p.Request.ApplicationPath).Replace(p.Request.ApplicationPath + p.Request.ApplicationPath + p.Request.ApplicationPath, p.Request.ApplicationPath);
                    strURLss = strURLss.Replace(p.Request.ApplicationPath + p.Request.ApplicationPath, p.Request.ApplicationPath).Replace(p.Request.ApplicationPath + p.Request.ApplicationPath + p.Request.ApplicationPath, p.Request.ApplicationPath);

                    //ErrorLogger.ERROR("Page Resolve Sub End Replace=> " + strURLss, row, p);
                }
                b.Replace(row, strURLss);
            }
            string strMainData = b.ToString();
            if (!string.IsNullOrWhiteSpace(p.Request.ApplicationPath.ToString().Replace("/", "")))
            {
                strMainData = strMainData.Replace("/" + p.Request.ApplicationPath.ToString().Replace("/", "") + "/" + p.Request.ApplicationPath.ToString().Replace("/", ""), "/" + p.Request.ApplicationPath.ToString().Replace("/", ""));

                strMainData = strMainData.Replace("/" + p.Request.ApplicationPath.ToString().Replace("/", "") + "/" + p.Request.ApplicationPath.ToString().Replace("/", "") + "/" + p.Request.ApplicationPath.ToString().Replace("/", ""), "/" + p.Request.ApplicationPath.ToString().Replace("/", ""));

                strMainData = strMainData.Replace("/" + p.Request.ApplicationPath.ToString().Replace("/", "") + "/" + p.Request.ApplicationPath.ToString().Replace("/", ""), "/" + p.Request.ApplicationPath.ToString().Replace("/", ""));
            }

            ErrorLogger.ERROR("Page Resolve main Data=> " + p.Request.ApplicationPath.ToString().Replace("/", ""), "Page Resolve main Data=> " + p.Request.ApplicationPath.ToString(), p);
            return strMainData;
        }


        public static void GetReloadPage(Page page, ref StringBuilder strContent)
        {
            if (Functions.LanguageId != 1)
            {
                string reloadPage = "window.setInterval('refresh()', 10000); function refresh() { window .location.reload();}";

                ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "asdasdsadasd", reloadPage, true);

                if (Functions.LanguageId == 3)
                {
                    strContent = new StringBuilder();
                    strContent.Append("");


                    strContent.Append("<div class='innerContentPart' translate='no'>");
                    strContent.Append("<div class='container'><div class='noContentInfo'>");

                    strContent.Append("<div class='alert alert-primary alert-dismissible fade show' role='alert'>");
                    strContent.Append("<p>Content of this page is available in English version website, you will be automatically redirected to the page in 10 sec…</p>");
                    strContent.Append("<p>આ પેજની માહિતી ફક્ત અંગ્રેજી વેબસાઇટમાં ઉપલબ્ધ છે, અગામી 10 સેકંડમાં આ ગુજરાતી પેજ પરથી તે અંગ્રેજી પેજ પર ઓટોમેટીક રીડાયરેક્ટ કરવામાં આવશે ...</p>");
                    strContent.Append("</div>");

                    strContent.Append("</div></div>");
                    strContent.Append("</div>");
                }

                if (Functions.LanguageId == 2)
                {
                    strContent = new StringBuilder();
                    strContent.Append("");
                    strContent.Append("<div class='innerContentPart' translate='no'>");
                    strContent.Append("<div class='container'><div class='noContentInfo'>");
                    strContent.Append("<div class='alert alert-primary alert-dismissible fade show' role='alert'>");
                    strContent.Append("<p>Content of this page is available in English version website, you will be automatically redirected to the page in 10 sec…</p>");
                    strContent.Append("<p>इस पृष्ठ की जानकारी केवल अंग्रेजी वेबसाइट में उपलब्ध है, यह अगले 10 सेकंड में इस गुजराती पृष्ठ से अंग्रेजी पृष्ठ पर स्वचालित रूप से पुनर्निर्देशित हो जाएगी। ...</p>");
                    strContent.Append("</div>");
                    strContent.Append("</div></div>");
                    strContent.Append("</div>");
                }

                Functions.LanguageId = 1;
            }
        }

        public static bool BooleanNullToFalse(dynamic value)
        {
            return Convert.ToBoolean(value);
        }

        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        public static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, string.IsNullOrEmpty(dr[column.ColumnName].ToString()) ? null : dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

        public static String sendSingleSMS(String username, String password, String senderid, String mobileNo, String message, string SMSAPI,int otp)
        {
            Page p = new Page() ;
            string requst = "", strrespons = "", strRtn = "0";
            try
            {
                requst = (SMSAPI + "?user=" + username + "&pass=" + password + "&sender=" + senderid + "&phone=" + mobileNo + "&text=" + message + "&priority=ndnd&stype=normal").ToString();
                var client = new RestClient(SMSAPI + "?user=" + username + "&pass=" + password + "&sender=" + senderid + "&phone=" + mobileNo + "&text=" + message + "&priority=ndnd&stype=normal");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
                IRestResponse response = client.Execute(request); 

                //  ErrorLogger.WriteToErrorLog(" response :" + message, response.ToString(), "Error",p);
                strrespons = "response.Content: " + Convert.ToString(response.Content) + " StatusCode: " + Convert.ToString(response.StatusCode) + "\r\n DESC: " + Convert.ToString(response.StatusDescription);
                ErrorLogger.WriteToErrorLog(" \r\n"+ HttpUtility.UrlDecode(message) + " \r\n"+requst, strrespons, "Error", p);
                strRtn = response.Content.ToString();
                strRtn = "1";
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message.ToString(), ex.ToString(), "Error", p);
                strRtn = ex.Message.ToString();
                strRtn = "0";
            }
            finally
            {
                try
                {
                    SMSlogBO obj = new SMSlogBO();
                    SMSLogBAL objBAL = new SMSLogBAL();
                    obj.MobileNo = mobileNo;
                    obj.TransectionId = Convert.ToString(otp);
                    obj.Message = message;
                    obj.Status = strrespons;
                    obj.RequestURL = requst;
                    objBAL.InsertRecord(obj);
                }
                catch (Exception ex)
                {
                    ErrorLogger.WriteToErrorLog(ex.Message.ToString(), ex.ToString(), "Error", p);
                    
                }
            }
            return strRtn;

        }

    }
}