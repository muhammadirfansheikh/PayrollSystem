using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

namespace ERP.Website.controls
{
    public partial class PagingAndSorting : System.Web.UI.UserControl
    {
        public ArrayList pageSizes = new ArrayList();
        public string selectedValue;
        public int currentPage
        {
            //set;
            //get;
            set;
            get;
        }
        public PagingAndSorting()
        {
            this.Visible = false;
        }

        public ImageButton ImgNext { set { imgNext = value; } get { return imgNext; } }
        public ImageButton ImgPrevious { set { imgPrevious = value; } get { return imgPrevious; } }
        public DropDownList DdlPage { set { ddlPage = value; } get { return ddlPage; } }
        public DropDownList DdlPageSize { set { ddlPageSize = value; } get { return ddlPageSize; } }
        public Label LblRecordCountText { set { lblRecordCountText = value; } get { return lblRecordCountText; } }
        public Label LblRecordCount { set { lblRecordCount = value; } get { return lblRecordCount; } }
        public Label LblPageSize { set { lblPageSize = value; } get { return lblPageSize; } }

        public void setPageSizeOptions(ArrayList pageSizes, string selectedValue)
        {

            ArrayList myPageSizes = new ArrayList();
            //myPageSizes.Add("5");
            myPageSizes.Add("10");
            myPageSizes.Add("25");
            myPageSizes.Add("50");
            myPageSizes.Add("100");
            myPageSizes.Add("250");
            //myPageSizes.Add("500");
            //myPageSizes.Add("1000");
            //myPageSizes.Add("2000");
            if (pageSizes != null)
            {
                if (pageSizes.Count == 0 || pageSizes == null)
                {
                    pageSizes = myPageSizes;
                }
            }
            else
            {
                pageSizes = myPageSizes;
            }
            ddlPageSize.DataSource = pageSizes;
            ddlPageSize.DataBind();
            if (pageSizes.Contains(selectedValue))
            {
                ddlPageSize.Items.FindByValue(selectedValue).Selected = true;
            }
            else
            {
                ddlPageSize.Items[2].Selected = true;
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // PAGING
                //String[] pageSizes = { "10", "20", "50", "100", "250", "1000", "2000", "All" };
                setPageSizeOptions(pageSizes, selectedValue);
                //ddlPageSize.Items.FindByText("All").Value = "0";

                // END PAGING
            }
        }
        public void imgNext_Click(object sender, ImageClickEventArgs e)
        {
            currentPage = ddlPage.SelectedValue.toInt();
            currentPage++;
            ddlPage.ClearSelection();
            ddlPage.Items.FindByValue(currentPage.ToString()).Selected = true;
        }





        public void imgPrevious_Click(object sender, ImageClickEventArgs e)
        {
            currentPage = ddlPage.SelectedValue.toInt();
            currentPage--;
            ddlPage.ClearSelection();
            ddlPage.Items.FindByValue(currentPage.ToString()).Selected = true;
        }


        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {

            //int pageSize = GetPageSize();
            //if (pageSize == int.MaxValue)
            //    currentPage = 1;
            //else
            currentPage = ddlPage.SelectedValue.toInt();


        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlPage_SelectedIndexChanged(sender, e);
            ddlPage.SelectedIndex = 0;
        }


        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            setPaginationLinks();

        }

        private void setPaginationLinks()
        {
            int totalPages = ddlPage.Items.Count;
            // Request["pid"].toInt();// ddlPage.Items.Count;
            if (totalPages < 2)
            {
                imgPrevious.Enabled = false;
                imgNext.Enabled = false;
                imgNext.ImageUrl = "~/images/Pagging/next_btn_ov.jpg";
                imgPrevious.ImageUrl = "~/images/Pagging/previous_btn_ov.jpg";
            }
            else
            {
                int currentPage = ddlPage.SelectedValue.toInt();
                //Request["cid"].toInt(); //dlPage.SelectedValue.toInt(); 

                if (totalPages == currentPage)
                {
                    imgNext.Enabled = false;
                    imgNext.ImageUrl = "~/images/Pagging/next_btn_ov.jpg";
                    imgPrevious.Enabled = true;
                    imgPrevious.ImageUrl = "~/images/Pagging/previous_btn.jpg";
                }
                else if (currentPage == 1)
                {
                    imgPrevious.Enabled = false;
                    imgPrevious.ImageUrl = "~/images/Pagging/previous_btn_ov.jpg";
                    imgNext.Enabled = true;
                    imgNext.ImageUrl = "~/images/Pagging/next_btn.jpg";
                }
                else
                {
                    imgPrevious.Enabled = true;
                    imgPrevious.ImageUrl = "~/images/Pagging/previous_btn.jpg";
                    imgNext.Enabled = true;
                    imgNext.ImageUrl = "~/images/Pagging/next_btn.jpg";
                }
            }
        }

        public void setPagingOptions(int recordCount)
        {
            if (string.IsNullOrEmpty(ddlPageSize.SelectedValue))
            {
                setPageSizeOptions(null, null);
            }
            int PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            int pageCount = (recordCount % PageSize) == 0 ? (recordCount / PageSize) : (recordCount / PageSize) + 1;
            currentPage = ddlPage.SelectedValue != null ? ddlPage.SelectedValue.toInt() : 0;

            ddlPage.Items.Clear();
            lblRecordCount.Text = recordCount.ToString();
            if (recordCount == 0)
            {
                this.Visible = false;
            }
            else
            {
                this.Visible = true;
                if (pageCount > 0)
                {
                    ddlPage.AddItemsOfRange(1, pageCount, "Page ", null);
                    if (currentPage == 0)
                    {
                        currentPage = 1;
                    }

                    if (pageCount < currentPage)
                    {
                        ddlPage.ClearSelection();
                        ddlPage.Items.FindByValue(pageCount.ToString()).Selected = true;
                    }
                    else
                    {
                        ddlPage.ClearSelection();
                        ddlPage.Items.FindByValue(currentPage.ToString()).Selected = true;
                    }
                }
            }
        }


    }
}