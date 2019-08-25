

var app = angular.module('MyApp', ['ui.bootstrap']);
app.controller('comapanyController', function ($scope, $http, $filter) {
    "use strict";

    $scope.companyModel = {};
    $scope.showSaveLoading = false;


    GetSctorType();



    $scope.onClikSave = function () {
        $scope.showSaveLoading = true;
        if ($scope.companyModel.ShortName !== null && $scope.companyModel.ShortName !== undefined && $scope.companyModel.ShortName !== "") {
            if ($scope.companyModel.SectorId !== null && $scope.companyModel.SectorId !== undefined && $scope.companyModel.SectorId !== "") {
                if ($scope.companyModel.WebSite !== null && $scope.companyModel.WebSite !== undefined && $scope.companyModel.WebSite !== "") {
                    if ($scope.companyModel.PhoneNumber !== null && $scope.companyModel.PhoneNumber !== undefined && $scope.companyModel.PhoneNumber !== "") {
                        if ($scope.companyModel.EmailAddress !== null && $scope.companyModel.EmailAddress !== undefined && $scope.companyModel.EmailAddress !== "") {

                            $http(CompanyCreateRequest()).then(function (res) {
                               
                                
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
            url: _link + "/CompanyClubs/web-create-company",
            headers: Headers,
            data: $scope.companyModel
        };
    };



    function GetSctorType() {
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
