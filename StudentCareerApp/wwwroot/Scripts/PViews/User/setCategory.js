var vueCategory = new Vue({
  el: "#vue-category",
  data: {
    selectedCategories: $("#infoArea").attr('data-selected-categories').split(',').filter(x=>x > 0),
    lastClickedMainCategory: 0,
    categories: $("#infoArea").val(),
    urlLinks: {
      getCategoryUrl: "/User/GetCategories",
      postCategories: "/User/PostCategories"
    }
  },
  mounted: function () {
    this.getCategories();
  },
  methods: {
    //#region
    getCategories: function (id) {
      var ths = this;
      $.ajax({
        url: this.urlLinks.getCategoryUrl,
        type: "get",
        success: function (res) {
          ths.categories = res.data;
        }
      });
    },
    postCategories: function(){
      var ths = this;
      var categories = this.selectedCategories.join(',');
      $.ajax({
        url: this.urlLinks.postCategories,
        type: 'post',
        data: {categories: categories},
        success: function(res){
          if(res.resultCode == 200){
            window.location.href = window.location.origin;
          }
          else
            toastr["error"](res.message);
        }
      })
    },
    changeSwitchery: function (element, checked) {
      if ((element.is(':checked') && checked === false) || (!element.is(':checked') && checked === true)) {
        element.parent().find('.switchery').trigger('click');
      }
    },
    setSelectState: function (id, checked) {
      !checked ? this.selectedCategories.splice(this.selectedCategories.indexOf(id), 1) : this.selectedCategories.push(id);
    },
    //#endregion
    openMainCategory: function (id, checked) {
      this.lastClickedMainCategory = id;
    },
    switcher: function (s, elm) {
      var ths = this;
      setTimeout(function () {
        new Switchery($("#" + elm)[0], $("#" + elm).data());
        ths.changeSwitchery($("#"+elm), ths.selectedCategories.filter(x=>x == $("#"+elm).attr('data-id')).length > 0)
        document.getElementById(elm).onchange = function (e) {
          ths.setSelectState(e.target.getAttribute('data-id'), e.target.checked);
        };
      }, s);
    }
  }
});
