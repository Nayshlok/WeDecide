$(document).ready(function () {
    var loginContainer = $('.login-container');

    if (loginContainer.length) {
        var login = {}
        login.email = $('#emailField');
        login.password = $('#passwordField');
        login.passwordConfirm = $('#passwordConfirm');
        login.loginBtn = $('#loginBtn');

        $(login.email).keypress(function () {
            btnValidation();
        });

        $(login.password).keypress(function () {
            btnValidation();
        });

        $(login.passwordConfirm).keypress(function () {
            btnValidation();
        });

        function btnValidation() {
            if ($('form').valid()) {
                window.console.log("form is valid");
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