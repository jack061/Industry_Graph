
function checkDayStr(obj, text, minNum, maxNum) {
    var numbers = obj.value.split(",");
    var mn = 0 + minNum;
    var mx = 0 + maxNum;
    for (var i = 0; i < numbers.length; i++) {
        // 测试整数
        var num = parseInt(numbers[i]);
        if (!checkNanN(num)) {
            alert(text + 必须为逗号分割的数字 + "!");
            return false

        }
        if (num < minNum || num > maxNum) {
            alert(每周天数字必须在 + minNum + 到 + maxNum + 之内 + "!");
            return false;
        }

    }
    if (!checkSort(numbers)) {
        alert(请从小到大录入 + "!");
        return false;
    }
    return true;
}

/**
 * 
 * @param cls	输入类
 * @param type	校验类型，type=job，校验实现类，type =trigger校验触发器
 * @return
 */
function checkClassz(dom, classzInfo, type) {
    var params = {
        sid: document.all.sid.value,
        type: type,
        classzInfo: classzInfo,
        cmd: "AWS_Sys_Schedule_Check_Class"
    };

    Ext.get(dom).load("../ajax", params);
}

function $(arg) {
    return document.getElementById(arg) ? document.getElementById(arg) : document.getElementsByName(arg)[0];
}

//trigger action...
function $v(id) {
    var obj = Ext.getDom(id);
    return obj.value;
}

function timezChange(val) {
    if (val == 0) {
        Ext.getDom("period").disabled = true;
    } else {
        Ext.getDom("period").disabled = false;
    }
}

function initSysConfig() {
    document.all.name.readOnly = true;
    document.all.classz.readOnly = true;
    document.all.desc.readOnly = true;
    document.all.name.title = 系统任务任务名称不允许编辑;
    document.all.classz.title = 系统任务任务执行类不允许编辑;
    document.all.desc.title = 系统任务描述不允许编辑;
}

function triggerChecked() {
    var key = getRuleType();
    if (key != "6") { // 除了AWS启动时规则其他都校验小时，分钟

        if (!checkNull(document.all.hour, 小时必填 + "!"))
            return false;
        if (!checkNull(document.all.minute, 分钟必填 + "!"))
            return false;
    }

    if (key == '1' && !checkDayStr(document.all.daysOfWeek, 每周天, 1, 7)) {
        return false;
    }
    if (key == '2' && !checkDayStr(document.all.daysOfMonth, 每月天, 1, 31)) {
        return false;
    }

    if (key == '5') {
        if (Ext.isEmpty(document.all.cronExp.value)) {
            alert(Cron表达式不能为空 + "!");
            return false;
        }

        var sid = document.all.sid.value;
        var myURL = encodeURI("../ajax?cmd=AWS_Sys_Schedule_Check_Cron&cron=" + document.all.cronExp.value + "&sid=" + sid);
        var req = Ext.lib.Ajax.getConnectionObject().conn;
        req.open("POST", myURL, false);
        req.send(null);
        if (req.responseText != "1") {
            alert(Cron表达式不正确 + "！");
            return false;
        }
    }

    if (key == '6') {
        if (Ext.isEmpty(document.all.timez.value) || isNaN(parseInt(document.all.timez.value)) || parseInt(document.all.timez.value) < -1) {
            alert(执行次数要求为数字大于等于_1);
            return false;
        }

        if (document.all.timez.value != "0" && (Ext.isEmpty(document.all.period.value) || isNaN(parseInt(document.all.period.value)) || parseInt(document.all.period.value) < -1)) {
            alert(执行间隔要求为数字大于等于_1);
            return false;
        }
    }

    if (key == '7') {
        if (Ext.isEmpty(document.all.trigger.value)) {
            alert(触发器执行类不能为空 + "!");
            return false;
        }
    }

    return true;
}

function getTriggerRule() {
    var ruleType = getRuleType();
    var rule = ruleType;
    switch (ruleType) {
        case '0': {
            rule += ":" + $v("hour");
            rule += ":" + $v("minute");
            break;
        }
        case '1': {
            rule += ":" + $v("daysOfWeek");
            rule += ":" + $v("hour");
            rule += ":" + $v("minute");
            break;
        }
        case '2': {
            rule += ":" + $v("daysOfMonth");
            rule += ":" + $v("hour");
            rule += ":" + $v("minute");
            break;
        }
        case '3': {
            rule += ":" + $v("monthOfSession");
            rule += ":" + $v("dayOfSession");
            rule += ":" + $v("hour");
            rule += ":" + $v("minute");
            break;
        }
        case '4': {
            rule += ":" + $v("monthOfYear");
            rule += ":" + $v("dayOfYear");
            rule += ":" + $v("hour");
            rule += ":" + $v("minute");
            break;
        }
        case '5': {
            rule += ":" + $v("cronExp");
            break;
        }
        case '6': {
            rule += ":" + $v("period");
            rule += ":" + $v("timez");
            break;
        }
        case '7': {
            rule += ":" + $v("trigger");
            break;
        }
    }
    return rule;
}

// 取得频率规则类型
function getRuleType() {
    var ruleTypes = document.all.type;
    var key;
    for (var i = 0; i < ruleTypes.length; i++) {
        if (ruleTypes[i].checked == true) {
            key = ruleTypes[i].value;
            break;
        }
    }
    return key;
}

