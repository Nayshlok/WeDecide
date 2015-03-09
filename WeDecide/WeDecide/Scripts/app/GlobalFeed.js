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
                // things
                console.log("Failures:\n{0}".format(data));
            });
        }

        //$scope.pullQuestions = function () {

        //    $http.get(self.allQuestionsURL).
        //        success(function (data, status, headers, config) {
        //            // this callback will be called asynchronously
        //            // when the response is available
        //            console.log("Successfully pulled: {0}".format(data))
        //            var size = data.length;

        //            //for (var i = 1; i <= size; i++) {
        //            //    $http.get(self.singleQuestionURL.format(i)).
        //            //        success(function (data, status, headers, config) {
        //            //            // foreach things
        //            //            console.log("Inner foreach data: {0}".format(data));
        //            //            $scope.questions.push(data);
        //            //        }).
        //            //        error(function (data, status, headers, config) {
        //            //            // log the error
        //            //        });
        //            //}

        //            //for (var d in data) {
        //            //    $scope.questions.push(d);
        //            //}
        //        }).
        //        error(function (data, status, headers, config) {
        //            // called asynchronously if an error occurs
        //            // or server returns response with an error status.
        //        });
        //};

        pullQuestions();

    }]);

    feedApp.controller('PersonalCtrl', ['$scope', '$http', function ($scope, $http) {
        // I need a body
    }])

})();