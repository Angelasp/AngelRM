/////////////////////////////////////////////////////////////////////////
//  多条件查询窗口
//  Angel工作室
//  Author:QQ:815657032 709047174 
//  Copyright (c) 2009-2015 angelasp.com
//  For further information go to http://www.angelasp.com
//  1、在主框架页面中设置，供所有标签页调用
//  2、读取标签页中列表的Columns值，载入到查询窗口中
//  3、将输入的条件作为参数发送到标签页对应的action页面
//  4、刷新列表数据
//////////////////////////////////////////////////////////////////////////
//
//定义局部变量
//
var lastIndex = 0;
var qwOption = {}; //参数对象
qwOption.tabTitle = ""; //发出查询请求的Tab标题
qwOption.tabFramId = ""; //发出查询请求的Tab Frame Id
qwOption.tabUrl = ""; //action Url
qwOption.tabCondition = ""; //tab列表获得的条件
var lst_id = []; //条件ID列表
var lst_value = []; //条件值列表
var str_jointAllValue = ""; //判断是否输入了条件

//
//     打开查询窗口
//     cc:  Columns对象
//    url:  表格数据url
//frameId:  请求查询的tab

function OpenQueryWindow(url, cc, frameId) {
    debugger;
    qwOption.tabColumns = cc; //初始化参数
    qwOption.tabUrl = url;
    qwOption.tabFramId = frameId;
    $("#querryWindow").window("open");

    //(1) 查询窗口内的条件表格 qt初始化，必须在数据载入前运行	 
    $(function () {
        $('#qt').datagrid({
            rownumbers: false
        });
    });

    //(2) 载入查询数据
    $(function () {
        var qqData = getId(qwOption.tabColumns).replace("},]}", "}]}");
        //   $('#qt').datagrid({ rownumbers: true });
        $("#qt").datagrid("loadData", eval('(' + qqData + ')'));
        ;

    });
    //(3) 行单击打开编辑框，关闭之前打开的编辑框
    $(function () {
        //$('#qt').datagrid({ rownumbers: true });
        $('#qt').datagrid({
            rownumbers: false,
            onBeforeLoad: function () {
                $(this).datagrid('rejectChanges'); //回滚所有被删除的行。
            },
            //行单击，关闭上一行的编辑状态。
            onClickRow: function (rowIndex) {
                //   $('#qt').datagrid({ rownumbers: true });
                $('#qt').datagrid('beginEdit', rowIndex);
                if (lastIndex != rowIndex) {
                    $('#qt').datagrid('endEdit', lastIndex);
                }
                lastIndex = rowIndex;

            }
        }); //设置qt表格
    }); //载入初始化
}
//
//点击取消按钮
//

function btn_cancelClick() {
    $('#querryWindow').window('close');
}
//
//查询窗口，确认按钮
//

function btn_okClick() {

    //获取查询窗口中输入的条件
    qwOption.tabCondition = get_ConditonString();
    //找到请求查询的Tab
    var querryFrame = $("#" + qwOption.tabFramId);
    if (querryFrame && querryFrame.length > 0) {
        //设置表格页面的条件变量
        var cc = querryFrame[0].contentWindow.querryConditionStr; //当前页面条件
        querryFrame[0].contentWindow.querryConditionStr = cc + " and " + qwOption.tabCondition;
        querryFrame[0].contentWindow.str_ExportConditon = cc + " and " + qwOption.tabCondition; //更新导出条件
        //重新载入数据    
        querryFrame[0].contentWindow.LoadAjaxData();
        //分页控件页码设为1
        querryFrame[0].contentWindow.setPageNumber(1);
        //载入后，复原页面条件  
        querryFrame[0].contentWindow.querryConditionStr = cc;
    }
    CloseQueryWindow();
}
//
//关闭查询窗口 
//

