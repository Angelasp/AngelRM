<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Systeminfo.aspx.cs" Inherits="AngelRM.Web.Admin.Systemset.Systeminfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>系统配置信息</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" /> 
    <link rel="stylesheet" type="text/css" href="../css/base.css">
    <link rel="stylesheet" type="text/css" href="../css/main.css">
    <link rel="stylesheet" type="text/css" href="../js/easyui/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../js/easyui/themes/icon.css" />
    <script type="text/javascript" src="../js/easyui/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../js/easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../js/easyui/locale/easyui-lang-zh_CN.js"></script>
</head>

<body>
    <form id="form1" method="post">
    <div class="easyui-tabs" data-options="tabWidth:100,tabHeight:160" style="width: 700px; height: auto;margin:10px">
        <div title="基本信息设置" style="padding: 10px">
            <!--系统基本信息-->
            <div class="tab-content">
                <table cellpadding="0" cellspacing="0" style="margin-top: 10px" width="100%" class="form_table table">
                    <tr>
                        <td align="right">
                            系统名称
                        </td>
                        <td>
                            <input class="textbox easyui-validatebox text inormal" type="text" name="Websitename"
                                id="Websitename" data-options="required:true" /><span>*任意字符，控制在255个字符内</span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            系统域名
                        </td>
                        <td>
                            <input class="easyui-validatebox  text inormal" type="text" name="Websiteurl" id="Websiteurl"
                                data-options="required:true" /><span></span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            系统LOGO
                        </td>
                        <td>
                            <input class="easyui-validatebox  text inormal" type="text" name="Websitelogo" id="Websitelogo"
                                data-options="required:true" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            公司名称
                        </td>
                        <td>
                            <input class="easyui-validatebox  text inormal" type="text" name="Websitecompany"
                                id="Websitecompany" data-options="required:true" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            通讯地址
                        </td>
                        <td>
                            <input class="easyui-validatebox  text inormal" type="text" name="Websiteaddress"
                                id="Websiteaddress" data-options="required:true" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            联系电话
                        </td>
                        <td>
                            <input class="easyui-validatebox  text inormal" type="text" name="Websitetel" id="Websitetel"
                                data-options="required:true" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            传真号码
                        </td>
                        <td>
                            <input class="easyui-validatebox  text inormal" type="text" name="Websitefax" id="Websitefax"
                                data-options="required:true" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            管理员邮箱
                        </td>
                        <td>
                            <input class="easyui-validatebox  text inormal" type="text" name="Websitemail" id="Websitemail"
                                data-options="required:true" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            系统标题
                        </td>
                        <td>
                            <input class="easyui-validatebox  text inormal" type="text" name="Websitetitle" id="Websitetitle"
                                data-options="required:true" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            系统描述
                        </td>
                        <td>
                            <input class="easyui-validatebox  text inormal" type="text" name="Websitedescription"
                                id="Websitedescription" data-options="multiline:true" style="height: 60px" data-options="required:true" /><span>系统描述</span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            系统版权信息
                        </td>
                        <td>
                            <input class="easyui-validatebox text inormal" type="text" name="Websitecopyright"
                                id="Websitecopyright" data-options="multiline:true" style="height: 60px" data-options="required:true" /><span>支持HTML</span>
                        </td>
                    </tr>
                </table>
            </div>
            <!--/网站基本信息-->
        </div>
        <div title="系统设置" style="padding: 10px">
            <!--功能权限设置-->
            <div class="tab-content">
                <table cellpadding="0" cellspacing="0" style="margin-top: 10px" width="100%" class="user_table table">
                    <tr>
                        <td align="right">
                            系统安装目录
                        </td>
                        <td>
                            <input class="easyui-validatebox  text inormal" type="text" name="Websitepath" id="Websitepath" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            系统目录
                        </td>
                        <td>
                            <input class="easyui-validatebox  text inormal" type="text" name="Websiteadminpath"
                                id="Websiteadminpath" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            开启管理日志
                        </td>
                        <td>
                            <div class="rule-single-checkbox">
                                <select class="easyui-combobox" panelheight="auto" name="Islogstatus">
                                    <option value="1">是</option>
                                    <option value="0">否</option>
                                </select>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            是否开启
                        </td>
                        <td>
                            <select class="easyui-combobox" panelheight="auto" name="Websitestatus">
                                <option value="1">是</option>
                                <option value="0">否</option>
                            </select><span>*关闭后系统将不能访问</span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            系统关闭原因
                        </td>
                        <td>
                            <input class="easyui-validatebox  text inormal" type="text" name="Websiteclosereason"
                                id="Websiteclosereason" id="Websitecopyright" data-options="multiline:true" style="height: 60px" /><span>支持HTML</span>
                        </td>
                    </tr>
                </table>
            </div>
            <!--/功能权限设置-->
        </div>
    </div>
    <div style="width: 700px; text-align: center; padding: 5px">
        <a href="javascript:void(0)" class="easyui-linkbutton" onclick="submitForm()">提 交</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" onclick="clearForm()">重 置</a>
    </div>
    <style scoped="scoped">
        .tt-inner
        {
            display: inline-block;
            line-height: 12px;
            padding-top: 5px;
        }
        .tt-inner img
        {
            border: 0;
        }
    </style>
    </form>
    <script type="text/javascript">
        $(function () {
            $('#form1').form('load', 'ashx/SystemInfo.ashx?action=fromJson');
        });
        function submitForm() {
            $.messager.confirm('确认', '你确定要修改？', function (r) {
                if (r) {
                    $('#form1').form('submit', {
                        url: "ashx/SystemInfo.ashx?action=Savewebsiteinfo",
                        type: 'post',
                        onSubimt: function () {
                            var isvaluenonull = $(this).form('validate');
                            if (isvaluenonull) {
                                $.messager.alert('操作提示', '信息填写不完整！', 'error');
                                return false;
                            }
                        },
                        success: function (data) {
                            if (data) {
                                var result = eval("(" + data + ")");
                                if (result.success) {
                                    $.messager.alert('操作提示', '修改信息成功！', 'info');
                                }
                            } else {
                                $.messager.alert('操作提示', '修改信息失败！', 'error');
                                return false;
                            }
                        }
                    });
                }
            });

        }
        function clearForm() {
            $.messager.confirm('确认', '你确定要清空在修改,对信息比较熟悉的情况下进行？', function (r) {
                if (r) {
                    $('#form1').form('clear');
                }
            });
        }
    </script>
</body>
</html>
