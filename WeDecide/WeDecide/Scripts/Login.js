$(document).ready(function () {
    var loginContainer = $('.login-container');

    if (loginContainer.length) {
        var login = {}
        login.email = $('#emailField');
        login.password = $('#passwordField');
        login.loginBtn = $('#loginBtn');

        $(login.email).keypress(function () {
            btnValidation();
        });

        $(login.password).keypress(function () {
            btnValidation();
        });

        function btnValidation() {
            if ($('form').valid()) {
                login.loginBtn.removeClass('btn-danger');
                login.loginBtn.addClass('btn-success');
            } else {
                login.loginBtn.removeClass('btn-success');
                login.loginBtn.addClass('btn-danger');
            }
        }
    }
});