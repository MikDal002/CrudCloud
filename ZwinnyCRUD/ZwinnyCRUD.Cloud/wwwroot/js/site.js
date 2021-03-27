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