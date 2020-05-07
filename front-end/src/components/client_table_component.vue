<template>
  <div id="client_table_component" class="client_table_component">
    <input
      class="search_input"
      type="text"
      name="search"
      v-model="search"
      placeholder="Search"
    />

    <div class="client_table">
      <div class="client_row client_menu_row">
        <div class="">Name</div>
        <div class="">URL</div>
        <div class="">Keywords</div>
        <div class="">Note</div>
      </div>
      <div class="client_row" :key="client.id" v-for="client in filteredList">
        <div class="client_data">{{ client.name }}</div>
        <div class="client_data">{{ client.url }}</div>
        <div class="client_data">{{ client.keywords }}</div>
        <input class="client_data client_data_note" v:model="client.note" />
        <router-link class="client_button" to="/keywords_dashboard_page"
          >Enter</router-link
        >
      </div>
    </div>
  </div>
</template>

<script>
class Client {
  constructor(name, url, keywords, note) {
    this.name = name;
    this.url = url;
    this.keywords = keywords;
    this.note = note;
  }
}

<input type="text" placeholder="enter the URL" />;

export default {
  name: "clientTableComponent",

  data() {
    return {
      ascending: false,
      sortColumn: "",
      client: "",
      search: "",
      clientList: [
        new Client("Vue.js", "https://vuejs.org/", "456", " "),
        new Client("React.js", "https://facebook.github.io/react/", "456", " "),
        new Client("Angular.js", "https://angularjs.org/", "4568", " "),
        new Client("Ember.js", "http://emberjs.com/", "200", " "),
        new Client("Meteor.js", "https://www.meteor.com/", "1235", " "),
        new Client("Aurelia", "http://aurelia.io/", "Tim", "896", " "),
        new Client("Node.js", "https://nodejs.org/en/", "A. A. Ron", " "),
        new Client("Pusher", "https://pusher.com/", "1238", " "),
        new Client("Feathers.js", "http://feathersjs.com/", "4568", " "),
      ],
    };
  },

  computed: {
    filteredList() {
      return this.clientList.filter((client) => {
        return client.name.toLowerCase().includes(this.search.toLowerCase());
      });
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

.client_menu_row {
  padding: 10px 0px;
}

.client_row {
  text-align: left;
  font-size: 12px;
  display: grid;
  grid-template-columns: 15% 20% 15% auto 70px;
  border-bottom: 1px solid $grey;
  color: $blue;
  font-size: 12px;
  padding: 1px 10px;
}

.client_data {
  margin-top: auto;
  margin-bottom: auto;
}

.client_data_note {
  margin-right: 15px;
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

.client_button {
  text-align: center;
  padding: 2px 10px;
  float: right;
  text-decoration: none;
  border: 2px solid $green;
  color: $green;
  background-color: $white;
  font-size: 12px;
  margin: 1px 2px;

  &:hover {
    background-color: $green;
    color: $white;
  }
}
</style>
