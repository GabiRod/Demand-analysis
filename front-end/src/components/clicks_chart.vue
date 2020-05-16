<template>
  <div id="clicks_chart" class="clicks_chart">
    <div class="chart">
      <div>By categories</div>
      <canvas id="keywords-chart"></canvas>
      <br>

      <div>By intent</div>
       <canvas id="intent-chart"></canvas>
    </div>
  </div>
</template>

<script>
import Chart from "chart.js";

export default {
  name: "clicks_chart",
  props: ['id'],
  data() {
    return {
      categoriesChart: null,
      keywordsChartData: keywordsChartData,
      intentChartData: intentChartData,
    };
  },
  methods: {
    createChart(chartId, chartData) {
      const ctx = document.getElementById(chartId);
      new Chart(ctx, {
        type: chartData.type,
        data: chartData.data,
        options: chartData.options,
      });
    },
  },
  mounted() {
    this.createChart("keywords-chart", this.keywordsChartData);
    this.createChart("intent-chart", this.intentChartData);
  },
};

 const keywordsChartData = {
 type: 'doughnut',
 data: {
      labels: ["Kitchen", "Living room", "Bw", "Latin America", "North America"],
      datasets: [{
       
        backgroundColor: ["#3e95cd", "#8e5ea2","#3cba9f","#e8c3b9","#c45850"],
        data: [2478,5267,734,784,433]
      }]
    },
  options: {
    responsive: true,
    labels: false,
    legend:false,
  }
}
const intentChartData = {
  type: 'line',
  data: {
    datasets: [{ // one line graph
        label: ' Upper',
        data: [100, 56, 1, 2, 67, 14],
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
    labels: true,
    scales: {
      yAxes: [{
        display: false

      }],
      xAxes: [{
        display: false
      }]
    }
  }
}

</script>

<style lang="scss">
.keywords_intent_component {
  background-color: $white;
  text-align: center;
}

.chart {
  height: 80%;
  width: 100%;
}
</style>

