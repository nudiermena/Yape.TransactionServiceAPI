# AntiFraud Sample Application

This sample project shows how to use MassTransit with Kafka Event Bus for Communication Purposes.

## Requirements


## Run Kafka Docker Compose
the Docker compose file contains the fundamental services to get started with the Solution
Path: https://github.com/nudiermena/Yape.TransactionServiceAPI/blob/master/docker-compose.yml

## Database Setup

The following databases need to be created on PostgreSQL to work as expected:

```sql
CREATE DATABASE TransactionDB;

CREATE TABLE Transactions (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    sourceaccountid UUID NOT NULL,
    targetaccountid UUID NOT NULL,
    transfertypeid INT NOT NULL,
    value DECIMAL(18, 2) NOT NULL,
    status VARCHAR(50) NOT NULL DEFAULT 'Pending',
    createdat TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE Antifraud (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    sourceaccountid UUID NOT NULL,
    value DECIMAL(18, 2) NOT NULL,
    createdat TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);
```


## Kafka Topic Setup

To set up the necessary Kafka topics, follow these steps:
After having the docker images running on the docker desktop App.

1. Open your browser and go to [http://localhost:8080/](http://localhost:8080/).
2. This will open the Kafka UI Admin.
3. Create two topics with the following names:
    - `transaction-created`
    - `transaction-antifraudchecked`

## Run the Yape.AntiFraudService Docker File
https://github.com/nudiermena/Yape.AntiFraudService/blob/master/src/AntiFraudService.Worker/Dockerfile



## Architecture Diagram
![MassTransit Kafka](https://github.com/nudiermena/Yape.TransactionServiceAPI/blob/9c8d378198bf04c3bc6d8204730801e9f55ebb26/Yape.TransactionService.Api/Images/YapeDiagram.PNG)




