<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExportCsv.aspx.cs" Inherits="AngelRM.Web.Admin.Fileinfo.ExportCsv" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>数据导出</title>
    <link href="../css/base.css" rel="stylesheet" type="text/css" />
    <link href="../css/main.css" rel="stylesheet" type="text/css" />
    <link href="../js/easyui/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../js/easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/easyui/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../js/easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../js/easyui/locale/easyui-lang-zh_CN.js"></script>
    <script src="../js/Angel_Common.js" type="text/javascript"></script>
    <script type="text/javascript">
        var formTitle = "数据导出";
    </script>
    <style type="text/css">
        .export_box
        {
            width: 580px;
            height: 240px;
            border: solid 10px #e9f2ff;
            border-radius: 16px;
            margin: 0 auto;
            margin-top: 100px;
        }
        .export_box p
        {
            height: 32px;
        }
        .export_box p.sj
        {
            margin-top: 0px;
        }
        .export_box p label
        {
            width: 210px;
            line-height: 28px;
            float: left;
            text-align: right;
            font-size: 14px;
            color: #3b3a3a;
        }
        .export_box p .export_input
        {
            float: left;
            width: 212px;
            height: 28px;
            border: solid 1px #dfdfdf;
        }
    </style>
</head>
<body>
   <form id="form1" runat="server">
    <div class="export_box">
        <input type="hidden" id="hidexportfield" runat="server" />
        <input type="hidden" id="hidexporttitle" runat="server" />
        <p style="height: 25px;">
            <asp:TextBox runat="server" ID="tb_strCondition" name="" class="user_input" Style="width: 120px;
                display: none;"></asp:TextBox></p>
        <p style="height: 25px;">
            <asp:TextBox runat="server" ID="tb_tableName" name="" class="user_input" Style="width: 120px;
                display: none;"></asp:TextBox></p>
        <p class="sj">
            <label>
                数据内容：</label><asp:TextBox runat="server" ID="tb_listTitle" name="tb_listTitle" class="export_input"
                    ReadOnly="true"></asp:TextBox></p>
        <p style="margin-top: 25px;">
            <label>
                导出条数：</label><asp:TextBox runat="server" ID="tb_Count" name="" class="export_input"
                    ReadOnly="true"></asp:TextBox></p>
        <p style="margin-top: 34px; padding-left: 210px;">
            <span>
                <asp:ImageButton ID="btn_export" runat="server" ImageUrl="../images/export.png" OnClick="btnExport_Click"
                    target="_top" Style="width: 65px; height: 28px" /></span><span style="padding-left: 60px;"><a
                        id="btn_Cancel" href="javascript:()"><img style="border: 0; cursor: pointer;" src="../images/btn_cancel_b.gif"
                            onclick="return btnCancel_Click()" /></a></span></p>
    </div>
    </form>
</body>
</html>
