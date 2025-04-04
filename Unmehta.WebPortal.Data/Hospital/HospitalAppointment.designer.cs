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

namespace Unmehta.WebPortal.Data.Hospital
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
	public partial class HospitalAppointmentDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public HospitalAppointmentDataContext() : 
				base(global::Unmehta.WebPortal.Data.Properties.Settings.Default.UNMehtaConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public HospitalAppointmentDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public HospitalAppointmentDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public HospitalAppointmentDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public HospitalAppointmentDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.GetAllHospitalAppointmentMasterBySpecializationId")]
		public ISingleResult<GetAllHospitalAppointmentMasterBySpecializationIdResult> GetAllHospitalAppointmentMasterBySpecializationId([global::System.Data.Linq.Mapping.ParameterAttribute(Name="SpecId", DbType="BigInt")] System.Nullable<long> specId)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), specId);
			return ((ISingleResult<GetAllHospitalAppointmentMasterBySpecializationIdResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.RemoveHospitalAppointmentMaster", IsComposable=true)]
		public object RemoveHospitalAppointmentMaster([global::System.Data.Linq.Mapping.ParameterAttribute(Name="Id", DbType="BigInt")] System.Nullable<long> id, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="UserName", DbType="VarChar(50)")] string userName)
		{
			return ((object)(this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), id, userName).ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.UpdateAppointmentMasterOtp")]
		public ISingleResult<UpdateAppointmentMasterOtpResult> UpdateAppointmentMasterOtp([global::System.Data.Linq.Mapping.ParameterAttribute(Name="Id", DbType="Int")] System.Nullable<int> id, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="IsOtpVerified", DbType="Bit")] System.Nullable<bool> isOtpVerified)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), id, isOtpVerified);
			return ((ISingleResult<UpdateAppointmentMasterOtpResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.GetAllHospitalAppointmentMasterByMobile")]
		public ISingleResult<GetAllHospitalAppointmentMasterByMobileResult> GetAllHospitalAppointmentMasterByMobile([global::System.Data.Linq.Mapping.ParameterAttribute(Name="MobileNo", DbType="NVarChar(MAX)")] string mobileNo)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), mobileNo);
			return ((ISingleResult<GetAllHospitalAppointmentMasterByMobileResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.InsertHospitalAppointmentMaster")]
		public ISingleResult<InsertHospitalAppointmentMasterResult> InsertHospitalAppointmentMaster([global::System.Data.Linq.Mapping.ParameterAttribute(Name="FirstName", DbType="NVarChar(MAX)")] string firstName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="MiddleName", DbType="NVarChar(MAX)")] string middleName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="LastName", DbType="NVarChar(MAX)")] string lastName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Gender", DbType="Int")] System.Nullable<int> gender, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="MobileNumber", DbType="NVarChar(50)")] string mobileNumber, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="EmailId", DbType="NVarChar(MAX)")] string emailId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="SpecializationId", DbType="BigInt")] System.Nullable<long> specializationId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="AppointmentDate", DbType="Date")] System.Nullable<System.DateTime> appointmentDate, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="AppointmentTime", DbType="NVarChar(50)")] string appointmentTime, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="ReasonForVisit", DbType="NVarChar(MAX)")] string reasonForVisit, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="AdditionalInformation", DbType="NVarChar(MAX)")] string additionalInformation, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="IsFollowUp", DbType="Bit")] System.Nullable<bool> isFollowUp, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="IsOTPVerify", DbType="Bit")] System.Nullable<bool> isOTPVerify, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="CreateBy", DbType="NVarChar(100)")] string createBy)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), firstName, middleName, lastName, gender, mobileNumber, emailId, specializationId, appointmentDate, appointmentTime, reasonForVisit, additionalInformation, isFollowUp, isOTPVerify, createBy);
			return ((ISingleResult<InsertHospitalAppointmentMasterResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.GetHospitalAppointmentMasterById")]
		public ISingleResult<GetHospitalAppointmentMasterByIdResult> GetHospitalAppointmentMasterById([global::System.Data.Linq.Mapping.ParameterAttribute(Name="Id", DbType="Int")] System.Nullable<int> id)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), id);
			return ((ISingleResult<GetHospitalAppointmentMasterByIdResult>)(result.ReturnValue));
		}
	}
	
	public partial class GetAllHospitalAppointmentMasterBySpecializationIdResult
	{
		
		private long _Id;
		
		private string _FirstName;
		
		private string _MiddleName;
		
		private string _LastName;
		
		private System.Nullable<int> _Gender;
		
		private string _MobileNumber;
		
		private string _EmailId;
		
		private System.Nullable<System.DateTime> _AppointmentDate;
		
		private string _AppointmentTime;
		
		private string _ReasonForVisit;
		
		private string _AdditionalInformation;
		
		private bool _IsFollowUp;
		
		public GetAllHospitalAppointmentMasterBySpecializationIdResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", DbType="BigInt NOT NULL")]
		public long Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this._Id = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FirstName", DbType="NVarChar(MAX)")]
		public string FirstName
		{
			get
			{
				return this._FirstName;
			}
			set
			{
				if ((this._FirstName != value))
				{
					this._FirstName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MiddleName", DbType="NVarChar(MAX)")]
		public string MiddleName
		{
			get
			{
				return this._MiddleName;
			}
			set
			{
				if ((this._MiddleName != value))
				{
					this._MiddleName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LastName", DbType="NVarChar(MAX)")]
		public string LastName
		{
			get
			{
				return this._LastName;
			}
			set
			{
				if ((this._LastName != value))
				{
					this._LastName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Gender", DbType="Int")]
		public System.Nullable<int> Gender
		{
			get
			{
				return this._Gender;
			}
			set
			{
				if ((this._Gender != value))
				{
					this._Gender = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MobileNumber", DbType="NVarChar(50)")]
		public string MobileNumber
		{
			get
			{
				return this._MobileNumber;
			}
			set
			{
				if ((this._MobileNumber != value))
				{
					this._MobileNumber = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EmailId", DbType="NVarChar(MAX)")]
		public string EmailId
		{
			get
			{
				return this._EmailId;
			}
			set
			{
				if ((this._EmailId != value))
				{
					this._EmailId = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AppointmentDate", DbType="Date")]
		public System.Nullable<System.DateTime> AppointmentDate
		{
			get
			{
				return this._AppointmentDate;
			}
			set
			{
				if ((this._AppointmentDate != value))
				{
					this._AppointmentDate = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AppointmentTime", DbType="NVarChar(50)")]
		public string AppointmentTime
		{
			get
			{
				return this._AppointmentTime;
			}
			set
			{
				if ((this._AppointmentTime != value))
				{
					this._AppointmentTime = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ReasonForVisit", DbType="NVarChar(MAX)")]
		public string ReasonForVisit
		{
			get
			{
				return this._ReasonForVisit;
			}
			set
			{
				if ((this._ReasonForVisit != value))
				{
					this._ReasonForVisit = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AdditionalInformation", DbType="NVarChar(MAX)")]
		public string AdditionalInformation
		{
			get
			{
				return this._AdditionalInformation;
			}
			set
			{
				if ((this._AdditionalInformation != value))
				{
					this._AdditionalInformation = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsFollowUp", DbType="Bit NOT NULL")]
		public bool IsFollowUp
		{
			get
			{
				return this._IsFollowUp;
			}
			set
			{
				if ((this._IsFollowUp != value))
				{
					this._IsFollowUp = value;
				}
			}
		}
	}
	
	public partial class UpdateAppointmentMasterOtpResult
	{
		
		private System.Nullable<int> _RecId;
		
		public UpdateAppointmentMasterOtpResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RecId", DbType="Int")]
		public System.Nullable<int> RecId
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
	
	public partial class GetAllHospitalAppointmentMasterByMobileResult
	{
		
		private long _Id;
		
		private string _FirstName;
		
		private string _MiddleName;
		
		private string _LastName;
		
		private string _Gender;
		
		private string _MobileNumber;
		
		private string _EmailId;
		
		private System.Nullable<System.DateTime> _AppointmentDate;
		
		private string _AppointmentTime;
		
		private string _ReasonForVisit;
		
		private string _AdditionalInformation;
		
		private string _FollowUp;
		
		public GetAllHospitalAppointmentMasterByMobileResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", DbType="BigInt NOT NULL")]
		public long Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this._Id = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FirstName", DbType="NVarChar(MAX)")]
		public string FirstName
		{
			get
			{
				return this._FirstName;
			}
			set
			{
				if ((this._FirstName != value))
				{
					this._FirstName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MiddleName", DbType="NVarChar(MAX)")]
		public string MiddleName
		{
			get
			{
				return this._MiddleName;
			}
			set
			{
				if ((this._MiddleName != value))
				{
					this._MiddleName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LastName", DbType="NVarChar(MAX)")]
		public string LastName
		{
			get
			{
				return this._LastName;
			}
			set
			{
				if ((this._LastName != value))
				{
					this._LastName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Gender", DbType="VarChar(6) NOT NULL", CanBeNull=false)]
		public string Gender
		{
			get
			{
				return this._Gender;
			}
			set
			{
				if ((this._Gender != value))
				{
					this._Gender = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MobileNumber", DbType="NVarChar(50)")]
		public string MobileNumber
		{
			get
			{
				return this._MobileNumber;
			}
			set
			{
				if ((this._MobileNumber != value))
				{
					this._MobileNumber = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EmailId", DbType="NVarChar(MAX)")]
		public string EmailId
		{
			get
			{
				return this._EmailId;
			}
			set
			{
				if ((this._EmailId != value))
				{
					this._EmailId = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AppointmentDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> AppointmentDate
		{
			get
			{
				return this._AppointmentDate;
			}
			set
			{
				if ((this._AppointmentDate != value))
				{
					this._AppointmentDate = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AppointmentTime", DbType="NVarChar(50)")]
		public string AppointmentTime
		{
			get
			{
				return this._AppointmentTime;
			}
			set
			{
				if ((this._AppointmentTime != value))
				{
					this._AppointmentTime = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ReasonForVisit", DbType="NVarChar(MAX)")]
		public string ReasonForVisit
		{
			get
			{
				return this._ReasonForVisit;
			}
			set
			{
				if ((this._ReasonForVisit != value))
				{
					this._ReasonForVisit = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AdditionalInformation", DbType="NVarChar(MAX)")]
		public string AdditionalInformation
		{
			get
			{
				return this._AdditionalInformation;
			}
			set
			{
				if ((this._AdditionalInformation != value))
				{
					this._AdditionalInformation = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FollowUp", DbType="VarChar(9) NOT NULL", CanBeNull=false)]
		public string FollowUp
		{
			get
			{
				return this._FollowUp;
			}
			set
			{
				if ((this._FollowUp != value))
				{
					this._FollowUp = value;
				}
			}
		}
	}
	
	public partial class InsertHospitalAppointmentMasterResult
	{
		
		private System.Nullable<decimal> _RecId;
		
		public InsertHospitalAppointmentMasterResult()
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
	
	public partial class GetHospitalAppointmentMasterByIdResult
	{
		
		private long _Id;
		
		private string _FirstName;
		
		private string _MiddleName;
		
		private string _LastName;
		
		private System.Nullable<int> _Gender;
		
		private string _MobileNumber;
		
		private string _EmailId;
		
		private System.Nullable<long> _SpecializationId;
		
		private System.Nullable<System.DateTime> _AppointmentDate;
		
		private string _AppointmentTime;
		
		private string _ReasonForVisit;
		
		private string _AdditionalInformation;
		
		private bool _IsFollowUp;
		
		public GetHospitalAppointmentMasterByIdResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", DbType="BigInt NOT NULL")]
		public long Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this._Id = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FirstName", DbType="NVarChar(MAX)")]
		public string FirstName
		{
			get
			{
				return this._FirstName;
			}
			set
			{
				if ((this._FirstName != value))
				{
					this._FirstName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MiddleName", DbType="NVarChar(MAX)")]
		public string MiddleName
		{
			get
			{
				return this._MiddleName;
			}
			set
			{
				if ((this._MiddleName != value))
				{
					this._MiddleName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LastName", DbType="NVarChar(MAX)")]
		public string LastName
		{
			get
			{
				return this._LastName;
			}
			set
			{
				if ((this._LastName != value))
				{
					this._LastName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Gender", DbType="Int")]
		public System.Nullable<int> Gender
		{
			get
			{
				return this._Gender;
			}
			set
			{
				if ((this._Gender != value))
				{
					this._Gender = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MobileNumber", DbType="NVarChar(50)")]
		public string MobileNumber
		{
			get
			{
				return this._MobileNumber;
			}
			set
			{
				if ((this._MobileNumber != value))
				{
					this._MobileNumber = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EmailId", DbType="NVarChar(MAX)")]
		public string EmailId
		{
			get
			{
				return this._EmailId;
			}
			set
			{
				if ((this._EmailId != value))
				{
					this._EmailId = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SpecializationId", DbType="BigInt")]
		public System.Nullable<long> SpecializationId
		{
			get
			{
				return this._SpecializationId;
			}
			set
			{
				if ((this._SpecializationId != value))
				{
					this._SpecializationId = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AppointmentDate", DbType="Date")]
		public System.Nullable<System.DateTime> AppointmentDate
		{
			get
			{
				return this._AppointmentDate;
			}
			set
			{
				if ((this._AppointmentDate != value))
				{
					this._AppointmentDate = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AppointmentTime", DbType="NVarChar(50)")]
		public string AppointmentTime
		{
			get
			{
				return this._AppointmentTime;
			}
			set
			{
				if ((this._AppointmentTime != value))
				{
					this._AppointmentTime = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ReasonForVisit", DbType="NVarChar(MAX)")]
		public string ReasonForVisit
		{
			get
			{
				return this._ReasonForVisit;
			}
			set
			{
				if ((this._ReasonForVisit != value))
				{
					this._ReasonForVisit = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AdditionalInformation", DbType="NVarChar(MAX)")]
		public string AdditionalInformation
		{
			get
			{
				return this._AdditionalInformation;
			}
			set
			{
				if ((this._AdditionalInformation != value))
				{
					this._AdditionalInformation = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsFollowUp", DbType="Bit NOT NULL")]
		public bool IsFollowUp
		{
			get
			{
				return this._IsFollowUp;
			}
			set
			{
				if ((this._IsFollowUp != value))
				{
					this._IsFollowUp = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
