(function ($) {
    var oldFnTab = $.fn.tab;
    $.fn.tab = function (options) {
        options = $.extend({
            defaultIndex: 0,
            activeClass: 'weui_bar_item_on',
            onToggle: $.noop
        }, options);
        const $tabbarItems = this.children().children('.weui_tabbar_item, .weui_navbar_item');
        const $tabBdItems = this.children().children('.weui_tab_bd_item');
        this.toggle = function (index) {
            const $defaultTabbarItem = $tabbarItems.eq(index);
            if ($defaultTabbarItem.hasClass(options.activeClass)) {
                return;
            }
            $defaultTabbarItem.addClass(options.activeClass).siblings().removeClass(options.activeClass);
            const $defaultTabBdItem = $tabBdItems.eq(index);
            $defaultTabBdItem.show().siblings('.weui_tab_bd_item').hide();
            options.onToggle(index);
        };
        const self = this;
        this.children().children('.weui_tabbar_item, .weui_navbar_item').on('click', function (e) {
            const index = $(this).index();
            self.toggle(index);
        });
        this.toggle(options.defaultIndex);
        return this;
    };
    $.fn.tab.noConflict = function () {
        return oldFnTab;
    };
    $.weuiSubmitAjax = function (options) {
        var defaults = {
            url: "",
            param: [],
            loading: "正在处理数据...",
            success: null
        };
        var options = $.extend(defaults, options);
        $.showLoading(options.loading);
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
                    $.hideLoading();
                    if (data.state == "success") {
                        options.success(data);
                        $.toptip(data.message, 'success');
                    } else {
                        $.toptip(data.message, 'error');
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    $.hideLoading();
                    $.toast(errorThrown, "forbidden");
                },
                beforeSend: function () {
                    $.showLoading(options.loading);
                },
                complete: function () {
                    $.hideLoading();
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
            $.showLoading(options.loading);
            window.setTimeout(function () {
                $.ajax({
                    url: options.url,
                    data: options.param,
                    type: "post",
                    dataType: "json",
                    success: function (data) {
                        $.hideLoading();
                        if (data.state == "success") {
                            options.success(data);
                            $.toptip(data.message, 'success');
                        } else {
                            $.toptip(data.message, 'error');
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        $.hideLoading();
                        $.toast(errorThrown, "forbidden");
                    },
                    beforeSend: function () {
                        $.showLoading(options.loading);
                    },
                    complete: function () {
                        $.hideLoading();
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
            loading: "正在处理数据...",
            success: null,
            close: true
        };
        var options = $.extend(defaults, options);
        if ($('[name=__RequestVerificationToken]').length > 0) {
            options.param["__RequestVerificationToken"] = $('[name=__RequestVerificationToken]').val();
        }
        $.confirm(options.prompt, options.promptTitle, function () {
            $.showLoading(options.loading);
            window.setTimeout(function () {
                $.ajax({
                    url: options.url,
                    data: options.param,
                    type: "post",
                    dataType: "json",
                    success: function (data) {
                        $.hideLoading();
                        if (data.state == "success") {
                            options.success(data);
                            $.toptip(data.message, 'success');
                        } else {
                            $.toptip(data.message, 'error');
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        $.hideLoading();
                        $.toast(errorThrown, "forbidden");
                    },
                    beforeSend: function () {
                        $.showLoading(options.loading);
                    },
                    complete: function () {
                        $.hideLoading();
                    }
                });
            }, 50);
        }, function () {
            //取消操作
        });

    }

    $.noFindHeadImage = function (image) {
        image.src = "/Content/images/headImage/head-default.jpg";
        image.onerror = null;
    }

})($);