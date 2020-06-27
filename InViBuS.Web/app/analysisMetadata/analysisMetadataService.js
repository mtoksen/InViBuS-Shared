'use strict';

angular.module('InViBuS').service('analysisMetaDataService', ['$sessionStorage', '$http',
    function ($sessionStorage, $http) {
        function setHttpAuthHeader(userData) {
            $http.defaults.headers.common.Authorization = 'Bearer ' + userData.bearerToken;
        }

        this.setAnalysisMetaData = function (param, id) {
            setHttpAuthHeader($sessionStorage.user);

            return $http({
                method: 'POST',
                cache: false,
                url: 'http://localhost:5342/api/AnalysisMetaData/postanalysismetadata/' + id,
                data: param,
                headers: {
                    'Content-Type': 'application/json'
                }
            });
        }
    }
])