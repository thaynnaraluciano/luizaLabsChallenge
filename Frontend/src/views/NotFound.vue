<script setup lang="ts">
import { useRouter } from 'vue-router';

const router = useRouter()

const redirectHome = () => {
  router.push('/')
}
</script>

<template>
  <main class="min-h-screen flex flex-col items-center justify-center bg-gray-100 text-center p-4">
    <h1 class="text-4xl font-bold text-red-600 mb-10">404 - Ops! Não encontramos o que procurava...</h1>
    <button
      @click="redirectHome"
      class="bg-blue-500 text-white py-2 px-6 rounded-lg hover:bg-blue-700 transition duration-300"
    >
      Voltar para a página inicial
    </button>
  </main>
</template>
