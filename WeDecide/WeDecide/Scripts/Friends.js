$(document).ready(function () {
    var friendsPage = $('.friends-page');

    if (friendsPage.length) {
        $('form').submit(function () {
            if ($(this).valid()) {
                $.ajax({
                    url: this.action,
                    type: this.method,
                    data: $(this).serialize()
                })
            }
            return false;
        });
    }
});