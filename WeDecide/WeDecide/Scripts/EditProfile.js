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
            window.console.log("You clicked the fade");
            profileEdit.editBox.css('display', 'none');
            profileEdit.fade.css('display', 'none');
        });
    }
});