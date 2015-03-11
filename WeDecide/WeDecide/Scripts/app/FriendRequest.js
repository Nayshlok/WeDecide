$(function () {
    var notify = $.connection.notificationHub;

    $.connection.hub.logging = true;

    $.connection.hub.start()
        .done(function () {
            console.log("connected, " + $.connection.hub.id);

            $(".add").click(function (event) {
                event.stopPropagation();
                event.stopImmediatePropagation();
                console.log("Id");
                console.log($(this).attr('id'));
                console.log($.connection.hub.id);
                var id = $(this).attr('id');
                notify.server.friendRequest(id)
                    .done(function () {
                        //id = id.replace(".", "\\.");
                        //id = id.replace("@", "\\@");
                        $("#" + id).attr('value', 'Pending');
                        $("#" + id).prop('disabled', true);
                    })
                    .fail(function (error) {
                        console.log(error);
                    });
            });
        })
        .fail(function () { console.log("connection failed"); });


    notify.client.addNotification = function () {
        console.log("Adding");
        $("<p>Notification</p>").insertAfter(".notifications");
    }
})

$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "/Profile/getNotifications",
        dataType: "json",
        success: function (result) {
            window.console.log(result);
        },
        error: function (response) {
            console.log(response);
            window.alert('error');
        }
    })
});



