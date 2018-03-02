//  Angel工作室
//  Author:QQ:815657032 709047174
//  Copyright (c) 2009-2015 angelasp.com

var selectListCaller_FrameId = "";
var selectListCaller_ControlId = "";
var addTabCondition = "";

var nav_MaxCount = 12; //设置页面tab标签最大提示

//
// 页面载入完成后，自动加载两个事件的处理
//
$(document).ready(function () {
    tabClose();
    tabCloseEven();
    setAccordingHeight();  //导航栏高度
    $('#tabs').tabs({
        tools: [{ iconCls: 'icon-screen_full', handler: setFullScreen },
                { iconCls: 'panel-tool-close', handler: tabClosemy }]
    });
});
//测试
function mf_show()
{
    alert(mf_1);
}
//设置左侧导航栏的高度
function setAccordingHeight() {
    var accHeight = $(document.body).height()-158;
    $("#west_accordion").accordion({ height: accHeight });

}
//
//刷新标签页
//
function updateTab(title,id, url) {
    if ($('#tabs').tabs('exists', title)) {
        var tb=$('#tabs').tabs('getTab', title);
        var content = '<iframe scrolling="auto" frameborder="0" id="' + id + '" src="' + url + '" style="width:100%;height:100%;"></iframe>';
        var opt={ title:title, content:content,  closable:true};
        $('#tabs').tabs('update', {tab:tb,options:opt});
        //tabClose(); //添加关闭处理
    }
}

//
function reloadTabByFrameId(tabFramId){
    frame2Load = $("#" + tabFramId);
    if (frame2Load && frame2Load.length > 0) {
        //设置表格页面的条件变量
        //frame2Load[0].contentWindow.querryConditionStr = qwOption.tabCondition;
        //重新载入数据
        frame2Load[0].contentWindow.LoadAjaxData();
        //分页控件页码设为1
        frame2Load[0].contentWindow.setPageNumber(1);
    }
}


function reloadTabByFrameIdMeeting(tabFramId) {
    frame2Load = $("#" + tabFramId);
    if (frame2Load && frame2Load.length > 0) {
        //设置表格页面的条件变量
        //frame2Load[0].contentWindow.querryConditionStr = qwOption.tabCondition;
        //重新载入数据
     //   frame2Load[0].contentWindow.LoadAjaxData();
        //分页控件页码设为1
     //   frame2Load[0].contentWindow.setPageNumber(1);
        frame2Load[0].contentWindow.loadMeetingroom();
    }
}


function reloadTabByFrameIdIbatis(tabFramId) {
    frame2Load = $("#" + tabFramId);
    if (frame2Load && frame2Load.length > 0) {
        frame2Load[0].contentWindow.LoadAjaxData();
    }
}

//设置选择列表页面的目标控件.
/*
function setSelectListTargetControl(tabFramId, tFrameId, tControlId) {
    subframe = $("#" + tabFramId);
    if (subframe && subframe.length > 0) {
        //调用页面中的Javascript函数
        subframe[0].contentWindow.setTargetControl(tFrameId, tControlId);
    }

}*/
//根据列表选中的值更新  目标页面的字段
function setSelectListCallerValue(v){
    if(selectListCaller_FrameId =="") return;
    if(selectListCaller_ControlId == "") return;
    setTabTextBoxValue(selectListCaller_FrameId,selectListCaller_ControlId,v);
}
//
//设置某Tab中文本框的值
//
function setTabTextBoxValue(tabFramId,textBoxId,textBoxValue) {
    frame2Load = $("#" + tabFramId);
    if (frame2Load && frame2Load.length > 0) {

        //调用页面中的Javascript函数
        frame2Load[0].contentWindow.setTextValue(textBoxId, textBoxValue);
    }
}
//调用Tab中的函数，更新tab数据
function updateTabByRawData(tabFramId, rawType, rawData){
    frame2Load = $("#" + tabFramId);
    if (frame2Load && frame2Load.length > 0) {
        //调用页面中的Javascript函数
        frame2Load[0].contentWindow.updateFormByRawData(rawType, rawData);
    }

}

//
//打开、添加标签页
//
function addTab(title, id, url) {
    $(function () {
        var $tab = $('#tabs');
        var tabCount = $tab.tabs('tabs').length;
        var hasTab = $tab.tabs('exists', title);
        var aaaa;
        var content;
      //  debugger;
        if ((tabCount <= nav_MaxCount) || hasTab) {

          //  btn_cancelClick();
            aaaa = $('#tabs').id;
            $('#tabs').tabs('close', title);

            content = '<iframe scrolling="auto" frameborder="0" id="' + id + '" src="' + url + '" style="width:100%;height:100%;"></iframe>';
            $('#tabs').tabs('add', {
                title: title,
                content: content,
                closable: true
            });

        }
        else
            $.messager.confirm("系统提示", '您当前打开了太多的页面，如果继续打开，会造成程序运行缓慢，无法流畅操作！', function (b) {
                if (b) {
                    btn_cancelClick();
                    aaaa = $('#tabs').id;
                    $('#tabs').tabs('close', title);
                    content = '<iframe scrolling="auto" frameborder="0" id="' + id + '" src="' + url + '" style="width:100%;height:100%;"></iframe>';
                    $('#tabs').tabs('add', {
                        title: title,
                        content: content,
                        closable: true
                    });

                }
            });

        /**
        *   if ($('#tabs').tabs('exists', title))
        *       $('#tabs').tabs('select', title);
        *   else {
        *       var content = '<iframe scrolling="auto" frameborder="0" id="' + id + '" src="' + url + '" style="width:100%;height:100%;"></iframe>';
        *       $('#tabs').tabs('add', {title:title,content:content, closable:true});
        *   }
        */
        tabClose(); //添加关闭处理
    })
}
//
//按照标题关闭标签页
//
function closeTabByTitle(title) {
    if ($('#tabs').tabs('exists', title))
        $('#tabs').tabs('close', title);
}
//
//按照标题选择标签页
//
function selectTabByTitle(title) {
    if ($('#tabs').tabs('exists', title))
        $('#tabs').tabs('select', title);
}
//
// 绑定右键和双击菜单事件
//

