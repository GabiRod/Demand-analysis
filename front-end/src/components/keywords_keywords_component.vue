<template>
  <div id="client_keywords_component" class="client_keywords_component">
    <div class="client_keywords_number">{{ keywords }}</div>
  
    <div class="client_keywords">keywords</div>
  </div>
</template>

<script>
export default {
  name: "clientKeywordsComponent",
  data() {
    return {
      keywords: 268238,
    };
  },
};
</script>

<style lang="scss">
.client_keywords_component {
  background-color: $white;
  text-align: center;
}

.client_keywords_number {
  font-size: 40px;
  color: $green;
  font-weight:600;

}

.client_keywords {
  color: $blue;
  font-weight:400;
}
</style>
