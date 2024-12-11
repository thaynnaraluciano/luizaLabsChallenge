<script setup lang="ts">
import { useToastStore } from '@/stores/toast'
import { XMarkIcon } from '@heroicons/vue/24/outline';
import { computed } from 'vue'

const toastStore = useToastStore()

const toastClass = computed(() => ({
  'bg-green-500': toastStore.type === 'success',
  'bg-red-500': toastStore.type === 'error',
}))
</script>

<template>
  <Transition
    enter-active-class="transform ease-out duration-300 transition"
    enter-from-class="translate-y-2 opacity-0 sm:translate-y-0 sm:translate-x-2"
    enter-to-class="translate-y-0 opacity-100 sm:translate-x-0"
    leave-active-class="transition ease-in duration-100"
    leave-from-class="opacity-100"
    leave-to-class="opacity-0">
    <div
      v-if="toastStore.isVisible"
      class="fixed top-4 right-4 z-50 rounded-md p-4 text-white shadow-lg"
      :class="toastClass">
      <span>{{ toastStore.message }}</span>
      <button
        @click="toastStore.hideToast"
        class="p-1 hover:bg-black/10 rounded-full transition-colors">
        <XMarkIcon class="h-4 w-4 ml-1"></XMarkIcon>
      </button>
    </div>
  </Transition>
</template>
