// Copyright (c) 2023 Paytools Foundation.  All rights reserved.
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

using Paytools.IncomeTax.ReferenceData;
using Paytools.NationalInsurance.ReferenceData;

namespace Paytools.ReferenceData;

/// <summary>
/// Interface that HMRC reference data providers must implement.
/// </summary>
public interface IHmrcReferenceDataProvider : 
    ITaxReferenceDataProvider,
    INiReferenceDataProvider
{
    string Health { get; }
}