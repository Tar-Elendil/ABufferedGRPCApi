# Demo setup

## Services

The `docker-compose.yaml` contains all services and port setups required to run the demo.

These will be 
1. a Kafka message broker
2. a schema registry for clients
3. a postgres database
4. [adminer](https://hub.docker.com/_/adminer/) for a simple browser based DBMS

Start the services in detached mode
```bash
docker compose up -d
```

### Kafka broker setup
The demo assumes the following topic is set up on the broker. `Demo.Movements.Save`
```bash
./opt/kafka/bin/kafka-topics.sh --bootstrap-server localhost:9092 --create --topic Demo.Movements.Save --partitions 1 --replication-factor 1
```
and confirm that the topic has been created with
```bash
./opt/kafka/bin/kafka-topics.sh --bootstrap-server localhost:9092 --list
```

### SQL setup
There is an initialisation file [init.sql](../scripts/postgres-setup/init.sql) that gets mapped to the docker entrypoint directory `docker-entrypoint-initdb.d`. This means that in most cases simply running the `docker compose up -d` command will run the database initialisation script, however this depends on the user setup. Should this setup fail to run you will need to manually run the init script either through adminer or another means.

### adminer
The adminer page will run on `http://localhost:8080/` and the following are the login details as configured in the [docker-compose.yaml](../docker-compose.yaml)
```
System: PostgreSQL
Server: db
Username: admin
Password: example
Database: demo
```
