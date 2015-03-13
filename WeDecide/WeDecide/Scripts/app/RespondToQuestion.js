$(document).ready(function () {

    $(document).on('click', 'input[type=radio]', function () {
        var QuestionId = $(this).attr('data-qid');
        var ThisButton = $(this);
        console.log("Clicked button " + QuestionId);
        $.ajax({
            url: "/Respond/Post/" + QuestionId,
            type: 'POST',
            data:
            {
                ChosenResponse: ThisButton.val(),
                QuestionId: QuestionId,
            }, success: function () {
                ThisButton.prop("checked", true);
            }
        });
        return false;
        //});
    });

    //$('input[type=radio]').click(function () {
    //    alert("Here");
    //    var QuestionId = $(this).attr('data-qid');
    //    var ThisButton = $(this);
    //    $.ajax({
    //        url: document.location.pathname,
    //        type: 'POST',
    //        data:
    //        {
    //            ChosenResponse: ThisButton.val(),
    //            QuestionId: QuestionId
    //        }, success: function () {
    //            ThisButton.prop("checked", true);
    //        }
    //    });
    //    return false;
    //});

    $("#FreeResponse").click(function () {
        var ThisButton = $(this);
        $.ajax({
            url: document.location.pathname,
            type: 'POST',
            data:
            {
                ChosenResponse: $("#FreeResponseChoice").val(),
                QuestionId: $("#QuestionId").val()
            }, success: function () {
                ThisButton.prop("checked", true);
            }
        });
        return false;
    })
})