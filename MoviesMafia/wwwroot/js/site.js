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
            $('main').html(data);
        },
        error: function (data) {
            console.error(data)
        }

    })
}