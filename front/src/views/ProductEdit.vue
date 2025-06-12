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

      <ProductDetailsEdit ref="productComponentRef" />
      <OptionsTable @open-modal="openModal" />
      <AddTierModal v-if="showModal" @close="showModal = false" @add-tier="handleAddTier" />
    </main>
  </div>
</template>

<script setup>
import "../global.css"

import { ref } from 'vue'
import { useRouter, useRoute } from 'vue-router'

// Import components
import ProductDetailsEdit from '../components/ProductEdit/ProductDetailsEdit.vue'
import OptionsTable from '../components/ProductEdit/OptionsTable.vue'
import AddTierModal from '../components/ProductEdit/AddTierModal.vue'
import { productService } from '@/services/product.service.ts'

const showModal = ref(false)
const router = useRouter()
const route = useRoute()

// Ref for the child component
const productComponentRef = ref(null)

function openModal() {
  showModal.value = true
}

function handleAddTier(newTier) {
  showModal.value = false
}

async function handleSubmit() {
  try {
    if (!productComponentRef.value) {
      throw new Error('Product component not mounted')
    }

    const product = productComponentRef.value.product
    const productId = route.params.product_id

    console.log("Submitting product:", product)

    await productService.update(productId, {
      name: product.name,
      description: product.description,
      productType: parseInt(product.productType)
    })

    console.log("Product updated successfully")
    router.push(`/products/${productId}`)
  } catch (err) {
    console.error("Failed to update product:", err)
  }
}

function handleCancel() {
  router.push(`/products/${route.params.product_id}`)
}
</script>

<style scoped>
/* No edit-mode styles */
</style>
