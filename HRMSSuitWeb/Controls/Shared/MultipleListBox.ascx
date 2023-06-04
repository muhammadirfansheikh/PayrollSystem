<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MultipleListBox.ascx.cs" Inherits="Controls_Shared_MultipleListBox" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table class="MultipleSelectionBox">
            <tr>
                <td>
                    <asp:Label Text="" ID="lblSource" runat="server"> </asp:Label>
                </td>
                <td></td>
                <td>
                    <asp:Label Text="" ID="lblDestination" runat="server"> </asp:Label>
                    <span class="mendatory">*</span>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:ListBox runat="server" ID="ListBoxSource" Style="width: 200px;" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox></td>
                <td style="padding: 10px;">
                    <asp:Button runat="server" ID="btnMoveRight" CssClass="button btn btn-info" Text="  Move >>" OnClick="btnMoveRight_Click"></asp:Button>
                    <asp:Button runat="server" ID="btnMoveLeft" CssClass="button btn btn-info" Text="  << Move" OnClick="btnMoveLeft_Click"></asp:Button></td>
                <td>
                    <asp:ListBox runat="server" ID="ListBoxDestination" Style="width: 200px;" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox></td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
