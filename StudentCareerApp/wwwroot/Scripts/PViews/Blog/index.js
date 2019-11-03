$(document).on('submit', '#commentForm', function(e){
    debugger;
    e.preventDefault();
    $.ajax({
        url: '/Blog/PostComment',
        type: 'post',
        data: $("#commentForm").serialize(),
        success: function(res){
            if(res.resultCode == 200)
                toastr["success"]("Yorumunuz başarıyla kaydedildi. Onay sürecinden sonra yorumunuz gözükecektir.");
        }
    })
})

$(document).on('click', '#addFavorite', function(e){
    var contentId = e.currentTarget.getAttribute('data-id');
    var isFavorite = e.currentTarget.classList.contains('active');
    $.ajax({
        url: '/Blog/AddOrRemoveFavorite',
        type: 'post',
        data: {ContentId: contentId, IsActive: !isFavorite},
        success: function(res){
            if(res.status){
                toastr["success"](!isFavorite ? "Haber favorilerinize eklendi." : "Haber favorilerinizden çıkarıldı.");
                !isFavorite ? $("#addFavorite").addClass("active") : $("#addFavorite").removeClass('active');
            }
            else
                toastr["error"](res.data.Explanation);

        }
    })
})