function CloseQueryWindow() {
    $('#querryWindow').window('close');
}
//
//读取输入值，得到查询对象
//
function getQueryString() {
    $("#qt").datagrid("endEdit", lastIndex);
    var rows = $('#qt').datagrid("getRows");
    var rowCount = rows.length;
    var allValue = "";
    var qString = "{";
    for (var i = 0; i < rowCount; i++) {
        id = rows[i].id;
        value = rows[i].idvalue;

        allValue += value; //所有值是否全为空
        qString += '"';
        qString += id;
        qString += '":"';
        qString += value
        qString += '"';
        if (i != rowCount - 1)
            qString += ',';
    }
    qString += '}';
    if (allValue == "") qString = "";
    alert(qString);

}
//判断idName在查询窗口中是否存在
function idNameExist(idName) {
    var rowNumber = 0;   //行号
    for (rowNumber = 0; rowNumber < lst_id.length; rowNumber++)
        if (idName == lst_id[rowNumber])
            return rowNumber;
    return rowNumber;
}
//
//得到查询条件
//
function get_ConditonString() {
    //读取页面载入条件(默认为"1=1")
    var qStr = "1=1";
    //(1) 读取条件表格
    get_Id_ValueList();
    //    如果未输入条件，直接返回全选条件  
    if (str_jointAllValue == "") return qStr;
    //(2) 格式化特殊条件
    //    条件种类 ： % > < =  
    //    以 % 开头则 条件变为 like '...'
    formatValue();
    //(3）得到最终条件
    for (var i = 0; i < lst_value.length; i++) {
        if (lst_value[i] != "") {

            qStr += " and " + lst_id[i] + " " + lst_value[i];
        }
    }
    return qStr;
}
//
//(1)读取输入值，得到查询对象,包括 lst_id,lst_value
//

function get_Id_ValueList() {

    $("#qt").datagrid("endEdit", lastIndex);
    var rows = $('#qt').datagrid("getRows");
    var rowCount = rows.length;
    lst_id = [];
    lst_value = [];
    str_jointAllValue = "";

    for (var i = 0; i < rowCount; i++) {
        lst_id.push(rows[i].id);
        lst_value.push(rows[i].idvalue);
        str_jointAllValue += rows[i].idvalue; //如果str_jointAllValue="",表示未输入查询值
    }
}
//
//(2)格式化查询值
//
function formatValue() {
    var v = "";
    for (var i = 0; i < lst_value.length; i++) {
        v = lst_value[i];
        if (v != "") {
            switch (v.substring(0, 1)) {
                case ">":
                    lst_value[i] = "> '" + v.substring(1) + "'";
                    break;
                case "<":
                    lst_value[i] = "< '" + v.substring(1) + "'";
                    break;
                case "=":
                    //=开头 变为 ='...'
                    lst_value[i] = "= '" + v.substring(1) + "'";
                    break;
                default:
                    //其他变为 like '...'
                    var aastring;
                    if ((lst_id[i].indexOf("Time") >= 0 || lst_id[i].indexOf("Date") >= 0) && lst_id[i] != "ArriveTime" && lst_id[i] != "LeaveTime") {
                        aastring = getNowFormatDate(v);

                        if (isNaN(aastring)) {
                            lst_value[i] = "like '%" + v + "%' ";
                        } else {
                            lst_value[i] = "like '%" + rapalce0(aastring) + "%' ";
                        }
                    } else {
                        lst_value[i] = "like '%" + v + "%' ";
                    }
                    break;
            } //switch
        } //if
    } //for
} //formatValue

function rapalce0(strc) {
    var stringa = strc;
    if (stringa.substr(stringa.length - 1, 1) == "0") {
        var astrc = removeLastOne(stringa);
        return rapalce0(astrc);
    } else {
        return stringa;
    }
}

function removeLastOne(str) {
    return str.substring(0, str.length - 1);

}

function stringreplaceall(str) {
    var reg = new RegExp("-", "g");
    var regs = new RegExp(" ", "g");
    var newstr;
    newstr = str.replace(reg, "").replace(regs, "");
    return newstr;
}
//
//执行函数，获得本页中表格的标题id列表，和标题名称列表
//
function getId(Col_Difine) {

    var qData = '{ "total":1,"rows":[';
    var cc = Col_Difine.columns[0]; //列信息
    var fildCount = cc.length; //列数
    for (var i = 1; i < fildCount; i++) {
        var strColId = cc[i].field;
        var strColTitle = cc[i].title;
        if (strColId != 'operation' && cc[i].hidden != true && cc[i].hidden != '') {
            var querryLine = '{"id":"' + strColId + '","idName":"' + strColTitle + '","idvalue":""}';
            qData += querryLine;

            if (i != fildCount - 2) //不计operation
                qData += ',';
        }
    }
    qData += ']}';
    return qData;
}

