//扩展表单提交成JSON格式数据
$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        if (o[this.name]) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || null);
        } else {
            o[this.name] = this.value || null;
        }
    });
    return o;
};
$.extend($.fn.textbox.methods, {
    addClearBtn: function (jq, iconCls) {
        return jq.each(function () {
            var t = $(this);
            var opts = t.textbox('options');
            opts.icons = opts.icons || [];
            opts.icons.unshift({
                iconCls: iconCls,
                handler: function (e) {
                    $(e.data.target).textbox('clear').textbox('textbox').focus();
                    $(this).css('visibility', 'hidden');
                }
            });
            t.textbox();
            if (!t.textbox('getText')) {
                t.textbox('getIcon', 0).css('visibility', 'hidden');
            }
            t.textbox('textbox').bind('keyup', function () {
                var icon = t.textbox('getIcon', 0);
                if ($(this).val()) {
                    icon.css('visibility', 'visible');
                } else {
                    icon.css('visibility', 'hidden');
                }
            });
        });
    }
});
$.extend($.fn.datagrid.methods, {
    autoMergeCells: function (jq, fields) {
        return jq.each(function () {
            var target = $(this);
            if (!fields) {
                fields = target.datagrid("getColumnFields");
            }
            var rows = target.datagrid("getRows");
            var i = 0,
                j = 0,
                temp = {};
            for (i; i < rows.length; i++) {
                var row = rows[i];
                j = 0;
                for (j; j < fields.length; j++) {
                    var field = fields[j];
                    var tf = temp[field];
                    if (!tf) {
                        tf = temp[field] = {};
                        tf[row[field]] = [i];
                    } else {
                        var tfv = tf[row[field]];
                        if (tfv) {
                            tfv.push(i);
                        } else {
                            tfv = tf[row[field]] = [i];
                        }
                    }
                }
            }
            $.each(temp, function (field, colunm) {
                $.each(colunm, function () {
                    var group = this;
                    if (group.length > 1) {
                        var before,
                            after,
                            megerIndex = group[0];
                        for (var i = 0; i < group.length; i++) {
                            before = group[i];
                            after = group[i + 1];
                            if (after && (after - before) == 1) {
                                continue;
                            }
                            var rowspan = before - megerIndex + 1;
                            if (rowspan > 1) {
                               target.datagrid('mergeCells', {
                                    index: megerIndex,
                                    field: field,
                                    rowspan: rowspan
                                });
                            }
                            if (after && (after - before) != 1) {
                                megerIndex = after;
                            }
                        }
                    }
                });
            });
        });
    }
});
//AJAX异步方法
function xAjax(url, method, jsondata, successFn, isLoading) {
    $.ajax({
        url: url, //提交接口地址
        type: method, //提交方法　post/get
        data: jsondata, //表单数据序列化
        async: true, //是否异步
        dataType: "json", //数据类型
        success: successFn, //成功的回调方法
        beforeSend: function () {
            if (isLoading == undefined || isLoading) {
                $.messager.progress();
            }
        },
        complete: function (jqXhr, textStatus) {
            //textStatus的值：success,notmodified,nocontent,error,timeout,abort,parsererror  
            if (isLoading == undefined || isLoading) {
                $.messager.progress('close');
            }
            if (textStatus == "timeout") {
                $.messager.alert('错误', "网络不给力,请稍后再试", 'error');
            }
            if (textStatus == 'error') {
                $.messager.alert('错误', "系统错误(" + jqXhr.status + ")", 'error');
            }
        }
    });
}
//新增
function addData(url, width, height) {
    openWin(url, "新增", width || 880, height || 600);
}

//修改
function updateData(url, width, height) {
    var rows = $('#dataGrid').datagrid('getSelections');//获取所选择的行
    if (rows.length != 1) {
        $.messager.alert("提示", "请选择要编辑的数据", "warning");
        return;
    }
    var newurl = url + "/";
    openWin(newurl + rows[0].Id, "编辑", width || 880, height || 600);
}

//删除
function deleteData(url,isTree) {
    var rows = $('#dataGrid').datagrid('getSelections');//获取所选择的行
    if (rows.length == 1) {
        $.messager.confirm('确认', '您确认想要删除吗？', function (r) {
            if (r) {
                xAjax(url, "post", { id: rows[0].Id }, function (data) {
                    if (data.State == 1) {
                        $.messager.alert("消息", data.Msg, "info", function () {
                            reloadData(isTree);
                        });
                    } else {
                        $.messager.alert("提示", data.Msg, "error");
                    }
                });
            }
        });
    } else {
        $.messager.alert("提示", "请选择要一条要删除的数据", "warning");
        return;
    }
}
//复制
function copyData(url, isTree) {
    var rows = $('#dataGrid').datagrid('getSelections');//获取所选择的行
    if (rows.length == 1) {
        xAjax(url, "post", { id: rows[0].Id }, function (data) {
            if (data.State == 1) {
                $.messager.alert("消息", data.Msg, "info", function () {
                    reloadData(isTree);
                });
            } else {
                $.messager.alert("提示", data.Msg, "error");
            }
        });
    } else {
        $.messager.alert("提示", "请选择要一条要复制的数据", "warning");
        return;
    }
}



