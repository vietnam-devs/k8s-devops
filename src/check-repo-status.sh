#!/bin/bash

git log -m -1 --name-only ${scmVars.GIT_COMMIT} | grep src/BiMonetaryApi 