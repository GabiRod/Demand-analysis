<template>
  <div id="menu_component" class="menu_component">
    <div class="menu_bar">
      <img class="menu_logo" src="../assets/resize.png" alt="" />
      <h1 class="menu_title">{{ route.title }}</h1>
    </div>

    <button class="logout_button" v-on:click="logout()">LOG OUT</button>
  </div>
</template>

<script>
export default {
  name: "loginPage",
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
  display: flex;
  justify-content: space-between;
}

.menu_logo {
  height: 70px;
  width: auto;
}

.menu_bar {
  display: flex;
}

.logout_button {
  margin: auto 10px;
  padding: 10px 20px;
  text-decoration: none;
  border: 3px solid $green;
  color: $green;
  background-color: $white;
}
</style>
