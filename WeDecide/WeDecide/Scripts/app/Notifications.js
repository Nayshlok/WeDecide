$(document).ready(function () {
    var notifyPanel = $('#notificationPanel'),
        notifyIcon = $('#notificationBtn'),
        notifyPanelOn = false;

    notifyIcon.on('click', function (e) {
        e.stopPropagation();
        notifyPanelOn = !notifyPanelOn;
        toggleNotifyPanel(notifyPanelOn);
    });

    notifyPanel.on('click', function (e) {
        e.stopPropagation();
    });

    $(document).on('click', function (e) {
        toggleNotifyPanel(false);
        notifyPanelOn = false;
    })

    function toggleNotifyPanel(show) {
        if (show) {
            notifyPanel.css('display', 'block');
        } else {
            notifyPanel.css('display', 'none');
        }
    }
});