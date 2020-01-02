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
    $scope.assayHeadrName = "Ekle";
    $scope.assayCreate = {};
    $scope.contentProcess = {};
    $scope.assayButtonName = "Kaydet";

    $scope.assayList = [];
    $scope.contentModel = {};
    $scope.events = [{ Id: 0, Description: 'Seçiniz' }, { Id: 1, Description: 'Mentor' }, { Id: 2, Description: 'Kariyer Sohbetleri' }];
    $scope.internList = [{ Id: 0, Description: 'Seçiniz' }, { Id: 1, Description: 'Deneme Staj' }, { Id: 2, Description: 'Kariyer Soheti' }];
    $scope.visibleList = [{ Id: 0, Description: 'Seçiniz' }, { Id: 1, Description: 'Herkese Açık' }, { Id: 1, Description: 'Parola Korumalı' }, { Id: 1, Description: 'Özel' }];
    $scope.confirmType = [{ Id: "0", Description: 'Seçiniz' }, { Id: "1", Description: 'Taslak' }, { Id: "2", Description: 'Yayın Aşamasında' }, { Id: "3", Description: 'Yayında Değil' }, { Id: "4", Description: 'Yayında' }];
    $scope.events = [{ Id: 0, Description: 'Seçiniz' }, { Id: 1, Description: 'Mentor' }, { Id: 2, Description: 'Kariyer Sohbetleri' }];
    $scope.platformTypeList = [{ Id: 0, Description: 'Seçiniz' }, { Id: 1, Description: 'Mobil' }, { Id: 2, Description: 'Web' }, { Id: 3, Description: 'Web/Mobil' }];
    $scope.menuList = JSON.parse(localStorage.getItem("menus"));
    $scope.contentModel.multipleList = [{ Id: "0", Description: 'Seçiniz' }, { Id: "1", Description: '1' }, { Id: "2", Description: '2' }, { Id: "3", Description: '3' }, { Id: "4", Description: '4' }, { Id: "5", Description: '5' },
    { Id: "6", Description: '6' }, { Id: "7", Description: '7' }, { Id: "8", Description: '8' }, { Id: "9", Description: '9' }, { Id: "10", Description: '10' }, { Id: "20", Description: '20' }, { Id: "50", Description: '50' }];

    getDashboard();
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

    $scope.ApproveComment = function (x) {
        approveComment(x);
    }
    $scope.contentModel.commentShow = false;
    function approveComment(x) {
        $scope.contentModel.commentShow = true;
        $.ajax({
            url: _link + "/Content/comment-approveForContent",
            type: "GET", async: true,
            dataType: Json_,
            data: { id: x },
            contentType: ContentType_,
            success: function (e) {
                if (e.resultCode === 200) {
                    shortMessage(e.message, "s");
                    $scope.commentList = {};
                    $scope.commentList = e.data;
                   
                    $scope.contentModel.commentShow = false;
                    $scope.$apply();


                } else {
                    $scope.contentModel.commentShow = false;
                    shortMessage("Yorum onaylanırken hata meydana geldi", "e");
                }
            }
        });
    }

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
                    $scope.$apply();
                } else {
                    shortMessage(e.message, "e");
                }
            }
        });
       
    }

    $scope.onClickDashboard = function () {
        $("#AssayCreate").hide();
        $("#home").show();
        $scope.assayHeadrName = "Ekle";
        getContentShortList();
        clearAll();

    };

    $scope.onClickContent = function () {
        $scope.assayButtonName = "Kaydet";
        $scope.assayHeadrName = "Ekle";
        clearAll();
        $scope.assayCreate.platformTypeId = $scope.platformTypeList[0];
        $scope.assayCreate.visibleId = $scope.visibleList[0];
        $scope.assayCreate.internId = $scope.events[0];
        $scope.assayCreate.eventId = $scope.internList[0];
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
        debugger
        $scope.contentProcess.header = x.header;
        $scope.contentProcess.platformTypeDes = x.platformTypeDes;
        $scope.contentProcess.id = x.id;
        $scope.contentProcess.publishState = $scope.confirmType[x.publishStateType];

        $scope.readCountList(x.id);

    };

    function findIndexOfPublishState() {

    }


    $scope.contentPublishStateShowLoading = false;
    $scope.postPublishState = function () {
        $scope.contentPublishStateShowLoading = true;
        $http(publishStateReq()).then(function (res) {
            if (res.data.resultCode === 200) {
                shortMessage(res.data.message, "s");
                $scope.contentPublishStateShowLoading = false;
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
        debugger;
       
        $scope.assayButtonName = "Güncelle";
        getMainCategories();
        getTags();
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
            data: { id: x.id },
            dataType: Json_,
            contentType: ContentType_,
            success: function (e) {
                if (e.resultCode === 200) {
                    getMainCategories();
                    // getTags();
                    console.log($scope.assayCreate);
                    $scope.assayHeadrName = "Güncelle";
                    $scope.assayCreate.header = e.data.header;
                    // $scope.categoryValue = "37";// e.data.category !== null ? e.data.category.split(',') : "";
                    debugger;
                    $scope.tagsValue = e.data.tags !== null ? e.data.tags.split(',') : "";
                    CKEDITOR.instances.ckeditorForAssayContent.setData(e.data.contentDescription);
                    $("#roxyField").val(e.data.imagePath);
                    $("#img_roxyField").attr('src', e.data.imagePath);
                    $("#publishHour").val(moment(e.data.publishDate).format('HH:mm'));
                    $("#publishDateAssay").datepicker("setDate", new Date(moment(e.data.publishDate)));
                    $scope.categoryValue = eval(e.data.category);
                    $("#categoryIds").val(e.data.category);
                    $("#multipleTags").val(e.data.tagsValue);


                    // $scope.categoryIds = ["37"];

                    changeSwitchery($("#togglwPublish"), e.data.isHeadLine);
                    changeSwitchery($("#toggleManset"), e.data.isManset);
                    changeSwitchery($("#toggleMainMenu"), e.data.isMainMenu);
                    changeSwitchery($("#toggleisConstantMainMenu"), e.data.isConstantMainMenu);

                    $scope.assayCreate.platformTypeId = $scope.platformTypeList[e.data.platformType];
                    $scope.assayCreate.visibleId = $scope.visibleList[e.data.visibleId];
                    $scope.assayCreate.internId = $scope.events[e.data.internId];
                    $scope.assayCreate.eventId = $scope.internList[e.data.eventId];

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

    function clearAll() {
        $scope.assayCreate.header = "";
        $scope.tagsValue = "";
        CKEDITOR.instances.ckeditorForAssayContent.setData("");
        $("#roxyField").val("");
        $("#img_roxyField").attr('src', "");
        $("#publishHour").val("");
        $("#publishDateAssay").val("")


        changeSwitchery($("#togglwPublish"), false);
        changeSwitchery($("#toggleManset"), false);
        changeSwitchery($("#toggleMainMenu"), false);
        changeSwitchery($("#toggleisConstantMainMenu"), false);

        $scope.assayCreate.platformTypeId = $scope.platformTypeList[0];
        $scope.assayCreate.visibleId = $scope.visibleList[0];
        $scope.assayCreate.internId = $scope.events[0];
        $scope.assayCreate.eventId = $scope.internList[0];
        $scope.categoryValue = [];
        $scope.tagsValue = [];


        $("#dashboardTab").removeClass("active");
        $("#contentTab").addClass("active");
    }
    function getDashboard() {
        $http({
            method: "get",
            url: _link + "/Content/get-dashboard",
            headers: Headers
        }).then(function (res) {
            if (res.status === 200) {
                $scope.DashboardData = res.data.data;
            } else {
                shortMessage(res.data.message, "e");
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
        debugger
        return {
            method: "post",
            url: _link + "/Content/UpdateContentPublish",
            headers: Headers,
            data: { id: $scope.contentProcess.id, publishState: $scope.contentProcess.publishState.Id }
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

    var postContentReadCount = function () {
        return {
            method: "post",
            url: _link + "/Settings/settings-setreadcount-only",
            headers: Headers,
            data: {
                value: parseInt($scope.contentModel.selectedId),
                Id: $scope.contentProcess.id
            }
        };
    };

    $scope.readCountList = function (x) {
        $scope.contentModel.showContentCountLoading = true;
        $.ajax({
            url: _link + "/Settings/settings-multipleReadCount-only",
            type: "GET",
            data: { id: x },
            dataType: Json_,
            contentType: ContentType_,
            success: function (e) {
                if (e.resultCode === 200) {
                    $scope.contentModel.data = e.data;
                    $scope.contentModel.selectedId = $scope.contentModel.multipleList[parseInt(parseInt(getContentCountIndexById($scope.contentModel.data.value)))].Description;
                    $scope.contentModel.showContentCountLoading = false;
                    $scope.$apply();
                }
            }
        });
    };

    $scope.postContent = function () {
        $scope.contentModel.showContentCountPostLoading = true;
        if ($scope.contentModel.selectedId === undefined || $scope.contentModel.selectedId === "0") {
            $scope.contentModel.showContentCountPostLoading = false;
            shortMessage("Okunma sayısı çarpan değeri boş geçilemez", "e");
            return;
        }
        $http(postContentReadCount()).then(function (e) {
            if (e.data.resultCode === 200) {
                shortMessage(e.data.message, "s");
                $scope.contentModel.showContentCountPostLoading = false;
            } else {
                shortMessage(e.data.message, "e");
                $scope.contentModel.showContentCountPostLoading = false;
            }
            $scope.$apply();
        });
    };

    function getContentCountIndexById(value) {
        let res = 0;
        for (let i = 0; i < $scope.contentModel.multipleList.length; i++) {
            if ($scope.contentModel.multipleList[i].Description === value) {
                res = i;
            }
        }
        return res;
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
    };
});