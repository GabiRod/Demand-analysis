<template>
  <div id="menu_component_client" class="menu_component_client">
    <div class="secondary_menu_bar">
    </div>

    <div class="primary_menu_bar">
      <div class="menu_bar">
        <img class="menu_logo" src="../assets/resize.png" alt="" />
        <h1 class="menu_title">
        <router-link to="/client_dashboard_page"><img
          class="go_back"
          src="../assets/bck-icon.png"
          alt="go back link"
        /></router-link>
          {{ keywordList.CustomerName }}</h1>
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

.go_back{
  padding-right: 20px;
  max-width: 20px;
}


.menu_bar {
  display: flex;
}

.menu_title {
  padding: 0px 20px;
  color: $blue;
  margin-top: auto;
  margin-bottom: auto;
  font-family: "ProximaExtra", sans-serif;
}


</style>
