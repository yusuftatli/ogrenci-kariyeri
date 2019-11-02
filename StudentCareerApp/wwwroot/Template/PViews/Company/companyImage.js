var vCompanyImage = new Vue({
    el: "#vue-company-images",
    data: {
        images: [],
        companyId: $("#image-info-area").attr('data-company'),
        companyName: $("#image-info-area").attr('data-company-name'),
        urls: {
            getCompanyImages: '/admin/companyclub/getcompanyimages/',
            addCompanyImage: '/admin/companyclub/addcompanyimage/',
            deleteCompanyImage: '/admin/companyclub/deletecompanyimage/'
        }
    },
    mounted: function(){
        this.getCompanyImages();
    },
    methods: {
        getCompanyImages: function(){
            $.ajax({
                url: this.urls.getCompanyImages,
                data: { companyId: this.companyId },
                success: ((res) => {
                    if (res.resultCode == 200)
                        this.images = res.data;
                })
            })
        },
        deleteImage: function(path, id){
            $.ajax({
                url: this.urls.deleteCompanyImage,
                data: {path: path, id: id},
                type: 'post',
                success: function(){
                    if (res.resultCode == 200)
                        toastr["success"]("Silme işlemi başarılı!");
                }
            })
        }
    }
})

var companyDropzone = new Dropzone("#company-dropzone", {
    url: vCompanyImage.urls.addCompanyImage+"?companyId="+vCompanyImage.companyId+"&companyName="+vCompanyImage.companyName,
    init: function(){
        this.on("complete", function(){
            vCompanyImage.getCompanyImages();
        })
    }
});