#!/usr/bin/env bats

# check dependencies
(
    type docker &>/dev/null || ( echo "docker is not available"; exit 1 )
    type curl &>/dev/null || ( echo "curl is not available"; exit 1 )
)>&2

# Assert that $1 is the output of a command $2
function assert {
    local expected_output=$1
    shift
    local actual_output
    actual_output=$("$@")
    actual_output="${actual_output//[$'\t\r\n']}" # remove newlines
    if ! [ "$actual_output" = "$expected_output" ]; then
        echo "expected: \"$expected_output\""
        echo "actual:   \"$actual_output\""
        false
    fi
}

function sut_image {
    echo "bats-${DOCKERFILE:-Dockerfile}" | tr '[:upper:]' '[:lower:]' | sed -e 's/dockerfile$/default/' | sed -e 's/dockerfile-//'
}

function cleanup {
	docker kill "$1" &>/dev/null ||:
    docker rm -fv "$1" &>/dev/null ||:
}
