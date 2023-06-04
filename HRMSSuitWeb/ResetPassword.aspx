<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BlankMaster.master" AutoEventWireup="true" CodeFile="ResetPassword.aspx.cs" Inherits="ResetPassword" %>

<%@ Register Src="~/Controls/Shared/InProgress.ascx" TagPrefix="uc" TagName="InProgress" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

     <div class="container-fluid" id="Div_Main" runat="server" visible="false">
        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
            <ProgressTemplate>
                <uc:InProgress runat="server" ID="InProgress" />
            </ProgressTemplate>
        </asp:UpdateProgress>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <input type="hidden" id="hfAppURL" runat="server" class="hfAppURL" value="" />
                <input type="hidden" id="hfToken" runat="server" class="hfToken" value="" />
                <div class="row">
                    <div class="panel panel-default" style="padding: 20px 20px 20px 20px;">
                        <div class="panel-heading">
                            <h3>Reset Password</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <p style="color: red">
                                            <strong>NOTE:</strong> Minimum size of password is 8 characters, it must include at least 1 Upper case letter,1 Lower case letter, 1 number, 1 special character ( ?=.*?[#?!@$%^&*-] )
                                        </p>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label class="form-label">
                                            New Password
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" Text="*" ErrorMessage="*" ForeColor="Red" ControlToValidate="NewPassword" ValidationGroup="ChangePassword"></asp:RequiredFieldValidator>
                                        </label>
                                        <div class="forpass1_c">
                                            <asp:TextBox runat="server" ID="NewPassword" onkeyup="checkStrength(this)" PlaceHolder="New Password" TextMode="Password" AutoCompleteType="Disabled" oncopy="return false" onpaste="return false" oncut="return false" ondelete="return false" MaxLength="128" CssClass="form-control TextBoxValidate NewPassword"></asp:TextBox>
                                            <span toggle="NewPassword" class="fa fa-eye pull-right" onclick="togglepassword(this)" style="margin-top: -25px; margin-right: 5px;"></span>
                                            <div id="popover-password">
                                                <p><span id="result"></span></p>
                                                <div class="progress" style="height: 5px;">
                                                    <div id="password-strength"
                                                        class="progress-bar"
                                                        role="progressbar"
                                                        aria-valuenow="40"
                                                        aria-valuemin="0"
                                                        aria-valuemax="100"
                                                        style="width: 0%">
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="NewPassword" ErrorMessage="New Password does not meet complexity requirements." ForeColor="Red" Display="Dynamic" CssClass="failureNotification" ValidationExpression="^(?!.*[ ]{2})(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{10,128}$" ValidationGroup="ChangePassword"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-4">
                                    <label class="form-label">
                                        Confirm New Password
                                      <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" Text="*" ErrorMessage="*" ForeColor="Red" ControlToValidate="ConfirmNewPassword" ValidationGroup="ChangePassword"></asp:RequiredFieldValidator>
                                    </label>
                                    <div class="forpass1_c">
                                        <asp:TextBox runat="server" ID="ConfirmNewPassword" PlaceHolder="Confirm New Password" TextMode="Password" MaxLength="128" AutoCompleteType="Disabled" CssClass="form-control TextBoxValidate ConfirmNewPassword" oncopy="return false" onpaste="return false" oncut="return false" ondelete="return false"></asp:TextBox>
                                        <span toggle="ConfirmNewPassword" class="fa fa-eye pull-right" onclick="togglepassword(this)" style="margin-top: -25px; margin-right: 5px;"></span>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="ConfirmNewPassword" ForeColor="Red" ErrorMessage="Confirm Password does not meet complexity requirements" Display="Dynamic" CssClass="failureNotification" ValidationExpression="^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{10,128}$" ValidationGroup="ChangePassword"></asp:RegularExpressionValidator>
                                        <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword" CssClass="failureNotification" Display="Dynamic" ForeColor="Red" ErrorMessage="The Confirm New Password must match the New Password entry." ValidationGroup="ChangePassword"></asp:CompareValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="row text-right" style="margin-top: 15px;">
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <asp:Button runat="server" ID="btn_ChangePassword" OnClick="btn_ChangePassword_Click" Text="Reset Password" class="btn btn-danger btn_Search" ValidationGroup="ChangePassword"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row btn-danger" id="divError_" runat="server" visible="false">
                        <div id="lblError_" runat="server"></div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <script type="text/javascript">

        $(document).ready(function () {
            ControlsValidateFunction();
        });

        $('.TextBoxValidate').on('keypress', function (e) {
            if (e.which == 32) {
                //console.log('Space Detected');
                return false;
            }
        });

        $('.TextBoxValidate').on('keyup', function (e) {
            $(".TextBoxValidate").each(function () {
                if ($(this).val().trim() == "") {
                    $(this).css("border-color", "Red");
                }
                else {
                    $(this).css("border-color", "#d4d4d4");
                }
            });
        });

        function AlertBox(title, Message, type) {
            swal(title, Message, type);
        }

        function ControlsValidateFunction() {
            var State = true;
            $(".DropDownValidate").each(function () {
                if ($(this).val() == "0" || $(this).val() == "") {
                    $(this).css("border-color", "Red");
                    State = false;

                }
                else {
                    $(this).css("border-color", "#d4d4d4");
                }
            });

            $(".TextBoxValidate").each(function () {
                if ($(this).val().trim() == "") {
                    $(this).css("border-color", "Red");
                    State = false;
                }
                else {
                    $(this).css("border-color", "#d4d4d4");
                }
            });
            return State;
        }

        function AlertBoxRedirect(title, Message, type, Page) {

            swal({
                html: true,
                title: title,
                text: Message,
                type: "success",
                confirmButtonText: "OK",
                confirmButtonColor: "#DD6B55",
            },
                 function () {
                     var page = $('.hfAppURL').val();
                     window.location.href = page;
                 });
        }

        function AlertBoxRedirect_Error(title, Message, type, Page) {

            swal({
                html: true,
                title: title,
                text: Message,
                type: "error",
            },
                 function () {
                     window.location.href = Page;
                 });
        }

    </script>

</asp:Content>

