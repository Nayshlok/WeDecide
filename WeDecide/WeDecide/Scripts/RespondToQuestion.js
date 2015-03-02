$(document).ready(function () {
    $('input[type=radio]').click(function () {
        var ThisButton = $(this);
        var Value = (ThisButton === $("#FreeResponse")) ? $("#FreeResponseChoice").val() : ThisButton.val();
        $.ajax({
            url: document.location.pathname,
            type: 'POST',
            data:
            {
                ChosenResponse: Value,
                QuestionId: $("#QuestionId").val()
            }, success: function () {
                ThisButton.prop("checked", true);
            }
        });
        return false;
    });
})