module.exports = {
  purge: [
    "./pages/**/*.{js,ts,jsx,tsx}",
    "./node_modules/flowbite/**/*.{js,ts,jsx,tsx,html}",
    "./node_modules/flowbite-react/**/*.{js,ts,jsx,tsx,html}"
  ],
  darkMode: false, // or 'media' or 'class'
  theme: {
    extend: {},
  },
  variants: {
    extend: {},
  },
  plugins: [require("flowbite/plugin")],
};