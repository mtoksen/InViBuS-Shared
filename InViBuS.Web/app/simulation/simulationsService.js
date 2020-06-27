'use strict';

angular.module('InViBuS').service('simulationService', ['$http', '$sessionStorage',
    function ($http, $sessionStorage) {

        // Constructs and returns the simulation url used by d3 requests
        this.getSimulationUrl = function (param) {
            return {
                url: 'http://localhost:5342/api/analysisdata/getanalysisdata/' + param,
                headerName: "Authorization",
                headerVal: 'Bearer ' + $sessionStorage.user.bearerToken
            }
        }
    }])