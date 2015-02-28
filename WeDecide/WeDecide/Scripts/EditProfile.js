$(document).ready(function () {
    var overlayBox = $('.overlay-box');

    if (overlayBox.length) {
        var profileEdit = {}

        profileEdit.editBox = $('#editBox');
        profileEdit.fade = $('#fade');
        profileEdit.editBtn = $('#editBtn');

        profileEdit.editBtn.on('click', function () {
            profileEdit.editBox.css('display', 'block');
            profileEdit.fade.css('display', 'block');
        });

        profileEdit.fade.on('click', function () {
            profileEdit.editBox.css('display', 'none');
            profileEdit.fade.css('display', 'none');
        });

        profileEdit.onEditFailure = function () {
            profileEdit.editBox.css('display', 'block');
            profileEdit.fade.css('display', 'block');
        };

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