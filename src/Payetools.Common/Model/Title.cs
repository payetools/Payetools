﻿// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.Common.Model;

/// <summary>
/// Represents an individual's title (e.g., Mr., Mrs).
/// </summary>
/// <remarks>Some ideas on standardisation sourced from
/// <see href="https://assets.publishing.service.gov.uk/government/uploads/system/uploads/attachment_data/file/1112942/Titles__V12_.pdf"/>.<br/><br/>
/// As per general Government guidance, no attempt is made to deduce a person's gender from their title.
/// </remarks>
public readonly struct Title
{
    private static readonly IReadOnlyDictionary<string, string> _standardTitles = new Dictionary<string, string>()
    {
        { "ms", "Ms" },
        { "miss", "Miss" },
        { "mr", "Mr" },
        { "mr.", "Mr" },
        { "mrs", "Mrs" },
        { "mrs.", "Mrs" },
        { "dr", "Dr." },
        { "dr.", "Dr." },
        { "doctor", "Dr." },
        { "rev", "Rev." },
        { "rev.", "Rev." },
        { "reverend", "Rev." },
        { "revd", "Rev." },
        { "revd.", "Rev." },
        { "prof", "Prof." },
        { "prof.", "Prof." },
        { "professor", "Prof." }
    };

    private readonly string _title;

    private Title(in string title)
    {
        _title = title;
    }

    /// <summary>
    /// Implicit cast from Title to string.
    /// </summary>
    /// <param name="title">Title to obtain the string representation of.</param>
    public static implicit operator string(in Title title) => title.ToString();

    /// <summary>
    /// Implicit cast from string to Title.
    /// </summary>
    /// <param name="title">String title to obtain a Title instance for.</param>
    /// <exception cref="ArgumentException">Thrown if the title exceeds 35 characters in length.</exception>
    public static implicit operator Title?(in string title) => Title.Parse(title);

    /// <summary>
    /// Inspects the supplied title and returns a new <see cref="Title"/> instance holding either the
    /// title supplied, or if it is a standard title (e.g., Mr, Mrs, Miss, etc.) then the standardised
    /// form of that title.
    /// </summary>
    /// <param name="title">Externally supplied string value for title.</param>
    /// <returns>Null if no title provided, a standardised title (e.g., "Mr") if a standardised title
    /// is provided, or the supplied string otherwise.</returns>
    public static Title? Parse(in string? title)
    {
        if (string.IsNullOrWhiteSpace(title))
            return null;

        if (_standardTitles.TryGetValue(title.ToLowerInvariant(), out var standardTitle))
            return new Title(standardTitle);

        if (title.Length > 35)
            throw new ArgumentException("Titles may not exceed 35 characters in length", nameof(title));

        return new Title(title);
    }

    /// <summary>
    /// Gets the string representation of the Title.
    /// </summary>
    /// <returns>String representation of title, e.g., "Mr", "Ms".</returns>
    public override string ToString() => _title ?? string.Empty;
}