using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    [Serializable()]
    public class DonationListBO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public System.Nullable<System.DateTime> DonationDate { get; set; }

    }
    public class BankDetails
    {
        public string bankTxnId { get; set; }
        public string otsBankName { get; set; }
        public string cardMaskNumber { get; set; }
    }

    public class MerchDetails
    {
        public int merchId { get; set; }
        public string merchTxnId { get; set; }
        public string merchTxnDate { get; set; }
    }

    public class PayDetails
    {
        public object atomTxnId { get; set; }
        public string product { get; set; }
        public double amount { get; set; }
        public double surchargeAmount { get; set; }
        public double totalAmount { get; set; }
    }

    public class PayInstrument
    {
        public SettlementDetails settlementDetails { get; set; }
        public MerchDetails merchDetails { get; set; }
        public PayDetails payDetails { get; set; }
        public PayModeSpecificData payModeSpecificData { get; set; }
        public ResponseDetails responseDetails { get; set; }
    }

    public class PayModeSpecificData
    {
        public string subChannel { get; set; }
        public BankDetails bankDetails { get; set; }
    }

    public class ResponseDetails
    {
        public string statusCode { get; set; }
        public string message { get; set; }
        public string description { get; set; }
    }

    public class ReCheckRoot
    {
        public List<PayInstrument> payInstrument { get; set; }
    }

    public class SettlementDetails
    {
        public string reconStatus { get; set; }
    }


    public class APIRequestPaymentBO
    {
        public string version { get; set; }
        public string api { get; set; }
        public string platform { get; set; }
        public string merchId { get; set; }
        public string userId { get; set; }
        public string password { get; set; }
        public string merchTxnId { get; set; }
        public string merchTxnDate { get; set; }
        public string amount { get; set; }
        public string product { get; set; }
        public string custAccNo { get; set; }
        public string txnCurrency { get; set; }
        public string custEmail { get; set; }
        public string custMobile { get; set; }
        public string udf1 { get; set; }
        public string udf2 { get; set; }
        public string udf3 { get; set; }
        public string udf4 { get; set; }
        public string udf5 { get; set; }

        public string Remarks { get; set; }
        public string TransectionDatetime { get; set; }
        public string IpAddress { get; set; }
        public string Contribution_Id { get; set; }
        //public string atomTxnId { get; set; }

    }

    public class APITransactionStatusRequest
    {
        public string api { get; set; }
        public string source { get; set; }
        public string merchId { get; set; }
        public string password { get; set; }
        public string merchTxnId { get; set; }
        public string merchTxnDate { get; set; }
        public string signature { get; set; }
        public string amount { get; set; }
        public string txnCurrency { get; set; }
    }

    public class APITransactionStatusResponse
    {
        public string reconStatus { get; set; }
        public string merchTxnId { get; set; }
        public string merchTxnDate { get; set; }
        public string atomTxnId { get; set; }
        public string product { get; set; }
        public decimal amount { get; set; }
        public decimal surchargeAmount { get; set; }
        public decimal totalAmount { get; set; }
        public string subChannel { get; set; }
        public string otsBankId { get; set; }
        public string bankTxnId { get; set; }
        public string cardMaskNumber { get; set; }
        public string statusCode { get; set; }
        public string message { get; set; }
        public string description { get; set; }
        public string TransectionDatetime { get; set; }
        public string IpAddress { get; set; }
        public string Contribution_Id { get; set; }
    }

    public class APITransactionRefundRequest
    {
        public string api { get; set; }
        public string merchId { get; set; }
        public string password { get; set; }
        public string merchTxnId { get; set; }
        public string atomTxnId { get; set; }
        public string txnCurrency { get; set; }
        public string signature { get; set; }
        public string totalRefundAmount { get; set; }
        public string prodName { get; set; }
        public string prodRefundId { get; set; }
        public string prodRefundAmount { get; set; }
    }
    
        public class APITransactionRefundResponse
    {
        public string atomTxnId { get; set; }
        public string totalRefundAmount { get; set; }
        public string txnCurrency { get; set; }
        public string prodName { get; set; }
        public string prodRefundAmount { get; set; }
        public decimal prodRefundId { get; set; }
        public decimal refundTxnId { get; set; }
        public decimal prodDescription { get; set; }
        public string prodStatusCode { get; set; }
        public string statusCode { get; set; }
        public string message { get; set; }
        public string description { get; set; }
        public string TransectionDatetime { get; set; }
        public string IpAddress { get; set; }
        public string Contribution_Id { get; set; }
 
    }

    public class APIResponsePaymentBO
    {
        public string atomTokenId { get; set; }
        public string txnStatusCode { get; set; }
        public string txnMessage { get; set; }
        public string txnDescription { get; set; }
        public string TransectionDatetime { get; set; }
        public string IpAddress { get; set; }
        public string Contribution_Id { get; set; }


    }

    public class APIResponsePaymentDetailBO
    {
        public string merchId { get; set; }
        public string merchTxnId { get; set; }
        public string merchTxnDate { get; set; }
        public string atomTxnId { get; set; }
        public string prodName { get; set; }
        public string prodAmount { get; set; }
        public string amount { get; set; }
        public string surchargeAmount { get; set; }
        public string totalAmount { get; set; }
        public string custAccNo { get; set; }
        public string clientCode { get; set; }
        public string txnCurrency { get; set; }
        public string signature { get; set; }
        public string txnInitDate { get; set; }
        public string txnCompleteDate { get; set; }
        public string subchannel { get; set; }
        public string otsBankId { get; set; }
        public string authId { get; set; }
        public string bankTxnId { get; set; }
        public string otsBankName { get; set; }
        public string cardType { get; set; }
        public string cardMaskNumber { get; set; }
        public string scheme { get; set; }
        public string statusCode { get; set; }
        public string message { get; set; }
        public string description { get; set; }
        public string TransectionDatetime { get; set; }
        public string IpAddress { get; set; }
        public string EntryBy { get; set; }
        public string UpdateBy { get; set; }
        public string Updatetime { get; set; }


    }


    public class PaymentListBO
    {
        public string atomTxnId { get; set; }
        public string bankTxnId { get; set; }
        public string custAccNo { get; set; }
        public string custMobile { get; set; }
        public string txnCompleteDate { get; set; }
        public string amount { get; set; }
        public string txnDescription { get; set; }
        public string message { get; set; }

    }
}
