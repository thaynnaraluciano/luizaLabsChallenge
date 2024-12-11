<script setup lang="ts">
import { computed } from 'vue'
import { ShieldCheckIcon } from '@heroicons/vue/24/outline';

const props = defineProps<{
  password: string,
}>()


const passwordLengthCheck = computed(() => props.password.length >= 8)
const passwordUppercaseAndLowerCaseCheck = computed(() => /[a-z]/.test(props.password) && /[A-Z]/.test(props.password))
const passwordNumberCheck = computed(() => /[0-9]/.test(props.password))
const passwordSpecialCharacterCheck = computed(() => /[!@#$%^&*()~_+\-=\[\]{};':"\\|,.<>\/?]+/.test(props.password))
</script>

<template>
  <div class="grid grid-cols-2 gap-2 mt-2">
    <p
      class="text-sm"
      :class="passwordUppercaseAndLowerCaseCheck ? 'text-green-500' : 'text-red-600'">
      <ShieldCheckIcon class="h-4 w-4 inline" ></ShieldCheckIcon>
      Letra(s) maiúscula e minúscula
    </p>
    <p
      class="text-sm"
      :class="passwordNumberCheck ? 'text-green-500' : 'text-red-600'">
      <ShieldCheckIcon class="h-4 w-4 inline" ></ShieldCheckIcon>
      Número(s)
    </p>
    <p
      class="text-sm"
      :class="passwordSpecialCharacterCheck ? 'text-green-500' : 'text-red-600'">
      <ShieldCheckIcon class="h-4 w-4 inline" ></ShieldCheckIcon>
      Caractere especial
    </p>
    <p
      class="text-sm"
      :class="passwordLengthCheck ? 'text-green-500' : 'text-red-600'">
      <ShieldCheckIcon class="h-4 w-4 inline" ></ShieldCheckIcon>
      8 ou mais caracteres
    </p>
  </div>
</template>
