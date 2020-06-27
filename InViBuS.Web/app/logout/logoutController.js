'use strict';

angular.module('InViBuS').controller('logoutController', ['userService', '$state', '$sessionStorage',
    function (userService, $state, $sessionStorage) {

        // Remove authentication and user information when logout is pressed. Go to login state
        userService.removeAuthentication();
        delete $sessionStorage.user;
        $state.go('login');
    }
])