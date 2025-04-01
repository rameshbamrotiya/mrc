using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Common;
using Unmehta.WebPortal.Repository.Interface.Payment;

namespace Unmehta.WebPortal.Repository.Repository.Payment
{
    public class PaymentRepository : IPaymentRepository
    {
        private string SqlConnectionSTring;
        public PaymentRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        //public RecruitmentAdvertisementGridModel GetRequestData()
        //{
        //    using (RecruitmentsDataContext db = new RecruitmentsDataContext(SqlConnectionSTring))
        //    {
        //        return GetAllTblRecruitmentAdvertisement().Where(x => x.Id == lgId).FirstOrDefault();
        //    }
        //}

        public bool InsertOrUpdatePaymentRequest(string strPayURL,
            string transactionCode, string merchantId, string  uniTranNo, string  nA1, string  txnAmount,
            string  nA2, string  nA3, string  nA4, string  currencyType, string  nA5, string  typeField1,
            string  securityId, string  nA6, string  nA7, string  typeField2, string  additionalInfo1, 
            string  additionalInfo2, string  additionalInfo3, string  additionalInfo4, string  additionalInfo5,
            string  additionalInfo6, string  additionalInfo7, string  returnURL, string  checksumKey, string  hashValue, 
            string  msgText ,out  long id
            , out string strError)
        {
            //Tbl_Education_Qualification objTbl_Education_Qualifications = new Tbl_Education_Qualification();
                id = 0;
            bool isError = false;
            strError = "";
            try
            {
                using (PaymentGatewayDataContext db = new PaymentGatewayDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertPaymentTransactionRequestMaster(strPayURL,  transactionCode, 
                        merchantId, uniTranNo, nA1, txnAmount, nA2, nA3, nA4, currencyType, nA5, typeField1, 
                        securityId, nA6, nA7, typeField2, additionalInfo1, additionalInfo2, additionalInfo3, 
                        additionalInfo4, additionalInfo5, additionalInfo6, additionalInfo7, returnURL, checksumKey,
                        hashValue, msgText);
                    if (dataIsDone != null)
                    {
                        id = (long)dataIsDone.FirstOrDefault().RecId;
                        {
                            strError = "Record Inserted Successfully";
                            isError = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                isError = true;
                strError = ex.ToString();
            }
            return isError;
        }

        public bool InsertOrUpdatePaymentResponse(string paymentPath, string  merchantId, string  uniTranNo,
            string  txnReferenceNo, string  bankReferenceNo, string  txnAmount, string  bankId, string  bankMerchantId,
            string  txnType, string  currencyType, string  itemCode, string  securityType, string  securityId,
            string  securityPasswod, string  txnDate, string  authStatus, string  settlementType, string  additionalInfo1,
            string  additionalInfo2, string  additionalInfo3, string  additionalInfo4, string  additionalInfo5, string  additionalInfo6, 
            string  additionalInfo7, string  errorStatus, string  errorDescription, string  checksum, bool  isCheckSumMatch, string  txRefNo,
            string  pgTxnNo, string  txAmount, string  txStatus, string  txMssg, string  paymentFor
            , out string strError)
        {
            //Tbl_Education_Qualification objTbl_Education_Qualifications = new Tbl_Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (PaymentGatewayDataContext db = new PaymentGatewayDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertPaymentTransactionResponseMaster(paymentPath, merchantId, uniTranNo, txnReferenceNo, bankReferenceNo, txnAmount, bankId, bankMerchantId, txnType, currencyType, itemCode, securityType, securityId, securityPasswod, txnDate, authStatus, settlementType, additionalInfo1, additionalInfo2, additionalInfo3, additionalInfo4, additionalInfo5, additionalInfo6, additionalInfo7, errorStatus, errorDescription, checksum, isCheckSumMatch, txRefNo, pgTxnNo, txAmount, txStatus, txMssg, paymentFor);
                    if (dataIsDone != null)
                    {
                        {
                            strError = "Record Inserted Successfully";
                            isError = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                isError = true;
                strError = ex.ToString();
            }
            return isError;
        }


        public bool ValidatePaymentTransactionCode(string TransactionCode, out string strError)
        {
            //Tbl_Education_Qualification objTbl_Education_Qualifications = new Tbl_Education_Qualification();
            bool isError = false;
            strError = "";
                using (PaymentGatewayDataContext db = new PaymentGatewayDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.ValidateTransactionCodeNotDublicate(TransactionCode);
                    if (dataIsDone != null)
                    {
                        {
                            isError = (bool)dataIsDone.FirstOrDefault().IsExist;
                        }
                    }
                }
            return isError;
        }

        public bool UpdatePaymentTransactionRequest(string TransactionCode ,bool isHit , out string strError)
        {
            //Tbl_Education_Qualification objTbl_Education_Qualifications = new Tbl_Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (PaymentGatewayDataContext db = new PaymentGatewayDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.UpdatePaymentTransactionRequestMasterStatus(TransactionCode, isHit);
                    if (dataIsDone != null)
                    {
                        {
                            strError = "Record Updated Successfully";
                            isError = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                isError = true;
                strError = ex.ToString();
            }
            return isError;
        }
        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
