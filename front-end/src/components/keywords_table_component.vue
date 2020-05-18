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
    <div class="scroll" >
      <div class="keyword_row" :key="query.id" v-for="query in keywordList">
        <div class="keyword_data"><a class="keyword_link" v-bind:href="''+query.Page+''">{{ query.Keyword }}</a></div>
        <div class="keyword_data">{{ query.Clicks }}</div>
        <div class="keyword_data">{{ query.Position }}</div>
        <div class="keyword_data">{{ query.Ctr }}</div>
        <input
          @onfocusout="save()"
          :value="query.Category"
          class="keyword_data keyword_data_input"
          v:model="category"
          v-bind:style='{
            "border-color" :  `${query.Colour}` == " " ? "$grey" : `${query.Colour}`,
            "background-color" :  `${query.Colour}` == " " ? "$grey" : `${query.Colour}`,
             "color" :`${query.Colour}` == " " ? "$blue" : "white"
            }'
        />
        <input
        @onfocusout="save()"
        :value="query.SubCategory1"
        class="keyword_data keyword_data_input"
        v:model="subCategory1"
        v-bind:style='{
            "border-color" :  `${query.Colour}` == " " ? "$grey" : `${query.Colour}`,
            "color" :`${query.Colour}` == " " ? "$blue" : `${query.Colour}`
            }'
        />
        <input
        @onfocusout="save()"
        :value="query.SubCategory2"
        class="keyword_data keyword_data_input"
        v:model="subCategory2"
        v-bind:style='{
            "border-color" :  `${query.Colour}` == " " ? "$grey" : `${query.Colour}`,
            "color" :`${query.Colour}` == " " ? "$blue" : `${query.Colour}`
            }'
        />
        <input 
        @onfocusout="save()"
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
      ascending: false,
      search: "",
      category: "",
            subCategory1: "",
            subCategory2: "",
            intent: "",
      intents : [
    {
        intent : "IntentTest",
        color : "red"
    },
    {
        intent : "transactional",
        color: "green"
    },
    {
        intent: "brand",
        color: "red"
    },
    {
        intent:"commertial",
        color:"orange"
    }
      ]
    };
   
  },
  computed: {
    filteredList() {
      return this.keywordList.filter((query) => {
        return query.Keyword.toLowerCase().includes(this.search.toLowerCase());
      });
    },  
  },
 mounted(){
    axios.get('http://demand-analysis.local/api/analysis/' + this.id)
    .then((response) => (this.keywordList = response.data.Results));
  },
  Mount(){
    this.submit()
    this.color()
  },
  methods:{
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
    color() {
      if (this.query.Intent.toLowerCase().includes("intenttest")){
      this.element.classList.add(".green_intent");
      }
      else{
        this.element.classList.add(".red_intent");
      }
    },
    save(){
      axios
      .put('http://demand-analysis.local/api/analysis/category/'+ this.keywordList.DataId, {
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
  border-bottom: 1px solid $grey;
  font-family: 'ProximaLight', sans-serif;
}

.keyword_row {
  text-align: left;
  font-size: 13px;
  display: grid;
  grid-template-columns: auto 10% 10% 10% 15% 15% 15% 10%;
  border-bottom: 1px solid $grey;
  color: $blue;
  font-size: 12px;
  padding: 1px 0px;
  margin: 3px 0px;
  font-family: 'ProximaLight', sans-serif;
}

.keyword_data {
  margin-top: auto;
  margin-bottom: auto;
}

.keyword_link{
  color: $blue;
  font-family: 'ProximaMedium', sans-serif;
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
  border: 2px solid $grey;
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

.scroll{
  max-height: 70vh;
  overflow-y: auto;
}

.green_intent{
background-color: green;
}

.red_intent{
background-color: red;
}
</style>
