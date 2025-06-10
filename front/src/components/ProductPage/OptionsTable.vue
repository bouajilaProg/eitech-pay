<template>
    <div class="bg-white shadow rounded-lg overflow-hidden">
      <div class="px-6 py-4 border-b border-gray-200 flex justify-between items-center">
        <h3 class="text-lg font-semibold text-gray-800">Options</h3>
      </div>
  
      <div class="overflow-x-auto">
        <table class="min-w-full divide-y divide-gray-200 text-sm text-gray-700">
          <thead class="bg-gray-50">
            <tr>
              <th class="px-6 py-3 text-left font-semibold">ID</th>
              <th class="px-6 py-3 text-left font-semibold">Name</th>
              <th class="px-6 py-3 text-left font-semibold">Price</th>
            </tr>
          </thead>
          <tbody class="divide-y divide-gray-100">
            <tr v-for="(option, index) in options" :key="index" class="hover:bg-gray-50">
              <td class="px-6 py-3 text-gray-600">{{ option.id }}</td>
              <td class="px-6 py-3 text-gray-600">{{ option.name }}</td>
              <td class="px-6 py-3 text-gray-600">{{ option.price }}</td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </template>
  
  <script setup>
  import { ref } from 'vue'
  import { productsData } from '../../temp-data.ts'
  
  const options = ref(
    productsData.find(p => p.id === 'learnito_web_1')?.options || [
      { id: 'option_1', name: 'Basic Support', price: '$5' },
      { id: 'option_2', name: 'Priority Access', price: '$15' },
      { id: 'option_3', name: 'Lifetime Updates', price: '$20' }
    ]
  )
  </script>
  