function createFrame(url) 
{ 
var src = '<iframe scrolling="auto" frameborder="0" src="' + url + '" style="width:100%;height:100%;"></iframe>'; 
return src; 
} 

function tabClose() {
    /* 双击关闭TAB选项卡 */
    $(".tabs-inner").dblclick(function () {
        var subtitle = $(this).children(".tabs-closable").text();
        $('#tabs').tabs('close', subtitle);
    });

    $(".tabs-inner").click(function () {
        btn_cancelClick();
    });
    /* 为选项卡绑定右键 */
    $(".tabs-inner").bind('contextmenu', function (e) {
        $('#mm').menu('show', {
            left:e.pageX,
            top:e.pageY
        });
        var subtitle = $(this).children(".tabs-closable").text();
        $('#mm').data("currtab", subtitle);
        $('#tabs').tabs('select', subtitle);
        return false;
    });
}
//收起Top
function closeTop() {
    $('#MainFrame').layout('collapse', 'north');
}
//---------------------------
$('#tt1').tree({
    animate:true,
    onClick:function (node) {
        alert('you click' + node.text);
    }
})
//
// 弹出菜单鼠标事件处理
//

function tabCloseEven() {
    // 刷新
    $('#mm-tabupdate').click(function () {
        var currTab = $('#tabs').tabs('getSelected');
        var url = $(currTab.panel('options').content).attr('src');
        $('#tabs').tabs('update', {
            tab: currTab,
            options: {
                content: createFrame(url)
            }
        });
    });

    // 关闭当前
    $('#mm-tabclose').click(function () {
        var currtab_title = $('#mm').data("currtab");
        $('#tabs').tabs('close', currtab_title);
    });
    // 全部关闭
    $('#mm-tabcloseall').click(function () {
        $('.tabs-inner span').each(function (i, n) {
            var t = $(n).text();
            $('#tabs').tabs('close', t);
        });
    });
    // 关闭除当前之外的TAB
    $('#mm-tabcloseother').click(function () {
        $('#mm-tabcloseright').click();
        $('#mm-tabcloseleft').click();
    });
    // 关闭当前右侧的TAB
    $('#mm-tabcloseright').click(function () {
        var nextall = $('.tabs-selected').nextAll();
        if (nextall.length == 0) {
            // msgShow('系统提示','后边没有啦~~','error');
            //alert('后边没有啦~~');
            return false;
        }
        nextall.each(function (i, n) {
            var t = $('a:eq(0) span', $(n)).text();
            $('#tabs').tabs('close', t);
        });
        return false;
    });
    // 关闭当前左侧的TAB
    $('#mm-tabcloseleft').click(function () {
        var prevall = $('.tabs-selected').prevAll();
        if (prevall.length == 0) {
            //alert('到头了，前边没有啦~~');
            return false;
        }
        prevall.each(function (i, n) {
            var t = $('a:eq(0) span', $(n)).text();
            $('#tabs').tabs('close', t);
        });
        return false;
    });

    // 退出
    $("#mm-exit").click(function () { 
        $('#mm').menu('hide');

    });
} // 弹出菜单鼠标事件处理 (End)

//提示是否关闭窗体
function tabClosemy() {
    if (confirm('确认要关闭所有窗口吗？'))
        rightMenuClick({ id: 'closeall' });
}
//对窗体进行判断如果是最后一个不关闭
 function rightMenuClick(item) {
    var $tab = $('#tabs');
    var currentTab = $tab.tabs('getSelected');
    var titles = getTabTitles($tab);
    switch (item.id) {
        case "closeall":
            $.each(titles, function () {
                if (this != '个人桌面') 
                    $tab.tabs('close', this);
            });
            break;
    }

 };
//关闭所有tab
function getTabTitles($tab) {
    var titles = [];
    var tabs = $tab.tabs('tabs');
    $.each(tabs, function () { titles.push($(this).panel('options').title); });
    return titles;
};

//设置窗体放大
function setFullScreen() {
    var that = $(this);
    if (that.find('.icon-screen_full').length) {
        that.find('.icon-screen_full').removeClass('icon-screen_full').addClass('icon-screen_actual');
        $('[region=north],[region=west]').panel('close')
        var panels = $('body').data().layout.panels;
        panels.north.length = 0;
        panels.west.length = 0;
        if (panels.expandWest) {
            panels.expandWest.length = 0;
            $(panels.expandWest[0]).panel('close');
        }
        $('body').layout('resize');
    } else if ($(this).find('.icon-screen_actual').length) {
        that.find('.icon-screen_actual').removeClass('icon-screen_actual').addClass('icon-screen_full');
        $('[region=north],[region=west]').panel('open');
        var panels = $('body').data().layout.panels;
        panels.north.length = 1;
        panels.west.length = 1;
        if ($(panels.west[0]).panel('options').collapsed) {
            panels.expandWest.length = 1;
            $(panels.expandWest[0]).panel('open');
        }
        $('body').layout('resize');
    }
};
//  Angel工作室
//  Author:QQ:815657032 709047174 
//  Copyright (c) 2009-2015 angelasp.com







