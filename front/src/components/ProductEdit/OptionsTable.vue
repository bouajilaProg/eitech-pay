<template>
  <div class="card mb-4">
    <div class="card-header flex justify-between items-center">
      <h3 class="card-title mb-0">License Options</h3>
      <button
        class="btn btn-primary bg-[#30d5c8] px-4 py-2 rounded text-white hover:bg-[#2bc2b5]"
        @click="showAddModal = true"
      >
        Add Option
      </button>
    </div>

    <div class="table-responsive mt-3">
      <table class="table table-vcenter card-table table-hover w-full">
        <thead>
          <tr>
            <th>Name</th>
            <th>Description</th>
            <th>Price (TND)</th>
            <th class="text-end">Actions</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(option, index) in licenseOptions" :key="index">
            
            <td>
              <input
                type="text"
                v-model="option.optionName"
                class="w-full border border-gray-300 rounded px-2 py-1 focus:outline-none focus:ring-2 focus:ring-[#30d5c8]"
                @input="markDirty(index)"
              />
            </td>
            <td>
              <input
                type="text"
                v-model="option.description"
                class="w-full border border-gray-300 rounded px-2 py-1 focus:outline-none focus:ring-2 focus:ring-[#30d5c8]"
                @input="markDirty(index)"
              />
            </td>
            <td>
              <input
                type="number"
                v-model.number="option.price"
                min="0"
                class="w-full border border-gray-300 rounded px-2 py-1 focus:outline-none focus:ring-2 focus:ring-[#30d5c8]"
                @input="markDirty(index)"
              />
            </td>
            <td class="text-end flex justify-end space-x-2">
              <button
                :disabled="!option.dirty || option.updating"
                @click="updateOption(option, index)"
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
      @confirm="confirmDeleteOption"
    />

    <!-- Add Option Modal -->
    <AddLicenseOptionModal
      v-if="showAddModal"
      @close="showAddModal = false"
      @add-option="handleAddOption"
      @option-created="reloadOptions"
    />
  </div>
</template>

<script setup>
import { ref, watch } from 'vue'
import DeleteModal from '../ProductPage/DeleteModal.vue'
import AddLicenseOptionModal from './AddLicenseOptionModal.vue'
import { CgTrash } from 'vue-icons-plus/cg'
import { CgPen } from 'vue-icons-plus/cg'
import { licenceService } from '@/services/licence.service.ts'
import { useRoute } from 'vue-router'
const route = useRoute()

// Props
const props = defineProps({
  options: {
    type: Array,
    required: true
  }
})

// Internal state
const licenseOptions = ref([])

watch(
  () => props.options,
  (newOptions) => {
    licenseOptions.value = newOptions.map(option => ({
      ...option,
      dirty: false,
      updating: false
    }))
  },
  { immediate: true }
)

const showAddModal = ref(false)
const showDeleteModal = ref(false)
const optionToDeleteIndex = ref(null)

// Mark option dirty when edited
function markDirty(index) {
  licenseOptions.value[index].dirty = true
}

// Update a specific option
async function updateOption(option, index) {
  if (!option.optionId) return

  option.updating = true
  try {
    await licenceService.updateOption(option.optionId, {
      optionId: option.optionId,
      licenceId: option.licenceId,
      optionName: option.optionName,
      description: option.description,
      price: option.price
    })
    option.dirty = false
  } catch (err) {
    console.error('Update failed:', err)
  } finally {
    option.updating = false
  }
}

async function reloadOptions() {
  const updated = await licenceService.getOptionsByLicenceId(route.params.product_id)
  licenseOptions.value = updated.map(option => ({
    ...option,
    dirty: false,
    updating: false,
  }))
}

// Deletion
function openDeleteModal(index) {
  optionToDeleteIndex.value = index
  showDeleteModal.value = true
}

async function confirmDeleteOption() {
  if (optionToDeleteIndex.value !== null) {
    const option = licenseOptions.value[optionToDeleteIndex.value]
    try {
      await licenceService.deleteOption(option.optionId)
      licenseOptions.value.splice(optionToDeleteIndex.value, 1)
    } catch (err) {
      console.error('Delete failed:', err)
    }
    optionToDeleteIndex.value = null
  }
  showDeleteModal.value = false
}

// Add new option
function handleAddOption(newOption) {
  licenseOptions.value.push({
    ...newOption,
    dirty: false,
    updating: false
  })
  showAddModal.value = false
}
</script>
