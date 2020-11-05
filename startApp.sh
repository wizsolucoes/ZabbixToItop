#!/bin/bash

path=./ZabbixToItop/bin/Release/netcoreapp3.1/ZabbixToItop.dll

if [ -z "${10}" ]
then
    if [ -z "$8" ]
    then
    dotnet $path "$1" "$2" "$3" "$4" "$5" "$6" "$7"
    else
    dotnet $path "$1" "$2" "$3" "$4" "$5" "$6" "$7" "$8" "$9"
    fi
else
  dotnet $path "$1" "$2" "$3" "$4" "$5" "$6" "$7" "$8" "$9" "${10}"
fi
