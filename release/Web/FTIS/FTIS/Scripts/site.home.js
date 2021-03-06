﻿/***************************************************************************
前台
***************************************************************************/
function SiteSearch(u) {
    var keyword = $("#txtSiteKeyWord").val();
    if (keyword.length <= 0) {
        alert("請輸入關鍵字!");
        return false;
    }

    var searchUrl = u + "?keyWord=" + encodeURIComponent(keyword);
    window.location.href = searchUrl;
}

function GoPage(url) {
    if (url.indexOf('?') > 0) {
        location.href = (url + "&page=" + $("#txtPage").val());
    }
    else {
        location.href = (url + "?page=" + $("#txtPage").val());
    }
}

function GetCheckBoxValue(selector) {
    if ($(selector).prop('checked')) {
        return $(selector).val();
    }
    else {
        return "";
    }
}

/***************************************************************************
Enter 預設按鈕
***************************************************************************/
(function ($) {
    $.prototype.enterPressed = function (fn) {
        $(this).keyup(function (e) {
            if ((e.keyCode || e.which) == 13) {
                fn();
            }
        });
    };
})(jQuery);

/***************************************************************************
清除輸入框
***************************************************************************/
function ClearUI(tag) {
    $(tag + " input:text").each(function () {
        $(this).val('');
    });
}

function ClearUIFile(tag) {
    $(tag).val('');
}

/***************************************************************************
Enter 預設按鈕
***************************************************************************/
(function ($) {
    $.prototype.enterPressed = function (fn) {
        $(this).keyup(function (e) {
            if ((e.keyCode || e.which) == 13) {
                fn();
            }
        });
    };
})(jQuery);

/***************************************************************************
判斷使用者輸入日期格式是否為 YYYY/MM/DD
***************************************************************************/
function DateValidationCheck(str) {
    var re = new RegExp("^([0-9]{4})[./]{1}([0-9]{1,2})[./]{1}([0-9]{1,2})$");
    var strDataValue;
    var infoValidation = true;

    if ((strDataValue = re.exec(str)) != null) {
        var i;
        i = parseFloat(strDataValue[1]);
        if (i <= 0 || i > 9999) { // 年
            infoValidation = false;
        }
        i = parseFloat(strDataValue[2]);
        if (i <= 0 || i > 12) { // 月
            infoValidation = false;
        }
        i = parseFloat(strDataValue[3]);
        if (i <= 0 || i > 31) { // 日
            infoValidation = false;
        }
    } else {
        infoValidation = false;
    }

    //if (!infoValidation) {
    //    alert('請輸入 YYYY/MM/DD 日期格式');
    //}
    return infoValidation;
}

/***************************************************************************
刊登日期改變的時候，不是正確的是日期就變空白
***************************************************************************/
function OnPostDateChange() {
    var date = this.value();
    if (date == null || !DateValidationCheck($("#PostDate").val())) {
        $("#PostDate").val('');
    }
}

function OnExpiredDateChange() {
    var date = this.value();
    if (date == null || !DateValidationCheck($("#ExpiredDate").val())) {
        $("#ExpiredDate").val('');
    }
}

/***************************************************************************
UI原本的
***************************************************************************/
function MM_swapImgRestore() { //v3.0
    var i, x, a = document.MM_sr; for (i = 0; a && i < a.length && (x = a[i]) && x.oSrc; i++) x.src = x.oSrc;
}
function MM_preloadImages() { //v3.0
    var d = document; if (d.images) {
        if (!d.MM_p) d.MM_p = new Array();
        var i, j = d.MM_p.length, a = MM_preloadImages.arguments; for (i = 0; i < a.length; i++)
            if (a[i].indexOf("#") != 0) { d.MM_p[j] = new Image; d.MM_p[j++].src = a[i]; }
    }
}

function MM_findObj(n, d) { //v4.01
    var p, i, x; if (!d) d = document; if ((p = n.indexOf("?")) > 0 && parent.frames.length) {
        d = parent.frames[n.substring(p + 1)].document; n = n.substring(0, p);
    }
    if (!(x = d[n]) && d.all) x = d.all[n]; for (i = 0; !x && i < d.forms.length; i++) x = d.forms[i][n];
    for (i = 0; !x && d.layers && i < d.layers.length; i++) x = MM_findObj(n, d.layers[i].document);
    if (!x && d.getElementById) x = d.getElementById(n); return x;
}

function MM_swapImage() { //v3.0
    var i, j = 0, x, a = MM_swapImage.arguments; document.MM_sr = new Array; for (i = 0; i < (a.length - 2) ; i += 3)
        if ((x = MM_findObj(a[i])) != null) { document.MM_sr[j++] = x; if (!x.oSrc) x.oSrc = x.src; x.src = a[i + 2]; }
}