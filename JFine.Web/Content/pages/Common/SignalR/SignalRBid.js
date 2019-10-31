var S_Bid;
$(function () {
    // Declare a proxy to reference the hub. 
    S_Bid = $.connection.bid;
    // Create a function that the hub can call to broadcast messages
    //StartBid
    S_Bid.client.BidStart = function (projectId, bidStartTime, bidEndTime, delayEndTime) {
        var reg = /T/g;
        bidStartTime = bidStartTime.replace(reg, ' ');
        bidEndTime = bidEndTime.replace(reg, ' ');
        delayEndTime = delayEndTime.replace(reg, ' ');
        if ($("#Id").val() == projectId) {
            $("#BidBeginDate").html(bidStartTime);
            $("#BidEndDate").html(bidEndTime);
            $("#DelayEndDate").html(delayEndTime);
            $("input[name=IsStart]").val('是');
            initTimer();
            playVoice("bidStart");
            $.modalAlert("现在开始进行竞价。。。", "success");
        }
    };
    // Start the connection.
    $.connection.hub.start().done(function () {
        console.log("S_Bid已连接");
    });
});

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


