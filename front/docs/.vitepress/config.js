import { defineConfig } from 'vitepress'

export default defineConfig({
  base: '/docs/',
  title: 'EI-Pay',
  description: 'API Documentation for EI-Pay',

  vite: {
    server: {
      fs: {
        allow: ['..'], // Allow accessing files from project root
      },
    },
  },

  locales: {
    root: {
      label: 'English',
      lang: 'en',
      link: '/'
    },
    fr: {
      label: 'Français',
      lang: 'fr',
      link: '/fr/'
    },
    ar: {
      label: 'العربية',
      lang: 'ar',
      link: '/ar/'
    }
  },

  themeConfig: {
    nav: [
      { text: 'Home', link: '/' }
    ],

    localeLinks: {
      text: 'Languages',
      items: [
        { text: 'English', link: '/' },
        { text: 'Français', link: '/fr/' },
        { text: 'العربية', link: '/ar/' },
      ]
    },

    sidebar: [
      {
        text: '🚀 Getting Started',
        link: '/getting-started/',
        collapsible: true,
        items: [
          { text: 'Introduction', link: '/' },
          { text: 'Get Started', link: '/get-started' }
        ]
      },
      {
        text: '🌐 Endpoints',
        link: '/public-api/',
        collapsible: true,
        items: [
          {
            text: '🔑 License',
            link: '/public-api/license/',
            items: [
              { text: 'Check License', link: '/public-api/license/check' },
              { text: 'Get License Details', link: '/public-api/license/details' },
              { text: 'Activate License', link: '/public-api/license/activate' }
            ]
          },
          {
            text: '📦 Subscription',
            link: '/public-api/subscription/',
            items: [
              { text: 'Check Subscription', link: '/public-api/subscription/check' },
              { text: 'Get Subscription Details', link: '/public-api/subscription/details' }
            ]
          },
          {
            text: '💳 Payment',
            link: '/public-api/payment/',
            items: [
              { text: 'Init Pay License', link: '/public-api/payment/license' },
              { text: 'Init Pay Subscription', link: '/public-api/payment/subscription' }
            ]
          }
        ]
      }
      
    ],

    outline: [2, 3],
    outlineTitle: 'On This Page',

    search: {
      provider: 'local',
      options: {
        placeholder: 'Search...',
        translations: {
          button: {
            buttonText: 'Search',
            buttonAriaLabel: 'Search'
          }
        }
      }
    }
  }
})
