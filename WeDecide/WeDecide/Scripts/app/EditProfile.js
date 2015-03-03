$(document).ready(function () {
    var overlayBox = $('.overlay-box');

    if (overlayBox.length) {
        var profileEdit = {}

        profileEdit.editBox = $('#editBox');
        profileEdit.fade = $('#fade');
        profileEdit.editBtn = $('#editBtn');

        profileEdit.editBtn.on('click', function () {
            profileEdit.togglePopup(true);
        });

        profileEdit.fade.on('click', function () {
            profileEdit.togglePopup(false);
        });

        profileEdit.togglePopup = function(show) {
            if(show) {
                profileEdit.editBox.css('display', 'block');
                profileEdit.fade.css('display', 'block');
            } else {
                profileEdit.editBox.css('display', 'none');
                profileEdit.fade.css('display', 'none');
            }
        }
    }
});