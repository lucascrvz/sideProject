# Etapa build
FROM node:20 AS build
WORKDIR /app

# Copiar archivos de dependencias
COPY package*.json ./
COPY next.config.ts ./
COPY tsconfig.json ./
COPY postcss.config.mjs ./

# Instalar dependencias
RUN npm install

# Copiar el resto del código fuente
COPY . .

# Construir la aplicación
RUN npm run build

# Etapa runtime con Node.js (Next.js standalone)
FROM node:20-alpine AS runtime
WORKDIR /app

# Copiar archivos necesarios para producción
COPY --from=build /app/.next/standalone ./
COPY --from=build /app/.next/static ./.next/static
COPY --from=build /app/public ./public

# Exponer puerto 3000 (puerto por defecto de Next.js)
EXPOSE 3000

# Comando para iniciar la aplicación
CMD ["node", "server.js"]
