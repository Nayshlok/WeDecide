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
    $.get('/api/UsersQuestion', function (data) {
        var questionArray = [];
        $.each(data, function (i, p) {
            var newQuestion = new question(p.Id, p.Text, p.User.Name, p.EndDate);
            $.each(p.responses, function (j, r) {
                var newResponse = new response(r.Id, r.Text, r.Count);
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

    hub.client.receivedResponse = function (questionId, response) {
        var questionFilter = $.grep(LiveQuestionView.questions, function (q){
            return q.id === questionId;
        });
        var thisQuestion = questionFilter[0];

        var responseFilter = $.grep(thisQuestion.responses, function (x) {
                return x.id === response.Id;
            });
        var singleResponse = responseFilter[0];
        singleResponse.Count = response.Count;
    };
})