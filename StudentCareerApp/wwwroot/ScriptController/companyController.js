

var app = angular.module('MyApp', ['ui.bootstrap']);
app.controller('comapanyController', function ($scope, $http, $filter) {
    "use strict";

    $scope.companyModel = {};
    $scope.showSaveLoading = false;
    $scope.showTable = true;

    function pagin() {
        $scope.currentPage = 0;
        $scope.pageSize = 20;
        $scope.data = [];

        $scope.getData = function () {
            return $filter('filter')($scope.companyList, $scope.search);
        };

        $scope.numberOfPages = function () {
            return Math.ceil($scope.getData().length / $scope.pageSize);
        };

        for (var i = 0; i < 50; i++) {
            $scope.data.push("Item " + i);
        };

        $scope.$watch('search', function (newValue, oldValue) {
            if (oldValue !== newValue) {
                $scope.currentPage = 0;
            }
        }, true);

    }
    $scope.onClickcompanyList = function () {
        getAllCompany();
    };
    $scope.menuList = JSON.parse(localStorage.getItem("menus"));
    getSectorType();
    getAllCompany();



    var trMap = {
        çÇ: "c",
        ğĞ: "g",
        şŞ: "s",
        üÜ: "u",
        ıİ: "i",
        öÖ: "o"
    };

    $scope.RegexSeo = function (link) {
        if (link === undefined) {
            link = "-";
        }
        for (var key in trMap) {
            link = link.replace(new RegExp("[" + key + "]", "g"), trMap[key]);
        }
        return link
            .toLowerCase()
            .replace(/[^a-z0-9]+/g, "-")
            .replace(/^-+|-+$/g, "-")
            .replace(/^-+|-+$/g, "");
    };

    $scope.onClikSave = function () {
        $scope.showSaveLoading = true;
        if ($scope.companyModel.ShortName !== null && $scope.companyModel.ShortName !== undefined && $scope.companyModel.ShortName !== "") {
            if ($scope.companyModel.SectorId !== null && $scope.companyModel.SectorId !== undefined && $scope.companyModel.SectorId !== "") {
                if ($scope.companyModel.WebSite !== null && $scope.companyModel.WebSite !== undefined && $scope.companyModel.WebSite !== "") {
                    if ($scope.companyModel.PhoneNumber !== null && $scope.companyModel.PhoneNumber !== undefined && $scope.companyModel.PhoneNumber !== "") {
                        if ($scope.companyModel.EmailAddress !== null && $scope.companyModel.EmailAddress !== undefined && $scope.companyModel.EmailAddress !== "") {
                            $scope.companyModel.SeoUrl = $scope.RegexSeo($scope.companyModel.ShortName);
                            $scope.companyModel.HeaderImage = $("#roxyField").val();
                            $scope.companyModel.ImageDirectory = '/AdminFiles/CMS/Content/Companies/' + $scope.RegexSeo($scope.companyModel.ShortName);
                            $http(CompanyCreateRequest()).then(function (res) {
                                if (res.data.resultCode === 200) {
                                    shortMessage(res.data.message, "s");
                                    $("#companyList").show();
                                    $("#createCompany").hide();
                                    $("#photoGallery").hide();
                                } else {
                                    shortMessage(res.data.message, "e");
                                }
                            });
                            $scope.showSaveLoading = false;
                        } else {
                            shortMessage("Şirket E-Posta Adresi Boş Geçilemez", "e");
                        }
                    } else {
                        shortMessage("Şirket Telefon Numarası Boş Geçilemez", "e");
                    }
                } else {
                    shortMessage("Şirket Web Sitesi Boş Geçilemez", "e");
                }
            } else {
                shortMessage("Şirket Sektör Türü Seçilmek Zorundadır", "e");
            }
        } else {
            shortMessage("Şirket Kısa Adı Boş Geçilemez", "e");
        }
        $scope.showSaveLoading = false;
    };



    var Headers = {
        "Content-Type": "application/json"
    };

    var CompanyCreateRequest = function () {
        return {
            method: "post",
            url: "/Admin/CompanyClub/AddOrUpdateCompany",
            headers: Headers,
            data: $scope.companyModel
        };
    };

    function getAllCompany() {
        $scope.showTable = true;
        $.ajax({
            url: _link + "/CompanyClubs/web-get-allcompanies",
            type: "GET", async: true,
            dataType: Json_,
            contentType: ContentType_,
            success: function (e) {
                if (e.resultCode === 200) {
                    if (e.data.length > 0) {
                        $scope.companyList = e.data;
                        pagin();
                        $scope.showTable = false;
                        $scope.$apply();
                    }
                } else {
                    shortMessage(e.message, "e");
                    $scope.showTable = false;
                }
            }
        });

    }

    function getSectorType() {
        $.ajax({
            url: _link + "/Definition/getallsector",
            type: "GET", async: true,
            dataType: Json_,
            contentType: ContentType_,
            success: function (e) {
                if (e.resultCode === 200) {
                    if (e.data.length > 0) {
                        $scope.sectorTypes = e.data;
                    }
                }
            }
        });
    }


});

app.filter('startFrom', function () {
    return function (input, start) {
        start = +start;
        return input.slice(start);
    };
});