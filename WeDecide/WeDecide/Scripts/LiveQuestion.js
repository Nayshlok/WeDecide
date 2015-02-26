var question = function (id, text, username, endDate) {
    this.id = id;
    this.message = message;
    this.username = username;
    this.endDate = endDate;
    this.responses = ko.observableArray([]);
}

var response = function (id, text, count) {
    this.id = id;
    this.text = text
    this.count = count;
}

var LiveQuestionView = {
    questions: ko.observableArray([])
}

ko.applyBindings(LiveQuestionView);

function loadQuestion() {
    $.get('api/question', function (data) {
        var questionArray = [];
        $.each(data, function (i, p) {
            var newQuestion = new question(p.id, p.text, p.username, p.endDate);
            $.each(p.responses, function (j, r) {
                var newResponse = new response(r.id, r.text, r.count);
                newQuestion.responses.push(newResponse);
            });
            LiveQuestionView.questions.push(newQuestion);
        });
    });
}

function loadSingleQuestion(id) {
    $.get('api/question/' + id, function (data) {
        //Not implemented
    });
}

$(function () {
    var hub = $.connection.questionHub;
    $.connection.hub.start().done(function () {
        loadQuestion();
    });

    hub.client.receivedNewQuestion = function (id, text, username, endDate) {
        var newQuestion = new question(id, text, username, endDate);
        LiveQuestionView.questions.unshift(newQuestion);
    }

    hub.client.receivedResponse = function () {
        loadQuestion();
    };
})