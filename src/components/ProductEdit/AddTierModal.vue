<template>
  <div>
    <!-- Backdrop -->
    <div class="fixed inset-0 bg-black bg-opacity-50 z-40 cursor-pointer" @click="close"></div>

    <!-- Modal -->
    <div class="fixed inset-0 flex items-center justify-center z-50  " role="dialog" aria-modal="true"
      aria-labelledby="modalLabel" @click.self="close">
      <div class="bg-white rounded-lg shadow-lg w-full max-w-lg" @click.stop>
        <div class="modal-header p-4 border-b border-gray-200 flex justify-between items-center">
          <h5 class="modal-title text-lg font-semibold" id="modalLabel">
            Add Product
          </h5>
          <button type="button" class="btn-close text-gray-500 hover:text-gray-700" aria-label="Close" @click="close">
            ✕
          </button>
        </div>

        <form @submit.prevent="handleSubmit">
          <div class="modal-body p-4 py-1 space-y-2 max-h-[60vh] overflow-y-auto">
            <div>
              <label for="id" class="form-label block mb-1 font-medium text-gray-700">ID</label>
              <input type="text" id="id" v-model="form.id" required
                class="form-control w-full rounded border border-gray-300 px-3 py-2 focus:outline-none focus:ring-2 focus:ring-cyan-500" />
            </div>

            <div>
              <label for="name" class="form-label block mb-1 font-medium text-gray-700">Name</label>
              <input type="text" id="name" v-model="form.name" required
                class="form-control w-full rounded border border-gray-300 px-3 py-2 focus:outline-none focus:ring-2 focus:ring-cyan-500" />
            </div>

            <div>
              <label for="price" class="form-label block mb-1 font-medium text-gray-700">Price</label>
              <input type="number" id="price" v-model.number="form.price" required min="0" step="0.01"
                class="form-control w-full rounded border border-gray-300 px-3 py-2 focus:outline-none focus:ring-2 focus:ring-cyan-500" />
            </div>
          </div>

          <div class="modal-footer p-4 border-t border-gray-200 flex justify-end space-x-2">
            <button type="button"
              class="btn btn-secondary px-4 py-2 rounded border border-gray-400 text-gray-700 hover:bg-gray-100"
              @click="close">
              Cancel
            </button>
            <button type="submit" class="btn btn-primary px-4 py-2 rounded bg-cyan-500 text-white hover:bg-cyan-600">
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
import { defaultTierTemplates } from '../../static-default.ts';

const emit = defineEmits(["close", "submit"]);

const form = reactive({
  id: "",
  name: "",
  price: null,
  duration: defaultTierTemplates[0].duration
});

function close() {
  emit("close");
}

function handleSubmit() {
  console.log("Submitted:", { ...form });
  emit("submit", { ...form });
  close();
}
</script>
