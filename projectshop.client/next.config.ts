import { NextConfig } from "next";

const nextConfig: NextConfig = {
  reactStrictMode: true,

  // Configure API proxy to .NET backend
  async rewrites() {
    return [
      {
        source: "/api/:path*",
        destination: "https://localhost:5294/api/:path*",
      },
    ];
  },

  // Optimize images
  images: {
    remotePatterns: [
      {
        protocol: "https",
        hostname: "**",
      },
    ],
  },
};

export default nextConfig;
