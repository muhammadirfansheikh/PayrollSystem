<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginHistory.aspx.cs" Inherits="Pages_LoginHistory" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .red {
            color: red;
        }

        .green {
            color: green;
        }

        .list {
            margin-top: 5px;
            border-collapse: collapse;
            color: Black;
            /*font-size: 11px;*/
            margin: auto;
            text-align: left;
            vertical-align: top;
            width: 98%;
        }
            /*.list table td
{
    border: 1px solid orange;
}*/
            .list td {
                border: #419639 1px solid;
                vertical-align: top;
                padding-top: 2px;
                padding: 0 15px;
            }

            .list thead tr th, .list tr th {
                background: -webkit-linear-gradient(#55557d, #55557d); /* For Safari 5.1 to 6.0 */
                background: -o-linear-gradient(#55557d, #55557d); /* For Opera 11.1 to 12.0 */
                background: -moz-linear-gradient(#55557d, #55557d); /* For Firefox 3.6 to 15 */
                background: linear-gradient(#55557d, #55557d); /* Standard syntax */ /*background-color: orange;*/
                color: white;
                /*font-size: 13px;*/
                font-weight: normal;
                padding: 5px 5px;
                height: 20px;
            }

                .list tr th:first-child {
                    border-top-left-radius: 15px;
                    padding-left: 15px;
                }

                .list tr th:last-child {
                    border-top-right-radius: 15px;
                }

            .list thead a {
                color: #540d59;
                text-decoration: underline;
            }

            .list tbody tr td, .list tr td {
                border: #55557d 1px solid;
                padding: 5px 5px;
                height: 20px;
            }

        .pagging {
            padding: 5px;
        }

            .pagging select {
                border: thin #CCCCCC solid;
                padding: 1px 5px 0 2px;
                font-family: Calibri;
                height: 19px;
                color: #000;
                font-size: 8pt;
            }
    </style>

</head>

<body>
    <form id="form1" runat="server">
        <div style="padding: 20px">
            <asp:Repeater ID="rptLoginHistory" runat="server">
                <HeaderTemplate>
                    <table class="list">
                        <thead>
                            <tr>
                                <th>Is Success</th>
                                <th>Login Date</th>
                                <th>User IP</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# Convert.ToBoolean( Eval("IsSuccess")) == false ? "<div class='red'>Failure</div>" :"<div class='green'>Success</div>" %></td>
                        <td><%# Eval("CreatedDate") %></td>
                        <td><%# Eval("UserIP") %></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
