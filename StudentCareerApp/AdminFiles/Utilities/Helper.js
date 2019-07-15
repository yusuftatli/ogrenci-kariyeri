var prodType = "d";//d,p
var _link = '';


if (prodType === "d") {
    _link = "https://localhost:44308/api";
}

var ContentType_ = "application/x-www-form-urlencoded; charset=utf-8;application/json";
var Json_ = "json";
var Get_ = "Get";
var Post_ = "Post";
//list modül
var ProductsGroup = 0;
var Units = 1;
var Supplier = 2;
var Title_ = "PTS";
//list modül

function errorData(value, type) {
    var d = value.substring(3);
    if (type) {
        focus(d);
    }
    messageShow(document.getElementById(value).innerHTML + " Hatalı!");
}

function shortMessage(value, type) {
    if (type == "s") {
        toastr.success(value, "Başarılı!", {
            "fadeIn": 100,
            "fadeOut": 100,
            "timeOut": 3000,
            "extendedTimeOut": 1000
        });
    }
    else if (type == "w") {
        toastr.warning(value, "Uyarı", {
            "fadeIn": 100,
            "fadeOut": 100,
            "timeOut": 3000,
            "extendedTimeOut": 1000
        });
    }
    else if (type == "i") {
        toastr.info(value, "Bilgi", {
            "fadeIn": 100,
            "fadeOut": 100,
            "timeOut": 3000,
            "extendedTimeOut": 1000
        });
    }
    else if (type == "e") {
        toastr.error(value, "Hata", {
            "fadeIn": 100,
            "fadeOut": 100,
            "timeOut": 3000,
            "extendedTimeOut": 1000
        });
    }
}

function canNotBeEmpty(value, type) {

    var d = value.substring(3);
    if (type) {
        focus(d);
    }
    toastr.error(document.getElementById(value).innerHTML + " Boş Geçilemez!", "Hata", {
        "fadeIn": 100,
        "fadeOut": 100,
        "timeOut": 3000,
        "extendedTimeOut": 1000
    });
}

function messageShow(msg) {
    if (msg.length == 0) {
        swal(msg);
    } else {
        swal(msg[0]);
    }
}

function focus(value) {
    $("#" + value + "").focus();
}

function dataControl(type, value) {

    if (!type) {
        return true;
    } else {
        if (value !== undefined && value !== null && value !== 0 && value !== "") {
            return true;
        } else {
            return false;
        }
    }
}



function isCheck(value) {
    return ($('#' + value + '').is(":checked")) ? true : false;
}

function getValue(value) {
    return $('#' + value + '').val();
}

function getBoolen(value) {
    return ($('#' + value + '').val()) ? true : false;
}

function Loading(value) {
    return (value) ? $(".modalload").show() : $(".modalload").hide();
}

function closeModal(value) {
    $('#' + value + '').modal('hide');
}

function showModal(value) {
    $('#' + value + '').modal('show');
}

function Mshow1(msg) {
    swal(msg, { icon: "warning" });
}

function Result(res_) {
    if (res_.IsList) {
        if (res_.IsSession) {
            if (res_.Success) {
                Loading(false);
                return true;
            } else {
                ShowErrorMessage(res_);
                return false;
            }
        } else {
            shortMessage("Oturum kapandı, Login sayfasına yönlendiriliyorsunuz...", "w");
            Loading(false);
            window.location.href = su_ + "/LogIn/Index";
            return false;
        }
    } else {
        var message = null;
        if (res_.IsSession) {
            if (res_.Success) {
                swal(res_.SuccessMessage, { icon: "success" });
                Loading(false);
                return true;
            } else {
                ShowErrorMessage(res_);
            }
        } else {
            Loading(false);
            shortMessage("Oturum kapandı, Login sayfasına yönlendiriliyorsunuz...", "w");
            window.location.href = su_ + "/LogIn/Index";
            return false;
        }
    }
}

function ShowErrorMessage(res_) {
    if (res_.Errors.length > 0) {
        swal(res_.Errors[0], { icon: "warning" });
        Loading(false);
    } else {
        swal(res_.ErrorMessage, { icon: "warning" });
        Loading(false);
    }
}


function Mshow(result) {
    var message = null;
    if (result.Success) {
        Loading(false);
        swal(result.SuccessMessage, { icon: "success" });
    }
    else {
        if (result.Errors.length > 0) {
            Loading(false);
            message = result.Errors[0];
            swal(message, { icon: "warning" });
        }
        else {
            Loading(false);
            swal(result.ErrorMessage, { icon: "warning" });
        }
    }
}

