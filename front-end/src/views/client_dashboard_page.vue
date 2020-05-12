<template>
  <div id="client_dashboard_page" class="client_dashboard_page">
    <menuComponent />

    <div class="column_one">
      <div class="clients_board">
        <clientTableComponent :clientList="clientList.data" />
       
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
import axios from "axios";
import menuComponent from "../components/menu_component.vue";
import clientTableComponent from "../components/client_table_component.vue";
import clientKeywordsComponent from "../components/client_keywords_component.vue";
import clientChartComponent from "../components/client_chart_component.vue";

export default {
  name: "clientDashboardPage",
  data(){
    return{
      clientList: null,
    }
  },
  components: {
    menuComponent,
    clientTableComponent,
    clientKeywordsComponent,
    clientChartComponent,
  },
   mounted() {
    axios
      .get("http://demand-analysis.local/api/analysis/all")
      .then((response) => (this.clientList = response));
  },
};
</script>

<style lang="scss">
.client_dashboard_page {
  background-color: $grey;
  margin: 0px;
  margin: 0 0 0 60px;
  height: 100vh;
}

.column_one {
  overflow: hidden;
  display: grid;
  grid-template-columns: 70vw 27vw;
  height: 88%;
}

.column_two {
  display: grid;
  grid-template-rows: 20% 77%;
}

.clients_board {
  margin: 24px 24px 0 24px;
  overflow: hidden;
  background-color: $white;
  padding: 24px;

}

.clients_keywords {
  margin: 24px 24px 0 0 ;
  overflow: hidden;
  background-color: $white;
  padding: 24px;

}

.clients_charts_board {
  margin:24px 24px 0 0;
  overflow: hidden;
  background-color: $white;
  padding: 24px;

}
</style>
