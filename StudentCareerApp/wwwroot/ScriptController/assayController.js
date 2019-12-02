$(document).ready(function () {
    CKEDITOR.replace('ckeditorForAssayContent');

    $("#publishHour").clockpicker({
        placement: 'top',
        align: 'left',
        autoclose: true,
        'default': 'now'
    });

    $("#searchStartDate").datepicker({
        format: 'dd.mm.yyyy'
    });
    $("#searchEndDate").datepicker({
        format: 'dd.mm.yyyy'
    })

    $("#publishDate").datepicker({
        format: 'dd.mm.yyyy'
    });
})



var app = angular.module("MyApp", ["ui.bootstrap", "ngVue"]);

app.controller("assayController", function ($scope, $http, $filter) {
    "use strict";

    $scope.testImage = defaultPage;
    $scope.showSaveLoading = false;
    $scope.showTable = true;

    $scope.assayCreate = {};
    $scope.contentProcess = {};


    $scope.assayList = [];
    $scope.events = [{ id: 0, description: '-----' }, { id: 1, description: 'Mentor' }, { id: 2, description: 'Kariyer Sohbetleri' }];
    $scope.internList = [{ id: 1, description: '-----' }, { id: 1, description: 'Deneme Staj' }];
    $scope.visibleList = [{ id: 1, description: 'Herkese Açık' }, { id: 1, description: 'Parola Korumalı' }, { id: 1, description: 'Özel' }];
    $scope.confirmType = [{ id: 0, description: '-----' }, { id: 1, description: 'Taslak' }, { id: 2, description: 'Yayın Aşamasında' }, { id: 3, description: 'Yayında Değil' }, { id: 4, description: 'Yayında' }];
    $scope.events = [{ id: 0, description: '-----' }, { id: 1, description: 'Mentor' }, { id: 2, description: 'Kariyer Sohbetleri' }];
    $scope.platformType = [{ id: 0, description: '-----' }, { id: 1, description: 'Mobil' }, { id: 2, description: 'Web' }, { id: 3, description: 'Web/Mobil' }];
    $scope.menuList = JSON.parse(localStorage.getItem("menus"));


    var trMap = {
        çÇ: "c",
        ğĞ: "g",
        şŞ: "s",
        üÜ: "u",
        ıİ: "i",
        öÖ: "o"
    };
    $scope.menuList = JSON.parse(localStorage.getItem("menus"));

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

    $scope.MainCategories = [];
    $scope.options = [];
    $scope.tagOptions = [];
    $scope.searchModel = {};


    $scope.showComment = function (x) {
        getAllComments(x);
    };

    function getAllComments(x) {
        $.ajax({
            url: _link + "/Content/comment-GetAllCommentsPendingApprovalByContentId",
            type: "GET", async: true,
            dataType: Json_,
            data: { contentId: x },
            contentType: ContentType_,
            success: function (e) {
                if (e.resultCode === 200) {
                    if (e.data.length > 0) {
                        $scope.commentList = {};
                        $scope.commentList = e.data;
                        $("#commentModal").modal('show');
                    } else {
                        $scope.commentList = {};
                    }
                } else {
                    shortMessage(e.message, "e");
                }
            }
        });
    }

    $scope.onClickDashboard = function () {
        $("#AssayCreate").hide();
        $("#home").show();
        getContentShortList();

    };

    $scope.onClickContent = function () {
        $("#AssayCreate").show();
        $("#home").hide();
        getMainCategories();
        getTags();
    };

    $scope.shoShortListAssay = function () {
        getContentShortList();
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

    //#region GET && POST METHODS

    //get main categories
    function getMainCategories() {
        $http(MainCategoriesReq()).then(function (res) {
            $scope.MainCategories = res.data.data;
            $scope.options = $scope.NestedCategory(null);
        });
    }

    function getTags() {
        $http(TagRequest()).then(function (res) {
            $scope.TagsList = res.data.data;
            $scope.tagOptions = $scope.NestedTags();
        });
    }


    $scope.onClikckProcess = function (x) {
        $scope.contentProcess.header = x.header;
        $scope.contentProcess.platformTypeDes = x.platformTypeDes;
        $scope.contentProcess.id = x.id;
    };

    $scope.postPublishState = function () {
        $scope.showSaveLoading = true;
        $http(publishStateReq()).then(function (res) {
            if (res.data.resultCode === 200) {
                shortMessage(res.data.message, "s");
                $("#contentProcessModal").modal('hide');
                getContentShortList();
            } else {
                shortMessage(res.data.message, "e");
            }
            $scope.showSaveLoading = false;
        });
    };

    //content list
    function getContentShortList() {
        $scope.showTable = true;
        $scope.searchModel.startDate = isNaN(moment($("#searchStartDate").val())) ? null : moment($("#searchStartDate").val()).format('DD.MM.YYYY');
        $scope.searchModel.endDate = isNaN(moment($("#searchEndDate").val())) ? null : moment($("#searchEndDate").val()).format('DD.MM.YYYY');
        $scope.searchModel.searhCategoryIds = $("#searhCategoryIds").val();
        $http(GetContentShortListReq()).then(function (res) {
            if (res.data.resultCode === 200) {
                $scope.assayList = res.data.data;
                $scope.assayListRoleType = res.data.roleTypeId;
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
                    $scope.categoryValue = e.data.category !== null ? e.data.category.split(',') : "";
                    $scope.tagsValue = e.data.tags !== null ? e.data.tags.split(',') : "";
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
                shortMessage(res.message, "s");
                $scope.showSaveLoading = false;
            } else {
                shortMessage(res.errorMessage, "e");
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

    var publishStateReq = function () {
        return {
            method: "post",
            url: _link + "/Content/UpdateContentPublish",
            headers: Headers,
            data: { id: $scope.contentProcess.id, publishState: $scope.contentProcess.publishState }
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
    };

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
    };
});