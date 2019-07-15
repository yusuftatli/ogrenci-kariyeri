var app = angular.module("MyApp", ["ui.bootstrap"]);

app.controller('roleTypeController', function ($scope, $http, $filter) {
    "use strict";

    $scope.roleButtonName = "Kaydet";

    $scope.roleTypeList = [];
    $scope.roleModel = {};

    getRoleTypeList();

    $scope.onClickRoleTab = function () {
        getRoleTypeList();
    };

    $scope.onClickRolePermissionTab = function () {
        $scope.roleButtonName = "Kaydet";
    };

    $scope.showRoleType = function (x) {
        $scope.roleButtonName = "Güncelle";
        $scope.roleModel = x;
    };

    $scope.openRoleModal = function () {
        $scope.roleModel = {};
        $scope.roleButtonName = "Kaydet";
        focus('roleTypeName');
    };

    $scope.roleCreate = function () {
        Loading(true);
        $http(postRoleModel()).then(function (res) {
            if (res.data.resultCode === 200) {
                $scope.roleTypeList = res.data.data;
                shortMessage(res.data.message, "s");
                closeModal("addRoleModal");
            }
            Loading(false);
        });
        $scope.$apply();
    };

    function getRoleTypeList() {
        Loading(true);
        $.ajax({
            url: _link +"/Roles/role-getroletypes",
            type: "GET",
            dataType: Json_,
            contentType: ContentType_,
            success: function (e) {
                if (e.resultCode === 200) {
                    if (e.data.length > 0) {
                        $scope.roleTypeList = e.data;
                    }
                }
                Loading(false);
            }
        });
    }

    var Headers = {
        "Content-Type": "application/json"
    };
    var postRoleModel = function () {
        return {
            method: "post",
            url: _link + "/Roles/role-createroletype",
            headers: Headers,
            data: $scope.roleModel
        };
    };

});

