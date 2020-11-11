var config = {
    type: 'doughnut',
    data: {
        datasets: {
            datasets: [{
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
                        '#37A62E'
                ],
                label: "liczba grających osób",
            }],
            labels: [
                "Gra1", "Gra2", "Gra3", "Gra4", "Gra5", "Gra6", "Gra7", "Gra8", "Gra9", "Gra10"
            ]
        }
    },
    options: {
        responsive: true,
        legend: {
            position: 'bottom',
            labels: {
                fontColor = 'white',
                fontFamily = "'Roboto', sans-serif",
                fontSize = 10,
            }
        },
        title: {
            display: true,
            text: 'Top 10 - Najczęściej grane'
        },
        animation: {
            animateScale: true,
            animateRotate: true
        }
    }
};

window.onload = function () {
    var ctx = document.getElementById('doughnut-chart').getContext('2d');
    window.myDoughnut = new Chart(ctx, config);
};

