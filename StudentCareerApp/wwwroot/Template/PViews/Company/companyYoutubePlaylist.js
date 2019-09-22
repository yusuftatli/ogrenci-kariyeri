var vYoutube = new Vue({
    el: '#vue-company-youtube',
    data: {
        model: {},
        playlist: [],
        seoUrl: $("#youtube-info-area").attr('data-seo-url'),
        companyId: $("#youtube-info-area").attr('data-company'),
        urls: {
            getYoutubePlaylist: '/admin/companyclub/GetCompanyYoutubePlayList/',
            addOrUpdateCompanyYoutubePlaylist: '/admin/companyclub/AddOrUpdateCompanyYoutubePlaylist/',
            deleteCompanyYoutubePlaylistItem: '/admin/companyclub/DeleteCompanyYoutubePlaylistItem/'
        }
    },
    mounted: function () {
        this.getYoutubePlaylist();
    },
    methods: {
        getYoutubePlaylist: function () {
            var ths = this;
            $.ajax({
                url: this.urls.getYoutubePlaylist,
                data: { seoUrl: this.seoUrl },
                success: function (res) {
                    if (res.resultCode == 200)
                        ths.playlist = res.data;
                }
            })
        },
        setYoutubeUpdateModel: function (id) {
            this.model = this.playlist.find(x => x.id == id);
            $("#roxyFieldAnnouncement").val(this.model.imagePath);
        },
        addOrUpdateCompanyYoutubePlaylist: function () {
            this.model.seoUrl = this.seoUrl;
            this.model.CompanyId = this.companyId;
            this.model.imagePath = $("#roxyFieldAnnouncement").val();
            var ths = this;
            $.ajax({
                url: this.urls.addOrUpdateCompanyYoutubePlaylist,
                data: { model: this.model },
                type: 'post',
                success: function (res) {
                    if (res.resultCode == 200) {
                        ths.model = {};
                        $("#roxyFieldAnnouncement").val("");
                        ths.getYoutubePlaylist();
                    }
                }
            })
        },
        deleteYoutubePlaylistItem: function (id) {
            var ths = this;
            Swal.fire({
                title: 'Emin misiniz?',
                text: ths.playlist.find(x=>x.id == id).title + " başlıklı duyurunuz silinecektir.",
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Evet, sil gitsin!',
                cancelButtonText: 'Hayır, kalsın.'
            }).then((result) => {
                if (result.value) {
                    $.ajax({
                        url: this.urls.deleteCompanyYoutubePlaylistItem,
                        type: 'post',
                        data: {id: id},
                        success: function(res){
                            if(res.resultCode == 200){
                                ths.getYoutubePlaylist();
                                ths.model.id = 0;
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