//查询
function searchData(isTree) {
    $('#searchFrm').form('submit', {
        onSubmit: function () {
            var jsonData = $(this).serializeObject();
            if (isTree) {
                $('#dataGrid').treegrid('load', jsonData);
            } else {
                $('#dataGrid').datagrid('load', jsonData);
            }
            return false;
        }
    });
}
//重置
function resetData(isTree) {
    $('#searchFrm').form('clear');
    if (isTree) {
        $('#dataGrid').treegrid('clearSelections');
    } else {
        $('#dataGrid').datagrid('clearSelections');
    }

    searchData(isTree);
}

//异步提交表单
function subForm(isTree) {

    $("form").form("submit", {
        onSubmit: function () {
            var isValid = $(this).form("validate");//表单验证
            if (!isValid) {
                return false;
            }
            var url = this.action;
            var jsondata = $(this).serialize();
            $.ajax({
                url: url,
                type: "Post",
                data: jsondata,
                dataType: "json",
                beforeSend: function () {
                    $.messager.progress({
                        title: '请等待',
                        text: '请求正在提交中'
                    });
                },
                success: function (data) {
                    if (data.State == 1) {
                        window.parent.window.reloadData(isTree);
                        if (isTree) {
                            reloadTree();
                        }
                        if ($.messager) {
                            $.messager.defaults.ok = "继续";
                            $.messager.defaults.cancel = "返回";
                            $.messager.confirm("操作提示", data.Msg, function (r) {
                                if (!r) {
                                    //关闭窗口，并刷新表单
                                    window.parent.window.closeWin();

                                }
                            });
                        }
                    } else {
                        $.messager.alert('错误', data.Msg, 'error');
                    }

                },
                complete: function () {
                    $.messager.progress('close');
                }
            });
            return false;
        }
    });
}
function reloadData(isTree) {
    if (isTree) {
        $("#dataGrid").treegrid("reload").treegrid("clearSelections");
    } else {
        $("#dataGrid").datagrid("reload").datagrid("clearSelections");
    }

}
//打开窗口
function openWin(url, title, width, height) {
    $("#popupWin").dialog({
        width: width,
        height: height,
        modal: true,
        title: title,
        top:5,
        maximizable: true,
        //fit: true,
        //inline: true,
        border: 'thin',
        //cls: 'c6',
        //toolbar: [{
        //    text: '保存',
        //    iconCls: 'icon-save',
        //    handler: function () {
        //        var childWindow = $(".xjh")[0].contentWindow; //#map是iframe
        //        childWindow.Save();
        //    }
        //}, '-', {
        //    text: '关闭',
        //    iconCls: 'icon-no',
        //    handler: function () {
        //        closeWin();
        //    }
        //}],
        content: '<iframe class="xjh" scrolling="auto" frameborder="0"  src="' + url + '" style="width:100%;height:99%;"></iframe>'
    });

}
//关闭窗口
function closeWin() {
    $("#popupWin").dialog('close');
}
//预览
function preview(path) {
    parent.window.parent.window.$('#popupWin').window({
        width: 800,
        height: 600,
        title: "图片预览",
        //noheader: true,
        border: false,
        minimizable: false,
        collapsible: false,
        plain: true,
        draggable: false,
        modal: true,
        resizable: false,
        content: "<img src='" + path + "' class='img-responsive'/>"
    });
}
//全选反选(树控件)
//参数:selected:传入this,表示当前点击的组件
//treeMenu:要操作的tree的id；如：id="userTree"
function treeChecked(selected, treeMenu) {
    var roots = $('#' + treeMenu).tree('getRoots');//返回tree的所有根节点数组
    var options = $(selected).linkbutton("options");
    if (options.iconCls == "icon-unchecked") {

        for (var i = 0; i < roots.length; i++) {
            var root = $('#' + treeMenu).tree('find', roots[i].id);//查找节点
            $('#' + treeMenu).tree('check', root.target);//将得到的节点选中
            var childrens = $('#' + treeMenu).tree('getChildren', root.target);
            for (var j = 0; j < childrens.length; j++) {
                var childrennode = $('#' + treeMenu).tree('find', childrens[j].id);//查找节点
                $('#' + treeMenu).tree('check', childrennode.target);//将得到的节点选中
            }
        }
        $(selected).linkbutton({ text: '清除', iconCls: 'icon-checked' });

    } else {
        for (var k = 0; k < roots.length; k++) {
            var root = $('#' + treeMenu).tree('find', roots[k].id);//查找节点
            $('#' + treeMenu).tree('uncheck', root.target);//将得到的节点选中
            var childrens = $('#' + treeMenu).tree('getChildren', root.target);
            for (var h = 0; h < childrens.length; h++) {
                var childrennode = $('#' + treeMenu).tree('find', childrens[h].id);//查找节点
                $('#' + treeMenu).tree('uncheck', childrennode.target);//将得到的节点选中
            }
        }
        $(selected).linkbutton({ text: '全选', iconCls: 'icon-unchecked' });
    }
}
//全选反选(数据列表控件)
//参数:selected:传入this,表示当前点击的组件
//treeMenu:要操作的tree的id；如：id="userTree"
function gridChecked(selected, gridId) {
    var options = $(selected).linkbutton("options");
    if (options.iconCls == "icon-unchecked") {

        $('#' + gridId).datalist('checkAll');
        $(selected).linkbutton({ text: '清除', iconCls: 'icon-checked' });

    } else {
        $('#' + gridId).datalist('uncheckAll');
        $(selected).linkbutton({ text: '全选', iconCls: 'icon-unchecked' });
    }
}
function datagridChecked(selected, gridId) {
    var options = $(selected).linkbutton("options");
    if (options.iconCls == "icon-unchecked") {

        $('#' + gridId).datagrid('selectAll');
        $(selected).linkbutton({ text: '清除', iconCls: 'icon-checked' });

    } else {
        $('#' + gridId).datagrid('clearSelections');
        $(selected).linkbutton({ text: '全选', iconCls: 'icon-unchecked' });
    }
}
//获取Tree选中ID
function getTreeIds(id) {
    var nodes = $("#" + id).tree("getChecked");
    var arr = [];
    $.each(nodes, function (k, v) {
        arr.push(v.id);
    });
    return arr.join(",");
}
//获取DataGrid选中ID
function getDataGridIds(rows) {
    var arr = [];
    $.each(rows, function (k, v) {
        arr.push(v.Id);
    });
    return arr.join(",");
}
//获取DataList选中ID
function getDataListIds(id) {
    var rows = $("#" + id).datalist("getChecked");
    var arr = [];
    if (rows.length > 0) {
        $.each(rows, function (k, v) {
            arr.push(v.Id);
        });
    }
    return arr.join(",");
}
//选择图标方法
function selectIcon(icon, setId) {
    $(setId).textbox("setText", icon).textbox({
        iconCls: icon,
        iconAlign: 'left'
    });
    //关闭窗口
    $("#dlg").dialog("close");

}
function checkURL(url) {
    var str = url;
    //判断URL地址的正则表达式为:http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?
    //下面的代码中应用了转义字符"\"输出一个字符"/"
    var expression = /http(s)?:\/\/([\w-]+\.)+[\w-]+(\/[\w- .\/?%&=]*)?/;
    var objExp = new RegExp(expression);
    if (objExp.test(str) == true) {
        return true;
    } else {
        return false;
    }
}
//===================================================格式化====================================================
//格式化时间
function formatDate(v, format) {
    if (!v) return "";
    var d = v;
    if (typeof v === 'string') {
        if (v.indexOf("/Date(") > -1)
            d = new Date(parseInt(v.replace("/Date(", "").replace(")/", ""), 10));
        else
            d = new Date(Date.parse(v.replace(/-/g, "/").replace("T", " ").split(".")[0])); //.split(".")[0] 用来处理出现毫秒的情况，截取掉.xxx，否则会出错
    }
    var o = {
        "M+": d.getMonth() + 1, //month
        "d+": d.getDate(), //day
        "h+": d.getHours(), //hour
        "m+": d.getMinutes(), //minute
        "s+": d.getSeconds(), //second
        "q+": Math.floor((d.getMonth() + 3) / 3), //quarter
        "S": d.getMilliseconds() //millisecond
    };
    if (/(y+)/.test(format)) {
        format = format.replace(RegExp.$1, (d.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    for (var k in o) {
        if (new RegExp("(" + k + ")").test(format)) {
            format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
        }
    }
    return format;
}
var formatValue = {};
//用户状态
formatValue.userStatus = function (value) {
    if (value == 1) {
        return "正常";
    } else {
        return "锁定";
    }
};
//时间
formatValue.date = function (value) {
    return formatDate(value, "yyyy-MM-dd");
};
formatValue.dateTime = function (value) {
    return formatDate(value, "yyyy-MM-dd hh:mm:ss");
};


//为下拉控件新增一个清除方法
var clearCombo = {
    box: function (obj) {
        obj.combobox({
            icons: [
                {
                    iconCls: 'icon-clear',
                    handler: function (e) {
                        $(e.data.target).combobox('clear');
                    }
                }
            ]
        });
    },
    tree: function (obj) {
        obj.combotree({
            icons: [
                {
                    iconCls: 'icon-clear',
                    handler: function (e) {
                        $(e.data.target).combotree('clear');
                    }
                }
            ]
        });
    }
    ,
    treegrid: function (obj) {
        obj.combotreegrid({
            icons: [
                {
                    iconCls: 'icon-clear',
                    handler: function (e) {
                        $(e.data.target).combotreegrid('clear');
                    }
                }
            ]
        });
    }
};