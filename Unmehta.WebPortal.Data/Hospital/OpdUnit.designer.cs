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
	public partial class OpdUnitDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public OpdUnitDataContext() : 
				base(global::Unmehta.WebPortal.Data.Properties.Settings.Default.UNMehtaConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public OpdUnitDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public OpdUnitDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public OpdUnitDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public OpdUnitDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.GetAllOpdUnitMaster")]
		public ISingleResult<GetAllOpdUnitMasterResult> GetAllOpdUnitMaster([global::System.Data.Linq.Mapping.ParameterAttribute(Name="LangId", DbType="BigInt")] System.Nullable<long> langId)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), langId);
			return ((ISingleResult<GetAllOpdUnitMasterResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.InsertOrUpdateOpdUnitMaster")]
		public ISingleResult<InsertOrUpdateOpdUnitMasterResult> InsertOrUpdateOpdUnitMaster([global::System.Data.Linq.Mapping.ParameterAttribute(Name="Id", DbType="BigInt")] System.Nullable<long> id, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="LanguageId", DbType="BigInt")] System.Nullable<long> languageId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="UnitName", DbType="NVarChar(MAX)")] string unitName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="IsVisible", DbType="Bit")] System.Nullable<bool> isVisible, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="UserName", DbType="NVarChar(500)")] string userName)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), id, languageId, unitName, isVisible, userName);
			return ((ISingleResult<InsertOrUpdateOpdUnitMasterResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.RemoveOpdUnitMaster")]
		public int RemoveOpdUnitMaster([global::System.Data.Linq.Mapping.ParameterAttribute(Name="Id", DbType="Int")] System.Nullable<int> id, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="UserName", DbType="NVarChar(100)")] string userName)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), id, userName);
			return ((int)(result.ReturnValue));
		}
	}
	
	public partial class GetAllOpdUnitMasterResult
	{
		
		private long _Id;
		
		private string _UnitName;
		
		private System.Nullable<long> _LanguageId;
		
		private System.Nullable<bool> _IsVisible;
		
		public GetAllOpdUnitMasterResult()
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UnitName", DbType="NVarChar(200)")]
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
	}
	
	public partial class InsertOrUpdateOpdUnitMasterResult
	{
		
		private System.Nullable<long> _RecId;
		
		public InsertOrUpdateOpdUnitMasterResult()
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
}
#pragma warning restore 1591
