'use strict';

var InViBuS = angular.module('InViBuS', ['ui.router', 'ngStorage', 'anguFixedHeaderTable', 'pascalprecht.translate']);

InViBuS.config(['$stateProvider', '$urlRouterProvider', '$translateProvider',
    function ($stateProvider, $urlRouterProvider, $translateProvider) {

        // If a state is not found, go to this state
        $urlRouterProvider.otherwise('/login');

        // Provide the different states and corresponding views, controllers etc.
        $stateProvider
            .state('login', {
                url: '/login',
                templateUrl: 'app/login/login-view.html',
                controller: 'loginController',
                data: {
                    pageTitle: 'Login'
                }
            })
            .state('logout', {
                controller: 'logoutController'
            })
            .state('home', {
                url: '/home',
                templateUrl: 'app/home/home-view.html',
                data: {
                    pageTitle: 'Home view' 
                }
            })
            .state('projects', {
                url: '/projects',
                templateUrl: 'app/projectList/project-list-view.html',
                controller: 'projectListController',
                data: {
                    pageTitle: 'Project List view'
                }
            })
            .state('project', {
                url: '/project/:id',
                templateUrl: 'app/project/project-view.html',
                controller: 'projectController',
                data: {
                    pageTitle: 'Project view'
                }
            })
            .state('simulation', {
                url: '/simulation/:id',
                templateUrl: "app/simulation/simulation-view.html",
                controller: 'simulationController',
                data: {
                    pageTitle: 'Simulation view'
                }
            });

        // Add translations to defined language
        $translateProvider.translations('en', {
            "Parameter": "Parameter",
            "Unit": "Unit",
            "_dist1": "Distribution",
            "_dist2": "Min.",
            "_dist3": "Max.",
            "_dist4": "(Steps)"
        });
        // Prevent Cross-Site Scripting attacks by correctly escaping variable content
        $translateProvider.useSanitizeValueStrategy('escaped');
        // Set preferred language
        $translateProvider.preferredLanguage('en');
    }]);

InViBuS.run(['$rootScope', '$state', '$stateParams', 'userService',
    function ($rootScope, $state, $stateParams, userService) {
        $rootScope.$state = $state;
        $rootScope.$stateParams = $stateParams;

        try {
            userService.isAuthenticated();
        } catch (e) {
            // do nothing with this error
        }
    }]);