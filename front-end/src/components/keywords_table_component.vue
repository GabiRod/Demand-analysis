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
        <div class="">Volume</div>
        <div class="">Position</div>
        <div class="">CTA</div>
        <div class="">Category</div>
        <div class="">Subcategory1</div>
        <div class="">Subcategory2</div>
        <div class="">Intent</div>
      </div>
      <div class="keyword_row" :key="post.id" v-for="post in filteredList">
        <div class="keyword_data">{{ post.keyword }}</div>
        <div class="keyword_data">{{ post.volume }}</div>
        <div class="keyword_data">{{ post.position }}</div>
        <div class="keyword_data">{{ post.cta }}</div>
        <input
          class="keyword_data keyword_data_input"
          v:model="post.category"
        />
        <input
          class="keyword_data keyword_data_input"
          v:model="post.subcategory1"
        />
        <input
          class="keyword_data keyword_data_input"
          v:model="post.subcategory2"
        />
        <input class="keyword_data keyword_data_input" v:model="post.intent" />
      </div>
    </div>
  </div>
</template>

<script>
class Post {
  constructor(
    keyword,
    volume,
    position,
    cta,
    category,
    subcategory1,
    subcategory2,
    intent
  ) {
    this.keyword = keyword;
    this.volume = volume;
    this.position = position;
    this.cta = cta;
    this.category = category;
    this.subcategory1 = subcategory1;
    this.subcategory2 = subcategory2;
    this.intent = intent;
  }
}

export default {
  name: "keywordsTableComponent",

  data() {
    return {
      ascending: false,
      sortColumn: "",
      col: "",
      search: "",
      postList: [
        new Post("Vue.js", "258", "Chris", "456", " ", " ", " ", " "),
        new Post("React.js", "526", "Tim", "456", " ", " ", " ", " "),
        new Post("Angular.js", "258", "Sam", "4568", " ", " ", " ", " "),
        new Post("Ember.js", "566", "Rachel", "200", " ", " ", " ", " "),
        new Post("Meteor.js", "528", "Chris", "1235", " ", " ", " ", " "),
        new Post("Aurelia", "525", "Tim", "896", " ", " ", " ", " "),
        new Post("Node.js", "452", "A. A. Ron", "4568", " ", " ", " ", " "),
        new Post("Pusher", "4525", "Alex", "1238", " ", " ", " ", " "),
        new Post("Feathers.js", "252", "Chuck", "4568", " ", " ", " ", " "),
      ],
    };
  },

  computed: {
    filteredList() {
      return this.postList.filter((post) => {
        return post.keyword.toLowerCase().includes(this.search.toLowerCase());
      });
    },
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
