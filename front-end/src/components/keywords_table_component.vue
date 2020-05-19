<template>
  <div id="keywords_table_component" class="keywords_table_component">
    <input
      class="keyword_search_input"
      type="text"
      name="search"
      v-model="search"
      placeholder="Search"
    />

    <div class="keyword_table">
      <div class="keyword_menu_row">
        <div class="">Keyword</div>
        <div class="">Clicks</div>
        <div class="">Position</div>
        <div class="">CTR</div>
        <div class="">Category</div>
        <div class="">Subcategory1</div>
        <div class="">Subcategory2</div>
        <div class="">Intent</div>
      </div>
    <div class="scroll1" >
      <div class="keyword_row" :key="query.id" v-for="query in keywordList">
        <div class="keyword_data"><a class="keyword_link" v-bind:href="''+query.Page+''">{{ query.Keyword }}</a></div>
        <div class="keyword_data">{{ query.Clicks }}</div>
        <div class="keyword_data">{{ query.Position }}</div>
        <div class="keyword_data">{{ query.Ctr}}</div>
        <input
          @blur="save"
          :value="query.Category"
          class="keyword_data keyword_data_input"
          v:model="category"
          v-bind:style='{
            "border-color" :  `${query.Colour}` == "" ? "$grey" : `${query.Colour}`,
            "background-color" :  `${query.Colour}` == "" ? "$grey" : `${query.Colour}`,
             "color" :`${query.Colour}` == "" ? "$blue" : "white"
            }'
        />
        <input
        @blur="save"
        :value="query.SubCategory1"
        class="keyword_data keyword_data_input"
        v:model="subCategory1"
        v-bind:style='{
            "border-color" :  `${query.Colour}` == "" ? "$grey" : `${query.Colour}`,
            "color" :`${query.Colour}` == " " ? "$blue" : `${query.Colour}`
            }'
        />
        <input
        @blur="save()"
        :value="query.SubCategory2"
        class="keyword_data keyword_data_input"
        v:model="subCategory2"
        v-bind:style='{
            "border-color" :  `${query.Colour}` == "" ? "$grey" : `${query.Colour}`,
            "color" :`${query.Colour}` == " " ? "$blue" : `${query.Colour}`
            }'
        />
        <input 
        @blur="save()"
        :value="query.Intent" 
        class="keyword_data keyword_data_input" 
        v:model="intent"
            />
      </div>
      </div>
    </div>
  </div>
</template>

<script>
import Vue from 'vue';
    // Import component
    import Loading from 'vue-loading-overlay';
    // Import stylesheet
    import 'vue-loading-overlay/dist/vue-loading.css';
    // Init plugin
    Vue.use(Loading);
import axios from "axios";
export default {
  name: "keywordsTableComponent",
  props: ['id'],
  data() {
    return {
      keywordList:null,
      search: "",
      category: "",
      subCategory1: "",
      subCategory2: "",
      intent: "",
    };
   
  },
  computed: {
    //code prepared to be use for filtering the the table
    filteredList() {
      return this.keywordList.filter((query) => {
        return query.Keyword.toLowerCase().includes(this.search.toLowerCase());
      });
    },  
  },
 mounted(){
   //fetching data form the database and padding the clients ID
    axios
    .get('https://demand-analysis.nozebrahosting.dk/api/analysis/' + this.id)
    .then((response) => (this.keywordList = response.data.Results));
  },
  beforeMount(){
    this.submit()    
  },
 // created(){
 //   this.color()
 // },
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
    },
    
    // prepared function for adding the color to the intent
    //color() {
    //  if (this.intent.toLowerCase() !== "intenttest"){
    //    this.element.classList.add("green_intent");
     // }
     // else{
     //   this.element.classList.add("red_intent");
     // }
    //},
    //posting the data from inputs to the database not accepting for now
    save(){
      axios
      .put('https://demand-analysis.nozebrahosting.dk/api/analysis/category/1', {
            "category": "",
            "subCategory1": "",
            "subCategory2": "",
            "intent": ""
        })
      .then(function (response) {
        console.log(response);
      })
    }

  }
};
</script>

<style lang="scss">
.keywords_table_component {
  background-color: $white;
  text-align: start;
  margin: 2px;
}

.keyword_menu_row {
  padding: 1px 0px;
  color: grey;
  font-size: 15px;
  display: grid;
  text-align: left;
  grid-template-columns: auto 10% 10% 10% 15% 15% 15% 10%;
  border-bottom: 1.5px solid $grey;
  font-weight:400;
}

.keyword_row {
  text-align: left;
  display: grid;
  grid-template-columns: auto 10% 10% 10% 15% 15% 15% 10%;
  border-bottom: 1px solid $grey;
  color: $blue;
  font-size: 12px;
  padding: 1px 0px;
  margin: 3px 0px;
  font-weight:300;
}

.keyword_data {
  margin-top: auto;
  margin-bottom: auto;
}

.keyword_link{
  color: $blue;
  font-weight:300;
   &:hover {
     color: $green;
   }
}

.keyword_row_data {
  color: $blue;
  font-size: 12px;
  padding: 5px 0px;
}

.keyword_data_input {
  margin: 1px 20px 1px 0px;
  border-radius: 5px;
  text-align: center;
  border: 1px solid $grey;
  color: $blue;
  margin-top: auto;
  margin-bottom: auto;
  background-color: $grey;
}

.keyword_search_input {
  float: right;
  padding: 10px;
  border: none;
  box-shadow: 3px 3px 7px rgb(161, 161, 161);
}

.keyword_table {
  padding-top: 60px;
  height: 100%;
}

.scroll1{
  max-height: 68vh;
  overflow-y: auto;
}

.green_intent{
background-color: green;
}

.red_intent{
background-color: red;
}
</style>
