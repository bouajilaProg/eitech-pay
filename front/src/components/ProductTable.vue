<template>
  <div class="card mb-4 text">
    <!-- Header with Add Product Button -->
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
          <tr v-for="product in products" :key="product.id" @click="router.push(product.route)" style="cursor: pointer">
            <td>{{ product.id }}</td>
            <td>
              <strong>{{ product.name }}</strong><br />
              <span class="text-secondary">{{ product.subtitle }}</span>
            </td>
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
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import AddProductModal from '@comp/AddProductModal.vue'
import { productsData } from '../temp-data.ts'

const router = useRouter()
const products = ref([...productsData])
const showModal = ref(false)

function addProduct(newProduct) {
  products.value.push({
    ...newProduct,
    subtitle: newProduct.type === 'license' ? 'License' : 'Subscription',
    route: `/products/${newProduct.id}`
  })
}
</script>
