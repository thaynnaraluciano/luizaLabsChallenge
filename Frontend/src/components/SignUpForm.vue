<script setup lang="ts">
import { computed, ref } from 'vue'
import { useRouter } from 'vue-router'
import { useToastStore } from '@/stores/toast';
import FormInput from '@/components/FormInput.vue';
import PasswordValidation from '@/components/PasswordValidation.vue';

const router = useRouter()
const toastStore = useToastStore()
const username = ref('')
const email = ref('')
const password = ref('')
const passwordConfirmation = ref('')

interface ApiError {
  Message: {
    UserName: string[],
    Email: string[],
    Password: string[],
  }
}

const errors = ref<ApiError>()

const passwordConfirmationIsValid = computed(() => {
  return password.value === passwordConfirmation.value
})

const handleSubmit = async (event: Event) => {
  event.preventDefault()

  try {
    const response = await fetch('http://localhost:4000/api/v1/user/create', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        userName: username.value,
        email: email.value,
        password: password.value,
      }),
    })

    const data = await response.json()
    if (response.ok) {
      toastStore.showToast('Usuário cadastrado com sucesso', 'success')
      router.push({ name: 'Login' })
    } else {
      errors.value = data as ApiError
      toastStore.showToast('Não foi possível completar o cadastro, verifique os erros indicados', 'error')
    }
  } catch (error) {
    toastStore.showToast('Erro ao cadastrar usuário', 'error')
    console.log(error)
  } finally {
    password.value = ''
    passwordConfirmation.value = ''
  }
}
</script>

<template>
  <form @submit="handleSubmit">
    <div class="grid grid-cols-2 gap-4">
      <div class="mb-4">
        <FormInput
          v-model="username"
          label="Usuário"
          id="username"
          name="username"
          :error="errors?.Message?.UserName?.join(', ')"
          required />
      </div>
      <div class="mb-4">
        <FormInput
          v-model="email"
          label="E-mail"
          id="email"
          name="email"
          type="email"
          :error="errors?.Message?.Email?.join(', ')"
          required />
        <span 
          v-if="email && !email.match(/^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/)"
          class="text-xs text-red-600">
          Digite um e-mail válido
        </span>
      </div>
      <div class="mb-4">
        <FormInput
          v-model="password"
          label="Senha"
          id="password"
          name="password"
          type="password"
          :error="errors?.Message?.Password?.join(', ')"
          required />
        <PasswordValidation :password="password" />
      </div>
      <div class="mb-4">
        <FormInput
          v-model="passwordConfirmation"
          label="Confirmação de senha"
          id="password_confirmation"
          name="password_confirmation"
          type="password"
          required />
        <span v-if="!passwordConfirmationIsValid && passwordConfirmation.length !== 0" class="text-xs text-red-600">Confirmação inválida</span>
      </div>
    </div>
    <div class="grid grid-cols-2 gap-2">
      <button
        @click="() => $router.back()"
        type="button"
        class="w-full p-2 mt-4 bg-gray-300 text-gray-700 rounded-md shadow-sm hover:bg-gray-400 focus:outline-none">
        Cancelar
      </button>
      <button
        type="submit"
        :disabled="!passwordConfirmationIsValid"
        class="w-full p-2 mt-4 bg-blue-500 text-white rounded-md shadow-sm hover:bg-blue-600 focus:outline-none disabled:bg-blue-300 disabled:cursor-not-allowed">
        Cadastrar
      </button>
    </div>
  </form>
</template>
