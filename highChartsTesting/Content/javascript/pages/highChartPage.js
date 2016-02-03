/*! ========================================================================
 * dashboard.js
 * Page/renders: index.html
 * Plugins used: highcharts, sparkline
 * ======================================================================== */
$(function () {


    // Moving Averages Chart - HighCharts
    // ================================

    (function () {
        $('#container').highcharts({
            chart: {
                type: 'bar'
            },
            title: {
                text: 'Fruit Consumption'
            },
            xAxis: {
                categories: ['Apples', 'Bananas', 'Oranges']
            },
            yAxis: {
                title: {
                    text: 'Fruit eaten'
                }
            },
            series: [{
                name: 'Jane',
                data: [1]

            }, {
                name: 'John',
                data: [5, 7, 3]
            }],
        });
    });
});
   