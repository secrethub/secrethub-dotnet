SHELL = bash
DOCKER_COMMAND = docker run --rm \
		-v $$(pwd):/home/secrethub-dotnet \
		-v $$(go env GOPATH):/root/go \
		-e SECRETHUB_CREDENTIAL=`cat ${HOME}/.secrethub/credential` \
		secrethub-dotnet-container \
		bash -c

.PHONY: container
container:
	@docker build -t secrethub-dotnet-container .

.PHONY: test
test: container
	@$(DOCKER_COMMAND) "make -f docker-makefile.mk test"
	@make -f docker-makefile.mk clean --no-print-directory

.PHONY: nupkg
nupkg: container
	@$(DOCKER_COMMAND) "make -f docker-makefile.mk nupkg"
	@make -f docker-makefile.mk clean --no-print-directory

.PHONY: nupkg-publish
nupkg-publish: nupkg
	@dotnet nuget push SecretHub.*.nupkg --api-key ${API_KEY} --source https://api.nuget.org/v3/index.json
