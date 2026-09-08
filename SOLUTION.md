## Problem
Typical functionality in most administration software will consist of 
- Saving event data (financial transactions, inventory changes, audit event etc)
- Dealing with variable flow
- Concurrently reading data whilst data is being recorded
- Handling failures gracefully or ideally without effecting users

## Solution
One proposed solution is decouple as many of these operations as possible whilst buffering critical operations to deal with the variable nature of real world.

![Architecture](/docs/img/architecture.svg)

## How this addresses the problem
- Having multiple instances of the user facing api will allow horizontal scale
- Should there be a surge in write operations the message queue can absorb it and allow sql to satisfy them in it's own time
- Should a single service fail the system may route flow to available services
- Should SQL fail the messages will backup on the queue
- Read operations do not effect write operations

The one failure point is the Kafka message queue. Should this fail the system will at least report the error back to any save operations but the system will should still serve read requests.