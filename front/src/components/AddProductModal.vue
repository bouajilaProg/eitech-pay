<template>
  <div>
    <!-- Backdrop -->
    <div class="fixed inset-0 bg-black bg-opacity-50 z-40 cursor-pointer" @click="close"></div>

    <!-- Modal -->
    <div class="fixed inset-0 flex items-center justify-center z-50" role="dialog" aria-modal="true" aria-labelledby="modalLabel" @click.self="close">
      <div class="bg-white rounded-lg shadow-lg w-full max-w-lg" @click.stop>
        <!-- Header -->
        <div class="modal-header p-4 border-b border-gray-200 flex justify-between items-center">
          <h5 class="text-lg font-semibold" id="modalLabel">Add Product</h5>
          <button type="button" class="text-gray-500 hover:text-gray-700" aria-label="Close" @click="close">✕</button>
        </div>

        <!-- Form -->
        <form @submit.prevent="handleSubmit">
          <div class="modal-body p-4 py-1 space-y-3 max-h-[60vh] overflow-y-auto">

            <!-- Type Switch styled as tabs using Tabler classes -->
            <ul class="nav nav-tabs mb-4">
              <li class="nav-item">
                <a href="#" class="nav-link"
                   :class="{ active: form.type === 'license' }"
                   @click.prevent="form.type = 'license'">
                  License
                </a>
              </li>
              <li class="nav-item">
                <a href="#" class="nav-link"
                   :class="{ active: form.type === 'subscription' }"
                   @click.prevent="form.type = 'subscription'">
                  Subscription
                </a>
              </li>
            </ul>

            <!-- Name -->
            <div>
              <label for="name" class="block mb-1 font-medium text-gray-700">Name</label>
              <input type="text" id="name" v-model="form.name" required
                class="w-full rounded border border-gray-300 px-3 py-2 focus:outline-none focus:ring-2 focus:ring-cyan-500" />
            </div>

            <!-- Description -->
            <div>
              <label for="description" class="block mb-1 font-medium text-gray-700">Description</label>
              <textarea id="description" v-model="form.description" rows="3"
                class="w-full rounded border border-gray-300 px-3 py-2 focus:outline-none focus:ring-2 focus:ring-cyan-500"></textarea>
            </div>

            

          </div>

          <!-- Footer -->
          <div class="modal-footer p-4 border-t border-gray-200 flex justify-end space-x-2">
            <button type="button"
              class="px-4 py-2 rounded border border-gray-400 text-gray-700 hover:bg-gray-100"
              @click="close">
              Cancel
            </button>
            <button type="submit"
              class="px-4 py-2 rounded bg-cyan-500 text-white hover:bg-cyan-600">
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
  name: "",
  description: "",
  type: "license", // Default type
});

function close() {
  emit("close");
}

function handleSubmit() {
  console.log("Product submitted:", { ...form });
  emit("submit", { ...form });
  close();
}
</script>

<!-- Add Tabler CSS to your main HTML entry or layout -->
<!-- Add this in your index.html if you haven't already -->
<!--
<link href="https://unpkg.com/@tabler/core@latest/dist/css/tabler.min.css" rel="stylesheet">
-->
