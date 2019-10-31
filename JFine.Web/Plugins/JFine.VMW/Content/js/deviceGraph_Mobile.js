$(window).load(function () {
    $(".loading").fadeOut()
})
$(function () {
    updateData();
    init();
    Init_Event();
    LoadGraph(5);
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
    $(".mainbox table").bind("click", function () {
        $(".graph div:eq(0) span").text($(this).attr("deviceName"));
        var deviceId = $(this).attr("deviceId")
        LoadGraph(deviceId);
    })
}

function LoadGraph(deviceId) {
    var data = [[1559039337000, 414],
    [1559039937000, 116],
    [1559040537000, 575],
    [1559041137000, 612],
    [1559041737000, 768]];
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
















