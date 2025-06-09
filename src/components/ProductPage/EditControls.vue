<template>
  <div class="d-flex justify-content-end mb-4">
    <!-- View Mode -->
    <button v-if="!editMode" @click="$emit('edit')" class="btn btn-dark">
      Edit
    </button>

    <!-- Edit Mode -->
    <template v-else>
      <button @click="$emit('save')" class="btn btn-success me-2">
        Save
      </button>
      <button @click="$emit('cancel')" class="btn btn-danger">
        Cancel
      </button>
    </template>
  </div>
</template>

<script setup>
defineProps({
  editMode: Boolean,
})
defineEmits(['edit', 'save', 'cancel'])
</script>
