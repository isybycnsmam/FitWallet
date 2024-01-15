FROM node:latest as build
WORKDIR /usr/local/app
COPY ./src/frontend /usr/local/app/
RUN npm install
RUN npm run build

FROM nginx:latest
COPY --from=build /usr/local/app/dist/fit-wallet/browser /usr/share/nginx/html
