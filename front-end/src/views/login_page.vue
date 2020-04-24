<template>
  <div id="login_page">
    <h1>Welcome to the demand analysis tool</h1>
    <img
      class="login_logo"
      src="../assets/no_zebra.png"
      alt="No zebra black and white logo"
    />
    <h2>
      please enter your company email adress
    </h2>
    <br />
    <div class="login_input_wrapper">
      <input class="login_input" type="text" placeholder="email" />

      <br />
      <br />
      <input class="login_input" type="text" placeholder="password" />
    </div>
    <br />
    <br />
    <router-link class="button" to="/dashboard_page">LOG IN</router-link>
  </div>
</template>

<script>
export default {
  name: "loginPage",
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style lang="scss">
#login_page {
  text-align: center;
}
h1 {
  margin: 40px 0 0;
  color: $green;
}
h2 {
  margin: 40px 0 0;
  text-align: center;
  color: #42b983;
}

.login_input_wrapper {
  text-align: center;
}
.login_logo {
  width: 30%;
}
.login_input {
  padding: 5px;
  box-shadow: 5px whitesmoke;
}
.button {
  padding: 10px 20px;
  text-decoration: none;
  border: 3px solid #42b983;
  color: #42b983;
}
</style>
