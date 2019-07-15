var app = angular.module('MyApp', ['ui.bootstrap']);
app.controller('userCreateController', function ($scope, $http, $filter) {
    "use strict";

    $scope.userModel = {};
    $scope.showHigSchoolType = false;
    $scope.showSaveLoading = false;
    $scope.genderList = [{ id: 1, description: 'Bay' }, { id: 2, description: 'Bayan' }];

    getCategories();

    $scope.onClikSave = function () {
        console.log($scope.userModel);
        $scope.showSaveLoading = true;
    };

    function loadForm() {
        getRoleTypeList();
        $scope.getCities();
        $scope.getHighScholl();
        $scope.getEducationTypeList();
        $scope.getUniversityList();
        $scope.getDepartment();
        $scope.getFaculty();
    }

    $scope.onChanageEdcationStatu = function () {
        if ($scope.userModel.educationstatusid === 0) {
            $scope.showHigSchoolType = true;
        } else {
            $scope.showHigSchoolType = false;
        }
    };


    function saveUserData() {
        Loading(true);
        $http(postUserData()).then(function (res) {
            if (res.data.resultCode === 200) {
                shortMessage("Success!", "s");
                $scope.showSaveLoading = false;
            } else {
                shortMessage("There is an exception occured", "e");
            }

        });
    }

    function getRoleTypeList() {
        Loading(true);
        $.ajax({
            url: _link + "/Roles/role-getroletypes",
            type: "GET",
            dataType: Json_,
            contentType: ContentType_,
            success: function (e) {
                if (e.resultCode === 200) {
                    if (e.data.length > 0) {
                        $scope.roleTypeList = e.data;
                    }
                }
                $scope.$apply();
                Loading(false);
            }
        });
    }

    $scope.getEducationTypeList = function () {
        Loading(true);
        $.ajax({
            url: _link + "/Roles/education-GetHighSchoolType",
            type: "GET",
            dataType: Json_,
            contentType: ContentType_,
            success: function (e) {
                if (e.resultCode === 200) {
                    if (e.data.length > 0) {
                        $scope.educationTypeList = e.data;
                    }
                }
                $scope.$apply();
                Loading(false);
            }
        });
    };

    $scope.getCities = function () {
        Loading(true);
        $.ajax({
            url: _link + "/Address/address-getcities",
            type: "GET",
            dataType: Json_,
            contentType: ContentType_,
            success: function (e) {
                if (e.resultCode === 200) {
                    if (e.data.length > 0) {
                        $scope.cityList = e.data;
                    }
                }
                Loading(false);
            }
        });
    };

    $scope.getHighScholl = function () {
        Loading(true);
        $.ajax({
            url: _link + "/education/education-gethighschool",
            type: "GET",
            dataType: Json_,
            contentType: ContentType_,
            success: function (e) {
                if (e.resultCode === 200) {
                    if (e.data.length > 0) {
                        $scope.highSchollList = e.data;
                    }
                }
                Loading(false);
            }
        });
    };

    $scope.getUniversityList = function () {
        Loading(true);
        $.ajax({
            url: _link + "/education/education-getuniversity",
            type: "GET",
            dataType: Json_,
            contentType: ContentType_,
            success: function (e) {
                if (e.resultCode === 200) {
                    if (e.data.length > 0) {
                        $scope.universityList = e.data;
                    }
                }
                Loading(false);
            }
        });
    };

    $scope.getDepartment = function () {
        Loading(true);
        $.ajax({
            url: _link + "/education/education-getdepartment",
            type: "GET",
            dataType: Json_,
            contentType: ContentType_,
            success: function (e) {
                if (e.resultCode === 200) {
                    if (e.data.length > 0) {
                        $scope.departmentList = e.data;
                    }
                }
                Loading(false);
            }
        });
    };

    $scope.getFaculty = function () {
        Loading(true);
        $.ajax({
            url: _link + "/education/education-getfaculty",
            type: "GET",
            dataType: Json_,
            contentType: ContentType_,
            success: function (e) {
                if (e.resultCode === 200) {
                    if (e.data.length > 0) {
                        $scope.facultyList = e.data;
                    }
                }
                Loading(false);
            }
        });
    };


    function getData() {
        $scope.categoyData = [
            mainCategory : {
                subCategory: {
                    subCategoryDetail: {

                    }
                }
            }
        ];


        for (var i = 0; i < $scope.catgoryList.length; i++) {
            if (true) {

            }
        }

    }


    function getCategories() {
        Loading(true);
        $.ajax({
            url: _link + "/Category/MainCategoryListWithParents",
            type: "GET",
            dataType: Json_,
            contentType: ContentType_,
            success: function (e) {
                if (e.resultCode === 200) {
                    if (e.data.length > 0) {
                        $scope.catgoryList = e.data;
                        console.log(e.data);
                    }
                }
                Loading(false);
            }
        });
    }

    var postUserData = function () {
        return {
            method: "post",
            url: _link + "/User/CreateUser",
            headers: Headers,
            data: $scope.userModel
        };
    };

});
