var signalR_vmw;
$(function () {
    // Declare a proxy to reference the hub. 
    signalR_vmw = $.connection.vmw;
    // Create a function that the hub can call to broadcast messages
    //预警通知
    signalR_vmw.client.warn = function (warnInfo, cameraInfo) {
        console.log("signalR_vmw收到信息：" + warnInfo);
        var warnInfoJson = JSON.parse(warnInfo);
        addWarn(warnInfoJson);

        var cameraInfoJson = JSON.parse(cameraInfo);
        updateCameraInfo(cameraInfoJson);

        //语音提示
        playAudio();
    };
    //处理通知
    //type:0:确认；1：处理；2：忽略；
    signalR_vmw.client.deal = function (warningId,type,cameraInfo) {

        var cameraInfoJson = JSON.parse(cameraInfo);
        updateCameraInfo(cameraInfoJson);
    };
    // Start the connection.
    $.connection.hub.start().done(function () {
        console.log("signalR_vmw已连接");
    });
});


