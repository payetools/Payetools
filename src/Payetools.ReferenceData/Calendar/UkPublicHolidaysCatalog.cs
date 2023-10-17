// Copyright (c) 2023 Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using System.Text.Json.Serialization;

namespace Payetools.ReferenceData.Calendar;

/// <summary>
/// Used to deserialise the public holiday dataset from gov.uk available at <see href="https://www.gov.uk/bank-holidays.json"/>.
/// </summary>
public record PublicHolidaysCatalog
{
    /// <summary>
    /// Gets the public holiday information for England and Wales.
    /// </summary>
    [JsonPropertyName("england-and-wales")]
    public CountryEntry EnglandAndWales { get; init; } = default!;

    /// <summary>
    /// Gets the public holiday information for Scotland.
    /// </summary>
    [JsonPropertyName("scotland")]
    public CountryEntry Scotland { get; init; } = default!;

    /// <summary>
    /// Gets the public holiday information for Scotland.
    /// </summary>
    [JsonPropertyName("northern-ireland")]
    public CountryEntry NorthernIreland { get; init; } = default!;
}

/// <summary>
/// Represents a data set of public holiday data for a specific country or countries within the UK.
/// </summary>
public class CountryEntry
{
    /// <summary>
    /// Gets the country division this data set applies to.
    /// </summary>
    [JsonPropertyName("division")]
    public string Division { get; init; } = default!;

    /// <summary>
    /// Gets a list of public holiday "events" as an array.
    /// </summary>
    [JsonPropertyName("events")]
    public Event[] Events { get; init; } = default!;
}

/// <summary>
/// Represents a public holiday "event".
/// </summary>
public class Event
{
    /// <summary>
    /// Gets the description of this public holiday event.
    /// </summary>
    [JsonPropertyName("title")]
    public string Description { get; init; } = default!;

    /// <summary>
    /// Gets the date of this public holiday event.
    /// </summary>
    [JsonPropertyName("date")]
    public DateOnly Date { get; init; } = default!;

    /// <summary>
    /// Gets any notes associated with this public holiday event.
    /// </summary>
    [JsonPropertyName("notes")]
    public string Notes { get; init; } = default!;

    /// <summary>
    /// Gets a value indicating whether "bunting" for this public holiday event
    /// is true or false.  Who knows what this means...
    /// </summary>
    [JsonPropertyName("bunting")]
    public bool Bunting { get; init; }
}