<template>
  <div id="keywords_dashboard_page" class="keywords_dashboard_page">
    <menuComponentClient :keywords="keywords" />

    <div class="column_one">
      <div class="column_two">
        <div class="categories"></div>
        <div class="clients_board">
          <button v-on:click="keywordsComponent()">Keywords</button>
          <button v-on:click="wordsComponent()">Words</button>
          <keep-alive>
            <component :is="component"></component>
          </keep-alive>
        </div>
      </div>

      <div class="column_two">
        <div class="clients_keywords"><clientKeywordsComponent /></div>

        <div class="clients_charts_board">
          Number of keywords by intent
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
