"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
function go() { /* code here */
    var test = document.querySelector("*");
    console.log(test);
    var container = document.querySelector("div.inbox_chatting_box");
    container.scrollTop = container.scrollHeight;

    //Disable the send button until connection is established.
    document.getElementById("sendButton").disabled = true;

    debugger;

    connection.on("ReceiveMessage", function (user, message) {
        var messageBody = document.createElement("li");
        var messageHead = document.createElement("div");
        var messageTimeAndUserNames = document.createElement("div");
        var messageTime = document.createElement("small");
        var messageUserPhoto = document.createElement("img");
        var messageContent = document.createElement("p");
        // We can assign user-supplied strings to an element's textContent because it
        // is not interpreted as markup. If you're assigning in any other way, you
        // should be aware of possible script injection concerns.
        if (userName === user) {
            messageBody.classList.add("reply", "float-end");
            messageHead.classList.add("d-flex", "align-items-center", "justify-content-end", "mb15");
            messageTimeAndUserNames.classList.add("title", "fz15");
            messageTime.classList.add("ml10");
            messageUserPhoto.classList.add("img-fluid", "rounded-circle", "align-self-end", "ml10");

            messageTimeAndUserNames.textContent = `Ти`;
            messageTime.textContent = `${message.createdOn}`;
            messageTimeAndUserNames.appendChild(messageTime);
            messageUserPhoto.src = `${message.userProfilePhoto}`;
            messageUserPhoto.width = `50`;
            messageUserPhoto.height = `50`;
            messageContent.textContent = `${message.content}`;

            messageHead.appendChild(messageTimeAndUserNames);
            messageHead.appendChild(messageUserPhoto);
        }
        else {
            messageBody.classList.add("sent", "float-start");
            messageHead.classList.add("d-flex", "align-items-center", "mb15");
            messageUserPhoto.classList.add("img-fluid", "rounded-circle", "align-self-start", "mr10");
            messageTimeAndUserNames.classList.add("title", "fz15");
            messageTime.classList.add("ml10");

            messageUserPhoto.src = `${message.userProfilePhoto}`;
            messageUserPhoto.width = `50`;
            messageUserPhoto.height = `50`;
            messageTimeAndUserNames.textContent = `${message.userFullName}`;
            messageTime.textContent = `${message.createdOn}`;/*`${user} ${message}`;*/
            messageContent.textContent = `${message.content}`;

            messageHead.appendChild(messageUserPhoto);
            messageHead.appendChild(messageTimeAndUserNames);
            messageTimeAndUserNames.appendChild(messageTime);
        }
        messageBody.appendChild(messageHead);
        messageBody.appendChild(messageContent);
        document.getElementById("my-chatting-content").appendChild(messageBody);

        container.scrollTop = container.scrollHeight;
        document.getElementById("messageInput").value = "";
    });

    connection.start().then(function () {
        document.getElementById("sendButton").disabled = false;
    }).catch(function (err) {
        return console.error(err.toString());
    });

    document.getElementById("sendButton").addEventListener("click", function (event) {
        var message = document.getElementById("messageInput").value;
        let chatId = document.getElementById("chatId").value;

        connection.invoke("SendMessage", chatId, message).catch(function (err) {
            return console.error(err.toString());
        });
        event.preventDefault();
    });
}

setTimeout(go, 3000);