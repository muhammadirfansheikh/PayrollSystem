<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Pages_Login" %>

<%@ Register Src="~/Controls/Shared/InProgress.ascx" TagName="InProgress" TagPrefix="uc2" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>HCM</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <link href="/css/bootstrap.min.css" rel="stylesheet" />
    <link href="/font-awesome/css/font-awesome.css" rel="stylesheet" />
    <%--<link href="/css/example.css" rel="stylesheet" />--%>
    <link href="/css/plugins/iCheck/custom.css" rel="stylesheet" />
    <!-- Data picker -->
    <script src="/js/plugins/datapicker/bootstrap-datepicker.js"></script>
    <script src="/js/plugins/daterangepicker/daterangepicker.js"></script>

    <link href="/css/plugins/chosen/chosen.css" rel="stylesheet" />

    <link href="/css/plugins/colorpicker/bootstrap-colorpicker.min.css" rel="stylesheet" />

    <link href="/css/plugins/cropper/cropper.min.css" rel="stylesheet" />

    <link href="/css/plugins/switchery/switchery.css" rel="stylesheet" />

    <link href="/css/plugins/jasny/jasny-bootstrap.min.css" rel="stylesheet" />

    <link href="/css/plugins/nouslider/jquery.nouislider.css" rel="stylesheet" />

    <link href="/css/plugins/datapicker/datepicker3.css" rel="stylesheet" />

    <link href="/css/plugins/ionRangeSlider/ion.rangeSlider.css" rel="stylesheet" />
    <link href="/css/plugins/ionRangeSlider/ion.rangeSlider.skinFlat.css" rel="stylesheet" />

    <link href="/css/plugins/awesome-bootstrap-checkbox/awesome-bootstrap-checkbox.css" rel="stylesheet" />

    <link href="/css/plugins/clockpicker/clockpicker.css" rel="stylesheet" />

    <link href="/css/plugins/daterangepicker/daterangepicker-bs3.css" rel="stylesheet" />

    <link href="/css/plugins/select2/select2.min.css" rel="stylesheet" />

    <link href="/css/plugins/touchspin/jquery.bootstrap-touchspin.min.css" rel="stylesheet" />

    <link href="/css/animate.css" rel="stylesheet" />
    <link href="/css/style.css" rel="stylesheet" />
    <link href="/css/bootstrap.min.css" rel="stylesheet" />
    <link href="/font-awesome/css/font-awesome.css" rel="stylesheet" />
    <%-- CK Editor Start --%>
    <%--<script src="/js/ckeditor.js"></script>
	<script src="/js/sample.js"></script>
	<link rel="stylesheet" href="/css/samples.css"/>
	<link rel="stylesheet" href="/toolbarconfigurator/lib/codemirror/neo.css"/>--%>
    <%-- CK Editor End --%>
    <link href="/css/animate.css" rel="stylesheet" />
    <link href="/css/style.css" rel="stylesheet" />


    <!--Sweet Alert-->
    <link href="/css/plugins/sweetalert/sweetalert.css" rel="stylesheet" />
    <script src="/js/plugins/sweetalert/sweetalert.min.js"></script>

    <!--End Sweet Alert-->

    <!-- Mainly scripts -->
    <script src="/js/jquery-2.1.1.js"></script>
    <script src="/js/bootstrap.min.js"></script>
    <script src="/js/plugins/metisMenu/jquery.metisMenu.js"></script>
    <%--<script src="/js/plugins/slimscroll/jquery.slimscroll.min.js"></script>--%>
    <!-- Flot -->
    <script src="/js/plugins/flot/jquery.flot.js"></script>
    <script src="/js/plugins/flot/jquery.flot.tooltip.min.js"></script>
    <script src="/js/plugins/flot/jquery.flot.spline.js"></script>
    <script src="/js/plugins/flot/jquery.flot.resize.js"></script>
    <script src="/js/plugins/flot/jquery.flot.pie.js"></script>
    <script src="/js/plugins/flot/jquery.flot.symbol.js"></script>
    <script src="/js/plugins/flot/jquery.flot.time.js"></script>

    <!-- Custom and plugin javascript -->
    <script src="/js/plugins/pace/pace.min.js"></script>

    <!-- Sparkline -->
    <script src="/js/plugins/sparkline/jquery.sparkline.min.js"></script>

    <!-- Mainly scripts -->
    <script src="/js/jquery-2.1.1.js"></script>

    <!-- Custom and plugin javascript -->
    <script src="/js/inspinia.js"></script>
    <script src="/js/plugins/pace/pace.min.js"></script>
    <script src="/js/plugins/slimscroll/jquery.slimscroll.min.js"></script>

    <!-- Chosen -->
    <script src="/js/plugins/chosen/chosen.jquery.js"></script>

    <!-- JSKnob -->
    <script src="/js/plugins/jsKnob/jquery.knob.js"></script>

    <!-- Input Mask-->
    <script src="/js/plugins/jasny/jasny-bootstrap.min.js"></script>

    <!-- Data picker -->
    <script src="/js/plugins/datapicker/bootstrap-datepicker.js"></script>

    <!-- NouSlider -->
    <script src="/js/plugins/nouslider/jquery.nouislider.min.js"></script>

    <!-- Switchery -->
    <script src="/js/plugins/switchery/switchery.js"></script>

    <!-- IonRangeSlider -->
    <script src="/js/plugins/ionRangeSlider/ion.rangeSlider.min.js"></script>

    <!-- iCheck -->
    <script src="/js/plugins/iCheck/icheck.min.js"></script>

    <!-- MENU -->
    <script src="/js/plugins/metisMenu/jquery.metisMenu.js"></script>

    <!-- Color picker -->
    <script src="/js/plugins/colorpicker/bootstrap-colorpicker.min.js"></script>

    <!-- Clock picker -->
    <script src="/js/plugins/clockpicker/clockpicker.js"></script>

    <!-- Image cropper -->
    <script src="/js/plugins/cropper/cropper.min.js"></script>

    <!-- Date range use moment.js same as full calendar plugin -->
    <script src="/js/plugins/fullcalendar/moment.min.js"></script>

    <!-- Date range picker -->
    <script src="/js/plugins/daterangepicker/daterangepicker.js"></script>

    <!-- Select2 -->
    <script src="/js/plugins/select2/select2.full.min.js"></script>

    <!-- TouchSpin -->
    <script src="/js/plugins/touchspin/jquery.bootstrap-touchspin.min.js"></script>
    <script src="/js/inspinia.js"></script>
    <script src="/js/jquerynumeric.js"></script>
    <link href="/css/custom.css" rel="stylesheet" />
