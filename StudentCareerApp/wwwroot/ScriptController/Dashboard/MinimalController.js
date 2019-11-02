

var app = angular.module('MyApp', []);
app.controller('MinimalController', function ($scope, $http, $filter) {
    "use strict";
    $scope.menuList = JSON.parse(localStorage.getItem("menus"));
   

});

