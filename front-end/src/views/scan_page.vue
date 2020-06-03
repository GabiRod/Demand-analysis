<template>
  <div id="scan_page" class="scan_page">
    <h1>Add new client</h1>
    <br />
    <br />
    Enter client's name
    <br>
    <input type="text" class="scan_input" placeholder="Enter client's name" v-model="customerName" />
    <br/>
    <br/>
    Enter the URL
    <br>
    <input type="text" class="scan_input" placeholder="Enter the URL" v-model="siteUrl" />
    <br />
    <br />
    <br />
    <router-link @click="save()" class="scan_button" to="/client_dashboard_page">SAVE</router-link>
  </div>
</template>

<script>
import axios from "axios";

export default {
  name: "scanPage",
  data(){
    return{
      customerName: " ",
      siteUrl: " ",
    }
  },

  methods: {
   save(){
     // posting the data to the database - database not accepting data
    axios
      .post("http://demand-analysis.local/api/createAnalysis", {
        "customerName": "",
        "siteUrl": ""
      })
     .then(function (response) {
      console.log(response);
      })
    }
  }
}
</script>

<style lang="scss">
.scan_page{
  margin: 0;
  text-align: center;
  background-color: $grey;
  height: 100vh;
  width: 100%;
font-weight:300;
}

.scan_input {
  padding: 10px;
  border: none;
 font-weight:300;
  box-shadow: 3px 3px 7px rgb(161, 161, 161);
}

.scan_button {
  padding: 10px 20px;
  text-decoration: none;
  border: 3px solid $green;
  color: $green;
  background-color: $grey;
font-weight:600;

  &:hover {
    background-color: $green;
    color: $grey;
  }
}
</style>
