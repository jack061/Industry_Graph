$(window).load(function () {
    $(".loading").fadeOut()
})
$(function () {
    updateData();
    init();
    Init_Event();
    LoadGraph(5);
    receiveData();
    //temptureDynamic();
    randomDynamic();
})
function init() {
    window.setInterval(updateData, 5000);
}
function updateData() {
    //根据Id获取最新的一条数据
    var $table = $(".table1");
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
                    $this.find("tr[id='" + data[i]["ParameterCode"] + "'] td:eq(1) span").text(data[i]["ParameterValue"]);
                }
            }
        })
    })
}

function Init_Event() {
    $(".mainbox .boxall").bind("click", function () {
        $(".graph div:eq(0) span").text($(this).find(".device_block").attr("deviceName"));
        var deviceId = $(this).find(".device_block").attr("deviceId")
        LoadGraph(deviceId);
    })
}

function LoadGraph(deviceId) {
    var powerData = LoadGraphData(deviceId, "power");
    var temperatureData = LoadGraphData(deviceId, "temperature");
    var humidityData = LoadGraphData(deviceId, "humidity");
    Highcharts.setOptions({
        lang: {
            rangeSelectorZoom: '', // 不显示 'zoom' 文字
            resetZoom: '重置',
            resetZoomTitle: '重置缩放比例'
        }
    })
    var chart = null;
    $('#container').highcharts('StockChart', {
        inverted: true,
        zoomType: 'x',
        responsive: {
            rules: [{
                condition: {
                    maxWidth: 500
                },
                chartOptions: {
                    legend: {
                        align: 'center',
                        verticalAlign: 'bottom',
                        layout: 'horizontal'
                    },
                    yAxis: {
                        labels: {
                            align: 'left',
                            x: 0,
                            y: -5
                        },
                        title: {
                            text: null
                        }
                    },
                    subtitle: {
                        text: null
                    },
                    credits: {
                        enabled: false
                    }
                }
            }]
        },
        rangeSelector: {
            buttons: [{
                type: 'hour',
                count: 1,
                text: '1小时'
            }, {
                type: 'day',
                count: 1,
                text: '一天'
            }, {
                type: 'day',
                count: 7,
                text: '七天'
            }, {
                type: 'month',
                count: 1,
                text: '一月'
            }, {
                type: 'all',
                count: 1,
                text: 'All'
            }],
            selected: 1,
            inputEnabled: true
        },
        title: {
            text: '电机电量采集'
        },
        series: [{
            name: '电量',
            data: powerData,
            tooltip: {
                valueDecimals: 2
            }
        }],
        xAxis: {
            events: {
                // 范围选择器改变的范围最终是改变坐标轴的范围，所以我们监听坐标的极值变更事件函数即可
                afterSetExtremes: function (e) {
                    // e.min 和 e.max 为坐标轴当前的范围
                    //alert(e.min + ":" + e.max);
                    console.log(e.min, e.max);
                }
            }
        },
        yAxis: { opposite: false },
        legend: { layout: "vertical", align: "right", verticalAlign: "middle" },
        plotOptions: { series: { label: { connectorAllowed: false }, pointStart: 2010 } },
        responsive: { rules: [{ condition: { maxWidth: 500 }, chartOptions: { legend: { layout: "horizontal", align: "center", verticalAlign: "bottom" } } }] }, function(c) {
            chart = c;
        }
    });

    $('#container2').highcharts('StockChart', {
        inverted: true,
        zoomType: 'x',
        responsive: {
            rules: [{
                condition: {
                    maxWidth: 500
                },
                chartOptions: {
                    legend: {
                        align: 'center',
                        verticalAlign: 'bottom',
                        layout: 'horizontal'
                    },
                    yAxis: {
                        labels: {
                            align: 'left',
                            x: 0,
                            y: -5
                        },
                        title: {
                            text: null
                        }
                    },
                    subtitle: {
                        text: null
                    },
                    credits: {
                        enabled: false
                    }
                }
            }]
        },
        rangeSelector: {
            buttons: [{
                type: 'hour',
                count: 1,
                text: '1小时'
            }, {
                type: 'day',
                count: 1,
                text: '一天'
            }, {
                type: 'day',
                count: 7,
                text: '七天'
            }, {
                type: 'month',
                count: 1,
                text: '一月'
            }, {
                type: 'all',
                count: 1,
                text: 'All'
            }],
            selected: 1,
            inputEnabled: true
        },
        title: {
            text: '电机温度采集'
        },
        series: [{
            name: '温度',
            data:temperatureData,
            tooltip: {
                valueDecimals: 2
            }
        }],
        xAxis: {
            events: {
                // 范围选择器改变的范围最终是改变坐标轴的范围，所以我们监听坐标的极值变更事件函数即可
                afterSetExtremes: function (e) {
                    // e.min 和 e.max 为坐标轴当前的范围
                    //alert(e.min + ":" + e.max);
                    console.log(e.min, e.max);
                }
            }
        },
        yAxis: { opposite: false },
        legend: { layout: "vertical", align: "right", verticalAlign: "middle" },
        plotOptions: { series: { label: { connectorAllowed: false }, pointStart: 2010 } },
        responsive: { rules: [{ condition: { maxWidth: 500 }, chartOptions: { legend: { layout: "horizontal", align: "center", verticalAlign: "bottom" } } }] }, function(c) {
            chart = c;
        }
    });

    $('#container3').highcharts('StockChart', {
        inverted: true,
        zoomType: 'x',
        responsive: {
            rules: [{
                condition: {
                    maxWidth: 500
                },
                chartOptions: {
                    legend: {
                        align: 'center',
                        verticalAlign: 'bottom',
                        layout: 'horizontal'
                    },
                    yAxis: {
                        labels: {
                            align: 'left',
                            x: 0,
                            y: -5
                        },
                        title: {
                            text: null
                        }
                    },
                    subtitle: {
                        text: null
                    },
                    credits: {
                        enabled: false
                    }
                }
            }]
        },
        rangeSelector: {
            buttons: [{
                type: 'hour',
                count: 1,
                text: '1小时'
            }, {
                type: 'day',
                count: 1,
                text: '一天'
            }, {
                type: 'day',
                count: 7,
                text: '七天'
            }, {
                type: 'month',
                count: 1,
                text: '一月'
            }, {
                type: 'all',
                count: 1,
                text: 'All'
            }],
            selected: 1,
            inputEnabled: true
        },
        title: {
            text: '电机湿度采集'
        },
        series: [{
            name: '湿度',
            data:humidityData,
            tooltip: {
                valueDecimals: 2
            }
        }],
        xAxis: {
            events: {
                // 范围选择器改变的范围最终是改变坐标轴的范围，所以我们监听坐标的极值变更事件函数即可
                afterSetExtremes: function (e) {
                    // e.min 和 e.max 为坐标轴当前的范围
                    //alert(e.min + ":" + e.max);
                    console.log(e.min, e.max);
                }
            }
        },
        yAxis: { opposite: false },
        legend: {
            layout: 'vertical',
            backgroundColor: '#FFFFFF',
            floating: true,
            align: 'left',
            x: 100,
            verticalAlign: 'top',
            y: 70
        },
        plotOptions: { series: { label: { connectorAllowed: false }, pointStart: 2010 } },
        responsive: { rules: [{ condition: { maxWidth: 500 }, chartOptions: { legend: { layout: "horizontal", align: "center", verticalAlign: "bottom" } } }] }, function(c) {
            chart = c;
        }
    });
}
function LoadGraphData(deviceId, ParameterCode) {
    var graphArray = [];
    $.ajax({
        url: '/GatewayVal/GetHistoryData',
        data: {
            queryJson: JSON.stringify({ deviceId: deviceId, ParameterCode: ParameterCode })
        },
        async: false,
        dataType: "json",
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                graphArray.push([data[i]["timeSpan"], parseInt(data[i]["ParameterValue"])]);
            }
        }
    })
    return graphArray;
}

