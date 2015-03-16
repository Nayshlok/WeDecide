$(document).ready(function () {

    //$(document).on('click', 'input[type=radio]', function () {
    $(document).on('click', '.ChosenResponse', function () {
        console.log("clicked a radio button");
        var QuestionId = $(this).attr('data-qid');
        var ThisButton = $(this);
        $.ajax({
            url: "/Respond/Post/" + QuestionId,
            type: 'POST',
            data:
            {
                ChosenResponse: ThisButton.val(),
                QuestionId: QuestionId
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

    $(document).on('click', '.FreeResponse', function () {

        var ThisButton = $(this);
        var QuestionId = $(this).attr('data-qid');
        var FreeResponseId = "#FreeResponseChoice" + QuestionId;
        if (!($(FreeResponseId).val().trim().length === 0)) {
            $.ajax({
                url: "/Respond/Post/" + QuestionId,
                type: 'POST',
                data:
                {
                    ChosenResponse: $(FreeResponseId).val(),
                    QuestionId: QuestionId,
                }, success: function () {
                    ThisButton.prop("checked", true);
                }
            });
        } else {
            ThisButton.prop("checked", false);
        }
        return false;
    });

    //$("#FreeResponse").click(function () {
    //    var ThisButton = $(this);
    //    $.ajax({
    //        url: document.location.pathname,
    //        type: 'POST',
    //        data:
    //        {
    //            ChosenResponse: $("#FreeResponseChoice").val(),
    //            QuestionId: $("#QuestionId").val()
    //        }, success: function () {
    //            ThisButton.prop("checked", true);
    //        }
    //    });
    //    return false;
    //})
})