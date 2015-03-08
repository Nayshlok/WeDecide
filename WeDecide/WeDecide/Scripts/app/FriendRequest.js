var notify = $.connection.notificationHub;

$.connection.hub.logging = true;
$.connection.hub.start()
    .done(function () {
        console.log("connected, " + $.connection.hub.id);
        $(".btn").click(function (event) {
            console.log("Id");
            console.log($(this).attr('id'));
            console.log($.connection.hub.id);
            var id = $(this).attr('id');
            notify.server.friendRequest(id)
                .done(function () {
                    id = id.replace(".", "\\.");
                    id = id.replace("@", "\\@");
                    $("#" + id).attr('value', 'Pending');
                    $("#" + id).prop('disabled', true);
                    //event.target.remove();
                })
                .fail(function (error) {
                    console.log(error);
                });
        });
    })
    .fail(function () { console.log("connection failed"); });
