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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="UNMWeb")]
	public partial class FrontAppointmentDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public FrontAppointmentDataContext() : 
				base(global::Unmehta.WebPortal.Data.Properties.Settings.Default.UNMWebConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public FrontAppointmentDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public FrontAppointmentDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public FrontAppointmentDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public FrontAppointmentDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.GetAllDepartmentForAppointment")]
		public ISingleResult<GetAllDepartmentForAppointmentResult> GetAllDepartmentForAppointment()
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
			return ((ISingleResult<GetAllDepartmentForAppointmentResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.GetWeekNoFromDeptIdForAppointment")]
		public ISingleResult<GetWeekNoFromDeptIdForAppointmentResult> GetWeekNoFromDeptIdForAppointment([global::System.Data.Linq.Mapping.ParameterAttribute(Name="DeptId", DbType="BigInt")] System.Nullable<long> deptId)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), deptId);
			return ((ISingleResult<GetWeekNoFromDeptIdForAppointmentResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.Insertorupdatepatientappointmentmaster")]
		public ISingleResult<InsertorupdatepatientappointmentmasterResult> Insertorupdatepatientappointmentmaster([global::System.Data.Linq.Mapping.ParameterAttribute(Name="Id", DbType="BigInt")] System.Nullable<long> id, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="PatientName", DbType="NVarChar(200)")] string patientName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="EMail", DbType="NVarChar(100)")] string eMail, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="MobileNo", DbType="NVarChar(12)")] string mobileNo, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="VisitTypeId", DbType="Int")] System.Nullable<int> visitTypeId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="UNMId", DbType="NVarChar(100)")] string uNMId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="ReasonForVisit", DbType="NVarChar(MAX)")] string reasonForVisit, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="UnitId", DbType="BigInt")] System.Nullable<long> unitId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="DoctorId", DbType="BigInt")] System.Nullable<long> doctorId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="SlotId", DbType="Int")] System.Nullable<int> slotId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="AppointmentDate", DbType="Date")] System.Nullable<System.DateTime> appointmentDate, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Username", DbType="NVarChar(12)")] string username)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), id, patientName, eMail, mobileNo, visitTypeId, uNMId, reasonForVisit, unitId, doctorId, slotId, appointmentDate, username);
			return ((ISingleResult<InsertorupdatepatientappointmentmasterResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.GetAllUnitByDeptIdForAppointment")]
		public ISingleResult<GetAllUnitByDeptIdForAppointmentResult> GetAllUnitByDeptIdForAppointment([global::System.Data.Linq.Mapping.ParameterAttribute(Name="DeptId", DbType="Int")] System.Nullable<int> deptId)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), deptId);
			return ((ISingleResult<GetAllUnitByDeptIdForAppointmentResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.GetAllVisitTypeForAppointment")]
		public ISingleResult<GetAllVisitTypeForAppointmentResult> GetAllVisitTypeForAppointment()
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
			return ((ISingleResult<GetAllVisitTypeForAppointmentResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.CheckSloatAlreadyBookOrNot")]
		public ISingleResult<CheckSloatAlreadyBookOrNotResult> CheckSloatAlreadyBookOrNot([global::System.Data.Linq.Mapping.ParameterAttribute(Name="AppointmentDate", DbType="Date")] System.Nullable<System.DateTime> appointmentDate, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="UnitId", DbType="BigInt")] System.Nullable<long> unitId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="DoctorId", DbType="BigInt")] System.Nullable<long> doctorId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="SlotId", DbType="BigInt")] System.Nullable<long> slotId)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), appointmentDate, unitId, doctorId, slotId);
			return ((ISingleResult<CheckSloatAlreadyBookOrNotResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.GetSlotListFromWeekNoForAppointment")]
		public ISingleResult<GetSlotListFromWeekNoForAppointmentResult> GetSlotListFromWeekNoForAppointment([global::System.Data.Linq.Mapping.ParameterAttribute(Name="WeekNoId", DbType="Int")] System.Nullable<int> weekNoId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="UnitId", DbType="Int")] System.Nullable<int> unitId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="AppointmentDate", DbType="Date")] System.Nullable<System.DateTime> appointmentDate, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="DoctorId", DbType="Int")] System.Nullable<int> doctorId)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), weekNoId, unitId, appointmentDate, doctorId);
			return ((ISingleResult<GetSlotListFromWeekNoForAppointmentResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.GetAllDoctorBySlotIdDeptIdForAppointment")]
		public ISingleResult<GetAllDoctorBySlotIdDeptIdForAppointmentResult> GetAllDoctorBySlotIdDeptIdForAppointment([global::System.Data.Linq.Mapping.ParameterAttribute(Name="DeptId", DbType="Int")] System.Nullable<int> deptId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="UnitId", DbType="Int")] System.Nullable<int> unitId)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), deptId, unitId);
			return ((ISingleResult<GetAllDoctorBySlotIdDeptIdForAppointmentResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.CheckSameSloatAlreadyBookOrNot")]
		public ISingleResult<CheckSameSloatAlreadyBookOrNotResult> CheckSameSloatAlreadyBookOrNot([global::System.Data.Linq.Mapping.ParameterAttribute(Name="MobileNo", DbType="VarChar(10)")] string mobileNo, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="AppointmentDate", DbType="Date")] System.Nullable<System.DateTime> appointmentDate, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="UnitId", DbType="BigInt")] System.Nullable<long> unitId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="DoctorId", DbType="BigInt")] System.Nullable<long> doctorId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="SlotId", DbType="BigInt")] System.Nullable<long> slotId)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), mobileNo, appointmentDate, unitId, doctorId, slotId);
			return ((ISingleResult<CheckSameSloatAlreadyBookOrNotResult>)(result.ReturnValue));
		}
	}
	
	public partial class GetAllDepartmentForAppointmentResult
	{
		
		private long _Id;
		
		private string _DepartmentName;
		
		public GetAllDepartmentForAppointmentResult()
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DepartmentName", DbType="NVarChar(200)")]
		public string DepartmentName
		{
			get
			{
				return this._DepartmentName;
			}
			set
			{
				if ((this._DepartmentName != value))
				{
					this._DepartmentName = value;
				}
			}
		}
	}
	
	public partial class GetWeekNoFromDeptIdForAppointmentResult
	{
		
		private System.Nullable<int> _WeekNo;
		
		private string _WeekdayName;
		
		public GetWeekNoFromDeptIdForAppointmentResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_WeekNo", DbType="Int")]
		public System.Nullable<int> WeekNo
		{
			get
			{
				return this._WeekNo;
			}
			set
			{
				if ((this._WeekNo != value))
				{
					this._WeekNo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_WeekdayName", DbType="NVarChar(30)")]
		public string WeekdayName
		{
			get
			{
				return this._WeekdayName;
			}
			set
			{
				if ((this._WeekdayName != value))
				{
					this._WeekdayName = value;
				}
			}
		}
	}
	
	public partial class InsertorupdatepatientappointmentmasterResult
	{
		
		private System.Nullable<long> _RecId;
		
		public InsertorupdatepatientappointmentmasterResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RecId", DbType="BigInt")]
		public System.Nullable<long> RecId
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
	
	public partial class GetAllUnitByDeptIdForAppointmentResult
	{
		
		private long _Id;
		
		private string _UnitName;
		
		public GetAllUnitByDeptIdForAppointmentResult()
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UnitName", DbType="NVarChar(250)")]
		public string UnitName
		{
			get
			{
				return this._UnitName;
			}
			set
			{
				if ((this._UnitName != value))
				{
					this._UnitName = value;
				}
			}
		}
	}
	
	public partial class GetAllVisitTypeForAppointmentResult
	{
		
		private int _Id;
		
		private string _VisitTypeName;
		
		public GetAllVisitTypeForAppointmentResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", DbType="Int NOT NULL")]
		public int Id
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_VisitTypeName", DbType="NVarChar(MAX)")]
		public string VisitTypeName
		{
			get
			{
				return this._VisitTypeName;
			}
			set
			{
				if ((this._VisitTypeName != value))
				{
					this._VisitTypeName = value;
				}
			}
		}
	}
	
	public partial class CheckSloatAlreadyBookOrNotResult
	{
		
		private string _SlotAvailability;
		
		public CheckSloatAlreadyBookOrNotResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SlotAvailability", DbType="VarChar(11) NOT NULL", CanBeNull=false)]
		public string SlotAvailability
		{
			get
			{
				return this._SlotAvailability;
			}
			set
			{
				if ((this._SlotAvailability != value))
				{
					this._SlotAvailability = value;
				}
			}
		}
	}
	
	public partial class GetSlotListFromWeekNoForAppointmentResult
	{
		
		private long _Id;
		
		private string _SloteName;
		
		private string _StartTimeHour;
		
		private string _StartTimeMin;
		
		private string _StartTimeTT;
		
		private string _EndTimeHour;
		
		private string _EndTimeMin;
		
		private string _EndTimeTT;
		
		private string _SlotAvailability;
		
		public GetSlotListFromWeekNoForAppointmentResult()
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SloteName", DbType="NVarChar(250)")]
		public string SloteName
		{
			get
			{
				return this._SloteName;
			}
			set
			{
				if ((this._SloteName != value))
				{
					this._SloteName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_StartTimeHour", DbType="NVarChar(2)")]
		public string StartTimeHour
		{
			get
			{
				return this._StartTimeHour;
			}
			set
			{
				if ((this._StartTimeHour != value))
				{
					this._StartTimeHour = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_StartTimeMin", DbType="NVarChar(2)")]
		public string StartTimeMin
		{
			get
			{
				return this._StartTimeMin;
			}
			set
			{
				if ((this._StartTimeMin != value))
				{
					this._StartTimeMin = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_StartTimeTT", DbType="NVarChar(2)")]
		public string StartTimeTT
		{
			get
			{
				return this._StartTimeTT;
			}
			set
			{
				if ((this._StartTimeTT != value))
				{
					this._StartTimeTT = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EndTimeHour", DbType="NVarChar(2)")]
		public string EndTimeHour
		{
			get
			{
				return this._EndTimeHour;
			}
			set
			{
				if ((this._EndTimeHour != value))
				{
					this._EndTimeHour = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EndTimeMin", DbType="NVarChar(2)")]
		public string EndTimeMin
		{
			get
			{
				return this._EndTimeMin;
			}
			set
			{
				if ((this._EndTimeMin != value))
				{
					this._EndTimeMin = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EndTimeTT", DbType="NVarChar(2)")]
		public string EndTimeTT
		{
			get
			{
				return this._EndTimeTT;
			}
			set
			{
				if ((this._EndTimeTT != value))
				{
					this._EndTimeTT = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SlotAvailability", DbType="VarChar(11) NOT NULL", CanBeNull=false)]
		public string SlotAvailability
		{
			get
			{
				return this._SlotAvailability;
			}
			set
			{
				if ((this._SlotAvailability != value))
				{
					this._SlotAvailability = value;
				}
			}
		}
	}
	
	public partial class GetAllDoctorBySlotIdDeptIdForAppointmentResult
	{
		
		private int _Id;
		
		private System.Nullable<int> _FacultyId;
		
		private System.Nullable<long> _LanguageId;
		
		private string _FacultyName;
		
		private string _ImageName;
		
		private string _ImagePath;
		
		private System.Nullable<int> _DesignationId;
		
		private System.Nullable<int> _DepartmentId;
		
		private string _DepartmentName;
		
		private System.Nullable<bool> _IsVisible;
		
		private System.Nullable<bool> _IsDelete;
		
		private string _MobileNumber;
		
		private string _Email;
		
		private string _FacultyDescription;
		
		private string _DesignationName;
		
		private System.Nullable<decimal> _sequenceNo;
		
		public GetAllDoctorBySlotIdDeptIdForAppointmentResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", DbType="Int NOT NULL")]
		public int Id
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FacultyId", DbType="Int")]
		public System.Nullable<int> FacultyId
		{
			get
			{
				return this._FacultyId;
			}
			set
			{
				if ((this._FacultyId != value))
				{
					this._FacultyId = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LanguageId", DbType="BigInt")]
		public System.Nullable<long> LanguageId
		{
			get
			{
				return this._LanguageId;
			}
			set
			{
				if ((this._LanguageId != value))
				{
					this._LanguageId = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FacultyName", DbType="NVarChar(200)")]
		public string FacultyName
		{
			get
			{
				return this._FacultyName;
			}
			set
			{
				if ((this._FacultyName != value))
				{
					this._FacultyName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ImageName", DbType="NVarChar(MAX)")]
		public string ImageName
		{
			get
			{
				return this._ImageName;
			}
			set
			{
				if ((this._ImageName != value))
				{
					this._ImageName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ImagePath", DbType="NVarChar(MAX)")]
		public string ImagePath
		{
			get
			{
				return this._ImagePath;
			}
			set
			{
				if ((this._ImagePath != value))
				{
					this._ImagePath = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DesignationId", DbType="Int")]
		public System.Nullable<int> DesignationId
		{
			get
			{
				return this._DesignationId;
			}
			set
			{
				if ((this._DesignationId != value))
				{
					this._DesignationId = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DepartmentId", DbType="Int")]
		public System.Nullable<int> DepartmentId
		{
			get
			{
				return this._DepartmentId;
			}
			set
			{
				if ((this._DepartmentId != value))
				{
					this._DepartmentId = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DepartmentName", DbType="NVarChar(200)")]
		public string DepartmentName
		{
			get
			{
				return this._DepartmentName;
			}
			set
			{
				if ((this._DepartmentName != value))
				{
					this._DepartmentName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsVisible", DbType="Bit")]
		public System.Nullable<bool> IsVisible
		{
			get
			{
				return this._IsVisible;
			}
			set
			{
				if ((this._IsVisible != value))
				{
					this._IsVisible = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsDelete", DbType="Bit")]
		public System.Nullable<bool> IsDelete
		{
			get
			{
				return this._IsDelete;
			}
			set
			{
				if ((this._IsDelete != value))
				{
					this._IsDelete = value;
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Email", DbType="NVarChar(50)")]
		public string Email
		{
			get
			{
				return this._Email;
			}
			set
			{
				if ((this._Email != value))
				{
					this._Email = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FacultyDescription", DbType="NVarChar(MAX)")]
		public string FacultyDescription
		{
			get
			{
				return this._FacultyDescription;
			}
			set
			{
				if ((this._FacultyDescription != value))
				{
					this._FacultyDescription = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DesignationName", DbType="VarChar(1) NOT NULL", CanBeNull=false)]
		public string DesignationName
		{
			get
			{
				return this._DesignationName;
			}
			set
			{
				if ((this._DesignationName != value))
				{
					this._DesignationName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_sequenceNo", DbType="Decimal(18,2)")]
		public System.Nullable<decimal> sequenceNo
		{
			get
			{
				return this._sequenceNo;
			}
			set
			{
				if ((this._sequenceNo != value))
				{
					this._sequenceNo = value;
				}
			}
		}
	}
	
	public partial class CheckSameSloatAlreadyBookOrNotResult
	{
		
		private int _Column1;
		
		public CheckSameSloatAlreadyBookOrNotResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="", Storage="_Column1", DbType="Int NOT NULL")]
		public int Column1
		{
			get
			{
				return this._Column1;
			}
			set
			{
				if ((this._Column1 != value))
				{
					this._Column1 = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
