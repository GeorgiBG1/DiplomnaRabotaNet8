function openFileExplorer() {
    document.getElementById('fileInput').click();
}

$(document).ready(function () {
    $('#fileInput').change(function () {
        var fileInput = document.getElementById('fileInput');
        var file = fileInput.files[0];

        var formData = new FormData();
        formData.append("file", file);

        $.ajax({
            url: '/Dashboard/UploadProfilePhoto',
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (response) {
                if (response.exists) {

                    if (response.userProfilePhoto) {
                        $("#user-profile-photo").attr("src", response.userProfilePhoto).show();
                        $("#setDefaultPhotoBtn").show();
                    }

                    $("#failUpload").text("");
                }
                else {
                    $("#failUpload").text(response.message);
                }
            },
            error: function (xhr, status, error) {
                console.error('Upload failed. Status: ' + status);
            }
        });
    })

    $('#setDefaultPhotoBtn').click(function () {
        $.ajax({
            url: '/Dashboard/SetProfilePhotoToDefault',
            type: 'POST',
            processData: false,
            contentType: false,
            success: function (response) {
                if (response.exists) {

                    if (response.userProfilePhoto) {
                        $("#user-profile-photo").attr("src", response.userProfilePhoto).show();
                        $("#setDefaultPhotoBtn").hide();
                    }

                    $("#failUpload").text("");
                }
                else {
                    $("#failUpload").text(response.message);
                }
            },
            error: function (xhr, status, error) {
                console.error('Upload failed. Status: ' + status);
            }
        });
    })
})