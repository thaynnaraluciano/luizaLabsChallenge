import { defineStore } from 'pinia'
import { ref } from 'vue'

export const useToastStore = defineStore('toast', () => {
  const message = ref('')
  const type = ref('')
  const isVisible = ref(false)

  const showToast = (toastMessage: string, toastType: 'success' | 'error') => {
    message.value = toastMessage
    type.value = toastType
    isVisible.value = true
    setTimeout(() => {
      isVisible.value = false
    }, 5000)
  }

  const hideToast = () => {
    isVisible.value = false
  }

  return {
    message,
    type,
    isVisible,
    showToast,
    hideToast
  }
})