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
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace Paytools.Rti;

public static class IRmarkGenerator
{
    public static string GetIRmark(XmlDocument payload)
    {
        var documentWithBody = GetDocumentWithBody(payload);

        var transform = new XmlDsigExcC14NTransform();

        transform.LoadInput(documentWithBody);

        var output = transform.GetDigestedOutput(SHA1.Create());

        return Convert.ToBase64String(output);
    }

    private static XmlDocument GetDocumentWithBody(XmlDocument payload)
    {
        var bodyDocument = new XmlDocument();
        
        bodyDocument.AppendChild(bodyDocument.CreateElement("Body", GovTalkMessage.Namespace));

        XmlNode importedNode = bodyDocument.ImportNode(payload.DocumentElement ??
            throw new ArgumentException("Unable to create document with <Body> as root node; payload document lacks content", nameof(payload)),
            true);

        bodyDocument.DocumentElement?.AppendChild(importedNode);

        return bodyDocument;
    }
}