"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable the send button until connection is established.
document.getElementById("showMessagesBtn").disabled = true;

connection.on("ReceiveCurrentMessages", function (messages) {
    messages.forEach(element => {
        var divMesContainer = document.createElement("div");
        var divMesContent = document.createElement("div");
        var pMesUser = document.createElement("p");
        var pMesDes = document.createElement("p");
        var pMesCreatedOn = document.createElement("p");

        divMesContainer.classList.add("notif_list", "d-flex", "align-items-start", "bdrb1", "pb25", "mb10");
        divMesContent.classList.add("details", "ml15");
        pMesUser.classList.add("dark-color", "fw500", "mb-2");
        pMesDes.classList.add("text", "mb-2");
        pMesCreatedOn.classList.add("mb-0", "text-thm");

        pMesUser.textContent = `${element.owner.firstName} ${element.owner.lastName}`;
        pMesDes.textContent = `${element.content}`;
        pMesCreatedOn.textContent = `${element.createdOn}`;

        divMesContent.appendChild(pMesUser);
        divMesContent.appendChild(pMesDes);
        divMesContent.appendChild(pMesCreatedOn);

        divMesContainer.appendChild(divMesContent);
        document.getElementById("messagesContainer").appendChild(divMesContainer);
    });
});

connection.start().then(function () {
    document.getElementById("showMessagesBtn").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("showMessagesBtn").addEventListener("click", function (event) {

    connection.invoke("ShowCurrentMessages").catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});