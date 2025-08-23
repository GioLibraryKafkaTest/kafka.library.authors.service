using library.core.domain.Events;
using library.core.domain.Events.Authors;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace infraestructure.Converters;
public class EventJsonConverter : JsonConverter<BaseEvent>
{
  public override bool CanConvert(Type typeToConvert)
  {
    return typeToConvert.IsAssignableFrom(typeof(BaseEvent));
  }
  public override BaseEvent? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    if (!JsonDocument.TryParseValue(ref reader, out var doc))
      throw new JsonException($"Fallo el parse json de {nameof(JsonDocument)}");

    if (!doc.RootElement.TryGetProperty("Type", out var type))
      throw new JsonException($"No se puede parsear la propiedad {doc.GetType()}");

    var typeDiscriminator = type.GetString();
    string json = doc.RootElement.GetRawText();

    return typeDiscriminator switch
    {
      nameof(AuthorCreatedEvent) => JsonSerializer.Deserialize<AuthorCreatedEvent>(json),
      nameof(AuthorUpdatedEvent) => JsonSerializer.Deserialize<AuthorUpdatedEvent>(json),
      _ => throw new JsonException($"{typeDiscriminator} no soportado")
    };
  }

  public override void Write(Utf8JsonWriter writer, BaseEvent value, JsonSerializerOptions options)
  {
    throw new NotImplementedException();
  }
}