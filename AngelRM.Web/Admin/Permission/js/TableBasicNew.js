var pSize = 10, lastIndex = 0, editing = !1, newRow = !1, listDataUrl = "../ADataTable/ADataTable_Data.ashx";

var SaveStuate = null;
var mID = null;
function initApearence() {
    initToolBar(), $("#tt").datagrid(Columns);
}

function initEvent() {
    initRowEvent(), initPageEvent();
}

function initRowEvent() {
    $("#tt").datagrid({
        "onBeforeEdit": function (index, row) {
            row.editing = !0, updateActions();
        },
        "onAfterEdit": function (index, row) {
            row.editing = !1, updateActions();
        },
        "onCancelEdit": function (index, row) {
            row.editing = !1, updateActions();
        }
        //"onDblClickRow": function (index, row) {
        //    rowDoubleClick(index, row);
        //}
    });
}

function initPageEvent() {
    var p = $("#tt").datagrid("getPager");
    p && $(p).pagination({
        "onSelectPage": function (pageNumber, pageSize) {
            $(this).pagination("loading"), LoadPageData(pageNumber, pageSize), $(this).pagination("loaded");
        },
        "onBeforeRefresh": function () {
            $(this).pagination("loading");
        },
        "onRefresh": function () {
            str_ExportConditon = "";
            $(this).pagination("loaded"), initTable(), initApearence(), initEvent(), initData();
        },
        "onChangePageSize": function (pageSize) {
            pSize = pageSize;
        }
    });
}

function initData() {
    parent.addTabCondition != "" && (querryConditionStr = parent.addTabCondition), LoadAjaxData(), parent.addTabCondition = "";
}

function toolBarSearchClick() {
    parent.OpenQueryWindow(listDataUrl, Columns, FrameId);
}

function initTable() {
    $("#tt").datagrid({
        "width": $(document).width() - 20,
        "height": 420,
        striped: true,
        "fitColumns": !1,
        "remoteSort": !0,
        "nowrap": !0,
        "singleSelect": !0,
        "pagination": !0,
        "pageSize": pSize,
        "pageNumber": 1,
        "rownumbers": 1,
        "loadMsg": "正在加载数据,请稍候...",
        "showFooter": !1
    });
}

function setMultiSelect() {
    $("#tt").datagrid({
        "singleSelect": !1
    });
}

function LoadAjaxData() {
    $.ajax({
        "type": "get",
        "url": listDataUrl,
        "data": {
            "tableName": pageTableName,
            "modelClassName": pageModelClassName,
            "pageNo": pNumber,
            "pageSize": pSize,
            "querryCondition": querryConditionStr
        },
        "cache": !1,
        "success": function (result) {
            if (result) {
                var obj_result = eval("(" + result + ")");
                $("#tt").datagrid("loadData", obj_result);
            }
            obj_result || $.messager.alert("系统提示", "数据载入失败。", "info");
        }
    });
}

function LoadPageData(pNumber, pSize) {
    $.ajax({
        "type": "get",
        "url": listDataUrl,
        "data": {
            "tableName": pageTableName,
            "modelClassName": pageModelClassName,
            "pageNo": pNumber,
            "pageSize": pSize,
            "querryCondition": querryConditionStr
        },
        "cache": !1,
        "success": function (result) {
            if (result) {
                var obj_result = eval("(" + result + ")");
                $("#tt").datagrid("loadData", obj_result);
            }
            obj_result || $.messager.alert("系统提示", "数据载入失败。", "info");
        }
    });
}

function LoadPageDataOrderBy(pNumber, pSize, str_orderBy, bl_asc) {
    $.ajax({
        "type": "get",
        "url": listDataUrl,
        "data": {
            "tableName": pageTableName,
            "modelClassName": pageModelClassName,
            "pageNo": pNumber,
            "pageSize": pSize,
            "orderBy": str_orderBy,
            "asc": bl_asc,
            "querryCondition": querryConditionStr
        },
        "cache": !1,
        "success": function (result) {
            if (result) {
                var obj_result = eval("(" + result + ")");
                $("#tt").datagrid("loadData", obj_result);
            }
            obj_result || $.messager.alert("系统提示", "数据载入失败。", "info");
        }
    });
}

function LoadUrlData() {
    obj_queryParams = {}, obj_queryParams.pageNo = pNumber, obj_queryParams.pageSize = pSize, obj_queryParams.querryCondition = querryConditionStr, dg_loadOption = {}, dg_loadOption.url = listDataUrl, dg_loadOption.queryParams = obj_queryParams, dg_loadOption.method = "post", $("#tt").datagrid(dg_loadOption);
}

