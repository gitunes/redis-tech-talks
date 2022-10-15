const hubAddress = "https://localhost:7502/dogustechnologyhub";

var connection = new signalR.HubConnectionBuilder()
.configureLogging(signalR.LogLevel.Debug)
.withAutomaticReconnect(1000, 1000, 3000, 3000)
.withUrl(hubAddress).build();


$(document).ready(() => {

    connection.start().then(() => { 
       $("#message").removeClass('text-info').addClass('text-sucess').text("Hub'a bağlantı sağlandı. Redisden kontrol ediniz.");
       setInterval(blinkMessage, 1000);
    })
    .catch((error) => {
        console.log(error);
    });

});

function blinkMessage() {
    $("#message").fadeOut(500);
    $("#message").fadeIn(500);
}