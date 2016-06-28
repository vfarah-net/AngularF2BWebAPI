(function () {
    "use strict";

    angular
        .module("common.services")
        .factory("userAccount", userAccount);

    userAccount.$inject = ["$resource", "appSettings"];

    function userAccount($resource, appSettings) {
        return {
            registration: $resource(appSettings.serverPath + "/api/Account/Register", null, {
                "registerUser": { method: "POST" }
            }),
            login: $resource(appSettings.serverPath + "/Token", null, {
                "loginUser": {
                    method: "POST",
                    headers: { "Content-Type": "application/x-www-form-urlencoded" },
                    transformRequest: function (data) {
                        var str = [];
                        for (var d in data) {
                            if (data.hasOwnProperty(d)) {
                                str.push(encodeURIComponent(d) + "=" + encodeURIComponent(data[d]));                                
                            }
                        }
                        return str.join("&");
                    }
                }
            })
        }
    }
})();