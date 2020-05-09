<template>
  <div id="keywords_dashboard_page" class="keywords_dashboard_page">
    <menuComponentClient :keywords="keywords" />

    <div class="column_one">
      <div class="column_two">
        <div class="categories"></div>
        <div class="clients_board2">
          <button class="keywords_menu" v-on:click="keywordsComponent()">Keywords</button>
          <button class="keywords_menu" v-on:click="wordsComponent()">Words</button>
          <keep-alive>
            <component :is="component" :keywords="keywords.data"></component>
          </keep-alive>
        </div>
      </div>

      <div class="column_two">
        <div class="clients_keywords"><clientKeywordsComponent /></div>

        <div class="clients_charts_board">
          Number of keywords by intent
          <keywordsChartComponent/>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import menuComponentClient from "../components/menu_component_client.vue";
import keywordsTableComponent from "../components/keywords_table_component.vue";
import wordsTableComponent from "../components/words_table_component.vue";
import clientKeywordsComponent from "../components/client_keywords_component.vue";
import keywordsChartComponent from "../components/keywords_chart_component.vue";

export default {
  name: "dashboardPage",
  data() {
    return {
      id:"",
      keywords: null,
    };
  },
  components: {
    menuComponentClient,
    keywordsTableComponent,
    wordsTableComponent,
    clientKeywordsComponent,
    keywordsChartComponent,
  },
  methods: {
    keywordsComponent() {
      this.component = "keywordsTableComponent";
    },
    wordsComponent() {
      this.component = "wordsTableComponent";
     
    },
  },
  mounted() {
    axios
      .get("http://demand-analysis.local/api/analysis/1")
      .then((response) => (this.keywords = response));
  },
};
</script>

<style lang="scss">
.keywords_dashboard_page {
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
  width: 100%;
}

.column_two {
  display: grid;
  grid-template-rows: 20% 77%;
  row-gap: 24px;
}

.clients_board2 {
  margin: 0 24px 0 24px;
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
  margin:0 24px 0 0;
  overflow: hidden;
  background-color: $white;
  padding: 24px;
}

.keywords_menu{
  float:left
}
</style>
