<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="AngelRM.Web.Admin.MainFrame.Main" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" /> 
    <title>系统后台管理中心</title>
    <link href="../css/base.css" rel="stylesheet" type="text/css" />
    <link href="../js/easyui/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../js/easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../css/main.css" rel="stylesheet" type="text/css" />
    <link href="../css/mydialog_style.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../js/easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../js/easyui/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script src="../js/Angel_MainFrame.js" type="text/javascript"></script>
    <script src="../js/Angel_QuerryWindow.js" type="text/javascript"></script>
    <script src="../js/MyDialog/jquery.MyDialog.js" type="text/javascript"></script>
    <script src="../js/Angel_layout.js" type="text/javascript"></script>
    <link rel="shortcut icon" href="../images/angelasp.ico" type="image/x-icon" />
    <script type="text/javascript">
        //登录后处理
        $(function () {
            // 展开三级菜单
//            $('.menuDirectory').tree({
//                // expanded: 'li:first'     // 默认展开
//                expanded: ''                 // 默认收起
//            });
        });
        function outconfirm() {
            $.messager.confirm('确认', '退出系统?', function (r) {
                if (r) {
                    window.location.href = "../login.aspx";
                }
            });
        }

        //窗体加载打开个人桌面
        $(function () {
         //   addTab("个人桌面", "user_desk", "../UserDesk/UserDesk.aspx");
        });

        function linkNotice() {
            addTab('通知信息', 'NoticeList', '../Notice/NoticeViewList.aspx');
        }

        function userSetup() {
            //alert("调用成功!");
            $('#updateDiv').window('open');
            //$('#UpdateName').val(id);
            document.getElementById("UpdateName").innerHTML = s_UserID;
        }
        var xmlhttp;

        //创建异步对象
        function createXMLHttpRequest() {
            if (window.XMLHttpRequest) {
                xmlhttp = new XMLHttpRequest();
            } else {
                xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
            }
        }

        //修改密码
        function PwdUpdate() {
            //非空验证
            if ($("#oldPwd").val() == null || $("#oldPwd").val() == "")
            { $.messager.alert("提示", "请输入旧密码！"); return; }

            if ($("#newPwd").val() == null || $("#newPwd").val() == "")
            { $.messager.alert("提示", "请输入新密码！"); return; }

            if ($("#sureNewPwd").val() == null || $("#sureNewPwd").val() == "")
            { $.messager.alert("提示", "请再次输入新密码！"); return; }

            if ($("#sureNewPwd").val() != $("#newPwd").val())
            { $.messager.alert("提示", "两次密码输入不一致！"); return; }
            //获取用户输入文本框值
            var oldPwd = $("#oldPwd").val();
            var newPwd = $("#newPwd").val();
            var url = "../Admin/ashx/AdminService.ashx?action=UpPwd";
            $.ajax({
                type: 'post',
                url:url ,
                data: {'OldPasswrod': oldPwd, 'NewPassword': newPwd },
                cache: false,
                success: function (result) {
                    var data = eval('(' + result + ')');
                    if (data.success) {
                            $.messager.alert("提示", "密码修改成功！请牢记新密码！");
                            $("#updateDiv").window('close');
                    }
                    else
                            $.messager.alert("提示", "密码修改失败！请仔细核对！");
                }
            });

        }

        function timeprint() {
            var week; var date;
            var today = new Date();
            var year = today.getYear();
            var month = today.getMonth() + 1;
            var day = today.getDate();
            var ss = today.getDay();
            var hours = today.getHours();
            var minutes = today.getMinutes();
            var seconds = today.getSeconds();
            date = year + "年" + month + "月" + day + "日 ";
            if (ss == 0) week = "星期日";
            if (ss == 1) week = "星期一";
            if (ss == 1) week = "星期一";
            if (ss == 2) week = "星期二";
            if (ss == 3) week = "星期三";
            if (ss == 4) week = "星期四";
            if (ss == 5) week = "星期五";
            if (ss == 6) week = "星期六";
            if (minutes <= 9)
                minutes = "0" + minutes;
            if (seconds <= 9)
                seconds = "0" + seconds;
            myclock = date + week + "" + hours + ":" + minutes + ":" + seconds;
            if (document.layers) {
                document.layers.liveclock.document.write(myclock);
                document.layers.liveclock.document.close();
            } else{
                var liveclock=document.getElementById("liveclock"); 
                liveclock.innerHTML = myclock;
            }
            setTimeout("timeprint()", 1000);
        }
       



    </script>
