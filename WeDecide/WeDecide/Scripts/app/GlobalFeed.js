/// <reference path="Scripts/app/functions.js" />
/// <reference path="Scripts/angular.js" />
(function () {

    var feedApp = angular.module('feedApp', []);

    feedApp.controller('GlobalCtrl', ['$scope', '$http', function ($scope, $http) {

        var self = this;

        self.allQuestionURL = '/api/questions/GetFilteredQuestions/';
        self.singleQuestionURL = '/api/questions/{0}';

        $scope.questions = [
            //{ Id: 1, QuestionText: "Test question", IsActive: true, EndTime: Date.now() }
        ];

        function pullQuestions() {
            $http.get(self.allQuestionURL)
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
        
        Debug.writeln("The controller was intitialized");
        var self = this;
        self.allQuestionURL = '/api/questions/GetFilteredQuestions/';

        $scope.questions = [
            { Id: 1, QuestionText: "Test question", IsActive: true, EndTime: Date.now() }
        ];

        var localCheckBox = document.getElementById('checkBox_Local'),
            friendsCheckBox = document.getElementById('checkBox_Friends'),
            globalCheckBox = document.getElementById('checkBox_Global');

        var localQuestionPool = [],
            friendsQuestionPool = [],
            globalQuestionPool = [];

        function ajaxQuestionLoader(searchFilter, poolInQuestion) {
            var requestURL = self.allQuestionURL + searchFilter;
            console.log("GETting from: {0}".format(requestURL));
            $http.get(requestURL).
                success(function (data, headers, status, config) {
                    console.log("Successfully fetched {0}, with length = {1}".format(data, data.length));
                    poolInQuestion = data; // no need for pushing, just copy the Array
                }).
                error(function (data, headers, status, config) {
                    printFailures(data); // defined in functions.js
                });
        };

        // TODO: set the interval function and click event on 'checkbox.checkEvent'
        function doQuestionFlow() {
            // while the box is checked, update the pools at an interval
            if (friendsCheckBox.checked) {
                setTimeout(function () {
                    // refresh the friends pool
                    ajaxQuestionLoader("friends", friendsQuestionPool);
                }, 3000);
            }

            if (localCheckBox.checked) {
                setTimeout(function () {
                    // refresh the local pool
                    ajaxQuestionLoader("local", localQuestionPool);
                }, 5000);
            }

            if (globalCheckBox.checked) {
                setTimeout(function () {
                    // refresh the global pool
                    ajaxQuestionLoader("global", globalQuestionPool);
                }, 7000);
            }
        }
    }]);
})();