# make server work!

run ```docker run --name some-postgres -e POSTGRES_PASSWORD=mysecretpassword -d -p 5432:5432 postgres``` to set up docker container for postgres

run ```docker exec -it some-postgres psql -U postgres``` to access psql on container

run ```CREATE DATABASE postgres; CREATE SCHEMA dbo AUTHORIZATION postgres;CREATE TABLE dbo.Foretak (Id serial Primary key, Orgnummer varchar(64), Navn varchar(64));``` as psql commands to set up table





