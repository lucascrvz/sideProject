import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  // Solo usar standalone para producción
  ...(process.env.NODE_ENV === "production" && { output: "standalone" }),
};

export default nextConfig;
