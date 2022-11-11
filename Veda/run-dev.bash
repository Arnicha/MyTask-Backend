#!/bin/bash
echo " __   _____ ___   _  
 \ \ / / __|   \ /_\  
  \ V /| _|| |) / _ \ 
   \_/ |___|___/_/ \_\

-------------------------
Liquibase Running
-------------------------
"
cd ./Liquibase/workspace

dbUrl="jdbc:postgresql://postgres:5432/postgres"
dbUserName="postgres"
dbPassword="12345678"

if [ $ASPNETCORE_ENVIRONMENT = "LocalRegression" ] ;\
then\
	dbUrl="jdbc:postgresql://pgsql_pos_chicken_backend_regression/postgres"
	dbUserName="postgres"
	dbPassword=""
fi;

java -jar liquibase.jar \
	--driver=org.postgresql.Driver \
	--classpath=./lib/postgresql.jar \
	--url=$dbUrl \
	--changeLogFile=./changelog/changelog-initschema.xml \
	--username=$dbUserName\
	--password=$dbPassword\
	--defaultSchemaName=public\
	update;
java -jar liquibase.jar \
	--driver=org.postgresql.Driver \
	--classpath=./lib/postgresql.jar \
	--url=$dbUrl \
	--changeLogFile=./changelog/changelog-master.xml \
	--username=$dbUserName\
	--password=$dbPassword\
	--defaultSchemaName=mytask\
	update && cd /app/out \
	&& dotnet MyTask.dll