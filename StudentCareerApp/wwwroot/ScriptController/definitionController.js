var app = angular.module("MyApp", ["ui.bootstrap"]);

app.controller('DefinitonManagerController', function ($scope, $http, $filter) {
    "use strict";





    //onClickDashboardTab
    $scope.contentDashboard = [{ quantity: '12' }, { quantity: '12' }, { quantity: '60' }, { quantity: '3' }];

    $scope.onClickDashboardTab = function () {

    };
    $scope.menuList = JSON.parse(localStorage.getItem("menus"));

    //ClassType
    $scope.classTypeModel = {};
    $scope.classTypeModel.classTypeCreateButtonName = "Kaydet";

    $scope.onClickClassTab = function () {
        $scope.getClassTypeList();
    };

    $scope.openClassTypeModal = function () {
        $scope.classTypeModel = {};
        $scope.classTypeModel.classTypeCreateButtonName = "Kaydet";
        focus('classTypeModal');
    };

    $scope.showClassType = function (x) {
        $scope.classTypeModel.classTypeCreateButtonName = "Güncelle";
        $scope.classTypeModel.id = x.id;
        $scope.classTypeModel.description = x.description;
    };

    var ClassTypePost = function () {
        return {
            method: "post",
            url: _link + "/Definition/education-createstudentclass",
            headers: Headers,
            data: $scope.classTypeModel
        };
    };

    $scope.postClassType = function () {
        Loading(true);
        $http(ClassTypePost()).then(function (res) {
            if (res.data.resultCode === 200) {
                $scope.getClassTypeList();
                shortMessage(res.data.message, "s");
                closeModal("classTypeModal");
            }
            Loading(false);
        });
    };

    $scope.getClassTypeList = function () {
        $.ajax({
            url: _link + "/Definition/education-getstudentclass",
            type: "GET",
            dataType: Json_,
            contentType: ContentType_,
            success: function (e) {
                if (e.resultCode === 200) {
                    if (e.data !== undefined) {
                        $scope.classTypeModel.classTypeList = e.data;
                    }
                    $scope.$apply();
                }
            }
        });
    };


    //EducationType
    $scope.educationTypeModel = {};
    $scope.educationTypeModel.EducationTypeCreateButtonName = "Kaydet";

    $scope.onClickEducationTypeTab = function () {
        $scope.getEducationStatusList();
    };

    $scope.openEducationTypeModal = function () {
        $scope.educationTypeModel = {};
        $scope.educationTypeModel.EducationTypeCreateButtonName = "Kaydet";
        focus('EducationTypeModal');
    };

    $scope.showEducationType = function (x) {
        $scope.educationTypeModel.EducationTypeCreateButtonName = "Güncelle";
        $scope.educationTypeModel.id = x.id;
        $scope.educationTypeModel.description = x.description;
    };

    var EducationTypePost = function () {
        return {
            method: "post",
            url: _link + "/Definition/education-createeducationstatus",
            headers: Headers,
            data: $scope.educationTypeModel
        };
    };

    $scope.postEducationType = function () {
        Loading(true);
        $http(EducationTypePost()).then(function (res) {
            if (res.data.resultCode === 200) {
                $scope.getEducationStatusList();
                shortMessage(res.data.message, "s");
                closeModal("EducationTypeModal");
            }
            Loading(false);
        });
        $scope.$apply();
    };

    $scope.getEducationStatusList = function () {
        Loading(true);
        $.ajax({
            url: _link + "/Definition/education-educationstatus",
            type: "GET",
            dataType: Json_,
            contentType: ContentType_,
            success: function (e) {
                if (e.resultCode === 200) {
                    if (e.data.length > 0) {
                        $scope.educationTypeModel.educationTypeList = e.data;
                    }
                    $scope.$apply();
                    Loading(false);
                }
            }
        });
    };

    //HighScholl
    $scope.highSchoolModel = {};
    $scope.highSchoolModel.highSchollCreateButtonName = "Kaydet";

    $scope.onClickhighSchollTab = function () {
        $scope.gethighSchollList();
        $scope.getCities();
    };

    $scope.openhighSchoolModal = function () {
        $scope.highSchoolModel = {};
        $scope.highSchoolModel.highSchoolCreateButtonName = "Kaydet";
        focus('EducationTypeModal');
    };

    $scope.showHighSchool = function (x) {
        $scope.highSchoolModel.highSchoolCreateButtonName = "Güncelle";
        $scope.highSchoolModel.id = x.id;
        $scope.highSchoolModel.cityId = x.cityId;
        $scope.highSchoolModel.schoolname = x.schoolName;
        $scope.highSchoolModel.highschoolcode = x.highSchoolCode;
    };

    var highSchoolPost = function () {
        return {
            method: "post",
            url: _link + "/Definition/education-createhighschool",
            headers: Headers,
            data: $scope.highSchoolModel
        };
    };

    $scope.postHighSchool = function () {
        if (FieldControl($scope.highSchoolModel.cityId)) {
            // if (FieldControl($scope.highSchoolModel.highSchoolCode)) {
            if (Number.isInteger(Number.parseInt($scope.highSchoolModel.highSchoolCode))) {
                if (FieldControl($scope.highSchoolModel.schoolName)) {
                    Loading(true);
                    $http(highSchoolPost()).then(function (res) {
                        if (res.data.resultCode === 200) {
                            $scope.gethighSchollList();
                            shortMessage(res.data.message, "s");
                            closeModal("highSchoolModal");
                        }
                        Loading(false);
                    });
                } else {
                    shortMessage("Okul Adı Boş Geçilemez.", "e");
                }
            } else {
                shortMessage("Okul Kodu rakamlardan oluşmalıdır.", "e");
            }
            // } else {
            //   shortMessage("Okul Kodu Boş Geçilemez.", "e");
            // }

        } else {
            shortMessage("Okul ili Boş Geçilemez.", "e");
        }
    };

    $scope.gethighSchollList = function () {
        Loading(true);
        $.ajax({
            url: _link + "/Definition/education-gethighschool",
            type: "GET",
            dataType: Json_,
            contentType: ContentType_,
            success: function (e) {
                if (e.resultCode === 200) {
                    if (e.data.length > 0) {
                        $scope.highSchoolModel.highSchoolList = e.data;
                    }
                    $scope.$apply();
                    Loading(false);
                }
            }
        });
    };


    //university
    $scope.universityModel = {};
    $scope.universityModel.universityCreateButtonName = "Kaydet";
    $scope.selected = {};
    $scope.showFacultyPage = false;

    $scope.onClickuniversityTab = function () {
        $scope.universityList();
        $scope.getCities();
    };

    $scope.openuniversityModal = function () {
        $scope.universityModel = {};
        $scope.universityModel.universityCreateButtonName = "Kaydet";
        focus('universityModal');
    };

    $scope.showuniversity = function (x) {
        $scope.universityModel.universityCreateButtonName = "Güncelle";
        $scope.universityModel.id = x.id;
        $scope.universityModel.cityId = x.cityId;
        $scope.universityModel.universitycode = x.universityCode;
        $scope.universityModel.universityname = x.universityName;
    };

    var universityPost = function () {
        return {
            method: "post",
            url: _link + "/Definition/education-createuniversity",
            headers: Headers,
            data: $scope.universityModel
        };
    };

    $scope.postuniversity = function () {
        if (FieldControl($scope.universityModel.cityId)) {
            if (FieldControl($scope.universityModel.universitycode)) {
                if (FieldControl($scope.universityModel.universityname)) {
                    Loading(true);
                    $http(universityPost()).then(function (res) {
                        if (res.data.resultCode === 200) {
                            $scope.universityList();
                            shortMessage(res.data.message, "s");
                            closeModal("universityModal");
                        }
                        Loading(false);
                    });
                } else {
                    shortMessage("Üniversite Adı Boş Geçilemez.", "e");
                }
            } else {
                shortMessage("Üniversite Kodu Boş Geçilemez.", "e");
            }
        } else {
            shortMessage("Üniversite İli Boş Geçilemez.", "e");
        }
    };

    $scope.universityList = function () {
        Loading(true);
        $.ajax({
            url: _link + "/Definition/education-getuniversity",
            type: "GET",
            dataType: Json_,
            contentType: ContentType_,
            success: function (e) {
                if (e.resultCode === 200) {
                    if (e.data.length > 0) {
                        $scope.universityModel.universityList = e.data;
                        universityPagin();
                    }
                    $scope.$apply();
                    Loading(false);
                }
            }
        });
    };

    var updataStatuUniversity = function () {
        return {
            method: "post",
            url: _link + "/Definition/education-Update-UniversityIsActive",
            headers: Headers,
            data: {
                Id: x,
                IsActive: true
            }
        };
    };

    $scope.onChageUniversityIsActive = function (x) {
        $http(updataStatuUniversity()).then(function (res) {
            if (res.data.resultCode === 200) {
                $scope.universityList();
                shortMessage(res.data.message, "s");
                closeModal("universityModal");
            }
        });
    };

    function universityPagin() {
        $scope.universitycurrentPage = 0;
        $scope.universitypageSize = 10;
        $scope.universitydata = [];

        $scope.universitygetData = function () {
            return $filter('universityfilter')($scope.universityModel.universityList, $scope.universitysearch);
        };

        $scope.universitynumberOfPages = function () {
            return Math.ceil($scope.universityModel.universityList.length / $scope.universitypageSize);
        };

        for (var i = 0; i < 20; i++) {
            $scope.universitydata.push("Item " + i);
        };

        $scope.$watch('universitysearch', function (newValue, oldValue) {
            if (oldValue !== newValue) {
                $scope.universitycurrentPage = 0;
            }
        }, true);

    }

    //faculty
    $scope.facultyModel = {};
    $scope.facultyModel.facultyCreateButtonName = "Kaydet";
    $scope.selected = {};


    $scope.onClickFacultyTab = function () {
        $scope.facultyList();
    };

    $scope.openfacultyModal = function () {
        $scope.facultyModel = {};
        $scope.facultyModel.facultyCreateButtonName = "Kaydet";
        focus('facultyModal');
    };

    $scope.showFaculty = function (x) {
        $scope.facultyModel.facultyCreateButtonName = "Güncelle";
        $scope.facultyModel.id = x.id;
        $scope.facultyModel.facultyCode = x.facultyCode;
        $scope.facultyModel.facultyName = x.facultyName;
    };

    var facultyPost = function () {
        return {
            method: "post",
            url: _link + "/Definition/education-createfaculty",
            headers: Headers,
            data: $scope.facultyModel
        };
    };

    $scope.postfaculty = function () {
        Loading(true);
        $scope.facultyModel.UniversityId = $scope.selected.universityId;
        $http(facultyPost()).then(function (res) {
            if (res.data.resultCode === 200) {
                $scope.facultyList();
                shortMessage(res.data.message, "s");
                closeModal("facultyModal");
            }
            Loading(false);
        });
    };

    $scope.facultyList = function (x) {
        Loading(true);
        $.ajax({
            url: _link + "/Definition/education-getfaculty",
            type: "GET",
            dataType: Json_,
            contentType: ContentType_,
            success: function (e) {
                if (e.resultCode === 200) {
                    if (e.data.length > 0) {
                        $scope.facultyModel.facultyList = e.data;
                        facultyPagin();
                    } else {
                        $scope.facultyModel.facultyList = {};
                    }
                    $scope.$apply();
                    Loading(false);
                }
            }
        });
    };

    function facultyPagin() {
        $scope.facultycurrentPage = 0;
        $scope.facultypageSize = 10;
        $scope.facultydata = [];

        $scope.facultygetData = function () {
            return $filter('facultyfilter')($scope.facultyModel.facultyList, $scope.facultysearch);
        };

        $scope.facultynumberOfPages = function () {
            return Math.ceil($scope.facultyModel.facultyList.length / $scope.facultypageSize);
        };

        for (var i = 0; i < 20; i++) {
            $scope.facultydata.push("Item " + i);
        };

        $scope.$watch('facultysearch', function (newValue, oldValue) {
            if (oldValue !== newValue) {
                $scope.universitycurrentPage = 0;
            }
        }, true);

    }



    //Department
    $scope.departmentModel = {};
    $scope.departmentModel.departmentCreateButtonName = "Kaydet";
    $scope.selected = {};


    $scope.onClickDepartmentTab = function () {
        $scope.departmentList();
    };

    $scope.opendepartmentModal = function () {
        $scope.departmentModel = {};
        $scope.departmentModel.departmentCreateButtonName = "Kaydet";
        focus('departmentModal');
    };

    $scope.showdepartment = function (x) {
        $scope.departmentModel.departmentCreateButtonName = "Güncelle";
        $scope.departmentModel.id = x.id;
        $scope.departmentModel.departmentCode = x.departmentCode;
        $scope.departmentModel.departmentName = x.departmentName;
    };

    var departmentPost = function () {
        return {
            method: "post",
            url: _link + "/Definition/education-cretadepartment",
            headers: Headers,
            data: $scope.departmentModel
        };
    };

    $scope.postdepartment = function () {
        Loading(true);
        $http(departmentPost()).then(function (res) {
            if (res.data.resultCode === 200) {
                $scope.departmentList();
                shortMessage(res.data.message, "s");
                closeModal("departmentModal");
            }
            Loading(false);
        });
    };

    $scope.departmentList = function (x) {
        Loading(true);
        $.ajax({
            url: _link + "/Definition/education-getdepartment",
            type: "GET",
            dataType: Json_,
            contentType: ContentType_,
            success: function (e) {
                if (e.resultCode === 200) {
                    if (e.data.length > 0) {
                        $scope.departmentModel.departmentList = e.data;
                    } else {
                        $scope.departmentModel.departmentList = {};
                    }
                    $scope.$apply();
                    Loading(false);
                }
            }
        });
    };

    //Sector
    $scope.sectorModel = {};
    $scope.sectorModel.sectorCreateButtonName = "Kaydet";


    $scope.onClicksectorTab = function () {
        $scope.sectorList();
    };

    $scope.opensectorModal = function () {
        $scope.sectorModel = {};
        $scope.sectorModel.sectorCreateButtonName = "Kaydet";
        focus('sectorModal');
    };

    $scope.showSector= function (x) {
        $scope.sectorModel.sectorCreateButtonName = "Güncelle";
        $scope.sectorModel.id = x.id;
        $scope.sectorModel.description = x.description;
    };

    var sectorPost = function () {
        return {
            method: "post",
            url: _link + "/Definition/createsektor",
            headers: Headers,
            data: $scope.sectorModel
        };
    };

    $scope.postsector = function () {
        Loading(true);
        $http(sectorPost()).then(function (res) {
            if (res.data.resultCode === 200) {
                $scope.sectorList();
                shortMessage(res.data.message, "s");
                closeModal("sectorModal");
            }
            Loading(false);
        });
    };

    $scope.sectorList = function (x) {
        Loading(true);
        $.ajax({
            url: _link + "/Definition/getallsector",
            type: "GET",
            dataType: Json_,
            contentType: ContentType_,
            success: function (e) {
                if (e.resultCode === 200) {
                    if (e.data.length > 0) {
                        $scope.sectorModel.sectorList = e.data;
                    } else {
                        $scope.sectorModel.sectorList = {};
                    }
                    $scope.$apply();
                    Loading(false);
                }
            }
        });
    };


    $scope.getCities = function () {
        Loading(true);
        $.ajax({
            url: _link + "/Address/GetCities",
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

    function FieldControl(value) {
        return (value !== undefined && value !== null && value !== 0 && value !== "") ? true : false;
    }

    var Headers = {
        "Content-Type": "application/json"
    };

});


app.filter('universitystartFrom', function () {
    return function (input, start) {
        start = +start;
        return input.slice(start);
    };
});

app.filter('facultystartFrom', function () {
    return function (input, start) {
        start = +start;
        return input.slice(start);
    };
});