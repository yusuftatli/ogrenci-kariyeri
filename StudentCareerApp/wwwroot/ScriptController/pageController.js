var app = angular.module("MyApp", []);

app.controller("pageController", function ($scope, $http, $filter) {
    "use strict";

    $scope.menuList = JSON.parse(localStorage.getItem("menus"));

});

