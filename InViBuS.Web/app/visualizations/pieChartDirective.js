angular.module("InViBuS")
    .directive("pieChart", ["$window",
    function ($window) {

        function link(scope, element, attrs) {

            var margin = { top: 0, right: 20, buttom: 0, left: 20 };

            scope.label = "";

            var graph = function (data) {
                return nv.addGraph(function () {
                    var chart = nv.models.pieChart()
                        // Set legend labels
                        .x(function (d) { // Always sets key (The legend of the chart)
                            for (i in d) {
                                return i;
                            }
                        })
                        // Set values
                        .y(function (d) { // Always sets value (The values of the chart)

                            for (i in d) {
                                return d[i];
                            }
                        })
                        .margin(margin)
                        .showLabels(true)
                        .labelType("percent");

                    var all = d3.select("#pieGraph")
                        .attr("id", "pieGraph" + scope.index)
                        .style({
                            "display": "inline-block"
                        });

                    // Add a div to element and add an id
                    all.append("div")
                    .attr("id", "pieChart" + scope.index)
                    // Set CSS of selected element
                    .style({
                        "margin-left": "30px",
                        "float": "left",
                        "position": "relative",
                        "display": "inline-block"
                    })
                    // Add am SVG element inside
                    .append("svg");

                    d3.select("#pieChart" + scope.index + " svg")
                        // Set CSS of selected element
                        .style({
                            "height": "350px"
                        })
                        // Set data for chart
                        .datum(data)
                        // Call the chart function when everything is ready
                        .call(chart);

                    d3.selectAll(".nv-pieWrap")
                    .attr("transform", "translate(0,-25)");

                    nv.utils.windowResize(          //Updates the window resize event callback.
                        function () {
                            chart.update();         //Renders the chart when window is resized.
                        }
                    );

                    return chart;
                });
            }

            // Watch the data and update this when it changes
            scope.$watch('data', function (newVal, oldVal) {
                if (typeof newVal !== "undefined" && newVal !== null) {
                    scope.label = newVal.Output;
                    graph(newVal.Values);
                }
            });
        }

        return {
            link: link,
            restrict: "E",
            scope: {
                data: "=",
                index: "="
            },
            controller: "simulationController",
            templateUrl: 'app/visualizations/pie-chart-view.html'
        }
    }
    ]);
