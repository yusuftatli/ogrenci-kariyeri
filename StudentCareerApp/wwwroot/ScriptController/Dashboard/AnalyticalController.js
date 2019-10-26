
var app = angular.module('MyApp', ['ui.bootstrap']);
app.controller('AnalyticalController', function ($scope, $http, $filter) {
    "use strict";
    $scope.menuList = JSON.parse(localStorage.getItem("menus"));


});

