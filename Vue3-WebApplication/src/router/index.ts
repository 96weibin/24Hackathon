import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'
import Summary from "../views/Summary/Summary.vue";


const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    name: 'Summary',
    component: Summary
  },
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
