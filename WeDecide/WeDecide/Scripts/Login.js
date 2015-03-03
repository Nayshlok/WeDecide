$(document).ready(function () {
    var loginContainer = $('.login-container');

    if (loginContainer.length) {
        var login = {}
        login.email = $('#emailField');
        login.password = $('#passwordField');
        login.passwordConfirm = $('#passwordConfirm');
        login.loginBtn = $('#loginBtn');

        $(login.email).keyup(function () {
            btnValidation();
        });

        $(login.password).keyup(function () {
            btnValidation();
        });

        $(login.passwordConfirm).keyup(function () {
            btnValidation();
        });

        function btnValidation() {
            if ($('form').valid()) {
                login.loginBtn.removeClass('btn-danger');
                login.loginBtn.addClass('btn-success');
                login.loginBtn.removeAttr('disabled');
            } else {
                login.loginBtn.removeClass('btn-success');
                login.loginBtn.addClass('btn-danger');
                login.loginBtn.attr('disabled','disabled');
            }
        }
    }
});