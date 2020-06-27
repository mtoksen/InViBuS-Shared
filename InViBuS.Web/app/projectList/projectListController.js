'use strict';

angular.module('InViBuS').controller('projectListController', ['$scope', 'projectListService', '$sessionStorage', '$state',
    function ($scope, projectListService, $sessionStorage, $state) {

        // If list of projets is not present, get the list and put it into session storage, else just set from session storage
        if (typeof $sessionStorage.projectList === 'undefined' || $sessionStorage.projectList === null) {
            projectListService.getProjectList()
                .then(function() {
                    $scope.projectList = $sessionStorage.projectList;
                });
        } else {
            $scope.projectList = $sessionStorage.projectList;
        }

        // GO to selected project
        $scope.selectProject = function (projectId) {
            $state.go('project', { id: projectId });
        }
    }
])