</head>
<!-- 主框架-->
<body class="easyui-layout" id="MainFrame" style="overflow-y: hidden" scroll="no">
    <form id="form1" runat="server">
        <!-- Top标题区-->
        <div region="north" split="false" style="background: #0c6ec7 url('../images/top_banner.jpg') left no-repeat; height: 100px; overflow-y: hidden; position: relative;" ondblclick="closeTop()">
          <div style="margin-top:10px;color:White;text-align:right;padding-right:10px">今天是：<span class="heise" id="liveclock"></span><script type="text/javascript">timeprint();</script></div>
            <div class="top_banner_line">
                <ul>
                    <li><a title="退出" href="javascript:outconfirm();">
                        <img src="../images/quit.png" width="22" height="22" /></a></li>
                    <li style="margin-top: -5px"><a title="设定" href="#" id="mb" class="easyui-menubutton" data-options="menu:'#usermm',iconCls:'icon-user'"><img src="../images/setup.png" width="22" height="22" /></a></li>
                   
                </ul>

               <div id="usermm" style="width: 80px;">
                    <div data-options="iconCls:'icon-edit'" onclick="javascript:userSetup()"><a href="#" style="color: #000; text-decoration: none;" >修改密码</a></div>
                    <div data-options="iconCls:'icon-rainbow'" onclick="javascript:addTab('个人桌面','user_desk','../Navigation/NavigationList.aspx')"><a href="#" style="color: #000; text-decoration: none;">个人桌面</a></div>
                    <div data-options="iconCls:'icon-user_zuo'"  onclick="javascript:addTab('编辑桌面','deskNo_update','../UserDesk/deskNoUpdate.aspx')"><a href="#" style="color: #000; text-decoration: none;">编辑桌面</a></div>
                </div>

                <div class="cb"></div>
            </div>
        </div>

        <!-- Left导航栏-->
        <div region="west" split="ture" hide="true" title="导航菜单" style="width: 160px;">
            <div class="easyui-accordion" id="west_accordion" fit="true" border="false">
                <%=NavigationList%>
            </div>
        </div>
        <!-- Center工作区-->
        <div region="center" style="padding: 1px; background: #eee;">
            <div id="tabs" class="easyui-tabs" fit="true">
            </div>
        </div>
        <!-- Bottom底栏-->
        <div region="south" style="height: 30px; padding: 5px; background: #D2E0F2;">
            <div class="footer" align="left" style="height: 18px; line-height: 20px; font-size: 12px; overflow: hidden;">  版权所有 2015 <a href="http://www.angelasp.com">www.angelasp.com</a> <div style="width:160px;float:right;">Angel工作室信息管理系统</div></div>
        </div>
        <!--标签页标题栏，右键弹出菜单-->
        <div id="mm" class="easyui-menu" style="width: 150px;">
            <div id="mm-tabupdate">
                刷新
            </div>
            <div class="menu-sep">
            </div>
            <div id="mm-tabclose">
                关闭
            </div>
            <div id="mm-tabcloseall">
                全部关闭
            </div>
            <div id="mm-tabcloseother">
                除此之外全部关闭
            </div>
            <div class="menu-sep">
            </div>
            <div id="mm-tabcloseright">
                当前页右侧全部关闭
            </div>
            <div id="mm-tabcloseleft">
                当前页左侧全部关闭
            </div>
            <div class="menu-sep">
            </div>
            <div id="mm-exit">
                退出
            </div>
        </div>
        <!------查询窗口qw------>
        <div id="querryWindow" class="easyui-window" title="条件查询" closed="true" collapsible="false" minimizable="false" maximizable="false" iconCls="icon-search"
            style="width: 335px; height: 400px; padding: 5px; background: #fafafa;" >
            <div class="easyui-layout" fit="true">
                <div region="center" border="false" style="padding: 0px; background: #fff; border: 0px solid #ccc;">
                    <!--查询条件表qt-->
                    <table id="qt" style="width: 311px; height: 300px" singleselect="true" idfield="itemid">
                        <thead>
                            <tr>
                                <th field="idName" width="80" align="center">属性名称
                                </th>
                                <th field="idvalue" width="120" editor="text" halign="center" align="left">属 性 值
                                </th>
                            </tr>
                        </thead>
                    </table>
                    <!--end 查询条件表qt-->
                </div>
                <div region="south" border="false" style="text-align: center; height: 30px; line-height: 30px;">
                    <a id="btn_ok" class="easyui-linkbutton" icon="icon-ok" href="javascript:void(0)"
                        onclick="btn_okClick()">确 定</a> <a id="btn_cancel" class="easyui-linkbutto" />
                </div>
            </div>
        </div>



        <div id="updateDiv" class="easyui-window" style="width: 452px; height: 222px;" data-options="buttons:'#tb',modal:true,iconCls:'icon-edit'"
            closed="true" title="修改密码" resizable="false" minimizable="false" maximizable="false" collapsible="false">
            <table cellpadding="0" cellspacing="0" style="margin-top: 0px; height: 172px; width: 97%;" class="table">
                <tr>
                    <td align="right">
                        <label>用 户 名&nbsp;</label></td>
                    <td>
                        <span style="height: 20px; width: 200px; font-weight: bolder; font-size: inherit; color: red"><%=model.LoginName%></span>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                
                        <label>旧 密 码&nbsp;</label></td>
                    <td>
                        <input type="password" id="oldPwd" name="oldPwd" style="height: 20px; width: 200px" class="easyui-validatebox text" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <label>新 密 码&nbsp;</label>
                    </td>
                    <td>
                        <input type="password" id="newPwd" name="newPwd" style="height: 20px; width: 200px" class="easyui-validatebox text" /></td>
                </tr>
                <tr>
                    <td align="right" class="auto-style2">确认密码&nbsp;</td>
                    <td class="auto-style2">
                        <input type="password" id="sureNewPwd" name="sureNewPwd" style="height: 20px; width: 200px" class="easyui-validatebox text" />
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <a id="btn_savePwd" class="easyui-linkbutton" icon="icon-ok" onclick="javascript:PwdUpdate()">确 定</a>
                        <a href="#" class="easyui-linkbutton" onclick="$('#updateDiv').window('close');" icon="icon-no">取消</a>
                    </td>
                </tr>
            </table>
        </div>

    </form>
</body>
</html>
