import Vue from 'vue'
import VueRouter from 'vue-router'
import loginPage from '../views/login_page.vue'
import scanPage from '../views/scan_page.vue'
import dashboardPage from '../views/dashboard_page.vue'
import keywordsDashboardPage from '../views/keywords_dashboard_page.vue'


Vue.use(VueRouter);

const routes = [
  {
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
    path: '/dashboard_page',
    name: "dashboardPage",
    component: dashboardPage
  },
  {
    path: '/keywords_dashboard_page',
    name: "keywordsDashboardPage",
    component: keywordsDashboardPage
  }
]

const router = new VueRouter({
  routes
})

export default router;