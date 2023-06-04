<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="CreateEmployee.aspx.cs" Inherits="Pages_HCM_Setup_CreateEmployee" %>

<%@ Register Src="~/Controls/Shared/InProgress.ascx" TagName="InProgress" TagPrefix="uc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.maskedinput/1.4.1/jquery.maskedinput.js" integrity="sha512-yVcJYuVlmaPrv3FRfBYGbXaurHsB2cGmyHr4Rf1cxAS+IOe/tCqxWY/EoBKLoDknY4oI1BNJ1lSU2dxxGo9WDw==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <style>
        h1:first-letter {
            font-size: 20pt;
        }

        h1 {
            font-size: 16pt;
            font-variant: small-caps;
        }

        tr:nth-child(even) {
            background-color: rgba(20, 134, 136, 0.07);
        }

        .nonMandatoryValue {
            /*color: #118a11;*/
        }

        .MandatoryValue {
            color: red;
        }

        .centerAllign {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress runat="server">
        <ProgressTemplate>
            <uc2:InProgress ID="InProgress1" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>

    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Add / Edit Employee</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="/Pages/Default.aspx">Dashboard</a>
                </li>
                <li>
                    <a href="#">HCM Setup</a>
                </li>
                <li class="active">
                    <strong>Add / Edit Employee</strong>
                </li>
            </ol>
        </div>
    </div>


    <input type="hidden" runat="server" id="hfEmployeeId" class="hfEmployeeId" value="0" />

    <div class="row">
        <div class="col-lg-12" style="margin-top: 11px;">

            <div class="panel">
                <div class="panel-body" style="padding-top: 1px !important; padding-bottom: 1px !important;">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="form-group col-lg-12">
                                    <asp:ValidationSummary ID="validationSummary1" runat="server" ForeColor="Red" EnableClientScript="true"
                                        Enabled="true" ValidationGroup="AddEmployee" DisplayMode="BulletList"
                                        ShowSummary="true" HeaderText="Please fill the required fields" CssClass='validationSummary' />
                                </div>
                            </div>
                            <input type="hidden" id="hfFile_Driving" runat="server" class="hfFile_Driving" value="" />
                            <legend>
                                <h1><strong style="font-size: inherit;">Company Information </strong></h1>
                            </legend>
                            <div class="row">
                            </div>
                            <div class="row">
                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputEmail2">
                                        Group 
                                        <label class="MandatoryValue">* </label>
                                    </label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="AddEmployee" Text="*" ErrorMessage="Group" ForeColor="Red"
                                        Display="Dynamic" ControlToValidate="ddlGroupAdd" InitialValue="0"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlGroupAdd" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlGroupAdd_SelectedIndexChanged" CssClass="form-control applyselect2">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputPassword2">
                                        Company
                                                    <label class="MandatoryValue">* </label>
                                    </label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="AddEmployee" Text="*" ErrorMessage="Company" ForeColor="Red"
                                        Display="Dynamic" ControlToValidate="ddlCompanyAdd" InitialValue="0"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlCompanyAdd" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCompanyAdd_SelectedIndexChanged" CssClass="form-control applyselect2">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputPassword2">
                                        Employee Code
                                                    <label class="MandatoryValue">* </label>
                                    </label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator43" runat="server" ValidationGroup="AddEmployee" Text="*"
                                        ErrorMessage="Employee Code" ForeColor="Red"
                                        Display="Dynamic" ControlToValidate="txtEmployeeNo" CssClass="rfv"></asp:RequiredFieldValidator>
                                    <asp:TextBox runat="server" placeholder="Employee Code" ID="txtEmployeeNo" CssClass="form-control numeric txtAdd" AutoCompleteType="Disabled" />

                                </div>

                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputPassword2">
                                        Date Of Joining
                                     <label class="MandatoryValue">* </label>
                                    </label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="AddEmployee" Text="*"
                                        ErrorMessage="Date Of Joining" ForeColor="Red"
                                        Display="Dynamic" ControlToValidate="txtDOJadd" CssClass="rfv"></asp:RequiredFieldValidator>
                                    <asp:TextBox runat="server" placeholder="Date Of Joining" ID="txtDOJadd" CssClass="form-control  txtAdd DatePicker" AutoCompleteType="Disabled" />

                                </div>



                            </div>
                            <div class="row">
                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputPassword2">
                                        Employee Type
                                                    <label class="MandatoryValue">* </label>
                                    </label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator44" runat="server" ValidationGroup="AddEmployee" Text="*" ErrorMessage="Employee Type" ForeColor="Red"
                                        Display="Dynamic" ControlToValidate="ddlEmptype" InitialValue="0"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator46" runat="server" ValidationGroup="AddEmployee" Text="*" ErrorMessage="Employee Type" ForeColor="Red"
                                        Display="Dynamic" ControlToValidate="ddlEmptype" InitialValue=""></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlEmptype" runat="server" CssClass="form-control applyselect2">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputPassword2">
                                        Location<label class="MandatoryValue"> * </label>
                                    </label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="AddEmployee" Text="*" ErrorMessage="Location" ForeColor="Red"
                                        Display="Dynamic" ControlToValidate="ddlLocationAdd" InitialValue="0"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlLocationAdd" runat="server" CssClass="form-control applyselect2">
                                    </asp:DropDownList>
                                </div>


                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputPassword2">
                                        Business Unit<label class="MandatoryValue"> * </label>
                                    </label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ValidationGroup="AddEmployee" Text="*" ErrorMessage="Business Unit" ForeColor="Red"
                                        Display="Dynamic" ControlToValidate="ddlBusinessUnitAdd" InitialValue="0"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlBusinessUnitAdd" runat="server" CssClass="form-control applyselect2" AutoPostBack="true" OnSelectedIndexChanged="ddlBusinessUnitAdd_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputPassword2">
                                        Department<label class="MandatoryValue"> * </label>
                                    </label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ValidationGroup="AddEmployee" Text="*" ErrorMessage="Department" ForeColor="Red"
                                        Display="Dynamic" ControlToValidate="ddlDepartmentAdd" InitialValue="0"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlDepartmentAdd" runat="server" CssClass="form-control applyselect2">
                                    </asp:DropDownList>
                                </div>

                            </div>
                            <div class="row">
                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputPassword2">
                                        Job Category
                                                    <label class="MandatoryValue">* </label>
                                    </label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ValidationGroup="AddEmployee" Text="*" ErrorMessage="Job Category" ForeColor="Red"
                                        Display="Dynamic" ControlToValidate="ddlJobCategoryAdd" InitialValue="0"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlJobCategoryAdd" runat="server" CssClass="form-control applyselect2" AutoPostBack="true" OnSelectedIndexChanged="ddlJobCategoryAdd_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputPassword2">
                                        Designation<label class="MandatoryValue"> * </label>
                                    </label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ValidationGroup="AddEmployee" Text="*" ErrorMessage="Designation" ForeColor="Red"
                                        Display="Dynamic" ControlToValidate="ddlDesignationAdd" InitialValue="0"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlDesignationAdd" runat="server" CssClass="form-control applyselect2 ddlAdd ddlDesignationAdd">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputPassword2">
                                        Cost Center<label class="MandatoryValue"> * </label>
                                    </label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ValidationGroup="AddEmployee" Text="*" ErrorMessage="Cost Center" ForeColor="Red"
                                        Display="Dynamic" ControlToValidate="ddlCostCenterAdd" InitialValue="0"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlCostCenterAdd" runat="server" CssClass="form-control applyselect2">
                                    </asp:DropDownList>
                                </div>

                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputPassword2">
                                        SAP Cost Center
                                        <label class="MandatoryValue">* </label>
                                    </label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="AddEmployee" Text="*" ErrorMessage="SAP Cost Center" ForeColor="Red"
                                        Display="Dynamic" ControlToValidate="ddlSapCostCenter" InitialValue="0"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="AddEmployee" Text="*" ErrorMessage="SAP Cost Center" ForeColor="Red"
                                        Display="Dynamic" ControlToValidate="ddlSapCostCenter" InitialValue=""></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlSapCostCenter" runat="server" CssClass="form-control applyselect2">
                                    </asp:DropDownList>
                                </div>

                            </div>
                            <div class="row">
                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputPassword2">
                                        Reporting Cost Center<label class="MandatoryValue"> * </label>
                                    </label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ValidationGroup="AddEmployee" Text="*" ErrorMessage="Reporting Cost Center" ForeColor="Red"
                                        Display="Dynamic" ControlToValidate="ddlReportingCostCenter" InitialValue="0"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlReportingCostCenter" runat="server" CssClass="form-control applyselect2">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputEmail2">Employee Function</label>
                                    <asp:DropDownList ID="ddlEmpFunc" runat="server" CssClass="form-control applyselect2" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpFunc_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputEmail2">Employee Sub Function</label>
                                    <asp:DropDownList ID="ddlEmployeeSubFunction" runat="server" CssClass="form-control applyselect2">
                                    </asp:DropDownList>
                                </div>

                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputEmail2">EOBI Status</label>
                                    <asp:CheckBox ID="chkEobiStatus" runat="server" CssClass="form-control"></asp:CheckBox>
                                </div>

                            </div>
                            <div class="row">
                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputEmail2">EOBI No</label>
                                    <asp:TextBox ID="txtEobiNo" placeholder="EOBI No" runat="server" CssClass="form-control" AutoCompleteType="Disabled">
                                    </asp:TextBox>
                                </div>
                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputPassword2">
                                        EOBI Date
                                    </label>
                                    <a onclick="CleartxtEobiDate();" style="color: red;">Clear</a>
                                    <asp:TextBox ID="txtEobiDate" runat="server" placeholder="EOBI Date" CssClass="form-control txtEobiDate DatePicker" AutoCompleteType="Disabled" />
                                </div>
                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputEmail2">SESA No</label>
                                    <asp:TextBox ID="txtSesaNo" placeholder="SESA No" runat="server" CssClass="form-control numeric" AutoCompleteType="Disabled">
                                    </asp:TextBox>
                                </div>
                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputEmail2">Allow Intrest</label>
                                    <asp:CheckBox ID="chkAllowIntrest" runat="server" CssClass="form-control"></asp:CheckBox>
                                </div>

                            </div>
                            <div class="row">
                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputEmail2">Group Insurance</label>
                                    <asp:DropDownList ID="ddlGroupInsurance" runat="server" CssClass="form-control applyselect2">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputPassword2">Confirmation Date </label>
                                    <a onclick="CleartxtConfirmationdate();" style="color: red;">Clear</a>
                                    <asp:TextBox runat="server" ID="txtConfirmationdate" placeholder="Confirmation Date" CssClass="form-control DatePicker txtConfirmationdate" AutoCompleteType="Disabled" />
                                </div>
                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputEmail2">Contract Start Date </label>
                                    <a onclick="CleartxtContractstartdate();" style="color: red;">Clear</a>
                                    <asp:TextBox runat="server" ID="txtContractstartdate" placeholder="Contract Start Date" CssClass="form-control DatePicker txtContractstartdate" AutoCompleteType="Disabled" />
                                </div>
                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputEmail2">Contract End Date</label>
                                    <a onclick="CleartxtContractenddate();" style="color: red;">Clear</a>
                                    <asp:CompareValidator ID="CompareValidator7" runat="server"
                                        ControlToCompare="txtContractstartdate" CultureInvariantValues="true"
                                        Display="Dynamic" EnableClientScript="true"
                                        ControlToValidate="txtContractenddate"
                                        ErrorMessage="Contract Start date must be earlier than End date"
                                        Type="Date" SetFocusOnError="true" Operator="GreaterThanEqual" ForeColor="Red"
                                        Text="*"></asp:CompareValidator>
                                    <asp:TextBox runat="server" ID="txtContractenddate" placeholder="Contract End Date" CssClass="form-control txtProbationenddate DatePicker txtContractenddate" AutoCompleteType="Disabled" />
                                </div>

                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputEmail2">Advance Tax Deduction</label>
                                    <asp:CheckBox ID="chkAdvanceTaxDeduction" runat="server" CssClass="form-control"></asp:CheckBox>
                                </div>

                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputEmail2">Att. Allowance Status</label>
                                    <asp:CheckBox ID="chkAttendanceAllowanceStatus" runat="server" CssClass="form-control"></asp:CheckBox>
                                </div>


                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputPassword2">
                                        Company Code
                                      
                                    </label>
                                    
                                    
                                    <asp:DropDownList ID="ddlCompanyCode" runat="server" CssClass="form-control applyselect2">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <legend>
                                <h1><strong style="font-size: inherit;">Personal Information </strong></h1>
                            </legend>
                            <div class="row">
                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputPassword2">
                                        CNIC
                                                    <label class="MandatoryValue">* </label>
                                    </label>
                                    <asp:RequiredFieldValidator ID="rfvtxtnic" runat="server" ValidationGroup="AddEmployee" Text="*"
                                        ErrorMessage="CNIC" ForeColor="Red"
                                        Display="Dynamic" ControlToValidate="txtcnicaddNew" CssClass="rfv"></asp:RequiredFieldValidator>
                                    <%--                                    <asp:RegularExpressionValidator Display="Dynamic" ForeColor="Red" ValidationGroup="AddEmployee" ControlToValidate="txtcnicadd" ID="RegularExpressionValidator3" ValidationExpression="^[\s\S]{13,13}$" runat="server" Text="13 characters are required" ErrorMessage="CNIC 13 characters are required."></asp:RegularExpressionValidator>--%>
                                    <asp:TextBox ID="txtcnicaddNew" runat="server" placeholder="CNIC" CssClass="form-control txtnic  txtAdd" MaxLength="15" AutoCompleteType="Disabled" />
                                </div>
                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputEmail2">
                                        First Name 
                                        <label class="MandatoryValue">* </label>
                                    </label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="AddEmployee" Text="*"
                                        ErrorMessage="First Name" ForeColor="Red"
                                        Display="Dynamic" ControlToValidate="txtFirstNameNew" CssClass="rfv"></asp:RequiredFieldValidator>

                                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtFirstNameNew" ControlToCompare="txtMiddleNameNew" Operator="NotEqual"
                                        ErrorMessage="First Name should not be same as Middle Name" Text="*" ForeColor="Red" Display="Dynamic" ValidationGroup="AddEmployee"></asp:CompareValidator>

                                    <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="txtFirstNameNew" ControlToCompare="txtLastNameNew" Operator="NotEqual"
                                        ErrorMessage="First Name should not be same as Last Name" Text="*" ForeColor="Red" Display="Dynamic" ValidationGroup="AddEmployee"></asp:CompareValidator>

                                    <%--<asp:RegularExpressionValidator Display="Dynamic" ForeColor="Red" ValidationGroup="AddEmployee" ControlToValidate="txtFirstNameNew" ID="RegularExpressionValidator4" ValidationExpression="[A-Za-z]{1,32}" runat="server" Text="*" ErrorMessage="Only alphabets are required in First Name"></asp:RegularExpressionValidator>--%>

                                    <%-- pattern="[A-Za-z]{1,32}"--%>
                                    <asp:TextBox ID="txtFirstNameNew" placeholder="First Name" runat="server" CssClass="form-control alpha txtAdd" AutoCompleteType="Disabled">
                                    </asp:TextBox>
                                </div>
                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputPassword2" class="nonMandatoryValue">Middle Name</label>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtMiddleNameNew" ControlToCompare="txtFirstNameNew" Operator="NotEqual"
                                        ErrorMessage="Middle Name should not be same as First Name" Text="*" ForeColor="Red" Display="Dynamic" ValidationGroup="AddEmployee"></asp:CompareValidator>

                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtMiddleNameNew" ControlToCompare="txtLastNameNew" Operator="NotEqual"
                                        ErrorMessage="Middle Name should not be same as Last Name" Text="*" ForeColor="Red" Display="Dynamic" ValidationGroup="AddEmployee"></asp:CompareValidator>

                                    <%--<asp:RegularExpressionValidator Display="Dynamic" ForeColor="Red" ValidationGroup="AddEmployee" ControlToValidate="txtMiddleNameNew" ID="RegularExpressionValidator2" ValidationExpression="[A-Za-z]{1,32}" runat="server" Text="*" ErrorMessage="Only alphabets are required in Middle Name"></asp:RegularExpressionValidator>--%>
                                    <%-- pattern="[A-Za-z]{1,32}"--%>
                                    <asp:TextBox ID="txtMiddleNameNew" placeholder="Middle Name" runat="server" CssClass="form-control alpha txtAdd" AutoCompleteType="Disabled">
                                    </asp:TextBox>

                                </div>
                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputPassword2" class="nonMandatoryValue">Last Name </label>
                                    <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="txtLastNameNew" ControlToCompare="txtFirstNameNew" Operator="NotEqual"
                                        ErrorMessage="Last Name should not be same as First Name" Text="*" ForeColor="Red" Display="Dynamic" ValidationGroup="AddEmployee"></asp:CompareValidator>

                                    <asp:CompareValidator ID="CompareValidator6" runat="server" ControlToValidate="txtLastNameNew" ControlToCompare="txtMiddleNameNew" Operator="NotEqual"
                                        ErrorMessage="Last Name should not be same as Middle Name" Text="*" ForeColor="Red" Display="Dynamic" ValidationGroup="AddEmployee"></asp:CompareValidator>

                                    <%--<asp:RegularExpressionValidator Display="Dynamic" ForeColor="Red" ValidationGroup="AddEmployee" ControlToValidate="txtLastNameNew" ID="RegularExpressionValidator1" ValidationExpression="[A-Za-z]{1,32}" runat="server" Text="*" ErrorMessage="Only alphabets are required in Last Name"></asp:RegularExpressionValidator>--%>

                                    <%--  pattern="[A-Za-z]{1,32}"--%>
                                    <asp:TextBox ID="txtLastNameNew" placeholder="Last Name" runat="server" CssClass="form-control alpha txtAdd" AutoCompleteType="Disabled">
                                    </asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputPassword2">
                                        Date Of Birth
                                    </label>
                                    <a onclick="CleartxtDOB();" style="color: red;">Clear</a>
                                    <asp:TextBox runat="server" ID="txtDOB" placeholder="Date Of Birth" CssClass="form-control txtDOB DatePicker txtAdd" AutoCompleteType="Disabled" />
                                </div>
                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputPassword2">
                                        Gender
                                         <label class="MandatoryValue">* </label>
                                    </label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ValidationGroup="AddEmployee" Text="*" ErrorMessage="Gender" ForeColor="Red"
                                        Display="Dynamic" ControlToValidate="ddlGenderAdd" InitialValue="0"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlGenderAdd" runat="server" CssClass="form-control applyselect2  ddlGenderAdd ddlAdd">
                                    </asp:DropDownList>
                                </div>

                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputPassword2">
                                        Marital Status
                                         <label class="MandatoryValue">* </label>
                                    </label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="AddEmployee" Text="*" ErrorMessage="Marital Status" ForeColor="Red"
                                        Display="Dynamic" ControlToValidate="ddlMaritalStatus" InitialValue="0"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlMaritalStatus" runat="server" CssClass="form-control applyselect2  ddlMaritalStatus ddlAdd">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputPassword2" class="nonMandatoryValue">Blood Group</label>
                                    <asp:DropDownList ID="ddlBloodGroup" runat="server" CssClass="form-control applyselect2  ddlBloodGroup ddlAdd">
                                    </asp:DropDownList>
                                </div>

                            </div>


                            <div class="row">
                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputPassword2">
                                        Phone No
                                                    <label class="MandatoryValue"></label>
                                    </label>

                                    <asp:RegularExpressionValidator Display="Dynamic" ForeColor="Red" ValidationGroup="AddEmployee" ControlToValidate="txtPhoneNo" ID="RegularExpressionValidator3" ValidationExpression="^[\s\S]{11,11}$" runat="server" Text="11 characters are required" ErrorMessage="Phone No 13 characters are required."></asp:RegularExpressionValidator>
                                    <asp:TextBox ID="txtPhoneNo" runat="server" placeholder="Phone No" CssClass="form-control  txtAdd numeric" MaxLength="13" AutoCompleteType="Disabled" />
                                </div>
                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputEmail2">
                                        Cell No
                                        <label class="MandatoryValue">* </label>
                                    </label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="AddEmployee" Text="*"
                                        ErrorMessage="First Name" ForeColor="Red"
                                        Display="Dynamic" ControlToValidate="txtFirstNameNew" CssClass="rfv"></asp:RequiredFieldValidator>


                                    <asp:RegularExpressionValidator Display="Dynamic" ForeColor="Red" ValidationGroup="AddEmployee" ControlToValidate="txtCellNo" ID="RegularExpressionValidator4" ValidationExpression="^[\s\S]{11,11}$" runat="server" Text="*" ErrorMessage="Cell Phone 11 characters are required."></asp:RegularExpressionValidator>

                                    <%-- pattern="[A-Za-z]{1,32}"--%>
                                    <asp:TextBox ID="txtCellNo" placeholder="Cell No" runat="server" CssClass="form-control  txtAdd numeric" AutoCompleteType="Disabled">
                                    </asp:TextBox>
                                </div>
                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputPassword2" class="">
                                        Personal Email
                                        <%--<label class="MandatoryValue">* </label>--%>
                                    </label>
                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="AddEmployee" Text="*"
                                        ErrorMessage="Personal Email" ForeColor="Red"
                                        Display="Dynamic" ControlToValidate="txtPersonalEmail" CssClass="rfv"></asp:RequiredFieldValidator>--%>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtPersonalEmail"
                                        ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                        Display="Dynamic" ErrorMessage="Invalid email address" />
                                    <%-- pattern="[A-Za-z]{1,32}"--%>
                                    <asp:TextBox ID="txtPersonalEmail" placeholder="Personal Email" runat="server" CssClass="form-control  txtAdd" AutoCompleteType="Disabled">
                                    </asp:TextBox>

                                </div>
                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputPassword2" class="">
                                        Official Email
                                        <label class="MandatoryValue">* </label>
                                    </label>

                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtOfficialEmail"
                                        ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                        Display="Dynamic" ErrorMessage="Invalid email address" />
                                    <%-- pattern="[A-Za-z]{1,32}"--%>
                                    <asp:TextBox ID="txtOfficialEmail" placeholder="Personal Email" runat="server" CssClass="form-control  txtAdd" AutoCompleteType="Disabled">
                                    </asp:TextBox>

                                </div>
                            </div>

                            <div class="row">
                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputPassword2">
                                        Father/Husband Name
                                    </label>
                                    <asp:RegularExpressionValidator Display="Dynamic" ForeColor="Red" ValidationGroup="AddEmployee" ControlToValidate="txtFatherName" ID="RegularExpressionValidator7" ValidationExpression="[a-zA-Z][a-zA-Z ]+" runat="server" Text="*" ErrorMessage="Only alphabets are required in Father/Husband Name"></asp:RegularExpressionValidator>
                                    <asp:TextBox ID="txtFatherName" placeholder="Father/Husband Name" runat="server" MaxLength="40" CssClass="form-control AcceptStringOnly txtAdd" AutoCompleteType="Disabled">
                                    </asp:TextBox>
                                </div>
                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputPassword2">
                                        Religion   
                                    </label>
                                    <asp:DropDownList ID="ddlReligion" runat="server" CssClass="form-control  applyselect2 ddlReligion ddlAdd">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputEmail2">Grade Code </label>
                                    <asp:DropDownList ID="ddlGrade" runat="server" CssClass="form-control applyselect2">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputPassword2">
                                        Country  
                                         <label class="MandatoryValue">* </label>
                                    </label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="AddEmployee" Text="*" ErrorMessage="Country" ForeColor="Red"
                                        Display="Dynamic" ControlToValidate="ddlCountry" InitialValue="0"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ValidationGroup="AddEmployee" Text="*" ErrorMessage="Country" ForeColor="Red"
                                        Display="Dynamic" ControlToValidate="ddlCountry" InitialValue=""></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" CssClass="form-control  applyselect2 ddlCountryssss ddlAdd">
                                    </asp:DropDownList>
                                </div>

                            </div>


                            <div class="row">


                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputEmail2">
                                        Province<label class="MandatoryValue">* </label>
                                    </label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ValidationGroup="AddEmployee" Text="*" ErrorMessage="Province" ForeColor="Red"
                                        Display="Dynamic" ControlToValidate="ddlProvince" InitialValue="0"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ValidationGroup="AddEmployee" Text="*" ErrorMessage="Province" ForeColor="Red"
                                        Display="Dynamic" ControlToValidate="ddlProvince" InitialValue=""></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlProvince" AutoPostBack="true" OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged" runat="server" CssClass="form-control applyselect2">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputEmail2">
                                        City<label class="MandatoryValue">* </label>
                                    </label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ValidationGroup="AddEmployee" Text="*" ErrorMessage="City" ForeColor="Red"
                                        Display="Dynamic" ControlToValidate="ddlCity" InitialValue="0"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ValidationGroup="AddEmployee" Text="*" ErrorMessage="City" ForeColor="Red"
                                        Display="Dynamic" ControlToValidate="ddlCity" InitialValue=""></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control applyselect2">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputEmail2">
                                        IBAN No
                                    </label>

                                    <asp:TextBox ID="txtIbanNo" placeholder="IBAN No" MaxLength="25" runat="server" CssClass="form-control numeric">
                                    </asp:TextBox>
                                </div>
                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputEmail2">
                                        Tax/NTN No
                                    </label>

                                    <asp:TextBox ID="txtTaxNTNNo" MaxLength="25" placeholder="TAX/NTN No" runat="server" CssClass="form-control numeric">
                                    </asp:TextBox>
                                </div>
                            </div>

                            <div class="row">
                                <div class="form-group col-lg-12">
                                    <label>
                                        Address  
                                    </label>
                                    <asp:TextBox ID="txtPermanentaddress" placeholder="Address" runat="server" CssClass="form-control">
                                    </asp:TextBox>
                                </div>
                            </div>

                            <legend>
                                <h1><strong style="font-size: inherit;">Education Information </strong></h1>
                            </legend>
                            <div class="row">
                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputEmail2">
                                        Eductaion Degree
                                        <label class="MandatoryValue">* </label>
                                    </label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ValidationGroup="AddEducation" Text="*" ErrorMessage="Education Degree" ForeColor="Red"
                                        Display="Dynamic" ControlToValidate="cmbEducationDegree" InitialValue="0"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ValidationGroup="AddEducation" Text="*" ErrorMessage="Education Degree" ForeColor="Red"
                                        Display="Dynamic" ControlToValidate="cmbEducationDegree" InitialValue=""></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="cmbEducationDegree" runat="server" CssClass="form-control applyselect2" AutoPostBack="true" OnSelectedIndexChanged="ddlBankMaster_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputEmail2">
                                        Major
                                        <label class="MandatoryValue">*</label>
                                    </label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="AddEducation" Text="*"
                                        ErrorMessage="Major" ForeColor="Red"
                                        Display="Dynamic" ControlToValidate="txtMajor" CssClass="rfv"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtMajor" MaxLength="60" placeholder="Major" runat="server" CssClass="form-control" AutoCompleteType="Disabled">
                                    </asp:TextBox>
                                </div>
                                <div class="form-group col-lg-4 col-sm-6">
                                    <label for="exampleInputEmail2">
                                        Name Of University/Institute
                                        <label class="MandatoryValue">* </label>
                                    </label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ValidationGroup="AddEducation" Text="*"
                                        ErrorMessage="Name Of University/Institute" ForeColor="Red"
                                        Display="Dynamic" ControlToValidate="txtEductionUniName" CssClass="rfv"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtEductionUniName" MaxLength="60" placeholder="Name Of University" runat="server" CssClass="form-control" AutoCompleteType="Disabled">
                                    </asp:TextBox>
                                </div>
                                <div class="form-group col-lg-2 col-sm-6">
                                    <label for="exampleInputEmail2">
                                        Education Year
                                        <label class="MandatoryValue">* </label>
                                    </label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ValidationGroup="AddEducation" Text="*"
                                        ErrorMessage="Education Year" ForeColor="Red"
                                        Display="Dynamic" ControlToValidate="txtEducationYear" CssClass="rfv"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtEducationYear" MaxLength="4" placeholder="Education Year" runat="server" CssClass="form-control numeric" AutoCompleteType="Disabled">
                                    </asp:TextBox>
                                </div>
                                <div class="form-group col-lg-12">
                                    <asp:Button ID="btnAddEducationDetail" ValidationGroup="AddEducation" CssClass="btn btn-success pull-right" runat="server" Text="Add" OnClick="btnAddEducationDetail_Click" />

                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-lg-12 col-sm-6">
                                    <asp:GridView ID="grdEducation" runat="server" CssClass="table table-responsive table-bordered table-hover" AutoGenerateColumns="false" OnRowCommand="gridService_RowCommand">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="SR.NO">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSerialNo" runat="server"
                                                        Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Education Degree">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hfEducationDegreeId" runat="server" Value='<%# Eval("EducationDegreeId") %>' />
                                                    <asp:Label ID="lblEducationDegree" runat="server"
                                                        Text='<%# Eval("EducationDegree") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Major">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hfMajor" runat="server" Value='<%# Eval("Major") %>' />
                                                    <asp:Label ID="lblMajor" runat="server"
                                                        Text='<%# Eval("Major") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="University/Institute Name">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblEducationUniversityName" runat="server"
                                                        Text='<%# Eval("UniversityName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Year">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblYear" runat="server"
                                                        Text='<%# Eval("Year") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton Style="text-align: center" ID="lnkRemoveRow" CommandArgument='<%# Eval("EducationDegreeId") %>' CommandName="RemoveRowEducationGird" CssClass="btn btn-danger" runat="server">Remove</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>


                            <legend>
                                <h1><strong style="font-size: inherit;">Bank Information </strong></h1>
                            </legend>

                            <div class="row">
                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputEmail2">Bank </label>
                                    <asp:DropDownList ID="ddlBankMaster" runat="server" CssClass="form-control applyselect2" AutoPostBack="true" OnSelectedIndexChanged="ddlBankMaster_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>

                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputEmail2">Branch </label>
                                    <asp:DropDownList ID="ddlBank" runat="server" CssClass="form-control applyselect2">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputEmail2">Account No  </label>
                                    <asp:TextBox ID="txtAccountno" placeholder="Account No" runat="server" CssClass="form-control numeric" AutoCompleteType="Disabled">
                                    </asp:TextBox>
                                </div>

                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputEmail2">Account Type  </label>
                                    <asp:DropDownList ID="ddlAccountType" runat="server" CssClass="form-control applyselect2">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-lg-3 col-sm-6">
                                    <label for="exampleInputEmail2">Payment Mode  </label>
                                    <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="form-control applyselect2">
                                    </asp:DropDownList>
                                </div>

                            </div>

                            <div class="row">
                                <div class="form-group col-lg-12">
                                    <div id="divError" runat="server" visible="false" class="alert alert-danger" style="bottom: 20px;">
                                        <asp:Label ID="lblError" runat="server"></asp:Label>
                                    </div>
                                </div>

                                <div class="form-group col-lg-12">
                                    <asp:Button ID="BtnBack" CssClass="btn btn-default pull-right" runat="server" Text="Back" OnClick="BtnBack_Click" UseSubmitBehavior="false" />
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-default pull-right" ValidationGroup="AddEmployee" OnClick="btnSubmit_Click" />

                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

        </div>
    </div>

    <script type="text/javascript">

        jQuery(document).ready(function () {
            //https://community.gravityforms.com/t/cursor-position-when-input-mask-is-set/11708/3
            jQuery(document).on('mouseup', '.txtnic', function (e) {
                
                    e.target.setSelectionRange(0, 0);
                

            });

        });

        function pageLoad() {
            $('.DatePicker').datepicker({
                forceParse: false,
                calendarWeeks: true,
                autoclose: true,
                format: 'mm/dd/yyyy',
            });
            $('.DatePicker').keydown(function () {
                return false;
            });

            $(".txtnic").mask("99999-9999999-9", { autoclear: false });

        
           
           
            //var dt = new Date();
            //dt.setFullYear(new Date().getFullYear() - 18);
            //$('.datetimedob').datepicker({
            //    viewMode: "years",
            //    endDate: dt,
            //    format: 'mm/dd/yyyy',
            //    autoclose: true
            //});
            //$('.datetimedob').keydown(function () {
            //    return false;
            //});

            $(".numeric").numeric();


            $('.alpha').keypress(function (e) {
                var regex = new RegExp("[A-Za-z ]{1,32}");
                var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
                if (regex.test(str)) {
                    return true;
                }
                else {
                    e.preventDefault();
                    return false;
                }
            });



            $('.applyselect2').select2();
        }


        function CleartxtConfirmationdate() {
            $('.txtConfirmationdate').val('');
        }
        function CleartxtContractstartdate() {
            $('.txtContractstartdate').val('');
        }
        function CleartxtContractenddate() {
            $('.txtContractenddate').val('');
        }
        function CleartxtEobiDate() {
            $('.txtEobiDate').val('');
        }
        function CleartxtDOB() {
            $('.txtDOB').val('');
        }


        function AlertBox(title, Message, type) {
            swal(title, Message, type);
        }
        function AlertBoxRedirect(title, Message, type, url) {
            swal({
                title: title,
                text: Message,
                type: type,
                confirmButtonText: "OK",
                confirmButtonColor: "#1ab394",
                html: true,
            },
                function () {
                    window.location = url;
                });
        }



    </script>
</asp:Content>
