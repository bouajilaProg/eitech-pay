<template>
    <div>
      <!-- Backdrop -->
      <div class="fixed inset-0 bg-black bg-opacity-50 z-40 cursor-pointer" @click="close"></div>
  
      <!-- Modal -->
      <div class="fixed inset-0 flex items-center justify-center z-50" role="dialog" aria-modal="true"
        aria-labelledby="deleteModalLabel" @click.self="close">
        <div class="bg-white rounded-lg shadow-lg w-full max-w-md" @click.stop>
          <div class="modal-header p-4 border-b border-gray-200 flex justify-between items-center">
            <h5 class="modal-title text-lg font-semibold text-red-600" id="deleteModalLabel">
              Confirm Deletion
            </h5>
            <button type="button" class="btn-close text-gray-500 hover:text-gray-700" aria-label="Close" @click="close">
              ✕
            </button>
          </div>
  
          <div class="modal-body text-lg p-4 text-gray-700">
            <p>Are you sure you want to delete this item? This action cannot be undone.</p>
          </div>
  
          <div class="modal-footer p-4 border-t border-gray-200 flex justify-end space-x-2">
            <button type="button"
              class="btn btn-secondary px-4 py-2 rounded border border-gray-400 text-gray-700 hover:bg-gray-100"
              @click="close">
              Cancel
            </button>
            <button type="button"
              class="btn btn-danger px-4 py-2 rounded bg-red-600 text-white hover:bg-red-700"
              @click="confirmDelete">
              Delete
            </button>
          </div>
        </div>
      </div>
    </div>
  </template>
  
  <script setup>
  import { defineEmits } from "vue";
  
  const emit = defineEmits(["close", "confirm"]);
  
  function close() {
    emit("close");
  }
  
  function confirmDelete() {
    emit("confirm");
    close();
  }
  </script>
  