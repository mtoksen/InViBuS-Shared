'use strict';

angular.module('InViBuS').controller('projectController', ['$scope', '$sessionStorage', '$stateParams', 'projectService',
    '$state', 'fileReaderFactory', 'analysisMetaDataService',
    function ($scope, $sessionStorage, $stateParams, projectService, $state, fileReaderFactory, analysisMetaDataService) {
        $scope.projectData = null;
        $scope.analysismetadata = null;

        $scope.$watch('$sessionStorage.analysismetadata', function () {
            console.log($sessionStorage.analysismetadata);
            $scope.analysismetadata = $sessionStorage.analysismetadata;
        });

        // Set analysismetadata from session storage
        projectService.getAnalysisMetadata($stateParams.id)
            .then(function () {
                $scope.analysismetadata = $sessionStorage.analysismetadata;
            });

        // Used for show/hide tables
        $scope.showHeaderTable = true;
        $scope.showDataTable = true;

        // Go to state when a simulation is selected
        $scope.selectSimulation = function (id) {
            $state.go('simulation', { id: id });
        }

        // Watch for changes in project id
        $scope.$watch('$stateParams.id', function () {
            projectService.getProject($stateParams.id)
                .then(function () {
                    $scope.project = $sessionStorage.project;
                });
        });

        // Geta file when selected from the computer
        $scope.getFile = function () {
            $scope.progress = 0;
            fileReaderFactory.readAsText($scope.file, $scope)
                .then(function (result) {
                    return analysisMetaDataService.setAnalysisMetaData(result, $scope.project.idProject);
                })
                .then(function (result) {
                    if (result.data) {
                        $state.reload();
                    }
                });
        };
    }
]);