﻿/// <reference path="Scripts/app/functions.js" />
/// <reference path="Scripts/angular.js" />
(function () {

    var feedApp = angular.module('feedApp', []);

    feedApp.controller('GlobalCtrl', ['$scope', '$http', function ($scope, $http) {

        var self = this;
        self.allQuestionURL = '/api/questions/';
        self.singleQuestionURL = '/api/questions/{0}';

        $scope.questions = [
            //{ Id: 1, QuestionText: "Test question", IsActive: true, EndTime: Date.now() }
        ];

        function pullQuestions() {
            $http.get(self.allQuestionURL).
            success(function (data, status, headers, config) {
                // things
                console.log("Data received: {0}, data length = {1}".format(data, data.length));
                for (var d in data) {
                    console.log("{0}\t{1}".format(d, data[d]));
                    var someObject = data[d];
                    for (var o in someObject) {
                        console.log("\tK: {0}, V: {1}".format(o, someObject[o]));
                    }
                    $scope.questions.push(data[d]);
                }
            }).
            error(function (data, status, headers, config) {
                // things that deal with errors
                console.log("Failures:\n{0}".format(data));
                printFailures(data);
            });
        }
        pullQuestions();

    }]);

    feedApp.controller('HomePageCtrl', ['$scope', '$http', function ($scope, $http) {

        var self = this;
        self.allQuestionURL = '/api/questions/GetFilteredQuestions/';
        self.singleResponseURL = 'api/questions/GetResponse/';

        $scope.questions = [];

        var localCheckBox = document.getElementById('checkbox_Local'),
            friendsCheckBox = document.getElementById('checkbox_Friends'),
            globalCheckBox = document.getElementById('checkbox_Global');

        // force it to check
        globalCheckBox.checked;

        //Add a question to the feed
        function addQuestion(question, type) {
            var questionWrap = $("<section id='" + type + "' class='question shadowed'></section>"),
                questionList = $("<ul></ul>");
            questionId = $("<label class='questionId'>Question #" + question.Id + "</label><hr />"),
            questionText = $("<li class='questionText'>" + question.QuestionText + "</li>"),
            questionActive = $("<li class='questionActive'>" + question.IsActive + "</li>"),
            questionEndTime = $("<li><label>Ends: " + question.EndTime + "</label></li>"),
            responseWrap = $("<li class='responses'></li>"),
            responseText = $("<label class='responseText'>Responses</label><hr />"),
            responseList = $("<ul></ul>");
            
            for (var r in question.Responses) {
                response = question.Responses[r];
                responseList.append("<li><input type='radio' data-qid='" + question.Id + "'name='question" + question.Id + "' value='" + response.Text + "' />" + response.Text + " " + response.VoteCount + "</li>");
            }

            questionWrap.append(questionId);
            questionList.append(questionText);
            questionList.append(questionActive);
            questionList.append(questionEndTime);
            responseWrap.append(responseText);
            responseWrap.append(responseList);
            questionList.append(responseWrap);
            questionWrap.append(questionList);

            $("#questionHolder").append(questionWrap);
        }

        var localQuestionPool = [],
            friendsQuestionPool = [],
            globalQuestionPool = [];

        /**
         * Helper function to pull async json from the @constant self.allQuestionURL
         * @param {string} searchFilter Valid: "local", "friends", "global" in any casing
         * @param {Array} poolInQuestion
         */
        function ajaxQuestionLoader(searchFilter, poolInQuestion) {
            var requestURL = self.allQuestionURL + searchFilter;
            console.log("GETting from: {0}".format(requestURL));
            $http.get(requestURL).
                success(function (data, headers, status, config) {
                    //console.log("Successfully fetched {0}, with length = {1}".format(data, data.length));
                    //poolInQuestion = data; // no need for pushing, just copy the Array

                    //poolInQuestion.forEach(function (element) {
                    //    $scope.questions.push(element);
                    //});
                    //console.log("Data received: {0}, data length = {1}".format(data, data.length));
                    for (var d in data) {
                        var someObject = data[d];
                        $scope.questions.push(someObject);
                        addQuestion(someObject, searchFilter);
                    }
                }).
                error(function (data, headers, status, config) {
                    printFailures(data); // defined in functions.js
                });
            // Update the feed with the new concerns           
            //$scope.questions = poolInQuestion;
        };

        // TODO: set the interval function and click event on 'checkbox.checkEvent'
        // TODO: decouple for the ng-click and pass the id into each call
        $scope.doQuestionFlow = function(filter) {
            console.log("Down in the questionFlow");
            // while the box is checked, update the pools at an interval

            if (filter.toString().toLowerCase() === "friends")
                if (friendsCheckBox.checked) {
                    console.log("Setting friends");
                    ajaxQuestionLoader("friends", friendsQuestionPool);
                    //setTimeout(function () {
                    //    // refresh the friends pool
                    //    ajaxQuestionLoader("friends", friendsQuestionPool);
                    //}, 1000);
                }
                else
                {
                    console.log("Emptying friend questions");
                    $('#questionHolder').children().remove('#friends');
                    //while ($scope.questions.length > 0) {
                    //    //$scope.questions.pop();
                    //    $("#questionHolder").empty();
                    //}
                    console.log("question array length : " + $scope.questions.length)
                }
            else if (filter.toString().toLowerCase() === "global")
                if (globalCheckBox.checked) {
                    console.log("Setting global");
                    ajaxQuestionLoader("global", globalQuestionPool);
                    //setInterval(function () {
                    //    // refresh the global pool
                    //    ajaxQuestionLoader("global", globalQuestionPool);
                    //}, 10000);
                }
                else
                {
                    console.log("Emptying global questions");
                    
                    $('#questionHolder').children().remove('#global');

                    //while($scope.questions.length > 0){
                    //    //$scope.questions.pop();
                    //}
                    console.log("question array length : " + $scope.questions.length)

                }

            function populateFeed() {
                for (var q in $scope.questions) {
                    window.console.log(q);
                }
            }

            populateFeed();
        }
        //$scope.doQuestionFlow("global");
        //setInterval(doQuestionFlow, 6000);
    }]);

    feedApp.controller('ProfileCtrl', ['$scope', '$http', function ($scope, $http) {

        var self = this;

        self.allQuestionURL = 'api/ProfileQuestion/CurrentQuestions';
        self.singleQuestionURL = '/api/questions/{0}';

        $scope.questions = [
            //{ Id: 1, QuestionText: "Test question", IsActive: true, EndTime: Date.now() }
        ];

        function pullQuestions() {
            console.log("Asking for current questions {0}".format(self.allQuestionURL));
            $http.get(self.allQuestionURL).
            success(function (data, status, headers, config) {
                // things
                console.log("Data received: {0}, data length = {1}".format(data, data.length));
                for (var d in data) {
                    console.log("{0}\t{1}".format(d, data[d]));
                    var someObject = data[d];
                    for (var o in someObject) {
                        console.log("\tK: {0}, V: {1}".format(o, someObject[o]));
                    }
                    $scope.questions.push(data[d]);
                }
            }).
            error(function (data, status, headers, config) {
                // things that deal with errors
                console.log("Failures:\n{0}".format(data));
                printFailures(data);
            });
        }
        pullQuestions();

    }]);


})();