<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PermissionList.aspx.cs"
    Inherits="AngelRM.Web.Admin.Permission.PermissionList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>栏目操作权限</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" type="text/css" href="../css/base.css">
    <link rel="stylesheet" type="text/css" href="../css/main.css">
    <link rel="stylesheet" type="text/css" href="../js/easyui/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../js/easyui/themes/icon.css" />
    <script type="text/javascript" src="../js/easyui/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../js/easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../js/easyui/locale/easyui-lang-zh_CN.js"></script>
    <script src="js/TableBasicNew.js" type="text/javascript"></script>
    <script type="text/javascript">
        //数据声明
        var pageTableName = "View_Angel_Admin_Roleoperate";
        var pageModelClassName = "View_Angel_Admin_Roleoperate";
        var listTitle = "操作权限";
        var pageTitle = "权限信息";
        var formTitle = "权限列表";
        var formUrl = "PermissionList.aspx";
        var listPageUrl = "../Permission/PermissionList.aspx";

        var FrameId = "PermissionList";
        var querryConditionStr = "1=1 ";
        var str_ExportConditon = "";

        var SelectedIds = [];
        var editing = false;
        var pNumber = 1;
        //页面一览表的列标题
        var Columns = {
            columns: [[
                        { field: 'id', title: '记录代码', align: 'left', headalign: 'center', width: 80, editor: 'text', hidden: true },
                        { field: 'RoleId', title: '角色ID', align: 'center', headalign: 'center', width: 80, editor: 'text', hidden: true },
                        { field: 'NavName', title: '栏目代码', align: 'center', headalign: 'center', width: 80, editor: 'text', hidden: true },
                        { field: 'RoleName', title: '角色', align: 'center', headalign: 'center', width: 80, editor: 'text' },
                        { field: 'TitleName', title: '栏目名称', align: 'center', headalign: 'center', width: 100, editor: 'text' },
                        { field: 'NavUrl', title: '栏目地址', align: 'center', headalign: 'center', width: 260, editor: 'text' },
                        { field: 'Operation_Value', title: '操作列表', align: 'center', headalign: 'center', width: 260, editor: 'text' },
                        { field: 'IsView', title: '是否显示', align: 'center', headalign: 'center', width: 80, editor: 'text' },
            //命令列
                        {
                        field: 'operation', title: '操 作', align: 'center', halign: 'center', width: 80, formatter: function (value, row, index) {

                            var e = '<a href="#" onclick="rowEditClick(' + index + ')">编辑</a> ';
                            var d = '<a href="#" onclick="rowDeleteClick(' + index + ')">删除</a> ';
                            return e + d;

                        }
                    }//命令列end
            ]],
          toolbar: '#tb'
        };
        function Serarch(rec) {
            querryConditionStr = "";
            if ($('#RoleID').combobox('getValue') != 0)
                querryConditionStr += " RoleId= " + $('#RoleID').combobox('getValue') + " ";
                LoadAjaxData();
        }
    </script>
</head>
<body>
    <div style="width: 100%; margin-top: 10px;" class="user">
        <!--白色背景区域-->
        <table width="100%" cellpadding="0" cellspacing="0" class="user_table">
            <!--表格区域-->
            <tr>
                <td style="border-bottom: none; _border-bottom: 0;" align="center">
                    <!-- DataGrid数据表格-->
                    <div id="Div1">
                        <table id="tt">
                        </table>
                        <br />
                    </div>
                </td>
            </tr>
        </table>

        <div id="tb">
            <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-search',plain:true" onclick="toolBarSearchClick();">查询</a>
            <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-reload',plain:true" onclick="toolBarReloadClick();">刷新</a>
            <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-redo',plain:true" onclick="toolBarExportClick();">导出</a>
            选择角色：<select id="RoleID" class="easyui-combobox" data-options="panelHeight: 'auto',valueField:'id', textField:'text',url:'../Admin/ashx/RoleService.ashx?action=List',onSelect: Serarch" style="width: 100px;"></select>
        </div>


        <div id="dd" class="easyui-dialog" style="width: 500px; overflow: hidden;" data-options="buttons:'#tb1',modal:true,closed:true">
            <form id="ff" method="post">
            <table width="100%" cellpadding="0" cellspacing="0" class="table" style="margin-top: 0px;">
                <tr>
                    <td align="right">
                        <input type="hidden" id="id" name="id" />
                        <input type="hidden" id="RoleId" name="RoleId" />
                        <label>
                           栏目代码</label>
                    </td>
                    <td>
                        <input id="NavName" name="NavName" style="width: 200px;" readonly />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <label>
                          栏目名称</label>
                    </td>
                    <td>
                        <input type="text" id="TitleName" name="TitleName" readonly />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        角色
                    </td>
                    <td>
                        <input type="text" id="RoleName" name="RoleName" readonly />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        操作列表
                    </td>
                    <td>
                   <select id="Operation_Value" name="Operation_Value" class="easyui-combotree" style="width: 200px" data-options="panelHeight:'auto',multiple: true,editable: false"></select>
                    </td>
                </tr>
            </table>
            </form>
        </div>
        <div id="tb1">
            <input type="hidden" id="hidindex" />
            <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-save'" onclick="Save($('#id').val())">
                保存</a> <a href="#" class="easyui-linkbutton" onclick="$('#dd').dialog('close');">取消</a>
        </div>
    </div>
</body>
</html>
