﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Unmehta.WebPortal.Data.Common
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="UNMehta")]
	public partial class PaymentGatewayDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public PaymentGatewayDataContext() : 
				base(global::Unmehta.WebPortal.Data.Properties.Settings.Default.UNMehtaConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public PaymentGatewayDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public PaymentGatewayDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public PaymentGatewayDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public PaymentGatewayDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.InsertPaymentTransactionRequestMaster")]
		public ISingleResult<InsertPaymentTransactionRequestMasterResult> InsertPaymentTransactionRequestMaster(
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="PaymentGeneratedUrl", DbType="NVarChar(MAX)")] string paymentGeneratedUrl, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="TransactionCode", DbType="NVarChar(MAX)")] string transactionCode, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="MerchantId", DbType="NVarChar(MAX)")] string merchantId, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="UniTranNo", DbType="NVarChar(MAX)")] string uniTranNo, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="NA1", DbType="NVarChar(MAX)")] string nA1, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="TxnAmount", DbType="NVarChar(MAX)")] string txnAmount, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="NA2", DbType="NVarChar(MAX)")] string nA2, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="NA3", DbType="NVarChar(MAX)")] string nA3, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="NA4", DbType="NVarChar(MAX)")] string nA4, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="CurrencyType", DbType="NVarChar(MAX)")] string currencyType, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="NA5", DbType="NVarChar(MAX)")] string nA5, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="TypeField1", DbType="NVarChar(MAX)")] string typeField1, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="SecurityId", DbType="NVarChar(MAX)")] string securityId, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="NA6", DbType="NVarChar(MAX)")] string nA6, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="NA7", DbType="NVarChar(MAX)")] string nA7, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="TypeField2", DbType="NVarChar(MAX)")] string typeField2, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="AdditionalInfo1", DbType="NVarChar(MAX)")] string additionalInfo1, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="AdditionalInfo2", DbType="NVarChar(MAX)")] string additionalInfo2, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="AdditionalInfo3", DbType="NVarChar(MAX)")] string additionalInfo3, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="AdditionalInfo4", DbType="NVarChar(MAX)")] string additionalInfo4, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="AdditionalInfo5", DbType="NVarChar(MAX)")] string additionalInfo5, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="AdditionalInfo6", DbType="NVarChar(MAX)")] string additionalInfo6, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="AdditionalInfo7", DbType="NVarChar(MAX)")] string additionalInfo7, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="ReturnURL", DbType="NVarChar(MAX)")] string returnURL, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="ChecksumKey", DbType="NVarChar(MAX)")] string checksumKey, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="HashValue", DbType="NVarChar(MAX)")] string hashValue, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="MsgText", DbType="NVarChar(MAX)")] string msgText)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), paymentGeneratedUrl, transactionCode, merchantId, uniTranNo, nA1, txnAmount, nA2, nA3, nA4, currencyType, nA5, typeField1, securityId, nA6, nA7, typeField2, additionalInfo1, additionalInfo2, additionalInfo3, additionalInfo4, additionalInfo5, additionalInfo6, additionalInfo7, returnURL, checksumKey, hashValue, msgText);
			return ((ISingleResult<InsertPaymentTransactionRequestMasterResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.InsertPaymentTransactionResponseMaster")]
		public ISingleResult<InsertPaymentTransactionResponseMasterResult> InsertPaymentTransactionResponseMaster(
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="PaymentPath", DbType="NVarChar(MAX)")] string paymentPath, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="MerchantId", DbType="NVarChar(MAX)")] string merchantId, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="UniTranNo", DbType="NVarChar(MAX)")] string uniTranNo, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="TxnReferenceNo", DbType="NVarChar(MAX)")] string txnReferenceNo, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="BankReferenceNo", DbType="NVarChar(MAX)")] string bankReferenceNo, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="TxnAmount", DbType="NVarChar(MAX)")] string txnAmount, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="BankId", DbType="NVarChar(MAX)")] string bankId, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="BankMerchantId", DbType="NVarChar(MAX)")] string bankMerchantId, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="TxnType", DbType="NVarChar(MAX)")] string txnType, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="CurrencyType", DbType="NVarChar(MAX)")] string currencyType, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="ItemCode", DbType="NVarChar(MAX)")] string itemCode, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="SecurityType", DbType="NVarChar(MAX)")] string securityType, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="SecurityId", DbType="NVarChar(MAX)")] string securityId, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="SecurityPasswod", DbType="NVarChar(MAX)")] string securityPasswod, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="TxnDate", DbType="NVarChar(MAX)")] string txnDate, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="AuthStatus", DbType="NVarChar(MAX)")] string authStatus, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="SettlementType", DbType="NVarChar(MAX)")] string settlementType, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="AdditionalInfo1", DbType="NVarChar(MAX)")] string additionalInfo1, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="AdditionalInfo2", DbType="NVarChar(MAX)")] string additionalInfo2, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="AdditionalInfo3", DbType="NVarChar(MAX)")] string additionalInfo3, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="AdditionalInfo4", DbType="NVarChar(MAX)")] string additionalInfo4, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="AdditionalInfo5", DbType="NVarChar(MAX)")] string additionalInfo5, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="AdditionalInfo6", DbType="NVarChar(MAX)")] string additionalInfo6, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="AdditionalInfo7", DbType="NVarChar(MAX)")] string additionalInfo7, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="ErrorStatus", DbType="NVarChar(MAX)")] string errorStatus, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="ErrorDescription", DbType="NVarChar(MAX)")] string errorDescription, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="Checksum", DbType="NVarChar(MAX)")] string checksum, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="IsCheckSumMatch", DbType="Bit")] System.Nullable<bool> isCheckSumMatch, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="TxRefNo", DbType="NVarChar(MAX)")] string txRefNo, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="PgTxnNo", DbType="NVarChar(MAX)")] string pgTxnNo, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="TxAmount", DbType="NVarChar(MAX)")] string txAmount, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="TxStatus", DbType="NVarChar(MAX)")] string txStatus, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="TxMssg", DbType="NVarChar(MAX)")] string txMssg, 
					[global::System.Data.Linq.Mapping.ParameterAttribute(Name="PaymentFor", DbType="NVarChar(MAX)")] string paymentFor)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), paymentPath, merchantId, uniTranNo, txnReferenceNo, bankReferenceNo, txnAmount, bankId, bankMerchantId, txnType, currencyType, itemCode, securityType, securityId, securityPasswod, txnDate, authStatus, settlementType, additionalInfo1, additionalInfo2, additionalInfo3, additionalInfo4, additionalInfo5, additionalInfo6, additionalInfo7, errorStatus, errorDescription, checksum, isCheckSumMatch, txRefNo, pgTxnNo, txAmount, txStatus, txMssg, paymentFor);
			return ((ISingleResult<InsertPaymentTransactionResponseMasterResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.UpdatePaymentTransactionRequestMasterStatus")]
		public int UpdatePaymentTransactionRequestMasterStatus([global::System.Data.Linq.Mapping.ParameterAttribute(Name="TransactionCode", DbType="NVarChar(MAX)")] string transactionCode, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="IsHit", DbType="Bit")] System.Nullable<bool> isHit)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), transactionCode, isHit);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.ValidateTransactionCodeNotDublicate")]
		public ISingleResult<ValidateTransactionCodeNotDublicateResult> ValidateTransactionCodeNotDublicate([global::System.Data.Linq.Mapping.ParameterAttribute(Name="TransactionCode", DbType="NVarChar(MAX)")] string transactionCode)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), transactionCode);
			return ((ISingleResult<ValidateTransactionCodeNotDublicateResult>)(result.ReturnValue));
		}
	}
	
	public partial class InsertPaymentTransactionRequestMasterResult
	{
		
		private System.Nullable<decimal> _RecId;
		
		public InsertPaymentTransactionRequestMasterResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RecId", DbType="Decimal(38,0)")]
		public System.Nullable<decimal> RecId
		{
			get
			{
				return this._RecId;
			}
			set
			{
				if ((this._RecId != value))
				{
					this._RecId = value;
				}
			}
		}
	}
	
	public partial class InsertPaymentTransactionResponseMasterResult
	{
		
		private System.Nullable<decimal> _RecId;
		
		public InsertPaymentTransactionResponseMasterResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RecId", DbType="Decimal(38,0)")]
		public System.Nullable<decimal> RecId
		{
			get
			{
				return this._RecId;
			}
			set
			{
				if ((this._RecId != value))
				{
					this._RecId = value;
				}
			}
		}
	}
	
	public partial class ValidateTransactionCodeNotDublicateResult
	{
		
		private System.Nullable<bool> _IsExist;
		
		public ValidateTransactionCodeNotDublicateResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsExist", DbType="Bit")]
		public System.Nullable<bool> IsExist
		{
			get
			{
				return this._IsExist;
			}
			set
			{
				if ((this._IsExist != value))
				{
					this._IsExist = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
