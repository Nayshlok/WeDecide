﻿$(function () {
    var notify = $.connection.notificationHub;

    $.connection.hub.logging = false;

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
                console.log(id);
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

    notify.client.addNotification = function (id, sender, senderId, message) {
        createNotification(id, sender, senderId, message);
        totalNotifications += 1;
        updateNotifyIcon();
    }

    notify.client.acceptNotification = function (message, senderName) {
        createAcceptNotification(message, senderName);
        totalNotifications += 1;
        updateNotifyIcon();
    }
    
    //Regular jQuery stuff
    var notifyPanel = $('#notificationPanel'),
        notifyIcon = $('#notificationBtn'),
        notifyCount = $('#notificationCount'),
        notifyPanelOn = false,
        totalNotifications = 0;

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
            acceptBtn = $("<input type='button' class='btn btn-primary' value='Accept' />"),
            declineBtn = $("<form method='post' action='Friends/RejectFriend'><input type='hidden' name='nId' value='" + id + "' /><input type='submit' class='btn btn-danger' value='Decline' /></form>");

        notificationWrap.append(acceptBtn);
        notificationWrap.append(declineBtn);
        $('#notificationPanel').append(notificationWrap);

        acceptBtn.on('click', function () {
            notify.server.addFriend(id, senderId)
                .done(function () {
                    notificationWrap.remove();
                    totalNotifications -= 1;
                    updateNotifyIcon();
                    window.location.reload();
                })
                .fail(function (error) {
                    console.log(error);
                });
        });

        declineBtn.on('submit', function () {
            if ($(this).valid()) {
                $.ajax({
                    url: this.action,
                    type: this.method,
                    data: $(this).serialize(),
                    success: function () {
                        notificationWrap.remove();
                        totalNotifications -= 1;
                        updateNotifyIcon();
                        window.location.reload();
                    }
                });
            }
            return false;
        });
    }

    function createAcceptNotification(message, sender) {
        var notificationWrap = $("<section class='notification bottom-shadow'><h4>" + sender + "</h4><p>" + message + "</p></section>");
        $('#notificationPanel').append(notificationWrap);
    }

    function checkForNotifications() {
        $.ajax({
            type: "GET",
            url: "/Profile/getNotifications",
            dataType: "json",
            success: function (result) {
                var i;
                for (i = 0; i < result.length; i++) {
                    createNotification(result[i].Id, result[i].SenderName, result[i].SenderID, result[i].Message);
                }
                //Update the notification count
                window.console.log("Total notifications: " + totalNotifications);
                totalNotifications += i;
                updateNotifyIcon(totalNotifications);
            },
            error: function (response) {
                console.log("Something went wrong with the request");
            }
        });
    }

    function updateNotifyIcon() {
        if (totalNotifications > 0) {
            notifyCount.html(totalNotifications);
        } else {
            notifyCount.html("");
        }
    }

    //Check for new notifications
    checkForNotifications();
});