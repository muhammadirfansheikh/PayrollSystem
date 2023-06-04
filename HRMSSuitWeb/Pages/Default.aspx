<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Pages_Default" MasterPageFile="~/MasterPage/AdminMaster.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">


    <script src="/js/Chart.bundle.js"></script>

    <script src="/js/Chart.bundle.min.js"></script>

    <script src='/js/Chart.js'></script>

    <script src="/js/plugins/chartJs/Chart.min.js"></script>
    <script src='/js/jquery.circliful.js'></script>

    <style>
        .clsfont {
            font-size: 15px !important;
        }

        .abc {
            /*font-weight: bold;
            border: none;
            color: white;*/
            background-color: #438f28 !important;
            /*height: 25px;
            border-radius: 3px;
            font-size: 12px;*/
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="row" style="margin-left: 0px;">
                <div class="panel panel-info">
                    <div class="panel panel-heading" style="margin-bottom: 0px;">
                        <h3>User Information</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row" style="margin-top: 7px;">
                            <div class="col-lg-12">
                                <div class="panel panel-info">
                                    <div class="panel-body" style="margin-top: 7px;">
                                        <div class="col-lg-12">
                                            <div class="col-lg-3">
                                                <div class="row">
                                                    <asp:Image ID="imgEmployeeImage" runat="server" Height="200px" Style="max-width: 100%;" CssClass="img-circle" ImageUrl="/images/noprofilepic.png" />
                                                </div>
                                            </div>
                                            <div class="col-lg-7">
                                                <div class="row">
                                                    <div class="col-lg-6">
                                                        <label for="exampleInputPassword2">Employee #</label>
                                                        <asp:TextBox ID="lblEmpCode" ReadOnly runat="server" CssClass="form-control abc"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <label for="exampleInputPassword2">Name</label>
                                                        <asp:TextBox ID="lblEmployeeName" ReadOnly runat="server" CssClass="form-control abc"></asp:TextBox>
                                                    </div>

                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-6">
                                                        <label for="exampleInputPassword2">Department</label>
                                                        <asp:TextBox ID="lblDepartment" ReadOnly runat="server" CssClass="form-control abc"></asp:TextBox>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <label for="exampleInputPassword2">Designation</label>
                                                        <asp:TextBox ID="lblDesignation" ReadOnly runat="server" CssClass="form-control abc"></asp:TextBox>
                                                    </div>


                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <label for="exampleInputPassword2">Official Email</label>
                                                        <asp:TextBox ID="lblOfficialEmail" ReadOnly runat="server" CssClass="form-control abc"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
