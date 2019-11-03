
var app = angular.module("MyApp", []);

app.controller("commnetsController", function ($scope, $http, $filter) {
    "use strict";
    $scope.menuList = JSON.parse(localStorage.getItem("menus"));

    $scope.aprrpveComment = true;
    $scope.roleTypeList = [{ id: 1, description: 'Test' }, { id: 2, description: 'Haber' }];
    getDashboard();
    getAllComments();

    $scope.onChangeReadType = function () {
        getAllComments();
    };

    $scope.showReadType = function () {
        if ($scope.readTypeId === undefined || $scope.readTypeId === "") {
            shortMessage("Kategori Boş Geçilemez", "e");
            return;
        }
        getDashboard();
        $scope.commentList = null;
        $scope.$apply();
        getAllComments();
    };

    function pagin() {
        $scope.currentPage = 0;
        $scope.pageSize = 20;
        $scope.data = [];

        $scope.getData = function () {
            return $filter('filter')($scope.commentList, $scope.search);
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

    function getAllComments() {
        $.ajax({
            url: _link + "/Content/comment-GetAllCommentsPendingApproval",
            type: "GET", async: true,
            dataType: Json_,
            data: { readType: $scope.readTypeId },
            contentType: ContentType_,
            success: function (e) {
                if (e.resultCode === 200) {
                    if (e.data.length > 0) {
                        $scope.commentList = e.data;
                        pagin();
                        $scope.$apply();
                    } else {
                        $scope.commentList = null;
                        $scope.$apply();
                    }
                } else {
                    shortMessage(e.message, "e");
                }
            }
        });
    }

    $scope.ApproveComment = function (x) {
        approveComment(x);
    };

    function approveComment(x) {
        $scope.aprrpveComment = false;
        $.ajax({
            url: _link + "/Content/comment-approve",
            type: "GET", async: true,
            dataType: Json_,
            data: { id: x },
            contentType: ContentType_,
            success: function (e) {
                if (e.resultCode === 200) {
                    shortMessage("Yorum onaylandı", "s");

                    for (var i = 0; i < $scope.commentList.length; i++) {
                        if ($scope.commentList[i].id === x) {
                            $scope.commentList[i].buttonName = "Onaylandı";
                            $scope.commentList[i].buttonClass = "danger";
                        }
                    }
                    getDashboard();
                    $scope.aprrpveComment = true;
                    $scope.$apply();


                } else {
                    $scope.aprrpveComment = true;
                    shortMessage("Yorum onaylanırken hata meydana geldi", "e");
                }
            }
        });
    }
    function getDashboard() {
        $.ajax({
            url: _link + "/Content/comment-Dashboard",
            type: "GET", async: true,
            dataType: Json_,
            contentType: ContentType_,
            success: function (e) {
                if (e.resultCode === 200) {
                    $scope.DashboardData = e.data;


                }
            }
        });

    }

    function isOk() {
        for (var i = 0; i < $scope.commentList.length; i++) {
            if ($scope.commentList[i].buttonName === "Onaylandı") {
                return false;
            }
        }
        return true;
    }


});

app.filter('startFrom', function () {
    return function (input, start) {
        start = +start;
        return input.slice(start);
    };
});