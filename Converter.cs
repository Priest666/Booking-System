using Booking_System;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

internal class PremisesConverter : JsonConverter<Premises>
{
    public override Premises Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
        {
            var rootElement = doc.RootElement;

            // If the "HasProjector" property exists, it's a ClassRoom
            if (rootElement.TryGetProperty("HasProjector", out _))
            {
                return JsonSerializer.Deserialize<ClassRoom>(rootElement.GetRawText(), options);
            }

            // If the "HasWhiteboard" property exists, it's a GroupRoom
            if (rootElement.TryGetProperty("HasWhiteboard", out _))
            {
                return JsonSerializer.Deserialize<GroupRoom>(rootElement.GetRawText(), options);
            }

            // Default case: if neither property exists, assume it's a generic Premises object
            return JsonSerializer.Deserialize<Premises>(rootElement.GetRawText(), options);
        }
    }

    public override void Write(Utf8JsonWriter writer, Premises value, JsonSerializerOptions options)
    {
        // Serialize based on the actual type of the object
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}
