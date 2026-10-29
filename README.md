# Docker Multi Replicas
Simple example on how to build one Container on Docker, and run multiples instances.

The main idea is to have an application (on any language or platform), that is built once, and runs multiples instances of the same image on as many containers as you want.

## Composing
On the `docker-compose.yml`, first you create the section that's do the _magic_.

These section you build your application, and create the first image and the first instance.

```
  app-worker-1:
    build:
      context: ./src
      dockerfile: dockerfile
    image: multi-replicas:latest
    environment:
      - INSTANCE_ID=replica_1
```

Then, for each adition instance, create another section with the following pattern:
```
  app-worker-2:
    image: multi-replicas:latest
    depends_on:
      - app-worker-1
    environment:
      - INSTANCE_ID=replica_2
```

Notice that, those sections:
- **DO NOT** have the section `build`
- **WILL HAVE** the parameter `depends_on`

This will instruct Docker to wait for the first build, and run the same image with different environment variables.

## Running
In the terminal, just run the following command:
```
docker compose up -d --build
```
