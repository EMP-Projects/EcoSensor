FROM postgis/postgis:16-3.4

RUN echo "shared_preload_libraries='pg_cron'" >> /usr/share/postgresql/postgresql.conf
RUN echo "cron.database_name='postgres'" >> /usr/share/postgresql/postgresql.conf

RUN mkdir -p /docker-entrypoint-initdb.d
COPY ./initdb-postgis.sh /docker-entrypoint-initdb.d
COPY ./update-postgis.sh /usr/local/bin
