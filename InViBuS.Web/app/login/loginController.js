'use strict';

angular.module('InViBuS').controller('loginController', [
    '$scope', '$state', 'userService',
    function ($scope, $state, userService) {
        $scope.errors = [];

        // When successful login, go to home
        function onSuccessfulLogin() {
            $state.go('home');
        }

        // Should handle login errors. NOT WORKING RIGHT NOW!
        function onFailedLogin(error) {

            if (typeof error === 'string' && $scope.errors.indexOf(error) === -1) {
                $scope.errors.push(error);
            };
        }

        // Function called when submit button pressed
        $scope.submit = function () {
            userService.authenticate($scope.username, $scope.password, onSuccessfulLogin(), onFailedLogin());
        }
    }
]);