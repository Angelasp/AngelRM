var Sys = {};
var ua = navigator.userAgent.toLowerCase();
var s;
(s = ua.match(/msie ([\d.]+)/)) ? Sys.ie = s[1] :
(s = ua.match(/firefox\/([\d.]+)/)) ? Sys.firefox = s[1] :
(s = ua.match(/chrome\/([\d.]+)/)) ? Sys.chrome = s[1] :
(s = ua.match(/opera.([\d.]+)/)) ? Sys.opera = s[1] :
(s = ua.match(/version\/([\d.]+).*safari/)) ? Sys.safari = s[1] : 0;

//以下进行测试
//if (Sys.ie) document.write('IE: ' + Sys.ie);
//if (Sys.firefox) document.write('Firefox: ' + Sys.firefox);
//if (Sys.chrome) document.write('Chrome: ' + Sys.chrome);
//if (Sys.opera) document.write('Opera: ' + Sys.opera);
//if (Sys.safari) document.write('Safari: ' + Sys.safari);

if (Sys.ie) {
    switch (Sys.ie) {
        case "7.0":
            document.write('<script src="../easyui/jquery-easyui-1.3.2/jquery-1.8.0.min.js"></script>');
            document.write('<script src="../easyui/jquery-easyui-1.3.2/jquery.easyui.min.js"></script>');
            document.write('<script src="../easyui/jquery-easyui-1.3.2/easyloader.js"></script>');
            document.write('<script src="../easyui/jquery-easyui-1.3.2/locale/easyui-lang-zh_CN.js"></script>');
            document.write('<link href="../easyui/jquery-easyui-1.3.2/themes/default/easyui.css" rel="stylesheet" />');
            document.write('<link href="../easyui/jquery-easyui-1.3.2/themes/icon.css" rel="stylesheet" />');
            break;
        case "8.0":
            document.write('<script src="../easyui/jquery-easyui-1.3.2/jquery-1.8.0.min.js"></script>');
            document.write('<script src="../easyui/jquery-easyui-1.3.2/jquery.easyui.min.js"></script>');
            document.write('<script src="../easyui/jquery-easyui-1.3.2/easyloader.js"></script>');
            document.write('<script src="../easyui/jquery-easyui-1.3.2/locale/easyui-lang-zh_CN.js"></script>');
            document.write('<link href="../easyui/jquery-easyui-1.3.2/themes/default/easyui.css" rel="stylesheet" />');
            document.write('<link href="../easyui/jquery-easyui-1.3.2/themes/icon.css" rel="stylesheet" />');
            break;
        case "9.0":
            document.write('<script src="../easyui/jquery-easyui-1.3.2/jquery-1.8.0.min.js"></script>');
            document.write('<script src="../easyui/jquery-easyui-1.3.2/jquery.easyui.min.js"></script>');
            document.write('<script src="../easyui/jquery-easyui-1.3.2/easyloader.js"></script>');
            document.write('<script src="../easyui/jquery-easyui-1.3.2/locale/easyui-lang-zh_CN.js"></script>');
            document.write('<link href="../easyui/jquery-easyui-1.3.2/themes/default/easyui.css" rel="stylesheet" />');
            document.write('<link href="../easyui/jquery-easyui-1.3.2/themes/icon.css" rel="stylesheet" />');
            break;
        case "10.0": 
            document.write('<script src="../easyui/jquery-easyui-1.3.5/jquery.min.js"></script>');
            document.write('<script src="../easyui/jquery-easyui-1.3.5/jquery.easyui.min.js"></script>');
            document.write('<script src="../easyui/jquery-easyui-1.3.5/easyloader.js"></script>');
            document.write('<script src="../easyui/jquery-easyui-1.3.5/locale/easyui-lang-zh_CN.js"></script>');
            document.write('<link href="../easyui/jquery-easyui-1.3.5/themes/default/easyui.css" rel="stylesheet" />');
            document.write('<link href="../easyui/jquery-easyui-1.3.5/themes/icon.css" rel="stylesheet" />');
            break;
    }
}
if (Sys.firefox) {
    document.write('<script src="../easyui/jquery-easyui-1.3.5/jquery.min.js"></script>');
    document.write('<script src="../easyui/jquery-easyui-1.3.5/jquery.easyui.min.js"></script>');
    document.write('<script src="../easyui/jquery-easyui-1.3.5/easyloader.js"></script>');
    document.write('<link href="../easyui/jquery-easyui-1.3.5/themes/default/easyui.css" rel="stylesheet" />');
    document.write('<link href="../easyui/jquery-easyui-1.3.5/themes/icon.css" rel="stylesheet" />');
}
if (Sys.chrome) {
    document.write('<script src="../easyui/jquery-easyui-1.3.5/jquery.min.js"></script>');
    document.write('<script src="../easyui/jquery-easyui-1.3.5/jquery.easyui.min.js"></script>');
    document.write('<script src="../easyui/jquery-easyui-1.3.5/easyloader.js"></script>');
    document.write('<link href="../easyui/jquery-easyui-1.3.5/themes/default/easyui.css" rel="stylesheet" />');
    document.write('<link href="../easyui/jquery-easyui-1.3.5/themes/icon.css" rel="stylesheet" />');
}
if (Sys.opera) {
    document.write('<script src="../easyui/jquery-easyui-1.3.5/jquery.min.js"></script>');
    document.write('<script src="../easyui/jquery-easyui-1.3.5/jquery.easyui.min.js"></script>');
    document.write('<script src="../easyui/jquery-easyui-1.3.5/easyloader.js"></script>');
    document.write('<link href="../easyui/jquery-easyui-1.3.5/themes/default/easyui.css" rel="stylesheet" />');
    document.write('<link href="../easyui/jquery-easyui-1.3.5/themes/icon.css" rel="stylesheet" />');
}
if (Sys.safari) {
    document.write('<script src="../easyui/jquery-easyui-1.3.5/jquery.min.js"></script>');
    document.write('<script src="../easyui/jquery-easyui-1.3.5/jquery.easyui.min.js"></script>');
    document.write('<script src="../easyui/jquery-easyui-1.3.5/easyloader.js"></script>');
    document.write('<link href="../easyui/jquery-easyui-1.3.5/themes/default/easyui.css" rel="stylesheet" />');
    document.write('<link href="../easyui/jquery-easyui-1.3.5/themes/icon.css" rel="stylesheet" />');
}


