<script setup lang="ts">
import ResendEmailConfirmationForm from '@/components/ResendEmailConfirmationForm.vue'
</script>

<template>
  <main class="flex items-center justify-center min-h-screen bg-gray-100">
    <div class="w-full max-w-sm p-4 bg-white rounded-md shadow-md">
      <h2 class="text-2xl font-semibold text-center text-gray-700 mb-4">E-mail de confirmação</h2>
      <ResendEmailConfirmationForm />
      <p class="mt-4 text-center">
        <RouterLink :to="{ name: 'Login' }" class="text-blue-500 hover:underline">Fazer login</RouterLink>
      </p>
    </div>
  </main>
</template>
