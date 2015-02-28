$(document).ready(function () {
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

    $('form').submit(function () {
        if ($(this).valid()) {
            $.ajax({
                url: this.action,
                type: this.method,
                data: $(this).serialize()
            })
        }
        return false;
    });



function showMakeQuestion() {
    //Add a JQuery UI datepicker to end date field
    $('#EndDate').datepicker();
    togglePopup(true);

    var questionElement = {}
    questionElement.responseHolder = $('#responseHolder');
    questionElement.newResponse = "<li class='response'><label for='Responses'>Response: </label><input type='text' name='Responses' class='form-control' placeholder='Possible question response' /></li>";
    questionElement.addResponseBtn = $('#addResponse');

    questionElement.addResponse = function () {
        questionElement.responseHolder.append(questionElement.newResponse);
    };

    //Handle new response button clicks
    questionElement.addResponseBtn.on('click', function () {
        questionElement.addResponse();
    });
}

function togglePopup(show) {
    if(show) {
        editBox.css('display', 'block');
        fade.css('display', 'block');
    } else {
        editBox.css('display', 'none');
        fade.css('display', 'none');
    }
}
});