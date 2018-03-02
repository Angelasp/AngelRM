<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleList.aspx.cs" Inherits="AngelRM.Web.Admin.Admin.RoleList" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>角色管理</title>
    <link rel="stylesheet" type="text/css" href="../css/base.css">
    <link rel="stylesheet" type="text/css" href="../css/main.css">
    <link href="../js/easyui/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../js/easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <script src="../js/easyui/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="../js/easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../js/easyui/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/TableBasicNew.js"></script>
    <script type="text/javascript" src="../js/TableView.js"></script>
    <script type="text/javascript">
        //数据声明
        var pageTableName = "Angel_Admin_Role";
        var pageModelClassName = "Angel_Admin_Role";
        var pageTitle = "角色管理";                         //本页标题
        var pageUrl = "../Admin/RoleList.aspx";             //本页Url
        var pageFrameId = "Rolelist";                 //本页FrameId
        var FrameId = "Rolelist";                        //工具栏查询按钮调用
        var formTitle = "编辑角色";                     //明细标题(双击行打开用)
        var formFrameId = "Admins";                      //明细FrameID
        var formUrl = "ashx/RoleService.ashx";        //操作Url
        var formTitled = "角色查看";                 //详细标题
        var formFrameIdd = "Adminsd";                 //详细FrameIDd
       // var formUrld = "../Admin/RoleList.aspx";  //详细Url

        var querryConditionStr = "1=1";
        var str_ExportConditon = "";
        var SelectedIds = [];
        var editing = false;
        var pNumber = 1;
        var listActionUrl = "../Admin/ashx/xDataTable_Action.ashx"; //Server端操作
        //页面一览表的列标题;
        var Columns = {
            columns: [[
                        { field: 'ID', title: '记录号', align: 'left', halign: 'center', width: 80, editor: 'text', hidden: true },
                        { field: 'RoleName', title: '角色名称', align: 'left', halign: 'center', width: 80, editor: { type: 'validatebox', options: { required: true}} },
                        { field: 'RoleType', title: '角色类型', align: 'left', halign: 'center', width: 80, editor: 'text' },
                        { field: 'IsSystem', title: '是否属于系统', align: 'left', headalign: 'center', width: 120, editor: 'text', formatter: function (value) { if (value == "1") { return "是"; } else { return "否"; } } },
                        { field: 'IsWorking', title: '是否启用', align: 'center', headalign: 'center', width: 80, editor: 'text', formatter: function (value) { if (value == "1") { return "是"; } else { return "否"; } } },
                        //命令列
                        {
                        field: 'operation', title: '操 作', align: 'center', halign: 'center', width: 200, formatter: function (value, row, index) {
                            s = '<a href="#" onclick="showRoleNavlist(' + index + ')">设计权限</a> ';
                            e = '<a href="#" onclick="viewCurRecord(' + index + ')">编辑</a> ';
                            d = '<a href="#" onclick="deleteCurRow(' + index + ')">删除</a> ';
                            return s + e + d;
                        }
                    }//命令列end

            ]]
        };

                //
                //信息修改表单
                //
                function viewCurRecord(index) {
                    SaveStuate = 'save';
                    //清除之前的选择
                    $("#tt").datagrid("clearSelections");
                    //获取选中的行数据
                    var raws = $('#tt').datagrid("getRows");
                    if ((index >= raws.length) || index < 0) return;
                    var raw = raws[index];
                    $('#ff').form('clear');
                    $('#dd').dialog({ title: '编辑角色', closed: false });
                    $('#id').val(raw.ID);
                    $('#RoleName').val(raw.RoleName);
                    $('#RoleType').val(raw.RoleType);
                    $("input[name='IsSystem'][value=" + raw.IsSystem + "]").attr("checked", true); 
                    $("input[name='IsWorking'][value=" + raw.IsWorking + "]").attr("checked", true);

                }


                //
                //信息修改表单
                //
                function showRoleNavlist(index) {
                    SaveStuate = 'save';
                    //清除之前的选择
                    $("#tt").datagrid("clearSelections");
                    //获取选中的行数据
                    var raws = $('#tt').datagrid("getRows");
                    if ((index >= raws.length) || index < 0) return;
                    var raw = raws[index];
                    $('#Permission').dialog({ title: '栏目权限', closed: false });
                    $('#rtt').tree({
                        url: 'ashx/RoleService.ashx?action=RoleTree&Rid=' + raw.ID,
                        method: 'get',
                        animate: true,
                        cascadeCheck: false,
                        checkbox: true
                    });
                    $('#Rid').val(raw.ID);
                }
</script>
</head>
<body>
    <style type="text/css">

    </style>

        <div style="background: #FFFFFF; width: 100%; margin-top: 10px;" class="user">
            <!--白色背景区域-->
            <table width="100%" cellpadding="0" cellspacing="0" class="user_table">
                <!--表格区域-->
                <tr>
                 <td style="border-bottom: none; _border-bottom: 0;" align="center">
                        <!-- DataGrid数据表格-->
                        <div id="Div1">
                            <table id="tt"></table>
                            <br />
                        </div>
                    </td>
                </tr>
            </table>

       <div id="dd" class="easyui-dialog" style="width: 500px; height:auto;" data-options="buttons:'#tb',modal:true,closed:true">
            <form id="ff" method="post">
                <table width="100%" cellpadding="0" cellspacing="0" style="margin-top: 0px" class="table">
                    <tr style="display: none;">
                        <td align="right"><label>角色编辑</label></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <input type="hidden" id="id" />
                            <input id="flowTypeId" name="flowTypeId" style="width: 260px; display: none" value="" />
                            <label>角色名称</label>
                        </td>
                        <td>
                            <input type="text" id="RoleName" class="textbox easyui-validatebox" style="width: 260px;" data-options="required:true" name="RoleName" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">角色类型</td>
                        <td>
                        <input type="text" id="RoleType" name="RoleType" style="width: 260px;" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">是否属于系统</td>
                        <td>
                            <input id="Radio1" name="IsSystem" type="radio" value="1" /><label for="Radio1">是</label>
                            <input id="Radio2" name="IsSystem" type="radio" value="0" /><label for="Radio2">否</label>
                         </td>
                    </tr>
                    <tr>
                        <td align="right">是否启用</td>
                        <td>
                            <input id="Radio3" name="IsWorking" type="radio" value="1" /><label for="Radio1">是</label>
                            <input id="Radio4" name="IsWorking" type="radio" value="0" /><label for="Radio2">否</label>
                        </td>
                    </tr>
                </table>
            </form>
        </div>
        <div id="tb">
            <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-save'" onclick="Save($('#id').val())">保存</a><a href="#" class="easyui-linkbutton" onclick="$('#dd').dialog('close');">取消</a>
        </div>
    </div>

    <div id="Permission" class="easyui-dialog" style="width: 500px; height: auto;" data-options="buttons:'#pb',modal:true,closed:true">
    <div class="easyui-panel" style="padding:5px">
       <input type="hidden" id="Rid" />
        <ul id="rtt" class="easyui-tree" ></ul>
    </div>
        <div id="pb">
            <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-save'" onclick="RoleSave($('#Rid').val())">保存</a><a href="#" class="easyui-linkbutton" onclick="$('#Permission').dialog('close');">取消</a>
        </div>
    </div>

</body>
</html>
