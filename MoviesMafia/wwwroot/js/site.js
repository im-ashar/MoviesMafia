﻿function SubmitForm() {
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

$(document).ready(function () {
    $("#updatePassword").click(function () {
        if ($("#updatePasswordForm").valid()) {
            UpdateAccount();
        }
    });
});

function UpdateAccount() {
    var CurrentPassword = $('#CurrentPassword').val();
    var Password = $('#Password').val();
    var ConfirmPassword = $('#ConfirmPassword').val();
    $("#updatePasswordHeading").text("Updating...");
    $.ajax({
        url: "/Account/UpdatePassword",
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
                $('#CurrentPassword').val('');
                $('#Password').val('');
                $('#ConfirmPassword').val('');
                alert(data)
                $('#closeBtn').trigger('click')
            }
        },
        error: function (data) {
            alert(data)
        }
    })
}



function updateProfilePicture() {
    var input = document.getElementById("updateProfilePictureInput"); //get file input id
    var updatedProfilePicture = input.files; //get files
    var formData = new FormData(); //create form
    for (var i = 0; i != updatedProfilePicture.length; i++) {
        formData.append("updatedProfilePicture", updatedProfilePicture[i]); //loop through all files and append
    }
    $("#updateProfilePictureHeading").text("Updating...");
    $.ajax(
        {
            url: "/Account/UpdateProfilePicture",
            data: formData, // send it as formData
            processData: false,// tell jQuery not to process the data
            contentType: false,// tell jQuery not to set contentType
            type: "POST", //type is post as we will need to post files
            success: function (data) {
                alert(data);
                $('#closeDpBtn').trigger('click');
                location.reload(true);
            },
            error: function (data) {
                alert(data)
                $('#closeDpBtn').trigger('click')
            }
        }
    );
}

