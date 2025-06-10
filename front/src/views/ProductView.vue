<template>
  <div>
    <main class="relative max-w-6xl mx-auto px-4 py-6">
      <div class="d-flex justify-content-end mb-4">
        <button @click="showModal = true" class="btn btn-danger me-2">
          Delete
        </button>
        <router-link to="/products/1/edit" class="btn btn-dark">
          Edit
        </router-link>
      </div>
      <ProductDetails />
      <RevenuChart />
      <OptionsTable />
    </main>
    <DeleteModal v-if="showModal" @close="showModal = false" @confirm="confirmDelete" />
  </div>
</template>

<script setup>
import "../global.css"

import ProductDetails from '../components/ProductPage/ProductDetails.vue'
import TiersTable from '../components/ProductPage/TiersTable.vue'
import OptionsTable from '../components/ProductPage/OptionsTable.vue'
import EditControls from '../components/ProductPage/EditControls.vue'
import RevenuChart from '../components/ProductPage/RevenueChart.vue'
import DeleteModal from '../components/ProductPage/DeleteModal.vue'
import { productService } from '@/services/product.service.ts'
import { useRoute, useRouter } from 'vue-router'

import { ref } from 'vue'

const showModal = ref(false)
const route = useRoute()
const router = useRouter()

function confirmDelete() {
  showModal.value = false
  deleteProduct()
}

async function deleteProduct() {
  const id = route.params.product_id

  try {
    await productService.delete(id)
    console.log("Product deleted successfully")
    router.push('/dashboard')
  } catch (err) {
    console.error("Failed to delete product:", err)
  }
}

</script>

<style scoped>
/* No modal styles needed, using Tabler UI classes */
</style>
