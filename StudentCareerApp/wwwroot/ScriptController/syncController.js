var app = angular.module("MyApp", ["ui.bootstrap", "ngVue"]);

app.controller("syncController", function ($scope, $http, $filter) {
    "use strict";


    $scope.syncAssay = function () {
        $http(SyncRequest()).then(function (res) {
        });
    };



    var SyncRequest = function () {
        return {
            method: "get",
            url: _link + "/Sync/SyncAssay",
            headers: Headers
        };
    };

});

