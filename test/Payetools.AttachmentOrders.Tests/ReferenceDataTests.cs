// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.AttachmentOrders.Model;
using Payetools.Common.Model;
using Payetools.ReferenceData;
using Payetools.Testing.Utils;
using Shouldly;

namespace Payetools.AttachmentOrders.Tests;

public class ReferenceDataTests
{
    private static readonly string[] _resourcePaths =
    [
        @"Resources\attachment-orders-reference-data.json"
    ];

    [Fact]
    public async Task VerifyReferenceDataLoad()
    {
        var streams = _resourcePaths.Select(p => Resource.Load(p)).ToArray();

        var provider = await ReferenceDataHelper.CreateProviderAsync<IHmrcReferenceDataProvider>(streams);

        var rateTable = provider.GetAllAttachmentOrderEntries(new TaxYear(TaxYearEnding.Apr5_2026));
            //CountriesForTaxPurposes.England,
            //AttachmentOrderCalculationType.TableBasedPercentageOfEarnings,
            //new DateOnly(2023, 1, 1));

        // rateTable.ShouldNotBeNull();

        // rateTable.Value.Length.ShouldBe(7);
    }
}
