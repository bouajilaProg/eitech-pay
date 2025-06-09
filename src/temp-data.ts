export const productsData = [  
  {
    id: 'learnito_web_1',
    name: 'Learnito Web',
    subtitle: 'Interactive learning platform',
    description: 'A modern web app that helps students learn faster through quizzes, flashcards, and personalized study plans.',
    route: '/products/learnito-web',
    tiers: [
      {
        id: 'learnito_web_1_tier_1',
        name: 'Basic',
        price: 10,
        features: ['Access to all quizzes', 'Basic analytics', 'Email support'],
        duration: '1 month',
        sales: 300
      },
      {
        id: 'learnito_web_1_tier_2',
        name: 'Pro',
        price: 20,
        features: ['All Basic features', 'Advanced analytics', 'Priority support'],
        duration: '1 month',
        sales:150
      },
    ],
  },
  {
    id: 'tunfact_1',
    name: 'TunFact',
    subtitle: 'Simple and efficient invoicing',
    description: 'An intuitive invoicing system built for freelancers and small businesses in Tunisia. Create, send, and manage invoices with ease.',
    route: '/products/tunfact',
    tiers: [
      {
        id: 'tunfact_1_tier_1',
        name: 'Basic',
        price: 25,
        features: ['Access to all quizzes', 'Basic analytics', 'Email support'],
        duration: '1 month',
        sales: 12
      },
      {
        id: 'tunfact_1_tier_2',
        name: 'Pro',
        price: 50,
        features: ['All Basic features', 'Advanced analytics', 'Priority support'],
        duration: '1 month',
        sales:9
      },
    ],

  },
]


