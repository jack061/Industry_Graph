$(window).load(function () {
    $(".loading").fadeOut()
})
$(function () {
    updateData();
    init();
 
    function init() {
        window.setInterval(updateData, 5000);
    }
    function updateData() {
        //根据Id获取最新的一条数据
        var $table = $(".navboxall");
        $.each($table, function () {
            var deviceId = $(this).attr("deviceId");
            var $this = $(this);
            $.ajax({
                url: '/GatewayVal/GetNewestData',
                data: {
                    queryJson: JSON.stringify({ deviceId: deviceId })
                },
                async: false,
                dataType: "json",
                success: function (data) {
                    for (var i = 0; i < data.length; i++) {
                        $this.find("div[id='" + data[i]["ParameterCode"] + "'] span:eq(1)").text(data[i]["ParameterValue"]);
                        //$this.find("tr[id='" + data[i]["ParameterCode"] + "'] td:eq(1) span").text(data[i]["ParameterValue"]);
                    }
                }
            })
        })
    }
})


















