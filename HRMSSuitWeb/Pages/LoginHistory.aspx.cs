using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
public partial class Pages_LoginHistory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    private void BindRepeater()
    {
        using (Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities())
        {
            Base bs = new Base();
            int pageSize = 10;
            //int pageNumber = 1;
            //if (PagingAndSorting.DdlPageSize.SelectedValue.toInt() > 0)
            //{
            //    pageSize = PagingAndSorting.DdlPageSize.SelectedValue.toInt();
            //}
            //if (PagingAndSorting.DdlPage.Items.Count > 0)
            //{
            //    pageNumber = PagingAndSorting.DdlPage.SelectedValue.toInt();
            //}

            //int skip = pageNumber * pageSize - pageSize;
            var Lst = context.UserLoginHistories.Where(a => a.IsActive == true && a.UserId == bs.UserId)

               .Select(a => new
               {
                   ID = a.ID,
                   IsSuccess = a.IsSuccess,
                   CreatedDate = a.CreatedDate,
                   UserIP = a.UserIP,
               }).OrderByDescending(a => a.ID).Take(10).ToList();

            //var List = Lst.Skip(skip).Take(pageSize).ToList();
            rptLoginHistory.DataSource = Lst;
            rptLoginHistory.DataBind();
            //PagingAndSorting.setPagingOptions(Lst.Count());
        }
    }

    #region PAGING
    //private void PagingHandler()
    //{
    //    PagingAndSorting.ImgNext.Click += ImgNext_Click;
    //    PagingAndSorting.ImgPrevious.Click += ImgPrevious_Click;
    //    PagingAndSorting.DdlPage.SelectedIndexChanged += DdlPage_SelectedIndexChanged;
    //    PagingAndSorting.DdlPageSize.SelectedIndexChanged += DdlPageSize_SelectedIndexChanged;
    //}

    //void DdlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    BindRepeater();
    //}
    //void DdlPage_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    BindRepeater();
    //}
    //void ImgNext_Click(object sender, ImageClickEventArgs e)
    //{
    //    BindRepeater();
    //}
    //void ImgPrevious_Click(object sender, ImageClickEventArgs e)
    //{
    //    BindRepeater();
    //}
    #endregion
}