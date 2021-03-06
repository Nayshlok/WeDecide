﻿// C#-style string formatting, but called on the string itself (so more like string.replace)
// think public static String format(this String format, params string[] matches){...}
if (!String.prototype.format) {
    String.prototype.format = function () {
        var args = arguments;
        return this.replace(/{(\d+)}/g, function (match, number) {
            return typeof args[number] != 'undefined'
              ? args[number]
              : match
            ;
        });
    };
}

function printFailures(failureObj) {
    for (var key in failureObj) {
        var val = failureObj[key];
        console.log("\tK: {0}, V: {1}".format(key, val))
        if (val instanceof Object)
            printFailures(val);
    }
}