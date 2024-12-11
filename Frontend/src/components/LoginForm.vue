<script setup lang="ts">
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from '@/stores/auth';
import { useToastStore } from '@/stores/toast';
import FormInput from '@/components/FormInput.vue';

const router = useRouter()
const authStore = useAuthStore()
const toastStore = useToastStore()
const username = ref('')
const password = ref('')

const handleSubmit = async (event: Event) => {
  event.preventDefault()

  try {
    const response = await fetch('http://localhost:4000/api/v1/user/login', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        userName: username.value,
        password: password.value
      }),
    })

    const data = await response.json()
    if (response.ok) {
      authStore.setToken(data.token)
      router.push({ name: 'Painel' })
    } else {
      toastStore.showToast(data.Message, 'error')
    }
  } catch(error) {
    toastStore.showToast('Erro ao realizar login', 'error')
    console.log(error)
  } finally {
    password.value = ''
  }
}
</script>

<template>
  <form @submit="handleSubmit">
    <div class="mb-4">
      <FormInput
        v-model="username"
        label="Usuário"
        id="username"
        name="username"
        required />
    </div>
    <div class="mb-4">
      <FormInput
        v-model="password"
        label="Senha"
        type="password"
        id="password"
        name="password"
        required />
    </div>
    <button
      type="submit"
      class="w-full p-2 bg-blue-500 text-white rounded-md shadow-sm hover:bg-blue-600 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500">
      Entrar
    </button>
  </form>
</template>
