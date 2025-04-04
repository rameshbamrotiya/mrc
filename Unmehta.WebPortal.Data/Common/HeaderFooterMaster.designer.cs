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
	public partial class HeaderFooterMasterDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public HeaderFooterMasterDataContext() : 
				base(global::Unmehta.WebPortal.Data.Properties.Settings.Default.UNMehtaConnectionString2, mappingSource)
		{
			OnCreated();
		}
		
		public HeaderFooterMasterDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public HeaderFooterMasterDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public HeaderFooterMasterDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public HeaderFooterMasterDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.GetAllReasearch")]
		public ISingleResult<GetAllReasearchResult> GetAllReasearch([global::System.Data.Linq.Mapping.ParameterAttribute(Name="LanguageId", DbType="BigInt")] System.Nullable<long> languageId)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), languageId);
			return ((ISingleResult<GetAllReasearchResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.GetHeaderFooter")]
		public ISingleResult<GetHeaderFooterResult> GetHeaderFooter()
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
			return ((ISingleResult<GetHeaderFooterResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.InsertOrUpdateHeaderFooterMaster")]
		public int InsertOrUpdateHeaderFooterMaster([global::System.Data.Linq.Mapping.ParameterAttribute(Name="Id", DbType="BigInt")] System.Nullable<long> id, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="HeaderDetails", DbType="NVarChar(MAX)")] string headerDetails, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="HeaderLogo", DbType="NVarChar(MAX)")] string headerLogo, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="FooterLogo", DbType="NVarChar(MAX)")] string footerLogo, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="FooterDetails", DbType="NVarChar(MAX)")] string footerDetails, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Username", DbType="NVarChar(MAX)")] string username)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), id, headerDetails, headerLogo, footerLogo, footerDetails, username);
			return ((int)(result.ReturnValue));
		}
	}
	
	public partial class GetAllReasearchResult
	{
		
		private System.Nullable<int> _AD_id;
		
		private string _Articles_Name;
		
		private string _Publication_Year;
		
		private string _Description;
		
		private string _Web_link;
		
		private string _Author;
		
		private string _Publication_Name;
		
		private string _ArticleType;
		
		private System.Nullable<int> _Publication_id;
		
		public GetAllReasearchResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AD_id", DbType="Int")]
		public System.Nullable<int> AD_id
		{
			get
			{
				return this._AD_id;
			}
			set
			{
				if ((this._AD_id != value))
				{
					this._AD_id = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Articles_Name", DbType="NVarChar(MAX)")]
		public string Articles_Name
		{
			get
			{
				return this._Articles_Name;
			}
			set
			{
				if ((this._Articles_Name != value))
				{
					this._Articles_Name = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Publication_Year", DbType="NVarChar(500)")]
		public string Publication_Year
		{
			get
			{
				return this._Publication_Year;
			}
			set
			{
				if ((this._Publication_Year != value))
				{
					this._Publication_Year = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Description", DbType="NText", UpdateCheck=UpdateCheck.Never)]
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				if ((this._Description != value))
				{
					this._Description = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Web_link", DbType="NVarChar(MAX)")]
		public string Web_link
		{
			get
			{
				return this._Web_link;
			}
			set
			{
				if ((this._Web_link != value))
				{
					this._Web_link = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Author", DbType="NVarChar(500)")]
		public string Author
		{
			get
			{
				return this._Author;
			}
			set
			{
				if ((this._Author != value))
				{
					this._Author = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Publication_Name", DbType="NVarChar(MAX)")]
		public string Publication_Name
		{
			get
			{
				return this._Publication_Name;
			}
			set
			{
				if ((this._Publication_Name != value))
				{
					this._Publication_Name = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ArticleType", DbType="NVarChar(MAX)")]
		public string ArticleType
		{
			get
			{
				return this._ArticleType;
			}
			set
			{
				if ((this._ArticleType != value))
				{
					this._ArticleType = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Publication_id", DbType="Int")]
		public System.Nullable<int> Publication_id
		{
			get
			{
				return this._Publication_id;
			}
			set
			{
				if ((this._Publication_id != value))
				{
					this._Publication_id = value;
				}
			}
		}
	}
	
	public partial class GetHeaderFooterResult
	{
		
		private long _Id;
		
		private string _HeaderDetails;
		
		private string _HeaderLogo;
		
		private string _FooterLogo;
		
		private string _FooterDetails;
		
		private bool _IsDelete;
		
		private string _CreateBy;
		
		private System.Nullable<System.DateTime> _CreateDate;
		
		private string _UpdateBy;
		
		private System.Nullable<System.DateTime> _UpdateDate;
		
		private string _DeleteBy;
		
		private System.Nullable<System.DateTime> _DeleteDate;
		
		public GetHeaderFooterResult()
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_HeaderDetails", DbType="NVarChar(MAX)")]
		public string HeaderDetails
		{
			get
			{
				return this._HeaderDetails;
			}
			set
			{
				if ((this._HeaderDetails != value))
				{
					this._HeaderDetails = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_HeaderLogo", DbType="NVarChar(MAX)")]
		public string HeaderLogo
		{
			get
			{
				return this._HeaderLogo;
			}
			set
			{
				if ((this._HeaderLogo != value))
				{
					this._HeaderLogo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FooterLogo", DbType="NVarChar(MAX)")]
		public string FooterLogo
		{
			get
			{
				return this._FooterLogo;
			}
			set
			{
				if ((this._FooterLogo != value))
				{
					this._FooterLogo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FooterDetails", DbType="NVarChar(MAX)")]
		public string FooterDetails
		{
			get
			{
				return this._FooterDetails;
			}
			set
			{
				if ((this._FooterDetails != value))
				{
					this._FooterDetails = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsDelete", DbType="Bit NOT NULL")]
		public bool IsDelete
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CreateBy", DbType="NVarChar(100)")]
		public string CreateBy
		{
			get
			{
				return this._CreateBy;
			}
			set
			{
				if ((this._CreateBy != value))
				{
					this._CreateBy = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CreateDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> CreateDate
		{
			get
			{
				return this._CreateDate;
			}
			set
			{
				if ((this._CreateDate != value))
				{
					this._CreateDate = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UpdateBy", DbType="NVarChar(100)")]
		public string UpdateBy
		{
			get
			{
				return this._UpdateBy;
			}
			set
			{
				if ((this._UpdateBy != value))
				{
					this._UpdateBy = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UpdateDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> UpdateDate
		{
			get
			{
				return this._UpdateDate;
			}
			set
			{
				if ((this._UpdateDate != value))
				{
					this._UpdateDate = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DeleteBy", DbType="NVarChar(100)")]
		public string DeleteBy
		{
			get
			{
				return this._DeleteBy;
			}
			set
			{
				if ((this._DeleteBy != value))
				{
					this._DeleteBy = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DeleteDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> DeleteDate
		{
			get
			{
				return this._DeleteDate;
			}
			set
			{
				if ((this._DeleteDate != value))
				{
					this._DeleteDate = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
