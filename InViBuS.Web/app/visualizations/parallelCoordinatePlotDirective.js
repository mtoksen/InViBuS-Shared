angular.module("InViBuS")
    .directive("parallelCoordinates", ["$window",
    function ($window) {

        function link(scope, element, attrs) {

            /********** Graph **********/

            // Initialization. Only done once per parallel-coordinates tag in the template.
            var margin = { top: 0, right: 20, buttom: 20, left: 20 },
                width = ($window.innerWidth * 1) - margin.left - margin.right;

            // Whenever the data changes, update the element data
            scope.$watch("analysisData", function (newVal, oldVal) {
                if (typeof newVal !== "undefined") {

                    // Extract the dimensions and crate a scale for each dimension.
                    var aData = []; // Container that unified data will be contained in
                    var tmpArray = [];

                    // Merge all relevant data
                    for (var i = 0; i < newVal.AnalysisIn.length; i++) {
                        aData.push(newVal.AnalysisIn[i]);
                        tmpArray.push(newVal.AnalysisOut[i]);
                    };
                    for (var j = 0; j < aData.length; j++) {
                        for (var k = 0; k < tmpArray.length; k++) {
                            angular.extend(aData[j], tmpArray[j]);
                        }
                    };

                    var parcoords = d3.parcoords()("div.parallel-coordinate-plot-graph")
                        .alpha(0.4)
                        .width(width)
                        .height(225)
                        .mode("queue")
                        .data(aData)
                        .render()
                        .reorderable()
                        .brushMode("1D-axes");

                    // Give each element an id
                    aData.forEach(function (d, i) {
                        d.id = d.id || i;
                    });

                    // Grid helper-function
                    function gridUpdate(data) {
                        dataView.beginUpdate();
                        dataView.setItems(data);
                        dataView.endUpdate();
                    };

                    // setting up grid
                    var column_keys = d3.keys(aData[0]);
                    var columns = column_keys.map(function (key, i) {
                        return {
                            id: key,
                            name: key,
                            field: key,
                            sortable: true
                        }
                    });

                    // Define the options for Slickgrid
                    var options = {
                        enableCellNavigation: true,
                        enableColumnReorder: false, //requires jquery-ui.sortable; not available now
                        multiColumnSort: false,
                        forceFitColumns: true
                    };

                    var dataView = new Slick.Data.DataView();
                    var grid = new Slick.Grid(".grid", dataView, columns, options);
                    var pager = new Slick.Controls.Pager(dataView, grid, angular.element(".pager"));

                    // Wire up model events to drive the grid
                    dataView.onRowCountChanged.subscribe(function (e, args) {
                        grid.updateRowCount();
                        grid.render();
                    });

                    dataView.onRowsChanged.subscribe(function (e, args) {
                        grid.invalidateRows(args.rows);
                        grid.render();
                    });

                    // Column sorting
                    var sortcolumn = column_keys[0];
                    var sortdir = 1;

                    function comparer(a, b) {
                        var x = a[sortcolumn], y = b[sortcolumn];
                        return (x == y ? 0 : (x > y ? 1 : -1));
                    }

                    // Click header to sort grid column
                    grid.onSort.subscribe(function (e, args) {
                        sortdir = args.sortAsc ? 1 : -1;
                        sortcolumn = args.sortCol.field;
                        dataView.sort(comparer, args.sortAsc);
                    });

                    // Highlight row in chart
                    grid.onMouseEnter.subscribe(function (e, args) {
                        var i = grid.getCellFromEvent(e).row;
                        var d = parcoords.brushed() || aData;
                        parcoords.highlight([d[i]]);
                    });
                    grid.onMouseLeave.subscribe(function (e, args) {
                        parcoords.unhighlight();
                    });

                    // Fill grid with data
                    gridUpdate(aData);

                    // Update grid on brush
                    parcoords.on("brush", function (d) {
                        gridUpdate(d);
                    });
                }
            });
        }

        return {
            link: link,
            restrict: "E",
            scope: {
                data: "="
            },
            controller: "simulationController",
            templateUrl: 'app/visualizations/parallel-coordinate-plot-view.html'
        }
    }
    ]);
