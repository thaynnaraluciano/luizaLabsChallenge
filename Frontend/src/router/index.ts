import { createRouter, createWebHistory } from 'vue-router'
import { jwtDecode } from 'jwt-decode'
import { useAuthStore } from '@/stores/auth'
import { useToastStore } from '@/stores/toast'
import LoginView from '@/views/LoginView.vue'
import SignUpView from '@/views/SignUpView.vue'
import EmailConfirmed from '@/views/EmailConfirmed.vue'
import NotFound from '@/views/NotFound.vue'


const isAuthenticated = (token: string) => {
  return Boolean(token && token.trim())
}

const isExpired = (token: string) => {
  if (!isAuthenticated(token)) return true

  const decodedToken: { exp: number } = jwtDecode(token)
  const currentTime = Date.now() / 1000

  return decodedToken.exp < currentTime
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
      path: '/login',
      name: 'Login',
      component: LoginView,
    },
    {
      path: '/sign_up',
      name: 'SignUp',
      component: SignUpView,
    },
    {
      path: '/email_confirmed',
      name: 'EmailConfirmed',
      component: EmailConfirmed,
    },
    {
      path: '/:pathMatch(.*)*',
      name: 'NotFound',
      component: NotFound,
    },
    {
      path: '/confirmEmail',
      name: 'ConfirmEmail',
      component: () => import('../views/ConfirmEmail.vue'),
      meta: { requiresAuth: false },
    },
    {
      path: '/resend_email_confirmation',
      name: 'ResendEmailConfirmation',
      component: () => import('@/views/ResendEmailConfirmation.vue'),
      meta: { requiresAuth: false },
    },
  ],
})

router.beforeEach((to, from, next) => {
  const authStore = useAuthStore()
  const toastStore = useToastStore()
  const token = authStore.token

  try {
    if (to.name === 'Login' && isAuthenticated(token) && !isExpired(token)) return next({ name: 'Painel' })
    if (isAuthenticated(token) && !isExpired(token)) return next()
    if (!to.meta.requiresAuth) return next()
  
    if (!isAuthenticated(token)) {
      toastStore.showToast('É necessário fazer login para acessar esta página', 'error')
    } else if (isExpired(token)) {
      toastStore.showToast('Sessão expirada. Faça login novamente', 'error')
    }
  } catch(error) {
    console.log(error)
    toastStore.showToast('Sessão inválida, faça login novamente', 'error')
  }
  authStore.clearToken()
  return next({ name: 'Login' })
})

export default router
