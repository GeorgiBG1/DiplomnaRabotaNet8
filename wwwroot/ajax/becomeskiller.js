$('#become-skiller-btn').click(function () {
    $.ajax({
        url: '/Dashboard/AddUserToRoleSkiller',
        type: 'POST',
        processData: false,
        contentType: false,
        success: function (response) {
            if (response.exists) {

                $("#message-become-skiller").text(response.message);
                $("#become-skiller-btn").hide();
            }
            else {
                $("#message-become-skiller").text(response.message);
            }
        },
        error: function (xhr, status, error) {
            console.error('Upload failed. Status: ' + status);
        }
    });
})