<template>
  <div id="keywords_dashboard_page" class="keywords_dashboard_page">
    <menuComponentClient :keywordList="keywordList" />

    <div class="column_one">
      <div class="column_two_full">
        <div class="categories"></div>
        <div class="clients_board2">
          <button class="keywords_menu" 
                  @click="keywordsComponent()" 
                  :class="{isActive:selected == 1}" >Keywords</button>

          <button class="keywords_menu" 
                  @click="wordsComponent()" 
                  :class="{isActive:selected == 2}">Words</button>
          <keep-alive>
            <component :is="component" :id='id'></component>
          </keep-alive>
        </div>
      </div>

      <div class="column_two">
        <div class="clients_keywords"><clientKeywordsComponent /></div>

        <div class="clients_charts_board">
          <button class="keywords_menu" 
                  @click="keywords_chart()" 
                  :class="{isActive:active == 1}" >Keywords</button>
         <button class="keywords_menu" 
                  @click="clicks_chart()" 
                  :class="{isActive:active == 2}" >Clicks</button>
          <button class="keywords_menu" 
                  @click="position_chart()" 
                  :class="{isActive:active == 3}">Position</button>
          <button class="keywords_menu" 
                  @click="ctr_chart()" 
                  :class="{isActive:active == 4}">Ctr</button>

          <keep-alive>
            <component :is="chart" :id='id'></component>
          </keep-alive>
         
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
import keywords_chart from "../components/keywords_chart.vue";
import clicks_chart from "../components/clicks_chart.vue";
import position_chart from "../components/position_chart.vue";
import ctr_chart from "../components/ctr_chart.vue";

export default {
  name: "keywordsDashboardPage",
  props: ['id'],

  data() {
    return {
      active:1,
      selected: 1,
      keywordList: null,
      chart:"ctr_chart",
      component:"keywordsTableComponent"
    };
  },
  components: {
    menuComponentClient,
    keywordsTableComponent,
    wordsTableComponent,
    clientKeywordsComponent,
    keywords_chart,
    clicks_chart,
    position_chart,
    ctr_chart,
  },
  methods: {
    keywordsComponent() {
      this.component = "keywordsTableComponent";
      this.selected = 1;
    },
    wordsComponent() {
      this.component = "wordsTableComponent";
      this.selected = 2;
    },
    keywords_chart() {
      this.chart = "keywords_chart";
      this.active = 1;
    },
    clicks_chart() {
      this.chart = "clicks_chart";
      this.active = 2;
    },
    position_chart() {
      this.chart = "position_chart";
      this.active = 3;
    },
    ctr_chart() {
      this.chart = "ctr_chart";
      this.active = 4;
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
border-bottom: solid 3px $green !important;
}

.column_two_full{
grid-template-rows: 100vh;
padding-top: 24px;
}

.column_one {
  overflow: hidden;
  display: grid;
  grid-template-columns: 75vw 23vw;
  height: 88%;
  width: 100%;
}

.column_two {
  display: grid;
  grid-template-rows: 20vh 78vh;
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
  float:left;
  background-color: white;
  border: none;
  padding: 10px 15px 0px;
  font-size: 16px;
  font-family: 'ProximaLight', sans-serif;
  margin-top: -20px;
}
</style>
