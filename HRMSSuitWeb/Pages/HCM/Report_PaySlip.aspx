<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_PaySlip.aspx.cs" Inherits="Pages_HCM_Report_PaySlip" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/PaySlip.ascx" TagPrefix="uc" TagName="PaySlip" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="Salary Slips" />
            </h2>
            <ol class="breadcrumb">
                
                <li>
                    <a href="#">HCM Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="Salary Slips" />
                    </strong>
                </li>
            </ol>
        </div>
    </div>
    <uc:EmployeeSearchFilters runat="server" ID="EmployeeSearchFilters" EmployeeCode="1" />

    <div class="panel panel-info">
        <div class="panel-heading">
            Report Functions
        </div>
        <div class="panel-body">
            <div class="col-lg-2 divMonthPayroll">
                <label>Select Month</label>
                <input type="text" class="form-control txtMonthOfPayroll DatePickerMonthComplete" />
            </div>
            <div class="col-lg-2">
                <label>Selected Employee </label>
                <input type="text" class="form-control txtSelectedEmployeeCode numeric" />
            </div>

            <div class="col-lg-8 div_reportbutton" style="margin-top: 23px; display: none;">
                <input type="button" class="btn btn-warning pull-left" value="Get" onclick="GetPaySlip();" />
                <div id="editor"></div>
                <input type="button" class="btn btn-info pull-right btnExport" value="Export" id="btnExport" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tablePrintData')" />

                <input type="button" class="btn btn-success pull-right m-r-sm" value="Next" onclick="Next();" />
                <input type="button" class="btn btn-success pull-right m-r-sm" value="Previous" onclick="Previous();" />

            </div>
        </div>
    </div>

    <div class="panel panel-info">
        <div class="panel-heading">
            Salary Slips
        </div>
        <div class="panel-body SalarySlips">
            <uc:PaySlip runat="server" ID="PaySlip" />
        </div>
    </div>

    <script src="../../js/Page_JS/Report_PaySlip.js"></script>

    <script>

        $(".btnExport").click(function () {

            var w = $('.tablePrintData').height();
            var h = $('.tablePrintData').width();

            var doc = new jsPDF();

            doc.addHTML($('.tablePrintData'),
            //doc.fromHTML($('.tablePrintData').html(),
                {
                    'background': '#fff',
                },

                function () {

                    doc.save($(".txtSelectedEmployeeCode").val() + '.pdf');
                });

            return false;
        });

        /*
        function ExportPdf()
        {
            ;
            var doc = new jsPDF();
            var specialElementHandlers = {
                '#editor': function (element, renderer) {
                    return true;
                }
            };

            //$('#btnExport').click(function () {
            doc.addHTML($('.tablePrintData'), 15, 15, {
                    'width': 170,
                    'elementHandlers': specialElementHandlers
                });
                doc.save('sample-file.pdf');
            //});

            // This code is collected but useful, click below to jsfiddle link.

        }
        */

    </script>

</asp:Content>

