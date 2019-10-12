var vCompany = new Vue({
    el: '#vue-company',
    data: {
        company: {},
        companyId: $("#company-info-area").data("company"),
        urls:{
            getCompany: "/admin/companyclub/GetCompanyDetails",
            addOrUpdateCompany: "/admin/companyclub/"
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
        addOrUpdateCompany: function(){
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
        }
    }
})