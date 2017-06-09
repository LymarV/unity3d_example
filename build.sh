#!/bin/bash

nuget install FAKE -OutputDirectory packages -ExcludeVersion
mono --runtime=v4.0 packages/FAKE/tools/FAKE.exe xfake/build.fsx $@