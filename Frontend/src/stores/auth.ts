import { ref } from 'vue'
import { defineStore } from 'pinia'

export const useAuthStore = defineStore('auth', () => {
  const token = ref(localStorage.getItem('token') || '') 
  const errorMessage = ref('')
  const successMessage = ref('')
  
  const setToken = (newToken: string) => {
    token.value = newToken
    localStorage.setItem('token', newToken)
  }

  const clearToken = () => {
    token.value = ''
    localStorage.removeItem('token')
  }

  const setErrorMessage = (message: string) => {
    errorMessage.value = message
  }

  const setSuccessMessage = (message: string) => {
    successMessage.value = message
  }

  return {
    token,
    errorMessage,
    successMessage,
    setToken,
    clearToken,
    setErrorMessage,
    setSuccessMessage,
  }
})
