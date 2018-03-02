//
// 初始化工具栏
//
function initToolBar() {
    $.ajax({
        type: "get",
        url: '../ADataTable/Operatingauthority.ashx?action=GetOdata',
        data: { NavlistName: FrameId },
        cache: false,
        async: false,
        success: function (result) {
            var obj_result = eval('(' + result + ')');   //返回信息 {success:true|false}
            if (obj_result.success) {
                var toolbar = {
                    toolbar: obj_result.operatevalue
                };
                $('#tt').datagrid(toolbar);
                operate = obj_result.operate;
            } else {
                $.messager.alert("系统提示", "操作权限加载错误", "info");
            }
        }
    });
}
//
// 鼠标事件
//
function initMouseEvent() {
    $(function () {
        $('#tt').datagrid({
            //行双击，查看功能
            onDblClickRow: function (index, row) { viewCurRecord(index); }
        })
    });
}
//行双击事件的实现
function rowDoubleClick(index, row) {
    viewCurRecord(index);
}

//
// 工具栏_新增按钮
//
function toolBarAddClick() {
    //alert('tttttt');
    SaveStuate = 'add';
    //打开空form
    $('#ff').form('clear');
    $('#dd').dialog({ title: '新增' + pageTitle + '', closed: false });
}

//新增修改方法
function Save(id) {
    var url = null;
    if (SaveStuate == "add") {
        url = formUrl+'?action=SaveDB&method=add';
    }
    if (SaveStuate == "save") {
        url = formUrl+'?action=SaveDB&method=modify&id=' + id + '';
    }
    if (id > 0) {

        if (SaveStuate == "save") {
            $('#ff').form('submit', {
                url: url,
                onSubmit: function () {
                    return $(this).form('validate');
                },
                success: function (data) {
                    var data = eval('(' + data + ')');
                    if (data.success == true) { 
                        $.messager.alert('系统提示', '保存成功');
                        LoadAjaxData();
                        setPageNumber(1);
                        $('#dd').dialog({ closed: true });

                    }
                    else {
                        $.messager.alert('系统提示', '保存失败');
                    }
                }

            });
        }
    }
    else {

        $('#ff').form('submit', {
            url: url,
            onSubmit: function () {
                return $(this).form('validate');
            },
            success: function (data) {
                var data = eval('(' + data + ')');

                if (data.success == true) {
                    $.messager.alert('系统提示', '保存成功');
                    LoadAjaxData();
                    setPageNumber(1);
                    $('#dd').dialog({ closed: true });
                }
                else {
                    $.messager.alert('系统提示', '保存失败');
                }
            }

        });
    }


}



//权限新增修改方法
function RoleSave(id) {
        var nodes = $('#rtt').tree('getChecked');
        var rvalue = '';
        for (var i = 0; i < nodes.length; i++) {
            if (rvalue != '') rvalue += ',';
            rvalue += nodes[i].id;
        }
    //    alert(rvalue);
        if (SaveStuate == "save") {
            $.ajax({
                type: 'get',
                url: formUrl + '?action=UpOpereate&id=' + id,
                data: {RoleValue: rvalue},
                cache: false,
                success: function (result) {
                    var data = eval('(' + result + ')');
                    if (data.success) {
                        $.messager.alert('系统提示', '角色权限修改成功');
                        LoadAjaxData();
                        setPageNumber(1);
                        $('#Permission').dialog({ closed: true });
                    }
                    else
                        $.messager.alert("系统提示", "角色权限修改失败！", "info");
                }
            });
        }
}


//
// 工具栏刷新按钮
//
function toolBarReloadClick() {
    LoadAjaxData();
    //分页控件页码设为1
    setPageNumber(1);
    str_ExportConditon = "";
}
//
// 工具栏_批量删除按钮
//
function toolBarDeleteClick() {
    $.messager.confirm('确 认', '确认要删除选中的数据吗?', function (r) {
        if (r) {
            var str_Json_l_ids = getSelectedIds();                  //1 获得选中的id
            if (str_Json_l_ids == "") return;
            if (actionRequest("deleteObjects", str_Json_l_ids)) {   //2请求Action操作
                LoadAjaxData();
                setPageNumber(1);
                $.messager.alert("系统提示", "数据删除完成", "info");
            }
        }
    });
}
//CSV数据导出
function toolBarExportClick() {
    // alert(querryConditionStr);
    var columns = Columns.columns[0];
    openExportForm(pageTitle, pageTableName, getValue(columns));

}
function getValue(data) {
    var field = [];
    var title = [];
    for (var i = 0; i < data.length; i++) {
        if (data[i].field != 'operation' && data[i].field != 'id') {
            field.push(data[i].field);
            title.push(data[i].title);
        }
    }
    var v = '&exportfield=' + field.toString() + '&exporttitle=' + title.toString();
    return v;
}


