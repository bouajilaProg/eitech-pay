<template>
  <div class="card mb-4">
    <div class="card-header flex justify-between items-center">
      <h3 class="card-title mb-0">Subscription Tiers</h3>
      <button
        class="btn btn-primary bg-[#30d5c8] px-4 py-2 rounded text-white hover:bg-[#2bc2b5]"
        @click="showAddModal = true"
      >
        Add Tier
      </button>
    </div>

    <div class="table-responsive mt-3">
      <table class="table table-vcenter card-table table-hover w-full">
        <thead>
          <tr>
            <th>Name</th>
            <th>Duration (days)</th>
            <th>Grace Period</th>
            <th>Price (TND)</th>
            <th class="text-end">Actions</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(tier, index) in subscriptionTiers" :key="index">
            <td>
              <input
                type="text"
                v-model="tier.tierName"
                class="w-full border border-gray-300 rounded px-2 py-1 focus:outline-none focus:ring-2 focus:ring-[#30d5c8]"
                @input="markDirty(index)"
              />
            </td>
            <td>
              <input
                type="number"
                v-model.number="tier.duration"
                min="1"
                class="w-full border border-gray-300 rounded px-2 py-1 focus:outline-none focus:ring-2 focus:ring-[#30d5c8]"
                @input="markDirty(index)"
              />
            </td>
            <td>
              <input
                type="text"
                v-model="tier.gracePeriod"
                class="w-full border border-gray-300 rounded px-2 py-1 focus:outline-none focus:ring-2 focus:ring-[#30d5c8]"
                @input="markDirty(index)"
              />
            </td>
            <td>
              <input
                type="number"
                v-model.number="tier.price"
                min="0"
                step="0.01"
                class="w-full border border-gray-300 rounded px-2 py-1 focus:outline-none focus:ring-2 focus:ring-[#30d5c8]"
                @input="markDirty(index)"
              />
            </td>
            <td class="text-end flex justify-end space-x-2">
              <button
                :disabled="!tier.dirty || tier.updating"
                @click="updateTier(tier, index)"
                class="p-1 transition-opacity duration-200 hover:opacity-80 disabled:opacity-40 disabled:cursor-not-allowed"
              >
                <CgPen class="w-5 h-5 text-blue-600" />
              </button>
              <button
                @click="openDeleteModal(index)"
                class="p-1 transition-opacity duration-200 hover:opacity-70"
              >
                <CgTrash class="w-5 h-5 text-red-600" />
              </button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>

    <!-- Delete Confirmation Modal -->
    <DeleteModal
      v-if="showDeleteModal"
      @close="showDeleteModal = false"
      @confirm="confirmDeleteTier"
    />

    <!-- Add Tier Modal -->
    <AddTierModal
      v-if="showAddModal"
      @close="showAddModal = false"
      @add-tier="handleAddTier"
      @tier-created="reloadTiers"
    />
  </div>
</template>

<script setup>
import { ref, watch } from 'vue'
import DeleteModal from '../ProductPage/DeleteModal.vue'
import AddTierModal from './AddTierModal.vue'
import { CgTrash } from 'vue-icons-plus/cg'
import { CgPen } from 'vue-icons-plus/cg'
import { subscriptionService } from '@/services/subscription.service.ts'
import { useRoute } from 'vue-router'

const route = useRoute()

// Props
const props = defineProps({
  tiers: {
    type: Array,
    required: true
  }
})

// Internal state
const subscriptionTiers = ref([])

watch(
  () => props.tiers,
  (newTiers) => {
    subscriptionTiers.value = newTiers.map(tier => ({
      ...tier,
      dirty: false,
      updating: false
    }))
  },
  { immediate: true }
)

const showAddModal = ref(false)
const showDeleteModal = ref(false)
const tierToDeleteIndex = ref(null)

// Mark tier dirty when edited
function markDirty(index) {
  subscriptionTiers.value[index].dirty = true
}

// Update a specific tier
async function updateTier(tier, index) {
  if (!tier.tierId) return

  tier.updating = true
  try {
    await subscriptionService.updateTier(tier.tierId, {
      tierId: tier.tierId,
      subscriptionId: tier.subscriptionId,
      description: tier.description,
      tierName: tier.tierName,
      duration: tier.duration,
      gracePeriod: tier.gracePeriod,
      price: tier.price
    })
    tier.dirty = false
  } catch (err) {
    console.error('Update failed:', err)
  } finally {
    tier.updating = false
  }
}

async function reloadTiers() {
  const updated = await subscriptionService.getTiersBySubscriptionId(route.params.product_id)
  subscriptionTiers.value = updated.map(tier => ({
    ...tier,
    dirty: false,
    updating: false,
  }))
}

// Deletion
function openDeleteModal(index) {
  tierToDeleteIndex.value = index
  showDeleteModal.value = true
}

async function confirmDeleteTier() {
  if (tierToDeleteIndex.value !== null) {
    const tier = subscriptionTiers.value[tierToDeleteIndex.value]
    try {
      await subscriptionService.deleteTier(tier.tierId)
      subscriptionTiers.value.splice(tierToDeleteIndex.value, 1)
    } catch (err) {
      console.error('Delete failed:', err)
    }
    tierToDeleteIndex.value = null
  }
  showDeleteModal.value = false
}

// Add new tier
function handleAddTier(newTier) {
  subscriptionTiers.value.push({
    ...newTier,
    dirty: false,
    updating: false
  })
  showAddModal.value = false
}
</script>
