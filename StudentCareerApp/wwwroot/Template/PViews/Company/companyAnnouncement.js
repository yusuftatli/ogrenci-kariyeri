var vCompanyAnnouncement = new Vue({
    el: '#vue-company-announcement',
    data: {
        model: {},
        announcements: [],
        seoUrl: $("#announcement-info-area").attr('data-seo-url'),
        urls: {
            getCompanyAnnouncement: '/admin/companyclub/getcompanyannouncements/',
            addOrUpdateCompanyAnnouncement: '/admin/companyclub/addorupdatecompanyannouncement/'
        }
    },
    mounted: function(){
        this.getCompanyAnnouncement();
    },
    methods: {
        getCompanyAnnouncement: function(){
            $.ajax({
                url: this.urls.getCompanyAnnouncement,
                data: {seoUrl: this.seoUrl},
                success: function(res){
                    if(res.resultCode == 200)
                        this.announcements = res.Data;
                }
            })
        },
        addOrUpdateAnnouncement: function(){
            this.$http.post('/Admin/CompanyClub/AddOrUpdateCompanyAnnouncement/', this.model).then((res) => {
                if(res.IsSuccess()){
                    this.model = {};
                    this.getCompanyAnnouncement();
                }
            })
        }
    }
})