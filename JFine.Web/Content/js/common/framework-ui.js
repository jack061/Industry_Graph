$(function () {
    //document.domain = 'localhost';
    $("[data-toggle='tooltip']").tooltip();
    //$('img').error(function () {
    //    $(this).attr('src', "/Content/images/headImage/hbis.png");
    //    //this.onerror = null;
    //});
    $(".ui-filter-text").click(function () {
        if ($(this).next('.ui-filter-list').is(":hidden")) {
            $(this).css('border-bottom-color', '#fff');
            $(".ui-filter-list").slideDown(10);
            $(this).addClass("active")
        } else {
            $(this).css('border-bottom-color', '#ccc');
            $(".ui-filter-list").slideUp(10);
            $(this).removeClass("active");
        }
    });
})
$.getLocal = function (name) {
    if (typeof (Storage) !== 'undefined') {
        return localStorage.getItem(name)
    } else {
        window.alert('请用新版浏览器，推荐最新版火狐!')
    }
}

$.store = function (name, val) {
    if (typeof (Storage) !== 'undefined') {
        localStorage.setItem(name, val)
    } else {
        window.alert('请用新版浏览器，推荐最新版火狐!')
    }
}
$.reload = function () {
    location.reload();
    return false;
}

$.loading = function (bool, text) {
    var $loadingpage = top.$("#loadingPage");
    var $loadingtext = $loadingpage.find('.loading-content');
    if (bool) {
        $loadingpage.show();
    } else {
        if ($loadingtext.attr('istableloading') == undefined) {
            $loadingpage.hide();
        }
    }
    if (!!text) {
        $loadingtext.html(text);
    } else {
        $loadingtext.html("数据加载中，请稍后…");
    }
    $loadingtext.css("left", (top.$('body').width() - $loadingtext.width()) / 2 - 50);
    $loadingtext.css("top", (top.$('body').height() - $loadingtext.height()) / 2);
}
$.request = function (name) {
    var search = location.search.slice(1);
    var arr = search.split("&");
    for (var i = 0; i < arr.length; i++) {
        var ar = arr[i].split("=");
        if (ar[0] == name) {
            if (decodeURI(ar[1]) == 'undefined') {
                return "";
            } else {
                return decodeURI(ar[1]);
            }
        }
    }
    return "";
}
$.currentWindow = function () {
    var iframeId = top.$(".JFine_iframe:visible").attr("id");
    return top.frames[iframeId];
}
$.noFindHeadImage = function (image) {
    image.src = "/Content/images/headImage/head-default.png";
    image.onerror = null;
}
$.noFindImage = function (image) {
    image.src = "/Content/images/default.jpg";
    image.onerror = null;
}
$.browser = function () {
    var userAgent = navigator.userAgent;
    var isOpera = userAgent.indexOf("Opera") > -1;
    if (isOpera) {
        return "Opera"
    };
    if (userAgent.indexOf("Firefox") > -1) {
        return "FF";
    }
    if (userAgent.indexOf("Chrome") > -1) {
        if (window.navigator.webkitPersistentStorage.toString().indexOf('DeprecatedStorageQuota') > -1) {
            return "Chrome";
        } else {
            return "360";
        }
    }
    if (userAgent.indexOf("Safari") > -1) {
        return "Safari";
    }
    if (userAgent.indexOf("compatible") > -1 && userAgent.indexOf("MSIE") > -1 && !isOpera) {
        return "IE";
    };
}
$.download = function (url, data, method) {
    if (url && data) {
        data = typeof data == 'string' ? data : jQuery.param(data);
        var inputs = '';
        $.each(data.split('&'), function () {
            var pair = this.split('=');
            inputs += '<input type="hidden" name="' + pair[0] + '" value="' + pair[1] + '" />';
        });
        $('<form action="' + url + '" method="' + (method || 'post') + '">' + inputs + '</form>').appendTo('body').submit().remove();
    };
};
$.checkedArray = function (id) {
    var isOK = true;
    if (id == undefined || id == null || id == "" || id == 'null' || id == 'undefined') {
        isOK = false;
        $.modalAlert("请先选择数据记录", "warning");
    }
    return isOK;
}
$.checkedRow = function (id) {
    var isOK = true;
    if (id == undefined || id == null || id == "" || id == 'null' || id == 'undefined') {
        isOK = false;
        $.modalAlert("请先选择一条数据记录", "warning");
    } else if (id.split(",").length > 1) {
        isOK = false;
        $.modalAlert("只能选择一条数据记录", "warning");
    }
    return isOK;
}
$.modalOpen = function (options) {
    var defaults = {
        id: null,
        title: '系统窗口',
        width: "100px",
        height: "100px",
        url: '',
        shade: 0.3,
        btn: ['确认', '关闭'],
        btnclass: ['btn btn-primary', 'btn btn-danger'],
        callBack: null
    };
    var options = $.extend(defaults, options);
    var _width = top.$(window).width() > parseInt(options.width.replace('px', '')) ? options.width : top.$(window).width() + 'px';
    var _height = top.$(window).height() > parseInt(options.height.replace('px', '')) ? options.height : top.$(window).height() + 'px';
    top.layer.open({
        id: options.id,
        type: 2,
        shade: options.shade,
        title: options.title,
        fix: false,
        area: [_width, _height],
        content: options.url,
        btn: options.btn,
        btnclass: options.btnclass,
        yes: function () {
            options.callBack(options.id)
        }, cancel: function () {
            return true;
        }
    });
}
$.modalConfirm = function (content, callBack) {
    top.layer.confirm(content, {
        icon: "fa-exclamation-circle",
        title: "系统提示",
        btn: ['确认', '取消'],
        btnclass: ['btn btn-primary', 'btn btn-danger'],
    }, function (index, layero) {
        top.layer.close(index);
        callBack(true);
    }, function () {
        callBack(false)
    });
}
$.modalAlert = function (content, type) {
    var icon = "";
    if (type == 'success') {
        icon = "fa-check-circle";
    }
    if (type == 'error') {
        icon = "fa-times-circle";
    }
    if (type == 'warning') {
        icon = "fa-exclamation-circle";
    }
    top.layer.alert(content, {
        icon: icon,
        title: "系统提示",
        btn: ['确认'],
        btnclass: ['btn btn-primary'],
    });
}

