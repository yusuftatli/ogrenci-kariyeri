
var app = angular.module("MyApp", ["ui.bootstrap", "ngVue"]);

app.controller("pageController", function ($scope, $http, $filter) {
    "use strict";

    $scope.testImage = defaultPage;
    $scope.showSaveLoading = false;
    $scope.showTable = true;

    $scope.pageCreate = {};
    $scope.contentProcess = {};


    $scope.pageList = [];
    var trMap = {
        çÇ: "c",
        ğĞ: "g",
        şŞ: "s",
        üÜ: "u",
        ıİ: "i",
        öÖ: "o"
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
    getPageList();
    $scope.onClickPageList = function () {
        $("#pageListTap").show();
        $("#pageTab").hide();
        getPageList();
    };

    $scope.onClickPage = function () {
        $("#pageListTap").hide();
        $("#pageTab").show();
        getPageList();
    };


    //post assay
    $scope.postAssay = function () {
        $scope.assayCreate.ContentDescription = CKEDITOR.instances.ckeditorForAssayContent.getData();
        $scope.assayCreate.isHeadLine = $("#togglwPublish")[0].checked;
        $scope.assayCreate.isManset = $("#toggleManset")[0].checked;
        $scope.assayCreate.isMainMenu = $("#toggleMainMenu")[0].checked;
        $scope.assayCreate.isConstantMainMenu = $("#toggleisConstantMainMenu")[0].checked;
        $scope.assayCreate.IsSendConfirm = $("#toggleisSendConfirm")[0].checked;
        $scope.assayCreate.ImagePath = $("#roxyField").val();
        $scope.assayCreate.SeoUrl = $("#seoUrl").val();
        $scope.assayCreate.Tags = $("#multipleTags").val();
        $scope.assayCreate.Category = $("#categoryIds").val();
        $scope.assayCreate.PublishDate = moment($("#publishDate").val() + " " + $("#publishHour").val(), 'DD.MM.YYYY HH:mm');

        if ($scope.assayCreate.header !== null && $scope.assayCreate.header !== undefined && $scope.assayCreate.header !== "") {
            if ($scope.assayCreate.Category !== null && $scope.assayCreate.Category !== undefined && $scope.assayCreate.Category !== "") {
                if ($scope.assayCreate.Tags !== null && $scope.assayCreate.Tags !== undefined && $scope.assayCreate.Tags !== "") {
                    if ($scope.assayCreate.ContentDescription !== null && $scope.assayCreate.ContentDescription !== undefined && $scope.assayCreate.ContentDescription !== "") {
                        //if (moment($scope.assayCreate.PublishDate < moment())) {
                        //    shortMessage("İçerik yayınlama tarihi geçmiş tarih olamaz", "e");
                        // } else {
                        $scope.showSaveLoading = true;
                        postAssay();
                        // }
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

    //content list
    function getPageList() {
        $http({
            method: 'get',
            url: _link + "/Pages/web-getAllPage",
            headers: Headers
        }).then(function (res) {
            if (res.data.resultCode === 200) {
                $scope.pageList = res.data.data;
                pagin();
                $scope.showTable = false;
            } else {
                shortMessage(res.data.message, "e");
                $scope.showTable = false;
            }
        });
    }

    $scope.showContentDetail = function (x) {
        getContentById(x);
    };




    function changeSwitchery(element, checked) {
        if ((element.is(':checked') && checked === false) || (!element.is(':checked') && checked === true)) {
            element.parent().find('.switchery').trigger('click');
        }
    }


    //get content by id
    function getContentById(x) {
        changeSwitchery($("#togglwPublish"), $scope.assayCreate.isHeadLine);
        $.ajax({
            url: _link + "/Content/getContentbyid",
            type: "GET",
            data: { id: x },
            dataType: Json_,
            contentType: ContentType_,
            success: function (e) {
                if (e.resultCode === 200) {
                    getMainCategories();
                    // getTags();
                    console.log($scope.assayCreate);

                    $scope.assayCreate.header = e.data.header;
                    $scope.assayCreate.eventId = e.data.eventId;
                    $scope.assayCreate.internId = e.data.internId;
                    $scope.assayCreate.visibleId = e.data.visibleId;
                    $scope.categoryValue = e.data.category != null ? e.data.category.split(',') : "";
                    $scope.tagsValue = e.data.tags != null ? e.data.tags.split(',') : "";
                    CKEDITOR.instances.ckeditorForAssayContent.setData(e.data.contentDescription);
                    $("#roxyField").val(e.data.imagePath);
                    $("#img_roxyField").attr('src', e.data.imagePath);
                    $("#publishHour").val(moment(e.data.publishDate).format('HH:mm'));
                    $("#publishDate").datepicker("setDate", new Date(moment(e.data.publishDate)));

                    changeSwitchery($("#togglwPublish"), e.data.isHeadLine);
                    changeSwitchery($("#toggleManset"), e.data.isManset);
                    changeSwitchery($("#toggleMainMenu"), e.data.isMainMenu);
                    changeSwitchery($("#toggleisConstantMainMenu"), e.data.isConstantMainMenu);
                    $("#publishDate").val(e.data.publishDate);

                    //$scope.assayCreate.eventId = $scope.events[0];

                    $("#AssayCreate").show();
                    $("#home").hide();

                    $("#dashboardTab").removeClass("active");
                    $("#contentTab").addClass("active");

                } else {
                    shortMessage("Hata Meydana Geldi", "e");
                }
            }
        });
    }

    //post assay
    function postAssay() {
        $http(PostAssayReq()).then(function (res) {
            if (res.data.resultCode === 200) {
                shortMessage("Kayıt İşlemi Başarılı", "s");
                $scope.showSaveLoading = false;
            }
        });
    }




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

    //#endregion

    //#region
    var Headers = {
        "Content-Type": "application/json"
    };

    var PostAssayReq = function () {
        return {
            method: "post",
            url: _link + "/content/contentcreate",
            headers: Headers,
            data: $scope.assayCreate
        };
    };

    //content list
    var getPageListReq = function () {
        return {
            method: 'get',
            url: _link + "/Pages/web-getAllPage",
            headers: Headers
        };
    };


});

app.directive("treeselect", createVueComponent =>
    createVueComponent(Vue.component("treeselect", VueTreeselect.Treeselect))
);

app.filter('startFrom', function () {
    return function (input, start) {
        start = +start;
        return input.slice(start);
    };
});