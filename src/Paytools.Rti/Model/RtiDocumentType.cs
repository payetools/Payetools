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

namespace Paytools.Rti.Model;

/// <summary>
/// 
/// </summary>
/// <remarks>These values are used elsewhere</remarks>
public enum RtiDocumentType
{
    EmployerPaymentSummary = 0,
    FullPaymentSubmission = 1,
    NinoVerificationRequest = 2
}

public static class RtiDocumentTypeExtensions
{
    private static readonly string[] MessageClasses = { "HMRC-PAYE-RTI-EPS", "HMRC-PAYE-RTI-FPS", "HMRC-PAYE-RTI-NVR" };

    public static string GetMessageClass(this RtiDocumentType documentType) =>
        documentType >= 0 && (int)documentType < MessageClasses.Length ? MessageClasses[(int)documentType] :
            throw new ArgumentException("Unrecognised Rti document type", nameof(documentType));
}