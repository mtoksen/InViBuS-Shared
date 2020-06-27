'use strict';

angular.module('InViBuS').controller('navigationController', ['$scope', 'userService', '$sessionStorage', 
    function ($scope, userService, $sessionStorage) {
        // If user is saved in session stoage, set user to that, else get user data
        $scope.user = $sessionStorage.user;
        if (typeof $scope.user === 'undefined') {
            $scope.user = userService.getUserData();
        }

        // Need for debugging
        //$scope.$watch('user', function() {
        //    console.log($scope.user);
        //});
    }
]);