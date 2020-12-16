$(function () {

var ctx = document.getElementById('myChart');
let myChart;

let chartLabels = [];
let chartData = [];


    $.getJSON('/api/PopGam.json', function (jd) {
        var data = jd.data;
        $.each(data, function (index, data) {
            chartLabels.push(data.title);
            var ratingTwoDecimalPlaces = parseFloat(data.rating).toFixed(2);
            chartData.push(ratingTwoDecimalPlaces);
        });
        myChart = new Chart(ctx, config);
    });


var dataset = {
    labels: chartLabels,
    datasets: [{
        data: chartData,
        backgroundColor: [
            pattern.draw('square', '#1f77b4'),
            pattern.draw('circle', '#ff7f0e'),
            pattern.draw('diamond', '#2ca02c'),
            pattern.draw('zigzag-horizontal', '#17becf'),
            pattern.draw('triangle', 'rgb(255, 99, 132, 0.4)'),
            pattern.draw('dot','#7D4721'),
            pattern.draw('dot-dash','#2AF5B3'),
            pattern.draw('cross','#335268'),
            pattern.draw('line','#7BECF7'),
            pattern.draw('line-vertical','#2ca12d')
        ],
        borderColor: '#343a40'
    }]
};


var config = {
    type: 'doughnut',

    data: dataset,

    options: {
        responsive: true,
        maintainAspectRatio: false,
        legend: {
            labels: {
                boxWidth: 22,
                fontColor: 'white',
                fontFamily: "'Roboto', sans-serif",
                fontSize: 18
            },
            position: 'bottom'
        },

        layout: {
            padding: {
                left: 0,
                right: 0,
                top: 10,
                bottom: 0
            }
        },

        animation: {
            animateScale: true,
            animateRotate: true
        }
    }
}
});