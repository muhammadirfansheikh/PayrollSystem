using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_Menu : System.Web.UI.UserControl
{
    public System.Text.StringBuilder menu = new System.Text.StringBuilder();
    List<RoleAccess_Select_Result> menuList;
    public string IsAdmin, CanAddUpdate, CanView;

    protected void Page_Init(object sender, EventArgs e)
    {
        // check if the current user is logged in , and has permissions.
        AuthenticateUser();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        {
            int? role = int.Parse(new Base().RoleCode);
            int ApplicationId = (int)Constant.Application.HRMS;
            Base Objbase = new Base();
            int UserCode = Objbase.UserId;
            Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities();
            menuList = context.RoleAccess_Select(role, ApplicationId).Where(r => r.Is_Active == true && r.Is_Displayed_In_Menu == true).Distinct()
                .ToList();
            MakeMenuHTML();
        }
    }
    private void AuthenticateUser()
    {
        if (new Base().UserKey == 0)
        {
            Response.Redirect("~/Login.aspx", true);
        }


        //// check if the user is logged in, and if Yes then set the access functions on the page according to database, for the javascript to work
        //if (new Base().UserKey == 0 || string.IsNullOrEmpty(new Base().RoleCode) == true)
        //    Response.Redirect("~/Login.aspx?returnurl=" + Request.Url.PathAndQuery, true);
        //else if (CheckPageAccess() == false)
        //{
        //    Response.Write("UnAuthorized Access");
        //    Response.End();
        //    Response.Flush();
        //    HttpContext.Current.Response.SuppressContent = true;
        //    HttpContext.Current.ApplicationInstance.CompleteRequest();
        //    Response.Redirect("~/ErrorPage.aspx", true);
        //    /*New Code*/
        //    HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
        //    HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
        //    HttpContext.Current.ApplicationInstance.CompleteRequest();

        //}
        //else
        //{
        //    int roleCode = int.Parse(new Base().RoleCode);
        //    string pageUrl = this.Request.Url.LocalPath;
        //    Setup_RoleAccessFunction accessFunction = new Sybrid_DatabaseEntities().Setup_RoleAccessFunction.OrderByDescending(f => f.Menu_Item_Code).FirstOrDefault(r => r.Role_Code == roleCode && (r.Menu_Item_Code == null || r.Setup_MenuItem.Menu_URL == pageUrl));
        //    if (accessFunction == null)
        //    {
        //        IsAdmin = CanAddUpdate = "false";
        //        CanView = "true";
        //    }
        //    else
        //    {
        //        //IsAdmin = accessFunction.Access_Function_Code == (int)Constant.enumAccessFunction.Admin ? "true" : "false";
        //        //CanAddUpdate = accessFunction.Access_Function_Code == (int)Constant.enumAccessFunction.AddUpdate ? "true" : "false";
        //        //CanView = accessFunction.Access_Function_Code == (int)Constant.enumAccessFunction.View ? "true" : "false";
        //    }
        //}
    }
    private bool CheckPageAccess()
    {
        string url = "";
        //if (new Base().AccountId == 0)
        //{
        url = Request.Url.PathAndQuery;
        //}
        //else
        //{
        //    url = "/Pages/InitiatedTicketAccount.aspx";
        //}

        int roleCode = int.Parse(new Base().RoleCode);

        Setup_RoleAccess roleAccess = new Sybrid_DatabaseEntities().Setup_RoleAccess.FirstOrDefault(r => r.Role_Code == roleCode && r.Is_Active == true && url.Contains(r.Setup_MenuItem.Menu_URL) == true);
        if (roleAccess == null || roleAccess.Has_Access == false)
        {
            return false;
        }
        else
        {
            return true;
        }

    }
    private void MakeMenuHTML()
    {
        // write html for the menu <a href="#"><i class="fa fa-desktop"></i><span class="nav-label">App Views</span>  <span class="pull-right label label-primary">SPECIAL</span></a>
        //menu.Append(@" <li><ul class='nav nav-second-level collapse' >");
        //menu.Append(@" <li>");
        List<RoleAccess_Select_Result> menuTop = menuList.Where(m => m.Parent_Menu_Item_Code.ToString() == string.Empty || m.Parent_Menu_Item_Code == 0).ToList().ToList();
        for (int i = 0; i < menuTop.Count; i++)
        {
            menu.Append(@" <li>");
            if ((menuTop[i].Menu_URL == "#" || menuTop[i].Menu_URL == string.Empty) || menuTop[i].Has_Access == true)
            {
                string url = menuTop[i].Menu_URL == string.Empty ? "#" : menuTop[i].Menu_URL;
                menu.Append(@" <a href='" + url + "'><i class='fa fa-bars' style='font-size: 20px;'></i><span class='nav-label'>" + menuTop[i].Menu_Item_Name + "</span></a>");
                List<RoleAccess_Select_Result> menuChild = menuList.Where(m => m.Parent_Menu_Item_Code == menuTop[i].Menu_Item_Code).ToList();
                if (menuChild.Count > 0)
                    MakeSubMenuHTML(menuChild);
                //menu.Append(@"</li>");
                menu.Append(@"</li>");
            }
        }
        //menu.Append(@"</li>");

    }
    private void MakeSubMenuHTML(List<RoleAccess_Select_Result> menuChild)
    {
        menu.Append(@"<ul class='nav nav-second-level collapse'>");
        for (int j = 0; j < menuChild.Count; j++)
        {
            if ((menuChild[j].Menu_URL == "#" || menuChild[j].Menu_URL == string.Empty) || menuChild[j].Has_Access == true)
            {
                string url = menuChild[j].Menu_URL == string.Empty ? "#" : menuChild[j].Menu_URL;
                menu.Append(@"<li><a href='" + url + "'>" + menuChild[j].Menu_Item_Name + "</a></li>");

                List<RoleAccess_Select_Result> moreMenuChild = menuList.Where(m => m.Parent_Menu_Item_Code == menuChild[j].Menu_Item_Code).ToList();
                if (moreMenuChild.Count > 0)
                    MakeSubMenuHTML(moreMenuChild);
                //menu.Append(@"</li>");
            }
        }
        menu.Append(@"</ul>  ");
    }

}