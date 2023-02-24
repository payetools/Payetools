// Copyright (c) 2022-2023 Paytools Ltd
//
// Licensed under the Apache License, Version 2.0 (the "License")~
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Paytools.Rti.Model;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Paytools.Rti;

public static class RtiDocumentSerializer
{
    private class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }

    private static readonly XmlSerializer _xmlSerializer;
    private static readonly XmlSerializerNamespaces _emptyNamespaces;

    static RtiDocumentSerializer()
    {
        _xmlSerializer = new XmlSerializer(typeof(GovTalkMessage));
        _emptyNamespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName() });
    }

    public static void Serialize(Stream stream, GovTalkMessage message, IRtiSerializable rtiDocument)
    {
        var payload = GetPayload(rtiDocument);

        message.Body = new()
        {
            Any = new XmlElement[]
            {
                payload.DocumentElement ??
                    throw new InvalidOperationException("Unable to access valid DocumentElement for payload")
            }
        };

        using var streamWriter = XmlWriter.Create(stream, new() { Encoding = Encoding.UTF8 });
        using var xmlWriter = XmlWriter.Create(streamWriter);

        _xmlSerializer.Serialize(xmlWriter, message, _emptyNamespaces);
    }

    public static GovTalkMessage Deserialize(Stream stream)
    {
        return _xmlSerializer.Deserialize(stream) as GovTalkMessage ??
            throw new ArgumentException("Unable to deserialize XML content into GovTalkMessage", nameof(stream));
    }

    private static XmlDocument GetPayload(IRtiSerializable rtiDocument)
    {
        // Write out document to memory stream
        var payloadMemoryStream = new MemoryStream();
        using var payloadStreamWriter = XmlWriter.Create(payloadMemoryStream, new() { Encoding = Encoding.UTF8 });
        using var payloadXmlWriter = XmlWriter.Create(payloadStreamWriter);

        rtiDocument.WriteXml(payloadXmlWriter);
        payloadXmlWriter.Close();

        // Read back document from memory stream so we can get the root XmlElement
        payloadMemoryStream.Seek(0, SeekOrigin.Begin);
        var payload = new XmlDocument();
        payload.Load(payloadMemoryStream);

        ApplyIRmark(payload, rtiDocument.DocumentNamespace);

        // Return the root element for insertion into the GovTalkMessage
        return payload;
    }

    private static void ApplyIRmark(XmlDocument payload, string documentNamespace)
    {
        var irMark = IRmarkGenerator.GetIRmark(payload);

        var irHeaderNode = payload.SelectSingleNode("//*[local-name()='IRenvelope']/*[local-name()='IRheader']") ??
            throw new ArgumentException("Unable to find <IRheader> element in supplied document", nameof(payload));

        var irHeaderSenderNode = payload.SelectSingleNode("//*[local-name()='IRenvelope']/*[local-name()='IRheader']/*[local-name()='Sender']") ??
            throw new ArgumentException("Unable to find <Sender> element within <IRheader> in supplied document", nameof(payload));

        var irMarkElement = payload.CreateElement("IRmark", documentNamespace);
        var typeAttribute = payload.CreateAttribute("Type");
        irMarkElement.InnerText = irMark;
        typeAttribute.Value = "generic";

        irMarkElement.Attributes.Append(typeAttribute);
        irHeaderNode.InsertBefore(irMarkElement, irHeaderSenderNode);
    }
}