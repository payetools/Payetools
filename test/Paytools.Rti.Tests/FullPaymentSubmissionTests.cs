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

namespace Paytools.Rti.Tests;

public class FullPaymentSubmissionTests
{
    [Fact]
    public void CreateBasicFps()
    {
        RtiDocumentFactory factory = new RtiDocumentFactory(new TaxYear(TaxYearEnding.Apr5_2023));

        var fps = factory.CreateFpsDocument();

        //IEmployer employer = RtiTestUtils.MakeEmployer();

        //IPayrunResult payrunResult = RtiTestUtils.MakePayrunResult(employer);

        //fps.Populate(payrunResult);

        //        return Encoding.UTF8.GetString(memoryStream.ToArray());

        //fps.WriteXml
    }

    private static string GetFpsXmlFor2023() =>
        @"?<?xml version=""1.0"" encoding=""utf-8""?>
<IRenvelope xmlns=""http://www.govtalk.gov.uk/taxation/PAYE/RTI/FullPaymentSubmission/22-23/1"">
  <IRheader>
    <PeriodEnd>2023-02-05</PeriodEnd>
    <Agent>
      <Company>SomeCorp Ltd</Company>
    </Agent>
    <Sender>Individual</Sender>
  </IRheader>
  <FullPaymentSubmission>
    <EmpRefs>
      <OfficeNo>100</OfficeNo>
      <PayeRef>AB12345</PayeRef>
      <AORef>123AB124323432</AORef>
    </EmpRefs>
  </FullPaymentSubmission>
</IRenvelope>";
}
