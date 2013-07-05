function exportExcel() {

    var sHtml = htmlEncode($("#ExpoerTable")[0].outerHTML);//做html編碼

    $("input[name='exportHtml']").val(sHtml);

    //表單提交
    $("form[name='exportForm']").submit();
}
//↓出自：http://stackoverflow.com/questions/1219860/javascript-jquery-html-encoding
function htmlEncode(value) {
    //create a in-memory div, set it's inner text(which jQuery automatically encodes)
    //then grab the encoded contents back out.  The div never exists on the page.
    return $('<div/>').text(value).html();
}