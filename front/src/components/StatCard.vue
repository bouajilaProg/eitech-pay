<template>
  <div class="col-sm-4 ">
    <div class="card">
      <div class="card-body">
        <div class="d-flex align-items-center">
          <div class="me-3">
            <span class="text-muted">{{ title }}</span>
            <div class="h2">{{ value }}</div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
defineProps({
  title: String,
  value: [String, Number]
});
</script>
