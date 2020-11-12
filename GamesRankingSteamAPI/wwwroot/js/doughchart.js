var ctx = document.getElementById('myChart');

var myChart = new Chart(ctx, {
    type: 'doughnut',

    data: {
        labels: [
            "Gra1", "Gra2", "Gra3", "Gra4", "Gra5", "Gra6", "Gra7", "Gra8", "Gra9", "Gra10"
        ],
        datasets: [{
            label: "liczba grających osób",
            data: [
                5, 10, 15, 20, 25, 30, 35, 40, 45, 50
            ],
            backgroundColor: [
                '#1f77b4',
                '#ff7f0e',
                '#2ca02c',
                '#d62728',
                '#96E90A',
                '#7D4721',
                '#2AF5B3',
                '#335268',
                '#7BECF7',
                '#2ca12d'
            ],
            borderColor: '#343a40'
        }]
    },

    options: {
        responsive: true,
        aspectRatio: 1,
        legend: {
            labels: {
                boxWidth: 15,
                fontColor: 'white',
                fontFamily: "'Roboto', sans-serif",
                fontSize: 16
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
});
    




