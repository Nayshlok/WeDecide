/// <reference path="Scripts/app/functions.js" />
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

        $scope.questions = [
            //{ Id: 1, QuestionText: "Test question", IsActive: true, EndTime: Date.now() }
        ];

        var localCheckBox = document.getElementById('checkbox_Local'),
            friendsCheckBox = document.getElementById('checkbox_Friends'),
            globalCheckBox = document.getElementById('checkbox_Global');

        // force it to check
        globalCheckBox.checked = true;

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
                    console.log("Successfully fetched {0}, with length = {1}".format(data, data.length));
                    poolInQuestion = data; // no need for pushing, just copy the Array

                    poolInQuestion.forEach(function (element) {
                        $scope.questions.push(element);
                    });
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
                    console.log("Setting friends")
                    setTimeout(function () {
                        // refresh the friends pool
                        ajaxQuestionLoader("friends", friendsQuestionPool);
                    }, 3000);
                }
                else
                {
                    console.log("Emptying friend questions");
                    $scope.questions.length = 0;
                    for (var i = 0; i < $scope.questions.length; i++) {
                        $scope.questions.pop();
                    }
                }
            else if (filter.toString().toLowerCase() === "global")
                if (globalCheckBox.checked) {
                    console.log("Setting global");
                    setTimeout(function () {
                        // refresh the global pool
                        ajaxQuestionLoader("global", globalQuestionPool);
                    }, 7000);
                }
                else
                {
                    console.log("Emptying global questions");
                    for (var i = 0; i < $scope.questions.length; i++) {
                        $scope.questions.pop();
                    }
                }
        }

        $scope.doQuestionFlow("global");
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