(function () {
    "use strict";
    angular
        .module("common.services")
        .factory("productResource",
        ["$resource", "appSettings", "currentUser", productResource]);

    function productResource($resource, appSettings, currentUser) {
        return $resource(appSettings.serverPath + "/api/products/:id", null,
        {
            "query": {
                headers: { "Authorization": "bearer " + currentUser.getProfile().token },
                isArray: true
            },
            "get": {
                headers: { "Authorization": "bearer " + currentUser.getProfile().token }
            },
            "save": {
                headers: { "Authorization": "bearer " + currentUser.getProfile().token }
            },
            "update": {
                method: "PUT",
                headers: { "Authorization": "bearer " + currentUser.getProfile().token }
            }
        });
    }
}());