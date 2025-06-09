<template>
  <div>
    <main class="relative max-w-6xl mx-auto px-4 py-6">

      <div class="d-flex justify-content-end mb-4">
        <router-link to="/products/1/edit" class="text-white btn btn-dark">
          Edit
        </router-link>
      </div>
      <ProductDetails />
      <RevenuChart />
      <TiersTable />
    </main>
  </div>
</template>

<script setup>
import "../global.css"

import ProductDetails from '../components/ProductPage/ProductDetails.vue'
import TiersTable from '../components/ProductPage/TiersTable.vue'
import EditControls from '../components/ProductPage/EditControls.vue'
import RevenuChart from '../components/ProductPage/RevenueChart.vue'
</script>

<style scoped>
/* Removed .edit-mode styles */
</style>
