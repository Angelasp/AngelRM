<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NavigationList.aspx.cs" Inherits="AngelRM.Web.Admin.Navigation.NavigationList" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>后台导航列表</title>
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
     var pageTableName = "Angel_Admin_Navigation";
     var pageModelClassName = "Angel_Admin_Navigation";
     var listTitle = "栏目菜单信息";
     var formTitle = "节点类型列表";
     var formUrl = "NavigationList.aspx";
     var listPageUrl = "../Navigation/NavigationList.aspx";

     var pageTitle = "导航列表";                         //本页标题
     var FrameId = "NavigationList";
     var querryConditionStr = "1=1 ";
     var str_ExportConditon = "";

     var SelectedIds = [];
     var editing = false;
     var pNumber = 1;
     //页面一览表的列标题
     var Columns = {
         columns: [[
                        { field: 'ID', title: 'ID编号', align: 'left', headalign: 'center', width: 60, editor: 'text' },
                        { field: 'NavName', title: '栏目代码', align: 'center', headalign: 'center', width: 80, editor: 'text' },
                        { field: 'TitleName', title: '栏目名称', align: 'center', headalign: 'center', width: 80, editor: 'text' },
                        { field: 'NavUrl', title: '栏目连接', align: 'center', headalign: 'center', width: 80, editor: 'text' },
                        { field: 'Remark', title: '栏目说明', align: 'left', headalign: 'center', width: 120, editor: 'text' },
                        { field: 'ParentID', title: '栏目父类', align: 'left', headalign: 'center', width: 80, editor: 'text' },
//                        { field: 'SortPath', title: '栏目类别', align: 'left', headalign: 'center', width: 80, editor: 'text' },
                        { field: 'Sequence', title: '排序号', align: 'left', headalign: 'center', width: 50, editor: 'text' },
//                        { field: 'ChannelId', title: '分类ID', align: 'center', headalign: 'center', width: 80, editor: 'text' },
                        { field: 'Operation_Value', title: '操作功能', align: 'center', headalign: 'center', width: 200, editor: 'text' },
                        { field: 'ViewFlag', title: '是否启用', align: 'center', headalign: 'center', width: 80, formatter: function (value) { if (value == "1") { return "是"; } else { return "否"; } } },
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
       //  $('#ParentID').val(raw.ParentID);
         $("#ParentID").combotree('setValue', raw.ParentID);

             $.ajax({
                 type: 'get',
                 url: 'ashx/NavigationService.ashx?action=OperationData',
                 cache: false,
                 success: function (result) {
                     var data = eval('(' + result + ')');
                     $('#Operation_Value').combotree('loadData', data);
                 }
             });

             var ovalues = raw.Operation_Value.split(',');
             $('#Operation_Value').combotree('setValues', ovalues);
//         $('#UrlId').combogrid('grid').datagrid('selectRecord', raw.UrlId);
//         $('#termUrlId').combogrid('grid').datagrid('selectRecord', raw.termUrlId);
//         //   $('#flowTypeId').combogrid('grid').datagrid('selectRecord', raw.flowTypeId);
         $('#Remark').val(raw.Remark);
         $('#Sequence').val(raw.Sequence);
         $("input:radio[value=" + raw.ViewFlag + "]").attr('checked', 'true');
     }


     $(function () {

         $('#ParentID').combotree({
             url: "ashx/NavigationService.ashx?action=NavList",
             method: 'get',
             valueField:'id',
             textField: 'text'
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

        <div id="dd" class="easyui-dialog" style="width: 500px; height:410px;overflow: hidden;" data-options="buttons:'#tb',modal:true,closed:true">
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
                            <label>栏目代码</label>
                        </td>
                        <td>
                            <input type="text" id="NavName" class="textbox easyui-validatebox text" style="width: 260px;" data-options="required:true" name="NavName" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">栏目名称</td>
                        <td>
                            <input type="text" id="TitleName" name="TitleName" style="width: 260px;" class="text" /></td>
                    </tr>
                    <tr>
                        <td align="right">栏目链接</td>
                        <td>
                            <input type="text" id="NavUrl" name="NavUrl" style="width: 260px;"  class="text"/></td>
                    </tr>
                    <tr>
                        <td align="right">栏目父类</td>
                        <td>
                            <input type="text" id="ParentID" name="ParentID" style="width: 260px;" /></td>
                    </tr>


                    <tr>
                        <td align="right">栏目排序号</td>
                        <td>
                            <input type="text" id="Sequence" data-options="required:true" class="textbox easyui-validatebox text"    style="width: 160px;" name="Sequence"  /><font color="red">请输入整数</font></td>
                    </tr>

                    <tr>
                        <td align="right">操作列表</td>
                        <td>
                            <select id="Operation_Value" name="Operation_Value" class="easyui-combotree text" style="width: 200px" data-options="panelHeight:'auto',multiple: true,editable: false"></select>
                        </td>
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
