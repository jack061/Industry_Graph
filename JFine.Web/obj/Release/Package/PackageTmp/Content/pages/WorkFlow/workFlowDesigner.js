var property = {
    width: 1200,
    height: 540,
    toolBtns: ["start round mix", "end round", "task", "node", "chat", "state", "plug", "join", "fork", "complex mix"],
    haveHead: true,
    headLabel: true,
    headBtns: ["new", "open", "save", "undo", "redo", "reload", "print"],//如果haveHead=true，则定义HEAD区的按钮
    haveTool: true,
    haveDashed: true,
    haveGroup: true,
    useOperStack: true
};
var remark = {
    cursor: "选择指针",
    direct: "结点连线",
    dashed: "关联虚线",
    start: "入口结点",
    "end": "结束结点",
    "task": "任务结点",
    node: "自动结点",
    chat: "决策结点",
    state: "状态结点",
    plug: "附加插件",
    fork: "分支结点",
    "join": "联合结点",
    "complex": "复合结点",
    group: "组织划分框编辑开关"
};
var flowDesigner;
$(document).ready(function () {
    flowDesigner = $.createGooFlow($("#workFlowDesign"), property);
    flowDesigner.setNodeRemarks(remark);
    //flowDesigner.loadData(jsondata);
    //demo.reinitSize(1000,520);
    flowDesigner.onItemRightClick = function (id, type) {
        console.log("onItemRightClick: " + id + "," + type);
        
        return false;//返回false可以阻止浏览器默认的右键菜单事件
    }
    flowDesigner.onItemDbClick = function (id, type) {
        console.log("onItemDbClick: " + id + "," + type);
        var node = flowDesigner.getItemInfo(id, type);
        if (type == "node")
        {            
            $.modalOpen({
                id: "NodeForm",
                title: "节点设置-" + node.name,
                url: "/WorkFlow/WorkFlow/NodeForm?Id="+id,
                width: "650px",
                height: "580px",
                callBack: function (iframeId) {
                    top.frames[iframeId].submitForm();
                }
            });
        } else if (type == "line")
        {
            $.modalOpen({
                id: "LineForm",
                title: "连线设置-" + node.name,
                url: "/WorkFlow/WorkFlow/LineForm?Id=" + id,
                width: "650px",
                height: "580px",
                callBack: function (iframeId) {
                    top.frames[iframeId].submitForm();
                }
            });
        } else {

        }
        return false;//返回false可以阻止原组件自带的双击直接编辑事件
    }
    flowDesigner.onPrintClick = function () {
        flowDesigner.print(0.8);
    }
});
//导出流程设计数据
function ExportWFData() {
    var wf_data = JSON.stringify(flowDesigner.exportData());
    return wf_data;
}