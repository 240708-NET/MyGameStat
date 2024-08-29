FROM node:alpine AS build
WORKDIR /app
# COPY below depends on .dockerignore to exclude node_modules, .next, etc.
COPY . .
RUN npm ci
RUN npm run build
ENTRYPOINT ["npm", "run", "start"]

EXPOSE 3000
