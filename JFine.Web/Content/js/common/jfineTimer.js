
var jfineTimer = {
    getTimer: function (options)
    {
        var Timer = {
            flag:0,//0:未开始；1：正在进行中；2：已经结束
            nowdate: new Date(),
            startdate: new Date(),
            enddate: new Date(),
            H: 0,
            M: 0,
            S: 0,
            MS: 0,
            data: [0, 0, 0, 0, 0, 0, 0],
            ifServerTime: false,
            serverTimeURL:"",
            param: {},
            init: function ()
            {
                var curentTimer = this;
                if (curentTimer.ifServerTime)
                {
                    curentTimer.nowdate = getServerDate(curentTimer.serverTimeURL);
                }
                var startcounts = curentTimer.startdate.getTime();
                var endcounts = curentTimer.enddate.getTime();
                var nowcounts = curentTimer.nowdate.getTime();
                if (nowcounts >= startcounts && endcounts >= nowcounts)
                {
                    curentTimer.flag = 1;
                    var leftcounts = endcounts - nowcounts;
                    curentTimer.H = parseInt(leftcounts / (1000 * 60 * 60));
                    curentTimer.M = parseInt((leftcounts - curentTimer.H * (1000 * 60 * 60)) / (1000 * 60));
                    curentTimer.S = parseInt((leftcounts - curentTimer.H * (1000 * 60 * 60) - curentTimer.M * (1000 * 60)) / (1000));
                    curentTimer.MS = parseInt((leftcounts - curentTimer.H * (1000 * 60 * 60) - curentTimer.M * (1000 * 60) - curentTimer.S * 1000) / (100));
                    curentTimer.begin();
                    curentTimer.begineEvent();

                } else if (nowcounts < startcounts)
                {
                    curentTimer.flag = 0;
                } else if (nowcounts > endcounts)
                {
                    curentTimer.flag = 2;
                }                
            },
            begin: function ()
            {
                var curentTimer = this;
                setTimeout(function () {
                    //判断是否正在进行中
                    if (curentTimer.flag == 1) {
                        if (curentTimer.MS > 0)
                        {
                            curentTimer.MS = curentTimer.MS - 1;
                        } else if (curentTimer.S > 0)
                        {
                            curentTimer.MS = 9;
                            curentTimer.S = curentTimer.S - 1;
                        } else if (curentTimer.M > 0) {
                            curentTimer.MS = 9;
                            curentTimer.S = 59;
                            curentTimer.M = curentTimer.M - 1;
                        } else if (curentTimer.H > 0) {
                            curentTimer.MS = 9;
                            curentTimer.S = 59;
                            curentTimer.M = 59;
                            curentTimer.H = curentTimer.H - 1;
                        }
                        curentTimer.middleEvent();
                        
                        if (curentTimer.S == 0 && curentTimer.M == 0 && curentTimer.H == 0)
                        {
                            curentTimer.MS = 0;                            
                            curentTimer.flag = 2;
                            curentTimer.middleEvent();
                            curentTimer.endEvent();

                        }
                        curentTimer.begin();
                    }
                }, 100);
            },
            set: function (startDate,endDate)
            {
                var curentTimer = this;
                curentTimer.startdate = startDate;
                curentTimer.enddate = endDate;
                if (curentTimer.ifServerTime) {
                    curentTimer.nowdate = getServerDate(curentTimer.serverTimeURL);
                }
                var startcounts = curentTimer.startdate.getTime();
                var endcounts = curentTimer.enddate.getTime();
                var nowcounts = curentTimer.nowdate.getTime();
                if (nowcounts >= startcounts && endcounts >= nowcounts) {
                    var leftcounts = endcounts - nowcounts;
                    curentTimer.H = parseInt(leftcounts / (1000 * 60 * 60));
                    curentTimer.M = parseInt((leftcounts - curentTimer.H * (1000 * 60 * 60)) / (1000 * 60));
                    curentTimer.S = parseInt((leftcounts - curentTimer.H * (1000 * 60 * 60) - curentTimer.M * (1000 * 60)) / (1000));
                    curentTimer.MS = parseInt((leftcounts - curentTimer.H * (1000 * 60 * 60) - curentTimer.M * (1000 * 60) - curentTimer.S * 1000) / (100));
                    if (curentTimer.flag != 1)
                    {
                        curentTimer.flag = 1;
                        curentTimer.begin();
                    }                    
                }
            },
            reset: function ()
            {
                curentTimer.H = 0;
                curentTimer.M = 0;
                curentTimer.S = 0;
                curentTimer.MS = 0;
                curentTimer.data = [0, 0, 0, 0, 0, 0, 0];
                curentTimer.flag = false;
            },
            begineEvent: function ()
            {
                
            },
            middleEvent: function ()
            {

            },
            endEvent: function ()
            {
                
            },
        };
        var Timer = $.extend(Timer, options);
        return Timer;
    }
}
function getServerDate(url) {
    var current = null;
    if (url != '' && url != undefined && url != null) {
        $.ajax({
            url: url + "?timestamp=" + new Date().getTime(),
            dataType: 'json',
            async: false,
            type: "post",
            success: function (result, status, xhr) {//返回json格式{"status":"T","msg":"2018-05-01 00:00:00"}
                current = new Date(result.msg.replace(/-/g, "/"));//获取时间1
                //current = new Date(xhr.getResponseHeader("Date"));//获取时间2
            }
        });
    } else {
        current = new Date();
    }
    return current;
}
