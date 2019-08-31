var app = angular.module('MyApp', ['ui.bootstrap']);
app.controller('userListController', function ($scope, $http, $filter) {
    "use strict";

    getUserList();



    function getUserList() {
        $scope.showTable = true;
        $http({
            method: "get",
            url: _link + "/User/web-getallusers",
            headers: Headers
        }).then(function (res) {
            if (res.status === 200) {
                $scope.userList = res.data;
                pagin();
                $scope.showTable = false;
            } else {
                shortMessage(res.data.message, "e");
                $scope.showTable = false;
            }
        });
    }

    $scope.postRoleTypeProcess = function (x) {

    };

    function pagin() {
        $scope.currentPage = 0;
        $scope.pageSize = 20;
        $scope.data = [];

        $scope.getData = function () {
            return $filter('filter')($scope.assayList, $scope.search);
        };

        $scope.numberOfPages = function () {
            return Math.ceil($scope.getData().length / $scope.pageSize);
        };

        for (var i = 0; i < 50; i++) {
            $scope.data.push("Item " + i);
        }

        $scope.$watch('search', function (newValue, oldValue) {
            if (oldValue !== newValue) {
                $scope.currentPage = 0;
            }
        }, true);

    }


    var Headers = {
        "Content-Type": "application/json"
    };

    var userListReq = function () {
        return {
            method: "get",
            url: _link + "/User/web-getallusers",
            headers: Headers
        };
    };

});

app.filter('startFrom', function () {
    return function (input, start) {
        start = +start;
        return input.slice(start);
    };
});