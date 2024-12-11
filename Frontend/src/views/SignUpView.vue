<script setup lang="ts">
import SignUpForm from '@/components/SignUpForm.vue';
</script>

<template>
  <main class="flex bg-gray-100 min-h-screen">
    <div class="md:container md:mx-auto">
      <div class="px-6 py-8 bg-white mt-16 rounded-md shadow-sm">
        <h1 class="text-xl text-center text-gray-700 font-semibold mb-6">Cadastro de Usuário</h1>
        <SignUpForm />
      </div>
    </div>
  </main>
</template>