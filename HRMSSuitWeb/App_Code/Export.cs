using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;

/// <summary>
/// Summary description for Export
/// </summary>
public class Export
{
	public Export()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static void ExportToCsv(DataTable dataTable, string fileName)
    {
        HttpResponse response = HttpContext.Current.Response;
        
        response.Clear();
        response.Buffer = true;
        response.AddHeader("content-disposition", "attachment;filename=" + fileName);
        response.Charset = "";
        response.ContentType = "application/text";

        //GridView1.AllowPaging = false;
        //GridView1.DataBind();

        StringBuilder sb = new StringBuilder();
        for (int k = 0; k < dataTable.Columns.Count; k++)
        {
            //add separator header
            sb.Append(dataTable.Columns[k].ColumnName + ',');
        }
        //append new line
        sb.Append("\r\n");

        foreach (DataRow dr in dataTable.Rows)
        {
            foreach (DataColumn dc in dataTable.Columns)
            {
                //add separator
                sb.Append(dr[dc].ToString() + ',');
            }
            //append new line
            sb.Append("\r\n");
        }
        response.Output.Write(sb.ToString());
        response.Flush();
        response.End();
    }
}