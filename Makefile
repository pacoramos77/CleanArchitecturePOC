dev:
	docker compose up -d
	dotnet watch --project src/WebApi run

start:
	dotnet run --project src/WebApi

start-prod:
	dotnet run --project src/WebApi --launch-profile "prod"

add-migration: # make add-migration NAME=Migration_Name
	dotnet ef migrations add ${NAME} --project src/Infrastructure --startup-project src/WebApi --output-dir Data/Migrations

update-database:
	dotnet ef database update --project src/Infrastructure --startup-project src/WebApi

test:
	dotnet watch --project test/Core.Tests test

test-ci:
	dotnet test --project test/Core.Tests

coverage:
	bash ./test/coverage.sh

format:
	dotnet csharpier .

.PHONY: dev start test test-ci coverage format
