FROM node:18.16.0 as build
WORKDIR /usr/local/app
COPY ./src/frontend /usr/local/app/
RUN npm install
RUN npm run build
FROM nginx:latest
COPY --from=build /usr/local/app/dist/fit-wallet /usr/share/nginx/html
