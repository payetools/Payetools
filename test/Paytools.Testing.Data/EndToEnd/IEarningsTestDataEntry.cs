// Copyright (c) 2023 Paytools Foundation.
//
// Licensed under the Apache License, Version 2.0 (the "License");
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

using System;
using Paytools.Common.Model;

namespace Paytools.Testing.Data.EndToEnd;

public interface IEarningsTestDataEntry
{
    string DefinitionVersion { get; }
    string TestIdentifier { get; }
    TaxYearEnding TaxYearEnding { get; }
    string ShortName { get; }
    string Description { get; }
    bool IsSubjectToTax { get; }
    bool IsSubjectToNi { get; }
    bool IsPensionable { get; }
    bool IsNetToGross { get; }
    bool IsTreatedAsOvertime { get; }
}
