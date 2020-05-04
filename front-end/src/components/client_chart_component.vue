<template>
  <div id="client_intent_component" class="client_intent_component">
    <div class="chart">
      <canvas id="intent-chart"></canvas>
    </div>
  </div>
</template>

<script>
import Chart from "chart.js";
import planetChartData from "../chart-data.js";

export default {
  data() {
    return {
      planetChartData: planetChartData,
    };
  },

  name: "clientIntentComponent",
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
    this.createChart("intent-chart", this.planetChartData);
  },
};
</script>

<style lang="scss">
.client_intent_component {
  background-color: $white;
  text-align: center;
}

.chart {
  height: 80%;
  width: 100%;
}
</style>
