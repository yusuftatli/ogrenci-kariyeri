var vBasicPage = new Vue({
    el: '#vue-basic-page',
    data: {
        pageList: [],
        search: '',
        urls: {
            getPageList: '/Admin/Pages/GetBasicPageList/'
        },
        menuList : JSON.parse(localStorage.getItem("menus"))

    },
    computed: {
        filteredPageList: function(){
            return this.pageList.filter(x=>x.title.indexOf(this.search) || x.seoUrl.indexOf(this.search));
        }
    },
    mounted: function(){
        this.getPageList();
    },
    methods: {
        getPageList: function(){
            $.ajax({
                url: this.urls.getPageList,
                type: 'get',
                success: ((res) => {
                    if(res.resultCode == 200)
                        this.pageList = res.data;
                })
            })
        }
    }
})