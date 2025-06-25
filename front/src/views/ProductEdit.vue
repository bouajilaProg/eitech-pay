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
      <OptionsTable v-if="type === 'licence'" :options="options" />
      <TiersTable v-if="type === 'subscription'" :tiers="tiers" />
      
      <AddLicenceOptionModal v-if="route.query.type === 'licence' && showModal" 
                             @close="showModal = false" 
                             @add-option="handleAddOption" />
      <AddTierModal v-if="route.query.type === 'subscription' && showModal" 
                    @close="showModal = false" 
                    @add-tier="handleAddTier" />
    </main>
  </div>
</template>

<script setup>
import "../global.css"

import { ref, onMounted } from 'vue'
import { useRouter, useRoute } from 'vue-router'

import ProductDetailsEdit from '../components/ProductEdit/ProductDetailsEdit.vue'
import OptionsTable from '../components/ProductEdit/OptionsTable.vue'
import TiersTable from '../components/ProductEdit/TiersTable.vue'
import AddLicenceOptionModal from '../components/ProductEdit/AddLicenseOptionModal.vue'
import AddTierModal from '../components/ProductEdit/AddTierModal.vue'

import { licenceService } from '@/services/licence.service.ts'
import { subscriptionService } from '@/services/subscription.service.ts'

const showModal = ref(false)
const router = useRouter()
const route = useRoute()

const productComponentRef = ref(null)
const product = ref(null)
const tiers = ref([])
const options = ref([])

const productId = route.params.product_id
const type = route.query.type

function openModal() {
  showModal.value = true
}

function handleAddTier(newTier) {
  showModal.value = false
  // Optional: re-fetch tiers
}

function handleAddOption(newOption) {
  showModal.value = false
  // Optional: re-fetch options
}

onMounted(async () => {
  try {
    if (!productId || !type) {
      console.error("Missing product ID or type in query")
      router.push('/dashboard')
      return
    }

    if (type === 'licence') {
      product.value = await licenceService.getLicenceById(productId)
      options.value = await licenceService.getOptionsByLicenceId(productId)
      tiers.value = []
    } else if (type === 'subscription') {
      product.value = await subscriptionService.getSubscriptionById(productId)
      tiers.value = await subscriptionService.getTiersBySubscriptionId(productId)
      options.value = []
    } else {
      console.error("Unknown product type:", type)
      router.push('/dashboard')
    }

    console.log("Product loaded:", product.value)
    console.log("Tiers:", tiers.value)
    console.log("Options:", options.value)
  } catch (error) {
    console.error("Failed to load product for edit:", error)
    router.push('/dashboard')
  }
})

async function handleSubmit() {
  try {
    if (!productComponentRef.value) throw new Error('Product component not mounted')

    const updatedProduct = productComponentRef.value.product

    if (type === 'licence') {
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
  router.push(`/products/${productId}?type=${type}`)
}
</script>
