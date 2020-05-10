<template>
  <div id="menu_component_client" class="menu_component_client">
    <div class="secondary_menu_bar">
      subsites menu

    </div>

    <div class="primary_menu_bar">
      <div class="menu_bar">
        <img class="menu_logo" src="../assets/resize.png" alt="" />
        <router-link to="/client_dashboard_page">back</router-link>
        <h1 class="menu_title">{{ keywordList.data.CustomerName }}</h1>
      </div>

      <button class="logout_button" v-on:click="logout()">LOG OUT</button>
    </div>
  </div>
</template>

<script>
export default {
  name: "menuComponentClient",
  props: { keywordList: Object },
  data() {
    return {
     data:""
    }
  },
  methods: {
    logout() {
      this.$emit("authenticated", false);
      this.$router.replace({ path: "/" });
    },
  },
};
</script>

<style lang="scss">
.menu_component_client {
  background-color: $white;
}

.menu_logo {
  height: 70px;
  width: auto;
}
.primary_menu_bar {
  display: flex;
  justify-content: space-between;
  margin-left: -80px;
}

.menu_bar {
  display: flex;
}

.menu_title {
  padding: 0px 20px;

  color: $blue;
  margin-top: auto;
  margin-bottom: auto;
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
      z-index: -1;
  margin: 100px 0 0 0;
  display: flex;
  flex-direction: column;
  float: left;
  background-color: $white;
  width: 60px;
  position: fixed;
  left: 0;
  height: 100vh;
  text-align: center;
}
</style>
