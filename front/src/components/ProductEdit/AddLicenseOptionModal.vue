<template>
    <div>
      <!-- Backdrop -->
      <div class="fixed inset-0 bg-black bg-opacity-50 z-40 cursor-pointer" @click="close"></div>
  
      <!-- Modal -->
      <div class="fixed inset-0 flex items-center justify-center z-50" role="dialog" aria-modal="true" @click.self="close">
        <div class="bg-white rounded-lg shadow-lg w-full max-w-lg" @click.stop>
          <!-- Header -->
          <div class="modal-header p-4 border-b border-gray-200 flex justify-between items-center">
            <h5 class="modal-title text-lg font-semibold">Add License Option</h5>
            <button class="text-gray-500 hover:text-gray-700" @click="close">✕</button>
          </div>
  
          <!-- Form -->
          <form @submit.prevent="handleSubmit">
            <div class="modal-body p-4 py-1 space-y-3 max-h-[60vh] overflow-y-auto">
              <div>
                <label class="block mb-1 font-medium text-gray-700">Option Name</label>
                <input v-model="form.optionName" type="text" required
                  class="w-full border border-gray-300 rounded px-3 py-2 focus:outline-none focus:ring-2 focus:ring-cyan-500" />
              </div>
  
              <div>
                <label class="block mb-1 font-medium text-gray-700">Description</label>
                <textarea v-model="form.description" required rows="3"
                  class="w-full border border-gray-300 rounded px-3 py-2 focus:outline-none focus:ring-2 focus:ring-cyan-500"></textarea>
              </div>
  
              <div>
                <label class="block mb-1 font-medium text-gray-700">Price (TND)</label>
                <input v-model.number="form.price" type="number" min="0" step="0.01" required
                  class="w-full border border-gray-300 rounded px-3 py-2 focus:outline-none focus:ring-2 focus:ring-cyan-500" />
              </div>
            </div>
  
            <!-- Footer -->
            <div class="modal-footer p-4 border-t border-gray-200 flex justify-end gap-2">
              <button type="button"
                class="px-4 py-2 rounded border border-gray-400 text-gray-700 hover:bg-gray-100"
                @click="close">
                Cancel
              </button>
              <button type="submit" class="px-4 py-2 rounded bg-cyan-500 text-white hover:bg-cyan-600">
                Submit
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </template>
  
  <script setup>
  import { reactive, defineEmits } from "vue";
  
  const emit = defineEmits(["close", "submit"]);
  
  const form = reactive({
    optionName: "",
    description: "",
    price: null,
  });
  
  function close() {
    emit("close");
  }
  
  function handleSubmit() {
    emit("submit", {
      ...form,
      createdAt: new Date().toISOString(),
      lastUpdateAt: new Date().toISOString(),
      isArchived: false,
    });
    close();
  }
  </script>
  