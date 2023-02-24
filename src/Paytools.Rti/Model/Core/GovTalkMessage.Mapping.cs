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

using Paytools.Common.Model;
using Paytools.Rti.Extensions;
using Paytools.Rti.Model.Core;
using System.Xml;
using System.Xml.Serialization;

namespace Paytools.Rti.Model;

public partial class GovTalkMessageGovTalkDetailsKey : ITypeValuePair
{
}

public partial class GovTalkMessage
{
    [XmlIgnore]
    private IRtiSerializable Payload { get; init; } = null!;

    public static string Namespace => "http://www.govtalk.gov.uk/CM/envelope";

    public void Serialize(Stream stream)
    {
        RtiDocumentSerializer.Serialize(stream, this, Payload);
    }

    public static GovTalkMessage Create(string messageClass,
        HmrcPayeReference hmrcPayeReference,
        IRtiSerializable rtiDocument)
    {
        return new GovTalkMessage()
        {
            Payload = rtiDocument,
            EnvelopeVersion = "2.0",
            Header = new GovTalkMessageHeader()
            {
                MessageDetails = new GovTalkMessageHeaderMessageDetails()
                {
                    Class = messageClass,
                    Qualifier = GovTalkMessageHeaderMessageDetailsQualifier.request,
                    Function = GovTalkMessageHeaderMessageDetailsFunction.submit,
                    TransactionID = "124342342423423423",
                    Transformation = GovTalkMessageHeaderMessageDetailsTransformation.XML,
                    GatewayTest = "1"
                },
                SenderDetails = new GovTalkMessageHeaderSenderDetails()
                {
                    X509Certificate = new byte[] { },
                    IDAuthentication = new GovTalkMessageHeaderSenderDetailsIDAuthentication()
                    {
                        SenderID = "1",
                        Authentication = new[] {
                        new GovTalkMessageHeaderSenderDetailsIDAuthenticationAuthentication()
                        {
                            Method = GovTalkMessageHeaderSenderDetailsIDAuthenticationAuthenticationMethod.clear,
                            Role = "principal",
                            Item = ""
                        }
                    }
                    },
                    EmailAddress = "a@b.c"
                }
            },
            GovTalkDetails = new GovTalkMessageGovTalkDetails()
            {
                Keys = hmrcPayeReference.ToTypeValuePairs<GovTalkMessageGovTalkDetailsKey>(),
                ChannelRouting = new[]
                {
                    new GovTalkMessageGovTalkDetailsChannelRouting()
                    {
                        Channel = new GovTalkMessageGovTalkDetailsChannelRoutingChannel()
                        {
                            Product = "Paytools",
                            Item = "7373",
                            Version = "1.0"
                        }

                    }
                }
            }
        };
    }

    public IRtiResponse? ExtractResponse()
    {
        var body = this.Body.Any[0];

        var name = body.Name;
        return name switch
        {
            "ErrorResponse" => Deserialize<ErrorResponse>(body),
            "SuccessResponse" => Deserialize<SuccessResponse>(body),
            _ => null
        };
    }


    public static GovTalkMessage Deserialize(Stream stream) =>
        RtiDocumentSerializer.Deserialize(stream);

    private static T Deserialize<T>(XmlNode data) where T : class, new()
    {
        var ser = new XmlSerializer(typeof(T));

        using var xmlNodeReader = new XmlNodeReader(data);

        return ser.Deserialize(xmlNodeReader) as T ??
            throw new ArgumentException($"Unable to deserialize object of type {typeof(T).Name}", nameof(data));
    }
}
