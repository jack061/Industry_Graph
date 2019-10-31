var chat;
$(function () {
    $("#chat-content").slimScroll({ height: "200px", railOpacity: .4 });
    // Reference the auto-generated proxy for the hub.
    chat = $.connection.bidChat;
    // 创建函数供Hub进行回调显示内容
    //实时沟通

    chat.client.receiveSupMessage = function ( account, name,message) {
        var nowStr = getNowFormatDate();
        $("#chat-content_2").append(
                       " <li><div class=\"jfine-chat-user\"><img src=\"/Content/img/headImage/" + account + ".png\" onerror=\"$.noFindHeadImage(this);\"><cite>" + htmlEncode(name) + "<i>" + nowStr + "</i></cite></div>"
                       + "<div class=\"jfine-chat-text\" > " + message + "</div></li>");
        if (!($(".jfine-chat-box").hasClass('active'))) {
            $("#chat_messagecount").text(parseInt($("#chat_messagecount").text()) + 1);
        }

       $('#jfine-chat-content').scrollTop($('#jfine-chat-content')[0].scrollHeight);
        

    };

    // 发送输入框获得焦点
    $("#clientMessage").focus();
    // Start the connection.
    $.connection.hub.start().done(function () {
        chat.server.connect(projectNo, useraccount,username, "0",IP);
        $("#sendmessage").click(function () {
            if ('' == $("#clientMessage").val()) {
                $("#clientMessage").focus();
            } else {
                send();
                $("#clientMessage").val(null);
            }

        });
        document.onkeydown = function (e) {
            var ev = document.all ? window.event : e;
            if (ev.keyCode === 13) {
                if ('' == $("#clientMessage").val()) {
                    $("#clientMessage").focus();
                } else {
                    send();
                    e.preventDefault();
                    $("#clientMessage").val(null);
                }
            }
        };
        function send() {
            //调用服务器端hub类的Send方法
            var tousers = "";
            var touserNames = "";
            $('.jfine-chat-list').find(':checkbox').each(function () {
                if ($(this).is(":checked")) {
                    tousers = tousers + "," + $(this).val();
                    touserNames = touserNames + "," + $(this).prev().html();
                }
            });
            if (tousers.length > 1) {
                tousers = tousers.substr(1);
                touserNames = touserNames.substr(1);
            }
            console.log(tousers);
            chat.server.sendMessage(projectNo, "0", useraccount, tousers, touserNames, $("#clientMessage").val(), "竞价");

            var nowStr = getNowFormatDate();
            $("#chat-content_2").append(
                       " <li class=\"jfine-chat-mine\"><div class=\"jfine-chat-user\"><img src=\"/Content/img/headImage/"
                       + useraccount + ".png\" onerror=\"$.noFindHeadImage(this);\"><cite style=\"top:-8px;\">From: "
                       + htmlEncode(username) + "<i>" + nowStr + "</i></cite></cite><cite style=\"top:6px;\">To: "
                       + htmlEncode(touserNames == "" ? "全体人员" : touserNames) + "</cite></div>"
                      + "<div class=\"jfine-chat-text\">" + $("#clientMessage").val() + "</div></li>");
            $('#jfine-chat-content').scrollTop($('#jfine-chat-content')[0].scrollHeight);

        }

    });
});

// This optional function html-encodes messages for display in the page.
function htmlEncode(value) {
    var encodedValue = $("<div />").text(value).html();
    return encodedValue;
}

function getNowFormatDate() {
    var date = new Date();
    var seperator1 = "-";
    var seperator2 = ":";
    var month = date.getMonth() + 1;
    var strDate = date.getDate();
    if (month >= 1 && month <= 9) {
        month = "0" + month;
    }
    if (strDate >= 0 && strDate <= 9) {
        strDate = "0" + strDate;
    }
    var currentdate = date.getFullYear() + seperator1 + month + seperator1 + strDate
            + " " + date.getHours() + seperator2 + date.getMinutes()
            + seperator2 + date.getSeconds();
    return currentdate;
}