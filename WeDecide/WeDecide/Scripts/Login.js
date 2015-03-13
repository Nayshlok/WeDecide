$(document).ready(function () {
    var loginContainer = $('.login-container');

    if (loginContainer.length) {
        var login = {}
        login.email = $('#emailField');
        login.password = $('#passwordField');
        login.passwordConfirm = $('#passwordConfirm');
        login.loginBtn = $('#loginBtn');

        login.email.on('blur', function () {
            btnValidation();
        });

        login.password.on('blur', function () {
            btnValidation();
        });

        login.passwordConfirm.on('blur', function () {
            btnValidation();
        });

        //login.loginBtn.on('mouseover', function () {
        //    btnValidation();
        //});

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