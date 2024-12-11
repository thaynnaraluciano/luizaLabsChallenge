<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useRouter } from 'vue-router';
import { useToastStore } from '@/stores/toast';

const router = useRouter();
const toastStore = useToastStore()

const confirmEmail = async (verificationCode: string) => {
	try {
		const response = await fetch(`http://localhost:4000/api/v1/user/confirmEmail`, {
				method: 'PUT',
				headers: {
						'Content-Type': 'application/json'
				},
				body: JSON.stringify({
						verificationCode: verificationCode
				}),
		});

		const data = await response.json()

		if (response.ok)
			router.push({ name: 'EmailConfirmed' })
		else
			toastStore.showToast(data.Message, 'error')
	} catch (error) {
		toastStore.showToast('Erro ao confirmar email', 'error')
  }
};

onMounted(() => {
  const urlParams = new URLSearchParams(window.location.search);
  const verificationCode = urlParams.get('verificationCode');

  // if (!verificationCode) {
  //   message.value = 'Código de verificação não informado.';
  //   return;
  // }

  confirmEmail(verificationCode!);
});

</script>

<template>
  <main class="flex bg-gray-100 min-h-screen">
    <div class="md:container md:mx-auto">
      <div class="px-6 py-8 bg-white mt-16 rounded-md shadow-sm">
        <h1 class="text-xl text-center text-gray-700 font-semibold mb-6">
          Verificando Token
        </h1>
      </div>
    </div>
  </main>
</template>
