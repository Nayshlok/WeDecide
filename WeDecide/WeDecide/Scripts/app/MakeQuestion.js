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
        if (ValidResponses() && ValidTimes()) {
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
        }
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

    function clearMakeQuestionForm() {
        $("#Question").val("");
        $("#Hours").val("0");
        $("#Minutes").val("10");
        $("#QuestionScope").val("0");
        $("#FreeResponseEnabled").prop("checked", false);
        $("input[name=Responses]").each(function () {
            $(this).val("");
        });
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