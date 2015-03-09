$(document).ready(function () {
    var friendsPage = $('.friends-page');

    if (friendsPage.length) {

        function displayPeople(potentialFriends) {
            window.console.log(potentialFriends);
            for (var u in potentialFriends) {
                window.console.log(potentialFriends[u].Name);
            }
        }

        $('form').submit(function () {
            if ($(this).valid()) {
                $.ajax({
                    url: this.action,
                    type: this.method,
                    data: $(this).serialize(),
                    dataType: 'json',
                    success: function (data) {
                        displayPeople(data.PotentialFriends);
                    },
                    error: function (jqXHR, status, error) {
                        window.console.log("Something went wrong with the request!\nStatus: {0} Error: {1}".format(status,error));
                    }
                })
            }
            return false;
        });
    }
});