function getSelectedIds() {
    SelectedIds = [];
    var rows = $("#tt").datagrid("getSelections");
    for (var i = 0; i < rows.length; i++) SelectedIds.push(rows[i].id);
    return JSON.stringify(SelectedIds);
}

function updateActions() {
    var rowcount = $("#tt").datagrid("getRows").length;
    for (var i = 0; i < rowcount; i++) $("#tt").datagrid("updateRow", {
        "index": i,
        "row": {
            "operation": ""
        }
    });
}

function setPageNumber(pNumber) {
    $(function () {
        var p = $("#tt").datagrid("getPager");
        p && $(p).pagination({
            "pageNumber": pNumber
        });
    });
}

function ClearDataGrid() {
    $("#tt").datagrid("loadData", {
        "total": 0,
        "rows": []
    });
}

function getEmptyData(Col_Difine) {
    var qData = "{", cc = Col_Difine.columns[0], fildCount = cc.length;
    for (var i = 0; i < fildCount; i++) {
        var strColId = cc[i].field;
        strColId != "operation" && (qData += strColId + ":''", i != fildCount - 2 && (qData += ","));
    }
    qData += "}";
    var obj_qData = eval("(" + qData + ")");
    return obj_qData;
}

function isRowEmpty(raw) {
    var cc = Columns.columns[0], fildCount = cc.length, strColId, rawData = "";
    for (var i = 0; i < fildCount; i++) strColId = cc[i].field, strColId != "operation" && (rawData += eval("raw." + strColId));
    return rawData == "";
}



//
// 初始化工具栏
//
function initToolBar() {
    //    var toolbar = {
    //        toolbar: [{
    //            text: '新增',
    //            iconCls: 'icon-add',
    //            handler: function () { toolBarAddClick(); }
    //        },
    //        '-', {
    //            text: '查询',
    //            iconCls: 'icon-search',
    //            handler: function () { toolBarSearchClick(); }
    //        }, {
    //            text: '刷新',
    //            iconCls: 'icon-reload',
    //            handler: function () { toolBarReloadClick(); }
    //        }, {
    //            text: '导出',
    //            iconCls: 'icon-redo',
    //            handler: function () { toolBarExportClick(); }
    //        }
    //        ]
    //    };
    //    $('#tt').datagrid(toolbar);
}
//编辑操作权限
function rowEditClick(index) {
    SaveStuate = 'save';
    //清除之前的选择
  //  $("#tt").datagrid("clearSelections");
    //获取选中的行数据
    var raws = $('#tt').datagrid("getRows");
    if ((index >= raws.length) || index < 0) return;
    var raw = raws[index];
    $('#ff').form('clear');

    $('#dd').dialog({ title: '编辑' + listTitle, closed: false });
    $('#id').val(raw.id);
    $('#RoleId').val(raw.RoleId);
    $('#NavName').val(raw.NavName);
    $('#TitleName').val(raw.TitleName);
    $('#RoleName').val(raw.RoleName);
    $.ajax({
        type: 'get',
        url: 'ashx/PermissionService.ashx?action=OperationData&NavName=' + raw.NavName + '',
        cache: false,
        success: function (result) {
            var data = eval('(' + result + ')');
            $('#Operation_Value').combotree('loadData', data);
        }
    });
    var ovalues = raw.Operation_Value.split(',');
    $('#Operation_Value').combotree('setValues', ovalues);
}


//
// 工具栏刷新按钮
//
function toolBarReloadClick() {
    LoadAjaxData();
    //分页控件页码设为1
    setPageNumber(1);
}
//CSV数据导出
function toolBarExportClick() {
    // alert(querryConditionStr);
    var columns = Columns.columns[0];
    openExportForm(pageTitle, pageTableName, getValue(columns));

}
//获取列
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

//行双击事件的实现
function rowDoubleClick(index, row) {
    viewCurRecord(index);
}


//
//行命令_"删除"
//
function rowDeleteClick(index) {
    $("#tt").datagrid("endEdit", lastIndex);
    $.messager.confirm('确 认', '确认要删除选中的数据吗?', function (r) {
        if (r) {
            var raws = $('#tt').datagrid("getRows");
            if ((index >= raws.length) || index < 0) return;
            var raw = raws[index];
            $.ajax({
                type: 'get',
                url: 'ashx/PermissionService.ashx?action=DelDB&id=' + raw.id,
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

function Save(id) {
    var url = null;
    if (SaveStuate == "save") {
        url = 'ashx/PermissionService.ashx?action=Modify';
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
 
}

$(function () {
    initTable(), initApearence(), initEvent(), initData();
});