//
//操作请求（操作类型，操作对象)
//str_actType: deleteObject, updateObject,InsertObject,DeleteObjects
//str_actObj:
function actionRequest(str_actType, str_actObj) {
    var actionSuccess = false;
    $.ajax({
        type: "get",
        url: listActionUrl,
        data: {
            tableName: pageTableName, modelClassName: pageModelClassName,
            actionType: str_actType, actionObj: str_actObj
        },
        cache: false,
        async: false,
        success: function (result) {
            var obj_result = eval('(' + result + ')');   //返回信息 {success:true|false}
            if (obj_result.success)
                actionSuccess = true;
        }
    });
    return actionSuccess;
}
//获得选中记录
//返回 str_Json_l_ids
function getSelectedIds() {
    SelectedIds = [];   //清空数组
    var rows = $('#tt').datagrid('getSelections');    //对象数组
    for (var i = 0; i < rows.length; i++) {
        SelectedIds.push(rows[i].id);            //保存选中的id
    }
    return JSON.stringify(SelectedIds);;
}
//
//
//刷新操作列的链接按钮
//
function updateActions() {
    var rowcount = $('#tt').datagrid('getRows').length; //当前页记录数
    for (var i = 0; i < rowcount; i++) {
        $('#tt').datagrid('updateRow', { index: i, row: { operation: '' } });
    }
}

////
////行命令_"删除"
////
//function deleteCurRow(index) {
//    $("#tt").datagrid("endEdit", lastIndex);
//    $.messager.confirm('确 认', '确认要删除选中的数据吗?', function (r) {
//        if (r) {
//            //$.messager.alert("系统提示", "执行数据删除", "info");
//            var raws = $('#tt').datagrid("getRows");
//            if ((index >= raws.length) || index < 0) return;
//            var raw = raws[index];
//            if (actionRequest("deleteObject", raw.id)) {
//                LoadAjaxData();
//                setPageNumber(1);
//                $.messager.alert("系统提示", "数据删除完成", "info");
//            }
//            else {
//                $.messager.alert("系统提示", "删除失败,请确认数据在其它业务中未被使用", "info");
//            }
//        }
//    });
//}

function deleteCurRow(index) {
    $("#tt").datagrid("endEdit", lastIndex);
    $.messager.confirm('确 认', '确认要删除选中的数据吗?', function (r) {
        if (r) {
            var raws = $('#tt').datagrid("getRows");
            if ((index >= raws.length) || index < 0) return;
            var raw = raws[index];
            $.ajax({
                type: 'get',
                url: formUrl+'?action=DelDB&id=' + raw.ID,
                cache: false,
                success: function (result) {
                    var data = eval('(' + result + ')');
                    if (data.success) {
                        LoadAjaxData();
                        setPageNumber(1);
                        $.messager.alert("系统提示", "数据删除完成", "info");
                    }
                    else
                        $.messager.alert("系统提示", "数据删除失败", "info");
                }
            });
        }
    });
}


function reviewCurRecordt(index) {
    //$("#tt").datagrid("endEdit", lastIndex);
    $.messager.confirm('确 认', '确认要审核通过该任务吗?', function (r) {
        if (r) {
            var raws = $('#tt').datagrid("getRows");
            if ((index >= raws.length) || index < 0) return;
            var raw = raws[index];
            if (actionRequest("reviewObject", raw.id)) {
                LoadAjaxData();
                setPageNumber(1);
                $.messager.alert("系统提示", "该任务审核完成", "info");
            }
        }
    });
}



//
//查看当前行的 明细表单
//
function viewCurRecord(index) {
    //清除之前的选择
    $("#tt").datagrid("clearSelections");
    //获取选中的行数据
    var raws = $('#tt').datagrid("getRows");
    if ((index >= raws.length) || index < 0) return;
    var raw = raws[index];
    //调用框架方法，打开明细表单
    viewUrl = formUrl + "?id=" + raw.id + "&isReview=false";  //打开表单（传入参数id）
    //关闭旧表单
    parent.closeTabByTitle(formTitle);
    //打开新表单
    parent.addTab(formTitle, formFrameId, viewUrl);
}

//显示详细detail
function showCurRecord(index) {
    //清除之前的选择
    $("#tt").datagrid("clearSelections");
    //获取选中的行数据
    var raws = $('#tt').datagrid("getRows");
    if ((index >= raws.length) || index < 0) return;
    var raw = raws[index];
    //调用框架方法，打开明细表单
    viewUrld = formUrld + "?id=" + raw.id + "&isReview=false";  //打开表单（传入参数id）
    //关闭旧表单
    parent.closeTabByTitle(formTitled);
    //打开新表单
    parent.addTab(formTitled, formFrameIdd, viewUrld);
}

