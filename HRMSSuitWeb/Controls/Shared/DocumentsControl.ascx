<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DocumentsControl.ascx.cs" Inherits="Controls_Shared_DocumentsControl" %>

<%@ Register Src="~/Controls/Shared/InProgress.ascx" TagName="InProgress" TagPrefix="uc2" %>
<script type="text/javascript" language="javascript">
    function ConfirmOnDelete() {
        if (confirm("Are you sure?") == true)
            return true;
        else
            return false;
    }
</script>
<div class="panel panel-info alert alert-success">
    <div class="panel-heading">
        Attachments
    </div>
    <div class="panel-body">
        <div class="alert alert-warning">
            <strong>You can attach reports / other documents through this upload documents sections...</strong>
        </div>
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnUpload1" />
                <asp:PostBackTrigger ControlID="grdDocumentsNEW" />
            </Triggers>
            <ContentTemplate>
                <table align="center" style="width: 100%">
                    <tr runat="server" id="trHeader">
                        <th class="">
                            <asp:Label EnableTheming="false" Text="" runat="server" ID="lblHeader" />
                            <asp:HiddenField ID="hdnFileCount" runat="server" Value="0" />
                        </th>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%" runat="server" id="tblUploadControls">
                                <tr>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtComments1" ForeColor="Red"
                                            Display="Dynamic" ErrorMessage="RequiredFieldValidator" ValidationGroup="UploadNew">*</asp:RequiredFieldValidator>
                                        <label>Comments</label>
                                    </td>
                                    <td>

                                        <asp:TextBox ID="txtComments1" runat="server" CssClass="text_box"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FileUpload11" ForeColor="Red"
                                            Display="Dynamic" ErrorMessage="RequiredFieldValidator" ValidationGroup="UploadNew">* Please select a file:</asp:RequiredFieldValidator>
                                        <asp:FileUpload ID="FileUpload11" runat="server" Width="200px" />
                                    </td>

                                    <td>
                                        <asp:Button ID="btnUpload1" runat="server" ToolTip="Attach file" Font-Size="XX-Small" Text="Upload" EnableTheming="false" CssClass="upload_btn"
                                            OnClick="btnUpload1_Click" ValidationGroup="UploadNew"
                                            OnClientClick=" if (Page_ClientValidate('UploadNew')) $('divInProgress').show();" />
                                        * Maximum File Size: 30 MB (30,720 KB)
                                    </td>
                                </tr>

                            </table>
                            <br />
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>


                                    <asp:GridView ID="grdDocumentsNEW" runat="server" Width="100%" AutoGenerateColumns="False" CssClass="grid"
                                        BorderColor="#D4E1ED" BorderWidth="1px" OnDataBound="grdDocumentsNEW_DataBound"
                                        EnableModelValidation="True" OnSelectedIndexChanged="grdDocumentsNEW_SelectedIndexChanged"
                                        DataKeyNames="FullPath,DocumentId" OnRowCommand="grdDocumentsNEW_RowCommand" OnRowDeleting="grdDocumentsNEW_RowDeleting"
                                        OnRowDataBound="grdDocumentsNEW_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="OriginalFileName" HeaderStyle-Width="15%" HeaderText="File Name" />
                                            <asp:BoundField DataField="FileType" HeaderText="Type" HeaderStyle-Width="20%" />
                                            <asp:BoundField DataField="Comments" HeaderText="Comments" HeaderStyle-Width="20%" />

                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="15%">
                                                <ItemTemplate>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="20%">
                                                <ItemTemplate>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Download" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                                <ItemTemplate>


                                                    <asp:ImageButton ID="DownloadButton" runat="server" ImageUrl="~/images/downld.gif"
                                                        CommandName="Download"
                                                        AlternateText="Download" CausesValidation="false" />

                                                    <asp:HiddenField ID="ValueHiddenField" Value='<%# Eval("FullPath") %>' runat="server" />
                                                    <asp:HiddenField ID="hftargetID" Value='<%# Eval("CreatedBy") %>' runat="server" />


                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                                <ItemTemplate>
                                                    <a target="_blank" onclick="var originalTarget = document.forms[0].target; document.forms[0].target = '_blank'; setTimeout(function () { document.forms[0].target = originalTarget; }, 3000);" href='<%# Eval("FullPath") %>'>
                                                        <img alt="Download" src="/Images/book-open-icon.png" />
                                                    </a>

                                                </ItemTemplate>
                                            </asp:TemplateField>



                                            <asp:TemplateField ShowHeader="True" HeaderText="Delete" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="DeleteButton" runat="server" ImageUrl="~/images/deletefile.gif"
                                                        CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete the file ?');"
                                                        AlternateText="Delete" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="Hidden" />
                                        <HeaderStyle BackColor="#dce4ee" Font-Bold="True" Font-Italic="False" Font-Names="Tahoma"
                                            Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False"
                                            HorizontalAlign="Center" ForeColor="#000000" />
                                    </asp:GridView>

                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </td>
                    </tr>

                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="divInProgress" style="display: none;">
            <uc2:InProgress ID="InProgress1" runat="server" />
        </div>
    </div>
</div>
