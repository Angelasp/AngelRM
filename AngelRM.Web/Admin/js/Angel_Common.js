$(function () { easyloader.locale = 'zh_CN'; });

//刷新列表数据
function refreshList() {
    parent.selectTabByTitle(listTitle);    //打开一览
    parent.reloadTabByFrameId(FrameId);    //加载数据
    parent.closeTabByTitle(formTitle);     //关闭本页Detail
}
//取消按钮,关闭当前表单

function btnCancel_Click() {
    parent.closeTabByTitle(formTitle);      //关闭表单
}

//设置Form控件的值
function setTextValue(textBox_id, textBox_value) {
    var txtBox_ID = document.getElementById(textBox_id);
    txtBox_ID.value = textBox_value;
}
//日历控件格式化
function dateFormatter(date) {
    if (date.length < 8) return;
    var y = date.getFullYear();
    var m = date.getMonth() + 1;
    var d = date.getDate();
    //return y + '-' + (m < 10 ? ('0' + m) : m) + '-' + (d < 10 ? ('0' + d) : d);
    return String(y) + String(m < 10 ? ('0' + m) : m) + String(d < 10 ? ('0' + d) : d);
}
//日历解析
function dateParser(s) {
    if (s.length < 8) return;
    if (!s) return new Date();
    var y = s.substring(0, 4);
    var m = s.substring(4, 6);
    var d = s.substring(6, 8);
    if (!isNaN(y) && !isNaN(m) && !isNaN(d) && y.length == 4) {
        return new Date(y, m - 1, d);
    } else {
        return new Date();
    }
}

//日历控件格式化
function dateFormatters(date) {
    if (date.length < 8) return;
    var y = date.getFullYear();
    var m = date.getMonth() + 1;
    var d = date.getDate();
    var hour = date.getHours();
    var minutes = date.getMinutes();
    var second = date.getSeconds();
    //return y + '-' + (m < 10 ? ('0' + m) : m) + '-' + (d < 10 ? ('0' + d) : d);
    return String(y) + String(m < 10 ? ('0' + m) : m) + String(d < 10 ? ('0' + d) : d) + String(hour < 10 ? ('0' + hour) : hour) + String(minutes < 10 ? ('0' + minutes) : minutes) + String(second < 10 ? ('0' + second) : second);
}
//日历解析
function dateParsers(s) {
    if (s.length < 8) return;
    if (!s) return new Date();
    var y = s.substring(0, 4);
    var m = s.substring(4, 6);
    var d = s.substring(6, 8);
    var hour = s.substring(8, 10);
    var minutes = s.substring(10, 12);
    var second = s.substring(12, 14);

    if (!isNaN(y) && !isNaN(m) && !isNaN(d) && y.length == 4) {
        return new Date(y, m - 1, d, hour, minutes, second);
    } else {
        return new Date();
    }
}
//
//打开明细窗口,显示headId对应的明细
//
function openDetailViewList() {
    //关闭旧明细列表
    //parent.closeTabByTitle(detailViewListTitle);
    //打开新明细列表
    parent.addTab(detailViewListTitle, detailViewListFrameId, detailViewListUrl);
    //设置条件
    conditionStr = "1=1";

    setDetailViewListCondition(conditionStr);
}
//设定明细列表的条件
function setDetailViewListCondition(conditionStr) {

    var detailViewListFrame = $("#" + detailViewListFrameId);
    if (detailViewListFrame && detailViewListFrame.length > 0) {
        //设置表格页面的条件变量
        detailViewListFrame[0].contentWindow.querryConditionStr = conditionStr;
        //重新载入数据
        detailViewListFrame[0].contentWindow.LoadAjaxData();
        //分页控件页码设为1
        detailViewListFrame[0].contentWindow.setPageNumber(1);
    }
}


//弹出提示消息
function messageBox(msg) {
    $(function () {
        easyloader.locale = 'zh_CN';
        easyloader.load('messager', function () {
            $.messager.confirm('系统提示', msg);
        });
    });
}

/*
获取Backspace按键事件
不跳转到上一个页面
*/
window.onload = function () {

    document.getElementsByTagName("body")[0].onkeydown = function () {

        //获取事件对象
        var elem = event.relatedTarget || event.srcElement || event.target || event.currentTarget;

        if (event.keyCode == 8) {//判断按键为backSpace键

            //获取按键按下时光标做指向的element
            var elem = event.srcElement || event.currentTarget;

            //判断是否需要阻止按下键盘的事件默认传递
            var name = elem.nodeName;

            if (name != 'INPUT' && name != 'TEXTAREA') {
                return _stopIt(event);
            }
            var type_e = elem.type.toUpperCase();
            if (name == 'INPUT' && (type_e != 'TEXT' && type_e != 'TEXTAREA' && type_e != 'PASSWORD' && type_e != 'FILE')) {
                return _stopIt(event);
            }
            if (name == 'INPUT' && (elem.readOnly == true || elem.disabled == true)) {
                return _stopIt(event);
            }
        }
    }
}
function _stopIt(e) {
    if (e.returnValue) {
        e.returnValue = false;
    }
    if (e.preventDefault) {
        e.preventDefault();
    }

    return false;
}
/*
获取Backspace按键事件
end
*/