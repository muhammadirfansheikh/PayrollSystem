<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Mapping_Management.aspx.cs" Inherits="Pages_HCM_Mapping_Management" %>

<%@ Register Src="~/Controls/Shared/InProgress.ascx" TagName="InProgress" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.9/js/jquery.dataTables.min.js"></script>
    <link type="text/css" rel="stylesheet" href="https://cdn.datatables.net/1.10.16/css/jquery.dataTables.min.css" />

    <style>
        a:hover,
        a:focus {
            text-decoration: none;
            outline: none;
        }

        .tab {
            font-family: 'Raleway', sans-serif;
            padding: 15px;
        }

            .tab .nav-tabs {
                padding: 0 10px;
                margin: 0;
                border: none;
            }

                .tab .nav-tabs li a {
                    color: #fff;
                    background: linear-gradient(#438f28,#808080);
                    font-size: 18px;
                    font-weight: 600;
                    text-align: center;
                    text-transform: capitalize;
                    padding: 8px 20px 10px;
                    margin: 0 10px 16px 0;
                    border: none;
                    border-radius: 10px 10px 0 0;
                    overflow: hidden;
                    position: relative;
                    z-index: 1;
                    transition: all 0.3s ease 0.15s;
                }

                    .tab .nav-tabs li.active a,
                    .tab .nav-tabs li a:hover,
                    .tab .nav-tabs li.active a:hover {
                        color: #438f28;
                        /*background: linear-gradient(#438f28,#808080);*/
                        border: none;
                        box-shadow: 0 -3px 7px rgba(0,0,0,0.15);
                    }

                    .tab .nav-tabs li a:before {
                        content: "";
                        background: #fff;
                        height: 100%;
                        width: 100%;
                        border-radius: 8px 8px 0 0;
                        position: absolute;
                        top: 100%;
                        left: 0;
                        z-index: -1;
                        transition: all 0.3s ease-out 0s;
                    }

                    .tab .nav-tabs li.active a:before,
                    .tab .nav-tabs li a:hover:before {
                        top: 0;
                    }

            .tab .tab-content {
                /*background: linear-gradient(#438f28,#808080);*/
                font-size: 14px;
                letter-spacing: 1px;
                line-height: 25px;
                padding: 20px;
                border-radius: 10px;
                box-shadow: 0 0 10px rgba(0,0,0,0.2),0 0 0 15px #fff,0 0 30px rgba(0,0,0,0.9);
                position: relative;
            }

        @media only screen and (max-width: 479px) {
            .tab .nav-tabs {
                padding: 0;
                margin: 0 0 15px;
            }

                .tab .nav-tabs li {
                    width: 100%;
                    text-align: center;
                }

                    .tab .nav-tabs li a {
                        margin: 0 0 15px;
                        border-radius: 10px;
                    }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server" AssociatedUpdatePanelID="UpdatePanel1">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <uc2:InProgress ID="InProgress2" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>




    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Mapping Management</h2>
            <ol class="breadcrumb">

                <li>
                    <a href="#">HCM</a>
                </li>
                <li class="active">
                    <strong>Mapping Management</strong>
                </li>
            </ol>
        </div>
    </div>
    <br />

    <div class="row">
        <div class="col-md-12">
            <div class="tab" role="tabpanel">
                <!-- Nav tabs -->

                <ul class="nav nav-tabs" role="tablist">
                    <li role="presentation" class="active"><a href="#AllownceMapping" aria-controls="home" role="tab" data-toggle="tab">Allownce Mapping</a></li>
                    <%-- <li role="presentation"><a href="#Section2" aria-controls="profile" role="tab" data-toggle="tab">Section 2</a></li>
                    <li role="presentation"><a href="#Section3" aria-controls="messages" role="tab" data-toggle="tab">Section 3</a></li>--%>
                </ul>
                <!-- Tab panes -->
                <div class="tab-content tabs">
                    <div role="tabpanel" class="tab-pane fade in active" id="AllownceMapping">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="ddlAllownces" EventName="SelectedIndexChanged" />
                            </Triggers>
                            <ContentTemplate>
                                <div class="panel panel-danger" id="divError" runat="server" visible="false">
                                    <div class="panel-heading" id="lblError" runat="server"></div>
                                </div>
                                <div class="row" runat="server" id="DivSearchPanel">
                                    <div class="col-lg-12" style="margin-top: 11px;">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <h3 class="panel-title">Assign Employee Allownces</h3>
                                            </div>
                                            <div class="panel-body">
                                                <div class="form-group col-lg-3">
                                                    <label for="exampleInputEmail2">Group</label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="Add" Text="*" ForeColor="Red"
                                                        Display="Dynamic" ControlToValidate="ddlgroupSearch" InitialValue="0"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="Add" Text="*" ForeColor="Red"
                                                        Display="Dynamic" ControlToValidate="ddlgroupSearch" InitialValue=""></asp:RequiredFieldValidator>
                                                    <asp:DropDownList ID="ddlgroupSearch" OnSelectedIndexChanged="ddlgroupSearch_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control select2apply">
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="form-group col-lg-3">
                                                    <label for="exampleInputEmail2">Company</label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="Add" Text="*" ForeColor="Red"
                                                        Display="Dynamic" ControlToValidate="ddlCompany" InitialValue="0"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="Add" Text="*" ForeColor="Red"
                                                        Display="Dynamic" ControlToValidate="ddlCompany" InitialValue=""></asp:RequiredFieldValidator>
                                                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control select2apply" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>



                                                <div class="form-group col-lg-3">
                                                    <label for="exampleInputEmail2">Allownce</label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ValidationGroup="Add" Text="*" ForeColor="Red"
                                                        Display="Dynamic" ControlToValidate="ddlAllownces" InitialValue="0"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ValidationGroup="Add" Text="*" ForeColor="Red"
                                                        Display="Dynamic" ControlToValidate="ddlAllownces" InitialValue=""></asp:RequiredFieldValidator>
                                                    <asp:DropDownList ID="ddlAllownces" runat="server" CssClass="form-control select2apply" AutoPostBack="true" OnSelectedIndexChanged="ddlAllownces_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="form-group col-lg-3">
                                                    <label>Measure</label>
                                                    <asp:RequiredFieldValidator ID="rqMeasure" Enabled="false" runat="server" ValidationGroup="Add" Text="*" ForeColor="Red"
                                                        Display="Dynamic" ControlToValidate="txtMeasure" CssClass="rfvbankdescription"></asp:RequiredFieldValidator>
                                                    <asp:TextBox runat="server" ID="txtMeasure" onkeypress="ForceNumericInput(this,0,false)" class="form-control numeric" placeholder="Measure" />

                                                </div>
                                                <div class="form-group col-lg-12">
                                                    <label for="exampleInputEmail2">Employee</label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ValidationGroup="Add" Text="*" ForeColor="Red"
                                                        Display="Dynamic" ControlToValidate="lstEmployees" InitialValue="0"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ValidationGroup="Add" Text="*" ForeColor="Red"
                                                        Display="Dynamic" ControlToValidate="lstEmployees" InitialValue=""></asp:RequiredFieldValidator>

                                                    <asp:ListBox ID="lstEmployees" SelectionMode="Multiple" runat="server"></asp:ListBox>
                                                </div>
                                                <div class="form-group col-lg-3">
                                                    </br>
                                <asp:Button Text="Reset" class="btn btn-danger" ID="btnReset" OnClick="btnReset_Click" runat="server" />
                                                    <asp:Button Text="Save" class="btn btn-primary" ID="btnSave" ValidationGroup="Add" OnClick="btnSave_Click" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <%-- Modal End Here --%>
                            </ContentTemplate>

                        </asp:UpdatePanel>
                    </div>
                    <%--    <div role="tabpanel" class="tab-pane fade" id="Section2">
                        <h3>SecKtion 2</h3>
                        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce semper, magna a ultricies volutpat, mi eros viverra massa, vitae consequat nisi justo in tortor. Proin accumsan felis ac felis dapibus, non iaculis mi varius.</p>
                    </div>
                    <div role="tabpanel" class="tab-pane fade" id="Section3">
                        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce semper, magna a ultricies volutpat, mi eros viverra massa, vitae consequat nisi justo in tortor. Proin accumsan felis ac felis dapibus, non iaculis mi varius.</p>
                    </div>--%>
                </div>
            </div>
        </div>
    </div>

    <script src="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js" type="text/javascript"></script>

    <script type="text/javascript">
        function ForceNumericInput(This, AllowDot, AllowMinus) {
            if (arguments.length == 1) {
                var s = This.value;
                // if "-" exists then it better be the 1st character
                var i = s.lastIndexOf("-");
                if (i == -1)
                    return;
                if (i != 0)
                    This.value = s.substring(0, i) + s.substring(i + 1);
                return;
            }
            var code = event.keyCode;
            switch (code) {
                case 8:     // backspace
                case 37:    // left arrow
                case 39:    // right arrow
                    //case 46:    // delete
                    event.returnValue = true;
                    return;
            }
            if (code == 45)     // minus sign
            {
                if (AllowMinus == false) {
                    event.returnValue = false;
                    return;
                }




                // wait until the element has been updated to see if the minus is in the right spot
                var s = "ForceNumericInput(document.getElementById('" + This.id + "'))";
                setTimeout(s, 2);
                return;
            }
            if (AllowDot && code == 46) {
                if (This.value.indexOf(".") >= 0) {
                    // don't allow more than one dot
                    event.returnValue = false;
                    return;
                }
                event.returnValue = true;
                return;
            }
            // allow character of between 0 and 9
            if (code >= 48 && code <= 57) {
                event.returnValue = true;
                return;
            }
            event.returnValue = false;
        }

        function selectlistBox() {
            $('[id*=lstEmployees]').select2({
                placeholder: "Select",
                allowClear: true
            });

        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    selectlistBox();
                }
            });
        };
        function pageLoad() {


            $("#MainContent_txtMonthSearch").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'yyyy-MM-dd',
                onClose: function (selectedDate) {
                    $("#MainContent_txtMonthSearch").datepicker("option", "minDate", selectedDate);
                }
            });

            selectlistBox();
            $("#MainContent_txtMonthAdd").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'yyyy-MM-dd',
                onClose: function (selectedDate) {
                    $("#MainContent_txtMonthAdd").datepicker("option", "minDate", selectedDate);
                }
            });
            function reset() {
                //$(".txtAdd").val('');
                //$(".hfModalId").val('');
            }

            $('.select2apply').select2();
            //$('[id*=lstEmployees]').multiselect({
            //    includeSelectAllOption: true
            //});
            $('.txtMonthOfPayroll').datepicker('setDate', null);


            //var $j = jQuery.noConflict();
            // DataTable, save state
            //var adminUsersDT = $("#MainContent_rpt").DataTable({
            //    'bDestroy': true,
            //    "bStateSave": true,
            //    "orderable": false, "targets": [1, 2, 3, 4,5],
            //    dom: 'lfrtip',
            //    "fnStateSave": function (oSettings, oData) {
            //        localStorage.setItem('MainContent_rpt', JSON.stringify(oData));
            //    },
            //    "fnStateLoad": function (oSettings) {
            //        return JSON.parse(localStorage.getItem('MainContent_rpt'));
            //    }
            //});
        }
        function AlertBox(title, Message, type) {
            swal(title, Message, type);
        }
        function ClosePopup() {
            $('.modal').hide();
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }
        function OpenPopup() {
            $('.openmodal').click();
        }
    </script>
</asp:Content>

