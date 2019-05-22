#!/usr/bin/env bats

load 'test_helper/bats-support/load'
load 'test_helper/bats-assert/load'
load test_helpers

SUT_IMAGE=$(sut_image)
SUT_CONTAINER=$(sut_image)

@test "build image" {
	cd $BATS_TEST_DIRNAME/..
	docker build -t $SUT_IMAGE .
}

@test "clean test containers" {
	cleanup $SUT_CONTAINER
}

@test "create test container" {
	docker run -d --name $SUT_CONTAINER -P $SUT_IMAGE
}

@test "test container is running" {
	sleep 1
	assert "true" docker inspect -f {{.State.Running}} $SUT_CONTAINER
}

@test "Opbeans is initialized" {
	URL="http://localhost:$(docker port "$SUT_CONTAINER" 80 | cut -d: -f2)"
	run curl --output /dev/null --silent --head --fail --connect-timeout 10 --max-time 30 "$URL/"
}

@test "clean test containers" {
	cleanup $SUT_CONTAINER
}
