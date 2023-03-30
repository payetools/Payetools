TESTPROJECTS=$(find test -name "Paytools.*.Tests")
for PROJECT in $TESTPROJECTS; do "ls ./$PROJECT"; done
