function SubmitForm() {
    var search = $("#search").val();
    var type = $("input[name='type']:checked").val();


    $('main').css('display', 'flex');
    $('main').css('justify-content', 'center');
    $('main').css('margin-top', '200px');
    $('main').html('<div class="spinner-border text-light d-flex justify-content-center align-items-center" role="status"><span class= "sr-only"></span></div> ');
    var ww = $(window).width();
    if (ww < 992) {
        $('.navbar-toggler').click();
    }

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
            if (data == 'New Password and Confirm Password Do Not Match' || data == 'Current Password Is Incorrect') {
                $('#CurrentPassword').val('');
                $('#Password').val('');
                $('#ConfirmPassword').val('');
                alert(data)
            }
            else {
                alert(data)
                $('#closeBtn').trigger('click')
            }
        },
        error: function (data) {
            alert(data)
        }
    })
}

