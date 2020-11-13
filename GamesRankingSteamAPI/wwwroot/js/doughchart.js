var ctx = document.getElementById('myChart');
var myChart;

var dataset = {
    labels: [
        "Gra1", "Gra2", "Gra3", "Gra4", "Gra5", "Gra6", "Gra7", "Gra8", "Gra9", "Gra10"
    ],
    datasets: [{
        label: "liczba grających osób",
        data: [
            10, 20, 30, 40, 50, 60, 70, 80, 90, 100
        ],
        backgroundColor: [
            pattern.draw('square', '#1f77b4'),
            pattern.draw('circle', '#ff7f0e'),
            pattern.draw('diamond', '#2ca02c'),
            pattern.draw('zigzag-horizontal', '#17becf'),
            pattern.draw('triangle', 'rgb(255, 99, 132, 0.4)'),
            '#7D4721',
            '#2AF5B3',
            '#335268',
            '#7BECF7',
            '#2ca12d'
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

myChart = new Chart(ctx, config);
