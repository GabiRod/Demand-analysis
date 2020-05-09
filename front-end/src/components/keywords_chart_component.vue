<template>
  <div id="keywords_intent_component" class="keywords_intent_component">
    <div class="chart">
      <canvas id="keywords-chart"></canvas>
    </div>
  </div>
</template>

<script>
import Chart from "chart.js";

export default {
  data() {
    return {
      keywordsChartData: keywordsChartData,
    };
  },

  name: "keywordsChartComponent",
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
  },
};

 const keywordsChartData = {
 type: 'pie',
 data: {
      labels: ["Africa", "Asia", "Europe", "Latin America", "North America"],
      datasets: [{
        label: "Population (millions)",
        backgroundColor: ["#3e95cd", "#8e5ea2","#3cba9f","#e8c3b9","#c45850"],
        data: [2478,5267,734,784,433]
      }]
    },
  options: {
    responsive: true,
    labels: true,
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

