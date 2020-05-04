<template>
  <div id="client_dashboard_page" class="client_dashboard_page">
    <menuComponent />

    <div class="column_one">
      <div class="clients_board">
        <clientTableComponent />
      </div>

      <div class="column_two">
        <div class="clients_keywords"><clientKeywordsComponent /></div>

        <div class="clients_charts_board">
          Number of keywords by intent

          <clientChartComponent />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import menuComponent from "../components/menu_component.vue";
import clientTableComponent from "../components/client_table_component.vue";
import clientKeywordsComponent from "../components/client_keywords_component.vue";
import clientChartComponent from "../components/client_chart_component.vue";

export default {
  name: "clientDashboardPage",
  components: {
    menuComponent,
    clientTableComponent,
    clientKeywordsComponent,
    clientChartComponent,
  },
};
</script>

<style lang="scss">
.client_dashboard_page {
  background-color: $grey;
  margin: 0px;
  margin: 0 0 0 80px;
  height: 100vh;
}

.column_one {
  display: grid;
  grid-template-columns: 70vw 20vw;
  column-gap: 24px;
  margin: 24px;
  height: 80%;
  width: 100%;
}

.column_two {
  display: grid;
  grid-template-rows: 20% 77%;
  row-gap: 24px;
}

.clients_board {
  background-color: $white;
  padding: 24px;
  height: 100% -24px;
  width: 100% -24px;
}
.clients_keywords {
  background-color: $white;
  padding: 24px;
}

.clients_charts_board {
  background-color: $white;
  padding: 24px;
}
</style>
