using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Play.Common.Common;
using Play.Common.Entities;
using Play.Common.Exceptions;
using Play.Common.Repositories;
using Play.Common.Service.Settings;

namespace Play.Common.MongoDb;

public static class Extensions
{
    public static void AddMongo(this IServiceCollection services, IConfiguration configuration)
    {
        var mongoDbSettings = configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();
        if (string.IsNullOrWhiteSpace(mongoDbSettings!.Host))
            throw new SettingException(MessageError.ServiceNameNotProvided, mongoDbSettings);

        var serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
        if (string.IsNullOrWhiteSpace(serviceSettings!.ServiceName))
            throw new SettingException(MessageError.ServiceNameNotProvided, serviceSettings);

        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
        BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

        services.AddSingleton(serviceProvider =>
        {
            var mongoClient = new MongoClient(mongoDbSettings!.ConnectionString);
            return mongoClient.GetDatabase(serviceSettings!.ServiceName);
        });
    }

    public static void AddMongoRepository<T>(this IServiceCollection services, string collectionName)
        where T : IEntity
    {
        services.AddSingleton<IRepository<T>>(serviceProvider =>
        {
            var database = serviceProvider.GetRequiredService<IMongoDatabase>();
            return new MongoRepository<T>(database, collectionName);
        });
    }
}