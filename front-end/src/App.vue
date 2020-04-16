<template>
  <div id="app">
    <loginPage />
  </div>
</template>

<script>
import loginPage from "./views/login_page.vue";

export default {
  name: "App",
  components: {
    loginPage,
  },
};
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
