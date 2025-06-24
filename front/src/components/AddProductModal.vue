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
            <!-- Type Switch styled as tabs -->
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

            <!-- ID -->
            <div>
              <label for="id" class="block mb-1 font-medium text-gray-700">ID</label>
              <input type="text" id="id" v-model="form.id"
                     class="w-full rounded border px-3 py-2 focus:outline-none focus:ring-2"
                     :class="errors.id ? 'border-red-600 focus:ring-red-600' : 'border-gray-300 focus:ring-cyan-500'" />
              <p v-if="errors.id" class="text-red-600 text-sm mt-1">{{ errors.id }}</p>
            </div>

            <!-- Name -->
            <div>
              <label for="name" class="block mb-1 font-medium text-gray-700">Name</label>
              <input type="text" id="name" v-model="form.name"
                     class="w-full rounded border px-3 py-2 focus:outline-none focus:ring-2"
                     :class="errors.name ? 'border-red-600 focus:ring-red-600' : 'border-gray-300 focus:ring-cyan-500'" />
              <p v-if="errors.name" class="text-red-600 text-sm mt-1">{{ errors.name }}</p>
            </div>

            <!-- Description -->
            <div>
              <label for="description" class="block mb-1 font-medium text-gray-700">Description</label>
              <textarea id="description" v-model="form.description" rows="3"
                        class="w-full rounded border px-3 py-2 focus:outline-none focus:ring-2 focus:ring-cyan-500"></textarea>
            </div>

            <!-- License Fields -->
            <div v-if="form.type === 'license'">
              <div>
                <label class="block mb-1 font-medium text-gray-700">Max Devices</label>
                <input type="number" v-model.number="form.maxDevices" min="1"
                       class="w-full rounded border px-3 py-2 focus:outline-none focus:ring-2"
                       :class="errors.maxDevices ? 'border-red-600 focus:ring-red-600' : 'border-gray-300 focus:ring-cyan-500'" />
                <p v-if="errors.maxDevices" class="text-red-600 text-sm mt-1">{{ errors.maxDevices }}</p>
              </div>

              <div>
                <label class="block mb-1 font-medium text-gray-700">Duration</label>
                <input type="number" v-model.number="form.duration" min="1"
                       class="w-full rounded border px-3 py-2 focus:outline-none focus:ring-2"
                       :class="errors.duration ? 'border-red-600 focus:ring-red-600' : 'border-gray-300 focus:ring-cyan-500'" />
                <p v-if="errors.duration" class="text-red-600 text-sm mt-1">{{ errors.duration }}</p>
              </div>

              <div>
                <label class="block mb-1 font-medium text-gray-700">Grace Period</label>
                <input type="text" v-model="form.gracePeriod"
                       class="w-full rounded border px-3 py-2 focus:outline-none focus:ring-2"
                       :class="errors.gracePeriod ? 'border-red-600 focus:ring-red-600' : 'border-gray-300 focus:ring-cyan-500'" />
                <p v-if="errors.gracePeriod" class="text-red-600 text-sm mt-1">{{ errors.gracePeriod }}</p>
              </div>

              <div>
                <label class="block mb-1 font-medium text-gray-700">Price</label>
                <input type="number" v-model.number="form.price" min="0.01" step="0.01"
                       class="w-full rounded border px-3 py-2 focus:outline-none focus:ring-2"
                       :class="errors.price ? 'border-red-600 focus:ring-red-600' : 'border-gray-300 focus:ring-cyan-500'" />
                <p v-if="errors.price" class="text-red-600 text-sm mt-1">{{ errors.price }}</p>
              </div>
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
import { licenceService } from "@/services/licence.service";
import { subscriptionService } from "@/services/subscription.service";

const emit = defineEmits(["close", "submit"]);

const form = reactive({
  id: "",
  name: "",
  description: "",
  type: "license",
  maxDevices: null,
  duration: null,
  gracePeriod: null,
  price: null,
});

const errors = reactive({});

function validateForm() {
  Object.keys(errors).forEach((key) => delete errors[key]);

  if (!form.id.trim()) 
    errors.id = "ID is required.";
  if (!form.name.trim())
    errors.name = "Name is required.";

  if (form.type === "license") {
    if (form.maxDevices == null || form.maxDevices <= 0)
      errors.maxDevices = "Max Devices must be greater than 0.";
    if (form.duration == null || form.duration <= 0)
      errors.duration = "Duration must be greater than 0.";
    if (form.gracePeriod == null || form.gracePeriod < 0)
      errors.gracePeriod = "Grace Period must be 0 or more.";
    if (form.price == null || form.price <= 0)
      errors.price = "Price must be greater than 0.";
  }

  return Object.keys(errors).length === 0;
}

function close() {
  emit("close");
}

async function handleSubmit() {
  if (!validateForm()) return;

  try {
    if (form.type === "license") {
      console.log("ssss trying")
      await licenceService.createLicence({
        licenceId: form.id,
        licenceName: form.name,
        description: form.description,
        maxDevices: form.maxDevices,
        duration: form.duration,
        gracePeriod: form.gracePeriod,
        price: form.price
      });
    } else if (form.type === "subscription") {
      await subscriptionService.createSubscription({
        subscriptionId: form.id,
        subscriptionName: form.name,
        description: form.description
      });
    }

    emit("submit");
    close();
  } catch (error) {
    console.error("Failed to create product:", error);
  }
}

</script>