function temptureDynamic() {
    var data_r=[];
    Highcharts.setOptions({
        global: {
            useUTC: false
        }
    });

    var chart;
    chart = new Highcharts.Chart({
        chart: {
            renderTo: 'container4',
            defaultSeriesType: 'spline',
            marginRight: 10,
            events: {
                //load: getForm
            }
        },
        title: {
            text: '实时参数曲线'
        },
        xAxis: {
            type: 'datetime',
            tickPixelInterval: 100
        },
        yAxis: {
            title: {
                text: 'Value'
            },
            plotLines: [{
                value: 0,
                width: 1,
                color: '#808080'
            }]
        },
        tooltip: {
            formatter: function () {
                return '<b>' + this.series.name + '</b><br/>' +
                    Highcharts.dateFormat('%Y-%m-%d %H:%M:%S', this.x) + '<br/>' +
                    Highcharts.numberFormat(this.y, 2);
            }
        },
        legend: {
            layout: 'vertical',
            align: 'right',
            verticalAlign: 'top',
            x: -10,
            y: 100,
            borderWidth: 0
        },
        exporting: {
            enabled: false
        },
        series: [{
            name: '参数',
            data: []
        }]
    });
    // Declare a proxy to reference the hub. 
    modbusHub = $.connection.modbus;
    // Create a function that the hub can call to broadcast messages
    //StartBid
    modbusHub.client.ReceiveData = function (deviceId, paraCode, data, dataTime) {
        console.log("接收到数据");
        chart.series[0].setData(data_r.push(data));
        //$("#data").append("设备Id:" + deviceId + ";参数编码:" + paraCode + ";数值：" + data + ";时间：" + dataTime + "<br>");
    };
    // Start the connection.
    $.connection.hub.start().done(function () {
        console.log("modbusHub已连接");
    });
}

