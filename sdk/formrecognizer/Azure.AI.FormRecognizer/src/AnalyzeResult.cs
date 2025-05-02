// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ClientModel.Primitives;
using System.Text.Json;
using Azure.Core;

namespace Azure.AI.FormRecognizer.DocumentAnalysis
{
    /// <summary>
    /// Represents one or more documents that have been analyzed by a trained or prebuilt model.
    /// </summary>
    public partial class AnalyzeResult : IJsonModel<AnalyzeResult>
    {
        internal AnalyzeResult()
        {
        }

        /// <summary>
        /// Service version used to produce this result.
        /// </summary>
        [CodeGenMember("ApiVersion")]
        public string ServiceVersion { get; }

        private StringIndexType StringIndexType { get; }

        AnalyzeResult IJsonModel<AnalyzeResult>.Create(ref Utf8JsonReader reader, ModelReaderWriterOptions options)
        {
            var format = options.Format == "W" ? ((IPersistableModel<AnalyzeResult>)this).GetFormatFromOptions(options) : options.Format;
            if (format != "J")
            {
                throw new FormatException($"The model {nameof(AnalyzeResult)} does not support reading '{format}' format.");
            }

            using JsonDocument document = JsonDocument.ParseValue(ref reader);
            if (document.RootElement.TryGetProperty("analyzeResult", out var analyzeResult))
            {
                return DeserializeAnalyzeResult(analyzeResult);
            }
            else
            {
                return DeserializeAnalyzeResult(document.RootElement);
            }
        }

        AnalyzeResult IPersistableModel<AnalyzeResult>.Create(BinaryData data, ModelReaderWriterOptions options)
        {
            var format = options.Format == "W" ? ((IPersistableModel<AnalyzeResult>)this).GetFormatFromOptions(options) : options.Format;

            switch (format)
            {
                case "J":
                    {
                        using JsonDocument document = JsonDocument.Parse(data, ModelSerializationExtensions.JsonDocumentOptions);
                        if (document.RootElement.TryGetProperty("analyzeResult", out var analyzeResult))
                        {
                            return DeserializeAnalyzeResult(analyzeResult);
                        }
                        else
                        {
                            return DeserializeAnalyzeResult(document.RootElement);
                        }
                    }
                default:
                    throw new FormatException($"The model {nameof(AnalyzeResult)} does not support reading '{options.Format}' format.");
            }
        }

        string IPersistableModel<AnalyzeResult>.GetFormatFromOptions(ModelReaderWriterOptions options) => "J";

        void IJsonModel<AnalyzeResult>.Write(Utf8JsonWriter writer, ModelReaderWriterOptions options)
        {
            writer.WriteStartObject();
            JsonModelWriteCore(writer, options);
            writer.WriteEndObject();
        }

        /// <param name="writer"> The JSON writer. </param>
        /// <param name="options"> The client options for reading and writing models. </param>
        protected virtual void JsonModelWriteCore(Utf8JsonWriter writer, ModelReaderWriterOptions options)
        {
            throw new NotImplementedException();
        }

        BinaryData IPersistableModel<AnalyzeResult>.Write(ModelReaderWriterOptions options)
        {
            var format = options.Format == "W" ? ((IPersistableModel<AnalyzeResult>)this).GetFormatFromOptions(options) : options.Format;

            switch (format)
            {
                case "J":
                    return ModelReaderWriter.Write(this, options);
                default:
                    throw new FormatException($"The model {nameof(AnalyzeResult)} does not support writing '{options.Format}' format.");
            }
        }
    }
}
