using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Unmehta.WebPortal.Model.Model;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository;

namespace Unmehta.WebPortal.Web.Common
{
    public class PaymentConfigDetailsValue
    {
        public static string strSqlConnectionString = ConfigurationManager.ConnectionStrings["UNMehtaConnectionString"].ToString();
        private static T GetFromTable<T>(string key)
        {
            using (IConfigDetailsRepository configDetailsRepository = new ConfigDetailsRepository(strSqlConnectionString))
            {
                string strError = "";

                ConfigDetailsModel objConfigDetailsModel = new ConfigDetailsModel();
                if (!configDetailsRepository.GetPaymentConfigDetails(key, out objConfigDetailsModel, out strError))
                {
                    if (objConfigDetailsModel != null)
                    {
                        object obj = (objConfigDetailsModel.ParameterValue == null ? "" : objConfigDetailsModel.ParameterValue).ToString();
                        return (T)obj;
                    }
                    else
                    {
                        object obj = ("DataNotFound" + "|" + false).ToString();
                        return (T)obj;
                    }
                }
                else
                {
                    object obj = (strError + "|" + true).ToString();
                    return (T)obj;
                }
            }
        }

        #region SMS
        public static string SMSAPI
        {
            get
            {
                return GetFromTable<string>("SMSAPI");
            }
        }

        public static string SMSTemplateid
        {
            get
            {
                return GetFromTable<string>("SMSTemplateid");
            }
        }

        public static string SMSSecureKey
        {
            get
            {
                return GetFromTable<string>("SMSSecureKey");
            }
        }

        public static string SMSSenderId
        {
            get
            {
                return GetFromTable<string>("SMSSenderId");
            }
        }

        public static string SMSPassword
        {
            get
            {
                return GetFromTable<string>("SMSPassword");
            }
        }

        public static string SMSUsername
        {
            get
            {
                return GetFromTable<string>("SMSUsername");
            }
        }
        #endregion

        public static string PaymentRequestVersion
        {
            get
            {
                return GetFromTable<string>("PaymentRequestVersion");
            }
        }
        public static string PaymentRequestAPI
        {
            get
            {
                return GetFromTable<string>("PaymentRequestAPI");
            }
        }
        public static string PaymentRequestPlatform
        {
            get
            {
                return GetFromTable<string>("PaymentRequestPlatform");
            }
        }
        public static string PaymentRequestMerchIdUAT
        {
            get
            {
                return GetFromTable<string>("PaymentRequestMerchIdUAT");
            }
        }
        public static string PaymentRequestUserIdUAT
        {
            get
            {
                return GetFromTable<string>("PaymentRequestUserIdUAT");
            }
        }
        public static string PaymentRequestPasswordUAT
        {
            get
            {
                return GetFromTable<string>("PaymentRequestPasswordUAT");
            }
        }
        public static string PaymentRequestProductUAT
        {
            get
            {
                return GetFromTable<string>("PaymentRequestProductUAT");
            }
        }
        public static string PaymentRequestCustAccNoUAT
        {
            get
            {
                return GetFromTable<string>("PaymentRequestCustAccNoUAT");
            }
        }
        public static string PaymentRequestTxnCurrency
        {
            get
            {
                return GetFromTable<string>("PaymentRequestTxnCurrency");
            }
        }
        public static string PaymentRequestEncpassphraseUAT
        {
            get
            {
                return GetFromTable<string>("PaymentRequestEncpassphraseUAT");
            }
        }
        public static string PaymentRequestEncsaltUAT
        {
            get
            {
                return GetFromTable<string>("PaymentRequestEncsaltUAT");
            }
        }
        public static string PaymentRequestDecpassphraseUAT
        {
            get
            {
                return GetFromTable<string>("PaymentRequestDecpassphraseUAT");
            }
        }
        public static string PaymentRequestDecsaltUAT
        {
            get
            {
                return GetFromTable<string>("PaymentRequestDecsaltUAT");
            }
        }
        public static string PaymentRequestRemarkUAT
        {
            get
            {
                return GetFromTable<string>("PaymentRequestRemarkUAT");
            }
        }
        public static string PaymentRequestIPUAT
        {
            get
            {
                return GetFromTable<string>("PaymentRequestIPUAT");
            }
        }
        public static string PaymentRequestContIDUAT
        {
            get
            {
                return GetFromTable<string>("PaymentRequestContIDUAT");
            }
        }
        public static string PaymentRequestMerchIdLive
        {
            get
            {
                return GetFromTable<string>("PaymentRequestMerchIdLive");
            }
        }

