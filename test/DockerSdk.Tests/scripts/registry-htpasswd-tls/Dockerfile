# Creates an image that hosts a Docker registry. The registry uses basic 
# authentication with TLS. The username is "testuser" and the password is
# "testpassword".

FROM alpine AS build
RUN apk update
RUN apk add openssl
RUN openssl req -newkey rsa:2048 -nodes -keyout sdk-test.key -x509 -days 3650 -out sdk-test.crt -subj "/C=US/ST=Wisconsin/L=Madison/O=Emdot/CN=docker-net-sdk-tests"
RUN apk add apache2-utils
RUN htpasswd -Bbn testuser testpassword > htpasswd

FROM registry:2
RUN mkdir /certs
COPY --from=build sdk-test.* /certs/
RUN mkdir /auth
COPY --from=build htpasswd /auth/
ENV REGISTRY_HTTP_ADDR=0.0.0.0:443
ENV REGISTRY_HTTP_TLS_CERTIFICATE=/certs/sdk-test.crt
ENV REGISTRY_HTTP_TLS_KEY=/certs/sdk-test.key
ENV REGISTRY_AUTH=htpasswd
ENV REGISTRY_AUTH_HTPASSWD_REALM="Registry Realm"
ENV REGISTRY_AUTH_HTPASSWD_PATH=/auth/htpasswd
