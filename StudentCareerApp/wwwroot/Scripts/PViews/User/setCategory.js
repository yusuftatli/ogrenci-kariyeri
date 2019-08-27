var vueCategory = new Vue({
  el: "#vue-category",
  data: {
    selectedCategories: [],
    lastClickedMainCategory: null,
    categories: $("#infoArea").val(),
    urlLinks: {
      getCategoryUrl: "/User/GetCategories"
    }
  },
  mounted: function() {
    this.getCategories();
  },
  methods: {
    //#region
    getCategories: function(id) {
      var ths = this;
      $.ajax({
        url: this.urlLinks.getCategoryUrl,
        type: "get",
        success: function(res) {
          ths.categories = res.data;
        }
      });
    },
    //#endregion
    openMainCategory: function(id) {
      this.lastClickedMainCategory = id;
    },
    switcher: function(s, elm) {
      setTimeout(function() {
        new Switchery($(elm)[0], $(elm).data());
      }, s);
    }
  }
});
