<script setup lang="ts">
import LoginForm from '../components/LoginForm.vue'
</script>

<template>
  <main class="flex items-center justify-center min-h-screen bg-gray-100">
    <div class="w-full max-w-sm p-4 bg-white rounded-md shadow-md">
      <h2 class="text-2xl font-semibold text-center text-gray-700">Login</h2>
      <LoginForm />
      <p class="mt-4 text-center">
        <RouterLink :to="{ name: 'SignUp' }" class="text-blue-500 hover:underline">Criar uma conta</RouterLink>
      </p>
      <p class="mt-1 text-center">
        <RouterLink :to="{ name: 'ResendEmailConfirmation' }" class="text-blue-500 hover:underline">Reenviar email de confirmação</RouterLink>
      </p>
    </div>
  </main>
</template>
