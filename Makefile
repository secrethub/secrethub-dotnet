SHELL = bash

.PHONY: container
container:
	@docker build -t secrethub-dotnet-container .

.PHONY: test
test: container
	@docker run --rm -v $(PWD):/home/secrethub-dotnet -e SECRETHUB_CREDENTIAL=`cat $(HOME)/.secrethub/credential` \
	secrethub-dotnet-container bash -c "make -f docker-makefile.mk test"
	@make -f docker-makefile.mk clean --no-print-directory

.PHONY: nupkg
nupkg: container
	@docker run --rm -v $(PWD):/home/secrethub-dotnet \
	secrethub-dotnet-container bash -c "make -f docker-makefile.mk nupkg"
	@make -f docker-makefile.mk clean --no-print-directory

.PHONY: nupkg-publish
nupkg-publish: nupkg
	@dotnet nuget push SecretHub.*.nupkg --api-key ${API_KEY} --source https://api.nuget.org/v3/index.json
