ROOT_DIR="$(shell dirname $(realpath $(lastword $(MAKEFILE_LIST))))/"

PORT ?= 80

all: build test

build:
	docker build --file Dockerfile .

bats:
	git clone https://github.com/sstephenson/bats.git ; \
	cd bats; \
	git reset --hard 03608115df2071fff4eaaff1605768c275e5f81f

prepare-test: bats
	npm install tap-xunit -g ; \
	mkdir -p target ; \
	#git submodule update --init --recursive

test: prepare-test
	DOCKERFILE=Dockerfile bats/bin/bats --tap tests | \
		tap-xunit --package="co.elastic.opbeans" > target/junit-results.xml

publish:
	docker build --file "Dockerfile" \
			 --tag opbeans/opbeans-dotnet:latest \
			 --no-cache --pull . ; \
	docker push opbeans/opbeans-dotnet:latest

clean:
	rm -rf tests/test_helper/bats-* ; \
	rm -rf bats ; \
	rm -rf target
