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

        <!-- API Key Section -->
<div>
  <h2 class="text-xl font-medium mb-4">API Key</h2>
  <div class="space-y-4">
    <div class="relative">
      <label class="block text-sm font-medium text-gray-700 mb-1">Your API Key</label>
      <input
        :type="showApiKey ? 'text' : 'password'"
        v-model="apiKey"
        class="w-full pr-10 px-4 py-2 border border-gray-300 rounded focus:outline-none focus:ring-2 focus:ring-blue-500"
        readonly
      />
      <div class="absolute inset-y-0 right-3 top-6 flex items-center">
        <button
          type="button"
          @click="showApiKey = !showApiKey"
          class="text-gray-600 hover:text-gray-800"
        >
          <svg v-if="showApiKey" xmlns="http://www.w3.org/2000/svg" class="h-4 w-4" viewBox="0 0 20 20" fill="currentColor">
            <path d="M10 3C5 3 1.73 7.11 1 10c.73 2.89 4 7 9 7s8.27-4.11 9-7c-.73-2.89-4-7-9-7zm0 12a5 5 0 110-10 5 5 0 010 10z" />
          </svg>
          <svg v-else xmlns="http://www.w3.org/2000/svg" class="h-4 w-4" viewBox="0 0 24 24" fill="currentColor">
            <path
              fill-rule="evenodd"
              d="M3.22 3.22a.75.75 0 011.06 0l16.5 16.5a.75.75 0 01-1.06 1.06l-2.612-2.613A9.564 9.564 0 0112 19.5C7.305 19.5 3.473 16.165 2.03 12a9.57 9.57 0 013.93-4.68L3.22 4.28a.75.75 0 010-1.06zM15 11.25a3 3 0 00-3-3c-.238 0-.466.028-.684.081l3.603 3.603c.053-.218.081-.446.081-.684zM9 12a3 3 0 004.684 2.316l-4-4A2.99 2.99 0 009 12zm11.52 3.184c.585-.803.984-1.7 1.145-2.684C20.527 8.165 16.695 4.5 12 4.5c-.984 0-1.935.16-2.816.457l-.983-.983A9.578 9.578 0 0112 3c4.695 0 8.527 3.335 9.97 7.5a9.57 9.57 0 01-1.45 2.434l-.001.001-1.088-1.088a.75.75 0 011.06-1.06l1.03 1.03z"
              clip-rule="evenodd"
            />
          </svg>
        </button>
      </div>
    </div>
    <div class="pt-2">
      <button @click="regenerateApiKey" class="px-4 py-2 bg-blue-600 text-white rounded hover:bg-blue-700">
        Regenerate API Key
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

// API Key
const apiKey = ref('')
const showApiKey = ref(false)

function updatePassword() {
  if (newPassword.value !== confirmPassword.value) {
    alert('New passwords do not match')
    return
  }

  console.log('Updating password with:', {
    oldPassword: oldPassword.value,
    newPassword: newPassword.value
  })

  oldPassword.value = ''
  newPassword.value = ''
  confirmPassword.value = ''
}

function saveKonnectId() {
  console.log('Saving Konnect Pay ID:', konnectPayId.value)
}

function regenerateApiKey() {
  const newKey = `sk-${Math.random().toString(36).substring(2, 10)}`
  apiKey.value = newKey
  alert('New API Key generated.')
}
</script>

<style scoped>
/* Scoped styling if needed */
</style>
