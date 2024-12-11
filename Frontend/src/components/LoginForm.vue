<script setup lang="ts">
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from '@/stores/auth';

const router = useRouter()
const authStore = useAuthStore()
const username = ref('')
const password = ref('')

const handleSubmit = async (event: Event) => {
  event.preventDefault()

  try {
    const response = await fetch('https://thaynnara.free.beeceptor.com/login', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        username: username.value,
        password: password.value
      }),
    })

    if (response.ok) {
      const data = await response.json()
      authStore.setToken(data.token)
      router.push('/')
    } else {
      authStore.setErrorMessage('Usuário ou senha inválidos')
    }
  } catch(error) {
    authStore.setErrorMessage('Erro ao realizar login')
  } finally {
    username.value = ''
    password.value = ''
  }
}
</script>

<template>
  <div class="mb-4">
    <p v-if="authStore.errorMessage" class="text-red-500 text-sm">{{ authStore.errorMessage }}</p>
  </div>
  <form @submit="handleSubmit">
    <div class="mb-4">
      <label for="username" class="block text-sm font-medium text-gray-700">Usuário</label>
      <input
        v-model="username"
        type="text"
        id="username"
        name="username"
        class="mt-1 p-2 block w-full border border-gray-300 rounded-md shadow-sm focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm" />
    </div>
    <div class="mb-4">
      <label for="password" class="block text-sm font-medium text-gray-700">password</label>
      <input
        v-model="password"
        type="password"
        id="password"
        name="password"
        class="mt-1 p-2 block w-full border border-gray-300 rounded-md shadow-sm focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm" />
    </div>
    <button
      type="submit"
      class="w-full p-2 bg-blue-500 text-white rounded-md shadow-sm hover:bg-blue-600 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500">
      Entrar
    </button>
  </form>
</template>