function initRule(array) {
    var key = array[0];
    switch (key) {
        case '0': {
            select(document.all.hour, array[1]);
            select(document.all.minute, array[2]);
            break;
        }
        case '1': {
            document.all.daysOfWeek.value = array[1];
            select(document.all.hour, array[2]);
            select(document.all.minute, array[3]);
            break;
        }
        case '2': {
            document.all.daysOfMonth.value = array[1];
            select(document.all.hour, array[2]);
            select(document.all.minute, array[3]);
            break;
        }
        case '3': {
            select(document.all.monthOfSession, array[1]);
            select(document.all.dayOfSession, array[2]);
            select(document.all.hour, array[3]);
            select(document.all.minute, array[4]);
            break;
        }
        case '4': {
            select(document.all.monthOfYear, array[1]);
            select(document.all.dayOfYear, array[2]);
            select(document.all.hour, array[3]);
            select(document.all.minute, array[4]);
            break;
        }
        case '5': {
            document.all.cronExp.value = array[1];
            break;
        }
        case '6': {
            document.all.period.value = array[1];
            document.all.timez.value = array[2];
            break;
        }
        case '7': {
            document.all.trigger.value = array[1] == undefined ? "" : array[1];
            break;
        }
    }
}

// 显示触发规则
function showCycleDiv(key) {
    switch (key) {
        case '0': {
            $("#dayDiv").hide();
            $("#weekDiv").hide();
            $("#monthDiv").hide();
            $("#quarterDiv").hide();
            $("#yearDiv").hide();
            $("#cronDiv").hide();
            $("#periodTR").hide();
            $("#hhmmssDiv").show();
            break;
        }
        case '1': {
            $("#dayDiv").hide();
            $("#weekDiv").show();
            $("#monthDiv").hide();
            $("#quarterDiv").hide();
            $("#yearDiv").hide();
            $("#cronDiv").hide();
            $("#periodTR").hide();
            $("#hhmmssDiv").show();
            break;
        }
        case '2': {
            $("#dayDiv").hide();
            $("#weekDiv").hide();
            $("#monthDiv").show();
            $("#quarterDiv").hide();
            $("#yearDiv").hide();
            $("#cronDiv").hide();
            $("#periodTR").hide();
            $("#hhmmssDiv").show();
            break;
        }
        case '3': {
            $("#dayDiv").hide();
            $("#weekDiv").hide();
            $("#monthDiv").hide();
            $("#quarterDiv").show();
            $("#yearDiv").hide();
            $("#cronDiv").hide();
            $("#periodTR").hide();
            $("#hhmmssDiv").show();
            break;
        }
        case '4': {
            $("#dayDiv").hide();
            $("#weekDiv").hide();
            $("#monthDiv").hide();
            $("#quarterDiv").hide();
            $("#yearDiv").show();
            $("#cronDiv").hide();
            $("#periodTR").hide();
            $("#hhmmssDiv").show();
            break;
        }
        case '5': {
            $("#dayDiv").hide();
            $("#weekDiv").hide();
            $("#monthDiv").hide();
            $("#quarterDiv").hide();
            $("#yearDiv").hide();
            $("#cronDiv").show();
            $("#periodTR").hide();
            $("#hhmmssDiv").hide();
            break;
        }
        case '6': {
            $("#dayDiv").hide();
            $("#weekDiv").hide();
            $("#monthDiv").hide();
            $("#quarterDiv").hide();
            $("#yearDiv").hide();
            $("#cronDiv").hide();
            $("#periodTR").show();
            $("#hhmmssDiv").show();
            break;
        }
    }
}

//img action...
function changeArea(obj, rowData) {
    var obj = Ext.getDom(obj);
    var rowData = Ext.getDom(rowData);
    var sourceIMG = obj.src.substring(obj.src.lastIndexOf("/") + 1);
    if (sourceIMG == "collapsed_button.gif") {
        obj.src = "../aws_img/expanded_button.gif";
        rowData.style.display = "";
    } else {
        obj.src = "../aws_img/collapsed_button.gif";
        rowData.style.display = "none";
    }
}

//-------------
function checkNull(obj, msg) {
    if (obj.value == "") {
        alert(msg);
        obj.focus();
        return false;
    } else {
        return true;
    }
}

// 检测数字
function checkNanN(x) {
    if (!isNaN(x)) {
        return true;
    } else {
        return false;
    }
}

// 检测排序,是否从小到大排列
function checkSort(arryObj) {
    var mn = -1;
    var mx;
    for (var i = 0; i < arryObj.length; i++) {
        if (mn < parseInt(arryObj[i])) {
            mn = parseInt(arryObj[i]);
        } else {
            return false;
        }
    }
    return true;
}

function goBack() {
    var sid = document.all.sid.value;
    window.location.href = encodeURI("./login.wf?sid=" + sid
			+ "&cmd=AWS_Sys_Schedule_List");
}

// 设置radio
function setRadioChecked(checkValue) {
    checkRadio(document.all.type, checkValue);
}

function checkRadio(r, v) {
    for (var i = 0; i < r.length; i++) {
        if (r[i].value == v) {
            r[i].checked = true;
            break;
        }
    }
}

function select(sel, value) {
    for (var i = 0; i < sel.options.length; i++) {
        if (sel.options[i].value == value) {
            sel.options[i].selected = true;
            break;
        }
    }
}
