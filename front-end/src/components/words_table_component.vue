<template>
  <div id="words_table_component" class="words_table_component">
    <input
      class="search_input"
      type="text"
      name="search"
      v-model="search"
      placeholder="search"
    />
    <div
      class="clients_column_menu"
      v-for="menu in constructor"
      v-on:click="sortTable(post)"
      :key="menu.id"
    ></div>
     <div class="words_table">
      <div class="words_row words_menu_row">
        <div class="">Word</div>
        <div class="">Volume</div>
      </div>
  
    
      <div class="words_row" :key="post.id" v-for="post in keywordList.Results">
        <div class="words_data">{{ post.Keyword }}</div>
        <div class="words_data">{{ post.Clicks}}</div>
        <router-link class="client_button" to="/keywords_dashboard_page"
          >ENTER</router-link>
      </div>
    </div>
</template>

<script>
import axios from "axios";

export default {
  name: "wordsTableComponent",
  data() {
    return {
      keywordList:null,
      search: "",
    };
  },
  computed: {
    filteredList() {
      return this.keywordList.filter((post) => {
        return post.keyword.toLowerCase().includes(this.search.toLowerCase());
      });
    },
  },
   mounted() {
    axios
      .get("http://demand-analysis.local/api/analysis/1")
      .then((response) => (this.keywordList = response));
  },
  
};
</script>

<style lang="scss">
.words_table_component {
  background-color: $white;
  text-align: start;
}

.words_column_menu {
  padding: 5px 10px;
  color: grey;
  font-size: 15px;
}

.words_menu_row {
  padding: 10px 0px;
}

.words_row {
  text-align: left;
  font-size: 12px;
  display: grid;
  grid-template-columns: 15% 20% 15% auto 70px;
  border-bottom: 1px solid $grey;
  color: $blue;
  font-size: 12px;
  padding: 1px 10px;
}

.words_data {
  margin-top: auto;
  margin-bottom: auto;
}

.search_input {
  float: right;
  padding: 10px;
  border: none;
  box-shadow: 3px 3px 7px rgb(161, 161, 161);
}
.words_table {
  padding-top: 40px;
}

.arrow {
  float: right;
  width: 12px;
  height: 15px;
  background-repeat: no-repeat;
  background-size: contain;
  background-position-y: bottom;
}
</style>
