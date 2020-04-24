<template>
  <div id="menu_component">
    <h1>menu</h1>
    <button class="button" v-on:click="logout()">LOG OUT</button>
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
h1 {
  background-color: white;
}
</style>
