<script setup lang="ts">
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { useToastStore } from '@/stores/toast'
import FormInput from '@/components/FormInput.vue';

const toastStore = useToastStore()
const router = useRouter()
const email = ref('')

const handleSubmit = async (event: Event) => {
  event.preventDefault()

  try {
    const response = await fetch('http://localhost:4000/api/v1/emailConfirmation', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        email: email.value
      }),
    })

    const data = await response.json()
    if (response.ok) {
      toastStore.showToast("Email de confirmação enviado", 'success')
      router.push({ name: 'Login' })
    } else {
      toastStore.showToast(data.Message, 'error')
    }
  } catch (error) {
    toastStore.showToast('Erro ao reenviar email de confirmação', 'error')
    console.log(error)
  }
}
</script>

<template>
  <form @submit="handleSubmit">
    <div class="mb-4">
      <FormInput
        v-model="email"
        label="E-mail"
        id="email"
        name="email"
        type="email"
        required />
    </div>
    <button
      type="submit"
      class="w-full p-2 bg-blue-500 text-white rounded-md shadow-sm hover:bg-blue-600 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500">
      Reenviar
    </button>
  </form>
</template>
