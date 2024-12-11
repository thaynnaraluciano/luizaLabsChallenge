import { createRouter, createWebHistory } from 'vue-router'
import { jwtDecode } from 'jwt-decode'
import { useAuthStore } from '@/stores/auth'
import LoginView from '@/views/LoginView.vue'
import SignUpView from '@/views/SignUpView.vue'
import NotFound from '@/views/NotFound.vue'

const isAuthenticated = () => {
  const authStore = useAuthStore()
  const token = authStore.token
  if (!token) {
    if (!token) return false
    return false
  }

  try {
    const decodedToken: { exp: number } = jwtDecode(token)
    const currentTime = Date.now() / 1000

    if (decodedToken.exp > currentTime) return true

    authStore.clearToken()
    authStore.setErrorMessage('Sessão expirada. Faça login novamente')
    return false
  } catch (error) {
    authStore.clearToken()
    authStore.setErrorMessage('Erro inesperado. Faça login novamente')
    return false
  }
}

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'Painel',
      component: () => import('../views/PainelView.vue'),
      meta: { requiresAuth: true },
    },
    {
      path: '/:pathMatch(.*)*',
      name: 'NotFound',
      component: NotFound,
    },
    {
      path: '/login',
      name: 'login',
      component: LoginView,
    },
    {
      path: '/sign_up',
      name: 'sign_up',
      component: SignUpView,
    },
  ],
})

router.beforeEach((to, from, next) => {
  const authStore = useAuthStore()
  if (to.name === 'login' && isAuthenticated()) return next({ name: 'Painel' })
  if (!to.meta.requiresAuth) return next()
  if (to.meta.requiresAuth && isAuthenticated()) return next()

  authStore.setErrorMessage('É necessário fazer login para acessar esta página')  
  next({ name: 'login'})
})

export default router
