import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'
import Summary from "../views/Summary/Summary.vue";
import Login from "../views/Login/Login.vue";
// import AI from '../views/AIQA/QuestionDetail.vue'
// import ask from '../views/AIQA/asked-question.vue';
// import qaList from '../views/AIQA/QAList.vue';
// import SearchResult from '../views/SearchResult/SearchResult.vue'
import Layout from "../views/Layout.vue";
import Charts from '../views/Charts/Charts.vue';
import Grid from '../views/Grid/Grid.vue'

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    name: 'Login',
    component: Login
  },

  {
    path: '/child',
    name: 'layout',
    component: Layout,
    children: [ {
      path: '/summary',
      name: 'Summary',
      component: Summary
    },
  
    {
      path: '/charts',
      name: 'charts',
      component: Charts
    },
    {
      path: '/grid',
      name: 'grid',
      component: Grid
    },
    
    // {
    //   path: '/ask',
    //   name: 'ask',
    //   component: ask
    // },
    // {
    //   path: '/qaList',
    //   name: 'qaList',
    //   component: qaList
    // },
    // {
    //   path: '/SearchResult',
    //   name: "SearchResult",
    //   component: SearchResult
    // }
  ]
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
