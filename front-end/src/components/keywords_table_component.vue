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
      <div class="keyword_menu_row">
        <div class="">Keyword</div>
        <div class="">Clicks</div>
        <div class="">Position</div>
        <div class="">CTR</div>
        <div class="">Category</div>
        <div class="">Subcategory1</div>
        <div class="">Subcategory2</div>
        <div class="">Intent</div>
      </div>
    
      <div class="keyword_row" :key="query.id" v-for="query in keywordList.data.Results ">
        <div class="keyword_data"><a :href="href">{{ query.Keyword }}</a></div>
        <div class="keyword_data">{{ query.Clicks }}</div>
        <div class="keyword_data">{{ query.Position }}</div>
        <div class="keyword_data">{{ query.Ctr }}</div>
        <input
        v-on:click="categoryColor()"
          class="keyword_data keyword_data_input"
          v:model="query.category"
         
        />
        <input

         class="keyword_data keyword_data_input"
          v:model="query.subcategory1"
        />
        <input
          class="keyword_data keyword_data_input"
          v:model="query.subcategory2"
        />
        <input class="keyword_data keyword_data_input" v:model="query.intent" />
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
      keywordList:null,
      search: "",
      href: 'query.SiteUrl',
      menuItems: [
      red='Home',
      blue='About',
      green=' '
      ]
    };
   
  },
  computed: {
    filteredList() {
      return this.keywordList.filter((query) => {
        return query.keyword.toLowerCase().includes(this.search.toLowerCase());
      });
    },
    categoryColor(){
      for
    }
   
  },

  mounted() {
    axios
      .get("http://demand-analysis.local/api/analysis/1")
      .then((response) => (this.keywordList = response));
  },
  
};
</script>

<style lang="scss">
.keywords_table_component {
  background-color: $white;
  text-align: start;

}

.keyword_menu_row {
  padding: 10px 0px;
    color: grey;
  font-size: 15px;
    display: grid;
  grid-template-columns: auto 10% 10% 10% 15% 15% 15% 10%;
  border-bottom: 1px solid $grey;
}

.keyword_row {
  text-align: left;
  font-size: 12px;
  display: grid;
  grid-template-columns: auto 10% 10% 10% 15% 15% 15% 10%;
  border-bottom: 1px solid $grey;
  color: $blue;
  font-size: 12px;
  padding: 1px 0px;
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

  float: right;
  padding: 10px;
  border: none;
  box-shadow: 3px 3px 7px rgb(161, 161, 161);
}

.keyword_table {
  padding-top: 50px;
}
</style>
