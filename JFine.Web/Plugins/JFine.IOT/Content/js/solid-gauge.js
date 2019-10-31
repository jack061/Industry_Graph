/*
  Highcharts JS v7.1.2 (2019-06-03)

 Solid angular gauge module

 (c) 2010-2019 Torstein Honsi

 License: www.highcharts.com/license
*/
(function (d) { "object" === typeof module && module.exports ? (d["default"] = d, module.exports = d) : "function" === typeof define && define.amd ? define("highcharts/modules/solid-gauge", ["highcharts", "highcharts/highcharts-more"], function (e) { d(e); d.Highcharts = e; return d }) : d("undefined" !== typeof Highcharts ? Highcharts : void 0) })(function (d) {
    function e(g, d, e, l) { g.hasOwnProperty(d) || (g[d] = l.apply(null, e)) } d = d ? d._modules : {}; e(d, "modules/solid-gauge.src.js", [d["parts/Globals.js"]], function (g) {
        var d = g.pInt, e = g.pick, l = g.isNumber,
        w = g.wrap, t; w(g.Renderer.prototype.symbols, "arc", function (a, f, h, c, d, b) { a = a(f, h, c, d, b); b.rounded && (c = ((b.r || c) - b.innerR) / 2, b = ["A", c, c, 0, 1, 1, a[12], a[13]], a.splice.apply(a, [a.length - 1, 0].concat(["A", c, c, 0, 1, 1, a[1], a[2]])), a.splice.apply(a, [11, 3].concat(b))); return a }); t = {
            initDataClasses: function (a) {
                var d = this.chart, h, c = 0, e = this.options; this.dataClasses = h = []; a.dataClasses.forEach(function (b, f) {
                    b = g.merge(b); h.push(b); b.color || ("category" === e.dataClassColor ? (f = d.options.colors, b.color = f[c++], c === f.length &&
                        (c = 0)) : b.color = g.color(e.minColor).tweenTo(g.color(e.maxColor), f / (a.dataClasses.length - 1)))
                })
            }, initStops: function (a) { this.stops = a.stops || [[0, this.options.minColor], [1, this.options.maxColor]]; this.stops.forEach(function (a) { a.color = g.color(a[1]) }) }, toColor: function (a, d) {
                var h = this.stops, c, g, b = this.dataClasses, f, e; if (b) for (e = b.length; e--;) { if (f = b[e], c = f.from, h = f.to, (void 0 === c || a >= c) && (void 0 === h || a <= h)) { g = f.color; d && (d.dataClass = e); break } } else {
                this.isLog && (a = this.val2lin(a)); a = 1 - (this.max - a) / (this.max -
                    this.min); for (e = h.length; e-- && !(a > h[e][0]);); c = h[e] || h[e + 1]; h = h[e + 1] || c; a = 1 - (h[0] - a) / (h[0] - c[0] || 1); g = c.color.tweenTo(h.color, a)
                } return g
            }
        }; g.seriesType("solidgauge", "gauge", { colorByPoint: !0, dataLabels: { y: 0 } }, {
            drawLegendSymbol: g.LegendSymbolMixin.drawRectangle, translate: function () { var a = this.yAxis; g.extend(a, t); !a.dataClasses && a.options.dataClasses && a.initDataClasses(a.options); a.initStops(a.options); g.seriesTypes.gauge.prototype.translate.call(this) }, drawPoints: function () {
                var a = this, f = a.yAxis, h =
                    f.center, c = a.options, t = a.chart.renderer, b = c.overshoot, u = l(b) ? b / 180 * Math.PI : 0, v; l(c.threshold) && (v = f.startAngleRad + f.translate(c.threshold, null, null, null, !0)); this.thresholdAngleRad = e(v, f.startAngleRad); a.points.forEach(function (b) {
                        if (!b.isNull) {
                            var m = b.graphic, k = f.startAngleRad + f.translate(b.y, null, null, null, !0), r = d(e(b.options.radius, c.radius, 100)) * h[2] / 200, n = d(e(b.options.innerRadius, c.innerRadius, 60)) * h[2] / 200, p = f.toColor(b.y, b), q = Math.min(f.startAngleRad, f.endAngleRad), l = Math.max(f.startAngleRad,
                                f.endAngleRad); "none" === p && (p = b.color || a.color || "none"); "none" !== p && (b.color = p); k = Math.max(q - u, Math.min(l + u, k)); !1 === c.wrap && (k = Math.max(q, Math.min(l, k))); q = Math.min(k, a.thresholdAngleRad); k = Math.max(k, a.thresholdAngleRad); k - q > 2 * Math.PI && (k = q + 2 * Math.PI); b.shapeArgs = n = { x: h[0], y: h[1], r: r, innerR: n, start: q, end: k, rounded: c.rounded }; b.startR = r; m ? (r = n.d, m.animate(g.extend({ fill: p }, n)), r && (n.d = r)) : (b.graphic = m = t.arc(n).attr({ fill: p, "sweep-flag": 0 }).add(a.group), a.chart.styledMode || ("square" !== c.linecap &&
                                    m.attr({ "stroke-linecap": "round", "stroke-linejoin": "round" }), m.attr({ stroke: c.borderColor || "none", "stroke-width": c.borderWidth || 0 }))); m && m.addClass(b.getClassName(), !0)
                        }
                    })
            }, animate: function (a) { a || (this.startAngleRad = this.thresholdAngleRad, g.seriesTypes.pie.prototype.animate.call(this, a)) }
        })
    }); e(d, "masters/modules/solid-gauge.src.js", [], function () { })
});
//# sourceMappingURL=solid-gauge.js.map