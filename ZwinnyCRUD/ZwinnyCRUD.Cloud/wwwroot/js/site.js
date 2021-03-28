// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.


function UpdateTask(elem) {
    var isDone = $(elem).is(':checked');
    var cid = $(elem).data('TaskId');
    $.ajax({
        type: 'POST',
        url: '@Url.Action("UpdateTask", "Home")',
        data: { check: isDone,customerId:cid },
        success: function(res) {
            console.log(res);
        }
    });
}

$(document).ready(function () {
    var current_location = location.pathname.split('/')[1];

    var navbars = $('.navbar-collapse ul.navbar-nav').find("a");
    for (var i = 0; i < navbars.length; i++) {
        if (navbars[i].text == current_location) {
            $(navbars[i].closest('li')).addClass('active');
        }
        else if (current_location == '' && navbars[i].text == 'Home')
        {
            $('.navbar-collapse ul.navbar-nav').children(":first").addClass('active');
        }
        else {
            $(navbars[i].closest('li')).removeClass('active');
        }
    }   
}); 
    
 