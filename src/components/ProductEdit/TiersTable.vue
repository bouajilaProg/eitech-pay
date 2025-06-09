<template>
  <div class="card mb-4">
    <div class="card-header flex justify-between items-center">
      <h3 class="card-title mb-0">Tiers</h3>
      <button class="btn btn-primary bg-[#30d5c8] px-4 py-2 rounded text-white hover:bg-[#2bc2b5]"
        @click="showModal = true">
        Add Tier
      </button>
    </div>

    <div class="table-responsive mt-3">
      <table class="table table-vcenter card-table table-hover w-full">
        <thead>
          <tr>
            <th>ID</th>
            <th>Name</th>
            <th>Duration</th>
            <th>Price (TND)</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(tier, index) in tiers" :key="index">
            <td>
              <input type="text" v-model="tier.id"
                class="w-full border border-gray-300 rounded px-2 py-1 focus:outline-none focus:ring-2 focus:ring-[#30d5c8]" />
            </td>
            <td>
              <input type="text" v-model="tier.name"
                class="w-full border border-gray-300 rounded px-2 py-1 focus:outline-none focus:ring-2 focus:ring-[#30d5c8]" />
            </td>
            
            <td>
              <input type="text" v-model="tier.duration"
                class="w-full border border-gray-300 rounded px-2 py-1 focus:outline-none focus:ring-2 focus:ring-[#30d5c8]" />
            </td>
            <td>
              <input type="number" v-model.number="tier.price"
                class="w-full border border-gray-300 rounded px-2 py-1 focus:outline-none focus:ring-2 focus:ring-[#30d5c8]"
                min="0" />
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>

  <!-- Add Tier Modal -->
  <AddTierModal v-if="showModal" @close="showModal = false" @add-tier="handleAddTier" />
</template>

<script setup>
import { ref } from 'vue'
import AddTierModal from './AddTierModal.vue'
import { productsData } from '../../temp-data.ts'

const tiers = ref(
  productsData.find(p => p.id === 'learnito_web_1')?.tiers || [
    { id: 'tier_1', name: 'Basic', price: 10, duration: '1 month' },
    { id: 'tier_2', name: 'Standard', price: 25, duration: '3 months' },
    { id: 'tier_3', name: 'Premium', price: 50, duration: '6 months' }
  ]
)

const showModal = ref(false)

function handleAddTier(newTier) {
  tiers.value.push(newTier)
  showModal.value = false
}
</script>
