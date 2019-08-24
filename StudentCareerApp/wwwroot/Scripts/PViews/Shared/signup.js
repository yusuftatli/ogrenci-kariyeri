﻿var isDetailsLoaded = false;

document.cookie
  .split(";")
  .map(x => x.trim())
  .find(x => x == "acceptCookie=1") == undefined
  ? $(".cookieEnable").removeClass("hidden")
  : $(".cookieEnable").addClass("hidden");

function openLoginMagnific() {
  $.ajax({
    url: '/Shared/_LoginPage',
    type: 'get',
    dataType: 'html',
    success: function(res){
      $.magnificPopup.open({
        items: {
          src: res,
          type: "inline"
        }
      });
    }
  })
  // $.magnificPopup.open({
  //   items: {
  //     src: $("#loginPanel").html(),
  //     type: "inline"
  //   }
  // });
}

function openRegisterMagnific() {
  $.ajax({
    url: '/Shared/_RegisterPage',
    type: 'get',
    dataType: 'html',
    success: function(res){
      $.magnificPopup.open({
        items: {
          src: res,
          type: "inline"
        }
      });
    }
  })

  initRegisterPanel();
}

$(document).on("click", "white-popup", function() {
  $.magnificPopup.close();
});

$(document).on("click", ".openLoginMagnific", function() {
  openLoginMagnific();
});

$(document).on("click", ".openRegisterMagnific", function() {
  openRegisterMagnific();
});

$(document).on("click", "#acceptCookie", function() {
  document.cookie =
    "acceptCookie=1; path=/; Expires=@DateTime.Now.AddYears(1);";
  $(".cookieEnable").addClass("hidden");
});

$(document).on("change", "#educationType", function(e) {
  e.target.value == 2 || e.target.value == 3
    ? $(".template-selected-university").removeClass("hidden")
    : $(".template-selected-university").addClass("hidden");
  e.target.value == 1
    ? $(".template-selected-highschool").removeClass("hidden")
    : $(".template-selected-highschool").addClass("hidden");
  e.target.value > 0 
    ? $(".template-still-student").removeClass("hidden")
    : $(".template-still-student").addClass("hidden");
});

$(document).on('submit', '.loginForm', function(e){
    var form = $(this).serialize();
    $.ajax({
        url: "/Shared/Login",
        type: "post",
        data: form,
        success: function(res){
            console.log(res);
        },
        error: function(res){
            console.log(res);
        }
    })
    e.preventDefault();
})

function initRegisterPanel() {
  new Switchery($("#stillStudent")[0], $("#stillStudent").data());

  document.querySelector(".stillStudent").onchange = function(e) {
    var stillStudent = $("#stillStudent")[0].checked;
    stillStudent
      ? $(".showClasses").removeClass("hidden")
      : $(".showClasses").addClass("hidden");
  };
}
