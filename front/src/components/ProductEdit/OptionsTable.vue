<template>
    <div class="card mb-4">
      <div class="card-header flex justify-between items-center">
        <h3 class="card-title mb-0">License Options</h3>
        <button class="btn btn-primary bg-[#30d5c8] px-4 py-2 rounded text-white hover:bg-[#2bc2b5]"
          @click="showAddModal = true">
          Add Option
        </button>
      </div>
  
      <div class="table-responsive mt-3">
        <table class="table table-vcenter card-table table-hover w-full">
          <thead>
            <tr>
              <th>ID</th>
              <th>Name</th>
              <th>Description</th>
              <th>Price (TND)</th>
              <th class="text-end">Actions</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(option, index) in licenseOptions" :key="index">
              <td>
                <input type="text" v-model="option.optionId"
                  class="w-full border border-gray-300 rounded px-2 py-1 focus:outline-none focus:ring-2 focus:ring-[#30d5c8]" />
              </td>
              <td>
                <input type="text" v-model="option.optionName"
                  class="w-full border border-gray-300 rounded px-2 py-1 focus:outline-none focus:ring-2 focus:ring-[#30d5c8]" />
              </td>
              <td>
                <input type="text" v-model="option.description"
                  class="w-full border border-gray-300 rounded px-2 py-1 focus:outline-none focus:ring-2 focus:ring-[#30d5c8]" />
              </td>
              <td>
                <input type="number" v-model.number="option.price" min="0"
                  class="w-full border border-gray-300 rounded px-2 py-1 focus:outline-none focus:ring-2 focus:ring-[#30d5c8]" />
              </td>
              <td class="text-end">
                <button @click="openDeleteModal(index)" class="p-1 transition-opacity duration-200 hover:opacity-70">
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
  
      <!-- Add Option Modal (optional) -->
      <AddLicenseOptionModal
        v-if="showAddModal"
        @close="showAddModal = false"
        @add-option="handleAddOption"
      />
    </div>
  </template>
  
  <script setup>
  import { ref } from 'vue'
  import DeleteModal from '../ProductPage/DeleteModal.vue'
  import AddLicenseOptionModal from './AddLicenseOptionModal.vue'
  import { CgTrash } from 'vue-icons-plus/cg'
  
  const licenseOptions = ref([
    {
      optionId: 1,
      licenseId: 1,
      optionName: 'Multi-user Support',
      description: 'Allows access for up to 5 users.',
      price: 49.99,
      createdAt: new Date().toISOString(),
      lastUpdateAt: new Date().toISOString(),
      isArchived: false
    },
    {
      optionId: 2,
      licenseId: 1,
      optionName: 'Priority Support',
      description: 'Includes 24/7 premium support.',
      price: 29.99,
      createdAt: new Date().toISOString(),
      lastUpdateAt: new Date().toISOString(),
      isArchived: false
    }
  ])
  
  const showAddModal = ref(false)
  const showDeleteModal = ref(false)
  const optionToDeleteIndex = ref(null)
  
  function openDeleteModal(index) {
    optionToDeleteIndex.value = index
    showDeleteModal.value = true
  }
  
  function confirmDeleteOption() {
    if (optionToDeleteIndex.value !== null) {
      licenseOptions.value.splice(optionToDeleteIndex.value, 1)
      optionToDeleteIndex.value = null
    }
    showDeleteModal.value = false
  }
  
  function handleAddOption(newOption) {
    licenseOptions.value.push(newOption)
    showAddModal.value = false
  }
  </script>
  