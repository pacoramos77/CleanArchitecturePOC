dev:
	docker compose up -d
	dotnet watch --project src/webapi run

start:
	dotnet run --project src/webapi

add-migration:
	dotnet ef migrations add "SampleMigration" --project src/External/Infrastructure --startup-project src/webapi --output-dir Migrations

test:
	dotnet watch --project test/Core.Tests test

test-ci:
	dotnet test --project test/Core.Tests

coverage:
	bash ./test/coverage.sh

format:
	dotnet csharpier .

.PHONY: dev start test test-ci coverage format
