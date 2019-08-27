

var app = angular.module('MyApp', ['ui.bootstrap']);
app.controller('clubController', function ($scope, $http, $filter) {
    "use strict";

    $scope.clubsModel = {};
    $scope.showSaveLoading = false;
    $scope.showTable = true;

    function pagin() {
        $scope.currentPage = 0;
        $scope.pageSize = 20;
        $scope.data = [];

        $scope.getData = function () {
            return $filter('filter')($scope.culubsList, $scope.search);
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
    $scope.onClickclubsList = function () {
        getAllClubs();
    };

    getSectorType();
    getAllClubs();


    $scope.onClikSave = function () {
        $scope.showSaveLoading = true;
        if ($scope.clubsModel.ShortName !== null && $scope.clubsModel.ShortName !== undefined && $scope.clubsModel.ShortName !== "") {
            if ($scope.clubsModel.SectorId !== null && $scope.clubsModel.SectorId !== undefined && $scope.clubsModel.SectorId !== "") {
                if ($scope.clubsModel.WebSite !== null && $scope.clubsModel.WebSite !== undefined && $scope.clubsModel.WebSite !== "") {
                    if ($scope.clubsModel.PhoneNumber !== null && $scope.clubsModel.PhoneNumber !== undefined && $scope.clubsModel.PhoneNumber !== "") {
                        if ($scope.clubsModel.EmailAddress !== null && $scope.clubsModel.EmailAddress !== undefined && $scope.clubsModel.EmailAddress !== "") {
                            $http(ClubsCreateRequest()).then(function (res) {
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

    var ClubsCreateRequest = function () {
        return {
            method: "post",
            url: _link + "/CompanyClubs/web-create-clubs",
            headers: Headers,
            data: $scope.clubsModel
        };
    };

    function getAllClubs() {
        $scope.showTable = true;
        $.ajax({
            url: _link + "/CompanyClubs/web-get-allclubs",
            type: "GET", async: true,
            dataType: Json_,
            contentType: ContentType_,
            success: function (e) {
                if (e.resultCode === 200) {
                    if (e.data.length > 0) {
                        $scope.culubsList = e.data;
                        pagin();
                        $scope.showTable = false;
                        $scope.$apply();
                    }
                } else {
                    shortMessage(res.data.message, "e");
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