
var app = angular.module("MyApp", ["ui.bootstrap", "ngVue"]);

app.controller("Dashboard1Controller", function ($scope, $http, $filter) {
    "use strict";

   $scope.menuList = JSON.parse(localStorage.getItem("menus"));


   

});

