<template>
  <div>
    <main class="relative max-w-6xl mx-auto px-4 py-6">
      <div class="d-flex justify-content-end mb-4">
        <button @click="showModal = true" class="btn btn-danger me-2">
          Delete
        </button>
        <router-link :to="`/products/${productId}/edit?type=${type}`" class="btn btn-dark">
          Edit
        </router-link>
      </div>

      <ProductDetails v-if="product" :product="product" />
      <RevenuChart v-if="product" :product="product" />
      <br />
      <TiersTable v-if="type === 'subscription'" :tiers="tiers" />
      <br />
      <OptionsTable v-if="type === 'licence'" :options="options" />
    </main>

    <DeleteModal
      v-if="showModal"
      @close="showModal = false"
      @confirm="confirmDelete"
    />
  </div>
</template>

<script setup>
import "../global.css"

import { ref, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'

import ProductDetails from '../components/ProductPage/ProductDetails.vue'
import TiersTable from '../components/ProductPage/TiersTable.vue'
import OptionsTable from '../components/ProductPage/OptionsTable.vue'
import RevenuChart from '../components/ProductPage/RevenueChart.vue'
import DeleteModal from '../components/ProductPage/DeleteModal.vue'

import { licenceService } from '@/services/licence.service.ts'
import { subscriptionService } from '@/services/subscription.service.ts'

const route = useRoute()
const router = useRouter()

const showModal = ref(false)
const product = ref(null)
const tiers = ref([])
const options = ref([])

const productId = route.params.product_id || route.params.id
const type = route.query.type || 'licence' // default to 'licence' if not provided

onMounted(async () => {
  try {
    if (!productId) throw new Error("Missing product ID")

    if (type === 'licence') {
      // Fetch licence details
      product.value = await licenceService.getLicenceById(productId)

      // Fetch licence options
      options.value = await licenceService.getOptionsByLicenceId(productId)

      console.log("Options loaded:", options.value, "for licence ID:", productId)
      console.log("Product loaded:", product.value)

      tiers.value = [] // clear tiers, irrelevant for licence
    } else if (type === 'subscription') {
      // Fetch subscription details
      product.value = await subscriptionService.getSubscriptionById(productId)

      // Fetch tiers for subscription
      tiers.value = await subscriptionService.getTiersBySubscriptionId(productId)

      console.log("Tiers loaded:", tiers.value, "for subscription ID:", productId)

      options.value = [] // clear options, irrelevant for subscription
    } else {
      throw new Error(`Unknown product type: ${type}`)
    }
  } catch (err) {
    console.error("Failed to load product:", err)
    router.push('/dashboard')
  }
})

function confirmDelete() {
  showModal.value = false
  deleteProduct()
}

async function deleteProduct() {
  try {
    if (type === 'licence') {
      await licenceService.deleteLicence(productId)
    } else if (type === 'subscription') {
      await subscriptionService.deleteSubscription(productId)
    } else {
      throw new Error(`Unknown product type: ${type}`)
    }
    router.push('/dashboard')
  } catch (err) {
    console.error("Failed to delete product:", err)
  }
}
</script>

<style scoped>
/* Using Tabler UI classes; no extra styling needed */
</style>
