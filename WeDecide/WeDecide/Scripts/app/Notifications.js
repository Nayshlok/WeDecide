$(document).ready(function () {
    (function () {
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
    });

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

    function createNotification(id, sender, senderId, message) {
        var notificationWrap = $("<section class='notification bottom-shadow'><h4>" + sender + "</h4><p>" + message + "</p></section>"),
            acceptBtn = $("<form method='post' action='/Friends/AddFriend'><input type='hidden' name='nId' value='" + id + "'/><input type='hidden' name='userId' value='" + senderId + "'/><input type='submit' class='btn btn-primary' value='Accept' /></form>"),
            declineBtn = $("<form method='post' action='/Friends/RejectFriend'><input type='hidden' name='nId' value='"+ id + "' /><input type='submit' class='btn btn-danger' value='Decline' /></form>");
            
        notificationWrap.append(acceptBtn);
        notificationWrap.append(declineBtn);
        notifyPanel.append(notificationWrap);
    }

    function checkForNotifications() {
        $.ajax({
            type: "GET",
            url: "/Profile/getNotifications",
            dataType: "json",
            success: function (result) {
                for (i = 0; i < result.length; i++) {
                    createNotification(result[i].Id, result[i].SenderName, result[i].SenderID, result[i].Message);
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