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
   
    <div class="client_table">
      <div class="row" :key="post.id" v-for="post in filteredList">
        <div class="keyword_row">{{ post.keyword }}</div>
        <div class="keyword_row">{{ post.volume }}</div>
        <div class="keyword_row">{{ post.position }}</div>
        <div class="keyword_row">{{ post.cta }}</div>
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
  name: "wordsTableComponent",

  data() {
    return {
      ascending: false,
      sortColumn: "",
      col: "",
      search: "",
      postList: [
        new Post(
          "Vue.js",
          "https://vuejs.org/",
          "Chris",
          "456",
          " ",
          " ",
          " ",
          " "
        ),
        new Post(
          "React.js",
          "https://facebook.github.io/react/",
          "Tim",
          "456",
          " ",
          " ",
          " ",
          " "
        ),
        new Post(
          "Angular.js",
          "https://angularjs.org/",
          "Sam",
          "4568",
          " ",
          " ",
          " ",
          " "
        ),
        new Post(
          "Ember.js",
          "http://emberjs.com/",
          "Rachel",
          "200",
          " ",
          " ",
          " ",
          " "
        ),
        new Post(
          "Meteor.js",
          "https://www.meteor.com/",
          "Chris",
          "1235",
          " ",
          " ",
          " ",
          " "
        ),
        new Post(
          "Aurelia",
          "http://aurelia.io/",
          "Tim",
          "896",
          " ",
          " ",
          " ",
          " "
        ),
        new Post(
          "Node.js",
          "https://nodejs.org/en/",
          "A. A. Ron",
          "4568",
          " ",
          " ",
          " ",
          " "
        ),
        new Post(
          "Pusher",
          "https://pusher.com/",
          "Alex",
          "1238",
          " ",
          " ",
          " ",
          " "
        ),
        new Post(
          "Feathers.js",
          "http://feathersjs.com/",
          "Chuck",
          "4568",
          " ",
          " ",
          " ",
          " "
        ),
      ],
    };
  },

  computed: {
    filteredList() {
      return this.postList.filter((post) => {
        return post.keyword.toLowerCase().includes(this.search.toLowerCase());
      });
    },
    fullName: function() {
      return this.post.category + " " + this.post.intent;
    },
  },
  
};
</script>

<style lang="scss">
.client_table_component {
  background-color: $white;
  text-align: start;
}

.clients_column_menu {
  padding: 5px 10px;
  color: grey;
  font-size: 15px;
}

.row {
  text-align: left;
  font-size: 12px;
  display: grid;
  grid-template-columns: 10% 10% 10% 10% 10% 10% 10% 10% auto;
}

.clients_row {
  border-bottom: 1px solid $grey;
}

.clients_row_data {
  color: $blue;
  font-size: 12px;
  padding: 5px 5%;
}

.search_input {
  float: right;
  padding: 10px;
  border: none;
  box-shadow: 3px 3px 7px rgb(161, 161, 161);
}
.client_table {
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
