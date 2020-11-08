new Chart(document.getElementById("doughnut-chart"), {
    type: 'doughnut',
    data: {
        labels: ["Gra1", "Gra2", "Gra3", "Gra4", "Gra5", "Gra6", "Gra7", "Gra8", "Gra9", "Gra10"],
        datasets: [
            {
                label: "grających osób",
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
                data: [5, 10, 15, 20, 25, 30, 35, 40, 45, 50]
            }
        ]
    },
    options: {
        title: {
            display: false,
            text: 'Najpopularniejsze gry na bazie aktywnych graczy.'
        }
    }
});