//
//执行函数，获得本页中表格的标题id列表，和标题名称列表
//

function getIds(Col_Difine) {

    var qData = '{ "total":1,"rows":[';
    var cc = Col_Difine.columns[0]; //列信息
    var fildCount = cc.length; //列数
    for (var i = 2; i < fildCount; i++) {
        var strColId = cc[i].field;
        var strColTitle = cc[i].title;
        if (strColId != 'operation' && cc[i].hidden != true) {
            var querryLine = '{"id":"' + strColId + '","idName":"' + strColTitle + '","idvalue":""}';
            qData += querryLine;
            if (i != fildCount - 2) //不计operation
                qData += ',';
        }
    }
    qData += ']}';
    return qData;
}

function getNowFormatDate(datetime) {
    var day = new Date(datetime.replace("-", "/"));
    var Year = 0;
    var Month = 0;
    var Day = 0;
    var CurrentDate = "";
    Year = day.getFullYear(); //支持IE和火狐浏览器.
    Month = day.getMonth() + 1;
    Day = day.getDate();
    var hour = day.getHours();
    var minu = day.getMinutes();
    var sec = day.getSeconds();
    CurrentDate += Year;
    if (Month >= 10) {
        CurrentDate += Month;
    } else {
        CurrentDate += "0" + Month;
    }
    if (Day >= 10) {
        CurrentDate += Day;
    } else {
        CurrentDate += "0" + Day;
    }
    if (hour < 10) hour = "0" + hour;
    if (minu < 10) minu = "0" + minu;
    if (sec < 10) sec = "0" + sec;

    return CurrentDate + hour + minu + sec;
}

Date.prototype.format = function (format) // 
{
    var o = {
        "M+": this.getMonth() + 1,
        // month
        "d+": this.getDate(),
        // day
        "h+": this.getHours(),
        // hour
        "m+": this.getMinutes(),
        // minute
        "s+": this.getSeconds(),
        // second
        "q+": Math.floor((this.getMonth() + 3) / 3),
        // quarter
        "S": this.getMilliseconds() // millisecond
    };
    if (/(y+)/.test(format)) format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o) if (new RegExp("(" + k + ")").test(format)) format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
    return format;
};

/**
* 根据指定格式解析日期
* @param date  日期字符串
* @param format 格式字符串
* @returns {Date}
*/

function parseDate(date, format) {
    var result = new Date();
    if (/(y+)/.test(format)) result.setFullYear(date.substring(format.indexOf(RegExp.$1), format.indexOf(RegExp.$1) + RegExp.$1.length));
    if (/(M+)/.test(format)) result.setMonth(parseInt(date.substring(format.indexOf(RegExp.$1), format.indexOf(RegExp.$1) + RegExp.$1.length), 10) - 1);
    if (/(d+)/.test(format)) result.setDate(date.substring(format.indexOf(RegExp.$1), format.indexOf(RegExp.$1) + RegExp.$1.length));
    if (/(h+)/.test(format)) result.setHours(date.substring(format.indexOf(RegExp.$1), format.indexOf(RegExp.$1) + RegExp.$1.length));
    if (/(m+)/.test(format)) result.setMinutes(date.substring(format.indexOf(RegExp.$1), format.indexOf(RegExp.$1) + RegExp.$1.length));
    if (/(s+)/.test(format)) result.setSeconds(date.substring(format.indexOf(RegExp.$1), format.indexOf(RegExp.$1) + RegExp.$1.length));
    if (/(S+)/.test(format)) result.setMilliseconds(date.substring(format.indexOf(RegExp.$1), format.indexOf(RegExp.$1) + RegExp.$1.length));
    return result;
}
//  Angel工作室
//  Author:QQ:815657032 709047174 
//  Copyright (c) 2009-2015 angelasp.com
//  For further information go to http://www.angelasp.com