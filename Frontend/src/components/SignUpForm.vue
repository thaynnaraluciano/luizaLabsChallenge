<script setup lang="ts">
import { computed, ref } from 'vue'
import { useRouter } from 'vue-router'

const username = ref('')
const email = ref('')
const password = ref('')
const passwordConfirmation = ref('')

const passwordStrength = computed(() => {
  if (!password.value) return { message: '', color: '' }
  const score = calcultePasswordScore(password.value)

  const passwordStrengthMap = {
    0: { message: 'Muito fraca', color: 'text-red-600' },
    1: { message: 'Fraca', color: 'text-red-400' },
    2: { message: 'Razoável', color: 'text-amber-600' },
    3: { message: 'Boa', color: 'text-cyan-600' },
    4: { message: 'Forte', color: 'text-green-300' },
    5: { message: 'Muito forte', color: 'text-green-500' }
  }

  return {
    score,
    ...passwordStrengthMap[score as keyof typeof passwordStrengthMap]
  }
})

const calcultePasswordScore = (password: string) => {
  let score = 0

  if (password.length < 8) return 1
  if (password.length >= 8) score++
  if (password.length >= 12) score++
  if (/[a-zA-Z]/.test(password) && /[0-9]/.test(password)) score++
  if (/[A-Z]/.test(password) && /[a-z]/.test(password)) score++
  if (/[^a-zA-Z0-9]/.test(password)) score++

  return score
}

const passwordConfirmationIsValid = computed(() => {
  return password.value === passwordConfirmation.value
})

const handleSubmit = async (event: Event) => {
  event.preventDefault()

  console.log({
    username: username.value,
    email: email.value,
    password: password.value,
    passwordConfirmation: passwordConfirmation.value
  })
}
</script>

<template>
  <form @submit="handleSubmit">
    <div class="grid grid-cols-2 gap-4">
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
        <label for="email" class="block text-sm font-medium text-gray-700">E-mail</label>
        <input
          v-model="email"
          type="email"
          id="email"
          name="email"
          required
          class="mt-1 p-2 block w-full border border-gray-300 rounded-md shadow-sm focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm" />
        <span 
          v-if="email && !email.match(/^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/)"
          class="text-xs text-red-600">
          Digite um e-mail válido
        </span>
      </div>
      <div class="mb-4">
        <label for="password" class="block text-sm font-medium text-gray-700">Senha</label>
        <input
          v-model="password"
          type="password"
          id="password"
          name="password"
          class="mt-1 p-2 block w-full border border-gray-300 rounded-md shadow-sm focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm" />
        <div>
          <span :class="[passwordStrength.color]" class="text-sm">{{ passwordStrength.message }}</span>
        </div>
      </div>
      <div class="mb-4">
        <label for="password_confirmation" class="block text-sm font-medium text-gray-700">Confirmação de senha</label>
        <input
          v-model="passwordConfirmation"
          type="password"
          id="password_confirmation"
          name="password_confirmation"
          class="mt-1 p-2 block w-full border border-gray-300 rounded-md shadow-sm focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm" />
        <span v-if="!passwordConfirmationIsValid && passwordConfirmation.length !== 0" class="text-xs text-red-600">Confirmação inválida</span>
      </div>
    </div>
    <div class="grid grid-cols-2 gap-2">
      <button
        @click="() => $router.back()"
        type="button"
        class="w-full p-2 mt-4 bg-gray-300 text-gray-700 rounded-md shadow-sm hover:bg-gray-400 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-gray-500">
        Cancelar
      </button>
      <button
        type="submit"
        class="w-full p-2 mt-4 bg-blue-500 text-white rounded-md shadow-sm hover:bg-blue-600 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500">
        Cadastrar
      </button>
    </div>
  </form>
</template>