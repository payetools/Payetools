﻿// Copyright (c) 2023 Paytools Foundation.  All rights reserved.
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

using Paytools.NationalInsurance;
using Paytools.NationalInsurance.ReferenceData;

namespace Paytools.ReferenceData.NationalInsurance;

public record NiThresholdEntry : INiThresholdEntry
{
    public NiThresholdType ThresholdType { get; init; }
    public decimal ThresholdValuePerWeek { get; init; }
    public decimal ThresholdValuePerMonth { get; init; }
    public decimal ThresholdValuePerYear { get; init; }
}
