var app = angular.module("MyApp", ["ui.bootstrap"]);

app.controller('roleTypeController', function ($scope, $http, $filter) {
    "use strict";

    $scope.roleButtonName = "Kaydet";
    $scope.showRoleTypeTable = false;
    $scope.roleTypeList = [];
    $scope.roleModel = {};
    $scope.menuList = JSON.parse(localStorage.getItem("menus"));
    $scope.showSyncMenu = false;

    getRoleTypeList();

    $scope.onClickRoleTab = function () {
        getRoleTypeList();
    };

    $scope.onClickRolePermissionTab = function () {
        $scope.roleButtonName = "Kaydet";
    };

    $scope.onChangeRoleType = function (x) {
        $http(postRoleMenus()).then(function (res) {
            if (res.data.resultCode === 200) {
                $scope.roleTypeWithMenus = res.data.data;
            }
            Loading(false);
        });
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

    $scope.onClikSyncMenu = function () {
        if ($scope.roleTypeId === undefined) {
            shortMessage("Rol Boş geçilemez", "e");
            return;
        }
        $scope.showSyncMenu = false;
        $.ajax({
            url: _link + "/Roles/menu-syncMenu?id=" + $scope.roleTypeId,
            type: "Post",
            dataType: Json_,
            contentType: ContentType_,
            data: { id: $scope.roleTypeId },
            success: function (e) {
                if (e.resultCode === 200) {
                    shortMessage(e.message, "s");
                    //if (e.data.length > 0) {
                    //    shortMessage(e.message, "s");
                    //    // $scope.roleTypeList = e.data;
                    //    $scope.showSyncMenu = true;
                    //    $scope.$apply();
                    //}
                } else {
                    shortMessage(e.message, "e");
                }
            }
        });
    };

    function getRoleTypeList() {
        $scope.showRoleTypeTable = false;
        $.ajax({
            url: _link + "/Roles/role-getroletypes",
            type: "GET",
            dataType: Json_,
            contentType: ContentType_,
            success: function (e) {
                if (e.resultCode === 200) {
                    if (e.data.length > 0) {
                        $scope.roleTypeList = e.data;
                        $scope.showRoleTypeTable = true;
                        $scope.$apply();
                    }
                } else {
                    $scope.showRoleTypeTable = true;
                }
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

    var postRoleMenus = function () {
        return {
            method: "post",
            url: _link + "/Roles/menu-sync-withRoleTypeId? roleTypeId=" + roleTypeId,
            headers: Headers
        };
    };
});

