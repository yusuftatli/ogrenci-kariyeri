var app = angular.module("MyApp", ["ui.bootstrap", "ngVue"]);

app.controller("syncController", function ($scope, $http, $filter) {
    "use strict";



    $scope.syncAssay1 = function () {
        alert("test");
    };

    $scope.syncAssay = function () {
        $.ajax({
            url: _link + "/Sync/SyncAssay",
            type: "GET",
            dataType: Json_,
            contentType: ContentType_
        });
    };

});