//显示审核页面
function showReview(index) {
    //清除之前的选择
    $("#tt").datagrid("clearSelections");
    //获取选中的行数据
    var raws = $('#tt').datagrid("getRows");
    if ((index >= raws.length) || index < 0) return;
    var raw = raws[index];
    //调用框架方法，打开明细表单
    viewUrlR = formUrlR + "?id=" + raw.id + "&isReview=false";  //打开表单（传入参数id）
    //关闭旧表单
    parent.closeTabByTitle(formTitleR);
    //打开新表单
    parent.addTab(formTitleR, formFrameIdR, viewUrlR);
}


//
//查看当前行的 明细表单
//
function AuditCurRecord(index) {
    //清除之前的选择
    $("#tt").datagrid("clearSelections");
    //获取选中的行数据
    var raws = $('#tt').datagrid("getRows");
    if ((index >= raws.length) || index < 0) return;
    var raw = raws[index];
    //调用框架方法，打开明细表单
    viewUrl = formAuditUrl + "?id=" + raw.id + "&isReview=false";  //打开表单（传入参数id）
    //关闭旧表单
    parent.closeTabByTitle(formAuditTitle);
    //打开新表单
    parent.addTab(formAuditTitle, formAuditFrameId, viewUrl);
}

//
// 打开导出画面
//
function openExportForm(listTitle, tableName, exportCondition) {
    //调用框架方法，打开明细表单
    var exportFormUrl = "../Fileinfo/exportCSV.aspx";
    exportFormUrl += "?listTitle=" + listTitle;
    exportFormUrl += "&tableName=" + tableName;

    exportFormUrl += exportCondition;

    //exportFormUrl += "&querryString=" + escape(parent.qwOption.tabCondition) + ""; //传入参数
    //exportFormUrl += "&querryString=" + querryConditionStr;  // 检索条件
    if (str_ExportConditon == null || str_ExportConditon == "")
        exportFormUrl += "&querryString=" + escape(querryConditionStr);  // 页面条件
    else
        exportFormUrl += "&querryString=" + escape(str_ExportConditon);  // 用户指定检索条件

    // exportFormUrl += "&querryString=" + querryConditionStr;  // 页面条件
    //打开表单
    parent.addTab("数据导出", "dataExport", exportFormUrl);
}

Date.prototype.format = function (format) // 
{
    var o = {
        "M+": this.getMonth() + 1, // month
        "d+": this.getDate(),    // day
        "h+": this.getHours(),   // hour
        "m+": this.getMinutes(), // minute
        "s+": this.getSeconds(), // second
        "q+": Math.floor((this.getMonth() + 3) / 3),  // quarter
        "S": this.getMilliseconds() // millisecond
    };
    if (/(y+)/.test(format)) format = format.replace(RegExp.$1,
    (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o) if (new RegExp("(" + k + ")").test(format))
        format = format.replace(RegExp.$1,
      RegExp.$1.length == 1 ? o[k] :
        ("00" + o[k]).substr(("" + o[k]).length));
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
    if (/(y+)/.test(format))
        result.setFullYear(date.substring(format.indexOf(RegExp.$1), format.indexOf(RegExp.$1) + RegExp.$1.length));
    if (/(M+)/.test(format))
        result.setMonth(parseInt(date.substring(format.indexOf(RegExp.$1), format.indexOf(RegExp.$1) + RegExp.$1.length), 10) - 1);
    if (/(d+)/.test(format))
        result.setDate(date.substring(format.indexOf(RegExp.$1), format.indexOf(RegExp.$1) + RegExp.$1.length));
    if (/(h+)/.test(format))
        result.setHours(date.substring(format.indexOf(RegExp.$1), format.indexOf(RegExp.$1) + RegExp.$1.length));
    if (/(m+)/.test(format))
        result.setMinutes(date.substring(format.indexOf(RegExp.$1), format.indexOf(RegExp.$1) + RegExp.$1.length));
    if (/(s+)/.test(format))
        result.setSeconds(date.substring(format.indexOf(RegExp.$1), format.indexOf(RegExp.$1) + RegExp.$1.length));
    if (/(S+)/.test(format))
        result.setMilliseconds(date.substring(format.indexOf(RegExp.$1), format.indexOf(RegExp.$1) + RegExp.$1.length));
    return result;
}
/**
 * 分割日期字符串
 */
function seprateDateString(value) {
    return value == null || value=="" ? "" : parseDate(value, "yyyyMMddhhmmss").format("yyyy-MM-dd hh:mm");
}


/**
* 八位日期字符串
*/
function EiteDateformatString(value) {
    return value == "" ? "" : parseDate(value, "yyyyMMddhhmmss").format("yyyy-MM-dd");
}