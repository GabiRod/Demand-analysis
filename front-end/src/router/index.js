import Vue from 'vue'
import VueRouter from 'vue-router'
import loginPage from '../views/login_page.vue'
import scanPage from '../views/scan_page.vue'
import clientDashboardPage from '../views/client_dashboard_page.vue'
import keywordsDashboardPage from '../views/keywords_dashboard_page.vue'


Vue.use(VueRouter);

const routes = [{
    path: '/',
    name: "loginPage",
    component: loginPage
  },
  {
    path: '/scan_page',
    name: "scanPage",
    component: scanPage
  },
  {
    path: '/client_dashboard_page',
    name: "client_dashboardPage",
    component: clientDashboardPage
  },
  {
    path: '/keywords_dashboard_page/:id',
    name: "keywordsDashboardPage",
    component: keywordsDashboardPage,
    props: true,
    children: [{
        path: '/keywords_table_component',
        name: 'keywordsTableComponent',

        props: true
      },
      {
        path: '/words_table_component',
        name: 'wordsTableComponent',

        props: true
      }
    ]
  }
]

const router = new VueRouter({
  routes
})

export default router;