$(document).ready(function () {
    var makeQuestion = $('.make-question'),
        makeQuestionBtn = $('#btnNewQuestion'),
        editBox = $('#editBox');
        fade = $('#fade');

        //$(document).on('click', makeQuestionBtn, function () {
        //    if (makeQuestion.length) {
        //        showMakeQuestion();
        //    }
        //});
        makeQuestionBtn.on('click', function () {
            if ($('.make-question').length) {
            showMakeQuestion();
        }
    });

    fade.on('click', function () {
        togglePopup(false);
    });

function showMakeQuestion() {
    togglePopup(true);

    var questionElement = {}
    questionElement.responseHolder = $('#responseHolder');
    var newResponse = "<li class='response'><label for='Responses'>Response: </label><input type='text' name='Responses' class='form-control' placeholder='Possible question response' /></li>";
    questionElement.addResponseBtn = $('#addResponse');

    questionElement.addResponse = function () {
            questionElement.responseHolder.append(newResponse);
    };

    //Handle new response button clicks
    questionElement.addResponseBtn.on('click', function () {
        questionElement.addResponse();
    });
    //$('form').submit(function () {
    //    togglePopup(false);
    //        if ($(this).valid()) {
    //            $.ajax({
    //                url: this.action,
    //                type: this.method,
    //                data: $(this).serialize()
    //            })
    //        }
    //        return false;
    //});
}

    //Jacob Code :
$('input[name=form_submit]').click(function () {
    var MyResponses = getResponses();
    var Question = $("#Question");
    var Hours = $("#Hours");
    var Minutes = $("#Minutes");
    var QuestionScope = $("#QuestionScope");
    var FreeResponseEnabled = $("#FreeResponseEnabled");
    $.ajax({
        url: "Question/New",
        type: 'POST',
        data:
        {
            Question: Question.val(),
            Hours: Hours.val(),
            Minutes: Minutes.val(),
            QuestionScope: QuestionScope.val(),
            FreeResponseEnabled: FreeResponseEnabled.is(":checked"),
            Responses: MyResponses
        },
        success: function (resp) {
            if (resp.length == 0) {
                togglePopup(false);
                clearMakeQuestionForm();
            }
            else {
                //Set the Validation Text
            }
        }
    });
    return false;
});

function getResponses() {
    var MyResponses = new Array();
    $("input[name=Responses]").each(function () {
        MyResponses.push($(this).val());
    });
    return MyResponses;
}

function ValidResponses() {
    var MyResponses = getResponses();
    var ValidResponses = new Array();
    Array.prototype.forEach(function (element, index, MyResponses) {
        
    });
}

function ValidTimes() {
    var hours = $('#Hours').val();
    var minutes = $('#Minutes').val();
    return (hours >= 0) && (minutes >= 0) && (minutes + hours > 0);
}

function clearMakeQuestionForm() {
    $("#Question").val("");
    $("#Hours").val("0");
    $("#Minutes").val("10");
    $("#QuestionScope").val("0");
    $("#FreeResponseEnabled").prop("checked", false);
    $("input[name=Responses]").each(function () {
        $(this).val("");
    });
}

function togglePopup(show) {
    if (show) {
        editBox.css('display', 'block');
        fade.css('display', 'block');
    } else {
        editBox.css('display', 'none');
        fade.css('display', 'none');
    }
}
});