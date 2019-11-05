import Vue from "vue";
import HighchartsVue from 'highcharts-vue'

document.addEventListener('DOMContentLoaded', function (event) {

    Vue.use(HighchartsVue);
    let view = new Vue({
        el: document.getElementById('view'),
        mounted: function () {
        },
        data: {
            chartOptions: {
                chart: {
                    type: 'spline'
                },
                title: {
                    text: 'Entire title  '
                },
                series: [{
                    data: [10, 0, 8, 2, 6, 4, 5, 5]
                }]
            },
            message: "One-way binding ",
            twoWayBindingMessage: "Type here ...",
            test : window.testData
        }
    });
});