        public static string PaymentRequestReturnURLUAT
        {
            get
            {
                return GetFromTable<string>("PaymentRequestReturnURLUAT");
            }
        }
        public static string PaymentRequestReturnURLLive
        {
            get
            {
                return GetFromTable<string>("PaymentRequestReturnURLLive");
            }
        }
        public static string PaymentRequestAPIURLUAT
        {
            get
            {
                return GetFromTable<string>("PaymentRequestAPIURLUAT");
            }
        }
        public static string PaymentRequestAPIURLLive
        {
            get
            {
                return GetFromTable<string>("PaymentRequestAPIURLLive");
            }
        }
        public static string BillDeskMerchantId
        {
            get
            {
                return GetFromTable<string>("BillDeskMerchantId");
            }
        }
        public static string BillDeskSecurityCode
        {
            get
            {
                return GetFromTable<string>("BillDeskSecurityCode");
            }
        }
        public static string BillDeskChecksumKey
        {
            get
            {
                return GetFromTable<string>("BillDeskChecksumKey");
            }
        }
        public static string BillDeskURL
        {
            get
            {
                return GetFromTable<string>("BillDeskURL");
            }
        }
        public static string BillDeskTestMerchantId
        {
            get
            {
                return GetFromTable<string>("BillDeskTestMerchantId");
            }
        }
        public static string BillDeskTestSecurityCode
        {
            get
            {
                return GetFromTable<string>("BillDeskTestSecurityCode");
            }
        }
        public static string BillDeskTestChecksumKey
        {
            get
            {
                return GetFromTable<string>("BillDeskTestChecksumKey");
            }
        }
        public static string BillDeskTestURL
        {
            get
            {
                return GetFromTable<string>("BillDeskTestURL");
            }
        }
        public static string PaymentMode
        {
            get
            {
                return GetFromTable<string>("PaymentMode");
            }
        }
        public static string BillDeskReturnURL
        {
            get
            {
                return GetFromTable<string>("BillDeskReturnURL");
            }
        }
        public static string paymentmodeSS
        {
            get
            {
                return GetFromTable<string>("paymentmodeSS");
            }
        }
        public static string paymentmodeSL
        {
            get
            {
                return GetFromTable<string>("paymentmodeSL");
            }
        }
        public static string paymentmodeRD
        {
            get
            {
                return GetFromTable<string>("paymentmodeRD");
            }
        }
        public static string PaymentRefundAPI
        {
            get
            {
                return GetFromTable<string>("PaymentRefundAPI");
            }
        }
        public static string PaymentRefundSource
        {
            get
            {
                return GetFromTable<string>("PaymentRefundSource");
            }
        }
        public static string PaymentRefundAPIURLLive
        {
            get
            {
                return GetFromTable<string>("PaymentRefundAPIURLLive");
            }
        }
        public static string PaymentRefundAPIURLUAT
        {
            get
            {
                return GetFromTable<string>("PaymentRefundAPIURLUAT");
            }
        }
        public static string PaymentRefundSignKeyLive
        {
            get
            {
                return GetFromTable<string>("PaymentRefundSignKeyLive");
            }
        }
        public static string PaymentRefundSignKeyUAT
        {
            get
            {
                return GetFromTable<string>("PaymentRefundSignKeyUAT");
            }
        }
        public static string PaymentRefundDecsaltLive
        {
            get
            {
                return GetFromTable<string>("PaymentRefundDecsaltLive");
            }
        }
        public static string PaymentRefundDecpassphraseLive
        {
            get
            {
                return GetFromTable<string>("PaymentRefundDecpassphraseLive");
            }
        }
        public static string PaymentRefundDecsaltUAT
        {
            get
            {
                return GetFromTable<string>("PaymentRefundDecsaltUAT");
            }
        }
        public static string PaymentRefundDecpassphraseUAT
        {
            get
            {
                return GetFromTable<string>("PaymentRefundDecpassphraseUAT");
            }
        }
        public static string PaymentRefundEncsaltLive
        {
            get
            {
                return GetFromTable<string>("PaymentRefundEncsaltLive");
            }
        }
        public static string PaymentRefundEncsaltUAT
        {
            get
            {
                return GetFromTable<string>("PaymentRefundEncsaltUAT");
            }
        }
        public static string PaymentRefundEncpassphraseLive
        {
            get
            {
                return GetFromTable<string>("PaymentRefundEncpassphraseLive");
            }
        }
        public static string PaymentRefundEncpassphraseUAT
        {
            get
            {
                return GetFromTable<string>("PaymentRefundEncpassphraseUAT");
            }
        }
        public static string PaymentStatusAPIURLLive
        {
            get
            {
                return GetFromTable<string>("PaymentStatusAPIURLLive");
            }
        }
        public static string PaymentStatusAPIURLUAT
        {
            get
            {
                return GetFromTable<string>("PaymentStatusAPIURLUAT");
            }
        }
        public static string PaymentStatusSignKeyLive
        {
            get
            {
                return GetFromTable<string>("PaymentStatusSignKeyLive");
            }
        }
        public static string PaymentStatusSignKeyUAT
        {
            get
            {
                return GetFromTable<string>("PaymentStatusSignKeyUAT");
            }
        }
        public static string PaymentStatusSource
        {
            get
            {
                return GetFromTable<string>("PaymentStatusSource");
            }
        }
        public static string PaymentStatusAPI
        {
            get
            {
                return GetFromTable<string>("PaymentStatusAPI");
            }
        }
        public static string PaymentRequestDecsaltLive
        {
            get
            {
                return GetFromTable<string>("PaymentRequestDecsaltLive");
            }
        }
        public static string PaymentRequestDecpassphraseLive
        {
            get
            {
                return GetFromTable<string>("PaymentRequestDecpassphraseLive");
            }
        }
        public static string PaymentRequestEncsaltLive
        {
            get
            {
                return GetFromTable<string>("PaymentRequestEncsaltLive");
            }
        }
        public static string PaymentRequestEncpassphraseLive
        {
            get
            {
                return GetFromTable<string>("PaymentRequestEncpassphraseLive");
            }
        }
        public static string PaymentRequestUserIdLive
        {
            get
            {
                return GetFromTable<string>("PaymentRequestUserIdLive");
            }
        }
        public static string PaymentRequestPasswordLive
        {
            get
            {
                return GetFromTable<string>("PaymentRequestPasswordLive");
            }
        }
        public static string PaymentRequestProductLive
        {
            get
            {
                return GetFromTable<string>("PaymentRequestProductLive");
            }
        }
        public static string PaymentRequestCustAccNoLive
        {
            get
            {
                return GetFromTable<string>("PaymentRequestCustAccNoLive");
            }
        }
    }
}