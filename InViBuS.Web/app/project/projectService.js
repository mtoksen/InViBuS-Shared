'use strict';

angular.module('InViBuS').service('projectService', ['$http', '$sessionStorage',
    function ($http, $sessionStorage) {
        function setHttpAuthHeader(userData) {
            $http.defaults.headers.common.Authorization = 'Bearer ' + userData.bearerToken;
        }

        this.getProject = function (param) {
            setHttpAuthHeader($sessionStorage.user);

            return $http.get('http://localhost:5342/api/Project/getproject/' + param)
                .success(function (data, status, headers, config) {
                    $sessionStorage.project = data;
                });
        }

        this.getAnalysisMetadata = function (param) {
            setHttpAuthHeader($sessionStorage.user);

            return $http.get('http://localhost:5342/api/Analysismetadata/getanalysismetadataforproject/' + param)
                .success(function (data, status, headers, config) {
                    $sessionStorage.analysismetadata = data;
                });
        }
    }])