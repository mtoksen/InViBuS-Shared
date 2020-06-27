'use strict';

angular.module('InViBuS').controller('simulationController', [
    '$scope', '$sessionStorage', '$stateParams', 'simulationService',
    function ($scope, $sessionStorage, $stateParams, simulationService) {
        var param = $stateParams.id;

        // Configure d3 request and set data when received. 
        d3.json(simulationService.getSimulationUrl(param).url)
        .header(simulationService.getSimulationUrl(param).headerName, simulationService.getSimulationUrl(param).headerVal)
        .get(function(err, data) {
            if (err) { throw err; }
            $scope.analysisData = JSON.parse(data.analysisDataJson);
            $scope.$apply();
        });
    }
]);