namespace Play.Common.Common;

public static class MessageError
{
    public const string HostNotProvided = "No se ha proporcionado un valor para el parámetro 'Host' de la sección 'RabbitMqSettings'.";
    public const string ServiceNameNotProvided = "No se ha proporcionado un valor para el parámetro 'ServiceName' de la sección 'ServiceSettings'.";
    public const string MongoSeetingsNotProvided = "No se han proporcionado valores para los parámetros de la conexión a Mongo.";
}