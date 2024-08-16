stack-dev-db:
	docker compose --env-file .env.stack -f docker-compose.yml -f docker-compose.dev.yml -p ecosensor --profile db down
	docker compose --env-file .env.stack -f docker-compose.yml -f docker-compose.dev.yml -p ecosensor --profile db up -d

stack-git-db:
	docker compose --env-file .env.stack -f docker-compose.yml -f docker-compose.git.yml -p ecosensor --profile db down
	docker compose --env-file .env.stack -f docker-compose.yml -f docker-compose.git.yml -p ecosensor --profile db up -d

stack-dev:
	docker compose --env-file .env.stack -f docker-compose.yml -f docker-compose.dev.yml -p ecosensor --profile all down
	docker compose --env-file .env.stack -f docker-compose.yml -f docker-compose.dev.yml -p ecosensor --profile all up -d

stack-all:
	docker compose --env-file .env.stack -f docker-compose.yml -f docker-compose.git.yml -p ecosensor --profile all down
	docker compose --env-file .env.stack -f docker-compose.yml -f docker-compose.git.yml -p ecosensor --profile all up -d