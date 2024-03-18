"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var li = document.createElement("li");
    li.classList.add("messages");
    document.getElementById("messagesList").appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you
    // should be aware of possible script injection concerns.
    if (userName === user) {
        li.textContent = `${message}`;
    }
    else {
        li.textContent = `@${user} ${message}`;
    }
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var message = document.getElementById("messageInput").value;
    let url = window.location.href;
    let urlParts = url.split('/');
    let chatId = urlParts[urlParts.length - 1];

    connection.invoke("SendMessage", chatId, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});