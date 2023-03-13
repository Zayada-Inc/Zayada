/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{js,ts,jsx,tsx,html}",
    "./node_modules/flowbite/**/*.{js,ts,jsx,tsx,html}",
    "./node_modules/flowbite-react/**/*.{js,ts,jsx,tsx,html}"
  ],
  theme: {
    extend: {},
  },
  plugins: [require("flowbite/plugin")]
};
