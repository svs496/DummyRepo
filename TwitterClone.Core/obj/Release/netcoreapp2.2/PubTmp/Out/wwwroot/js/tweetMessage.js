"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/tweetsHub").build();

//Disable send button until connection is established
document.getElementById("btnPostedTweet").disabled = true;

connection.on("ReceiveMessage", function (user, message) {

    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + msg;
    var li = document.createElement("li");
    li.setAttribute('class', 'list-group-item');
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function () {
    document.getElementById("btnPostedTweet").disabled = false;
}).catch(function (err) {
    console.log(err);
    return console.error(err.toString());
});

document.getElementById("btnPostedTweet").addEventListener("click", function (event) {
   
    var user = document.getElementById("userFullName").innerText;
    
    var message = " posted a new tweet.";
    connection.invoke("SendMessage", user, message).catch(function (err) {
        console.log(err);
        return console.error(err.toString());
    });
   // event.preventDefault();
});