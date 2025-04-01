using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Unmehta.WebPortal.Web
{
    public class GridViewTemplate : ITemplate
    {
        private string columnNameBinding;

        public GridViewTemplate(string colname, string colNameBinding)
        {
            columnNameBinding = colNameBinding;
        }

        public void InstantiateIn(System.Web.UI.Control container)
        {
            TextBox tb = new TextBox();
            tb.ID = "txtDynamic" + columnNameBinding;
            container.Controls.Add(tb);
        }
    }

    public partial class DynamicTable : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void GenerateGridView(object sender, EventArgs e)
        {
            gvData.Columns.Clear();
            DataTable dt = new DataTable();
            int cols = Convert.ToInt32(txtColumns.Text.Trim());
            int rows = Convert.ToInt32(txtRows.Text.Trim());
            for (int i = 0; i < cols; i++)
            {
                TemplateField field = new TemplateField();
                field.HeaderText = "Column" + i.ToString();
                field.ItemTemplate = new GridViewTemplate("Column" + i.ToString(), i.ToString());
                gvData.Columns.Add(field);
            }

            for (int i = 0; i < rows; i++)
            {
                dt.Rows.Add();
            }
            gvData.DataSource = dt;
            gvData.DataBind();
        }

        protected void btnGetData_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            // add the columns to the datatable            
            if (gvData.HeaderRow != null)
            {
                for (int i = 0; i < gvData.HeaderRow.Cells.Count; i++)
                {
                    dt.Columns.Add(gvData.HeaderRow.Cells[i].Text);
                }
            }

            int j = 2;
            //  add each of the data rows to the table
            foreach (GridViewRow row in gvData.Rows)
            {
                DataRow dr;
                dr = dt.NewRow();

                for (int i = 0; i < gvData.HeaderRow.Cells.Count; i++)
                {
                    //TextBox orignalAmount = (TextBox)row.Cells[i].FindControl("txtDynamic" + i.ToString()+"_"+ j.ToString());
                    string strColValue = "ctl00$" + gvData.ClientID.Replace("_","$") + "$ctl0"+ j.ToString() + "$txtDynamic" + i.ToString();
                    string strValue = Request[strColValue].ToString();
                    dr[i] = strValue;
                }
                dt.Rows.Add(dr);
                j++;
            }

            //  add the footer row to the table
            if (gvData.FooterRow != null)
            {
                DataRow dr;
                dr = dt.NewRow();

                for (int i = 0; i < gvData.FooterRow.Cells.Count; i++)
                {
                    dr[i] = gvData.FooterRow.Cells[i].Text.Replace("&nbsp;", "");
                }
                dt.Rows.Add(dr);
            }

            
        }
    }
}