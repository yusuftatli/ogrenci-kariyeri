﻿var isDetailsLoaded = false;

toastr.options = { timeOut: 1500 };

function set4guid() {
  return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
}

document.cookie
  .split(";")
  .map(x => x.trim())
  .find(x => x === "acceptCookie=1") === undefined
  ? $(".cookieEnable").removeClass("hidden")
  : $(".cookieEnable").addClass("hidden");

!document.cookie
  .split(';')
  .find(x => x.indexOf('okgdy') > -1)
  ? document.cookie = "okgdy=" +
  set4guid() + "-" +
  set4guid() + "-" +
  set4guid() + "-" +
  set4guid() + "; path=/; Expires=" + moment().add('Y', 1).toDate() : "";

function openLoginMagnific() {
  $.ajax({
    url: "/Shared/_LoginPage",
    type: "get",
    dataType: "html",
    success: function (res) {
      $.magnificPopup.open({
        items: {
          src: res,
          type: "inline"
        }
      });
    }
  });
}

function openRegisterMagnific() {
  $.ajax({
    url: "/Shared/_RegisterPage",
    type: "get",
    dataType: "html",
    success: function (res) {
      $.magnificPopup.open({
        items: {
          src: res,
          type: "inline"
        }
      });
      initRegisterPanel();
    }
  });
}

function openForgetMagnific() {
  $.ajax({
    url: '/Shared/_ForgetPasswordPage',
    type: 'get',
    dataType: 'html',
    success: function (res) {
      $.magnificPopup.open({
        items: {
          src: res,
          type: 'inline'
        }
      })
    }
  });
}

$(document).on("click", "white-popup", function () {
  $.magnificPopup.close();
});

$(document).on("click", ".openLoginMagnific", function () {
  openLoginMagnific();
});

$(document).on("click", ".openRegisterMagnific", function () {
  openRegisterMagnific();
});

$(document).on('click', '.openForgetMagnific', function () {
  openForgetMagnific();
})

$(document).on("click", "#acceptCookie", function () {
  document.cookie =
    "acceptCookie=1; path=/; Expires=" + moment().add('Y', 1).toDate() + ";";
  $(".cookieEnable").addClass("hidden");
});

$(document).on("change", "#educationType", function (e) {
  let val = e.target.value;
  if (val === "1" || val === "4")
    $(".template-selected-highschool").removeClass("hidden");
  else
    $(".template-selected-highschool").addClass("hidden");

  if (val === "2" || val === "3" || val === "8" || val === "9" || val === "5" || val === "6")
    $(".template-selected-university").removeClass("hidden");
  else
    $(".template-selected-university").addClass("hidden");

  if (val === "4" || val === "6" || val === "3" || val === "8" || val === "9")
    $(".showGraduate").removeClass("hidden");
  else
    $(".showGraduate").addClass("hidden");

  if (val === "8" || val === "9")
    $(".template-selected-master").removeClass("hidden");
  else
    $(".template-selected-master").addClass("hidden");

  if (val === "9")
    $(".showMasterGraduate").removeClass("hidden");
  else
    $(".showMasterGraduate").addClass("hidden");

  if (val === "1" || val === "5" || val === "2" || val === "8")
    $(".showClasses").removeClass("hidden");
  else
    $(".showClasses").addClass("hidden");
});

$(document).on("submit", ".loginForm", function (e) {
  var form = $(this).serialize();
  $.ajax({
    url: "/Shared/Login",
    type: "post",
    data: form,
    success: function (res) {
      if (res.resultCode === 200) {
        toastr["success"]("Seni aramızda görmekten çok mutlu olduk :)", res.message);
        document.location.reload();
      } else toastr["error"](res.message, "Birşeyler hatalı!");
    },
    error: function (res) {
      console.log(res);
    }
  });
  e.preventDefault();
});

$(document).on('submit', '.forgetForm', function (e) {
  $("#spinner-for-pass").toggle("hidden");
  var form = $(this).serialize();
  $("#inputUsernameEmail").attr('disabled', '');
  $(".forgetForm button[type='submit']").toggle('hidden');
  $.ajax({
    url: '/Shared/ForgetPassword',
    type: 'post',
    data: form,
    success: function (res) {
      if (res.resultCode === 200) {
        $("h4.forgetText").text("Email adresinize bir kurtarma maili gönderdik. Maildeki yönergeleri takip ederek şifrenizi başarıyla değiştirebilirsiniz.");
        $("#spinner-for-pass").toggle("hidden");
        toastr["success"]("Email adresinize bir kurtarma maili gönderdik. Maildeki yönergeleri takip ederek şifrenizi başarıyla değiştirebilirsiniz.")
      }
    }
  })
  e.preventDefault();
})

$(document).on('submit', '.changePassword', function (e) {
  var form = $(this).serialize();
  $.ajax({
    url: '/Shared/ChangeForgottenPassword',
    type: 'post',
    data: form,
    success: function (res) {
      if (res.resultCode === 200) {
        toastr["success"]("Şifre değiştirme işlemi başarılı!");
        setTimeout(() => {
          document.location.href = document.location.origin;
        }, 500);
      }
      else
        toastr["error"](res.message);
    }
  })
  e.preventDefault();
})

$(document).on("click", "#logoutButton", function (e) {
  $.ajax({
    url: "/Shared/Logout",
    type: "get",
    success: function (res) {
      toastr["success"](res.message, "Çıkış yaptınız.");
      document.location.reload();
    }
  });
});

function initRegisterPanel() {
  new Switchery($("#stillStudent")[0], $("#stillStudent").data());

  document.querySelector(".stillStudent").onchange = function (e) {
    var stillStudent = $("#stillStudent")[0].checked;
    stillStudent
      ? $(".showClasses").removeClass("hidden")
      : $(".showClasses").addClass("hidden");
  };
}
