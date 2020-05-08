<template>
  <div id="scan_page">
    <h1>Welcome to the scan page</h1>
    <input type="text" placeholder="Enter client's name" :customerName="customerName" />
    <input type="text" placeholder="Enter the URL" :siteUrl="siteUrl" />
    <br />
    <router-link to="/dashboard_page">Scan</router-link>
  </div>
</template>

<script>
import axios from "axios";

export default {
  name: "scanPage",

  mounted() {
    axios
      .post("demand-analysis.local/api/createAnalysis", {
        customerName: "",
        siteUrl: "",
      })
      .then((response) => (this.info = response));
    (function(response) {
      console.log(response);
    });
  },
};
</script>

<style lang="scss">
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
  color: $green;
}
</style>
