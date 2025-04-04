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
	public partial class AboutUsMasterDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public AboutUsMasterDataContext() : 
				base(global::Unmehta.WebPortal.Data.Properties.Settings.Default.UNMehtaConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public AboutUsMasterDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public AboutUsMasterDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public AboutUsMasterDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public AboutUsMasterDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.InsertOrUpdateAboutUsDesignationMaster")]
		public ISingleResult<InsertOrUpdateAboutUsDesignationMasterResult> InsertOrUpdateAboutUsDesignationMaster([global::System.Data.Linq.Mapping.ParameterAttribute(Name="Id", DbType="BigInt")] System.Nullable<long> id, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="AboutUsId", DbType="BigInt")] System.Nullable<long> aboutUsId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="LangId", DbType="BigInt")] System.Nullable<long> langId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="DesignationName", DbType="NVarChar(MAX)")] string designationName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="DesignationId", DbType="Int")] System.Nullable<int> designationId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="PhotoName", DbType="NVarChar(MAX)")] string photoName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="PhotoPath", DbType="NVarChar(MAX)")] string photoPath, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Message", DbType="NVarChar(MAX)")] string message, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Username", DbType="NVarChar(MAX)")] string username)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), id, aboutUsId, langId, designationName, designationId, photoName, photoPath, message, username);
			return ((ISingleResult<InsertOrUpdateAboutUsDesignationMasterResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.RemoveAboutUsDesignationMaster")]
		public int RemoveAboutUsDesignationMaster([global::System.Data.Linq.Mapping.ParameterAttribute(Name="Id", DbType="BigInt")] System.Nullable<long> id, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="UserName", DbType="NVarChar(MAX)")] string userName)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), id, userName);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.RemoveAboutUsMaster")]
		public ISingleResult<RemoveAboutUsMasterResult> RemoveAboutUsMaster([global::System.Data.Linq.Mapping.ParameterAttribute(Name="Id", DbType="BigInt")] System.Nullable<long> id, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="UserName", DbType="VarChar(500)")] string userName)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), id, userName);
			return ((ISingleResult<RemoveAboutUsMasterResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.GetAllAboutUsDesignationMasterByLangId")]
		public ISingleResult<GetAllAboutUsDesignationMasterByLangIdResult> GetAllAboutUsDesignationMasterByLangId([global::System.Data.Linq.Mapping.ParameterAttribute(Name="AboutUsId", DbType="BigInt")] System.Nullable<long> aboutUsId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="LangId", DbType="BigInt")] System.Nullable<long> langId)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), aboutUsId, langId);
			return ((ISingleResult<GetAllAboutUsDesignationMasterByLangIdResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.GetAllAboutUsMaster")]
		public ISingleResult<GetAllAboutUsMasterResult> GetAllAboutUsMaster([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="BigInt")] System.Nullable<long> lanId)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), lanId);
			return ((ISingleResult<GetAllAboutUsMasterResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.InsertOrUpdateAboutUsMaster")]
		public ISingleResult<InsertOrUpdateAboutUsMasterResult> InsertOrUpdateAboutUsMaster([global::System.Data.Linq.Mapping.ParameterAttribute(Name="Id", DbType="BigInt")] System.Nullable<long> id, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="LanguageId", DbType="BigInt")] System.Nullable<long> languageId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="MetaTitle", DbType="NVarChar(MAX)")] string metaTitle, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="MetaDescription", DbType="NVarChar(MAX)")] string metaDescription, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="AboutUsDescription", DbType="NVarChar(MAX)")] string aboutUsDescription, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="HeadingTitle", DbType="NVarChar(MAX)")] string headingTitle, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="RightSideHeadingTitle", DbType="NVarChar(MAX)")] string rightSideHeadingTitle, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="UserName", DbType="NVarChar(500)")] string userName)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), id, languageId, metaTitle, metaDescription, aboutUsDescription, headingTitle, rightSideHeadingTitle, userName);
			return ((ISingleResult<InsertOrUpdateAboutUsMasterResult>)(result.ReturnValue));
		}
	}
	
	public partial class InsertOrUpdateAboutUsDesignationMasterResult
	{
		
		private System.Nullable<long> _RecId;
		
		public InsertOrUpdateAboutUsDesignationMasterResult()
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
	
	public partial class RemoveAboutUsMasterResult
	{
		
		private System.Nullable<long> _RecId;
		
		public RemoveAboutUsMasterResult()
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
	
	public partial class GetAllAboutUsDesignationMasterByLangIdResult
	{
		
		private long _Id;
		
		private string _DesignationName;
		
		private System.Nullable<int> _DesignationId;
		
		private string _DesName;
		
		private string _PhotoName;
		
		private string _PhotoPath;
		
		private string _Message;
		
		public GetAllAboutUsDesignationMasterByLangIdResult()
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DesignationName", DbType="NVarChar(MAX)")]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DesName", DbType="NVarChar(200)")]
		public string DesName
		{
			get
			{
				return this._DesName;
			}
			set
			{
				if ((this._DesName != value))
				{
					this._DesName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PhotoName", DbType="NVarChar(MAX)")]
		public string PhotoName
		{
			get
			{
				return this._PhotoName;
			}
			set
			{
				if ((this._PhotoName != value))
				{
					this._PhotoName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PhotoPath", DbType="NVarChar(MAX)")]
		public string PhotoPath
		{
			get
			{
				return this._PhotoPath;
			}
			set
			{
				if ((this._PhotoPath != value))
				{
					this._PhotoPath = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Message", DbType="NVarChar(MAX)")]
		public string Message
		{
			get
			{
				return this._Message;
			}
			set
			{
				if ((this._Message != value))
				{
					this._Message = value;
				}
			}
		}
	}
	
	public partial class GetAllAboutUsMasterResult
	{
		
		private long _Id;
		
		private System.Nullable<long> _LanguageId;
		
		private string _AboutUsDescription;
		
		private string _MetaTitle;
		
		private string _MetaDescription;
		
		private string _HeadingTitle;
		
		private string _RightSideHeadingTitle;
		
		public GetAllAboutUsMasterResult()
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AboutUsDescription", DbType="NVarChar(MAX)")]
		public string AboutUsDescription
		{
			get
			{
				return this._AboutUsDescription;
			}
			set
			{
				if ((this._AboutUsDescription != value))
				{
					this._AboutUsDescription = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MetaTitle", DbType="NVarChar(MAX)")]
		public string MetaTitle
		{
			get
			{
				return this._MetaTitle;
			}
			set
			{
				if ((this._MetaTitle != value))
				{
					this._MetaTitle = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MetaDescription", DbType="NVarChar(MAX)")]
		public string MetaDescription
		{
			get
			{
				return this._MetaDescription;
			}
			set
			{
				if ((this._MetaDescription != value))
				{
					this._MetaDescription = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_HeadingTitle", DbType="NVarChar(MAX)")]
		public string HeadingTitle
		{
			get
			{
				return this._HeadingTitle;
			}
			set
			{
				if ((this._HeadingTitle != value))
				{
					this._HeadingTitle = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RightSideHeadingTitle", DbType="NVarChar(MAX)")]
		public string RightSideHeadingTitle
		{
			get
			{
				return this._RightSideHeadingTitle;
			}
			set
			{
				if ((this._RightSideHeadingTitle != value))
				{
					this._RightSideHeadingTitle = value;
				}
			}
		}
	}
	
	public partial class InsertOrUpdateAboutUsMasterResult
	{
		
		private System.Nullable<long> _RecId;
		
		public InsertOrUpdateAboutUsMasterResult()
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
