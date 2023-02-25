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

using Paytools.Payroll.Payruns;

namespace Paytools.Rti.Model._2023;

internal class RtiDocumentFactoryForTaxYear : IRtiDocumentFactory
{
    public IRtiDocument<object> CreateEpsDocument()
    {
        throw new NotImplementedException();
    }

    public IRtiDocument<IPayrunResult> CreateFpsDocument()
    {
        var fps = new IRenvelope<FullPaymentSubmission>();

        fps.IRheader = new IRheader()
        {

        };

        return new RtiDocument<IPayrunResult, IRenvelope<FullPaymentSubmission>>(RtiDocumentType.FullPaymentSubmission, fps); ;
    }

    public IRtiDocument<object> CreateNvrDocument()
    {
        throw new NotImplementedException();
    }
}