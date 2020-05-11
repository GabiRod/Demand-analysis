<template>
  <div id="keywords_dashboard_page" class="keywords_dashboard_page">
    <menuComponentClient :keywordList="keywordList" />

    <div class="column_one">
      <div class="column_two_full">
        <div class="categories"></div>
        <div class="clients_board2">
          <button v-bind:class="{ active: isActive }" class="keywords_menu" @click="keywordsComponent()">Keywords</button>
          <button class="keywords_menu" @click="wordsComponent()">Words</button>
          <keep-alive>
            <component :is="component" :id='id'></component>
          </keep-alive>
        </div>
      </div>

      <div class="column_two">
        <div class="clients_keywords"><clientKeywordsComponent /></div>

        <div class="clients_charts_board">
          Number of keywords by intent
          <keywordsChartComponent :id='id' />
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
  name: "keywordsDashboardPage",
  props: ['id'],

  data() {
    return {
      keywordList: null,
      component:"keywordsTableComponent"
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
      .get('http://demand-analysis.local/api/analysis/' + this.id)
      .then((response) => (this.keywordList = response.data));
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
 
.isActive{
  border-bottom: 5px green;
}
.column_two_full{

grid-template-rows: 100vh;
padding-top: 24px;
}

.column_one {
  overflow: hidden;
  display: grid;
  grid-template-columns: 75vw 21vw;
  height: 88%;
  width: 100%;
}

.column_two {
  display: grid;
  grid-template-rows: 20vh 77vh;
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
