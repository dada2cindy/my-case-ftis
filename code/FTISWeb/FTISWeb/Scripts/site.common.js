/* CKEditor */
function SetCKEditorLanguage(lang) {
    if (lang != 'zh') {
        CKEDITOR.config.language = lang;
    } else {
        CKEDITOR.config.language = 'zh-TW';
    }
}

(function ($) {
    $.fn.CKEditorInit = function (ckfinderPath) {
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
            width: "85%",
            fullPage: true
            //filebrowserUploadUrl: null
        });

        // ��CKFinder�j�iCKEditor
        CKFinder.setupCKEditor(editor, ckfinderPath);

        // �ץ� Chrome �L�k��ckeditor�Ϥ���Ԥj�p�A�ޥ�Plugin jquery.webkitresize.min.js ��A�[�J���qCode�A�Y�i��ԹϤ��j�p
        //editor = CKEDITOR.on('instanceReady', function () {
        //    $(".cke_editor iframe").webkitimageresize().webkittableresize().webkittdresize();
        //});
    };    

})(jQuery);
/* CKEditor - End */