</head>
<body style="background: #2C3E50; /* fallback for old browsers */
background: -webkit-linear-gradient(to left, #2C3E50 , #4CA1AF); /* Chrome 10-25, Safari 5.1-6 */
background: linear-gradient(to left, #2C3E50 , #4CA1AF); /* W3C, IE 10+/ Edge, Firefox 16+, Chrome 26+, Opera 12+, Safari 7+ */">
    <div class="loginColumns animated fadeInDown">
        <div class="row" style="background-color: white;">
            <div class="col-md-6">

                <img src="/img/newlogo.png" class="img img-responsive" alt="Alternate Text" />
            </div>
            <div class="col-md-6">
                <div class="ibox-content" style="height: 300px;">
                    <div class="m-t" role="form">
                        <form id="form1" runat="server">
                            <asp:ScriptManager runat="server" />
                            <div class="form-group" runat="server" id="divform">
                                <div class="form-group">
                                    <asp:TextBox runat="server" ID="txtUserName" CssClass="form-control" placeholder="Username" />
                                </div>
                                <div class="form-group">
                                    <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" CssClass="form-control" placeholder="Password" />
                                </div>
                                <asp:Button Text="Login" ID="btnLogin" CssClass="btn btn-primary block full-width m-b" OnClick="btnLogin_Click" runat="server" />

                                <%--<div class="alert alert-danger" id="divError" runat="server" visible="false">
                                    <%--Wrong <a class="alert-link" href="#">Username or Password</a>.--%>
                                <asp:Label ID="lblError" CssClass="alert-link" runat="server" />
                                <div class="alert alert-danger" id="divError" runat="server" visible="false">
                                    Wrong <a class="alert-link" href="#">Username or Password</a>.
                                </div>
                                <%--</div>--%>
                                <%--<input type="button" class="btn btn-primary btnAdd" ></input>--%>
                                <span class="ForgetPassword">Forgot Password?
                                </span>
                                <%--<small>Forgot password</small>--%>
                            </div>
                             <input type="button" class="OpenModal" data-toggle="modal" data-target="#CreateProjectModal" style="display: none;" />
                            <div class="modal inmodal" id="CreateProjectModal" tabindex="-1" role="dialog" aria-hidden="true">
                                <div class="modal-dialog">
                                    <div class="modal-content animated flipInY">
                                        <div class="modal-header" style="padding-bottom: 9px; padding-top: 9px;">
                                            <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                                            <h4 class="modal-title">Forgot Password</h4>

                                            <input type="hidden" id="Hidden1" runat="server" class="hfCompanyId" />
                                        </div>
                                        <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>--%>
                                        <div class="modal-body" style="padding-bottom: 10px; border-bottom-width: 10px; padding-top: 10px;">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <div class="panel" style="margin-bottom: 0px;">
                                                        <div class="panel-heading">
                                                            <h3 class="panel-title">Secure login</h3>
                                                        </div>
                                                        <div class="panel-body">
                                                            <div class="col-lg-12">
                                                                <div class="form-group" style="padding-left: 0px; padding-right: 25px;">
                                                                    <label for="exampleInputPassword2">
                                                                        Enter your Login ID to receive your password</label><asp:RequiredFieldValidator ID="rfvtxtnic" runat="server" ValidationGroup="Save" Text="*"
                                                                            ErrorMessage="*" ForeColor="Red"
                                                                            Display="Dynamic" ControlToValidate="txtLoginId" CssClass="rfv"></asp:RequiredFieldValidator>

                                                                    <asp:TextBox ID="txtLoginId" runat="server" CssClass="form-control numeric txtLoginId"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <label id="Label2" runat="server" class="label label-danger" visible="false"></label>
                                                    </div>
                                                    <div class="modal-footer">
                                                        <asp:Button Text="Save" class="btn btn-primary" ID="btnForgetPassword" ValidationGroup="Save" OnClick="btnForgetPassword_Click" runat="server" />
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                             <%--</ContentTemplate>
                                        </asp:UpdatePanel>--%>
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                            <ProgressTemplate>
                                                <uc2:inprogress id="ucInprogress1" runat="server" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                        </div>
                                    </div>
                                </div>
                                </div>
                                <%--<a class="btn btn-sm btn-white btn-block" href="register.html">Create an account</a>--%>
                        </form>
                    </div>
                </div>
            </div>
        </div>

        <hr />
        <div class="row" style="color: white;">
            <div class="col-md-6">
                Copyright Sybrid Pvt LTD
            </div>
            <div class="col-md-6 text-right">
                <small>© 2016-2017</small>
            </div>
        </div>
    </div>
    <script>
        function pageLoad() {
            $('.ForgetPassword').click(function () {
                $('.txtLoginId').val('');
                $(".OpenModal").click();
            });
        }
        function AlertBox(title, Message, type) {
            swal(title, Message, type);
        }
        function ClosePopup() {
            $('.modal').hide();
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }
    </script>
</body>
</html>
