#!/bin/bash

if [ -d "./ZabbixToItop" ] 
then
    echo "Repository already cloned"
else
    echo "Cloning repository"
    git clone https://github.com/wizsolucoes/ZabbixToItop.git
fi

echo "Entering the folder"
cd ZabbixToItop/ZabbixToItop

UPSTREAM=${1:-'@{u}'}
LOCAL=$(git rev-parse @)
REMOTE=$(git rev-parse "$UPSTREAM")
BASE=$(git merge-base @ "$UPSTREAM")

if [ $LOCAL = $REMOTE ]; then
    echo "Up-to-date"
elif [ $LOCAL = $BASE ]; then
    echo "Pulling from main"
    git pull origin main
    echo "Bulding project"
    dotnet build
fi
