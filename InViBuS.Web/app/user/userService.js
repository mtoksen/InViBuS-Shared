'use strict';

angular.module('InViBuS').service('userService', ['$http', '$sessionStorage',
    function ($http, $sessionStorage) {
        var userData = {
            isAuthenticated: false,
            username: '',
            bearerToken: '',
            expirationDate: null
        };

        function setHttpAuthHeader() {
            $http.defaults.headers.common.Authorization = 'Bearer ' + userData.bearerToken;
        }

        this.getUserData = function () {
            return userData;
        }

        // Clear the user data
        function clearUserData() {
            userData.isAuthenticated = false;
            userData.username = '';
            userData.bearerToken = '';
            userData.expirationDate = null;
        }

        // Contact database to authenticate user
        this.authenticate = function (username, password, successCallback, errorCallback) {
            var config = {
                method: 'POST',
                url: 'http://localhost:5342/token',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded'
                },
                data: 'grant_type=password&username=' + username + '&password=' + password
            };

            $http(config)
                .success(function (data) {
                    userData.isAuthenticated = true;
                    userData.username = username;
                    userData.bearerToken = data.access_token;
                    userData.expirationDate = data.expires_in;
                    setHttpAuthHeader();
                    if (typeof successCallback === 'function') {
                        successCallback();
                    }
                    $sessionStorage.user = userData;
                })
                .error(function (data) {
                    if (typeof errorCallback === 'function') {
                        if (data.error_description) {
                            errorCallback(data.error_description);
                        } else {
                            errorCallback('Unable to contact server; please try again later.');
                        };
                    };
                });
        }

        // Remove user authentication
        this.removeAuthentication = function () {
            clearUserData();
            delete $sessionStorage.user;
            $http.defaults.headers.common.Authorization = null;
        }
    }]);