<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogList.aspx.cs" Inherits="AngelRM.Web.Admin.Log.LogList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>后台日志列表</title>
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
     var pageTableName = "Angel_Admin_Log";
     var pageModelClassName = "Angel_Admin_log";
     var listTitle = "日志列表信息";
     var formTitle = "日志列表信息";
     var formUrl = "LogList.aspx";
     var listPageUrl = "../Log/LogList.aspx";

     var pageTitle = "日志列表信息";                         //本页标题
     var FrameId = "LogList";
     var querryConditionStr = "1=1 ";
     var str_ExportConditon = "";

     var SelectedIds = [];
     var editing = false;
     var pNumber = 1;
     //页面一览表的列标题
     var Columns = {
         columns: [[
                        { field: 'ID', title: 'ID编号', align: 'left', headalign: 'center', width: 80, editor: 'text' },
                        { field: 'AdminId', title: '管理员编号', align: 'center', headalign: 'center', width: 80, editor: 'text' },
                        { field: 'AdminName', title: '管理员名', align: 'center', headalign: 'center', width: 80, editor: 'text' },
                        { field: 'OperateIP', title: '登录IP', align: 'center', headalign: 'center', width: 80, editor: 'text' },
                        { field: 'Operate_Value', title: '操作代码', align: 'left', headalign: 'center', width: 120, editor: 'text' },
                        { field: 'Explain', title: '日志说明', align: 'left', headalign: 'center', width: 120, editor: 'text' },
                        { field: 'AddTime', title: '添加时间', align: 'center', headalign: 'center', width: 120, editor: 'text' },
                         //命令列
                        {
                        field: 'operation', title: '操 作', align: 'center', halign: 'center', width: 160, formatter: function (value, row, index) {
                            var e = '<a href="#" onclick="viewCurRecord(' + index + ')">编辑</a> ';
                            var f = ' <a href="#" onclick="deleteCurRow(' + index + ')">删除</a>';
                            return e + f;
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
         $('#dd').dialog({ title: '编辑后台栏目', closed: false });
         $('#id').val(raw.ID);
         $('#NavName').val(raw.NavName);
         $('#TitleName').val(raw.TitleName);
         $('#NavUrl').val(raw.NavUrl);
         $('#ParentID').val(raw.ParentID);
//         $('#UrlId').combogrid('grid').datagrid('selectRecord', raw.UrlId);
//         $('#termUrlId').combogrid('grid').datagrid('selectRecord', raw.termUrlId);
//         //   $('#flowTypeId').combogrid('grid').datagrid('selectRecord', raw.flowTypeId);
         $('#Remark').val(raw.Remark);
         $('#Sequence').val(raw.Sequence);
         $("input:radio[value=" + raw.ViewFlag + "]").attr('checked', 'true');
     }


     $(function () {
         $("#navnation").css("width", $("body").width() - 20);
         $('#UrlId').combogrid({
             url: 'NodeService.ashx?action=List&tableName=t_nodeurl&Type=pc',
             panelWidth: 500,
             panelHeight: 200,
             idField: 'id',
             textField: 'nodeUrlName',
             //pagination: true,
             fitColumns: true,
             required: true,
             rownumbers: true,
             editable: true,
             mode: 'remote',
             delay: 500,
             sortName: 'id',
             sortOrder: 'asc',
             pageSize: 10,
             pageList: [10, 5, 3],
             columns: [[
                    {
                        field: 'id',
                        title: '模块ID',
                        hidden: true
                    },
                    {
                        field: 'nodeUrlName',
                        title: '模块名称',
                        width: 100,
                        sortable: true
                    }, {
                        field: 'Url',
                        title: '路径',
                        width: 100,
                        sortable: true
                    }]]
         });


         $('#termUrlId').combogrid({
             url: 'NodeService.ashx?action=List&tableName=t_nodeurl&Type=term',
             panelWidth: 500,
             panelHeight: 200,
             idField: 'id',
             textField: 'nodeUrlName',
             //pagination: true,
             fitColumns: true,
             required: true,
             rownumbers: true,
             editable: true,
             mode: 'remote',
             delay: 500,
             sortName: 'id',
             sortOrder: 'asc',
             pageSize: 10,
             pageList: [10, 5, 3],
             columns: [[
                    {
                        field: 'id',
                        title: '模块ID',
                        hidden: true
                    },
                    {
                        field: 'nodeUrlName',
                        title: '模块名称',
                        width: 100,
                        sortable: true
                    }, {
                        field: 'Url',
                        title: '路径',
                        width: 100,
                        sortable: true
                    }]]
         });

         $('#dd').dialog({
             onClose: function () {
                 //解决弹出窗口关闭后，验证消息还显示在最上面
                 $('.validatebox-tip').remove();

             }
         });

     });

     //
     function RoleCurRecord(index) {
         parent.addTab("节点角色信息", "NodeRoleList", "../Node/NodeRoleList.aspx?nodeTypeId=" + index);    //打开节点角色列表
     }
     //
     function ActionCurRow(index) {
         parent.addTab("活动信息", "ActionTypeList", "../Flow/ActionTypeList.aspx?nodeTypeId=" + index);    //打开活动列表
     }

     //刷新列表
     function refreshList() {
         parent.selectTabByTitle("流程定义");
         parent.reloadTabByFrameId("FlowTypeList", "流程定义");
         parent.closeTabByTitle(pageTitle);
     }

    </script>
    <style type="text/css">
        .table td {
            border: 1px solid #dddddd;
            padding: 5px 5px;
        }

        .table input {
            border: 1px solid #dddddd;
        }

        .table textarea {
            border: 1px solid #dddddd;
        }

        .panel-tool {
            height: 22px;
            margin-top: -11px;
        }

            .panel-tool a {
                margin-top: 3px;
            }

        ul {
            margin: 0;
            padding: 0;
            list-style: none;
        }

        input {
            border: 1px solid #ccc;
            margin: 2px;
            height: 20px;
            line-height: 20px;
        }

        a {
            margin-left: 5px;
        }

        .title_jd {
            background: url(../images/title_jd_bg.jpg) repeat-x;
            height: 32px;
            line-height: 32px;
            margin: 0px 10px;
            border: solid 1px #95b8e7;
            border-bottom: 0;
            padding-left: 10px;
        }

            .title_jd a {
                color: #000;
                text-decoration: none;
            }

                .title_jd a:hover {
                    color: #F00;
                    text-decoration: underline;
                }

            .title_jd span {
                padding: 0px 5px;
            }
    </style>
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
                        <td align="right">
                            <input type="hidden" id="id" />
                            <input id="flowTypeId" name="flowTypeId" style="width: 260px; display: none" value="" />
                            <label>栏目代码</label>
                        </td>
                        <td>
                            <input type="text" id="NavName" class="textbox easyui-validatebox" style="width: 260px;" data-options="required:true" name="NavName" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">栏目名称</td>
                        <td>
                            <input type="text" id="TitleName" name="TitleName" style="width: 260px;" /></td>
                    </tr>
                    <tr>
                        <td align="right">栏目链接</td>
                        <td>
                            <input type="text" id="NavUrl" name="NavUrl" style="width: 260px;" /></td>
                    </tr>
                    <tr>
                        <td align="right">栏目节点</td>
                        <td>
                            <input type="text" id="ParentID" name="ParentID" style="width: 260px;" /></td>
                    </tr>


                    <tr>
                        <td align="right">栏目排序号</td>
                        <td>
                            <input type="text" id="Sequence" data-options="required:true" class="textbox easyui-validatebox"    style="width: 160px;" name="Sequence"  /><font color="red">请输入整数</font></td>
                    </tr>

                    <tr>
                        <td align="right">是否启用</td>
                        <td>
                            <input id="Radio1" name="ViewFlag" type="radio" value="1" /><label for="Radio1">是</label>
                            <input id="Radio2" name="ViewFlag" type="radio" value="0" /><label for="Radio2">否</label>
                        </td>
                    </tr>

                    <tr>
                        <td align="right">栏目说明</td>
                        <td>
                            <textarea id="Remark" class="textbox" name="Remark" cols="20" rows="6" onKeyUp="if (this.value.length>=210){event.returnValue=false}"  onKeyDown="if (this.value.length>=210){event.returnValue=false}" style="width: 260px;"></textarea>
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
