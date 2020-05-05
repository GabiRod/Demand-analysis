<template>
  <div id="app">
    <div>
      {{ info }}
    </div>
    <router-view />
  </div>
</template>

<script>
import axios from "axios";

export default {
  name: "App",
  data() {
    return {
      authenticated: false,
      info: null,
    };
  },
  methods: {
    setAuthenticated(status) {
      this.authenticated = status;
    },
    logout() {
      this.authenticated = false;
    },
  },
  mounted() {
    axios
      .get("http://demand-analysis.local/api/sites")
      .then((response) => (this.info = response));

    if (!this.authenticated) {
      this.$router.replace({ path: "/" });
    }
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
  margin: 0px;
}
</style>
