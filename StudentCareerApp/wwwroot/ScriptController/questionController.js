var app = angular.module('MyApp', ['ui.bootstrap']);
app.controller('questionController', function ($scope, $http, $filter) {
    "use strict";

    $scope.questionList = [];
    $scope.questionOptionsList = [];
    $scope.questionAnswer = [];
    $scope.testImage = defaultPage;
    $scope.contentDashboard = [{ quantity: '140', percent: '25' }, { quantity: '12', percent: '30' }, { quantity: '60', percent: '20' }, { quantity: '3', percent: '40' }];
    
    var trMap = {
        çÇ: "c",
        ğĞ: "g",
        şŞ: "s",
        üÜ: "u",
        ıİ: "i",
        öÖ: "o"
    };

    $scope.onAddQuestionAnswer = function () {
        var data = {
            guidId: getId(), first: '', second: '', description: ''
        };
        $scope.questionAnswer.push(data);
    }

    $scope.onDeleteQuestionAnswer = function (answerId) {
        var index = 0;
        for (var i = 0; i < $scope.questionAnswer.length; i++) {
            if ($scope.questionAnswer[i].guidId === answerId) {
                $scope.questionAnswer.splice(i, 1);
            }
        }
    }


    $scope.addQuestion = function () {
        var data = {
            guidId: getId(), question: '', description: '', image: defaultPage, questionOptionList: []
        };
        $scope.questionList.push(data);
    }

    $scope.hideOptions = function(elmId){
        $(elmId).hasClass("hidden") ? $(elmId).removeClass("hidden")
                                    && $(elmId+"_hider").text("Seçenekleri Gizle (" + $(elmId+"_hider").attr('data-count') +")")
                                    : $(elmId).addClass("hidden")
                                    && $(elmId+"_hider").text("Seçenekleri Göster (" + $(elmId+"_hider").attr('data-count') +")");
    }

    $scope.postTest = function () {
        $scope.testData.questionList = $scope.questionList;
        if($scope.questionAnswer.length > -1){
            $scope.questionAnswer.forEach(answer => {
                $scope.questionAnswer[$scope.questionAnswer.indexOf(answer)].description = CKEDITOR.instances['ckeditorForAnswer_'+answer.guidId].getData();
            });
        }
        $scope.testData.url = $("#testDataUrl").val();
        $scope.testData.testValues = $scope.questionAnswer;
        postTest();
    }   

    $scope.addOptions = function (x) {
        var data = { guidId: getId(), option: '', description: '', image: defaultPage, totalMarks: 0 };
        for (var i = 0; i < $scope.questionList.length; i++) {
            if ($scope.questionList[i].guidId == x.guidId) {
                $scope.questionList[i].questionOptionList.push(data);
            }
        }
    }

    $scope.clicktest = function () {
        $scope.testData.questionList = $scope.questionList;
        if($scope.questionAnswer.length > -1){
            $scope.questionAnswer.forEach(answer => {
                $scope.questionAnswer[$scope.questionAnswer.indexOf(answer)].description = CKEDITOR.instances['ckeditorForAnswer_'+answer.guidId].getData();
            });
        }
        $scope.testData.url = $("#testDataUrl").val();
        $scope.testData.testValues = $scope.questionAnswer;
        console.log($scope.testData);
    }

    $scope.deleteQuestion = function (x) {
        var index = 0;
        for (var i = 0; i < $scope.questionList.length; i++) {
            if ($scope.questionList[i].guidId === x) {
                $scope.questionList.splice(i, 1);
            }
        }
    }

    $scope.deleteOption = function (questionId, optionId) {
        var index = 0;
        for (var x = 0; x < $scope.questionList.length; x++) {
            if ($scope.questionList[x].guidId === questionId) {
                for (var y = 0; y < $scope.questionList[x].questionOptionList.length; y++) {
                    if ($scope.questionList[x].questionOptionList[y].guidId === optionId) {
                        $scope.questionList[x].questionOptionList.splice(y, 1);
                    }
                }
            }
        }
    }

    $scope.onChangeOptionName = function (x, y) {
        for (var i = 0; i < length; i++) {
            if ($scope.questionList[i].guidId == x.guidId) {
                if (true) {

                }
            }
        }
    }

    $scope.onChangeQuestionName = function (x) {
        for (var i = 0; i < $scope.questionList.length; i++) {
            if ($scope.questionList[i].guidId == x.guidId) {
                $scope.questionList[i].question == x.question;
            }
        }
    }

    $scope.onChangeQuestionOption = function (x) {
        for (var i = 0; i < $scope.questionOptionsList.length; i++) {
            if ($scope.questionOptionsList[i].guidId == x.guidId) {
                $scope.questionOptionsList[i].option == x.option;
            }
        }
    }
    
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

    $scope.initDropify = function(elmId){
        setTimeout(() => {
            $(elmId).dropify();
        }, 100);
    }

    $scope.initCK = function(elmId){
        setTimeout(() => {
            CKEDITOR.replace(elmId);
        }, 100);
    }

    var getId = function () {
        return '_' + Math.random().toString(36).substr(2, 9);
    };

    //#region REQUEST
    function postTest(){
        $http(CreateQuestionReq()).then(function(res){
            console.log(res.result.data);
        });
    }
    //#endregion

    //#region REQUEST OPTIONS
    var Headers = {
        "Content-Type": "application/json"
    };

    var CreateQuestionReq = function(){
        return {
            method: 'post',
            url: _link + "/question/createtest",
            headers: Headers,
            data: $scope.testData
        };
    }
    //#endregion
});

app.directive("ngFileSelect", function (fileReader, $timeout) {
    return {
        scope: {
            ngModel: '='
        },
        link: function ($scope, el) {
            function getFile(file) {
                fileReader.readAsDataUrl(file, $scope)
                    .then(function (result) {
                        $timeout(function () {
                            $scope.ngModel = result;
                        });
                    });
            }

            el.bind("change", function (e) {
                var file = (e.srcElement || e.target).files[0];
                getFile(file);
            });
        }
    };
});

app.factory("fileReader", function ($q, $log) {
    var onLoad = function (reader, deferred, scope) {
        return function () {
            scope.$apply(function () {
                deferred.resolve(reader.result);
            });
        };
    };

    var onError = function (reader, deferred, scope) {
        return function () {
            scope.$apply(function () {
                deferred.reject(reader.result);
            });
        };
    };

    var onProgress = function (reader, scope) {
        return function (event) {
            scope.$broadcast("fileProgress", {
                total: event.total,
                loaded: event.loaded
            });
        };
    };

    var getReader = function (deferred, scope) {
        var reader = new FileReader();
        reader.onload = onLoad(reader, deferred, scope);
        reader.onerror = onError(reader, deferred, scope);
        reader.onprogress = onProgress(reader, scope);
        return reader;
    };

    var readAsDataURL = function (file, scope) {
        var deferred = $q.defer();

        var reader = getReader(deferred, scope);
        reader.readAsDataURL(file);

        return deferred.promise;
    };

    return {
        readAsDataUrl: readAsDataURL
    };
});