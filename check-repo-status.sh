#!/bin/bash

git log -m -1 --name-only $1 | grep src/BiMonetaryApi 