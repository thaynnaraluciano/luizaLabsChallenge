<script setup lang="ts">
</script>

<template>
  <main class="flex bg-gray-100 min-h-screen">
    <div class="md:container md:mx-auto">
      <div class="px-6 py-8 bg-white mt-16 rounded-md shadow-sm">
        <h1 class="text-xl text-center text-gray-700 font-semibold mb-6">E-mail confirmado</h1>
        
        <div class="text-center">
          <p class="text-gray-600">Seu e-mail foi confirmado com sucesso.</p>
          <p class="text-gray-600">Você já pode fazer login na aplicação.</p>
        </div>

        <div class="flex justify-center mt-6">
          <RouterLink :to="{ name: 'Login' }" class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded">
            Fazer login
          </RouterLink>
        </div>
      </div>
    </div>
  </main>
</template>
