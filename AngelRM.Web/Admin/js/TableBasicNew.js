var operate = null;

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
        },
        "onDblClickRow": function (index, row) {
            rowDoubleClick(index, row);
        }
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
        "fitColumns": !1,
        "remoteSort": !0,
        "nowrap": !0,
        "singleSelect": !0,
        "pagination": !0,
        "pageSize": pSize,
        // "pageList": [5, 10, 15,20],
        "pageNumber": 1,
        "rownumbers": !1,
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
    //解决分页时条件还原问题
    if (str_ExportConditon == "") {
        str_ExportConditon = querryConditionStr;
    }
    $.ajax({
        "type": "get",
        "url": listDataUrl,
        "data": {
            "tableName": pageTableName,
            "modelClassName": pageModelClassName,
            "pageNo": pNumber,
            "pageSize": pSize,
            "querryCondition": str_ExportConditon
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

var pSize = 10, lastIndex = 0, editing = !1, newRow = !1, listDataUrl = "../ADataTable/ADataTable_Data.ashx";

$(function () {
    initTable(), initApearence(), initEvent(), initData();
});