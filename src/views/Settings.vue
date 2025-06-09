<template>
  <div>
    <main class="relative max-w-6xl mx-auto px-4 py-6">
      <h1 class="text-2xl font-semibold mb-6">Admin Settings</h1>

      <div class="space-y-12">
        <!-- Change Password Section -->
        <div>
          <h2 class="text-xl font-medium mb-4">Change Password</h2>
          <div class="space-y-4">
            <div>
              <label class="block text-sm font-medium text-gray-700 mb-1">Old Password</label>
              <input
                type="password"
                v-model="oldPassword"
                class="w-full px-4 py-2 border border-gray-300 rounded focus:outline-none focus:ring-2 focus:ring-blue-500"
              />
            </div>
            <div>
              <label class="block text-sm font-medium text-gray-700 mb-1">New Password</label>
              <input
                type="password"
                v-model="newPassword"
                class="w-full px-4 py-2 border border-gray-300 rounded focus:outline-none focus:ring-2 focus:ring-blue-500"
              />
            </div>
            <div>
              <label class="block text-sm font-medium text-gray-700 mb-1">Confirm New Password</label>
              <input
                type="password"
                v-model="confirmPassword"
                class="w-full px-4 py-2 border border-gray-300 rounded focus:outline-none focus:ring-2 focus:ring-blue-500"
              />
            </div>
            <div class="pt-2">
              <button @click="updatePassword" class="px-4 py-2 bg-blue-600 text-white rounded hover:bg-blue-700">
                Update Password
              </button>
            </div>
          </div>
        </div>

        <!-- Konnect Pay ID Section -->
        <div>
          <h2 class="text-xl font-medium mb-4">Konnect Pay</h2>
          <div class="space-y-4">
            <div>
              <label class="block text-sm font-medium text-gray-700 mb-1">Konnect Pay ID</label>
              <input
                type="text"
                v-model="konnectPayId"
                class="w-full px-4 py-2 border border-gray-300 rounded focus:outline-none focus:ring-2 focus:ring-blue-500"
              />
            </div>
            <div class="pt-2">
              <button @click="saveKonnectId" class="px-4 py-2 bg-blue-600 text-white rounded hover:bg-blue-700">
                Save Konnect ID
              </button>
            </div>
          </div>
        </div>
      </div>
    </main>
  </div>
</template>

<script setup>
import { ref } from 'vue'

// Password fields
const oldPassword = ref('')
const newPassword = ref('')
const confirmPassword = ref('')

// Konnect Pay ID
const konnectPayId = ref('')

// Password update logic
function updatePassword() {
  if (newPassword.value !== confirmPassword.value) {
    alert('New passwords do not match')
    return
  }

  console.log('Updating password with:', {
    oldPassword: oldPassword.value,
    newPassword: newPassword.value
  })

  // Call API to change password here
  // Reset fields on success
  oldPassword.value = ''
  newPassword.value = ''
  confirmPassword.value = ''
}

// Konnect Pay ID save logic
function saveKonnectId() {
  console.log('Saving Konnect Pay ID:', konnectPayId.value)

  // Call API to save Konnect ID here
}
</script>

<style scoped>
/* Scoped styling if needed */
</style>
