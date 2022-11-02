const hubAddress = "https://localhost:7500/dogustechnologyhub";

var connection = new signalR.HubConnectionBuilder()
    .configureLogging(signalR.LogLevel.Debug)
    .withAutomaticReconnect(1000, 1000, 3000, 3000)
    .withUrl(hubAddress).build();

$(document).ready(() => {
    setInterval(startConnection, 1000);
});

function blinkMessage() {
    $("#message").fadeOut(500);
    $("#message").fadeIn(500);
}

function startConnection() {
    if ($("#message").hasClass('text-info')) {
        connection.start().then(() => {
            $("#message").removeClass('text-info').addClass('text-success');
            $("#message").html("Hub'a bağlantı sağlandı.<br/> Redis <u style='color:red'>MONITOR</u> komutuyla kontrol ediniz.");
            setInterval(blinkMessage, 1000);
        })
        .catch((error) => {
            console.log(error);
        });
    } 
}