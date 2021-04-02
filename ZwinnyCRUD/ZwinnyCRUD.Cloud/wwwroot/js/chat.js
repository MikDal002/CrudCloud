"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/MessageHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var container = document.createElement("div");
    container.classList.add("chat_box");
    container.classList.add("row");
    var messagesList = document.getElementById("messagesList");
    messagesList.appendChild(container);

    var created_container = messagesList.lastChild;

    var d_user = document.createElement("div");
    d_user.textContent = user;
    d_user.classList.add("user");
    d_user.classList.add("col-md-4");
    d_user.classList.add("col-sm-5");
    created_container.appendChild(d_user);

    var d_message = document.createElement("div");
    d_message.textContent = msg;
    d_message.classList.add("message");
    d_message.classList.add("col-md-8");
    d_message.classList.add("col-sm-7");
    created_container.appendChild(d_message);

    var msgList = document.getElementById("messagesList");
    msgList.scrollTop = msgList.scrollHeight;

    document.getElementById("userInput").disabled = true;
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;

    if (user !== '' && message !== '') {
        connection.invoke("SendMessage", user, message).catch(function (err) {
            return console.error(err.toString());
        });

        document.getElementById("messageInput").value = '';
    }

    event.preventDefault();
});