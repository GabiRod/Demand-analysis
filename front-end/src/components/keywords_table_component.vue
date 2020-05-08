<template>
  <div id="keywords_table_component" class="keywords_table_component">
    <input
      class="keyword_search_input"
      type="text"
      name="search"
      v-model="search"
      placeholder="Search"
    />

    <div class="keyword_table">
      <div class="keyword_row keyword_menu_row">
        <div class="">Keyword</div>
        <div class="">Clicks</div>
        <div class="">Position</div>
        <div class="">CTR</div>
        <div class="">Category</div>
        <div class="">Subcategory1</div>
        <div class="">Subcategory2</div>
        <div class="">Intent</div>
        
      </div>
      <div class="keyword_row" :key="keyword.id" v-for="keyword in keywords">
        <div class="keyword_data"><a href="{{keyword.Page}}">{{ keyword.Keyword }}</a></div>
        <div class="keyword_data">{{ keyword.Clicks }}</div>
        <div class="keyword_data">{{ keyword.Position }}</div>
        <div class="keyword_data">{{ keyword.Ctr }}</div>
        <input
          class="keyword_data keyword_data_input"
          v:model="keyword.category"
        />
        <input
          class="keyword_data keyword_data_input"
          v:model="keyword.subcategory1"
        />
        <input
          class="keyword_data keyword_data_input"
          v:model="keyword.subcategory2"
        />
        <input class="keyword_data keyword_data_input" v:model="keyword.intent" />
      </div>
    </div>
  </div>
</template>

<script>

import axios from "axios";
export default {
  name: "keywordsTableComponent",

  data() {
    return {
      keywords:null,
      search: ""
    };
  },

  computed: {
    filteredList() {
      return this.keywords.filter((keyword) => {
        return keyword.Keyword.toLowerCase().includes(this.search.toLowerCase());
      });
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
.keywords_table_component {
  background-color: $white;
  text-align: start;
}

.keyword_column_menu {
  padding: 5px 10px;
  color: grey;
  font-size: 15px;
}

.keyword_menu_row {
  padding: 10px 0px;
}

.keyword_row {
  text-align: left;
  font-size: 12px;
  display: grid;
  grid-template-columns: 15% 10% 10% 10% 15% 15% 15% 10%;
  border-bottom: 1px solid $grey;
  color: $blue;
  font-size: 12px;
  padding: 1px 10px;
}

.keyword_data {
  margin-top: auto;
  margin-bottom: auto;
}

.keyword_row_data {
  color: $blue;
  font-size: 12px;
  padding: 5px 5%;
}

.keyword_data_input {
  margin: 1px 10px 1px 0px;
  border-radius: 5px;
}

.keyword_search_input {
  margin-top: -30px;
  float: right;
  padding: 10px;
  border: none;
  box-shadow: 3px 3px 7px rgb(161, 161, 161);
}

.keyword_table {
  padding-top: 40px;
}
</style>
