using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data;
using Unmehta.WebPortal.Model.Model;
using Unmehta.WebPortal.Repository.Interface;

namespace Unmehta.WebPortal.Repository.Interface.Payment
{
    public interface IPaymentRepository : IDisposable
    {
        bool InsertOrUpdatePaymentRequest(string strPayURL,
            string transactionCode, string merchantId, string uniTranNo, string nA1, string txnAmount, string nA2, string nA3, string nA4, string currencyType, string nA5, string typeField1,
            string securityId, string nA6, string nA7, string typeField2, string additionalInfo1, string additionalInfo2, string additionalInfo3, string additionalInfo4, string additionalInfo5,
            string additionalInfo6, string additionalInfo7, string returnURL, string checksumKey, string hashValue, string msgText, out long id, out string strError);

        bool InsertOrUpdatePaymentResponse(string paymentPath, string merchantId, string uniTranNo, string txnReferenceNo, string bankReferenceNo, string txnAmount, string bankId, string bankMerchantId,
            string txnType, string currencyType, string itemCode, string securityType, string securityId, string securityPasswod, string txnDate, string authStatus, string settlementType, string additionalInfo1,
            string additionalInfo2, string additionalInfo3, string additionalInfo4, string additionalInfo5, string additionalInfo6, string additionalInfo7, string errorStatus, string errorDescription,
            string checksum, bool isCheckSumMatch, string txRefNo, string pgTxnNo, string txAmount, string txStatus, string txMssg, string paymentFor, out string strError);

        bool ValidatePaymentTransactionCode(string TransactionCode, out string strError);

        bool UpdatePaymentTransactionRequest(string TransactionCode, bool isHit, out string strError);
    }
}
