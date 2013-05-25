/* CKEditor */
function SetCKEditorLanguage(lang) {
    if (lang != 'zh') {
        CKEDITOR.config.language = lang;
    } else {
        CKEDITOR.config.language = 'zh-TW';
    }
}

(function ($) {
    $.fn.CKEditorInit = function () {
        ////�P�_�O�_�n�ϥθm������
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
            width: "80%",
            fullPage: true
            //filebrowserUploadUrl: null
        });

        // ��CKFinder�j�iCKEditor
        ////var setting = {};
        ////setting.basePath = ckfinderPath;
        ////setting.id = apiUrl + "&" + storeFolderPath + "/CkFiles/&" + enable + "&" + CKEDITOR.config.language;
        ////CKFinder.setupCKEditor(editor, setting, "Images", "Flash");

        // �ץ� Chrome �L�k��ckeditor�Ϥ���Ԥj�p�A�ޥ�Plugin jquery.webkitresize.min.js ��A�[�J���qCode�A�Y�i��ԹϤ��j�p
        //editor = CKEDITOR.on('instanceReady', function () {
        //    $(".cke_editor iframe").webkitimageresize().webkittableresize().webkittdresize();
        //});
    };

    //$.fn.CKEditorMailDispatchInit = function (ckfinderPath, apiUrl, storeFolderPath, enable, useTags) {
    //    ////�P�_�O�_�n�ϥθm������
    //    var targetId = $(this).attr('id');
    //    var editor = CKEDITOR.replace(targetId, {
    //        toolbar: [
    //                                                  ['Preview'], ['BGColor', 'TextColor'], ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord'],
    //                                                  ['Undo', 'Redo', '-', 'Find', 'Replace', '-', 'SelectAll', 'RemoveFormat'],
    //                                                  ['Image', 'Table', 'HorizontalRule', 'Smiley', 'PageBreak'],
    //                                                  ['Maximize', 'ShowBlocks'],
    //                                                  '/',
    //                                                  ['Bold', 'Italic', 'Underline', 'Strike', '-', 'Subscript', 'Superscript'],
    //                                                  ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', 'Blockquote'],
    //                                                  ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
    //                                                  ['Link', 'Unlink', 'Anchor'],
    //                                                  '/',
    //                                                  ['Styles', 'Format', 'Font', 'FontSize', 'Source']
    //        ],
    //        resize_enabled: true,
    //        height: "500",
    //        fullPage: true
    //        //filebrowserUploadUrl: null
    //    });

    //    // ��CKFinder�j�iCKEditor
    //    var setting = {};
    //    setting.basePath = ckfinderPath;
    //    setting.id = apiUrl + "&" + storeFolderPath + "/CkFiles/&" + enable + "&" + CKEDITOR.config.language;
    //    CKFinder.setupCKEditor(editor, setting, "Images", "Flash");

    //    // �ץ� Chrome �L�k��ckeditor�Ϥ���Ԥj�p�A�ޥ�Plugin jquery.webkitresize.min.js ��A�[�J���qCode�A�Y�i��ԹϤ��j�p
    //    editor = CKEDITOR.on('instanceReady', function () {
    //        $(".cke_editor iframe").webkitimageresize().webkittableresize().webkittdresize();
    //    });
    //};

})(jQuery);
/* CKEditor - End */