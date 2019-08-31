var app = angular.module('MyApp', ['ui.bootstrap']);
app.controller('userListController', function ($scope, $http, $filter) {
    "use strict";

    $scope.userProcess = {};
    getUserList();

    $scope.roleTypes = [{ id: 0, description: '-----' }, { id: 2, description: 'Admin' }, { id: 3, description: 'Öğrenci' }, { id: 4, description: 'Editör' }, { id: 5, description: 'Yazar' }];


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

    $scope.onClikckProcess = function (x) {
        $scope.userProcess.id = x.id;
        $scope.userProcess.roleDescription = x.roleDescription;
        $scope.userProcess.name = x.name;
        $scope.userProcess.surname = x.surname;
        $scope.userProcess.roleType = $scope.roleTypes[0].id;
    };

    $scope.postRoleTypeProcess = function (x) {
        $scope.showSaveLoading = true;
        $http(publishStateReq()).then(function (res) {
            if (res.data.resultCode === 200) {
                shortMessage(res.data.message, "s");
                getUserList();
            } else {
                shortMessage(res.data.message, "e");
            }
            $scope.showSaveLoading = false;
        });
    };

    var publishStateReq = function () {
        return {
            method: "post",
            url: _link + "/User/web-updateUserRoleType",
            headers: Headers,
            data: { userId: $scope.userProcess.id, roleTypeId: $scope.userProcess.roleType }
        };
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