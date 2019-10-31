Ext.onReady(function () {
    if (triggerRule.length > 0) {
        initRule(triggerRule);
        setRadioChecked(triggerRule[0]);
        showCycleDiv(triggerRule[0]);
    }
});

function save(cmd) {
    if (!checkInput()) {
        return false;
    } else {
        var frm = Ext.getDom("frmMain");
        frm.target = '_self';
        frm.triggerRule.value = getTriggerRule();
        frm.cmd.value = cmd;
        frm.submit();
        return true;
    }
}

// 检测输入交验
function checkInput() {
    if (Ext.isEmpty(document.all.name.value)) {
        alert(计划任务名称必填 + "!");
        return false;
    }

    if (Ext.isEmpty(document.all.classz.value)) {
        alert(任务执行类必填 + "!");
        return false;
    }

    if (triggerChecked() === false) {
        return false;
    }

    return true;
}
