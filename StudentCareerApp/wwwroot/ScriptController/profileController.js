var app = angular.module('MyApp', []);
app.controller('profileController', function ($scope, $http, $filter) {


    let Header = {
        "Content-Type": "application/json",
        "Authorization": "Bearer " + localStorage.getItem("token")
    };
    $scope.menuList = JSON.parse(localStorage.getItem("menus"));
    $scope.genderList = [{ GenderId: 0, description: 'Seçiniz' }, { GenderId: 1, description: 'Bay' }, { GenderId: 2, description: 'Bayan' }];
    $scope.userModel = {};
    $scope.userModel.GenderId = $scope.genderList[0].description;

    getProfilData();

    function getProfilData() {

        //$.ajax({
        //    url: _link + "/User/web-getinfo",
        //    type: 'GET',
        //    beforeSend: function (xhr) {
        //        xhr.setRequestHeader('Authorization', 'Bearer' + localStorage.getItem("token"));
        //    },
        //    //data: {},
        //    success: function (e) {
        //        if (e.resultCode === 200) {
        //            if (e.data.length > 0) {
        //                $scope.userProfile = e.data;
        //            }
        //        }},
        //    error: function () { }
        //});


        $.ajax({
            method: 'GET',
            contentType: "application/x-www-form-url; charset=urf-8",
            url: _link + "/User/web-getinfo",
            headers: {
                "accept": "application/json",
                "content-type": "application/json",
                "authorization": "Bearer " + localStorage.getItem("token")
            }
        }).done(function (e, statusText, xhdr) {
            if (e.resultCode === 200) {
                $scope.userModel = e.data;
            }
        }).fail(function (xhdr, statusText, errorText) {
        });
        $scope.$apply();
        //$.ajax({
        //    url: _link + "/User/web-getinfo",
        //    type: "GET", async: true,
        //    dataType: Json_,
        //    headers: Header,
        //    contentType: ContentType_,
        //    success: function (e) {
        //        if (e.resultCode === 200) {
        //            if (e.data.length > 0) {
        //                $scope.userProfile = e.data;
        //            }
        //        }
        //    }
        //});
    }


    //var getDataModel = function () {
    //    return {
    //        method: "get",
    //        url: _link + "/User/web-getinfo",
    //        headers: Header,
    //        data: $scope.userModel
    //    };
    //};

});

