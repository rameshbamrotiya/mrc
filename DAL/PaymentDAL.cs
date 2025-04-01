using BO;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class PaymentDAL : DBConnection
    {
        protected bool InsertAPIRequest(APIRequestPaymentBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_AuthApiRequest_Insert";
                command.Parameters.AddWithValue("@version", objbo.version);
                command.Parameters.AddWithValue("@api", objbo.api);
                command.Parameters.AddWithValue("@platform", objbo.platform);
                command.Parameters.AddWithValue("@merchId", objbo.merchId);
                command.Parameters.AddWithValue("@userId", objbo.userId);
                command.Parameters.AddWithValue("@password", objbo.password);
                command.Parameters.AddWithValue("@merchTxnId", objbo.merchTxnId);
                command.Parameters.AddWithValue("@merchTxnDate", objbo.merchTxnDate);
                command.Parameters.AddWithValue("@amount", objbo.amount);
                command.Parameters.AddWithValue("@product", objbo.product);
                command.Parameters.AddWithValue("@custAccNo", objbo.custAccNo);
                command.Parameters.AddWithValue("@txnCurrency", objbo.txnCurrency);
                command.Parameters.AddWithValue("@custEmail", objbo.custEmail);
                command.Parameters.AddWithValue("@custMobile", objbo.custMobile);
                command.Parameters.AddWithValue("@udf1", objbo.udf1);
                command.Parameters.AddWithValue("@udf2", objbo.udf2);
                command.Parameters.AddWithValue("@udf3", objbo.udf3);
                command.Parameters.AddWithValue("@udf4", objbo.udf4);
                command.Parameters.AddWithValue("@udf5", objbo.udf5);
                command.Parameters.AddWithValue("@Remarks", objbo.Remarks);
                command.Parameters.AddWithValue("@TransectionDatetime", objbo.TransectionDatetime);
                command.Parameters.AddWithValue("@IpAddress", objbo.IpAddress);
                command.Parameters.AddWithValue("@Contribution_Id", objbo.Contribution_Id);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected bool InsertAPIResponse(APIResponsePaymentBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_AuthApiResponse_Insert";
                command.Parameters.AddWithValue("@atomTokenId", objbo.atomTokenId);
                command.Parameters.AddWithValue("@txnStatusCode", objbo.txnStatusCode);
                command.Parameters.AddWithValue("@txnMessage", objbo.txnMessage);
                command.Parameters.AddWithValue("@txnDescription", objbo.txnDescription);
                command.Parameters.AddWithValue("@TransectionDatetime", objbo.TransectionDatetime);
                command.Parameters.AddWithValue("@IpAddress", objbo.IpAddress);
                command.Parameters.AddWithValue("@Contribution_Id", objbo.Contribution_Id);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected bool InsertAPIResponseDetail(APIResponsePaymentDetailBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_TransactionResponse_Insert";
                command.Parameters.AddWithValue("@merchId", objbo.merchId);
                command.Parameters.AddWithValue("@merchTxnId", objbo.merchTxnId);
                command.Parameters.AddWithValue("@merchTxnDate", objbo.merchTxnDate);
                command.Parameters.AddWithValue("@atomTxnId", objbo.atomTxnId);
                command.Parameters.AddWithValue("@prodName", objbo.prodName);
                command.Parameters.AddWithValue("@prodAmount", objbo.prodAmount);
                command.Parameters.AddWithValue("@amount", objbo.amount);
                command.Parameters.AddWithValue("@surchargeAmount", objbo.surchargeAmount);
                command.Parameters.AddWithValue("@totalAmount", objbo.totalAmount);
                command.Parameters.AddWithValue("@custAccNo", objbo.custAccNo);
                command.Parameters.AddWithValue("@clientCode", objbo.clientCode);
                command.Parameters.AddWithValue("@txnCurrency", objbo.txnCurrency);
                command.Parameters.AddWithValue("@signature", objbo.signature);
                command.Parameters.AddWithValue("@txnInitDate", objbo.txnInitDate);
                command.Parameters.AddWithValue("@txnCompleteDate", objbo.txnCompleteDate);
                command.Parameters.AddWithValue("@subchannel", objbo.subchannel);
                command.Parameters.AddWithValue("@otsBankId", objbo.otsBankId);
                command.Parameters.AddWithValue("@authId", string.IsNullOrEmpty(objbo.authId) ? DBNull.Value.ToString() : objbo.authId);
                command.Parameters.AddWithValue("@bankTxnId", objbo.bankTxnId);
                command.Parameters.AddWithValue("@otsBankName", objbo.otsBankName);
                command.Parameters.AddWithValue("@cardType", string.IsNullOrEmpty(objbo.cardType) ? DBNull.Value.ToString() : objbo.cardType);
                command.Parameters.AddWithValue("@cardMaskNumber", string.IsNullOrEmpty(objbo.cardMaskNumber) ? DBNull.Value.ToString() : objbo.cardMaskNumber);
                command.Parameters.AddWithValue("@scheme", string.IsNullOrEmpty(objbo.scheme) ? DBNull.Value.ToString() : objbo.scheme);
                command.Parameters.AddWithValue("@statusCode", objbo.statusCode);
                command.Parameters.AddWithValue("@message", objbo.message);
                command.Parameters.AddWithValue("@description", objbo.description);
                command.Parameters.AddWithValue("@TransectionDatetime", objbo.TransectionDatetime);
                command.Parameters.AddWithValue("@IpAddress", objbo.IpAddress);
                command.Parameters.AddWithValue("@EntryBy", objbo.EntryBy);
                command.Parameters.AddWithValue("@UpdateBy", objbo.UpdateBy);
                command.Parameters.AddWithValue("@Updatetime", string.IsNullOrEmpty(objbo.Updatetime) ? DBNull.Value.ToString() : objbo.Updatetime);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected DataSet GetPaymentData(string Mobileno)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetPaymentDetailByMobile";
                command.Parameters.AddWithValue("@Mobileno", Mobileno);
                ExecuteNonQuery(command);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected DataSet GetPaymentDetails(string MerchTxnId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetPaymentDetailByMerchTxnId";
                command.Parameters.AddWithValue("@MerchTxnId", MerchTxnId);
                ExecuteNonQuery(command);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetStudentRegistrationByMerchTxnId(string MerchTxnId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetStudentRegistrationByMerchTxnId";
                command.Parameters.AddWithValue("@MerchTxnId", MerchTxnId);
                ExecuteNonQuery(command);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected DataSet GetPaymentDetailsForRefund(string MerchTxnId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetPaymentDetailForRefund";
                command.Parameters.AddWithValue("@MerchTxnId", MerchTxnId);
                ExecuteNonQuery(command);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected bool InsertTransactionStatusRequest(APITransactionStatusRequest objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_TransactionStatusRequest_Insert";
                command.Parameters.AddWithValue("@api", objbo.api);
                command.Parameters.AddWithValue("@source", objbo.source);
                command.Parameters.AddWithValue("@merchId", objbo.merchId);
                command.Parameters.AddWithValue("@password", objbo.password);
                command.Parameters.AddWithValue("@merchTxnId", objbo.merchTxnId);
                command.Parameters.AddWithValue("@merchTxnDate", objbo.merchTxnDate);
                command.Parameters.AddWithValue("@signature", objbo.signature);
                command.Parameters.AddWithValue("@amount", objbo.amount);
                command.Parameters.AddWithValue("@txnCurrency", objbo.txnCurrency);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected bool InsertTransactionStatusResponse(APITransactionStatusResponse objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_TransactionStatusResponse_Insert";
                command.Parameters.AddWithValue("@reconStatus", objbo.reconStatus);
                command.Parameters.AddWithValue("@merchTxnId", objbo.merchTxnId);
                command.Parameters.AddWithValue("@merchTxnDate", objbo.merchTxnDate);
                command.Parameters.AddWithValue("@atomTxnId", objbo.atomTxnId);
                command.Parameters.AddWithValue("@amount", objbo.amount);
                command.Parameters.AddWithValue("@surchargeAmount", objbo.surchargeAmount);
                command.Parameters.AddWithValue("@totalAmount", objbo.totalAmount);
                command.Parameters.AddWithValue("@subChannel", objbo.subChannel);
                command.Parameters.AddWithValue("@otsBankId", objbo.otsBankId);
                command.Parameters.AddWithValue("@bankTxnId", objbo.bankTxnId);
                command.Parameters.AddWithValue("@cardMaskNumber", string.IsNullOrEmpty(objbo.cardMaskNumber) ? DBNull.Value.ToString() : objbo.cardMaskNumber);
                command.Parameters.AddWithValue("@statusCode", objbo.statusCode);
                command.Parameters.AddWithValue("@message", objbo.message);
                command.Parameters.AddWithValue("@description", objbo.description);

                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected bool InsertTransactionRefundRequest(APITransactionRefundRequest objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_TransactionRefundRequest_Insert";
                command.Parameters.AddWithValue("@api", objbo.api);
                command.Parameters.AddWithValue("@merchId", objbo.merchId);
                command.Parameters.AddWithValue("@password", objbo.password);
                command.Parameters.AddWithValue("@merchTxnId", objbo.merchTxnId);
                command.Parameters.AddWithValue("@atomTxnId", objbo.atomTxnId);
                command.Parameters.AddWithValue("@txnCurrency", objbo.txnCurrency);
                command.Parameters.AddWithValue("@signature", objbo.signature);
                command.Parameters.AddWithValue("@totalRefundAmount", objbo.totalRefundAmount);
                command.Parameters.AddWithValue("@prodName", objbo.prodName);
                command.Parameters.AddWithValue("@prodRefundId", objbo.prodRefundId);
                command.Parameters.AddWithValue("@prodRefundAmount", objbo.prodRefundAmount);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected bool InsertTransactionRefundResponse(APITransactionRefundResponse objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_TransactionRefundResponse_Insert";
                command.Parameters.AddWithValue("@atomTxnId", objbo.atomTxnId);
                command.Parameters.AddWithValue("@totalRefundAmount", objbo.totalRefundAmount);
                command.Parameters.AddWithValue("@txnCurrency", objbo.txnCurrency);
                command.Parameters.AddWithValue("@prodName", objbo.prodName);
                command.Parameters.AddWithValue("@prodRefundAmount", objbo.prodRefundAmount);
                command.Parameters.AddWithValue("@prodRefundId", objbo.prodRefundId);
                command.Parameters.AddWithValue("@refundTxnId", objbo.refundTxnId);
                command.Parameters.AddWithValue("@prodDescription", objbo.prodDescription);
                command.Parameters.AddWithValue("@prodStatusCode", objbo.prodStatusCode);
                command.Parameters.AddWithValue("@statusCode", objbo.statusCode);
                command.Parameters.AddWithValue("@message", objbo.message);
                command.Parameters.AddWithValue("@description", objbo.description);
                command.Parameters.AddWithValue("@TransectionDatetime", objbo.TransectionDatetime);
                command.Parameters.AddWithValue("@IpAddress", objbo.IpAddress);
                command.Parameters.AddWithValue("@Contribution_Id", objbo.Contribution_Id);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected bool UpdateStatusbyMerchTxnId(string MerchtxnId)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "UpdateStatusbyMerchTxnId";
                command.Parameters.AddWithValue("@MerchTxnId", MerchtxnId);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected bool UpdateAPIRequestErrorData(int ErrorFlag, string ErrorDesc, string MerchtxnId)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "UpdateAPIRequestErrorbyMerchTxnId";
                command.Parameters.AddWithValue("@ErrorFlag", ErrorFlag);
                command.Parameters.AddWithValue("@ErrorDesc", ErrorDesc);
                command.Parameters.AddWithValue("@MerchTxnId", MerchtxnId);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected bool UpdateAtomTxnID(string AtomtxnId, string MerchtxnId)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "UpdateAtomTxnIDbyMerchTxnId";
                command.Parameters.AddWithValue("@ATOMTxnId", AtomtxnId);
                command.Parameters.AddWithValue("@MerchTxnId", MerchtxnId);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected string GetStatuscodeData(string Statuscode,string description)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GETERRORCODE";
                command.Parameters.AddWithValue("@StatusCode", Statuscode);
                command.Parameters.AddWithValue("@description", description);
                string result = ExecuteScalar(command).ToString();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected DataSet GetDatewiseDonation(string startdate, string enddate)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllDonationListDatewise";
                command.Parameters.AddWithValue("@startdate", startdate);
                command.Parameters.AddWithValue("@enddate", enddate);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
