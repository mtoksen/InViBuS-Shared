'use strict';

angular.module('InViBuS').service('projectListService', ['$http', '$sessionStorage',
    function ($http, $sessionStorage) {
        function setHttpAuthHeader(userData) {
            $http.defaults.headers.common.Authorization = 'Bearer ' + userData.bearerToken;
        }

        this.getProjectList = function () {
            setHttpAuthHeader($sessionStorage.user);

            return $http.get('http://localhost:5342/api/Project/getprojects')
                .success(function(data, status, headers, config) {
                    $sessionStorage.projectList = data;
                });
        }
    }])