<template>
  <div id="keywords_chart" class="keywords_chart">
    <div class="chart">
      <div>By categories</div>
      <canvas id="keywords-chart"></canvas>
      <br>
      <br>
 
      <div>By intent</div>
       <canvas id="intent-chart"></canvas>
    </div>
  </div>
</template>

<script>
import Chart from "chart.js";

export default {
  name: "keywords_chart",
  props: ['id'],
  data() {
    return {
      categoriesChart: null,
      keywordsChartData: keywordsChartData,
      intentChartData: intentChartData,
    };
  },
  methods: {
    // create chart- chart.js documentation
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
      labels: ["Kitchen", "Living room", "Bedroom", "Garden", "Bathroom"],
      datasets: [{
       
        backgroundColor: ["#9BE1E3", "#E28383","#F1C491","#A99BE3","#F8D983"],
        data: [2478,5267,734,784,433]
      }]
    },
  options: {
    responsive: true,
    legend:false,
    weight: 2,
  }
}
const intentChartData = {
  type: 'line',
  
  data: {
    labels: ['Janurary', 'February', 'March', 'April', 'May'],
    datasets: [{ // one line graph
        label: ' Upper',
        data: [100, 16, 139, 200, 67, 143],
        backgroundColor: [
          'rgba(251,43,43,.8)', // Red
        ],
        borderColor: [
          'rgba(251,43,43,.8)'
        ],
        borderWidth: 3
      },
      { // another line graph
        label: 'Middle',
        data: [200, 120, 122, 69, 139, 268],
        backgroundColor: [
          'rgba(250, 124,7,.8)', // Orange
        ],
        borderColor: [
          'rgba(250, 124,7,.8)',
        ],
        borderWidth: 3
      },
      { // another line graph
        label: 'Lower',
        data: [195, 278, 122, 60, 115, 116],
        backgroundColor: [
          'rgba(71, 183,132,.8)', // Green
        ],
        borderColor: [
          'rgba(71, 183,132,.8)',
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

