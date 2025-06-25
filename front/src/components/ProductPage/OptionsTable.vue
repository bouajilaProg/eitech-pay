<template>
  <div class="bg-white shadow rounded-lg overflow-hidden card">
    <div class="px-6 py-4 border-b border-gray-200 flex justify-between items-center">
      <h3 class="text-lg font-semibold text-gray-800">Options</h3>
    </div>

    <div class="overflow-x-auto">
      <table class="min-w-full divide-y divide-gray-200 text-sm text-gray-700">
        <thead class="bg-gray-50">
          <tr>
            <th class="px-6 py-3 text-left font-semibold">ID</th>
            <th class="px-6 py-3 text-left font-semibold">Name</th>
            <th class="px-6 py-3 text-left font-semibold">Description</th>
            <th class="px-6 py-3 text-left font-semibold">Price</th>
          </tr>
        </thead>
        <tbody class="divide-y divide-gray-100">
          <tr v-for="option in options" :key="option.optionId" class="hover:bg-gray-50">
            <td class="px-6 py-3 text-gray-600">{{ option.optionId }}</td>
            <td class="px-6 py-3 text-gray-600">{{ option.descripiton }}</td>
            <td class="px-6 py-3 text-gray-600">{{ option.optionName }}</td>
            <td class="px-6 py-3 text-gray-600">{{ option.price }}</td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script setup>
defineProps({
  options: {
    type: Array,
    required: true
  }
})
</script>
