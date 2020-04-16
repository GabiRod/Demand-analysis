<template>
  <div id="dasboard_page">
    <menuComponent />

    <h1>Welcome to the dashboard</h1>

    <router-link to="/scan_page">go back</router-link>
  </div>
</template>

<script>
import menuComponent from "../components/menu_component.vue";

export default {
  name: "dashboardPage",
  components: { menuComponent },

  data() {
    return {
      routes: this.$root.routes,
    };
  },
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
