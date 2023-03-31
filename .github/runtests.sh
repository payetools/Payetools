#!/bin/bash

FAILURES=0
TEST_PROJECTS=$(find test -name "Paytools.*.Tests")

for PROJECT in $TEST_PROJECTS
do 
    dotnet test ./$PROJECT /p:Configuration=$BUILD_CONFIG --no-build --verbosity normal --logger "console;verbosity=normal"
    if test "$?" != "0" 
    then ((FAILURES+=1)) 
    fi    
done

exit $FAILURES
