/***************************************************************************
CKEditor
***************************************************************************/
function SetCKEditorLanguage(lang) {
    if (lang != 'zh') {
        CKEDITOR.config.language = lang;
    } else {
        CKEDITOR.config.language = 'zh-TW';
    }
}

(function ($) {
    $.fn.CKEditorInit = function (ckfinderPath) {
        ////判斷是否要使用置換標籤
        var targetId = $(this).attr('id');
        var editor = CKEDITOR.replace(targetId, {
            //toolbar: [
            //                                          ['Preview'], ['BGColor', 'TextColor'], ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord'],
            //                                          ['Undo', 'Redo', '-', 'Find', 'Replace', '-', 'SelectAll', 'RemoveFormat'],
            //                                          ['Image', 'Table', 'HorizontalRule', 'Smiley', 'PageBreak'],
            //                                          ['Maximize', 'ShowBlocks'],
            //                                          '/',
            //                                          ['Bold', 'Italic', 'Underline', 'Strike', '-', 'Subscript', 'Superscript'],
            //                                          ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', 'Blockquote'],
            //                                          ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
            //                                          ['Link', 'Unlink', 'Anchor'],
            //                                          '/',
            //                                          ['Styles', 'Format', 'Font', 'FontSize', 'Source']
            //],
            resize_enabled: true,
            height: "400",
            width: "85%",
            fullPage: true
            //filebrowserUploadUrl: null
        });

        // 把CKFinder綁進CKEditor
        CKFinder.setupCKEditor(editor, ckfinderPath);

        // 修正 Chrome 無法讓ckeditor圖片拖拉大小，引用Plugin jquery.webkitresize.min.js 後，加入此段Code，即可拖拉圖片大小
        //editor = CKEDITOR.on('instanceReady', function () {
        //    $(".cke_editor iframe").webkitimageresize().webkittableresize().webkittdresize();
        //});
    };    

})(jQuery);
/* CKEditor - End */

/***************************************************************************
CKFinder 單個Input使用的上傳圖片
***************************************************************************/
function BrowseServer(startupPath, functionData) {
    // You can use the "CKFinder" class to render CKFinder in a page:
    var finder = new CKFinder();

    // The path for the installation of CKFinder (default = "/ckfinder/").
    //finder.basePath = '../';

    //Startup path in a form: "Type:/path/to/directory/"
    finder.startupPath = startupPath;

    // Name of a function which is called when a file is selected in CKFinder.
    finder.selectActionFunction = SetFileField;

    // Additional data to be passed to the selectActionFunction in a second argument.
    // We'll use this feature to pass the Id of a field that will be updated.
    finder.selectActionData = functionData;

    // Name of a function which is called when a thumbnail is selected in CKFinder.
    //finder.selectThumbnailActionFunction = ShowThumbnails;

    // Launch CKFinder
    finder.popup();
}

/***************************************************************************
For Checkbox
***************************************************************************/
function SelectAll(tag) {
    $(tag + " input:checkbox").each(function () {
        $(this).prop("checked", true);
    });
}

function ClearSelect(tag) {
    $(tag + " input:checkbox").each(function () {
        $(this).prop("checked", false);
    });
}

function ClearUI(tag) {
    $(tag + " input:text").each(function () {
        $(this).val('');
    });
}

