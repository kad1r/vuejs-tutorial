import Vue from "vue";
import Buefy from "buefy"
import 'buefy/dist/buefy.css'

document.addEventListener('DOMContentLoaded', function (event) {
    Vue.use(Buefy);

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
