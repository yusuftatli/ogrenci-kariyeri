var vSocialMedia = new Vue({
    el: '#vue-company-social-media',
    data: {
        model: {},
        socialMedias: [],
        companyId: $("#socialmedia-info-area").attr('data-company'),
        urls: {
            getCompanySocialMedias: "/Admin/CompanyClub/GetCompanySocialMedias",
            addOrUpdateCompanySocialMedia: "/Admin/CompanyClub/AddOrUpdateCompanySocialMedia",
            deleteSocialMedia: "/Admin/CompanyClub/DeleteSocialMedia"
        }
    },
    mounted: function(){
        this.getCompanySocialMedias();
    },
    methods: {
        getCompanySocialMedias: function () {
            $.ajax({
                url: this.urls.getCompanySocialMedias,
                data: { 'companyId': this.companyId },
                success: (res) => {
                    if (res.resultCode == 200)
                        this.socialMedias = res.data;
                }
            })
        },
        setSocialMediaUpdateModel: function(id){
            this.model = this.socialMedias.find(x=>x.id == id);
        },
        addOrUpdateCompanySocialMedia: function(){
            this.model.companyClupId = this.companyId;
            this.model.isActive = true;
            $.ajax({
                url: this.urls.addOrUpdateCompanySocialMedia,
                data: { model: this.model},
                type: 'post',
                success: (res) => {
                    if (res.resultCode == 200){
                        this.model = {};
                        this.getCompanySocialMedias();
                        toastr["success"]("Kayıt başarılı!");
                    }
                    else
                        toastr["error"]("Kayıt sırasında bir hata oluştu!");
                }
            })
        },
        deleteSocialMedia: function(id){
            Swal.fire({
                title: 'Emin misiniz?',
                text: this.socialMedias.find(x=>x.id == id).url + " url'e sahip sosyal medya hesabınız sistemden silinecektir.",
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Evet, sil gitsin!',
                cancelButtonText: 'Hayır, kalsın.'
            }).then((result) => {
                if (result.value) {
                    $.ajax({
                        url: this.urls.deleteSocialMedia,
                        type: 'post',
                        data: {id: id},
                        success: (res) => {
                            if(res.resultCode == 200){
                                this.getCompanySocialMedias();
                                this.model.id = 0;
                                toastr["success"]("Silme işlemi başarılı!");
                            }
                        }
                    })
                }
            })
        },
        resetForm: function(){
            this.model = {};
        }
    }
})