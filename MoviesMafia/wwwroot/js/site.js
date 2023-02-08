function SubmitForm() {
    var search = $("#search").val();
    var type = $("input[name='type']:checked").val();
    $.ajax({
        url: "/Search/Search",
        type: "POST",
        data: { search: search, type: type },
        success: function (data) {
            $('#main').html(data).fadeIn('slow');
        },
        error: function (data) {
            console.error(data)
        }

    })
}