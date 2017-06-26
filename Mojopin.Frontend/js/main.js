$(document).ready(function () {

    console.log('INIT');
    $('.post-preview:gt(4)').hide().filter(":last");
    var a = $('.next');
    if ($('.post-preview:not(:visible)').length == 0) $(a).remove();
    $(a).click(function () {
        $('.post-preview:not(:visible):lt(5)').fadeIn(function () {
            if ($('.post-preview:not(:visible)').length == 0) $(a).remove();
        });
        return false;
    });
    //);
});