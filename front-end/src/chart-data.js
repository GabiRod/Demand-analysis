export const intentChartData = {
  type: 'line',
  data: {
    labels: ['Janurary', 'February', 'March', 'April', 'May'],
    datasets: [
      { // one line graph
        label: ' Upper',
        data: [0, 56, 1, 2, 67, 144],
        backgroundColor: [
          'rgba(251,43,43,.8)', // Red
        ],
        borderColor: [
          'rgba(251,43,43,.5)'
        ],
        borderWidth: 3
      },
      { // another line graph
        label: 'Middle',
        data: [7.8, 12.1, 122.7, 69.7, 139.8],
        backgroundColor: [
          'rgba(250, 124,7,.8)', // Orange
        ],
        borderColor: [
          'rgba(250, 124,7,.5)',
        ],
        borderWidth: 3
      },
      { // another line graph
        label: 'Lower',
        data: [195.8, 78.1, 122.7, 6.7, 115.8, 116.4, 50.7, 49.2],
        backgroundColor: [
          'rgba(71, 183,132,.8)', // Green
        ],
        borderColor: [
          'rgba(71, 183,132,.5)',
        ],
        borderWidth: 1
      }
    ]
  },
  options: {
    responsive: true,
    lineTension: 1,
    legend: {
      display: true,
      labels: {
        fontColor: '#283c66'
      },
      position: 'bottom',
    },
    scales: {
      yAxes: [{
        ticks: {
          beginAtZero: true,
          padding: 15,
          max: 200,
        }
      }],
      xAxes: [{
        display: false
      }]
    }
  }
}

export default intentChartData;