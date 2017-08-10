jQuery.validator.setDefaults({
    debug: true,
    success: "valid"
});
$().ready(function () {
    $(".searchForm").validate({
        rules: {
            userNameTextBox: {
                required: true,
                maxlength: 20
            }
        }
    });
});