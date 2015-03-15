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
    var VQuestion = ValidQuestion();
    var VResponses = ValidResponses();
    var VTimes = ValidTimes();
    if (VQuestion && VResponses && VTimes) {
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
                console.log("Response " + resp.Id);
                togglePopup(false);
                clearMakeQuestionForm();
            }
        });
    }
    return false;
});

//Gets all the possible responses that the User has inputed
function getResponses() {
    var MyResponses = new Array();
    $("input[name=Responses]").each(function () {
        MyResponses.push($(this).val());
    });
    return MyResponses;
}

//Checks to see if a question is valid, if it is not, adds an error message to the span
function ValidQuestion() {
    var Question = $('#Question').val();
    var IsValid = !(Question.trim().length === 0)
    //If Question is not valid
    if (!IsValid) {
        //Add message to span
        $('#ValidateQuestion').html("You need to have a question");
    }
    return IsValid;
}

//Check to see if there are at least two unique responses that are not empty space
function ValidResponses() {
    var MyResponses = getResponses();
    var ValidResponses = new Array();
    for (var i = 0; i < MyResponses.length; i++) {
        if ((ValidResponses.indexOf(MyResponses[i]) === -1) && !(MyResponses[i].trim().length === 0)) {
            ValidResponses.push(MyResponses[i]);
        }
    }
    var ResponsesAreValid = (ValidResponses.length > 1);
    if (!ResponsesAreValid) {
        //Add Validation message to span
        $("#ValidateResponses").html("You need at least two unique responses.");
    }
    return ResponsesAreValid;
}

//Checks to see if the given CheckResponse that already exists
function ResponsesContains(ValidResponses, CheckResponse) {
    var Contains = false;
    for (var i = 0; (i < ValidResponses.length) && !Contains; i++) {
        Contains = (element === ValidResponse);
    }
    Array.prototype.forEach(function (element, index, AllResponses) {
        if (!Contains) {
            Contains = element === ValidResponses[i];
        }
    });
    return Contains;
}

//Makes sure none of the times are less than zero, and the times together provide longer than zero minutes to answer
function ValidTimes() {
    var hours = $('#Hours').val();
    var minutes = $('#Minutes').val();
    var TimesAreValid = (hours >= 0) && (minutes >= 0) && (minutes + hours > 0);
    if (!TimesAreValid) {
        //Add Validation to Span
        $('#ValidateTimes').html("You must have at least some time for others to answer.");
    }
    return TimesAreValid;
}

//Clears the Make Question Form to allow it to be used again
function clearMakeQuestionForm() {
    $("#Question").val("");
    $("#Hours").val("0");
    $("#Minutes").val("10");
    $("#QuestionScope").val("0");
    $("#FreeResponseEnabled").prop("checked", false);
    $("input[name=Responses]").each(function () {
        $(this).val("");
    });
    $('#ValidateQuestion').html("");
    $("#ValidateResponses").html("");
    $('#ValidateTimes').html("");
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