// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Text.Json;
using Azure.Core;

namespace Azure.Management.Resources.Models
{
    public partial class ParameterValuesValue : IUtf8JsonSerializable
    {
        void IUtf8JsonSerializable.Write(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            if (Value != null)
            {
                writer.WritePropertyName("value");
                writer.WriteObjectValue(Value);
            }
            writer.WriteEndObject();
        }

        internal static ParameterValuesValue DeserializeParameterValuesValue(JsonElement element)
        {
            object value = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("value"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        continue;
                    }
                    value = property.Value.GetObject();
                    continue;
                }
            }
            return new ParameterValuesValue(value);
        }
    }
}