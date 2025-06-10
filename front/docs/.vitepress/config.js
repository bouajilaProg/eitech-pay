import { defineConfig } from 'vitepress'

export default defineConfig({
  base: '/docs/',
  title: 'My Docs',
  // Add this build configuration
  vite: {
    server: {
      fs: {
        allow: ['..'], // Allow accessing files from project root
      },
    },
  },
})