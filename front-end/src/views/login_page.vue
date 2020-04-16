<template>
  <div id="login_page">
    <h1>Welcome to the demand analysis tool</h1>
    <input type="text" placeholder="email" />
    <br />
    <br />
    <input type="text" placeholder="password" />
    <br />
    <br />
    <router-link to="/scan_page">Log in</router-link>
  </div>
</template>

<script>
export default {
  name: "loginPage",
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
