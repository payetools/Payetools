TESTPROJECTS=$(find test -name "Paytools.*.Tests")
for PROJECT in $TESTPROJECTS; do "dotnet test $PROJECT --no-build"; done
