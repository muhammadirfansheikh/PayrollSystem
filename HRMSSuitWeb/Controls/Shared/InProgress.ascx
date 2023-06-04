<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InProgress.ascx.cs" Inherits="Controls_InProgress" %>

<%--<style type="text/css">
    .UpdateProgressTemplateContent, .MessageWrapper.ShowAsModalPopup .divMessage {
        border: 1px solid gray;
        position: fixed;
        left: 50%;
        top: 50%;
        margin-left: -250px;
        margin-top: -37px;
        width: 500px;
        height: 74px;
        background-color: white;
        border-radius: 5px;
        padding: 10px;
        z-index: 99999999;
    }

    .UpdateProgressTemplateContent {
        text-align: center;
        margin-left: -37px;
        margin-top: -37px;
        width: 74px;
        z-index: 99999999;
        height: 74px;
    }


        .UpdateProgressTemplateContent.WithMessage {
            margin-left: -250px;
            width: 500px;
        }

            .UpdateProgressTemplateContent.WithMessage .Message {
                padding-right: 20px;
            }

            .UpdateProgressTemplateContent.WithMessage .Image {
                width: 30px;
            }

    .UpdateProgressTemplate, .MessageWrapper.ShowAsModalPopup .MessageFullWindowBackground {
        position: fixed;
        width: 100%;
        height: 100%;
        left: 0;
        top: 0;
        background-color: Gray;
        opacity: 0.7;
        filter: alpha(opacity = 70);
        z-index: 9999;
    }
</style>--%>
<%--<div class="UpdateProgressTemplate"></div>--%>
<%--<table runat="server" id="divContent" class="UpdateProgressTemplateContent" style="border: solid">
    <tr>--%>
<%--       <td class="Image">
            <asp:Image ID="Image1" runat="server" alt="" ImageUrl="~/img/ajax-loader.gif" /></td>--%>
<%--  <td runat="server" id="tdMessage" class="Message">
            <asp:Label ID="lblMessage" runat="server" Text="Please wait while processing.">
            </asp:Label>
            <asp:Image ID="Image1" runat="server" alt="" ImageUrl="~/img/ajax-loader.gif" />
        </td>
    </tr>
</table>--%>


<style>
    .loaderBody1 {
        position: fixed;
        top: 40%;
        left: 35%;
        z-index: 99999;
    }

    .loader1 {
        background-color: black;
        position: fixed;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        z-index: 9999;
        overflow: hidden;
        opacity: 0.7;
    }
</style>

<div id="waitProgressBar1" class="col-xs-4 loaderBody1">
    <div class="widget lazur-bg p-lg text-center ">
        <div class="sk-spinner sk-spinner-rotating-plane" style="background-color: white"></div>
        <span>Please wait while loading..</span>
    </div>
</div>

<div class="loader1"></div>
