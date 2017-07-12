var mainTabTitle = "系统首页";
//初始化
$(function() {
    $(".treenode").click(function () {

        var tabTitle = $(this).text();

        var url = $(this).attr("rel");
        var icon = $(this).attr("icon");
        var id = $(this).attr("id");
        addTab(tabTitle, url, icon, id);
    });
    tabClose();
    tabCloseEven();
});

//新增TAB
function addTab(subtitle, url, icon, id) {
    if (!$("#tabs").tabs("exists", subtitle)) {
        $("#tabs").tabs("add", {
            title: subtitle,
            content: createFrame(url, id),
            iconCls: icon || "icon-html",
            closable: true,
            width: $("#mainPanle").width() - 10,
            height: $("#mainPanle").height() - 25
        });
    } else {
        $("#tabs").tabs("select", subtitle);
    }
    tabClose();
}
//创建FRAME
function createFrame(url, id) {
    var s = '<iframe id="' + id + '" scrolling="auto" frameborder="0"  src="' + url + '" style="width:100%;height:99%;" ></iframe>';
    return s;
}
//关闭TAB
function tabClose() {
    /*双击关闭TAB选项卡*/
    $(".tabs-inner").dblclick(function () {
        var subtitle = $(this).children("span").text();
        if (subtitle != mainTabTitle) {
            $("#tabs").tabs("close", subtitle);
        }

    });

    $(".tabs-inner").bind("contextmenu", function (e) {
        $("#mm").menu("show", {
            left: e.pageX,
            top: e.pageY
        });

        var subtitle = $(this).children("span").text();
        $("#mm").data("currtab", subtitle);

        return false;
    });
}
//绑定右键菜单事件
function tabCloseEven() {

    //关闭当前
    $("#mm-tabclose").click(function () {
        var currtabTitle = $("#mm").data("currtab");
        if (currtabTitle != mainTabTitle) {
            $("#tabs").tabs("close", currtabTitle);
        }

    });
    //全部关闭
    $("#mm-tabcloseall").click(function () {
        $(".tabs-inner span").each(function (i, n) {
            var t = $(n).text();
            if (t != mainTabTitle) {
                $("#tabs").tabs("close", t);
            }

        });
    });
    //关闭除当前之外的TAB
    $("#mm-tabcloseother").click(function () {
        var currtabTitle = $("#mm").data("currtab");
        $('.tabs-inner span').each(function (i, n) {
            var t = $(n).text();
            if (t != currtabTitle && t != mainTabTitle) {
                $("#tabs").tabs("close", t);
            }

        });
    });
    //关闭当前右侧的TAB
    $("#mm-tabcloseright").click(function () {
        var nextall = $(".tabs-selected").nextAll();
        if (nextall.length == 0) {
            //msgShow('系统提示','后边没有啦~~','error');
            //alert('后边没有啦~~');
            return false;
        }
        nextall.each(function (i, n) {
            var t = $("a:eq(0) span", $(n)).text();
            if (t != mainTabTitle) {
                $("#tabs").tabs("close", t);
            }
        });
        return false;
    });
    //关闭当前左侧的TAB
    $("#mm-tabcloseleft").click(function () {
        var prevall = $(".tabs-selected").prevAll();
        if (prevall.length == 0) {
            //alert('到头了，前边没有啦~~');
            return false;
        }
        prevall.each(function (i, n) {
            var t = $("a:eq(0) span", $(n)).text();
            if (t != mainTabTitle) {
                $("#tabs").tabs("close", t);
            }
        });
        return false;
    });

    //退出
    $("#mm-exit").click(function () {
        $("#mm").menu("hide");
    });
    //刷新
    $("#mm-reload").click(function () {
        var currTab = $("#tabs").tabs("getSelected");
        var url = $(currTab.panel("options").content).attr("src");
        var id = $(currTab.panel("options").content).attr("id");; //获取id
        if (url && id) {
            $("#tabs").tabs("update", {
                tab: currTab,
                options: {
                    content: createFrame(url, id)
                }
            });
        }

    });
}