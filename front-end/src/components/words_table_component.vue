<template>
  <div id="words_table_component" class="words_table_component">
    <input
      class="search_input"
      type="text"
      name="search"
      v-model="search"
      placeholder="search"
    />
     <div class="words_table">
      <div class="words_menu_row">
        <div class="">Word</div>
        <div class="">Volume</div>
      </div>
  
   <div class="scroll" >
      <div class="words_row" :key="post.id" v-for="post in wordList">
        <div class="words_data">{{ post.keyword }}</div>
        <div class="words_data">{{ post.count}}</div>
         
        <router-link class="word_link" to="/keywords_table_component" >to keywords <img
          class="arrow_right"
          src="../assets/arrow_right.webp"
          alt="linking"
        /></router-link>
      </div>
   </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import Vue from 'vue';
    // Import component
    import Loading from 'vue-loading-overlay';
    // Import stylesheet
    import 'vue-loading-overlay/dist/vue-loading.css';
    // Init plugin
    Vue.use(Loading);
    
export default {
  name: "wordsTableComponent",
  props: ['id'],
  data() {
    return {
      wordList:null,
      search: "",
    };
  },
  computed: {
    //code prepared to be use for filtering the the table
    filteredList() {
      return this.wordList.filter((post) => {
        return post.keyword.toLowerCase().includes(this.search.toLowerCase());
      });
    },
  },
   mounted() {
     //fething data from the database
    axios
      .get('https://demand-analysis.nozebrahosting.dk/api/analysis/' + this.id +'/keywords')
      .then((response) => (this.wordList = response.data));
  },
     beforeMount(){
    this.submit()
  },
  methods:{
    //loader
    submit() {
      let loader = this.$loading.show({
        canCancel: true,
        onCancel: this.onCancel,
        color: '#17bb7c',
      });
      // simulate AJAX
      setTimeout(() => {
        loader.hide()
      },5000)                 
    }    
  }
  
};
</script>

<style lang="scss">
.words_table_component {
  background-color: $white;
  text-align: start;
}

.words_menu_row {
  padding: 1px 0px;
  color: grey;
  font-size: 15px;
   display: grid;
  grid-template-columns: 15% 20% 15% auto 70px;
  border-bottom: 1px solid $grey;
}

.words_row {
  text-align: left;
  display: grid;
  grid-template-columns: 15% 20% 15% auto 70px;
  border-bottom: 1px solid $grey;
  color: $blue;
  font-size: 12px;
  padding: 1px 1px;
}

.words_data {
  margin-top: auto;
  margin-bottom: auto;
}

.search_input {
  float: right;
  padding: 10px;
  border: none;
  box-shadow: 3px 3px 7px rgb(161, 161, 161);
}
.words_table {
  padding-top: 40px;
}

.word_link{
  color: $blue;
     &:hover {
     color: $green;
   }
}

.arrow {
  float: right;
  width: 12px;
  height: 15px;
  background-repeat: no-repeat;
  background-size: contain;
  background-position-y: bottom;
}

.scroll{
  max-height: 70vh;
  overflow-y: auto;
}

.arrow_right{
   margin-top: auto;
  margin-bottom: auto;
  max-height: 12px;
}
</style>
