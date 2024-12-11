<script setup lang="ts">
import { RouterView } from 'vue-router'
import Toast from '@/components/ToastNotification.vue'
</script>

<template>
  <header>
    <div class="wrapper"></div>
  </header>
  <RouterView />
  <Toast />
</template>
