import { createRouter, createWebHistory } from 'vue-router'
import Dashboard from './views/Dashboard.vue'
// import Details from '../views/Details.vue'
import Login from './views/Login.vue'
// import Register from '../views/Register.vue'
//
const routes = [
  { path: '/', redirect: '/dashboard' },
  { path: '/dashboard', name: 'Dashboard', component: Dashboard },
  //   { path: '/details', name: 'Details', component: Details },
  { path: '/login', name: 'Login', component: Login },
  //   { path: '/register', name: 'Register', component: Register }
]

const router = createRouter({
  history: createWebHistory(),
  routes,
})

export default router
