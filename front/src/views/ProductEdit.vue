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

      <ProductDetailsEdit ref="productComponentRef" v-if="product" :product="product" />
      <OptionsTable v-if="product" @open-modal="openModal" />
      
      <AddTierModal v-if="showModal" @close="showModal = false" @add-tier="handleAddTier" />
    </main>
  </div>
</template>

<script setup>
import "../global.css"

import { ref, onMounted } from 'vue'
import { useRouter, useRoute } from 'vue-router'

import ProductDetailsEdit from '../components/ProductEdit/ProductDetailsEdit.vue'
import OptionsTable from '../components/ProductEdit/OptionsTable.vue'
import AddTierModal from '../components/ProductEdit/AddTierModal.vue'

import { licenceService } from '@/services/licence.service.ts'
import { subscriptionService } from '@/services/subscription.service.ts'

const showModal = ref(false)
const router = useRouter()
const route = useRoute()

const productComponentRef = ref(null)
const product = ref(null)

function openModal() {
  showModal.value = true
}

function handleAddTier(newTier) {
  showModal.value = false
  // Optionally refresh tiers or product details if needed
}

onMounted(async () => {
  const productId = route.params.product_id
  const type = route.query.type

  if (!productId || !type) {
    console.error("Missing product ID or type in query")
    router.push('/dashboard')
    return
  }

  try {
    if (type === 'licence') {
      product.value = await licenceService.getLicenceById(productId)
    } else if (type === 'subscription') {
      product.value = await subscriptionService.getSubscriptionById(productId)
    } else {
      console.error("Unknown product type:", type)
      router.push('/dashboard')
    }
  } catch (error) {
    console.error("Failed to load product for edit:", error)
    router.push('/dashboard')
  }
})

async function handleSubmit() {
  try {
    if (!productComponentRef.value) {
      throw new Error('Product component not mounted')
    }

    const updatedProduct = productComponentRef.value.product
    const productId = route.params.product_id
    const type = route.query.type

    if (type === 'licence') {
      console.log("Updating licence with ID:", updatedProduct);
      await licenceService.updateLicence(productId, updatedProduct)
    } else if (type === 'subscription') {
      await subscriptionService.updateSubscription(productId, updatedProduct)
    } else {
      throw new Error("Unknown product type on submit")
    }

    console.log("Product updated successfully")
    router.push(`/products/${productId}?type=${type}`)
  } catch (err) {
    console.error("Failed to update product:", err)
  }
}

function handleCancel() {
  const productId = route.params.product_id
  const type = route.query.type
  router.push(`/products/${productId}?type=${type}`)
}
</script>

<style scoped>
/* No edit-mode styles */
</style>
