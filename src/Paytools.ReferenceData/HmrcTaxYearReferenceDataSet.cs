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

using Paytools.Common.Model;
using Paytools.Common.Serialization;
using Paytools.ReferenceData.IncomeTax;
using Paytools.ReferenceData.NationalInsurance;
using Paytools.ReferenceData.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Paytools.ReferenceData;

/// <summary>
/// Data structure used to represent HMRC reference data for a given tax year.
/// </summary>
public  class HmrcTaxYearReferenceDataSet
{
    public TaxYearEnding ApplicableTaxYearEnding { get; init; }

    public IReadOnlyList<IncomeTaxBandSet> IncomeTax { get; init; }

    public IReadOnlyList<NiReferenceDataEntry> NationalInsurance { get; init; }

    public static HmrcTaxYearReferenceDataSet Load(Stream jsonContent)
    {
        var referenceData = JsonSerializer.Deserialize<HmrcTaxYearReferenceDataSet>(jsonContent, new JsonSerializerOptions()
        {
            // See https://github.com/dotnet/runtime/issues/31081 on why we can't just use JsonStringEnumConverter
            Converters =
            {
                new PayFrequencyJsonConverter(),
                new CountriesForTaxPurposesJsonConverter(),
                new TaxYearEndingJsonConverter(),
                new DateOnlyJsonConverter(),
                new NiThresholdTypeJsonConverter()
            },
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        return referenceData;
    }
}
