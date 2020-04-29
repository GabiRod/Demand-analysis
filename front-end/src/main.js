import Vue from 'vue'
import App from './App.vue'
import router from './router/index.js'


Vue.config.productionTip = false


new Vue({
  router,
  render: h => h(App),
}).$mount('#app')


// fetch('http://demand-analysis.local/#/api/sites')
//   .then(response => {
//     return response.json();
//   })
//   .then(sites => {
//     console.log(sites);
//   })

