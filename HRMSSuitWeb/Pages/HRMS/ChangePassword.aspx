<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="Pages_ChangePassword" %>

<%--<%@ Register Src="~/Control/Shared/MessageCtrl.ascx" TagPrefix="uc1" TagName="MessageCtrl" %>--%> 
<%--<%@ Register Src="~/Controls/Shared/MessageCtrl.ascx" TagPrefix="uc2" TagName="MessageCtrl" %>--%> 

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

   <%-- <uc2:MessageCtrl runat="server" ID="MessageCtrl" />--%>

    <%--<uc1:MessageCtrl runat="server" id="MessageCtrl" />--%>


    <div id="RightContentError" runat="server" visible="false">
        <asp:Label ID="LB_Error" runat="server" />
    </div>
    <asp:ValidationSummary ID="validationSummary" runat="server" EnableClientScript="true"
        Enabled="true" ValidationGroup="ChangeUserPasswordValidationGroup" DisplayMode="BulletList"
        ShowSummary="true" HeaderText="Required Fields" CssClass='validationSummary' />

 
        <div class="panel panel-info">
            <div class="panel-heading">
                <h3>Change Password</h3>
            </div>
            <div class="panel-body">

    

    <div class="col-lg-6 col-lg-offset-2">
       <div class="col-lg-12">
           <label class="control-label col-lg-4">
               Old Password
           </label>
           <div class="col-lg-8">
                <asp:TextBox ID="CurrentPassword" runat="server"  CssClass="text_box form-control" TextMode="Password" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" ControlToValidate="CurrentPassword"
                        CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Old Password is required."
                        ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
           </div>
       </div>
        <div class="col-lg-12">
            <label class="control-label col-lg-4">
                New Password
            </label>
            <div class="col-lg-8">
                <asp:TextBox ID="NewPassword" runat="server" CssClass="text_box form-control" TextMode="Password" MaxLength="16" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword"
                        CssClass="failureNotification" ErrorMessage="New Password is required." ToolTip="New Password is required."
                        ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="NewPassword"
                        ErrorMessage="New Password does not meet complexity requirements" Display="Dynamic" CssClass="failureNotification"
                        ValidationExpression="^(?=.*[^a-zA-Z])(.{8,16})" Text="*"
                        ValidationGroup="ChangeUserPasswordValidationGroup"></asp:RegularExpressionValidator>

            </div>
        </div>
        <div class="col-lg-12">
            <label class="control-label col-lg-4">
                Confirm New Password
            </label>
            <div class="col-lg-8">
                    <asp:TextBox ID="ConfirmNewPassword" runat="server" CssClass="text_box form-control" TextMode="Password" MaxLength="16"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword"
                        CssClass="failureNotification" Display="Dynamic" ErrorMessage="Confirm New Password is required."
                        ToolTip="Confirm New Password is required." ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword"
                        ControlToValidate="ConfirmNewPassword" CssClass="failureNotification" Display="Dynamic"
                        ErrorMessage="The Confirm New Password must match the New Password entry." ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:CompareValidator>
                    
                    
                    
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="ConfirmNewPassword"
                        ErrorMessage="Confirm Password does not meet complexity requirements" Display="Dynamic" CssClass="failureNotification"
                        ValidationExpression="^(?=.*[^a-zA-Z])(.{8,16})" Text="*"
                        ValidationGroup="ChangeUserPasswordValidationGroup"></asp:RegularExpressionValidator>
            </div>
        </div>
         <p style="color: red">
                       <strong>NOTE:</strong> Minimum size of password is 8 characters, it must include at least 1 Upper case letter,1 Lower case letter, 1 number, 1 special character ( ?=.*?[#?!@$%^&*-] )
                    </p>
        <div class="col-lg-12">
            <div class="pull-right">
                 <asp:Button ID="ChangePasswordPushButton" runat="server" CommandName="ChangePassword"
                        Text="Change Password" ValidationGroup="ChangeUserPasswordValidationGroup" CssClass="save_btn btn btn-success " OnClick="ChangePasswordPushButton_Click" />
            </div>
        </div>
    </div>


                        </div>
        </div>




    <script>
        function AlertBox(title, Message, type) {
            swal(title, Message, type);
        }
</script>
</asp:Content>


