var uiLoading;
bui.ready(function () {
    uiLoading = bui.loading({
        display: "block",
        width: 30,
        height: 30,
        text: '正在处理。。。',
        callback: function (argument) {
            console.log("clickloading")
        }
    });
});
$.get = function (name) {
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
$.back = function () {
    //window.history.back(-1);
    window.history.go(-1);
}
$.backRefresh = function () {
    window.location.href = document.referrer;
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

$.weuiSubmitAjax = function (options) {
    var defaults = {
        url: "",
        param: [],
        loading: "正在提交数据...",
        success: null,
        fail: null
    };
    var options = $.extend(defaults, options);
    uiLoading.show();
    uiLoading.text(options.loading);
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
                uiLoading.hide();
                if (data.state == "success") {
                    options.success(data);
                    //bui.alert(data.message);
                } else {
                    bui.alert(data.message);
                    options.fail(data);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                uiLoading.hide();
                bui.alert(errorThrown);
            },
            beforeSend: function () {
                uiLoading.show();
            },
            complete: function () {
                uiLoading.hide();
            }
        });
    }, 50);
}
$.weuiDeleteForm = function (options) {
    var defaults = {
        prompt: "注：您确定要删除该项数据吗？",
        promptTitle: "确认删除?",
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
    $.confirm(options.prompt, options.promptTitle, function () {
        uiLoading.show();
        window.setTimeout(function () {
            $.ajax({
                url: options.url,
                data: options.param,
                type: "post",
                dataType: "json",
                success: function (data) {
                    uiLoading.hide();
                    if (data.state == "success") {
                        options.success(data);
                        bui.alert(data.message);
                    } else {
                        bui.alert(data.message);
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    uiLoading.hide();
                    $.toast(errorThrown, "forbidden");
                },
                beforeSend: function () {
                    uiLoading.show();
                },
                complete: function () {
                    uiLoading.hide();
                }
            });
        }, 50);
    }, function () {
        //取消操作
    });

}
$.weuiConfirmSubmitForm = function (options) {
    var defaults = {
        prompt: "您确定要提交数据吗？",
        promptTitle: "确认提交?",
        url: "",
        param: [],
        loading: "正在提交数据...",
        success: null,
        close: true
    };
    var options = $.extend(defaults, options);
    if ($('[name=__RequestVerificationToken]').length > 0) {
        options.param["__RequestVerificationToken"] = $('[name=__RequestVerificationToken]').val();
    }
    $.confirm(options.prompt, options.promptTitle, function () {
        uiLoading.show();
        window.setTimeout(function () {
            $.ajax({
                url: options.url,
                data: options.param,
                type: "post",
                dataType: "json",
                success: function (data) {
                    uiLoading.hide();
                    if (data.state == "success") {
                        options.success(data);
                        bui.alert(data.message);
                    } else {
                        bui.alert(data.message);
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    uiLoading.hide();
                    $.toast(errorThrown, "forbidden");
                },
                beforeSend: function () {
                    uiLoading.show();
                },
                complete: function () {
                    uiLoading.hide();
                }
            });
        }, 50);
    }, function () {
        //取消操作
    });

}

$.noFindHeadImage = function (image) {
    image.src = "/Content/images/headImage/head-default.png";
    image.onerror = null;
}

$.noFindImage = function (image) {
    image.src = "/Content/images/default.jpg";
    image.onerror = null;
}

$.fn.formValid = function () {
    return $(this).valid({
        errorPlacement: function (error, element) {
            element.siblings('.has-error').remove();
            element.addClass('has-error');
            element.after('<i class="error has-error" style="width:100%;">' + error + '</i>');
        },
        success: function (element) {
            element.siblings('.has-error').remove();
            element.removeClass('has-error');
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
        var type = $this.attr('type');
        switch (type) {
            case "checkbox":
                postdata[id] = $this.is(":checked");
                break;
            case "radio":
                if (id != '' && id != undefined && id != null) {
                    postdata[id] = $("input[name='" + id + "']:checked").val();
                }
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

$.fn.bindWeuiSelect = function (options) {
    var defaults = {
        title: "选择",
        id: "id",
        text: "text",
        url: "",
        param: {},
        items: [],
        onChange: null,
        onClose: null,
        onOpen: null
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
                $.each(data, function (i) {
                    $element.append($("<option></option>").val(data[i][options.id]).html(data[i][options.text]));
                    options.items.push({ "title": data[i][options.text], "value": data[i][options.id] });
                });
                $element.select(options);
            }
        });
    } else {
        $element.select(options);
    }
}

//打开新页面
$.openNewPage = function (title,url) {
    var pageii = layer.open({
        title: [
            '' + title,
            'background-color:#39a4ff; color:#fff;'
        ]
        , topClose: true
        , topClosePos: 'right'
       , type: 1
      , content: '<iframe src="' + url + '" width="100%" height="100%" frameborder="0">'
      , anim: 'up'
      , style: 'position:fixed; left:0; top:0; width:100%; height:100%; border: none; -webkit-animation-duration: .5s; animation-duration: .5s;'
    });
}

//id:元素id
//title：打开页面标题
//url:打开页面链接
$.buiExSelect = function (id,title,url)
{
    var T = title;
    $("#" + id).click(function (T, url)
    {
        $.openNewPage(T, url);
    });
};