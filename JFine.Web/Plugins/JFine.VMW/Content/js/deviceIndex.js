$(window).load(function () {
    $(".loading").fadeOut()
})
$(function () {
    //updateData();
    //init();
    randomDynamic();
    instrumentGraph();
    SpeedGraph();
    receiveData();
   
   

})
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

function receiveData() {
    // Declare a proxy to reference the hub. 
    modbusHub = $.connection.modbus;
    // Create a function that the hub can call to broadcast messages
    //StartBid
    modbusHub.client.ReceiveData = function (deviceId, paraCode, data, dataTime) {
        console.log("接收到数据");
        //$("#deviceId").val(deviceId);
        //$("#paraCode").val(paraCode);
        //$("#data").val(data);
        var reg = /T/g;
        dataTime = dataTime.replace(reg, ' ');
        //$("#dataTime").val(dataTime);
        //$("#data").append("设备Id:" + deviceId + ";参数编码:" + paraCode + ";数值：" + data + ";时间：" + dataTime + "<br>");
        $(".navboxall[deviceId='" + deviceId + "']").find("div[id='" + paraCode + "'] span:eq(1)").text(data);
    };
    // Start the connection.
    $.connection.hub.start().done(function () {
        console.log("modbusHub已连接");
    });
}

function SpeedGraph() {
    // 公共配置
    Highcharts.setOptions({
        chart: {
            type: 'solidgauge',
            backgroundColor: 'none',

        },
        title: null,
        pane: {
            center: ['55%', '85%'],
            size: '140%',
            startAngle: -90,
            endAngle: 90,
            background: {
                backgroundColor: 'none',
                innerRadius: '60%',
                outerRadius: '100%',
                shape: 'arc'
            }
        },
        tooltip: {
            enabled: false
        },
        yAxis: {
            stops: [
                [0.1, '#55BF3B'], // green
                [0.5, '#DDDF0D'], // yellow
                [0.9, '#DF5353'] // red
            ],
            lineWidth: 0,
            minorTickInterval: null,
            tickPixelInterval: 400,
            tickWidth: 0,
            title: {
                y: -70
            },
            labels: {
                y: 16
            }
        },
        plotOptions: {
            solidgauge: {
                dataLabels: {
                    y: 5,
                    borderWidth: 0,
                    useHTML: true
                }
            }
        }
    });
    // 速度仪表
    var chart1 = Highcharts.chart('container-speed', {
        yAxis: {
            min: 0,
            max: 200,
            title: {
                text: '电流'
            }
        },
        credits: {
            enabled: false
        },
        series: [{
            name: '电流',
            data: [80],
            dataLabels: {
                format: '<div style="text-align:center"><span style="font-size:25px;color:' +
                    ((Highcharts.theme && Highcharts.theme.contrastTextColor) || 'red') + '">{y}</span><br/>' +
                    '<span style="font-size:12px;color:silver">电流</span></div>'
            },
            tooltip: {
                valueSuffix: ' km/h'
            }
        }]
    });
    // 定时刷新数据
    setInterval(function () {
        var point,
            newVal,
            inc;
        if (chart1) {
            point = chart1.series[0].points[0];
            inc = Math.round((Math.random() - 0.5) * 100);
            newVal = point.y + inc;
            if (newVal < 0 || newVal > 200) {
                newVal = point.y - inc;
            }
            point.update(newVal);
        }
    }, 2000);

}

function randomDynamic() {
    Highcharts.setOptions({
        chart: {
            backgroundColor: 'none',

        },
        global: {
            useUTC: false
        }
    });
    function activeLastPointToolip(chart) {
        var points = chart.series[0].points;
        chart.tooltip.refresh(points[points.length - 1]);
    }
    var chart = Highcharts.chart('container4', {
        chart: {
            type: 'spline',
            marginRight: 10,
            events: {
                load: function () {
                    var series = this.series[0],
                        chart = this;
                    //activeLastPointToolip(chart);
                    setInterval(function () {
                        var x = (new Date()).getTime(), // 当前时间
                            y = Math.random();          // 随机值
                        series.addPoint([x, y], true, true);
                        //activeLastPointToolip(chart);
                    }, 1000);
                }
            }
        },
        title: {
            text: ''
        },
        xAxis: {
            type: 'datetime',
            tickPixelInterval: 150
        },
        yAxis: {
            title: {
                text: null
            }
        },
        tooltip: {
            formatter: function () {
                return '<b>' + this.series.name + '</b><br/>' +
                    Highcharts.dateFormat('%Y-%m-%d %H:%M:%S', this.x) + '<br/>' +
                    Highcharts.numberFormat(this.y, 2);
            }
        },
        legend: {
            enabled: false
        },
        credits: {
            enabled: false
        }, 
        series: [{
            name: '温度',
            data: (function () {
                // 生成随机值
                var data = [],
                    time = (new Date()).getTime(),
                    i;
                for (i = -19; i <= 0; i += 1) {
                    data.push({
                        x: time + i * 1000,
                        y: Math.random()
                    });
                }
                return data;
            }())
        }]
    });
}

function instrumentGraph() {
    var chart = Highcharts.chart('container5', {
        chart: {
            type: 'gauge',
            plotBorderWidth: 1,
            plotBackgroundColor: {
                linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                stops: [
                    [0, '#FFF4C6'],
                    [0.3, '#FFFFFF'],
                    [1, '#FFF4C6']
                ]
            },
            plotBackgroundImage: null,
            height: 100
        },
        exporting: {
            enabled: false //用来设置是否显示‘打印’,'导出'等功能按钮，不设置时默认为显示
        },
        credits: {
            enabled: false
        },
        title: {
            text: ''
        },
        pane: [{
            startAngle: -45,
            endAngle: 45,
            background: null,
            center: ['50%', '200%'],
            size: 230
        }],
        yAxis: [{
            min: -20,
            max: 6,
            minorTickPosition: 'outside',
            tickPosition: 'outside',
            labels: {
                rotation: 'auto',
                distance: 20
            },
            plotBands: [{
                from: 0,
                to: 6,
                color: '#C02316',
                innerRadius: '100%',
                outerRadius: '105%'
            }],
            pane: 0,
            title: {
                text: 'VU<br/><span style="font-size:8px">变频器频率</span>',
                y: -40
            }
        }],
        plotOptions: {
            gauge: {
                dataLabels: {
                    enabled: false
                },
                dial: {
                    radius: '100%'
                }
            }
        },
        series: [{
            data: [-20],
            yAxis: 0
        }]
    }, function (chart) {
        setInterval(function () {
            if (chart.series) { // the chart may be destroyed
                var left = chart.series[0].points[0],
                    leftVal,
                    inc = (Math.random() - 0.5) * 3;
                leftVal = left.y + inc;
                if (leftVal < -20 || leftVal > 6) {
                    leftVal = left.y - inc;
                }
                left.update(leftVal, false);
                chart.redraw();
            }
        }, 500);
    });
}














