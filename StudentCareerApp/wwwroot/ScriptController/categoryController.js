$(".js-switch").on('change', function () {
    console.log(10);
})

var app = angular.module('MyApp', ['ui.bootstrap']);
app.controller('categoryController', function ($scope, $http, $filter) {
    "use strict";

    $scope.tstss = false;
    $scope.category = {};
    //create models
    $scope.categoryCrateModel = {};
    $scope.subCategoryCreateModel = {};
    $scope.subCategoryDetailCreateModel = {};
    $scope.updateCategoryIsActiveModel = {};

    $scope.subCategoryTableShow = true;
    $scope.subCategoryDetailTableShow = false;

    $scope.mainCategoryDescription = "";
    $scope.subCategoryDescription = "";

    $scope.menuList = JSON.parse(localStorage.getItem("menus"));
    MainCategoryList();
    // mainCategoryCreate();

    //$scope.onChangeIsActive = function (x) {
    //    for (var i = 0; i < $scope.category.mainCategoryList.length; i++) {
    //        if (x.isActive) {
    //            $scope.category.mainCategoryList[i].isActive = false;
    //        } else {
    //            $scope.category.mainCategoryList[i].isActive = true;
    //        }

    //    }
    //}

    //main category add function
    $scope.mainCategoryAdd = function () {
        mainCategoryCreate();
    }

    //sub category add function
    $scope.subCategoryAdd = function () {
        console.log($scope.subCategoryCreateModel);
        subCategoryCreate();
    }

    //sub category detail add function
    $scope.subCategoryDetailAdd = function (id) {
        $scope.subCategoryDetailCreateModel.Id = id;
        subCategoryDetailCreate();
    }

    $scope.showSubCategory = function (x) {
        $scope.mainCategoryDescription = " - " + x.description;
        $scope.subCategoryCreateModel = { parentId: x.id };
        SubCategoryList(x.id);
        $scope.category.subCategoryDetailList = [];
        $scope.subCategoryDetailCreateModel = {};
    }

    $scope.shoSubCategoryDetail = function (x) {
        $scope.subCategoryDescription = " - " + x.description;
        $scope.subCategoryDetailCreateModel = { parentId: x.id };
        SubCategoryDetailList(x.id);
    }

    //#region INSERT & UPDATE FUNCTIONS
    function mainCategoryCreate() {
        $scope.categoryCrateModel.IsActive = 1;
        Loading(true);
        $http(MainCategoryCreateReq()).then(function (res) {
            res.data.resultCode === 200 ? ($scope.categoryCrateModel.id = res.data.data)
                && $scope.category.mainCategoryList.push($scope.categoryCrateModel)
                && ($scope.categoryCrateModel = {})
                && shortMessage("Success!", "s")
                : shortMessage("There is an exception occured.", "e");
            Loading(false);
        });
    }

    function subCategoryCreate() {
        Loading(true);
        $scope.subCategoryCreateModel.IsActive = 1;
        $http(SubCategoryCreateReq()).then(function (res) {
            res.data.resultCode === 200 ? ($scope.subCategoryCreateModel.id = res.data.data)
                && $scope.category.subCategoryList.push($scope.subCategoryCreateModel)
                && console.log($scope.subCategoryCreateModel)
                && ($scope.subCategoryCreateModel = { parentId: $scope.subCategoryCreateModel.parentId })
                && shortMessage("Success!", "s")
                : shortMessage("There is an exception occured", "e");
            Loading(false);
        });
    }

    function subCategoryDetailCreate() {
        Loading(true);
        $scope.subCategoryDetailCreateModel.IsActive = 1;
        $http(SubCategoryDetailCreateReq()).then(function (res) {
            res.data.resultCode === 200 ? ($scope.subCategoryDetailCreateModel.Id = res.data.data)
                && $scope.category.subCategoryDetailList.push($scope.subCategoryDetailCreateModel)
                && ($scope.subCategoryDetailCreateModel = { parentId: $scope.subCategoryDetailCreateModel.parentId })
                && shortMessage("Success!", "s")
                : shortMessage("There is an exception occured", "e");
            Loading(false);

        });
    }

    $scope.onClickMainCategoryIsActive = function (id, state) {
        $("#spin_" + id).removeClass("hidden");
        $scope.updateCategoryIsActiveModel.id = id;
        $scope.updateCategoryIsActiveModel.state = state;
        $http(updateCategoryIsActive()).then(function (res) {
            console.log(res.data.resultCode);
            res.data.resultCode === 200 ? shortMessage("Güncelleme Başarılı!", "s")
                : shortMessage("There is an exception occured", "e");
            $("#spin_" + id).addClass("hidden");
        });
    };
    //#endregion

    //#region LIST FUNCTIONS


    var Headers = {
        "Content-Type": "application/json",
        "Authorization": "Bearer " + localStorage.getItem("token")
    };

    function MainCategoryList() {

        let Header = {
            "Content-Type": "application/json",
            "Authorization": "Bearer " + localStorage.getItem("token")
        };

        $.ajax({
            url: _link + "/Category/MainCategoryListWithParents",
            type: 'GET',
            contentType: 'application/json',
            headers: Header,
            success: function (e) {
                if (e.resultCode === 200) {
                    $scope.category.mainCategoryList = e.data.filter(x=>x.parentId === null);
                    // $scope.switcher(100);
                }
                $scope.$apply();
            },
            error: function (error) {
                if (error.status === 401) {
                    window.location.href = 'https://localhost:44308/admin/Login/Login';
                }
            }
        });

    }

    function SubCategoryList(id) {
        Loading(true);
        $.ajax({
            url: _link + "/Category/MainCategoryList",
            type: "GET",
            data: { parentId: id },
            dataType: Json_,
            contentType: ContentType_,
            success: function (e) {
                if (e.resultCode === 200) {
                    $scope.category.subCategoryList = e.data;
                }
                $scope.$apply();
            }
        });
        Loading(false);
    }

    function SubCategoryDetailList(id) {
        Loading(true);
        $.ajax({
            url: _link + "/Category/MainCategoryList",
            type: "GET",
            data: { parentId: id },
            dataType: Json_,
            contentType: ContentType_,
            success: function (e) {
                if (e.resultCode === 200) {
                    $scope.category.subCategoryDetailList = e.data;
                }
                $scope.$apply();
            }
        });
        Loading(false);
    }

    //#endregion

    //#region  NORMAL FUNCTIONS
    $scope.switcher = function (s, elm) {
        setTimeout(function () {
            new Switchery($(elm)[0], $(elm).data());
            document.querySelector(elm).onchange = function (e) {
                $scope.onClickMainCategoryIsActive(elm.split('_').reverse()[0], e.target.checked);
            };
        }, s);
    };
    //#endregion

    //#region REQUESTS

    var MainCategoryCreateReq = function () {
        return {
            method: "post",
            url: _link + "/Category/MainCategoryCreate",
            headers: Headers,
            data: $scope.categoryCrateModel
        };
    };

    var SubCategoryCreateReq = function () {
        return {
            method: "post",
            url: _link + "/Category/MainCategoryCreate",
            headers: Headers,
            data: $scope.subCategoryCreateModel
        };
    };

    var SubCategoryDetailCreateReq = function () {
        return {
            method: "post",
            url: _link + "/Category/MainCategoryCreate",
            headers: Headers,
            data: $scope.subCategoryDetailCreateModel
        };
    };

    var updateCategoryIsActive = function () {
        return {
            method: "post",
            url: _link + `/Category/MainCategoryStatusUpdate?id=${$scope.updateCategoryIsActiveModel.id}&state=${$scope.updateCategoryIsActiveModel.state}`,
            headers: Headers
        };
    };

    //#endregion

});
