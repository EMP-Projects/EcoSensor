build-osm:
	@echo Building and tagging OSM2PGSQL 
	docker build -t teamsviluppo/ecosensor-osm2pgsql:latest -f osm2pgsql/Dockerfile ./osm2pgsql

build-backend: build-osm
	@echo Building and tagging BACKEND 
	docker build -t teamsviluppo/ecosensor-backend:latest -f backend/EcoSensorApi/Dockerfile ./backend/EcoSensorApi

stack-osm:
	docker-compose -f ./docker-compose.yml --profile osm down
	docker-compose -f ./docker-compose.yml --profile osm rm

stack-db: stack-osm
	docker-compose -f ./docker-compose.yml --profile db down
	docker-compose -f ./docker-compose.yml --profile db rm

stack-openmeteo: stack-db
	docker-compose -f ./docker-compose.yml --profile osm down
	docker-compose -f ./docker-compose.yml --profile osm rm

stack-backend: stack-openmeteo
	docker-compose -f ./docker-compose.yml --profile backend down
	docker-compose -f ./docker-compose.yml --profile backend rm

stack-all:
	docker-compose -f ./docker-compose.yml --profile all down
	docker-compose -f ./docker-compose.yml --profile all rm
