# Demo setup

## Services

The `docker-compose.yaml` contains all services and port setups required to run the demo.

These will be 
1. a Kafka message broker
2. a schema registry for clients
3. a postgres database
4. an nginx proxy and load balancer

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