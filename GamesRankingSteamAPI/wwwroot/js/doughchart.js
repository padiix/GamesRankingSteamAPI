var ctx = document.getElementById('myChart');
var holder = document.getElementById('canvas-holder');
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
};

var configNotMobile = {
    type: 'doughnut',

    data: dataset,

    options: {
        responsive: true,
        aspectRatio: 2,
        legend: {
            labels: {
                boxWidth: 25,
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

var configMobile = {
    type: 'doughnut',

    data: dataset,

    options: {
        responsive: true,
            aspectRatio: 1,
                legend: {
            labels: {
                boxWidth: 15,
                fontColor: 'white',
                fontFamily: "'Roboto', sans-serif",
                fontSize: 11
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

function updateForMobile(myChart) {
    myChart.options = {
        responsive: true,
            aspectRatio: 1,
                legend: {
            labels: {
                boxWidth: 15,
                    fontColor: 'white',
                        fontFamily: "'Roboto', sans-serif",
                            fontSize: 11
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
    };
    myChart.update();
}

function updateForOthers(myChart) {
    myChart.options = {
        responsive: true,
        aspectRatio: 2,
        legend: {
            labels: {
                boxWidth: 25,
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
    };
    myChart.update();
}


window.onload = function () {
    if (window.screen.availWidth >= 360) {
        myChart = new Chart(ctx, configNotMobile);
        holder.style.width = "80%";
    }
    else {
        myChart = new Chart(ctx, configMobile);
        holder.style.width = "100%";
    }
}



