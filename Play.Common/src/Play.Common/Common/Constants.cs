namespace Play.Common.Common;

public class Constants
{
    /// <summary>
    /// Cantidad de veces que se intentará enviar un elemento por medio de la cola en caso de que el consumidor no responda.
    /// </summary>
    public const int QuantityRetriesShippingConsumer = 3;

    /// <summary>
    /// Intervalo de tiempo en milisegundos entre cada intento de envio de elemento por medio de la cola en caso de que el consumidor no responda.
    /// </summary>
    public const int TimeIntervalBetweenRetries = 15;
}