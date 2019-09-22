var vCompanyAnnouncement = new Vue({
    el: '#vue-company-announcement',
    data: {
        model: {},
        announcements: [],
        seoUrl: $("#announcement-info-area").attr('data-seo-url'),
        companyId: $("#announcement-info-area").attr('data-company'),
        urls: {
            getCompanyAnnouncement: '/admin/companyclub/getcompanyannouncements/',
            addOrUpdateCompanyAnnouncement: '/admin/companyclub/addorupdatecompanyannouncement/',
            deleteCompanyAnnouncement: '/admin/companyclub/DeleteCompanyAnnouncement/'
        }
    },
    mounted: function () {
        this.getCompanyAnnouncement();
    },
    methods: {
        getCompanyAnnouncement: function () {
            var ths = this;
            $.ajax({
                url: this.urls.getCompanyAnnouncement,
                data: { seoUrl: this.seoUrl },
                success: function (res) {
                    if (res.resultCode == 200)
                        ths.announcements = res.data;
                }
            })
        },
        setAnnouncementUpdateModel: function (id) {
            var announcement = this.announcements.find(x => x.announcementId == id);
            this.model = Object.assign({}, announcement, {
                endDate: moment(announcement.endDate).format('YYYY-MM-DD'),
                createdDate: moment(announcement.createdDate).format('YYYY-MM-DD')
            });
            $("#roxyFieldAnnouncement").val(announcement.imagePath);
        },
        addOrUpdateAnnouncement: function () {
            this.model.SeoUrl = this.seoUrl;
            this.model.CompanyId = this.companyId;
            this.model.Id = this.model.announcementId;
            this.model.ImagePath = $("#roxyFieldAnnouncement").val();
            var ths = this;
            $.ajax({
                url: this.urls.addOrUpdateCompanyAnnouncement,
                data: { model: this.model },
                type: 'post',
                success: function (res) {
                    if (res.resultCode == 200) {
                        ths.model = {};
                        $("#roxyFieldAnnouncement").val("");
                        ths.getCompanyAnnouncement();
                    }
                }
            })
        },
        deleteAnnouncement: function (id) {
            var ths = this;
            Swal.fire({
                title: 'Emin misiniz?',
                text: ths.announcements.find(x=>x.announcementId == id).title + " başlıklı duyurunuz silinecektir.",
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Evet, sil gitsin!',
                cancelButtonText: 'Hayır, kalsın.'
            }).then((result) => {
                if (result.value) {
                    $.ajax({
                        url: this.urls.deleteCompanyAnnouncement,
                        type: 'post',
                        data: {id: id},
                        success: function(res){
                            if(res.resultCode == 200){
                                ths.getCompanyAnnouncement();
                                ths.model.announcementId = 0;
                                toastr["success"]("Silme işlemi başarılı!");
                            }
                        }
                    })
                }
            })
        },
        resetForm: function () {
            this.model = {};
            $("#roxyFieldAnnouncement").val("");
        }
    }
})