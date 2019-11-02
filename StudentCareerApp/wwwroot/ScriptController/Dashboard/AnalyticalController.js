
var app = angular.module('MyApp', []);
app.controller('AnalyticalController', function ($scope, $http, $filter) {
    "use strict";
    $scope.menuList = JSON.parse(localStorage.getItem("menus"));


});