function MessageShow(msg_, type_) {
    return (type_) ? swal(msg_, { icon: "success" }) : swal(msg_, { icon: "warning" });
}

function getValuefromLocalStorage(value) {
    return localStorage.getItem(value);
}

function isNull(value) {
    return (value == null && value == undefined) ? true : false;
}

function fC_(s, p, n) {
    return ((!s[p + n + '_V']) ? true : (s[p + n + '_M']) ? false : true) ? true : ((s[n] !== undefined && s[n] !== null && s[n] !== 0 && s[n] !== "") ? true : false) ? true : false;
}

function paginationLoad(x, x1) {
    x.viewby = 10;
    x.totalItems = x1;
    x.currentPage = 1;
    x.itemsPerPage = x.viewby;
    x.maxSize = 5; //Number of pager buttons to show

    x.setItemsPerPage = function (num) {
        x.itemsPerPage = num;
        x.currentPage = 1; //reset to first page
    }
}


function TabShow(x_, y_) {
    for (var i = 1; i <= x_; i++) {
        if (i == y_) {
            $("#cardTab" + i).show();
        } else {
            $("#cardTab" + i).hide();
        }
    }
}


function isOnline() {
    if (navigator.onLine) {
        return true;
    } else {
        return false;
    }
}

function setFieldValues(e, n, a) {
    if (!emptyControl(e)) {
        messageShow('Depositor için gerekli ekran dil ayarları yüklenemedi');
    } else {
        if (Loading(!0), isOnline()) for (var s = 0; s < e.length; s++)for (var t = 0; t < e.length; t++)e[s].ID == e[t].ID && (n[a][e[t].CODE] = e[t].DESCRIPTION, n[a][e[t].CODE + "_M"] = e[t].ISMANDATORY, n[a][e[t].CODE + "_V"] = e[t].ISVISILE); else shortMessage("İnternet bağlantısı yok!", "w"); Loading(!1)
    }
}




//msg hata mesajı
//type success, error
//head başlık
function ShowMessage(type_, msg_, head_) {
    let type = "";

    if (type_ == true) {
        type = "success";
    } else if (type_ == false) {
        type = "error";
    } else {
        type = "info";
    }

    swal(head_, msg_, type);
}

function emptyControl(value_) {

    if (value_ != 'undefined' && value_ != null && value_ != '0' && value_ != "") {
        return true;
    } else {
        return false;
    }
}
var defaultPage = "data:image/svg+xml;charset=UTF-8,%3Csvg%20width%3D%2264%22%20height%3D%2264%22%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%20viewBox%3D%220%200%2064%2064%22%20preserveAspectRatio%3D%22none%22%3E%3Cdefs%3E%3Cstyle%20type%3D%22text%2Fcss%22%3E%23holder_16abcaa325b%20text%20%7B%20fill%3Argba(255%2C255%2C255%2C.75)%3Bfont-weight%3Anormal%3Bfont-family%3AHelvetica%2C%20monospace%3Bfont-size%3A10pt%20%7D%20%3C%2Fstyle%3E%3C%2Fdefs%3E%3Cg%20id%3D%22holder_16abcaa325b%22%3E%3Crect%20width%3D%2264%22%20height%3D%2264%22%20fill%3D%22%23777%22%3E%3C%2Frect%3E%3Cg%3E%3Ctext%20x%3D%2213.84375%22%20y%3D%2236.453125%22%3E64x64%3C%2Ftext%3E%3C%2Fg%3E%3C%2Fg%3E%3C%2Fsvg%3E";



function showModal(value) {
    $("#" + value).modal("show");
}


function closeModal(value) {
    $("#" + value).modal("hide");
}

function openCustomRoxy2(id) {
    $(id).dialog({ modal: true, width: 875, height: 600 });
}
function closeCustomRoxy2(id) {
    $(id).dialog('close');
}

function paginationLoad(x, x1) {
    x.viewby = 10;
    x.totalItems = x1;
    x.currentPage = 1;
    x.itemsPerPage = x.viewby;
    x.maxSize = 5; //Number of pager buttons to show

    x.setItemsPerPage = function (num) {
        x.itemsPerPage = num;
        x.currentPage = 1; //reset to first page
    }
}