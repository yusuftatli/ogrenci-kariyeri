﻿
var app = angular.module("MyApp", ["ui.bootstrap", "ngVue"]);

app.controller("assayConfirmController", function ($scope, $http, $filter) {
    "use strict";

    $scope.testImage = defaultPage;
    $scope.showSaveLoading = false;
    $scope.showTable = true;

    $scope.assayCreate = {};

    $scope.assayList = [];
    $scope.events = [{ id: 0, description: '-----' }, { id: 1, description: 'Mentor' }, { id: 2, description: 'Kariyer Sohbetleri' }];
    $scope.internList = [{ id: 1, description: '-----' }, { id: 1, description: 'Deneme Staj' }];
    $scope.visibleList = [{ id: 1, description: 'Herkese Açık' }, { id: 1, description: 'Parola Korumalı' }, { id: 1, description: 'Özel' }];

    var trMap = {
        çÇ: "c",
        ğĞ: "g",
        şŞ: "s",
        üÜ: "u",
        ıİ: "i",
        öÖ: "o"
    };

    $scope.MainCategories = [];
    $scope.options = [];
    $scope.tagOptions = [];
    $scope.searchModel = {};

    $scope.onClickDashboard = function () {
        $scope.showTable = true;
        getContentShortList();
    };

    $scope.onClickContent = function () {
        getMainCategories();
        getTags();
        $scope.showSaveLoading = false;
    };

    $scope.showContentDetail = function () {

    };

    $scope.shoShortListAssay = function () {
        getContentShortList();
    };

    //post assay
    $scope.postAssay = function () {
        $scope.assayCreate.ContentDescription = CKEDITOR.instances.ckeditorForAssayContent.getData();
        $scope.assayCreate.HeadLine = $("#toggleHeadLine")[0].checked;
        $scope.assayCreate.ImagePath = $("#roxyField").val();
        $scope.assayCreate.SeoUrl = $("#seoUrl").val();
        $scope.assayCreate.Tags = $("#multipleTags").val();
        $scope.assayCreate.Category = $("#categoryIds").val();
        $scope.assayCreate.PublishDate = moment($("#publishDate").val() + " " + $("#publishHour").val(), 'DD.MM.YYYY HH:mm');

        if ($scope.assayCreate.Header !== null && $scope.assayCreate.Header !== undefined && $scope.assayCreate.Header !== "") {
            if ($scope.assayCreate.Category !== null && $scope.assayCreate.Category !== undefined && $scope.assayCreate.Category !== "") {
                if ($scope.assayCreate.Tags !== null && $scope.assayCreate.Tags !== undefined && $scope.assayCreate.Tags !== "") {
                    if ($scope.assayCreate.ContentDescription !== null && $scope.assayCreate.ContentDescription !== undefined && $scope.assayCreate.ContentDescription !== "") {
                        $scope.showSaveLoading = true;
                        postAssay();
                    } else {
                        shortMessage("İçerik Girilmek Zorundadır", "e");
                    }

                } else {
                    shortMessage("Etiket Seçilmek Zorundadır", "e");
                }
            } else {
                shortMessage("Kategori Seçilmek Zorundadır", "e");
            }

        } else {
            shortMessage("Makale Başlık Boş Geçilemez", "e");
        }
    };

    function pagin() {
        $scope.currentPage = 0;
        $scope.pageSize = 20;
        $scope.data = [];

        $scope.getData = function () {
            return $filter('filter')($scope.assayList, $scope.search);
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

    //#region GET && POST METHODS

    //get main categories
    function getMainCategories() {
        $http(MainCategoriesReq()).then(function (res) {
            $scope.MainCategories = res.data.data;
            $scope.options = $scope.NestedCategory();
        });
    }

    function getTags() {
        $http(TagRequest()).then(function (res) {
            $scope.TagsList = res.data.data;
            $scope.tagOptions = $scope.NestedTags();
        });
    }

    //content list
    function getContentShortList() {
        $scope.showTable = true;
        $scope.searchModel.StartDate = moment($("#searchStartDate").val());
        $scope.searchModel.EndDate = moment($("#searchEndDate").val());
        $scope.searchModel.Category = $("#searhCategoryIds").val();
        $http(GetContentShortListReq()).then(function (res) {
            if (res.data.resultCode === 200) {
                $scope.assayList = res.data.data;
               // pagin();
                $scope.showTable = false;
            } else {
                shortMessage(res.data.message, "e");
                $scope.showTable = false;
            }
        });
    }

    //post assay
    function postAssay() {
        $http(PostAssayReq()).then(function (res) {
            if (res.data.resultCode === 200) {
                shortMessage(res.data.message, "s");
                $scope.showSaveLoading = false;
            }
        });
    }


    //#endregion

    //#region NORMAL JS FUNCTIONS

    $scope.NestedCategory = function (id) {
        var arr = [];
        var model = $scope.MainCategories;
        model.forEach(element => {
            if (element.parentId === id) {
                var item = {
                    label: element.description,
                    id: element.id
                };
                var nested = $scope.NestedCategory(element.id);
                if (nested.length > 0)
                    item = Object.assign({}, item, { children: nested });
                arr.push(item);
            }
        });
        return arr;
    };

    $scope.NestedTags = function (id) {
        var arr = [];
        var model = $scope.TagsList;
        model.forEach(element => {
            if (element.parentId === id) {
                var item = {
                    label: element.description,
                    id: element.id
                };
                var nested = $scope.NestedTags(element.id);
                if (nested.length > 0)
                    item = Object.assign({}, item, { children: nested });
                arr.push(item);
            }
        });
        return arr;
    };

    $scope.RegexSeo = function (link) {
        for (var key in trMap) {
            link = link.replace(new RegExp("[" + key + "]", "g"), trMap[key]);
        }
        return link
            .toLowerCase()
            .replace(/[^a-z0-9]+/g, "-")
            .replace(/^-+|-+$/g, "-")
            .replace(/^-+|-+$/g, "");
    };

    //#endregion

    //#region
    var Headers = {
        "Content-Type": "application/json"
    };
    //main category request object
    var MainCategoriesReq = function () {
        return {
            method: "get",
            url: _link + "/category/MainCategoryListWithParents",
            headers: Headers
        };
    };

    var TagRequest = function () {
        return {
            method: "get",
            url: _link + "/Content/GetTags",
            headers: Headers
        };
    };

    //post assay request object
    var PostAssayReq = function () {
        return {
            method: "post",
            url: _link + "/content/contentcreate",
            headers: Headers,
            data: $scope.assayCreate
        };
    };

    //content list
    var GetContentShortListReq = function () {
        return {
            method: 'post',
            url: _link + "/content/contentshortlist",
            headers: Headers,
            data: $scope.searchModel
        };
    }


    //send content for un publish process
    var contentUnPublishProcess = function () {
        return {
            method: 'post',
            url: _link + "/content/content-sendConfirmUnPublishProcess",
            headers: Headers,
            data: $scope.searchModel
        };
    }
    //#endregion
    getContentShortList();
    getMainCategories();

});

app.directive("treeselect", createVueComponent =>
    createVueComponent(Vue.component("treeselect", VueTreeselect.Treeselect))
);


app.filter('startFrom', function () {
    return function (input, start) {
        start = +start;
        return input.slice(start);
    }
});