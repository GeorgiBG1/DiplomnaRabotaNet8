$(document).ready(function () {
    $("#userEmail").on("input", function () {
        var userEmail = $(this).val();

        $.ajax({
            url: "/Chats/CheckUserEmail",
            type: "POST",
            data: { email: userEmail },
            success: function (response) {
                if (response.exists) {
                    $("#userName").text(response.user.firstName + " " + response.user.lastName);

                    if (response.user.profilePhoto) {
                        $("#uProfilePhoto").attr("src", response.user.profilePhoto).show();
                    } else {
                        $("#uProfilePhoto").hide();
                    }

                    $("#noResults").text("");
                } else {
                    $("#userName").text("");
                    $("#uProfilePhoto").attr("src", "").hide();
                    $("#noResults").text("Няма намерен потребител с този имейл!");
                }
            },
            error: function () {
                console.error("Error checking user email");
            }
        });
    });

    $("#addParticipantBtn").click(function () {
        var userEmail = $("#userEmail").val();
        $.ajax({
            url: "/Chats/AddParticipant",
            type: "POST",
            data: { email: userEmail, chatId: '@Model.Id' },
            success: function (response) {
                if (response.exists) {
                    $("noResults").text("");
                    $("#exampleModal2").modal("hide");
                    // Show a Bootstrap toast with success message
                    $(".toast-body").text("Успешно добавихте този потребител във вашия разговор!");
                    $(".toast").toast("show");
                    let currentCount = parseInt($("#participantsCounter").text());
                    let newCount = currentCount + 1;
                    // Update the count in the HTML
                    $("#participantsCounter").text(newCount);
                } else {
                    $("#noResults").text(response.message);
                }
            },
            error: function () {
                console.error("Error adding participant to chat");
            }
        });
    });
});