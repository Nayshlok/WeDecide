(function () {
    /// <reference path="Scripts/app/functions.js" />
    /// <reference path="Scripts/angular.js" />

    var feedApp = angular.module('feedApp', []);

    feedApp.controller('GlobalCtrl', ['$scope', '$http', function ($scope, $http) {

        var self = this;

        self.allQuestionURL = '/api/questions';
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
            });
        }

        pullQuestions();

    }]);

    feedApp.controller('HomePageCtrl', ['$scope', '$http', function ($scope, $http) {
        //
        console.log("The controller was intitialized");
        $scope.questions = [
            { Id: 1, QuestionText: "Test question", IsActive: true, EndTime: Date.now() }
        ];
    }]);
})();