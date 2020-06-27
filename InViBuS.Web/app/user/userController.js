'use strict';

angular.module('InViBuS').controller('userController', ['$scope', 'userService',
    function($scope, userService) {
        $scope.user = userService.getUserData();
    }
])