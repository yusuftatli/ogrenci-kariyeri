var vCompany = new Vue({
    el: '#vue-company',
    data: {
        company: {},
        companyId: $("#company-info-area").data("company"),
        sectorTypes: [],
        urls:{
            getCompany: "/admin/companyclub/GetCompanyDetails",
            getSectorTypes: "/admin/definition/getallsector",
            addOrUpdateCompany: "/admin/companyclub/"
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
    mounted: function(){
        this.getCompany();
    },
    methods: {
        getCompany: function(){
            $.ajax({
                url: this.urls.getCompany,
                type: 'get',
                data: {id: this.companyId },
                success: (res) => {
                    if(res.resultCode == 200)
                        this.company = res.data;
                    else
                        toastr["error"]("Şirket bilgileri yüklenemedi.");
                }
            })
        },
        getSectorTypes: function(){
            $.ajax({
                url: this.urls.getSectorTypes,
                type: 'get',
                success: (res) => {
                    if (res.resultCode == 200)
                        this.sectorTypes = res.data;
                    else
                        toastr["error"]("Sektör tipleri yüklenemedi.");
                }
            })
        },
        addOrUpdateCompany: function(){
            this.company.headerImage = $(".roxyFieldAnnouncement")[0].value();
            $.ajax({
                url: this.urls.addOrUpdateCompany,
                type: 'post',
                data: {'model': this.company},
                success: (res) => {
                    if (res.resultCode == 200){
                        toastr["success"]("Şirket kayıt işlemi başarılı!");
                        $("#popupModal").modal('hide');
                    }
                    else
                        toastr["error"]("Şirket kayıt işlemi sırasında hata oluştu!");
                }
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
        }
    }
})