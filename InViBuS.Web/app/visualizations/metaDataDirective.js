angular.module("InViBuS")
    .directive("metaData", [
    function () {

        function link(scope, element, attrs) {
            // Nothing needed here right now
        }

        return {
            link: link,
            restrict: "E",
            scope: {
                data: "="
            },
            controller: "simulationController",
            templateUrl: 'app/visualizations/meta-data-view.html'
        }
    }
    ]);
