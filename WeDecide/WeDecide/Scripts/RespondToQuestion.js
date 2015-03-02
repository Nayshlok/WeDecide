$(document).ready(function () {
    $('input[type=radio]').click(function () {
        var ThisButton = $(this);
        $.ajax({
            url: document.location.pathname,
            type: 'POST',
            data:
            {
                ChosenResponse: ThisButton.val(),
                QuestionId: $("#QuestionId").val()
            }, success: function () {
                ThisButton.prop("checked", true);
            }
        });
        return false;
    });
})