var isDetailsLoaded = false;

document.cookie
  .split(";")
  .map(x => x.trim())
  .find(x => x == "acceptCookie=1") == undefined
  ? $(".cookieEnable").removeClass("hidden")
  : $(".cookieEnable").addClass("hidden");

function openLoginMagnific() {
  $.magnificPopup.open({
    items: {
      src: $("#loginPanel").html(),
      type: "inline"
    }
  });
}

function openRegisterMagnific() {
  $.magnificPopup.open({
    items: {
      src: $("#registerPanel").html(),
      type: "inline"
    }
  });

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

// $(document).on('click', '#registerSubmit', function(){

// })

function initRegisterPanel() {
  new Switchery($("#stillStudent")[0], $("#stillStudent").data());

  document.querySelector(".stillStudent").onchange = function(e) {
    var stillStudent = $("#stillStudent")[0].checked;
    stillStudent
      ? $(".showClasses").removeClass("hidden")
      : $(".showClasses").addClass("hidden");
  };
}
