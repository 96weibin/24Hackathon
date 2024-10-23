import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'
import Summary from "../views/Summary/Summary.vue";
import Login from "../views/Login/Login.vue";
import Layout from "../views/Layout.vue";
import Charts from '../views/Charts/Charts.vue';
import Grid from '../views/Grid/Grid.vue'

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    name: 'Summary',
    component: Summary
  },

  // {
  //   path: '/child',
  //   name: 'layout',
  //   component: Layout,
  //   children: [ {
  //     path: '/summary',
  //     name: 'Summary',
  //     component: Summary
  //   },
  
  //   {
  //     path: '/charts',
  //     name: 'charts',
  //     component: Charts
  //   },
  //   {
  //     path: '/grid',
  //     name: 'grid',
  //     component: Grid
  //   },
  // ]
  // }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
