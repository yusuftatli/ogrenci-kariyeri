var app = angular.module('MyApp', ['ui.bootstrap']);
app.controller('userCreateController', function ($scope, $http, $filter) {
    "use strict";
    $scope.menuList = JSON.parse(localStorage.getItem("menus"));
    

    $scope.userModel = {};
    $scope.showHigSchoolType = false;
    $scope.showSaveLoading = false;
    $scope.genderList = [{ id: 1, description: 'Bay' }, { id: 2, description: 'Bayan' }];

    loadForm();

    $scope.onClikSave = function () {
        saveUserData();
    };

    function loadForm() {
        getRoleTypeList();
        // getCities();
        //getHighScholl();
        //getEducationTypeList();
        //getUniversityList();
        //getDepartment();
        //getFaculty();
    }

    $scope.onChanageEdcationStatus = function () {
        if (getEducationStatusDes($scope.userModel.educationstatusid) === "Lise") {
            $scope.showHigSchoolType = true;
        } else {
            $scope.showHigSchoolType = false;
        }
    };

    function getEducationStatusDes(x) {
        for (var i = 0; i < $scope.educationTypeList.length; i++) {
            if ($scope.educationTypeList[i]['id'] === x) {
                return $scope.educationTypeList[i]['description'];
            }
        }
    }

    function saveUserData() {
        $scope.showSaveLoading = true;
        if ($scope.userModel.name === undefined || $scope.userModel.name === "") {
            $scope.showSaveLoading = false;
            shortMessage("Ad Boş Geçilemez", "e");
            return;
        }
        if ($scope.userModel.surname === undefined || $scope.userModel.surname === "") {
            $scope.showSaveLoading = false;
            shortMessage("Soyad Boş Geçilemez", "e");
            return;
        }
        if ($scope.userModel.EmailAddress === undefined || $scope.userModel.EmailAddress === "") {
            $scope.showSaveLoading = false;
            shortMessage("Email Adresi Boş Geçilemez", "e");
            return;
        }
        if ($scope.userModel.Password === undefined || $scope.userModel.Password === "") {
            $scope.showSaveLoading = false;
            shortMessage("Şifre Boş Geçilemez", "e");
            return;
        }
        if ($scope.userModel.genderid === undefined || $scope.userModel.genderid === "") {

            $scope.showSaveLoading = false;
            shortMessage("Cinsiyet Boş Geçilemez", "e");
            return;
        }
        if ($scope.userModel.birthdate === undefined || $scope.userModel.birthdate === "") {

            $scope.showSaveLoading = false;
            shortMessage("Doğum Tarihi Boş Geçilemez", "e");
            return;
        }
        if ($scope.userModel.roletypeid === undefined || $scope.userModel.roletypeid === "") {

            $scope.showSaveLoading = false;
            shortMessage("Kullanıcı Rolü Boş Geçilemez", "e");
            return;
        }
        if ($scope.userModel.roleexpiresdate === undefined || $scope.userModel.roleexpiresdate === "") {

            $scope.showSaveLoading = false;
            shortMessage("Role Geçerlilik Tarihi Boş Geçilemez", "e");
            return;
        }
        $http(postUserData()).then(function (res) {
            if (res.data.resultCode === 200) {
                shortMessage(res.data.message, "s");
                $scope.showSaveLoading = false;
            } else {
                shortMessage(res.data.message, "e");
                $scope.showSaveLoading = false;
            }
        });
    }

    function getRoleTypeList() {
        $.ajax({
            url: _link + "/Roles/role-getroletypes",
            type: "GET",
            async: true,
            dataType: Json_,
            contentType: ContentType_,
            success: function (e) {
                if (e.resultCode === 200) {
                    if (e.data.length > 0) {
                        $scope.roleTypeList = e.data;
                    }
                } else {
                    console.log(e);
                }
                $scope.$apply();
            }
        });
    }

    function getEducationTypeList() {
        Loading(true);
        $.ajax({
            url: _link + "/education/education-educationstatus",
            type: "GET", async: true,
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
    }

    function getCities() {
        Loading(true);
        $.ajax({
            url: _link + "/Address/GetCities",
            type: "GET", async: true,
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
    }

    function getHighScholl() {
        Loading(true);
        $.ajax({
            url: _link + "/education/education-gethighschool",
            type: "GET", async: true,
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
    }

    function getUniversityList() {
        Loading(true);
        $.ajax({
            url: _link + "/education/education-getuniversity",
            type: "GET", async: true,
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
    }

    function getDepartment() {
        Loading(true);
        $.ajax({
            url: _link + "/education/education-getdepartment",
            type: "GET",
            dataType: Json_, async: true,
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
    }

    function getFaculty() {
        Loading(true);
        $.ajax({
            url: _link + "/education/education-getfaculty",
            type: "GET", async: true,
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

    let Header = {
        "Content-Type": "application/json",
        "Authorization": "Bearer " + localStorage.getItem("token")
    };
    var postUserData = function () {
        return {
            method: "post",
            url: _link + "/User/user-create-web",
            headers: Header,
            data: $scope.userModel
        };
    };

});

app.directive("ngFileSelect", function (fileReader, $timeout) {
    return {
        scope: {
            ngModel: '='
        },
        link: function ($scope, el) {
            function getFile(file) {
                fileReader.readAsDataUrl(file, $scope)
                    .then(function (result) {
                        $timeout(function () {
                            $scope.ngModel = result;
                        });
                    });
            }

            el.bind("change", function (e) {
                var file = (e.srcElement || e.target).files[0];
                getFile(file);
            });
        }
    };
});


app.factory("fileReader", function ($q, $log) {
    var onLoad = function (reader, deferred, scope) {
        return function () {
            scope.$apply(function () {
                deferred.resolve(reader.result);
            });
        };
    };

    var onError = function (reader, deferred, scope) {
        return function () {
            scope.$apply(function () {
                deferred.reject(reader.result);
            });
        };
    };

    var onProgress = function (reader, scope) {
        return function (event) {
            scope.$broadcast("fileProgress", {
                total: event.total,
                loaded: event.loaded
            });
        };
    };

    var getReader = function (deferred, scope) {
        var reader = new FileReader();
        reader.onload = onLoad(reader, deferred, scope);
        reader.onerror = onError(reader, deferred, scope);
        reader.onprogress = onProgress(reader, scope);
        return reader;
    };

    var readAsDataURL = function (file, scope) {
        var deferred = $q.defer();

        var reader = getReader(deferred, scope);
        reader.readAsDataURL(file);

        return deferred.promise;
    };

    return {
        readAsDataUrl: readAsDataURL
    };
});
