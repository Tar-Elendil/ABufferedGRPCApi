# A buffered GRPC API

This is a project to demonstrate a gRPC api where object mutation requests are buffered through a kafka queue.

## Tech
- Postgresql read/write operations
- Decoupled reader-writer service architecture
- Using Kafka as a buffer 

## Solution
The solution [guide](SOLUTION.md) contains a comprehensive break down of the problem and how this pattern addresses it.

## Local setup
Follow the setup [guide](docs/LOCAL_SETUP.md)