TESTPROJECTS=$(find test -name "Paytools.*.Tests")
for PROJECT in $TESTPROJECTS; do dotnet test ./$PROJECT /p:Configuration=$BUILD_CONFIG --no-build --verbosity normal; done
