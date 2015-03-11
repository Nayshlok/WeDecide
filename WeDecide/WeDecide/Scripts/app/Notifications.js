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
    var notifyPanel = $('#notificationPanel'),
        notifyIcon = $('#notificationBtn'),
        notifyPanelOn = false;

    notifyIcon.on('click', function (e) {
        e.stopPropagation();
        notifyPanelOn = !notifyPanelOn;
        toggleNotifyPanel(notifyPanelOn);
    });

    notifyPanel.on('click', function (e) {
        e.stopPropagation();
    });

    $(document).on('click', function (e) {
        toggleNotifyPanel(false);
        notifyPanelOn = false;
    })

    function toggleNotifyPanel(show) {
        if (show) {
            notifyPanel.css('display', 'block');
        } else {
            notifyPanel.css('display', 'none');
        }
    }

    function createNotification(sender, message) {
        notifyPanel.append("<section class='notification bottom-shadow'><h4>" + sender + "</h4><p>" + message + "</p></section>");
    }

    function checkForNotifications() {
        $.ajax({
            type: "GET",
            url: "/Profile/getNotifications",
            dataType: "json",
            success: function (result) {
                for (i = 0; i < result.length; i++) {
                    createNotification(result[i].SenderName, result[i].Message);
                }
            },
            error: function (response) {
                console.log("Something went wrong with the request");
            }
        });
    }

    //Check for new notifications
    checkForNotifications();
});