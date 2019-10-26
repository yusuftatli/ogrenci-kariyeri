
var app = angular.module("MyApp", [""]);

app.controller("menuController", function ($scope, $http, $filter) {
    "use strict";

    getMenu();

    function getMenu() {
        $.ajax({
            url: _link + "/Roles/role-GetScreens",
            type: "GET",
            dataType: Json_,
            contentType: ContentType_,
            success: function (e) {
                if (e.resultCode === 200) {
                    localStorage.setItem("menus", e.data);
                } else {
                    shortMessage("Menu yüklenirken hata meydana geldi", "e");
                }
            }
        });
    }
});

