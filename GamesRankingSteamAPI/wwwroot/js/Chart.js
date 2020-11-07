// chart colors
var colors = ['#007bff', '#097CF6', '#127CED', '#1B7CE4', '#247CDB', '#2E7DD1', '#377DC8', '#407DBF', '#497EB6', '#527EAD'];

//chart options
var donutOptions = {
    cutoutPercentage: 85,
    legend: {
        position: 'bottom',
        padding: 5,
        labels: {
            pointStyle: 'circle',
            usePointStyle: true
        }
    }
};

//chart data
var chDonutData1 = {
    labels: ['Game1', 'Game2', 'Game3', 'Game4', 'Game5', 'Game6', 'Game7', 'Game8', 'Game9', 'Game10'],
    datasets: [
        {
            backgroundColor: colors.slice(0, 10),
            borderWidth: 0,
            data: [10, 15, 20, 25, 30, 35, 40, 45, 50, 55]
        }
    ]
};

//script creating the chart
var chDonut1 = document.getElementById("chDonut1");
if (chDonut1) {
    new Chart(chDonut1, {
        type: 'pie',
        data: chDonutData1,
        options: donutOptions
    });
}