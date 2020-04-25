import Vue from 'vue'
import App from './App.vue'
import router from './router/index.js'


Vue.config.productionTip = false


new Vue({
  router,
  render: h => h(App),
}).$mount('#app')

// Create a request variable and assign a new XMLHttpRequest object to it.
var request = new XMLHttpRequest()

// Open a new connection, using the GET request on the URL endpoint
request.open('GET', 'https://ghibliapi.herokuapp.com/films', true)

request.onload = function () {
  // Begin accessing JSON data here
}

// Send request
request.send()