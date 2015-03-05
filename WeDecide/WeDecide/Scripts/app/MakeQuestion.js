﻿$(document).ready(function () {
    var makeQuestion = $('.make-question'),
        makeQuestionBtn = $('#btnNewQuestion'),
        editBox = $('#editBox');
        fade = $('#fade');

    makeQuestionBtn.on('click', function () {
        if (makeQuestion.length) {
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
        /*questionElement.addResponseBtn.on('click', function () {
        questionElement.addResponse();
        });*/

    $('form').submit(function () {
        togglePopup(false);
            if ($(this).valid()) {
                $.ajax({
                    url: this.action,
                    type: this.method,
                    data: $(this).serialize()
                })
            }
            return false;
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