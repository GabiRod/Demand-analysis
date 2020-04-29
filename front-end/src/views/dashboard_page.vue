<template>
  <div id="dashboard_page" class="dashboard_page">
    <menuComponent />

    <div class="column_one">
      <div class="clients_board">
        <clientTableComponent />
      </div>

      <div class="column_two">
        <div class="clients_keywords"><clientKeywordsComponent /></div>

        <div class="clients_charts_board">
          clients charts board
        </div>
      </div>
    </div>

    <router-link to="/scan_page">go back</router-link>
  </div>
</template>

<script>
import menuComponent from "../components/menu_component.vue";
import clientTableComponent from "../components/client_table_component.vue";
import clientKeywordsComponent from "../components/client_keywords_component.vue";

export default {
  name: "dashboardPage",
  components: {
    menuComponent,
    clientTableComponent,
    clientKeywordsComponent,
  },
};
</script>

<style lang="scss">
.dashboard_page {
  background-color: $grey;
  margin: 0px;
  margin: 0 0 0 80px;
  height: 100vh;
}

.column_one {
  display: grid;
  grid-template-columns: 70% 25%;
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
  height: 100% -24px;
  width: 100% -24px;
}

.clients_charts_board {
  background-color: $white;
  padding: 24px;
  height: 100% -24px;
  width: 100% -24px;
}
</style>
