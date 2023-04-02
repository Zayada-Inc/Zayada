/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ['./index.html', './src/**/*.{js,ts,jsx,tsx}'],
  theme: {
    extend: {
      colors: {
        'primary-color': '#FF8138',
        'secondary-color': '#000AFF',
        'dark-color': '#1E1E1E',
        'primary-btn': '#EB600F',
      },
      backgroundImage: {
        'brand-pattern': "url('/src/assets/brand_pattern2.svg')",
      },
    },
  },
  plugins: [],
};
