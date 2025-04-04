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
	public partial class HistoryMasterDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public HistoryMasterDataContext() : 
				base(global::Unmehta.WebPortal.Data.Properties.Settings.Default.UNMehtaConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public HistoryMasterDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public HistoryMasterDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public HistoryMasterDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public HistoryMasterDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.GetHistoryMasterById")]
		public ISingleResult<GetHistoryMasterByIdResult> GetHistoryMasterById([global::System.Data.Linq.Mapping.ParameterAttribute(Name="Id", DbType="Int")] System.Nullable<int> id)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), id);
			return ((ISingleResult<GetHistoryMasterByIdResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.RemoveHistoryMaster")]
		public ISingleResult<RemoveHistoryMasterResult> RemoveHistoryMaster([global::System.Data.Linq.Mapping.ParameterAttribute(Name="Id", DbType="Int")] System.Nullable<int> id, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="UserName", DbType="VarChar(500)")] string userName)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), id, userName);
			return ((ISingleResult<RemoveHistoryMasterResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.GetAllHistoryMaster")]
		public ISingleResult<GetAllHistoryMasterResult> GetAllHistoryMaster([global::System.Data.Linq.Mapping.ParameterAttribute(Name="LangId", DbType="BigInt")] System.Nullable<long> langId)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), langId);
			return ((ISingleResult<GetAllHistoryMasterResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.InsertOrUpdateHistoryMaster")]
		public ISingleResult<InsertOrUpdateHistoryMasterResult> InsertOrUpdateHistoryMaster([global::System.Data.Linq.Mapping.ParameterAttribute(Name="Id", DbType="Int")] System.Nullable<int> id, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="LanguageId", DbType="BigInt")] System.Nullable<long> languageId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="MetaTitle", DbType="NVarChar(MAX)")] string metaTitle, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="MetaDescription", DbType="NVarChar(MAX)")] string metaDescription, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Year", DbType="NVarChar(10)")] string year, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="HistoryTitle", DbType="NVarChar(MAX)")] string historyTitle, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="HistoryDescription", DbType="NVarChar(MAX)")] string historyDescription, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="HistoryImage", DbType="NVarChar(MAX)")] string historyImage, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="IsVisible", DbType="Bit")] System.Nullable<bool> isVisible, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="UserName", DbType="NVarChar(500)")] string userName)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), id, languageId, metaTitle, metaDescription, year, historyTitle, historyDescription, historyImage, isVisible, userName);
			return ((ISingleResult<InsertOrUpdateHistoryMasterResult>)(result.ReturnValue));
		}
	}
	
	public partial class GetHistoryMasterByIdResult
	{
		
		private int _Id;
		
		private int _LanguageId;
		
		private string _LanguageName;
		
		private string _Year;
		
		private string _HistoryTitle;
		
		private string _HistoryDescription;
		
		private string _HistoryPhotoName;
		
		private string _HistoryPhotoPath;
		
		private System.Nullable<bool> _IsVisible;
		
		public GetHistoryMasterByIdResult()
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LanguageId", DbType="Int NOT NULL")]
		public int LanguageId
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LanguageName", DbType="NVarChar(500)")]
		public string LanguageName
		{
			get
			{
				return this._LanguageName;
			}
			set
			{
				if ((this._LanguageName != value))
				{
					this._LanguageName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Year", DbType="NVarChar(10)")]
		public string Year
		{
			get
			{
				return this._Year;
			}
			set
			{
				if ((this._Year != value))
				{
					this._Year = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_HistoryTitle", DbType="VarChar(200)")]
		public string HistoryTitle
		{
			get
			{
				return this._HistoryTitle;
			}
			set
			{
				if ((this._HistoryTitle != value))
				{
					this._HistoryTitle = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_HistoryDescription", DbType="NVarChar(MAX)")]
		public string HistoryDescription
		{
			get
			{
				return this._HistoryDescription;
			}
			set
			{
				if ((this._HistoryDescription != value))
				{
					this._HistoryDescription = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_HistoryPhotoName", DbType="NVarChar(MAX)")]
		public string HistoryPhotoName
		{
			get
			{
				return this._HistoryPhotoName;
			}
			set
			{
				if ((this._HistoryPhotoName != value))
				{
					this._HistoryPhotoName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_HistoryPhotoPath", DbType="NVarChar(MAX)")]
		public string HistoryPhotoPath
		{
			get
			{
				return this._HistoryPhotoPath;
			}
			set
			{
				if ((this._HistoryPhotoPath != value))
				{
					this._HistoryPhotoPath = value;
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
	
	public partial class RemoveHistoryMasterResult
	{
		
		private System.Nullable<int> _RecId;
		
		public RemoveHistoryMasterResult()
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
	
	public partial class GetAllHistoryMasterResult
	{
		
		private int _Id;
		
		private System.Nullable<long> _LanguageId;
		
		private string _Year;
		
		private string _HistoryTitle;
		
		private string _HistoryDescription;
		
		private string _MetaTitle;
		
		private string _MetaDescription;
		
		private string _HistoryImage;
		
		private System.Nullable<bool> _IsVisible;
		
		public GetAllHistoryMasterResult()
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Year", DbType="NVarChar(10)")]
		public string Year
		{
			get
			{
				return this._Year;
			}
			set
			{
				if ((this._Year != value))
				{
					this._Year = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_HistoryTitle", DbType="NVarChar(MAX)")]
		public string HistoryTitle
		{
			get
			{
				return this._HistoryTitle;
			}
			set
			{
				if ((this._HistoryTitle != value))
				{
					this._HistoryTitle = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_HistoryDescription", DbType="NVarChar(MAX)")]
		public string HistoryDescription
		{
			get
			{
				return this._HistoryDescription;
			}
			set
			{
				if ((this._HistoryDescription != value))
				{
					this._HistoryDescription = value;
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_HistoryImage", DbType="NVarChar(MAX)")]
		public string HistoryImage
		{
			get
			{
				return this._HistoryImage;
			}
			set
			{
				if ((this._HistoryImage != value))
				{
					this._HistoryImage = value;
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
	
	public partial class InsertOrUpdateHistoryMasterResult
	{
		
		private System.Nullable<int> _RecId;
		
		public InsertOrUpdateHistoryMasterResult()
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
}
#pragma warning restore 1591
