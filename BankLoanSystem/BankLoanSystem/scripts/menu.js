function slideRight_open() {

   $('body').addClass('has-active-menu');
   $('#o-wrapper').addClass('has-slide-right');
   $('#c-menu--slide-right').addClass('is-active');
   //$('#c-menu--slide-right').show('slow');
    $('#c-mask').addClass('is-active');
    //this.disableMenuOpeners();

}

function slideRight_close() {

    $('body').removeClass('has-active-menu');
    $('#o-wrapper').removeClass('has-slide-right');
    $('#c-menu--slide-right').removeClass('is-active');
    //$('#c-menu--slide-right').hide('slow');
    $('#c-mask').removeClass('is-active');
    //this.disableMenuOpeners();

}


$(document).on('click', '.c-menu__close, #c-mask', function () {

    slideRight_close();
});