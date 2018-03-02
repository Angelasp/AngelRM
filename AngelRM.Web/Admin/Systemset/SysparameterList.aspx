<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysparameterList.aspx.cs" Inherits="AngelRM.Web.Admin.Systemset.SysparameterList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>系统参数设置</title>
    <link rel="stylesheet" type="text/css" href="../css/base.css">
    <link rel="stylesheet" type="text/css" href="../css/main.css">
    <link rel="stylesheet" type="text/css" href="../js/easyui/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../js/easyui/themes/icon.css" />
    <script type="text/javascript" src="../js/easyui/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../js/easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../js/easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../js/jquery.EasyInsert.js" ></script>
    <script type="text/javascript" src="js/TableBasicNew.js" ></script>
    <script type="text/javascript">
        //数据声明
        var pageTableName = "Angel_System_Parameter";
        var pageModelClassName = "Angel_System_Parameter";
        var listTitle = "系统参数信息";
        var formTitle = "系统参数列表";
        var formUrl = "SysparameterList.aspx";
        var listPageUrl = "../Systemset/SysparameterList.aspx";
        var pageTitle = "系统参数信息";                         //本页标题
        var FrameId = "SysparameterList";
        var querryConditionStr = "1=1";
        var str_ExportConditon = "";

        var SelectedIds = [];
        var editing = false;
        var pNumber = 1;
        //页面一览表的列标题
        var Columns = {
            columns: [[
                        { field: 'id', title: '记录代码', align: 'left', headalign: 'center', width: 80, editor: 'text', hidden: true },
                        { field: 'ParaID', title: '参数代码', align: 'center', headalign: 'center', width: 120, editor: 'text' },
                        { field: 'ParaName', title: '参数名称', align: 'center', headalign: 'center', width: 80, editor: 'text' },
                        { field: 'Data', title: '参数内容', align: 'left', headalign: 'center', width: 320, editor: 'text' },
            //{ field: 'IsView', title: '允许编辑', align: 'center', headalign: 'center', width: 80, editor: 'text' },
            //命令列
                        {
                        field: 'operation', title: '操 作', align: 'center', halign: 'center', width: 110, formatter: function (value, row, index) {
                            var s = '<a href="#" onclick="viewCurRecord(' + index + ')">编辑</a> ';
                            var d = '<a href="#" onclick="deleteCurRow(' + index + ')">删除</a> ';
                            if (row.IsView == '0') {
                                return '';
                            }
                            else
                                return s + d;
                        }
                    }//命令列end 

            ]]
        };
        function GetValue() {
            var v = [];
            $("#Data").find('input').each(function () {
                if ($.trim($(this).val()) != null && $.trim($(this).val()) != '')
                    v.push($.trim($(this).val()));
            });
            $('#hidData').val(v.toString());
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
            color: #07F;
            text-decoration: none;
        }

            a:hover {
                text-decoration: underline;
            }
    </style>
</head>
<body>
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
        <div id="dd" class="easyui-dialog" style="width: 500px;  overflow: hidden;" data-options="buttons:'#tb',modal:true,closed:true">
            <form id="ff" method="post">
                <table width="100%" cellpadding="0" cellspacing="0" style="margin-top: 0px" class="table">
                    <tr>
                        <td align="right">
                            <input type="hidden" id="id" />
                            <label>参数代码</label></td>
                        <td>
                            <input id="ParaID" name="ParaID" style="width: 200px;" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <label>参数名称</label></td>
                        <td>
                            <input type="text" id="ParaName" name="ParaName" /></td>
                    </tr>
                    <tr>
                        <td align="right">参数内容</td>
                        <td>
                            <div id="divShow" style="height: 140px; overflow-y: auto">
                                <!-- <ul id="Data"></ul>
                                <a href="#">+ 添加</a>-->
                            </div>
                            <input type="hidden" id="hidData" name="hidData" />
                        </td>
                    </tr>
                </table>
            </form>
        </div>
        <div id="tb">
            <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-save'" onclick="GetValue();Save($('#id').val())">保存</a>
            <a href="#" class="easyui-linkbutton" onclick="$('#dd').dialog('close');">取消</a>
        </div>
    </div>
</body>
</html>
