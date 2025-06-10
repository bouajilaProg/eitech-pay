import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import path from 'path'
import tailwindcss from '@tailwindcss/vite'

export default defineConfig({
  plugins: [vue(), tailwindcss()],
  resolve: {
    alias: {
      '@': path.resolve(__dirname, 'src'),
      '@comp': path.resolve(__dirname, 'src/components'),
    },
  },
  // Add this server configuration
  server: {
    fs: {
      allow: ['..'], // Allow serving files from project root
    },
  },
})