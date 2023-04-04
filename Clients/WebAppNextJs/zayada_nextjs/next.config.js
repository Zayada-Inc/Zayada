/** @type {import('next').NextConfig} */

const path = require('path');
const nextConfig = {
  experimental: {
    appDir: true,
    reactStrictMode: true,
    pagesDirectory: path.join(__dirname, 'src/app'),
  },
}

module.exports = nextConfig
