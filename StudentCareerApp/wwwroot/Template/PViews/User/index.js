var editorUserDesc = new EditorJS({
    holderId: 'user-description',
    placeholder: 'Kendin hakkında birşeyler yaz...',
    data: {
        "time": 1550476186479,
        "blocks": [
            {
                "type": "paragraph",
                "data": {
                    "text": "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec libero sem, eleifend sit amet mauris ut, venenatis ultricies lectus. Phasellus mollis sollicitudin neque. Aenean faucibus felis sed tortor consectetur, et pharetra mauris pretium. Vivamus ac orci eu ipsum congue posuere. Nunc dignissim felis vel tellus pharetra sollicitudin. Nunc finibus, arcu sed tempor scelerisque, sapien urna rhoncus nisl, eu consequat leo sem pulvinar quam. Integer at lacus convallis, dictum ante non, dignissim libero. Phasellus varius sapien nunc, quis commodo lectus cursus id. Nullam vel dui tincidunt, convallis tortor vehicula, facilisis magna."
                }
            },
            {
                "type": "paragraph",
                "data": {
                    "text": "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec libero sem, eleifend sit amet mauris ut, venenatis ultricies lectus. Phasellus mollis sollicitudin neque. Aenean faucibus felis sed tortor consectetur, et pharetra mauris pretium. Vivamus ac orci eu ipsum congue posuere. Nunc dignissim felis vel tellus pharetra sollicitudin. Nunc finibus, arcu sed tempor scelerisque, sapien urna rhoncus nisl, eu consequat leo sem pulvinar quam. Integer at lacus convallis, dictum ante non, dignissim libero. Phasellus varius sapien nunc, quis commodo lectus cursus id. Nullam vel dui tincidunt, convallis tortor vehicula, facilisis magna."
                }
            }
        ]
    }
})
$(document).on('click', '.pointer', function () {
    var area = $(this).attr('for');
    $(this).toggleClass("fa-chevron-up")
    $(this).toggleClass("fa-chevron-down")
    $(`#${area}`).toggle(300);
})
$(document).on('click', '.edit-icon', function () {
    var area = $(this).attr('for');
    $(this).toggleClass("fa-edit");
    $(this).toggleClass("fa-check");
    $(this).toggleClass("green-icon");
    $(this).hasClass("fa-edit") ? $(`#${area}`).attr('contenteditable', false) : $(`#${area}`).attr('contenteditable', true);
    $(`#${area}`).toggleClass('editable-area');
})
$(document).on('click', '.edit-icon-input', function () {
    var area = $(this).attr('for');
    $(this).toggleClass("fa-edit");
    $(this).toggleClass("fa-check");
    $(this).toggleClass("green-icon");
    $(`#${area}`).toggleClass("hidden");
})
$(document).on('click', '#user-save', function () {
    saveChanges();
})

function saveChanges() {
    var data = {
        name: $("#user-name").text(),
        facebook: $("#user-facebook").text(),
        twitter: $("#user-twitter").text(),
        instagram: $("#user-instagram").text(),
        linkedin: $("#user-linkedin").text(),
        address: $("#user-address").text(),
        phone: $("#user-phone").text(),
        mail: $("#user-mail").text(),
        description: ""
    }
    editorUserDesc.save().then((outputData) => {
        data.description = outputData.blocks;
    });
    console.log(data);
}