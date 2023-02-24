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

using Paytools.Payroll;
using System.Xml;

namespace Paytools.Rti.Model;

public class RtiDocument<Tsource, Ttarget> : IRtiDocument<Tsource> where Ttarget : IRtiDocumentForTaxYear where Tsource : class, IEmployerInfoProvider
{
    private readonly Ttarget _target;

    public RtiDocumentType RtiDocumentType { get; init; }

    public RtiDocument(RtiDocumentType rtiDocumentType, Ttarget target)
    {
        RtiDocumentType = rtiDocumentType;
        _target = target;
    }

    public void Populate(Tsource sourceData)
    {
        _target.Populate(sourceData);
    }

    public void WriteXml(XmlWriter writer)
    {
        _target.WriteXml(writer);
    }

    public void ReadXml(XmlReader reader)
    {
        throw new NotImplementedException();
    }

    public string DocumentNamespace => _target.DocumentNamespace;
}