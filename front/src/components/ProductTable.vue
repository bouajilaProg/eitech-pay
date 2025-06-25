<template>
  <div class="card mb-4 text">
    <!-- Header -->
    <div class="card-header d-flex justify-content-between align-items-center">
      <h3 class="card-title mb-0">Products</h3>
      <button
        id="chrt_dry_add_product"
        class="btn btn-primary"
        title="Add a new product to the catalog"
        @click="showModal = true"
      >
        New Product
      </button>
    </div>

    <!-- Products Table -->
    <div class="table-responsive">
      <table class="table table-vcenter card-table table-hover">
        <thead>
          <tr>
            <th>ID</th>
            <th>Name</th>
            <th>Description</th>
          </tr>
        </thead>
        <tbody>
          <tr
            v-for="product in products"
            :key="product.id"
            :style="{ cursor: 'pointer' }"
            @click="router.push(product.route)"
          >
            <td>{{ product.id }}</td>
            <td><strong>{{ product.name }}</strong></td>
            <td>{{ product.description }}</td>
          </tr>
        </tbody>
      </table>
    </div>

    <!-- Add Product Modal -->
    <AddProductModal
      v-if="showModal"
      @close="showModal = false"
      @submit="addProduct"
    />
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import AddProductModal from '@comp/AddProductModal.vue'

import { licenceService } from '@/services/licence.service.ts'
import { subscriptionService } from '@/services/subscription.service.ts'

const router = useRouter()
const products = ref([])
const showModal = ref(false)

async function loadProducts() {
  try {
    const [licences, subscriptions] = await Promise.all([
      licenceService.getAllLicences(),
      subscriptionService.getAllSubscriptions()
    ])

    products.value = [
      ...licences.map(l => ({
        id: l.licenceId,
        name: l.licenceName,
        description: l.description,
        type: 'licence',
        route: `/products/${l.licenceId}?type=licence`
      })),
      ...subscriptions.map(s => ({
        id: s.subscriptionId,
        name: s.subscriptionName,
        description: s.description,
        type: 'subscription',
        route: `/products/${s.subscriptionId}?type=subscription`
      }))
    ]
  } catch (error) {
    console.error('Failed to load products:', error)
  }
}

async function addProduct(newProductInput) {
  try {
    // You may want to branch logic depending on whether it's a licence or subscription
    await productService.create(newProductInput)
    await loadProducts()
    showModal.value = false
  } catch (error) {
    console.error('Failed to add product:', error)
  }
}

onMounted(loadProducts)
</script>

