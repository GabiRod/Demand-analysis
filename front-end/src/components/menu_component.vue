<template>
  <div id="menu_component" class="menu_component">
    <div class="secondary_menu_bar"></div>

    <div class="primary_menu_bar">
      <div class="menu_bar">
        <img class="menu_logo" src="../assets/resize.png" alt="" />

        <h1 class="menu_title">Clients</h1>
      </div>

      <button class="logout_button" v-on:click="logout()">LOG OUT</button>
    </div>
  </div>
</template>

<script>
export default {
  name: "menuComponent",
  methods: {
    logout() {
      this.$emit("authenticated", false);
      this.$router.replace({ path: "/" });
    },
  },
};
</script>

<style lang="scss">
.menu_component {
  background-color: $white;
 
}

.menu_logo {
  height: 80px;
  width: auto;
  z-index: 2;
  margin-left: -10px;
}

.primary_menu_bar {
  display: flex;
  justify-content: space-between;
  margin-left: -80px;
  box-shadow: 1px 1px 10px 0.1px $bordergrey;
}

.menu_bar {
  display: flex;
}

.menu_title {
  padding-left: 20px;
  color: $blue;
  font-family: "ProximaExtra", sans-serif;
}

.logout_button {
  margin: auto 10px;
  padding: 10px 20px;
  text-decoration: none;
  border: 3px solid $green;
  color: $green;
  background-color: $white;

  &:hover {
    background-color: $green;
    color: $white;
  }
}

.secondary_menu_bar {
  display: flex;
  flex-direction: column;
  float: left;
  background-color: $white;
  width: 60px;
  position: fixed;
  left: 0;
  height: 100vh;
  text-align: center;
  box-shadow: 1px 90px 10px 0.1px $bordergrey;
}
</style>
