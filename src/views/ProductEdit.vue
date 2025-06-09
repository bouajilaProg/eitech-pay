<template>
  <div>
    <main class="relative max-w-6xl mx-auto px-4 py-6">
      <!-- Buttons at the top -->
      <div class="flex space-x-4 mb-6 justify-start flex-row-reverse">
        <button @click="handleSubmit" class="px-4 py-2 bg-blue-600 text-white rounded hover:bg-blue-700">
          Submit
        </button>
        <button @click="handleCancel" class="px-4 py-2 bg-gray-300 text-gray-700 rounded hover:bg-gray-400">
          Cancel
        </button>
      </div>

      <ProductDetailsEdit />
      <TiersTable @open-modal="openModal" />
      <AddTierModal v-if="showModal" @close="showModal = false" @add-tier="handleAddTier" />
    </main>
  </div>
</template>

<script setup>
import "../global.css"

import { ref } from 'vue'
import { useRouter } from 'vue-router'

// Import components from ProductEdit folder
import ProductDetailsEdit from '../components/ProductEdit/ProductDetailsEdit.vue'
import TiersTable from '../components/ProductEdit/TiersTable.vue'
import AddTierModal from '../components/ProductEdit/AddTierModal.vue'

// Modal visibility state
const showModal = ref(false)
const router = useRouter()

function handleAddTier(newTier) {
  // Close modal after adding tier
  showModal.value = false
}

function openModal() {
  showModal.value = true
}

function handleSubmit() {
  // Simulate sending a submit request
  console.log("Submit button clicked: sending request...")
  // Add your actual request logic here
}

function handleCancel() {
  // Navigate to /products/1 on cancel
  router.push('/products/1')
}
</script>

<style scoped>
/* No edit-mode styles */
</style>
