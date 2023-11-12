# Create your first application with KafkaFlow

https://farfetch.github.io/kafkaflow/docs/getting-started/create-your-first-application


# Start the cluster
docker-compose up -d

# Run the Consumer:

dotnet run --project Consumer/Consumer.csproj 

# From another terminal, run the Producer:

dotnet run --project Producer/Producer.csproj 
