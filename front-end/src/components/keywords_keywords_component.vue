<template>
  <div id="client_keywords_component" class="client_keywords_component">
    <div class="client_keywords_number"> <ICountUp
      :delay="delay"
      :endVal="endVal"
      :options="options"
      @ready="onReady"
    /></div>
  
    <div class="client_keywords">keywords</div>
  </div>
</template>

<script>
export default {
  name: "clientKeywordsComponent",
  data() {
    return {
         delay: 1000,
        endVal: 120500,
        options: {
          useEasing: true,
          useGrouping: false,
          duration:5,
    }
    }
  },
   methods: {
      onReady: function(instance) {
        const that = this;
        instance.update(that.endVal);
      }
    }  
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
  font-size: 20px;
  font-weight:400;
}
</style>
