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
          <tr @click="router.push('/products/1')" style="cursor: pointer">
            <td>prod_1</td>
            <td>
              <strong>subscription_1</strong><br />
            </td>
            <td>description_1</td>
          </tr>
          <tr @click="router.push('/products/2')" style="cursor: pointer">
            <td>prod_2</td>
            <td>
              <strong>licence_1</strong><br />
            </td>
            <td>description_2</td>
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
import { productService } from '@/services/product.service.ts'

const router = useRouter()
const products = ref([])
const showModal = ref(false)

async function loadProducts() {
  try {
    products.value = await productService.getAll()
    console.log('Products loaded:', products.value)
    products.value = products.value.map(p => ({
      ...p,
      route: `/products/${p.id}`
    }))
  } catch (error) {
    console.error('Failed to load products:', error)
  }
}

async function addProduct(newProductInput) {
  try {
    await productService.create(newProductInput);
    await loadProducts();
    showModal.value = false;
  } catch (error) {
    console.error('Failed to add product:', error);
  }
}

onMounted(loadProducts)
</script>

