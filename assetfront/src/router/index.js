import { createRouter, createWebHashHistory } from 'vue-router';
import HomeView from '../views/HomeView.vue';
import LoginView from '../views/LoginView.vue';
import { isSignedIn } from '../auth';


const routes = [
  {
    path: '/',
    name: 'login',
    component: LoginView,
    beforeEnter(_, __, next) { // Impede usuários não assinados
      if (!isSignedIn()) {       // de acessar a página Home.
        next();
        return;
      }
      next('/home')
    }
  },
  {
    path: '/home',
    name: 'home',
    component: HomeView,
    beforeEnter(_, __, next) { // Impede usuários não assinados
      if (isSignedIn()) {       // de acessar a página Home.
        next();
        return;
      }
      next('/login')
    }
  },
  {
    path: '/about',
    name: 'about',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/AboutView.vue')
  }
]

const router = createRouter({
  history: createWebHashHistory(),
  routes
})

export default router
