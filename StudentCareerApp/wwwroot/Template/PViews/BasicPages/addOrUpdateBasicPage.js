var vAddOrUpdateBasicPage = new Vue({
    el: "#vue-basic-page-add-or-update",
    data: {
        pageId: $("#page-info-area").data('page-id'),
        basicPage: {},
        urls: {
            getBasicPage: '/Admin/Pages/GetBasicPageDetails',
            addOrUpdateBasicPage: '/Admin/Pages/AddOrUpdateBasicPage'
        },
        trMap: {
            çÇ: "c",
            ğĞ: "g",
            şŞ: "s",
            üÜ: "u",
            ıİ: "i",
            öÖ: "o"
        }
    },
    created: function(){
        
    },
    mounted: function(){

        CKEDITOR.replace('ckeditorForBasicPageDesc');
        new Switchery($("#toggleActive")[0], $("#toggleActive").data());
        this.$nextTick(() => {
            this.getBasicPage();
        })
    },
    methods: {
        getBasicPage: function(){
            $.ajax({
                url: this.urls.getBasicPage,
                data: { id: this.pageId },
                type: 'get',
                success: (res => {
                    if (res.resultCode == 200 && !!res.data) {
                        this.basicPage = res.data;
                        $("#roxyFieldAnnouncement")[0].value = this.basicPage.imagePath;
                        this.changeSwitchery($("#toggleActive"), this.basicPage.isActive);
                        setTimeout(() => {
                            CKEDITOR.instances.ckeditorForBasicPageDesc.setData(res.data.description);
                        }, 200);
                    }
                })
            })
        },
        addOrUpdateBasicPage: function(){
            this.basicPage.imagePath = $("#roxyFieldAnnouncement")[0].value;
            this.basicPage.description = CKEDITOR.instances.ckeditorForBasicPageDesc.getData();
            this.basicPage.id = this.pageId;
            this.basicPage.seoUrl = this.regexSeo(this.basicPage.title);
            $.ajax({
                url: this.urls.addOrUpdateBasicPage,
                data: {model: this.basicPage},
                type: 'post',
                success: (res => {
                    if(res.resultCode == 200){
                        toastr["success"]("Sayfa kaydı başarıyla tamamlandı.");
                        $("#popupModalModal").modal('hide');
                    }
                    else
                        toastr["error"]("Sayfa kaydı sırasında hata oluştu.");
                })
            })
        },
        regexSeo: function(link){
            if (link === undefined) {
                link = "-";
            }
            for (var key in this.trMap) {
                link = link.replace(new RegExp("[" + key + "]", "g"), this.trMap[key]);
            }
            return link
                .toLowerCase()
                .replace(/[^a-z0-9]+/g, "-")
                .replace(/^-+|-+$/g, "-")
                .replace(/^-+|-+$/g, "");
        },
        changeSwitchery: function(element, checked) {
            if ((element.is(':checked') && checked === false) || (!element.is(':checked') && checked === true)) {
                element.parent().find('.switchery').trigger('click');
            }
        }
    }
})