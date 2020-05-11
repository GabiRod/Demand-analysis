<template>
  <div id="client_table_component" class="client_table_component">
    <router-link class="new_client" to="/scan_page">NEW</router-link>

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
        <div class="client_data">{{ client.customerName}}</div>
        <div class="client_data">{{ client.url }}</div>
        <div class="client_data">{{ client.createdDate }}</div>
        <input class="client_data client_data_note" v:model="client.note" />
        <router-link class="client_button" to="/keywords_dashboard_page"
          >ENTER</router-link
        >
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "clientTableComponent",
  props:{ clientList: Object },
  data() {
    return {
      search: "",
    };
  },
   computed: {
    filteredList() {
      return this.clientList.filter((client) => {
        return client.customerName.toLowerCase().includes(this.search.toLowerCase());
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

.client_menu_row {
  padding: 10px 0px;
  color: grey;
    font-size: 15px;
      display: grid;
  grid-template-columns: 15% 20% 15% auto 70px;
  border-bottom: 1px solid $grey;
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
  padding-top: 50px;
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

.new_client{
  padding: 5px 20px;
   text-decoration: none;
  border: 2px solid $green;
  color: $green;
  background-color: $white;
  font-size: 15px;
  margin: 1px 2px;

  &:hover {
    background-color: $green;
    color: $white;
  }
}
</style>
