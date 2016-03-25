$(document).ready(function () {
    // createdBy :: MAM.IRFAN
    // if you want to modify this functionality ovewrite in your html file, don't edit in this file

    // all input type numbers are tex align to right
    $(':input[type="number"]').css('text-align', 'right');

    $('#clear').attr("tabindex", -1);

    var array = [2, 3, 4, 5, 7, 8, 9, 10];
    for (i = 0; i < array.length; i++) {
        $('#btnPreStep' + array[i]).attr("tabindex", -1);
    }
    
    $('input').focus(function () {
        $(this).attr('placeholder', '');
    });
    // clear error messages if you use ValidationMaessageFor function to handle error

    $('#clear').click(function () {

        // reset the form
        document.getElementsByTagName("form")[0].reset();
        // for all input field
        $('input').next('span').children('span').text('');
        $('input').removeClass('valid');
        // for dropdown field
        $('select').next('span').children('span').text('');
        $('select').removeClass('valid');
    });


    // createdBy :: Kanishka
    // clear error messages when clicked input field if you use custum validation message function to handle error
    $('input').click(function () {
        $(this).next("span").children("span").text("");
        //$(this).siblings('div').children('span').text('');
        //$('input').siblings('div').children('span').text('');
    });
   
    $('input').on('input', function () {
        $(this).next("span").children("span").text("");
        //$(this).siblings('div').children('span').text('');
        //$('input').siblings('div').children('span').text('');
    });

    //$('select').click(function () {
    //    //alert($(this));
    //    $(this).next("span").children("span").text("");
    //});
});

// CreatedBy :: kasun
// prevent keypress dot charactors twise in amount
//

function numericAllowDecimal(e, val) {
    var ch = e.which;
    if (val.indexOf('.') === -1) {
        if ((ch >= 48 && ch <= 57) || ch == 46) {
            return 1;
        }
    } else {
        if (ch >= 48 && ch <= 57) {
            return 1;
        }
    }
    return 0;
}

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

    if (val.length === 1) {
        document.getElementById(id).value = val.toUpperCase();
    } else if (val.length > 1) {
        if (val[val.length - 2] === " ") {
            val = SetCharAt(val, val.length - 1, val[val.length - 1].toUpperCase());
            document.getElementById(id).value = val;
        }
    }
}

// CreatedBy :: Kanishka
// replace to title case
function ToTitleCase(id, str) {
    //var val = str.replace(/\w\S*/g, function (txt) { return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase(); });
    var val = str.replace(/\w\S*/g, function (txt) { return txt.charAt(0).toUpperCase() + txt.substr(1); });
    document.getElementById(id).value = val;
}

// CreatedBy :: Kanishka
// Prevent input for phone number
function PhoneNumber(id, val) {
    if ((48 <= val && val <= 57) || val === 40 || val === 41 || val === 45)
        return 1;
    //$(id).siblings('div').children('span').text("Not valid character for phone number");
    return 0;
}

//function Phone(id, val, code) {//
//    var phoneText = val + String.fromCharCode(code);
//    var regPhoneno = /^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/;

//    if (phoneText.match(regPhoneno)) {
//        return 1;
//    }

//    $(id).siblings('div').children('span').text("Not valid character for phone number");
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

function InvalidCharacters(id, array, code) {
    for (var i = 0; i < array.length; i++) {
        if (array[i] === String.fromCharCode(code)) {
            //$(id).siblings('div').children('span').text("Invalid character.");
            return 1;
        }
    }
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
    //$(id).siblings('div').children('span').text("Maximum length reached");
    return 0;
}

// CreatedBy :: Kanishka
// Check max length
// return 0 - not allow to enter
// return 1 - allow to enter
function AllowNumbers(id, code) {
    if (48 <= code && code <= 58)
        return 1;
    //$(id).siblings('div').children('span').text("Allow only numbers");
    return 0;
}

// CreatedBy :: Irfan
// Validates that the input string is a valid date formatted as "mm/dd/yyyy"
function isValidDate(dateString) {
    // First check for the pattern
    if (!/^\d{1,2}\/\d{1,2}\/\d{4}$/.test(dateString))
        return false;

    // Parse the date parts to integers
    var parts = dateString.split("/");
    var day = parseInt(parts[1], 10);
    var month = parseInt(parts[0], 10);
    var year = parseInt(parts[2], 10);

    // Check the ranges of month and year
    if (year < 1000 || year > 3000 || month == 0 || month > 12)
        return false;

    var monthLength = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];

    // Adjust for leap years
    if (year % 400 == 0 || (year % 100 != 0 && year % 4 == 0))
        monthLength[1] = 29;

    // Check the range of the day
    return day > 0 && day <= monthLength[month - 1];
};

//CreatedBy : Nadeeka
//round numbers upto given decimal points

function roundNumber(number, decimal_points) {
    if (!decimal_points) return Math.round(number);
    if (number == 0) {
        var decimals = "";
        for (var i = 0; i < decimal_points; i++) decimals += "0";
        return "0." + decimals;
    }

    var exponent = Math.pow(10, decimal_points);
    var num = Math.round((number * exponent)).toString();
    return num.slice(0, -1 * decimal_points) + "." + num.slice(-1 * decimal_points)
}


// CreatedBy :: Irfan
// Restrict the key for date formatted as "mm/dd/yyyy"
$(document).on('keypress', '.dateKeyPressValidate', function (e) {
    var ch = e.which;
    if ( (ch >= 47 && ch <= 57) || ch == 127 || ch == 8) {
        return 1;
    }
    else {

        e.preventDefault();
    }


});