var vBasicPage = new Vue({
    el: '#vue-basic-page',
    data: {
        pageList: [],
        search: '',
        urls: {
            getPageList: '/Pages/GetBasicPageList/'
        }
    },
    mounted: function(){

    },
    methods: {
        getPageList: function(){
            $.ajax({
                url: this.urls.getPageList,
                type: 'get',
                success: ((res) => {
                    this.pageList = res.data;
                })
            })
        }
    }
})