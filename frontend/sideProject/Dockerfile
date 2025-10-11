# Etapa build
FROM node:20 AS build
WORKDIR /app

COPY package*.json ./
COPY pnpm-lock.yaml ./
RUN npm install -g pnpm
RUN pnpm install
COPY . .

RUN npm run build --prod

# Etapa runtime con Nginx
FROM nginx:alpine AS runtime
COPY --from=build /app/dist/side-project/browser /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
