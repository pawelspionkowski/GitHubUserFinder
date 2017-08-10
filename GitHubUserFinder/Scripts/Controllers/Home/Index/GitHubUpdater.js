app = {
    getUrl: function () {
        return $(".userNameButton").data('request-url');
    },
    getUserNameTextBox: function () {
        return $('#userNameTextBox').val();
    },
    getJsonObject: function () {
        return jsonObject = {
            "userName": app.getUserNameTextBox()
        };
    },
    execute: function () {
        var form = $(".searchForm");
        form.valid();

        if ($("#userNameTextBox").hasClass("valid"))
        {
            $('#loadingDiv').show();

            return $.ajax({
                url: app.getUrl(),
                type: "POST",
                data: JSON.stringify(app.getJsonObject()),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error: function (response) {
                    console.log(response.responseText);
                },
                failure: function (response) {
                    console.log(response.responseText);
                },
                success: function (response) {

                    if (response.Repository.length == 0)
                        $(".repositories").hide();
                    else
                        $(".repositories").show();

                    $(".repositoriesTable").find("tr:gt(0)").remove();
                    $('.userAvatar').attr('src', response.AvataruUrl);

                    $(".userNameLabel").html(response.Name);
                    $(".userLocationLabel").html(response.Location);
                    $('.browserSpace').css('margin-left', '50px');

                    $(".userDescriptionSpace").show();

                    for (var k in response.Repository) {
                        $('.repositoriesTable tbody').append('<tr><td>' + response.Repository[k].Name + '</td><td>' + response.Repository[k].Language + '</td><td>' + response.Repository[k].StargazerCount + '</td></tr>');
                    }

                }
            })
                .done(function (data) {
                    $('#loadingDiv').hide();
                });
        }
        else
            return false;
    }
}
$(document).ready(function () {
    $(function () {
        $("#userNameTextBox").focus();
    });
});