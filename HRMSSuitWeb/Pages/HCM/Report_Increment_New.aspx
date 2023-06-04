<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_Increment_New.aspx.cs" Inherits="Pages_HCM_Report_Increment_New" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <iframe id="txtArea1" style="display: none"></iframe>
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="Employee Increment List" />
            </h2>
            <ol class="breadcrumb">

                <li>
                    <a href="#">Other Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="Employee Increment List" />
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


            <%--<div class="col-lg-2 ">
                <label>Bank</label>
                <select class="form-control ddlBank" onchange="BindBankBranch();"></select>
            </div>

            <div class="col-lg-2 ">
                <label>Branch</label>
                <select class="form-control ddlBranch"></select>
            </div>--%>

            <%--            <div class="col-lg-2 ">
                <label>Select </label>
                <select class="form-control  ddlGroupBy"></select>
            </div>--%>

            <div class="col-lg-3">
                Month
                <input type="text" class="form-control txtFromDate DatePickerMonthComplete txtMonthOfPayroll" />
            </div>

            <div class="col-lg-2 ">
                <label>Select </label>
                <select class="form-control  ddlGroupBy"></select>
            </div>
            <div class="col-lg-8 div_reportbutton" style="margin-top: 23px; display: none;">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelExportWithFileSaver('tableIncrementNewListListing','Report_Employee_Increment_List')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tableIncrementNewListListing')" />
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            Employee Increment List
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tableIncrementNewListListing" id="tableIncrementNewListListing">
                <thead class="theadEmployeeIncrementListlisting">
                    <tr>
                        <uc:ReportHeader runat="server" ID="ReportHeader2" />
                    </tr>
                    <tr class="info">
                        <th>Sr No</th>
                        <th>acd</th>
                        <th>co</th>
                        <th>Employee Code</th>
                        <th>Name Of Employee</th>
                        <th>odsg</th>
                        <th>dsg</th>
                        <th>yr</th>

                        <th>incr</th>
                        <th>nbas</th>
                        <th>nhrt</th>
                        <th>ncla1</th>
                        <th>nmed</th>
                        <th>nspl1</th>
                        <th>ngros</th>
                        <th>obas</th>
                        <th>ohrt</th>
                        <th>ocla1</th>
                        <th>omed</th>
                        <th>ospl1</th>
                        <th>ogros</th>
                        <th>normal</th>
                        <th>spl</th>
                        <th>adj</th>
                        <th>pro</th>


                        <th>loc</th>
                        <th>prov</th>
                        <th>dep</th>
                        <th>cost</th>
                        <th>jndt1</th>
                        <th>cfdt1</th>
                        <th>yr1</th>
                        <th>efdate</th>
                        <th>ncost</th>



                    </tr>
                </thead>
                <tbody class="tbodyEmployeeIncrementListListing">
                </tbody>
                <tfoot>
                    <tr class="info">



                        <td colspan="8" style="font-weight: bold">Total</td>


                        <td class="tdincr" style="text-align: right"></td>
                        <td class="tdnbas" style="text-align: right"></td>
                        <td class="tdnhrt" style="text-align: right"></td>
                        <td class="tdncla1" style="text-align: right"></td>
                        <td class="tdnmed" style="text-align: right"></td>
                        <td class="tdnspl1" style="text-align: right"></td>
                        <td class="tdngros" style="text-align: right"></td>
                        <td class="tdobas" style="text-align: right"></td>
                        <td class="tdohrt" style="text-align: right"></td>
                        <td class="tdocla1" style="text-align: right"></td>
                        <td class="tdomed" style="text-align: right"></td>
                        <td class="tdospl1" style="text-align: right"></td>
                        <td class="tdogros" style="text-align: right"></td>
                        <td class="tdnormal" style="text-align: right"></td>
                        <td class="tdspl" style="text-align: right"></td>
                        <td class="tdadj" style="text-align: right"></td>
                        <td class="tdpro" style="text-align: right"></td>

                        <td class="tdloc" style="text-align: right"></td>
                        <td class="tdprov" style="text-align: right"></td>
                        <td class="tddep" style="text-align: right"></td>
                        <td class="tdcost" style="text-align: right"></td>
                        <td class="tdjndt1" style="text-align: right"></td>
                        <td class="tdcfdt1" style="text-align: right"></td>
                        <td class="tdyr1" style="text-align: right"></td>
                        <td class="tdefdate" style="text-align: right"></td>
                        <td class="tdncost" style="text-align: right"></td>

                    </tr>
                </tfoot>
            </table>
        </div>
        <br />
    </div>
    <script type="text/x-jQuery-tmpl" id="EmployeeIncrementListListing">
        <tr class="trList">
            <td class="project-title"></td>

            <td class="project-title clsacd ABC">${acd}
                  <input class="clsSapCostCenter" type="hidden" value="${clsSapCostCenter}" />
                <input class="clsCostCenter" type="hidden" value="${clsCostCenter}" />
                <input class="clsLocation" type="hidden" value="${clsLocation}" />
                <input class="clsDepartment" type="hidden" value="${clsDepartment}" />
            </td>
            <td class="project-title clsco">${co}</td>
            <td class="project-title clsEmployeeCode">${EmployeeCode}</td>
            <td class="project-title clsNameOfEmployees">${NameOfEmployees}</td>
            <td class="project-title clsodsg">${odsg}</td>
            <td class="project-title clsdsg">${dsg}</td>
            <td class="project-title clsyr">${yr}</td>


            <td class="project-title clsincr" style="text-align: right">${incr}</td>
            <td class="project-title clsnbas" style="text-align: right">${nbas}</td>
            <td class="project-title clsnhrt" style="text-align: right">${nhrt}</td>
            <td class="project-title clsncla1" style="text-align: right">${ncla1}</td>
            <td class="project-title clsnmed" style="text-align: right">${nmed}</td>
            <td class="project-title clsnspl1" style="text-align: right">${nspl1}</td>
            <td class="project-title clsngros" style="text-align: right">${ngros}</td>
            <td class="project-title clsobas" style="text-align: right">${obas}</td>
            <td class="project-title clsohrt" style="text-align: right">${ohrt}</td>
            <td class="project-title clsocla1" style="text-align: right">${ocla1}</td>
            <td class="project-title clsomed" style="text-align: right">${omed}</td>
            <td class="project-title clsospl1" style="text-align: right">${ospl1}</td>
            <td class="project-title clsogros" style="text-align: right">${ogros}</td>
            <td class="project-title clsnormal" style="text-align: right">${normal}</td>
            <td class="project-title clsspl" style="text-align: right">${spl}</td>
            <td class="project-title clsadj" style="text-align: right">${adj}</td>
            <td class="project-title clspro" style="text-align: right">${pro}</td>


            <td class="project-title clsloc" style="text-align: right">${loc}</td>
            <td class="project-title clsprov" style="text-align: right">${prov}</td>
            <td class="project-title clsdep" style="text-align: right">${dep}</td>
            <td class="project-title clscost" style="text-align: right">${cost}</td>
            <td class="project-title clsjndt1" style="text-align: right">${jndt1}</td>
            <td class="project-title clscfdt1" style="text-align: right">${cfdt1}</td>
            <td class="project-title clsyr1" style="text-align: right">${yr1}</td>
            <td class="project-title clsefdate" style="text-align: right">${efdate}</td>
            <td class="project-title clsncost" style="text-align: right">${ncost}</td>











        </tr>
    </script>

    <script src="../../js/Page_JS/Report_Employee_Increment.js"></script>

</asp:Content>

