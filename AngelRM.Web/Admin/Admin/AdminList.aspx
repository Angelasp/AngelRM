<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminList.aspx.cs" Inherits="AngelRM.Web.Admin.Admin.AdminList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>管理员管理</title>
    <link href="../css/base.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../css/main.css" />
    <link href="../js/easyui/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../js/easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <script src="../js/easyui/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="../js/easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../js/easyui/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/TableBasicNew.js"></script>
    <script type="text/javascript" src="../js/TableView.js"></script>
    <script type="text/javascript">
        //数据声明
        var pageTableName = "Angel_Admin";
        var pageModelClassName = "Angel_Admin";
        var pageTitle = "管理员管理";                         //本页标题
        var pageUrl = "../Admin/AdminList.aspx";             //本页Url
        var pageFrameId = "AdminList";                 //本页FrameId
        var FrameId = "AdminList";                        //工具栏查询按钮调用
        var formTitle = "编辑用户";                     //明细标题(双击行打开用)
        var formFrameId = "Admins";                      //明细FrameID
        var formUrl = "ashx/AdminService.ashx";        //明细Url
        var formTitled = "用户查看";                 //详细标题
        var formFrameIdd = "Adminsd";                 //详细FrameIDd
        var formUrld = "../Admin/AdminDetail.aspx";  //详细Url

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
                        { field: 'RoleId', title: '角色ID', align: 'left', halign: 'center', width: 80, editor: { type: 'validatebox', options: { required: true}} },
                        { field: 'LoginName', title: '登录名称', align: 'left', halign: 'center', width: 80, editor: 'text' },
                        { field: 'UserName', title: '用户名', align: 'left', headalign: 'center', width: 120, editor: 'text' },
                        { field: 'UserEmail', title: 'Email邮箱', align: 'center', halign: 'center', width: 150, editor: 'text' },
                        { field: 'IsWorking', title: '是否启用', align: 'center', headalign: 'center', width: 80, editor: 'text', formatter: function (value) { if (value == "1") { return "是"; } else { return "否"; } } },
                        { field: 'AddTime', title: '添加日期', align: 'left', headalign: 'center', width: 130, editor: 'text' },
                         //命令列
                        {
                        field: 'operation', title: '操 作', align: 'center', halign: 'center', width: 200, formatter: function (value, row, index) {
                            s = '<a href="#" onclick="showCurRecord(' + index + ')">查看</a> ';
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
                    $('#dd').dialog({ title: '编辑管理员', closed: false });
                    $('#id').val(raw.ID);
                    $('#RoleId').combobox('setValue', raw.RoleId);
                    $('#LoginName').val(raw.LoginName);
                    $('#UserName').val(raw.UserName);
                    $('#UserEmail').val(raw.UserEmail);
                    $("input[name='IsWorking'][value=" + raw.IsWorking + "]").attr("checked", true);
                }


                $(function () {

                    $('#RoleId').combobox({
                        url: "ashx/RoleService.ashx?action=List",
                        valueField: 'id',
                        textField: 'text',
                        panelHeight: 'auto'
                    });

                    $('#dd').dialog({
                        onClose: function () {
                            //解决弹出窗口关闭后，验证消息还显示在最上面
                            $('.validatebox-tip').remove();

                        }
                    });


                });






        
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
                            <table id="tt"></table>
                        </div>
                    </td>
                </tr>
            </table>

       <div id="dd" class="easyui-dialog" style="width: 500px; height: 390px; overflow: hidden;" data-options="buttons:'#tb',modal:true,closed:true">
            <form id="ff" method="post">
                <table width="100%" cellpadding="0" cellspacing="0" style="margin-top: 0px" class="table">
                    <tr style="display: none;">
                        <td align="right">
                            <label>栏目编辑</label></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td align="right">选择角色</td>
                        <td>
                        <input type="text" id="RoleId" name="RoleId" style="width: 260px;" />
                        </td>
                    </tr>

                    <tr>
                        <td align="right">
                            <input type="hidden" id="id" />
                            <input id="flowTypeId" name="flowTypeId" style="width: 260px; display: none" value="" />
                            <label>登录名</label>
                        </td>
                        <td>
                            <input type="text" id="LoginName" class="easyui-validatebox text" style="width: 260px;" data-options="required:true" name="LoginName" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">登录密码</td>
                        <td>
                            <input type="text" id="Password" name="Password" style="width: 260px;" class="text"/></td>
                    </tr>
                    <tr>
                        <td align="right">用户名</td>
                        <td>
                            <input type="text" id="UserName" name="UserName" style="width: 260px;" class="text" /></td>
                    </tr>
                    <tr>
                        <td align="right">电子邮件</td>
                        <td>
                            <input type="text" id="UserEmail" name="UserEmail" style="width: 260px;" class="text"/></td>
                    </tr>
                    <tr>
                        <td align="right">是否启用</td>
                        <td>
                            <input id="Radio1" name="IsWorking" type="radio" value="1" /><label for="Radio1">是</label>
                            <input id="Radio2" name="IsWorking" type="radio" value="0" /><label for="Radio2">否</label>
                        </td>
                    </tr>
                </table>
            </form>
        </div>
        <div id="tb">
            <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-save'" onclick="Save($('#id').val())">保存</a><a href="#" class="easyui-linkbutton" onclick="$('#dd').dialog('close');">取消</a>
        </div>
    </div>



</body>
</html>
