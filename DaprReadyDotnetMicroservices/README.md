# Overview

This is the demo for presenting Sidecar Proxy pattern based on Dapr and Azure Cloud. It requires Azure Subscription or working local environment. Below that is dedicated link to present how to get started with Dapr on local environment with Docker and Docker Compose

# Architecture

![architecture](architecture.svg)

# Sidecar

[Sidecar pattern](https://learn.microsoft.com/en-us/azure/architecture/patterns/sidecar)

# Dapr

* [Dapr](https://dapr.io/)
* [Getting Started](https://docs.dapr.io/getting-started/)
* [API Reference](https://docs.dapr.io/reference/api/)
* [Building Blocks](https://docs.dapr.io/developing-applications/building-blocks/)
* [.NET SDK](https://docs.dapr.io/developing-applications/sdks/dotnet/)

# Configuration

## Container Apps Environment

[Quickstart](https://learn.microsoft.com/en-us/azure/container-apps/get-started?tabs=bash)

### UI

* Dapr must be enabled end configured -> use http protocol and port exposed in Docker
* Ingress must be enabled and configured for **Accepting traffic from anywhere**

### Api

* Dapr must be enabled end configured -> use http protocol and port exposed in Docker
* Ingress must be enabled and configured for **Limited to Container Apps Environment**

### Consumer

* Dapr must be enabled end configured -> use http protocol and port exposed in Docker

## Dapr Components

* Components must be configured in 

### Redis

[Getting started - Create cache instance with Azure Portal](https://learn.microsoft.com/en-us/azure/azure-cache-for-redis/cache-dotnet-core-quickstart)

#### Dapr component details

* Name: ***redis*** (it can be different but it requires code modification, DAPR_STORE_NAME must contain the same value as configured component name)
* Component type: ***state.redis***
* Version: ***v1***

#### Secrets

* redis-password (with value from Redis configuration)

#### Metadata

|Name|Source|Value|
|---|---|---|
|redisHost|Manual entry|***redis access url***|
|redisPassword|Reference a secret|redis-password|
|enableTLS|Manual entry|true|

#### Scopes

|App Id|App Name|
|---|---|
|consumer|consumer|

**INFO** it will make redis accessible only for Consumer microservice

### Service Bus

[Getting started - Create Service Bus Instance with Azure Portal](https://learn.microsoft.com/en-us/azure/service-bus-messaging/service-bus-quickstart-portal)

**IMPORTANT**

Select Standard Pricing tier if you want to use Service Bus Topics. Example uses Topics, not Queues.

#### Dapr component details

* Name: ***service-bus*** (it can be different but it requires code modification, DAPR_MESSAGE_BROKER_NAME must contain the same value as configured component name)
* Component type: ***pubsub.azure.servicebus***
* Version: ***v1***

#### Metadata

|Name|Source|Value|
|---|---|---|
|connectionString|Manual entry|***service bus instance connection string***|

**IMPORTANT**

Consumer app subscribes ***test*** topic so if you want to configure different topic, change value of TOPIC_NAME in Api project -> Program.cs line 13 and set the same value in ***Topic*** annotation in Consumer project -> Program.cs line 12