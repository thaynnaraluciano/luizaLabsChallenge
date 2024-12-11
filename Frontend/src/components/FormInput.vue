<script setup lang="ts">
defineProps<{
  modelValue: string,
  label: string,
  type?: string,
  id: string,
  name: string,
  required?: boolean,
  error?: string,
}>()
</script>

<template>
  <label :for="id" class="block text-sm font-medium text-gray-700">{{ label }}</label>
  <input
    :value="modelValue"
    @input="$emit('update:modelValue', ($event.target as HTMLInputElement).value)"
    :type="type || 'text'"
    :id="id"
    :name="name"
    :required="required"
    class="mt-1 p-2 block w-full border border-gray-300 rounded-md shadow-sm focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
    :class="[
      error ? 'border-red-600 text-red-500 focus:ring-red-500 focus:border-red-500' : ''
    ]" />
  <span v-if="error" class="mt-1 text-xs text-red-600">{{ error }}</span>
</template>
