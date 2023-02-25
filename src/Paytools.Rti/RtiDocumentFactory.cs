// Copyright (c) 2023 Paytools Foundation.
//
// Licensed under the Apache License, Version 2.0 (the "License") ~
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
using Paytools.Payroll.Payruns;
using Paytools.Rti.Model;
using System.Reflection;
using System.Xml.Serialization;

namespace Paytools.Rti;

public class RtiDocumentFactory : IRtiDocumentFactory
{
    private readonly IRtiDocumentFactory _factoryForTaxYear;

    static RtiDocumentFactory()
    {
        // This code needs to be here because if it isn't executed before any RTI type is created, the runtime 
        // throws a PlatformNotSupportedException with the message "Compiling JScript/CSharp scripts is not supported"
        // when attempting to create the serializer for the target type.  See https://github.com/dotnet/wcf/issues/2219.
        MethodInfo method = typeof(XmlSerializer).GetMethod("set_Mode", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static) ??
            throw new InvalidOperationException("Unable to find set_Mode method");
        method.Invoke(null, new object[] { 1 });
    }

    public RtiDocumentFactory(TaxYear taxYear)
    {
        _factoryForTaxYear = GetFactoryForTaxYear(taxYear.TaxYearEnding);
    }

    public RtiDocumentFactory(PayDate payDate)
        : this(payDate.TaxYear)
    {
    }

    public IRtiDocument<IPayrunResult> CreateFpsDocument() =>
        _factoryForTaxYear.CreateFpsDocument();

    public IRtiDocument<object> CreateEpsDocument() =>
        _factoryForTaxYear.CreateEpsDocument();

    public IRtiDocument<object> CreateNvrDocument() =>
        _factoryForTaxYear.CreateNvrDocument();

    private static IRtiDocumentFactory GetFactoryForTaxYear(TaxYearEnding taxYearEnding)
    {
        var targetNamespace = $"Paytools.Rti.Model._{taxYearEnding.YearAsString()}";
        var targetTypeName = $"{targetNamespace}.RtiDocumentFactoryForTaxYear";

        Assembly thisAssembly = Assembly.GetExecutingAssembly();

        Type factoryType = thisAssembly.GetType(targetTypeName) ??
            throw new InvalidOperationException($"Unable find factory type '{targetTypeName}'");

        return (IRtiDocumentFactory)(Activator.CreateInstance(factoryType) ??
            throw new InvalidOperationException($"Unable create instance of factory type '{targetTypeName}'"));
    }
}