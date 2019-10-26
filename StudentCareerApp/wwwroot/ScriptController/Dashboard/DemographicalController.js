
var app = angular.module('MyApp', ['ui.bootstrap']);
app.controller('DemographicalController', function ($scope, $http, $filter) {
    "use strict";
    $scope.menuList = JSON.parse(localStorage.getItem("menus"));


});

