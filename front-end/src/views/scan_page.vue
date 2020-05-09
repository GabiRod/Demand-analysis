<template>
  <div id="scan_page" class="scan_page">
    <h1>Add new client</h1>
    <br />
    <br />
    <input type="text" class="scan_input" placeholder="Enter client's name" :customerName="customerName" />
    <br/>
    <br/>
    <input type="text" class="scan_input" placeholder="Enter the URL" :siteUrl="siteUrl" />
    <br />
    <br />
    <br />
    <router-link class="scan_button" to="/client_dashboard_page">SAVE</router-link>
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
.scan_page{
  margin: 0;
  text-align: center;
  background-color: $grey;
  height: 100vh;
  width: 100%;
}

.scan_input {
  padding: 10px;
  border: none;
  box-shadow: 3px 3px 7px rgb(161, 161, 161);
}

.scan_button {
  padding: 10px 20px;
  text-decoration: none;
  border: 3px solid $green;
  color: $green;
  background-color: $grey;

  &:hover {
    background-color: $green;
    color: $grey;
  }
}
</style>
