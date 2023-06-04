using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_Shared_MultipleListBox : System.Web.UI.UserControl
{
    #region Properties

    public string SourceHeadingText
    {
        set { lblSource.Text = value; }
    }
    public string DestinationHeadingText
    {
        set { lblDestination.Text = value; }
    }

    public string DataValueField
    {
        set;
        get;
    }
    public string DataTextField
    {
        set;
        get;
    }

    private DataTable _SourceDatasource;
    public DataTable SourceDatasource
    {
        set
        {
            _SourceDatasource = value;
            SetSource();
        }
        get { return (DataTable)ListBoxSource.DataSource; }
    }

    private DataTable _DestinationDatasource;
    public DataTable DestinationDatasource
    {
        set
        {
            _DestinationDatasource = value;
            SetDestination();
        }
        get
        {
            return GetDestinationDatatable();
        }
    }

    #endregion



    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            SetSource();
            SetDestination();
        }
    }
    private void SetSource()
    {


        if (_SourceDatasource != null)
        {
            ListBoxSource.DataSource = _SourceDatasource;
            ListBoxSource.DataValueField = this.DataValueField;
            ListBoxSource.DataTextField = this.DataTextField;
            ListBoxSource.DataBind();
        }
        else
        {
            ListBoxSource.Items.Clear();
            ListBoxSource.DataSource = null;
            ListBoxSource.DataBind();
        }
    }
    private void SetDestination()
    {


        if (_DestinationDatasource != null)
        {
            ListBoxDestination.DataSource = _DestinationDatasource;
            ListBoxDestination.DataValueField = this.DataValueField;
            ListBoxDestination.DataTextField = this.DataTextField;
            ListBoxDestination.DataBind();
        }
        else
        {
            ListBoxDestination.Items.Clear();
            ListBoxDestination.DataSource = null;
            ListBoxDestination.DataBind();
        }
    }

    protected void btnMoveRight_Click(object sender, System.EventArgs e)
    {
        for (int i = ListBoxSource.Items.Count - 1; i >= 0; i--)
        {
            if (ListBoxSource.Items[i].Selected == true)
            {
                ListBoxDestination.Items.Add(ListBoxSource.Items[i]);
                ListItem li = ListBoxSource.Items[i];
                ListBoxSource.Items.Remove(li);
            }
        }
    }

    protected void btnMoveLeft_Click(object sender, System.EventArgs e)
    {
        for (int i = ListBoxDestination.Items.Count - 1; i >= 0; i--)
        {
            if (ListBoxDestination.Items[i].Selected == true)
            {
                ListBoxSource.Items.Add(ListBoxDestination.Items[i]);
                ListItem li = ListBoxDestination.Items[i];
                ListBoxDestination.Items.Remove(li);
            }
        }
    }

    private DataTable GetDestinationDatatable()
    {
        DataTable DT = new DataTable();
        DT.Columns.Add(DataValueField);
        DT.Columns.Add(DataTextField);

        foreach (ListItem li in ListBoxDestination.Items)
        {
            DT.Rows.Add(li.Value, li.Text);
        }

        return DT;

    }

}