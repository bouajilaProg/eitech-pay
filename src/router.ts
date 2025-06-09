import { createRouter, createWebHistory } from 'vue-router'

import Dashboard from './views/Dashboard.vue'
import ProductView from './views/ProductView.vue'
import ProductEdit from './views/ProductEdit.vue'
import Login from './views/Login.vue'
import NotFound from './views/NotFound.vue'
import Settings from './views/Settings.vue'

const routes = [
  { path: '/', redirect: '/dashboard' },
  { path: '/dashboard', name: 'Dashboard', component: Dashboard },
  { path: '/products/:product_id', name: 'ProductView', component: ProductView, props: true },
  { path: '/products/:product_id/edit', name: 'ProductEdit', component: ProductEdit, props: true },
  { path: '/login', name: 'Login', component: Login },
  { path:"/settings", name: 'Settings', component: Settings},

  // 404 fallback
  { path: '/:pathMatch(.*)*', name: 'NotFound', component: NotFound },
]

const router = createRouter({
  history: createWebHistory(),
  routes,
})

export default router
