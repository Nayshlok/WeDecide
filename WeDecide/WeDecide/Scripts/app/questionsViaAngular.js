/// <reference path="Scripts/angular.js" />
/// <reference path="Scripts/app/MakeQuestion.js" />
var questionApp = angular.module('questionApp',[]);

questionApp.controller('UxCtrl', function ($scope, $http) {

    $scope.responses = [];

    $scope.addResponse = function () {
        var itemToAdd = $scope.newResp;
        $scope.responses.push(itemToAdd);
        $scope.newResp = "";
    };
    $scope.removeResponse = function (resp) {
        var index = $scope.responses.indexOf(resp);
        var capture = $scope.responses.splice(index, 1);
        console.log(capture);
    }
});
