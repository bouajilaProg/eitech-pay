<template>
  <div class="card mb-4 text">
    <div class="card-header d-flex justify-content-between align-items-center">
      <h3 class="card-title mb-0">Products</h3>
      <button id="chrt_dry_add_product" class="btn btn-primary " title="Add a new product to the catalog"
        data-bs-toggle="modal" data-bs-target="#newProductModal">
        New Product
      </button>
    </div>
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
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()

const products = ref([
  {
    id: 'chrt_dry_1',
    name: 'Product A',
    subtitle: 'High-quality product A',
    description: 'Essential for modern workflows, reliable and efficient.',
    route: '/products/1', // Adjust this route according to your route setup
  },
  {
    id: 'chrt_dry_2',
    name: 'Product B',
    subtitle: 'Reliable product B',
    description: 'Built with consistency in mind and performs at scale.',
    route: '/products/2',
  },
])
</script>
