using BO;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class PaymentBAL : PaymentDAL
    {
        public bool InsertAPIRequestRecord(APIRequestPaymentBO objBO)
        {
            try
            {
                return InsertAPIRequest(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InsertAPIResponseRecord(APIResponsePaymentBO objBO)
        {
            try
            {
                return InsertAPIResponse(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InsertAPIResponseDetailRecord(APIResponsePaymentDetailBO objBO)
        {
            try
            {
                return InsertAPIResponseDetail(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet GetPayment(string Mobileno)
        {
            try
            {
                return GetPaymentData(Mobileno);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet GetPaymentDetail(string MerchtxnId)
        {
            try
            {
                return GetPaymentDetails(MerchtxnId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetStudentRegistrationByMerchTxnIdDetail(string MerchtxnId)
        {
            try
            {
                return GetStudentRegistrationByMerchTxnId(MerchtxnId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet GetDetailForRefund(string MerchtxnId)
        {
            try
            {
                return GetPaymentDetailsForRefund(MerchtxnId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GetStatuscodeDetail(string statuscode,string description)
        {
            try
            {
                return GetStatuscodeData(statuscode, description);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InsertTransactionStatusRequestData(APITransactionStatusRequest objBO)
        {
            try
            {
                return InsertTransactionStatusRequest(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InsertTransactionStatusResponseData(APITransactionStatusResponse objBO)
        {
            try
            {
                return InsertTransactionStatusResponse(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateStatusForDonation(string MerchTxnid)
        {
            try
            {
                return UpdateStatusbyMerchTxnId(MerchTxnid);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateAPIRequestError(int ErrorFlag, string ErrorDesc, string MerchTxnid)
        {
            try
            {
                return UpdateAPIRequestErrorData(ErrorFlag, ErrorDesc, MerchTxnid);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateAtomTxnIDForRequest(string AtomtxnId, string MerchTxnid)
        {
            try
            {
                return UpdateAtomTxnID(AtomtxnId, MerchTxnid);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet SelectDatewiseDonationRecord(string startdate, string enddate)
        {
            try
            {
                return GetDatewiseDonation(startdate, enddate);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
