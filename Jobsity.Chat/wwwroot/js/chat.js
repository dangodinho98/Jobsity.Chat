
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
var max = 50;

//Disable send button until connection is established
$("#sendMessage").prop('disabled', true);

connection.on("ReceiveMessage", function (user, message, date) {
    var msg = message.replace(/&/g, "&").replace(/</g, "<").replace(/>/g, ">");
    var encodedMsg = date + " - " + user + " says: " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    $("#messagesList").prepend(li);
    $("ul").each(function () {
        $(this).find('li').each(function (index) {
            if (index >= max) $(this).hide();
        });
    });
});

connection.start().then(function () {
    $("#sendMessage").prop('disabled', false);
}).catch(function (err) {
    return console.error(err.toString());
});

$("#sendMessage").click(function () {

    var sender = $("#sender").val();
    var message = $("#message").val().trim();

    if (message === "") {
        alert("Empty messages are not allowed.");
        return;
    }

    connection.invoke("SendMessage", sender, message).catch(function (err) {
        return console.error(err.toString());
    });

    $("#message").val("");
    event.preventDefault();
});