function randomDynamic() {
    Highcharts.setOptions({
        global: {
            useUTC: false
        }
    });
    // Create the chart
    Highcharts.stockChart('container4', {
        chart: {
            events: {
                load: function () {
                    // set up the updating of the chart each second
                    var series = this.series[0];
                    modbusHub = $.connection.modbus;
                    modbusHub.client.ReceiveData = function (deviceId, paraCode, data, dataTime) {
                        var x = new Date(dataTime).getTime(), // current time
                        //var x = (new Date()).getTime(), // current time
                            y = parseInt(data);
                        series.addPoint([x, y], true, true);
                        //chart.series[0].setData(data_r.push(data));
                    };
                    //setInterval(function () {
                    //    var x = (new Date()).getTime(), // current time
                    //        y = Math.round(Math.random() * 100);
                    //    series.addPoint([x, y], true, true);
                    //}, 1000);
                }
            }
        },
        rangeSelector: {
            buttons: [{
                count: 1,
                type: 'minute',
                text: '1分钟'
            }, {
                count: 5,
                type: 'minute',
                text: '5分钟'
            }, {
                type: 'all',
                text: '全部'
            }],
            inputEnabled: false,
            selected: 0
        },
        title: {
            text: ''
        },
        tooltip: {
            split: false
        },
        exporting: {
            enabled: false
        },
        series: [{
            name: '温度',
          
            data: (function () {
                // generate an array of random data
                var data = [], time = (new Date()).getTime(), i;
                data.push([
                    time + i * 1000,
                    0
                ]);
                for (i = -999; i <= 0; i += 1) {
                    data.push([
                        time + i * 1000,
                        Math.round(Math.random() * 100)
                    ]);
                }
                return data;
            }())
        }]
    });
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
        //$(".navboxall[deviceId='" + deviceId + "']").find("div[id='" + paraCode + "'] span:eq(1)").text(data);
    };
    // Start the connection.
    $.connection.hub.start().done(function () {
        console.log("modbusHub已连接");
    });
}


















