function SubmitForm() {
    var search = $("#search").val();
    var type = $("input[name='type']:checked").val();
    $.ajax({
        url: "/Search/Search",
        type: "POST",
        data: { search: search, type: type },
        success: function (data) {
            $('main').css('display', 'flex');
            $('main').css('flex-wrap', 'wrap');
            $('main').css('justify-content', 'center');
            $('main').css('margin-top', '48px');
            $('main').html(data)
        },
        error: function (data) {
            console.error(data)
        }

    })
}


function UpdateAccount() {
    var CurrentPassword = $('#CurrentPassword').val();
    var Password = $('#Password').val();
    var ConfirmPassword = $('#ConfirmPassword').val();
    $.ajax({
        url: "/Account/UpdateAccount",
        type: "POST",
        data: { CurrentPassword: CurrentPassword, NewPassword: Password, ConfirmPassword: ConfirmPassword },
        success: function (data) {
            alert(data)
            $('#closeBtn').trigger('click')
        },
        error: function (data) {
            alert(data)
        }

    })
}