function ClearFile(tag) {
    $(tag).val('');
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
Grid相關刪除動作
***************************************************************************/
//刪除多筆
function DeleteItems(content) {
    var checkboxChecked = $("#dvGrid input[name='IsDelete']:checked");
    var inputCheckboxLength = checkboxChecked.length;
    if (inputCheckboxLength == 0) {
        alert('請選擇要刪除的項目');
        return false;
    }

    if (OnDelete_Before(content)) {
        var allId = '';
        for (var i = 0; i < inputCheckboxLength; i++) {
            allId += checkboxChecked[i].value + ',';
        }
        OnDeleteItems(allId);
    }
}

//刪除一筆
function DeleteItem(id, content) {
    if (OnDelete_Before(content)) {
        OnDeleteItem(id);
    }
}

//刪除前的動作
function OnDelete_Before(content) {
    $('.deleteMsgFail').html('');
    return (confirm('確定刪除? ' + content))
}

//刪除完成的動作
function OnDelete_Complete() {
    if ($('.deleteMsgFail').html().length > 0) {
        alert('刪除完成，尚有失敗項目，請查看下方訊息。');
    }
    else {
        alert('刪除完成');
        Query();
    }
}

/***************************************************************************
從下拉帶入 主題分類, 施政分類, 服務分類
***************************************************************************/
function GetSelInfo(ddlId, txtCodeId, txtNameId) {
    var code = $('#' + ddlId).val();
    var name = $('#' + ddlId + ' :selected').text();
    $('#' + txtCodeId).val(code);
    $('#' + txtNameId).val(name);
}

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
uu.upload CreateImgUploader
***************************************************************************/
function CreateImgUploader(targetField, url) {
    var uploader = new qq.FileUploader(
    {
        element: $('#file-uploader' + targetField)[0],
        action: url,
        allowedExtensions: ["bmp", "gif", "jpeg", "jpg", "png"],
        multiple: false,
        params: { targetField: targetField, folder: 'imgs' },
        template: '<div class="qq-uploader">' +
      '<div class="qq-upload-drop-area"><span>拖曳檔案到這裡上傳</span></div>' +
      '<div class="qq-upload-button">上傳圖片</div>' +
      '<ul class="qq-upload-list" style="display:none;"></ul>' +
  '</div>',
        messages:
        {
            typeError: "{file} 檔案類型錯誤. 只允許上傳以下副檔名的檔案：\r\n{extensions}.",
            sizeError: "{file} 超過檔案大小最大限制, 最大檔案大小為 {sizeLimit}.",
            emptyError: "請選擇上傳的圖片."
        },
        showMessage: function (message) {
            alert(message);
        },
        onComplete: function (id, fileName, responseJSON) {
            $('#file-uploader' + targetField).show();
            if (responseJSON.success) {
                $('#' + targetField).val(responseJSON.filePath);
                $('#file-function' + targetField).show();
                alert(" 檔案上傳完成!");
            }
            else {
                alert(" 檔案上傳錯誤! ");
                //alert(" 檔案上傳錯誤! error: " + responseJSON.error);
            }
        },
        onSubmit: function (id, fileName) {
            $('#file-uploader' + targetField).hide();
        }
    });
}

function CreateFileUploader(targetField, url) {
    var uploader = new qq.FileUploader(
    {
        element: $('#file-uploader' + targetField)[0],
        action: url,
        allowedExtensions: ["7z", "aiff", "asf", "avi", "bmp", "csv", "doc", "docx", "fla", "flv", "gif", "gz", "gzip", "jpeg", "jpg", "mid", "mov", "mp3", "mp4", "mpc", "mpeg", "mpg", "ods", "odt", "pdf", "png", "ppt", "pptx", "pxd", "qt", "ram", "rar", "rm", "rmi", "rmvb", "rtf", "sdc", "sitd", "swf", "sxc", "sxw", "tar", "tgz", "tif", "tiff", "txt", "vsd", "wav", "wma", "wmv", "xls", "xlsx", "zip"],
        multiple: false,
        params: { targetField: targetField, folder: 'files' },
        template: '<div class="qq-uploader">' +
      '<div class="qq-upload-drop-area"><span>拖曳檔案到這裡上傳</span></div>' +
      '<div class="qq-upload-button">上傳檔案</div>' +
      '<ul class="qq-upload-list" style="display:none;"></ul>' +
  '</div>',
        messages:
        {
            typeError: "{file} 檔案類型錯誤. 只允許上傳以下副檔名的檔案：\r\n{extensions}.",
            sizeError: "{file} 超過檔案大小最大限制, 最大檔案大小為 {sizeLimit}.",
            emptyError: "請選擇上傳的檔案."
        },
        showMessage: function (message) {
            alert(message);
        },
        onComplete: function (id, fileName, responseJSON) {
            $('#file-uploader' + targetField).show();
            if (responseJSON.success) {
                $('#' + targetField).val(responseJSON.filePath);
                $('#file-function' + targetField).show();
                alert(" 檔案上傳完成!");
            }
            else {
                alert(" 檔案上傳錯誤! ");
                //alert(" 檔案上傳錯誤! error: " + responseJSON.error);
            }
        },
        onSubmit: function (id, fileName) {
            $('#file-uploader' + targetField).hide();
        }
    });
}

function ClearFileAndHide(targetField) {
    $('#' + targetField).val('');
    $('#file-function' + targetField).hide();
}

function OpenUploadImgFile(targetField, url) {
    var fileName = $('#' + targetField + 'Name').val();
    var filePath = $('#' + targetField).val();
    var fullFilePath = url + '/?path=' + filePath + '&fileName=' + fileName;
    window.open(fullFilePath);
}

function OpenUploadFile(targetField, url) {
    var fileName = $('#' + targetField + 'Name').val();
    var filePath = $('#' + targetField).val();
    var fullFilePath = url + filePath + '&fileName=' + fileName;
    window.open(fullFilePath);
}

function CreateFlashUploader(targetField, url) {
    var uploader = new qq.FileUploader(
    {
        element: $('#file-uploader' + targetField)[0],
        action: url,
        allowedExtensions: ["swf", "flv"],
        multiple: false,
        params: { targetField: targetField, folder: 'Scripts/ckfinder/userfiles/flash' },
        template: '<div class="qq-uploader">' +
      '<div class="qq-upload-drop-area"><span>拖曳檔案到這裡上傳</span></div>' +
      '<div class="qq-upload-button">上傳檔案</div>' +
      '<ul class="qq-upload-list" style="display:none;"></ul>' +
  '</div>',
        messages:
        {
            typeError: "{file} 檔案類型錯誤. 只允許上傳以下副檔名的檔案：\r\n{extensions}.",
            sizeError: "{file} 超過檔案大小最大限制, 最大檔案大小為 {sizeLimit}.",
            emptyError: "請選擇上傳的檔案."
        },
        showMessage: function (message) {
            alert(message);
        },
        onComplete: function (id, fileName, responseJSON) {
            $('#file-uploader' + targetField).show();
            if (responseJSON.success) {
                $('#' + targetField).val(responseJSON.filePath);
                $('#file-function' + targetField).show();
                alert(" 檔案上傳完成!");
            }
            else {
                alert(" 檔案上傳錯誤! ");
                //alert(" 檔案上傳錯誤! error: " + responseJSON.error);
            }
        },
        onSubmit: function (id, fileName) {
            $('#file-uploader' + targetField).hide();
        }
    });
}