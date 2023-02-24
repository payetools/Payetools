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

using Paytools.Common.Extensions;
using Paytools.Payroll;
using Paytools.Rti.Extensions;
using System.Xml;
using System.Xml.Serialization;

namespace Paytools.Rti.Model._2023;

public partial class IRheaderKey : ITypeValuePair
{
}

public partial class IRenvelope<T> : IRtiDocumentForTaxYear where T : IRtiDataTarget, new()
{
    private static readonly XmlSerializer _xmlSerializer;
    private static readonly XmlSerializerNamespaces _emptyNamespaces;

    public string DocumentNamespace => Namespace;

    static IRenvelope()
    {
        _xmlSerializer = new XmlSerializer(typeof(IRenvelope<T>), GetXmlAttributeOverrides(Namespace));
        _emptyNamespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName() });
    }

    public IRenvelope()
    {
        IRheader = new IRheader();
        //IRheader.Agent = new IRheaderAgent();
        //IRheader.Agent.Company = "SomeCorp Ltd";
        //IRheader.PeriodEnd = new DateTime(2023, 2, 5, 12, 0, 0, DateTimeKind.Utc);
        //IRheader.IRmark = new IRheaderIRmark() { Type = IRheaderIRmarkType.generic };
    }

    public void Populate<Tsource>(Tsource data, DateOnly? periodEnd = null) where Tsource : class, IEmployerInfoProvider
    {
        IRheader.PeriodEnd = periodEnd?.ToMiddayUtcDateTime() ?? DateTime.UtcNow.MiddayUtc();
        IRheader.Keys = data.Employer.HmrcPayeReference?.ToTypeValuePairs<IRheaderKey>();

        Content.Populate(data);
    }

    public void ReadXml(XmlReader reader)
    {
        throw new NotImplementedException();
    }

    public void WriteXml(XmlWriter writer)
    {
        _xmlSerializer.Serialize(writer, this, _emptyNamespaces);
    }

    private static string Namespace =>
        typeof(T) switch
        {
            var type when type == typeof(EmployerPaymentSummary) => "http://www.govtalk.gov.uk/taxation/PAYE/RTI/EmployerPaymentSummary/22-23/1",
            var type when type == typeof(FullPaymentSubmission) => "http://www.govtalk.gov.uk/taxation/PAYE/RTI/FullPaymentSubmission/22-23/1",
            var type when type == typeof(NINOverificationRequest) => "http://www.govtalk.gov.uk/taxation/PAYE/RTI/NINOverificationRequest/1",
            _ => throw new InvalidOperationException("Unable to obtain namespace for target type")
        };

    private static XmlAttributeOverrides GetXmlAttributeOverrides(string targetNamespace)
    {
        var rootAttrs = new XmlAttributes
        {
            XmlRoot = new XmlRootAttribute() { ElementName = "IRenvelope", Namespace = targetNamespace, IsNullable = false },
            XmlType = new XmlTypeAttribute() { AnonymousType = true, Namespace = targetNamespace }
        };

        var leafAttrs = new XmlAttributes
        {
            XmlType = new XmlTypeAttribute() { AnonymousType = true, Namespace = targetNamespace }
        };

        var attributeOverrides = new XmlAttributeOverrides();

        // Add root overrides
        attributeOverrides.Add(typeof(IRenvelope<T>), rootAttrs);

        // Add leaf overrides
        attributeOverrides.Add(typeof(IRheader), leafAttrs);
        attributeOverrides.Add(typeof(IRheaderKey), leafAttrs);
        attributeOverrides.Add(typeof(IRheaderIRmark), leafAttrs);
        attributeOverrides.Add(typeof(IRheaderIRmarkType), leafAttrs);
        attributeOverrides.Add(typeof(IRheaderSender), leafAttrs);
        attributeOverrides.Add(typeof(IRheaderPrincipal), leafAttrs);
        attributeOverrides.Add(typeof(IRheaderAgent), leafAttrs);
        attributeOverrides.Add(typeof(IRheaderAgentAddress), leafAttrs);
        attributeOverrides.Add(typeof(IRheaderDefaultCurrency), leafAttrs);
        attributeOverrides.Add(typeof(IRheaderManifest), leafAttrs);
        attributeOverrides.Add(typeof(IRheaderManifestReference), leafAttrs);

        return attributeOverrides;
    }
}
