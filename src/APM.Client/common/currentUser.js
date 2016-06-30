(function () {
    'use strict';

    angular
        .module('common.services')
        .factory('currentUser', currentUser);

    //currentUser.$inject = ['anythingNeeded'];

    function currentUser() {
        var profile = {
            isLoggedIn: false,
            userName: "",
            token: ""
        }

        function setProfile(username, token) {
            profile.userName = username;
            profile.token = token;
            profile.isLoggedIn = true;
        }

        function getProfile() {
            return profile;
        }

        var service = {
            setProfile: setProfile,
            getProfile: getProfile
        };

        return service;
    }
})();