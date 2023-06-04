<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PagingAndSorting.ascx.cs"
    Inherits="ERP.Website.controls.PagingAndSorting" %>
<table cellpadding="0" cellspacing="0" runat="server" id="tblPaging" class="table">
    <tr>
        <td nowrap="nowrap" ><%--&nbsp;&nbsp;--%><asp:Label ID="lblPageSize" runat="server" Text="Page Size:" Font-Bold="true"></asp:Label><%--&nbsp;--%>
            <asp:DropDownList CssClass="dropDown" ID="ddlPageSize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged"></asp:DropDownList><%--&nbsp;&nbsp;&nbsp;&nbsp;--%>
        </td>
        <td >
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td style="vertical-align: top">
                        <asp:ImageButton ID="imgPrevious" runat="server" OnClick="imgPrevious_Click"
                            ImageUrl="~/images/previous_btn.jpg" ToolTip="Previous" Width="17px" Height="19px" /><%--&nbsp;--%>
                    </td>
                    <td style="vertical-align: top">
                        <asp:DropDownList CssClass="dropDown" ID="ddlPage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPage_SelectedIndexChanged"></asp:DropDownList><%--&nbsp;--%>
                    </td>
                    <td style="vertical-align: top">
                        <asp:ImageButton ID="imgNext" runat="server" OnClick="imgNext_Click" ImageUrl="~/images/next_btn.jpg" ToolTip="Next" Width="17px" Height="19px" />
                    </td>
                </tr>
            </table>
        </td>
        <td></td>
        <td nowrap="nowrap" ><%--&nbsp;&nbsp;&nbsp;&nbsp;--%><asp:Label ID="lblRecordCountText" runat="server" Text="Total Records : "></asp:Label>
            <asp:Label ID="lblRecordCount" CssClass="clsRecordCount" runat="server" Font-Bold="true"></asp:Label><%--&nbsp;&nbsp;--%>
        </td>
    </tr>
</table>
