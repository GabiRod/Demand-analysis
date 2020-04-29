<template>
  <div id="login_page" class="login_page">
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
      <input
        class="login_input"
        type="text"
        name="email"
        v-model="input.email"
        placeholder="email"
      />

      <br />
      <br />
      <input
        class="login_input"
        type="text"
        name="password"
        v-model="input.password"
        placeholder="password"
      />
    </div>
    <br />
    <br />
    <button class="login_button" v-on:click="login()">LOG IN</button>
  </div>
</template>

<script>
export default {
  name: "loginPage",
  data() {
    return {
      input: {
        email: "",
        password: "",
      },
    };
  },
  methods: {
    login() {
      if (this.input.email != "" && this.input.password != "") {
        if (
          this.input.email == "gro@nozebra.dk" &&
          this.input.password == "123"
        ) {
          this.$emit("authenticated", true);
          this.$router.replace({ path: "/dashboard_page" });
        } else {
          console.log("The username and / or password is incorrect");
          alert("The email adress and / or password is incorrect");
        }
      } else {
        console.log("A username and password must be present");
      }
    },
  },
};
</script>

<style lang="scss">
.login_page {
  margin: 0;
  text-align: center;
  background-color: $grey;
  height: 100vh;
  width: 100%;
}

h1 {
  margin: 0px;
  padding: 24px 0 0;
  color: $green;
}
h2 {
  margin: 40px 0 0;
  text-align: center;
  color: $green;
}

.login_input_wrapper {
  text-align: center;
}

.login_logo {
  height: 30%;
}

.login_input {
  padding: 10px;
  border: none;
  box-shadow: 3px 3px 7px rgb(161, 161, 161);
}

.login_button {
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
