
(function ($) {
    $.jfineStack = {
        array: new Array(),
        getCount: function () {
            return $.jfineStack.array.length;
        },
        insert: function (element) {
            $.jfineStack.array.push(element);
        },
        pop: function (element) {
            var index = $.inArray(element, $.jfineStack.array);
            if (index >= 0) {
                $.jfineStack.array.splice(index, 1);
            }
        },
        getElement: function (i) {
            return $.jfineStack.array[i];
        }
    };
    $.jfinetab = {
        requestFullScreen: function () {
            var de = document.documentElement;
            if (de.requestFullscreen) {
                de.requestFullscreen();
            } else if (de.mozRequestFullScreen) {
                de.mozRequestFullScreen();
            } else if (de.webkitRequestFullScreen) {
                de.webkitRequestFullScreen();
            }
        },
        exitFullscreen: function () {
            var de = document;
            if (de.exitFullscreen) {
                de.exitFullscreen();
            } else if (de.mozCancelFullScreen) {
                de.mozCancelFullScreen();
            } else if (de.webkitCancelFullScreen) {
                de.webkitCancelFullScreen();
            }
        },
        refreshTab: function () {
            var currentId = $('.page-tabs-content').find('.active').attr('data-id');
            var target = $('.JFine_iframe[data-id="' + currentId + '"]');
            var url = target.attr('src');
            $.loading(true);
            target.attr('src', url).load(function () {
                $.loading(false);
            });
        },
        getCurrentTabId: function () {
            var currentId = $('.page-tabs-content').find('.active').attr('data-id');
            return currentId;
        },
        getCurrentTabParentId: function () {
            var currentId = $('.page-tabs-content').find('.active').attr('data-parentId');
            return currentId;
        },
        getTabContentFromName: function (pagename) {
            var resultPage;
            var pages = $('.page-tabs-content').children("[data-id]").find('.fa-remove').parents('a');
            for (var i = 0; i < pages.length; i++) {
                var pageText = pages[i].text;
                if ($.trim(pageText) == $.trim(pagename)) {
                    var page = $('.JFine_iframe[data-id="' + $(pages[i]).data('id') + '"]');
                    if (page.length > 0) {
                        resultPage = page[0].contentWindow;
                    }
                }
            }
            return resultPage;
        },
        getTabContentFromId: function (id) {
            var resultPage;
            var page = $('.JFine_iframe[data-id="' + id + '"]');
            if (page.length > 0) {
                resultPage = page[0].contentWindow;
            }
            return resultPage;
        },
        RefreshTabFromName: function (pagename) {
            var pages = $('.page-tabs-content').children("[data-id]").find('.fa-remove').parents('a');
            for (var i = 0; i < pages.length; i++) {
                var pageText = pages[i].text;
                if ($.trim(pageText) == $.trim(pagename)) {
                    var page = $('.JFine_iframe[data-id="' + $(pages[i]).data('id') + '"]');
                    if (page.length > 0) {
                        var url = page.attr('src');
                        $.loading(true);
                        target.attr('src', url).load(function () {
                            $.loading(false);
                        });
                    }
                }
            }
        },
        RefreshTabFromId: function (id) {
            var target = $('.JFine_iframe[data-id="' + id + '"]');
            var url = target.attr('src');
            $.loading(true);
            target.attr('src', url).load(function () {
                $.loading(false);
            });
        },
        scrollRefreshTabFromId: function (id) {
            $.jfinetab.closeCurrentTab();
            if (!$(this).hasClass('active')) {
                $('.mainContent .JFine_iframe').each(function () {
                    if ($(this).data('id') == id) {
                        $(this).show().siblings('.JFine_iframe').hide();
                        return false;
                    }
                });
                $(this).addClass('active').siblings('.menuTab').removeClass('active');
                $.jfinetab.scrollToTab(this);
            }
            $.jfinetab.RefreshTabFromId(id);
        },
        activeTab: function () {
            var currentId = $(this).data('id');
            if (!$(this).hasClass('active')) {
                $('.mainContent .JFine_iframe').each(function () {
                    if ($(this).data('id') == currentId) {
                        $(this).show().siblings('.JFine_iframe').hide();
                        return false;
                    }
                });
                $(this).addClass('active').siblings('.menuTab').removeClass('active');
                $.jfinetab.scrollToTab(this);
            }
            $.jfineStack.pop(currentId);
            $.jfineStack.insert(currentId);

        },
        closeOtherTabs: function () {
            $('.page-tabs-content').children("[data-id]").find('.fa-remove').parents('a').not(".active").each(function () {
                $.jfineStack.pop($(this).data('id'));
                $('.JFine_iframe[data-id="' + $(this).data('id') + '"]').remove();
                $(this).remove();
            });
            $('.page-tabs-content').css("margin-left", "0");
        },
        closeTab: function () {
            var closeTabId = $(this).parents('.menuTab').data('id');
            var currentWidth = $(this).parents('.menuTab').width();
            if ($(this).parents('.menuTab').hasClass('active')) {
                if ($.jfineStack.getCount() >= 2) {
                    var activeId = $.jfineStack.getElement($.jfineStack.getCount() - 2);
                    $('.page-tabs-content .menuTab').each(function () {
                        if ($(this).data('id') == activeId) {
                            $(this).addClass('active');
                            return false;
                        }
                    });
                    $('.mainContent .JFine_iframe').each(function () {
                        if ($(this).data('id') == activeId) {
                            $(this).show().siblings('.JFine_iframe').hide();
                            return false;
                        }
                    });
                    var marginLeftVal = parseInt($('.page-tabs-content').css('margin-left'));
                    if (marginLeftVal < 0) {
                        $('.page-tabs-content').animate({
                            marginLeft: (marginLeftVal + currentWidth) + 'px'
                        }, "fast");
                    }
                    $(this).parents('.menuTab').remove();
                    $('.mainContent .JFine_iframe').each(function () {
                        if ($(this).data('id') == closeTabId) {
                            $(this).remove();
                            $.jfineStack.pop(closeTabId);
                            return false;
                        }
                    });
                }
                if ($(this).parents('.menuTab').prev('.menuTab').size()) {
                    var activeId = $(this).parents('.menuTab').prev('.menuTab:last').data('id');
                    $(this).parents('.menuTab').prev('.menuTab:last').addClass('active');
                    $('.mainContent .JFine_iframe').each(function () {
                        if ($(this).data('id') == activeId) {
                            $(this).show().siblings('.JFine_iframe').hide();
                            return false;
                        }
                    });
                    $(this).parents('.menuTab').remove();
                    $('.mainContent .JFine_iframe').each(function () {
                        if ($(this).data('id') == closeTabId) {
                            $(this).remove();
                            $.jfineStack.pop(closeTabId);
                            return false;
                        }
                    });
                }
            }
            else {
                $(this).parents('.menuTab').remove();
                $('.mainContent .JFine_iframe').each(function () {
                    if ($(this).data('id') == closeTabId) {
                        $(this).remove();
                        $.jfineStack.pop(closeTabId);
                        return false;
                    }
                });
                $.jfinetab.scrollToTab($('.menuTab.active'));
            }
            return false;
        },
        closeCurrentTab: function () {
            $('.page-tabs-content').find('.active i').trigger("click");
        },
        addTab: function () {
            $("#header-nav>ul>li.open").removeClass("open");
            var dataId = $(this).attr('data-id');
            var dataTarget = $(this).attr('data-target');
            if (dataId != "") {
                top.$.cookie('jfine_currentmoduleid', dataId, { path: "/" });
            }
            var dataUrl = $(this).attr('href');
            var menuName = $.trim($(this).text());
            var flag = true;
            if (dataUrl == undefined || $.trim(dataUrl).length == 0) {
                return false;
            }
            if (dataTarget == 'open')
            {
                return false;
            }
            if (dataTarget == 'blank') {
                window.open(dataUrl, menuName);
                return false;
            }
            $('.menuTab').each(function () {
                if ($(this).data('url') == dataUrl) {
                    if (!$(this).hasClass('active')) {
                        $(this).addClass('active').siblings('.menuTab').removeClass('active');
                        $.jfinetab.scrollToTab(this);
                        $('.mainContent .JFine_iframe').each(function () {
                            if ($(this).data('url') == dataUrl) {
                                $(this).show().siblings('.JFine_iframe').hide();
                                return false;
                            }
                        });
                    }
                    flag = false;
                    return false;
                }
            });
            if (flag) {
                var str = '<a href="javascript:;" class="active menuTab" data-id="' + dataId + '" data-url="' + dataUrl + '"  data-parentId="' + dataId + '">' + menuName + ' <i class="fa fa-remove"></i></a>';
                $('.menuTab').removeClass('active');
                var str1 = '<iframe class="JFine_iframe" allowfullscreen="true"  id="iframe_' + dataId + '" name="iframe_' + dataId + '"  width="100%" height="100%" src="' + dataUrl + '" frameborder="0" data-id="' + dataId + '" data-url="' + dataUrl + '"  data-parentId="' + dataId + '" seamless></iframe>';
                $('.mainContent').find('iframe.JFine_iframe').hide();
                $('.mainContent').append(str1);
                $.loading(true);
                $('.mainContent iframe:visible').load(function () {
                    $.loading(false);
                });
                $('.menuTabs .page-tabs-content').append(str);
                $.jfinetab.scrollToTab($('.menuTab.active'));
            }
            $.jfineStack.pop(dataId);
            $.jfineStack.insert(dataId);
            return false;
        },
        addTabContent: function (url, title, id, parentId) {
            $("#header-nav>ul>li.open").removeClass("open");
            var dataId = id;
            if (dataId != "") {
                top.$.cookie('jfine_currentmoduleid', dataId, { path: "/" });
            }
            var dataUrl = url;
            var menuName = title;
            var flag = true;
            if (dataUrl == undefined || $.trim(dataUrl).length == 0) {
                return false;
            }
            $('.menuTab').each(function () {
                if ($(this).data('url') == dataUrl) {
                    if (!$(this).hasClass('active')) {
                        $(this).addClass('active').siblings('.menuTab').removeClass('active');
                        $.jfinetab.scrollToTab(this);
                        $('.mainContent .JFine_iframe').each(function () {
                            if ($(this).data('url') == dataUrl) {
                                $(this).show().siblings('.JFine_iframe').hide();
                                return false;
                            }
                        });
                    }
                    flag = false;
                    return false;
                }
            });
            if (flag) {
                var str = '<a href="javascript:;" class="active menuTab" data-id="' + dataId + '" data-url="' + dataUrl + '" data-parentId="' + parentId + '" >' + menuName + ' <i class="fa fa-remove"></i></a>';
                $('.menuTab').removeClass('active');
                var str1 = '<iframe class="JFine_iframe" allowfullscreen="true"  id="iframe_' + dataId + '" name="iframe_' + dataId + '"  width="100%" height="100%" src="' + dataUrl + '" frameborder="0" data-id="' + dataId + '" data-url="' + dataUrl + '"  data-parentId="' + parentId + '"  seamless></iframe>';
                $('.mainContent').find('iframe.JFine_iframe').hide();
                $('.mainContent').append(str1);
                $.loading(true);
                $('.mainContent iframe:visible').load(function () {
                    $.loading(false);
                });
                $('.menuTabs .page-tabs-content').append(str);
                $.jfinetab.scrollToTab($('.menuTab.active'));
            }
            $.jfineStack.pop(dataId);
            $.jfineStack.insert(dataId);
            return false;
        },
        scrollTabRight: function () {
            var marginLeftVal = Math.abs(parseInt($('.page-tabs-content').css('margin-left')));
            var tabOuterWidth = $.jfinetab.calSumWidth($(".content-tabs").children().not(".menuTabs"));
            var visibleWidth = $(".content-tabs").outerWidth(true) - tabOuterWidth;
            var scrollVal = 0;
            if ($(".page-tabs-content").width() < visibleWidth) {
                return false;
            } else {
                var tabElement = $(".menuTab:first");
                var offsetVal = 0;
                while ((offsetVal + $(tabElement).outerWidth(true)) <= marginLeftVal) {
                    offsetVal += $(tabElement).outerWidth(true);
                    tabElement = $(tabElement).next();
                }
                offsetVal = 0;
                while ((offsetVal + $(tabElement).outerWidth(true)) < (visibleWidth) && tabElement.length > 0) {
                    offsetVal += $(tabElement).outerWidth(true);
                    tabElement = $(tabElement).next();
                }
                scrollVal = $.jfinetab.calSumWidth($(tabElement).prevAll());
                if (scrollVal > 0) {
                    $('.page-tabs-content').animate({
                        marginLeft: 0 - scrollVal + 'px'
                    }, "fast");
                }
            }
        },
        scrollTabLeft: function () {
            var marginLeftVal = Math.abs(parseInt($('.page-tabs-content').css('margin-left')));
            var tabOuterWidth = $.jfinetab.calSumWidth($(".content-tabs").children().not(".menuTabs"));
            var visibleWidth = $(".content-tabs").outerWidth(true) - tabOuterWidth;
            var scrollVal = 0;
            if ($(".page-tabs-content").width() < visibleWidth) {
                return false;
            } else {
                var tabElement = $(".menuTab:first");
                var offsetVal = 0;
                while ((offsetVal + $(tabElement).outerWidth(true)) <= marginLeftVal) {
                    offsetVal += $(tabElement).outerWidth(true);
                    tabElement = $(tabElement).next();
                }
                offsetVal = 0;
                if ($.jfinetab.calSumWidth($(tabElement).prevAll()) > visibleWidth) {
                    while ((offsetVal + $(tabElement).outerWidth(true)) < (visibleWidth) && tabElement.length > 0) {
                        offsetVal += $(tabElement).outerWidth(true);
                        tabElement = $(tabElement).prev();
                    }
                    scrollVal = $.jfinetab.calSumWidth($(tabElement).prevAll());
                }
            }
            $('.page-tabs-content').animate({
                marginLeft: 0 - scrollVal + 'px'
            }, "fast");
        },
        scrollToTab: function (element) {
            var marginLeftVal = $.jfinetab.calSumWidth($(element).prevAll()), marginRightVal = $.jfinetab.calSumWidth($(element).nextAll());
            var tabOuterWidth = $.jfinetab.calSumWidth($(".content-tabs").children().not(".menuTabs"));
            var visibleWidth = $(".content-tabs").outerWidth(true) - tabOuterWidth;
            var scrollVal = 0;
            if ($(".page-tabs-content").outerWidth() < visibleWidth) {
                scrollVal = 0;
            } else if (marginRightVal <= (visibleWidth - $(element).outerWidth(true) - $(element).next().outerWidth(true))) {
                if ((visibleWidth - $(element).next().outerWidth(true)) > marginRightVal) {
                    scrollVal = marginLeftVal;
                    var tabElement = element;
                    while ((scrollVal - $(tabElement).outerWidth()) > ($(".page-tabs-content").outerWidth() - visibleWidth)) {
                        scrollVal -= $(tabElement).prev().outerWidth();
                        tabElement = $(tabElement).prev();
                    }
                }
            } else if (marginLeftVal > (visibleWidth - $(element).outerWidth(true) - $(element).prev().outerWidth(true))) {
                scrollVal = marginLeftVal - $(element).prev().outerWidth(true);
            }
            $('.page-tabs-content').animate({
                marginLeft: 0 - scrollVal + 'px'
            }, "fast");
        },
        calSumWidth: function (element) {
            var width = 0;
            $(element).each(function () {
                width += $(this).outerWidth(true);
            });
            return width;
        },
        init: function () {
            $('.menuItem').on('click', $.jfinetab.addTab);
            $('.menuTabs').on('click', '.menuTab i', $.jfinetab.closeTab);
            $('.menuTabs').on('click', '.menuTab', $.jfinetab.activeTab);
            $('.tabLeft').on('click', $.jfinetab.scrollTabLeft);
            $('.tabRight').on('click', $.jfinetab.scrollTabRight);
            $('.tabReload').on('click', $.jfinetab.refreshTab);
            $('.tabCloseCurrent').on('click', function () {
                $('.page-tabs-content').find('.active i').trigger("click");
            });
            $('.tabCloseAll').on('click', function () {
                $('.page-tabs-content').children("[data-id]").find('.fa-remove').each(function () {
                    $('.JFine_iframe[data-id="' + $(this).data('id') + '"]').remove();
                    $(this).parents('a').remove();
                });
                $('.page-tabs-content').children("[data-id]:first").each(function () {
                    $('.JFine_iframe[data-id="' + $(this).data('id') + '"]').show();
                    $(this).addClass("active");
                });
                $('.page-tabs-content').css("margin-left", "0");
            });
            $('.tabCloseOther').on('click', $.jfinetab.closeOtherTabs);
            $('.fullscreen').on('click', function () {
                if (!$(this).attr('fullscreen')) {
                    $(this).attr('fullscreen', 'true');
                    $.jfinetab.requestFullScreen();
                } else {
                    $(this).removeAttr('fullscreen')
                    $.jfinetab.exitFullscreen();
                }
            });
        }
    };
    $(function () {
        $.jfinetab.init();
    });
})(jQuery);