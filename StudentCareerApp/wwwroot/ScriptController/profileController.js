
var app = angular.module("MyApp", []);
app.controller('profileController', function ($scope, $http, $filter) {
    "use strict";

    $scope.menuList = JSON.parse(localStorage.getItem("menus"));

    getProfilData();

    function getProfilData() {
        $http(getDataModel()).then(function (res) {
            if (res.data.resultCode === 200) {
                $scope.userProfile = res.data;
            } else {
                shortMessage(res.data.message, "e");
            }
        });
    }


    let Header = {
        "Content-Type": "application/json",
        "Authorization": "Bearer " + localStorage.getItem("token")
    };
    var getDataModel = function () {
        return {
            method: "get",
            url: _link + "/User/web-getinfo",
            headers: Header,
            data: $scope.userModel
        };
    };

});
