SHELL = bash

.PHONY: secrethub-dotnet-container
secrethub-dotnet-container:
	@docker build -t dotnet-container .

.PHONY: test
test: secrethub-dotnet-container
	@docker run -v $(PWD):/secrethub-dotnet dotnet-container bash -c "make -f docker-makefile.mk dotnet-test"
	@make -f docker-makefile.mk clean --no-print-directory

.PHONY: nupkg
nupkg: secrethub-dotnet-container
	@docker run -v $(PWD):/secrethub-dotnet dotnet-container bash -c "make -f docker-makefile.mk nupkg"
	@make -f docker-makefile.mk clean --no-print-directory

.PHONY: nupkg-publish
nupkg-publish: nupkg
	@dotnet nuget push SecretHub.*.nupkg --api-key ${API_KEY} --source https://api.nuget.org/v3/index.json