$.modalMsg = function (content, type) {
    if (type != undefined) {
        var icon = "";
        if (type == 'success') {
            icon = "fa-check-circle";
        }
        if (type == 'error') {
            icon = "fa-times-circle";
        }
        if (type == 'warning') {
            icon = "fa-exclamation-circle";
        }
        top.layer.msg(content, { icon: icon, time: 4000, shift: 5 });
        top.$(".layui-layer-msg").find('i.' + icon).parents('.layui-layer-msg').addClass('layui-layer-msg-' + type);
    } else {
        top.layer.msg(content);
    }
}

$.modalClose = function () {
    var index = top.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
    var $IsdialogClose = top.$("#layui-layer" + index).find('.layui-layer-btn').find("#IsdialogClose");
    var IsClose = $IsdialogClose.is(":checked");
    if ($IsdialogClose.length == 0) {
        IsClose = true;
    }
    if (IsClose) {
        top.layer.close(index);
    } else {
        location.reload();
    }
}

$.submitAjax = function (options) {
    var defaults = {
        url: "",
        param: [],
        loading: "正在提交数据...",
        iftips: true,
        success: null
    };
    var options = $.extend(defaults, options);
    $.loading(true, options.loading);
    window.setTimeout(function () {
        if ($('[name=__RequestVerificationToken]').length > 0) {
            options.param["__RequestVerificationToken"] = $('[name=__RequestVerificationToken]').val();
        }
        $.ajax({
            url: options.url,
            data: options.param,
            type: "post",
            dataType: "json",
            success: function (data) {
                if (data.state == "success") {
                    options.success(data);
                    if (options.iftips) {
                        $.modalMsg(data.message, data.state);
                    }

                } else {
                    $.modalAlert(data.message, data.state);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                $.loading(false);
                if (!(XMLHttpRequest.responseText == undefined || XMLHttpRequest.responseText == null || XMLHttpRequest.responseText == '')) {
                    $.modalMsg(XMLHttpRequest.responseJSON.message, "error");
                } else {
                    $.modalMsg(errorThrown, "error");
                }
                
            },
            beforeSend: function () {
                $.loading(true, options.loading);
            },
            complete: function () {
                $.loading(false);
            }
        });
    }, 50);
}
$.submitForm = function (options) {
    var defaults = {
        url: "",
        param: [],
        loading: "正在处理数据...",
        success: null,
        fail: null,
        error: null,
        close: true,
        modal: true
    };
    var options = $.extend(defaults, options);
    $.loading(true, options.loading);
    window.setTimeout(function () {
        if ($('[name=__RequestVerificationToken]').length > 0) {
            options.param["__RequestVerificationToken"] = $('[name=__RequestVerificationToken]').val();
        }
        $.ajax({
            url: options.url,
            data: options.param,
            type: "post",
            dataType: "json",
            success: function (data) {
                $.loading(false);
                if (data.state == "success") {
                    options.success(data);
                    $.modalMsg(data.message, data.state);
                    if (options.close == true && options.modal) {
                        $.modalClose();
                    }
                    else if (!options.close) {
                        top.$.jfinetab.refreshTab();
                    }
                    else {
                        var parentId = top.$.jfinetab.getCurrentTabParentId();
                        top.$.jfinetab.scrollRefreshTabFromId(parentId);

                    }
                } else {
                    if (options.fail != null) {
                        options.fail(data);
                    }
                    $.modalAlert(data.message, data.state);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                $.loading(false);
                if (options.error != null) {
                    options.error();
                }
                $.modalMsg(errorThrown, "error");
            },
            beforeSend: function () {
                $.loading(true, options.loading);
            },
            complete: function () {
                $.loading(false);
            }
        });
    }, 50);
}
$.deleteForm = function (options) {
    var defaults = {
        prompt: "注：您确定要删除该项数据吗？",
        url: "",
        param: [],
        loading: "正在删除数据...",
        success: null,
        close: true
    };
    var options = $.extend(defaults, options);
    if ($('[name=__RequestVerificationToken]').length > 0) {
        options.param["__RequestVerificationToken"] = $('[name=__RequestVerificationToken]').val();
    }
    $.modalConfirm(options.prompt, function (r) {
        if (r) {
            $.loading(true, options.loading);
            window.setTimeout(function () {
                $.ajax({
                    url: options.url,
                    data: options.param,
                    type: "post",
                    dataType: "json",
                    success: function (data) {
                        $.loading(false);
                        if (data.state == "success") {
                            options.success(data);
                            $.modalMsg(data.message, data.state);
                        } else {
                            $.modalAlert(data.message, data.state);
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        $.loading(false);
                        if (!(XMLHttpRequest.responseText == undefined || XMLHttpRequest.responseText == null || XMLHttpRequest.responseText == '')) {
                            $.modalMsg(XMLHttpRequest.responseJSON.message, "error");
                        } else {
                            $.modalMsg(errorThrown, "error");
                        }
                    },
                    beforeSend: function () {
                        $.loading(true, options.loading);
                    },
                    complete: function () {
                        $.loading(false);
                    }
                });
            }, 500);
        }
    });

}

$.confirmSubmitForm = function (options) {
    var defaults = {
        prompt: "您确定要提交数据吗？",
        url: "",
        param: [],
        loading: "正在处理数据...",
        success: null,
        close: true
    };
    var options = $.extend(defaults, options);
    if ($('[name=__RequestVerificationToken]').length > 0) {
        options.param["__RequestVerificationToken"] = $('[name=__RequestVerificationToken]').val();
    }
    $.modalConfirm(options.prompt, function (r) {
        if (r) {
            $.loading(true, options.loading);
            window.setTimeout(function () {
                $.ajax({
                    url: options.url,
                    data: options.param,
                    type: "post",
                    dataType: "json",
                    success: function (data) {
                        if (data.state == "success") {
                            options.success(data);
                            $.modalMsg(data.message, data.state);
                        } else {
                            $.modalAlert(data.message, data.state);
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        $.loading(false);
                        if (!(XMLHttpRequest.responseText == undefined || XMLHttpRequest.responseText == null || XMLHttpRequest.responseText == '')) {
                            $.modalMsg(XMLHttpRequest.responseJSON.message, "error");
                        } else {
                            $.modalMsg(errorThrown, "error");
                        }
                    },
                    beforeSend: function () {
                        $.loading(true, options.loading);
                    },
                    complete: function () {
                        $.loading(false);
                    }
                });
            }, 500);
        }
    });

}
$.jsonWhere = function (data, action) {
    if (action == null) return;
    var reval = new Array();
    $(data).each(function (i, v) {
        if (action(v)) {
            reval.push(v);
        }
    })
    return reval;
}
$.fn.jqGridRowValue = function () {
    var $grid = $(this);
    var selectedRowIds = $grid.jqGrid("getGridParam", "selarrrow");
    if (selectedRowIds != "") {
        var json = [];
        var len = selectedRowIds.length;
        for (var i = 0; i < len ; i++) {
            var rowData = $grid.jqGrid('getRowData', selectedRowIds[i]);
            json.push(rowData);
        }
        return json;
    } else {
        return $grid.jqGrid('getRowData', $grid.jqGrid('getGridParam', 'selrow'));
    }
}
$.fn.jqGridRow = function () {
    var $grid = $(this);
    var id = $grid.jqGrid('getGridParam', "selrow");
    if (id == null || id == undefined || id == "" || id == 'null' || id == 'undefined') {
        return null;
    } else {
        var rowData = $grid.jqGrid('getRowData', id);
        return rowData;
    }
}
$.fn.jqGridRows = function () {
    var $grid = $(this);
    var json = [];
    var selectedRowIds = $grid.jqGrid("getGridParam", "selarrrow");
    if (selectedRowIds != "") {
        var len = selectedRowIds.length;
        for (var i = 0; i < len ; i++) {
            var rowData = $grid.jqGrid('getRowData', selectedRowIds[i]);
            json.push(rowData);
        }
    }
    return json;
}
$.fn.formValid = function () {
    return $(this).valid({
        errorPlacement: function (error, element) {
            element.parents('.formValue').addClass('has-error');
            element.parents('.has-error').find('i.error').remove();
            element.parents('.has-error').append('<i class="form-control-feedback fa fa-exclamation-circle error" data-placement="left" data-toggle="tooltip" title="' + error + '"></i>');
            $("[data-toggle='tooltip']").tooltip();
            if (element.parents('.input-group').hasClass('input-group')) {
                //element.parents('.has-error').find('i.error').css('right', '33px')
            }
        },
        success: function (element) {
            element.parents('.has-error').find('i.error').remove();
            element.parent().removeClass('has-error');
        }
    });
}
$.fn.formSerialize = function (formdate) {
    var element = $(this);
    if (!!formdate) {
        for (var key in formdate) {
            var $id = element.find('#' + key);
            var value = $.trim(formdate[key]).replace(/&nbsp;/g, '');
            var type = $id.attr('type');
            if ($id.hasClass("select2-hidden-accessible")) {
                type = "select";
            }
            switch (type) {
                case "checkbox":
                    if (value == "true") {
                        $id.attr("checked", 'checked');
                    } else {
                        $id.removeAttr("checked");
                    }
                    break;
                case "radio":
                    $("input[name='" + key + "'][value=" + value + "]").attr("checked", true);
                    break;
                case "select":
                    if (value.indexOf(",") >= 0) {
                        value = value.split(',');
                    }
                    $id.val(value).trigger("change");
                    break;
                default:
                    $id.val(value);
                    break;
            }
        };
        return false;
    }
    var postdata = {};
    element.find('input,select,textarea').each(function (r) {
        var $this = $(this);
        var id = $this.attr('id');
        if (id == undefined) {
            id = $this.attr('name');
        }
        var type = $this.attr('type');
        if ($this.hasClass("select2-hidden-accessible")) {
            type = "select";
        }
        switch (type) {
            case "checkbox":
                postdata[id] = $this.is(":checked");
                break;
            case "radio":
                if (id != '' && id != undefined && id != null) {
                    postdata[id] = $("input[name='" + id + "']:checked").val();
                }
                break;
            case "select":
                value = $this.val();
                var selectResult = "";
                if (value instanceof Array) {
                    for (var i = 0; i < value.length; i++) {
                        if (i == value.length - 1) {
                            selectResult = selectResult + value[i];
                        } else {
                            selectResult = selectResult + value[i] + ",";
                        }
                    }
                } else {
                    selectResult = value;
                }
                postdata[id] = selectResult;
                break;
            default:
                //var value = $this.val() == "" ? "&nbsp;" : $this.val();
                var value = ($this.val() == "" || $this.val() == null) ? "&nbsp;" : $this.val();
                if (!$.request("keyValue")) {
                    value = value.replace(/&nbsp;/g, '');
                }
                postdata[id] = value;
                break;
        }
    });
    if ($('[name=__RequestVerificationToken]').length > 0) {
        postdata["__RequestVerificationToken"] = $('[name=__RequestVerificationToken]').val();
    }
    return postdata;
};
$.fn.formInitialize = function (formdate) {
    var element = $(this);
    if (!!formdate) {
        for (var key in formdate) {
            var $id = element.find('div#' + key);
            var value = $.trim(formdate[key]).replace(/&nbsp;/g, '');
            $id.html(value);
        };
        return false;
    }
};
$.fn.bindSelect = function (options) {
    var defaults = {
        id: "id",
        text: "text",
        search: false,
        placeholder: '请选择',
        allowClear: true,
        url: "",
        param: {},
        defaultContent: "<option></option>",
        data: [],
        change: null
    };
    var options = $.extend(defaults, options);
    var $element = $(this);
    if (options.url != "") {
        $.ajax({
            url: options.url,
            data: options.param,
            dataType: "json",
            async: false,
            success: function (data) {
                $element.html(options.defaultContent);
                $.each(data, function (i) {
                    $element.append($("<option></option>").val(data[i][options.id]).html(data[i][options.text]));
                });
                $element.select2({
                    minimumResultsForSearch: options.search == true ? 0 : -1,
                    placeholder: options.placeholder,
                    allowClear: options.allowClear,
                });
                $element.on("change", function (e) {
                    if (options.change != null) {
                        var blank = $(this).html().indexOf(options.defaultContent);
                        if (blank < 0) {
                            options.change(data[$(this).find("option:selected").index()]);
                        } else {
                            var index = $(this).find("option:selected").index();
                            if (index == 0) {
                                options.change({});
                            } else {
                                options.change(data[index - 1]);
                            }
                        }
                    }
                   // $("#select2-" + $element.attr('id') + "-container").html($(this).find("option:selected").text().replace(/　　/g, ''));
                });
            }
        });
    } else {
        $.each(options.data, function (i) {
            $element.append($("<option></option>").val(options.data[i][options.id]).html(options.data[i][options.text]));
        });
        $element.on("change", function (e) {
            if (options.change != null) {
                var blank = $(this).html().indexOf(options.defaultContent);
                if (blank < 0) {
                    options.change(options.data[$(this).find("option:selected").index()]);
                } else {
                    var index = $(this).find("option:selected").index();
                    if (index == 0) {
                        options.change({});
                    } else {
                        options.change(options.data[index - 1]);
                    }
                }
            }
            $("#select2-" + $element.attr('id') + "-container").html($(this).find("option:selected").text().replace(/　　/g, ''));
        });
        $element.select2({
            minimumResultsForSearch: -1,
            placeholder: options.placeholder,
            allowClear: options.allowClear,
        });
    }
}

$.fn.bindTreeSelect = function (options) {
    var defaults = {
        id: "id",
        text: "text",
        parentId: "parentId",
        search: false,
        placeholder: '请选择',
        allowClear: true,
        url: "",
        param: {},
        defaultContent: "<option></option>",
        data: [],
        change: null
    };
    var options = $.extend(defaults, options);
    var $element = $(this);
    if (options.url != "") {
        $.ajax({
            url: options.url,
            data: options.param,
            dataType: "json",
            async: false,
            success: function (data) {
                var data_new = [];
                getNewTreeData(data, data_new);
                $element.html(options.defaultContent);
                options = $.extend(options, { treeData: { dataArr: data } });
                $element.select2ToTree(options);
                $element.on("change", function (e) {
                    if (options.change != null) {
                        var blank = $(this).html().indexOf(options.defaultContent);
                        if (blank < 0) {
                            options.change(data_new[$(this).find("option:selected").index()]);
                        } else {
                            var index = $(this).find("option:selected").index();
                            if (index == 0) {
                                options.change({});
                            } else {
                                options.change(data_new[index - 1]);
                            }
                        }

                    }
                   // $("#select2-" + $element.attr('id') + "-container").html($(this).find("option:selected").text().replace(/　　/g, ''));
                });
            }
        });
    } else {
        options = $.extend(options, { treeData: { dataArr: data } });
        $element.select2ToTree(options);
        $element.on("change", function (e) {
            if (options.change != null) {
                var blank = $(this).html().indexOf(options.defaultContent);
                if (blank < 0) {
                    options.change(options.data[$(this).find("option:selected").index()]);
                } else {
                    var index = $(this).find("option:selected").index();
                    if (index == 0) {
                        options.change({});
                    } else {
                        options.change(options.data[index - 1]);
                    }
                }

            }
            $("#select2-" + $element.attr('id') + "-container").html($(this).find("option:selected").text().replace(/　　/g, ''));
        });
    }
}

function getNewTreeData(data,newData)
{
    for (var i = 0; i < data.length; i++)
    {
        newData.push(data[i]);
        if (data[i].ChildNodes != undefined && data[i].ChildNodes != null)
        {
            getNewTreeData(data[i].ChildNodes, newData);
        }
    }
}

$.fn.authorizeButton = function () {
    var moduleId = top.$(".JFine_iframe:visible").attr("id").substr(7);
    var dataJson = jsonsql.query("select * from json where (BindId == '" + moduleId + "')", top.clients.authorizeButton);
    var $element = $(this);
    if (dataJson != undefined) {
        $.each(dataJson, function (i) {
            var str = '<a id="JF_' + dataJson[i].Code + '" class="btn btn-primary dropdown-text" onclick="' + dataJson[i].Code + '()"><i class="' + dataJson[i].Icon + '"></i>' + dataJson[i].Name + '</a>';
            $element.append(str);
        });
    }
}
$.fn.dataGrid = function (options) {
    var defaults = {
        datatype: "json",
        autowidth: true,
        rownumbers: true,
        shrinkToFit: false,
        gridview: true
    };
    var options = $.extend(defaults, options);
    var $element = $(this);
    $element.jqGrid(options);
};
$.fn.guid = function () {
    function S4() {
        return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
    }
    return (S4() + S4() + "-" + S4() + "-" + S4() + "-" + S4() + "-" + S4() + S4() + S4());
}
//form表单erialize()之后转json
$.fn.formToJson = function (data) {
    data = decodeURIComponent(data, true);//防止中文乱码
    data = data.replace(/&/g, "\",\"");
    data = data.replace(/=/g, "\":\"");
    data = "{\"" + data + "\"}";
    return JSON.parse(data);
}
//打开新页面
$.fn.newPage = function (url, name) {
    var win = window.open(url, name, "location=no,menubar=no,toolbar=no,status=no,directories=no,scrollbars=yes,resizable=yes");
    win.resizeTo(screen.width, screen.height);
    win.moveTo(0, 0);
}
//根据语音名称播放 XX.wav
$.fn.playVoiceFromName = function (name) {
    var audioPlay = $(this)[0];
    audioPlay.src = "/Content/Files/Voice/" + name;
    audioPlay.play();
}
//根据语音路径播放
$.fn.playVoiceFromPath = function (path) {
    var audioPlay = $(this)[0];
    audioPlay.src = path;
    audioPlay.play();
}
//根据文字播放（文字转语音）
$.fn.palyVoiceFromText = function (text) {
    var audioPlay = $(this)[0];
    audioPlay.src = "/SysCommon/Common/GetVoice?voice=" + text;
    audioPlay.play();
};
var mvcParamMatch = (function () {
    var MvcParameterAdaptive = {};
    MvcParameterAdaptive.isArray = Function.isArray ||
	function (o) {
	    return typeof o === "object" && Object.prototype.toString.call(o) === "[object Array]"
	};
    MvcParameterAdaptive.convertArrayToObject = function (arrName, array, saveOjb) {
        var obj = saveOjb || {};

        function func(name, arr) {
            for (var i in arr) {
                if (!MvcParameterAdaptive.isArray(arr[i]) && typeof arr[i] === "object") {
                    for (var j in arr[i]) {
                        if (MvcParameterAdaptive.isArray(arr[i][j])) {
                            func(name + "[" + i + "]." + j, arr[i][j])
                        } else if (typeof arr[i][j] === "object") {
                            MvcParameterAdaptive.convertObject(name + "[" + i + "]." + j + ".", arr[i][j], obj)
                        } else {
                            obj[name + "[" + i + "]." + j] = arr[i][j]
                        }
                    }
                } else {
                    obj[name + "[" + i + "]"] = arr[i]
                }
            }
        }
        func(arrName, array);
        return obj
    };
    MvcParameterAdaptive.convertObject = function (objName, turnObj, saveOjb) {
        var obj = saveOjb || {};

        function func(name, tobj) {
            for (var i in tobj) {
                if (MvcParameterAdaptive.isArray(tobj[i])) {
                    MvcParameterAdaptive.convertArrayToObject(i, tobj[i], obj)
                } else if (typeof tobj[i] === "object") {
                    func(name + i + ".", tobj[i])
                } else {
                    obj[name + i] = tobj[i]
                }
            }
        }
        func(objName, turnObj);
        return obj
    };
    return function (json, arrName) {
        arrName = arrName || "";
        if (typeof json !== "object") throw new Error("请传入json对象");
        if (MvcParameterAdaptive.isArray(json) && !arrName) throw new Error("请指定数组名，对应Action中数组参数名称！");
        if (MvcParameterAdaptive.isArray(json)) {
            return MvcParameterAdaptive.convertArrayToObject(arrName, json)
        }
        return MvcParameterAdaptive.convertObject("", json)
    }
})();

//jqgrid 合并相同行
function Merger(gridName, CellName) {
    //得到显示到界面的id集合  
    var mya = $("#" + gridName + "").getDataIDs();
    //数据总行数  
    var length = mya.length;
    //定义合并行数  
    var rowSpanTaxCount = 1;
    for (var i = 0; i < length; i += rowSpanTaxCount) {
        //从当前行开始比对下面的信息  
        var before = $("#" + gridName + "").jqGrid('getRowData', mya[i]);
        rowSpanTaxCount = 1;
        for (j = i + 1; j <= length; j++) {
            //和上边的信息对比 如果值一样就合并行数+1 然后设置rowspan 让当前单元格隐藏  
            var end = $("#" + gridName + "").jqGrid('getRowData', mya[j]);
            if (before[CellName] == end[CellName]) {
                rowSpanTaxCount++;
                $("#" + gridName + "").setCell(mya[j], CellName, '', { display: 'none' });
            } else {
                break;
            }
        }
        $("#" + gridName + "").setCell(mya[i], CellName, '', '', { rowspan: rowSpanTaxCount });
    }
}

/*
 *   功能:实现VBScript的DateAdd功能.
 *   参数:interval,字符串表达式，表示要添加的时间间隔.
 *   参数:number,数值表达式，表示要添加的时间间隔的个数.
 *   参数:date,时间对象.
 *   返回:新的时间对象.
 *   var now = new Date();
 *   var newDate = DateAdd( "d", 5, now);
 *---------------   DateAdd(interval,number,date)   -----------------
 */
$.fn.DateAdd = function (interval, number, date) {
    switch (interval) {
        case "y ": {
            date.setFullYear(date.getFullYear() + number);
            return date;
            break;
        }
        case "q ": {
            date.setMonth(date.getMonth() + number * 3);
            return date;
            break;
        }
        case "m ": {
            date.setMonth(date.getMonth() + number);
            return date;
            break;
        }
        case "w ": {
            date.setDate(date.getDate() + number * 7);
            return date;
            break;
        }
        case "d ": {
            date.setDate(date.getDate() + number);
            return date;
            break;
        }
        case "h ": {
            date.setHours(date.getHours() + number);
            return date;
            break;
        }
        case "m ": {
            date.setMinutes(date.getMinutes() + number);
            return date;
            break;
        }
        case "s ": {
            date.setSeconds(date.getSeconds() + number);
            return date;
            break;
        }
        default: {
            date.setDate(d.getDate() + number);
            return date;
            break;
        }
    }
}

/*扩展日期格式化
调用： 
var time1 = new Date().Format("yyyy-MM-dd");
var time2 = new Date().Format("yyyy-MM-dd HH:mm:ss");
*/
Date.prototype.Format = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "H+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}

//下载文件
//param:{fileName:"",headerJson:"{}",dataJson:"[]"}
$.downloadExcel = function (options) {
    var defaults = {
        url: "/SysCommon/Common/downloadExcel",
        fileName:"",
        headerJson:{},
        dataJson: [],
        headType: 1,
        isTitle:1
    };
    var options = $.extend(defaults, options);
    var param = {};
    param.fileName = options.fileName;
    param.headerJson = JSON.stringify(options.headerJson);
    param.dataJson = JSON.stringify(options.dataJson);
    param.headType = options.headType;
    param.isTitle = options.isTitle;
    $.submitAjax({
        url: options.url,
        iftips: false,
        param: param,
        success: function (data) {
            window.open("/SysCommon/Common/DownloadExcelFile?filename=" + options.fileName + "&id=" + data.name);
        }
    });
}
$.downloadFile = function (url, filename) {
    window.open("/SysCommon/Common/DownloadFile?url=" + url + "&filename=" + filename);
}

$.download = function (options) {
    var defaults = {
        method: "GET",
        url: "",
        param: []
    };
    var options = $.extend(defaults, options);
    if (options.url && options.param) {
        var $form = $('<form action="' + options.url + '" method="' + (options.method || 'post') + '"></form>');
        for (var key in options.param) {
            var $input = $('<input type="hidden" />').attr('name', key).val(options.param[key]);
            $form.append($input);
        }
        $form.appendTo('body').submit().remove();
    };
}