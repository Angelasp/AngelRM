<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AngelRM.Web.Admin.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Angel工作室通用权限管理系统后台登录</title>
    <link rel="shortcut icon" href="images/angelasp.ico" type="image/x-icon" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="js/Angel_login_cloud.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(function () {
            $('.loginbox').css({ 'position': 'absolute', 'left': ($(window).width() - 692) / 2 });
            $(window).resize(function () {
                $('.loginbox').css({ 'position': 'absolute', 'left': ($(window).width() - 692) / 2 });
            })

        });    
    </script>
</head>
<body style="background-color: #1c77ac; background-image: url(images/light.png);
    background-repeat: no-repeat; background-position: center top; overflow: hidden;">
    <form id="form1" runat="server">
    <div id="mainBody">
        <div id="cloud1" class="cloud">
        </div>
        <div id="cloud2" class="cloud">
        </div>
    </div>
    <div class="logintop">
        <span>欢迎登录Angel工作室通用权限管理系统</span>
        <ul>
            <li><a href="http://www.angelasp.com/">官方首页</a></li>
            <li><a href="http://www.angelasp.com/Contact.asp">帮助</a></li>
            <li><a href="http://www.angelasp.com/About.asp">关于</a></li>
        </ul>
    </div>
    <div class="loginbody">
        <span class="systemlogo"></span>
        <div class="loginbox">
            <ul>
                <li>
                    <asp:TextBox ID="txtUserName" runat="server" onclick="JavaScript:this.value=''" CssClass="loginuser"
                        placeholder="用户名" title="用户名"></asp:TextBox>
                </li>
                <li>
                    <asp:TextBox ID="txtPassword" runat="server" onclick="JavaScript:this.value=''" CssClass="loginpwd"
                        TextMode="Password" placeholder="密码" title="密码"></asp:TextBox>
                    <label class="login-field-icon pwd" for="txtPassword">
                    </label>
                    <span class="login-tips"><i></i><b id="msgtip" runat="server">请输入用户名和密码</b></span>
                </li>
                <li>
                    <asp:Button ID="btnSubmit" runat="server" Text="登 录" CssClass="loginbtn" OnClick="btnSubmit_Click" />
                    <label>
                    <asp:CheckBox ID="Issavepwd" runat="server" Checked="true" Text="记住用户名" />
                    </label>
                    <label>
                        <a href="http://www.angelasp.com">忘记密码？</a></label>
                </li>
            </ul>
        </div>
    </div>
    <div class="loginbm">
        版权所有 2014 <a href="http://www.angelasp.com">www.angelasp.com</a> Angel工作室后台管理</div>
    </form>
</body>
</html>
