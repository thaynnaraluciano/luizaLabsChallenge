<script setup lang="ts">
import { useRouter } from 'vue-router'
import { useAuthStore } from '@/stores/auth';

const authStore = useAuthStore()
const router = useRouter()

const signOut = () => {
  authStore.clearToken()
  router.push({ name: 'Login' })
}
</script>

<template>
   <main class="flex bg-gray-100 min-h-screen">
    <div class="md:container md:mx-auto">
      <div class="px-6 py-8 bg-white mt-16 rounded-md shadow-sm">
        <h1 class="text-xl text-center text-gray-700 font-semibold mb-6">Painel</h1>

        <div class="flex justify-center mt-6">
          <button
            @click="signOut"
            class="bg-red-500 hover:bg-red-700 text-white font-bold py-2 px-4 rounded">
            Sair
          </button>
        </div>
      </div>
    </div>
  </main>
</template>
