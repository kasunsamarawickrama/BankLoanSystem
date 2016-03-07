$(document).ready(function () {
    // createdBy :: MAM.IRFAN
    // if you want to modify this functionality ovewrite in your html file, don't edit in this file

    // all input type numbers are tex align to right
    $(':input[type="number"]').css('text-align', 'right');

    // clear error messages if you use ValidationMaessageFor function to handle error

    $('#clear').click(function () {

        // reset the form
        document.getElementsByTagName("form")[0].reset();
        // for all input field
        $('input').next('span').children('span').text('');

        // for dropdown field
        $('select').next('span').children('span').text('');
    });


    // createdBy :: Kanishka
    // clear error messages when clicked input field if you use ValidationMaessageFor function to handle error
    $('input').click(function () {
        $(this).next("span").children("span").text("");
    });

    $('input').on('input', function () {
        $(this).next("span").children("span").text("");
    });

    //$('select').click(function () {
    //    //alert($(this));
    //    $(this).next("span").children("span").text("");
    //});
});

// CreatedBy :: Kanishka
// Clears error messege on click event
// Call: $('input').on('click', function () {
//           ClearErrorMessegeOnClickEvent(this);
//      });
function ClearErrorMessegeOnClickEvent(id) { //val, vals
    $(id).next("span").children("span").text("");
}

// CreatedBy :: Kanishka
// Replace character by another
function SetCharAt(str, index, chr) {
    if (index > str.length - 1) return str;
    return str.substr(0, index) + chr + str.substr(index + 1);
}

// CreatedBy :: Kanishka
// Capitalize the first letter of a text
function ChangeToCapital(id, val) {
    if (val.length === 1)
        document.getElementById(id).value = val.toUpperCase();
    else if (val.length > 1) {
        if (val[val.length - 2] === " ") {
            val = SetCharAt(val, val.length - 1, val[val.length - 1].toUpperCase());
            document.getElementById(id).value = val;
        }
    }
}

// CreatedBy :: Kanishka
// Prevent input for phone number
function PhoneNumber(id, val) {
    if ((48 <= val && val <= 57) || val === 40 || val === 41 || val === 45)
        return 1;
    $(id).siblings('div').children('span').fadeIn();
    $(id).siblings('div').children('span').text("Not valid character for phone number");
    $(id).siblings('div').children('span').delay(100).fadeOut();
    return 0;
}

//function Phone(id, val, code) {//
//    var phoneText = val + String.fromCharCode(code);
//    var regPhoneno = /^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/;

//    if (phoneText.match(regPhoneno)) {
//        return 1;
//    }

//    $(id).siblings('div').children('span').fadeIn();
//    $(id).siblings('div').children('span').text("Not valid character for phone number");
//    $(id).siblings('div').children('span').delay(100).fadeOut();
//    return 0;
//}

// CreatedBy :: Kanishka
// Remove default input value
// for decimal and integer
function RemoveText(id, val) {
    if (val === "0.00") {
        document.getElementById(id).value = "";
    }
    else if (val === "0") {
        document.getElementById(id).value = "";
    }
}

// CreatedBy :: Kanishka
// Prevent input for user name
// return 0 - allow this character
// return 1 - not allow this character
function BlockText(code) { //val, vals
    if ((32 <= code && code <= 47) || (58 <= code && code <= 64)
        || (91 <= code && code <= 96) || (123 <= code && code <= 126))
        return 1;
    return 0;
}

// CreatedBy :: Kanishka
// Check max length
// return 0 - not allow to enter
// return 1 - allow to enter
function CheckMaxLenth(id, val, maxLenght) {
    if (val.length < maxLenght) {
        return 1;
    }
    $(id).siblings('div').children('span').fadeIn();
    $(id).siblings('div').children('span').text("Maximum length reached");
    $(id).siblings('div').children('span').delay(100).fadeOut();
    return 0;
}

// CreatedBy :: Kanishka
// Check max length
// return 0 - not allow to enter
// return 1 - allow to enter
function AllowNumbers(id, code) {
    if (47 <= code && code <= 58)
        return 1;
    $(id).siblings('div').children('span').fadeIn();
    $(id).siblings('div').children('span').text("Allow only numbers");
    $(id).siblings('div').children('span').delay(100).fadeOut();
    return 0;
}