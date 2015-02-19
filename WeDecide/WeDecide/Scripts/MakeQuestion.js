$(document).ready(function () {
    var makeQuestion = $('.make-question');

    if (makeQuestion.length) {
        //Add a JQuery UI datepicker to end date field
        $('#EndDate').datepicker();

        var questionElement = {}
        questionElement.responseHolder = $('#responseHolder');
        questionElement.newResponse = $("<li class='response'><p>Response: </p><input type='text' name='Responses' /></li>");
        questionElement.addResponseBtn = $('#addResponse');

        questionElement.addResponse = function () {
            questionElement.responseHolder.append(questionElement.newResponse);
        };

        //Handle new response button clicks
        questionElement.addResponseBtn.on('click', function () {
            questionElement.addResponse();
        });
    }
});