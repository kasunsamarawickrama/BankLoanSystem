﻿$(document).ready(function () {
    // createdBy :: MAM.IRFAN
    // if you want to modify this functionality ovewrite in your html file, don't edit in this file

    // all input type numbers are tex align to left
    $(':input[type="number"]').css('text-align', 'right');

    // clear error messages if you use ValidationMaessageFor function to handle error
    
    $('#clear').click(function () {

        // for all input field
        $('input').next('span').children('span').text('');

        // for dropdown field
        $('select').next('span').children('span').text('');
    });



    

});