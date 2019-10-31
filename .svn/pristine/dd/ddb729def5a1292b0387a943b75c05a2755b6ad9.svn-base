
var mySkins = [
        'skin-blue',
        'skin-black',
        'skin-red',
        'skin-yellow',
        'skin-purple',
        'skin-green',
        'skin-blue-light',
        'skin-black-light',
        'skin-red-light',
        'skin-yellow-light',
        'skin-purple-light',
        'skin-green-light'
];
$(function () {
    var tmp = $.getLocal('skin');
    if (tmp && $.inArray(tmp, mySkins)) {
        changeSkin(tmp);
    } else
    {
        changeSkin(defatultSkin);
    }        
})
$.fn.removeClassPrefix = function (prefix) {
    this.each(function (i, el) {
        var classes = el.className.split(" ").filter(function (c) {
            return c.lastIndexOf(prefix, 0) !== 0;
        });
        el.className = classes.join(" ");
    });
    return this;
};
function changeSkin(cls) {
    $.each(mySkins, function (i) {
        $('body').removeClass(mySkins[i])
    })
    $('body').addClass(cls);
    $.store('skin', cls);
    return false
}

function changeSkinTemp(cls) {
    $.each(mySkins, function (i) {
        $('body').removeClass(mySkins[i])
    })
    $('body').addClass(cls);
    return false
}
$(function ($) {
    $('#config-tool-cog').on('click', function () { $('#config-tool').toggleClass('closed'); }); $('#config-fixed-header').on('change', function () {
        var fixedHeader = '';
        if ($(this).is(':checked')) {
            $('body').addClass('fixed-header'); fixedHeader = 'fixed-header';
        }
        else {
            $('body').removeClass('fixed-header');
            if ($('#config-fixed-sidebar').is(':checked')) {
                $('#config-fixed-sidebar').prop('checked', false);
                $('#config-fixed-sidebar').trigger('change'); location.reload();
            }
        }
    });
    $(".sidebar-toggle").click(function () {
        if (!$("body").hasClass("sidebar-collapse")) {
            $("body").addClass("sidebar-collapse");
        } else {
            $("body").removeClass("sidebar-collapse");
        }
    });
});
$(function ($) {
    $("#content-wrapper").find('.mainContent').height($(window).height() - 100);
    $(window).resize(function (e) {
        $("#content-wrapper").find('.mainContent').height($(window).height() - 100);
    });
    $('#sidebar-nav,#nav-col-submenu').on('click', '.dropdown-toggle', function (e) {
        e.preventDefault();
        var $item = $(this).parent();
        if (!$item.hasClass('open')) {
            $item.parent().find('.open .submenu').slideUp('fast');
            $item.parent().find('.open').toggleClass('open');
        }
        $item.toggleClass('open');
        if ($item.hasClass('open')) {
            $item.children('.submenu').slideDown('fast', function () {
                var _height1 = $(window).height() - 92 - $item.position().top;
                var _height2 = $item.find('ul.submenu').height() + 10;
                var _height3 = _height2 > _height1 ? _height1 : _height2;
                $item.find('ul.submenu').css({
                    overflow: "auto",
                    height: _height3
                })
            });
        }
        else {
            $item.children('.submenu').slideUp('fast');
        }
    });
    GetLoadNav();
    $('body').on('mouseenter', '#page-wrapper.nav-small #sidebar-nav .dropdown-toggle', function (e) {
        if ($(document).width() >= 992) {
            var $item = $(this).parent();
            if ($('body').hasClass('fixed-leftmenu')) {
                var topPosition = $item.position().top;

                if ((topPosition + 4 * $(this).outerHeight()) >= $(window).height()) {
                    topPosition -= 6 * $(this).outerHeight();
                }
                $('#nav-col-submenu').html($item.children('.submenu').clone());
                $('#nav-col-submenu > .submenu').css({ 'top': topPosition });
            }

            $item.addClass('open');
            $item.children('.submenu').slideDown('fast');
        }
    });
    $('body').on('mouseleave', '#page-wrapper.nav-small #sidebar-nav > .nav-pills > li', function (e) {
        if ($(document).width() >= 992) {
            var $item = $(this);
            if ($item.hasClass('open')) {
                $item.find('.open .submenu').slideUp('fast');
                $item.find('.open').removeClass('open');
                $item.children('.submenu').slideUp('fast');
            }
            $item.removeClass('open');
        }
    });
    $('body').on('mouseenter', '#page-wrapper.nav-small #sidebar-nav a:not(.dropdown-toggle)', function (e) {
        if ($('body').hasClass('fixed-leftmenu')) {
            $('#nav-col-submenu').html('');
        }
    });
    $('body').on('mouseleave', '#page-wrapper.nav-small #nav-col', function (e) {
        if ($('body').hasClass('fixed-leftmenu')) {
            $('#nav-col-submenu').html('');
        }
    });
    $('body').find('#make-small-nav').click(function (e) {
        $('#page-wrapper').toggleClass('nav-small');
    });
    $('body').find('.mobile-search').click(function (e) {
        e.preventDefault();
        $('.mobile-search').addClass('active');
        $('.mobile-search form input.form-control').focus();
    });
    $(document).mouseup(function (e) {
        var container = $('.mobile-search');
        if (!container.is(e.target) && container.has(e.target).length === 0) // ... nor a descendant of the container
        {
            container.removeClass('active');
        }
    });
    $(window).load(function () {
        window.setTimeout(function () {
            $('#ajax-loader').fadeOut();
        }, 300);
    });
});
function GetLoadNav() {
    var data = top.clients.authorizeMenu;
    var _html = "";
    $.each(data, function (i) {
        var row = data[i];
        if (row.BindId == "0") {
            if (i == 0) {
                _html += '<li class="treeview active">';
            } else {
                _html += '<li class="treeview">';
            }
            _html += '<a data-id="' + row.Id + '" href="#" class="dropdown-toggle"><i class="' + row.Icon + '"></i><span>' + row.Name + '</span><i class="fa fa-angle-right pull-right"></i></a>';
            var childNodes = row.ChildNodes;
            if (childNodes.length > 0) {
                _html += '<ul class="treeview-menu">';
                $.each(childNodes, function (i) {
                    var subrow = childNodes[i];
                    var subChildNodes = subrow.ChildNodes;
                    _html += '<li>';
                    if (subChildNodes.length > 0) {
                        _html += '<a data-id="' + subrow.Id + '" href="#" class="dropdown-toggle"><i class="' + subrow.Icon + '"></i><span>' + subrow.Name + '</span><i class="fa fa-angle-right pull-right"></i></a>';
                        _html += '<ul class="treeview-menu">';
                        $.each(subChildNodes, function (i) {
                            var subChildNodesRow = subChildNodes[i];
                            _html += '<li><a class="menuItem" data-id="' + subChildNodesRow.Id + '" href="' + subChildNodesRow.UrlAddress + '" data-index="' + subChildNodesRow.Sort + '" data-target="' + subChildNodesRow.Target + '"><i class="' + subChildNodesRow.Icon + '"></i>' + subChildNodesRow.Name + '</a></li>';
                        });
                        _html += '</ul>';
                    } else {
                        _html += '<a class="menuItem" data-id="' + subrow.Id + '" href="' + subrow.UrlAddress + '" data-index="' + subrow.Sort + '" data-target="' + subrow.Target + '"><i class="' + subrow.Icon + '"></i>' + subrow.Name + '</a>';
                    }
                   _html += '</li>';
                });
                _html += '</ul>';
            }
            _html += '</li>';
        }
    });
    //$("#sidebar-nav ul").prepend(_html);
    $("#sidebar-menu").append(_html);
    $("#sidebar-menu li a").click(function () {
        var d = $(this), e = d.next();
        if (e.is(".treeview-menu") && e.is(":visible")) {
            e.slideUp(500, function () {
                e.removeClass("menu-open")
            }),
            e.parent("li").removeClass("active")
        } else if (e.is(".treeview-menu") && !e.is(":visible")) {
            var f = d.parents("ul").first(),
            g = f.find("ul:visible").slideUp(500);
            g.removeClass("menu-open");
            var h = d.parent("li");
            e.slideDown(500, function () {
                e.addClass("menu-open"),
                f.find("li.active").removeClass("active"),
                h.addClass("active");

                var _height1 = $(window).height() - $("#sidebar-menu >li.active").position().top - 41;
                var _height2 = $("#sidebar-menu li > ul.menu-open").height() + 10
                if (_height2 > _height1) {
                    $("#sidebar-menu >li > ul.menu-open").css({
                        overflow: "auto",
                        height: _height1
                    })
                }
            })
        }
        e.is(".treeview-menu");
    });
}