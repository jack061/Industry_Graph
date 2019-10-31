$(function () {
    $(".open-small-chat").click(function () {
        $(this).children().toggleClass("fa-comments").toggleClass("fa-remove");
        $(".jfine-chat-box").toggleClass("active");
        $("#chat_messagecount").text('0');

    }),
    $("ul.jfine-chat-list > li").click(function () {
        ipt = $('input', this)[0];
        ipt.checked = !ipt.checked;
        $(this).toggleClass("active");
    });
});

function closeThisChat() {
    $(".open-small-chat").children().toggleClass("fa-comments").toggleClass("fa-remove"), $(".jfine-chat-box").toggleClass("active")
}